//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.Loans.GenerateEvents
{
    public static class Accrual
    {
        private const int AVERAGE_NB_OF_DAYS_IN_MONTH = 30;
        private const int PAST_DUE_MIN_DAYS = 3;

        public static LoanDisbursmentEvent GenerateLoanDisbursmentEvent(Loan pLoan, ApplicationSettings pGeneralSettings, DateTime pDisburseDate, bool pAlignInstallmentsDatesOnRealDisbursmentDate, bool pDisableFees, User pUser)
        {
            if (pAlignInstallmentsDatesOnRealDisbursmentDate)
            {
                //pLoan.StartDate = pDisburseDate;
                //for (int i = 1; i <= pLoan.NbOfInstallments; i++)
                //{
                //    pLoan.InstallmentList[i - 1].ExpectedDate = pLoan.CalculateInstallmentDate(pLoan.StartDate, i);
                //}
            }
            else
            {
                if (pGeneralSettings.PayFirstInterestRealValue)
                {
                    TimeSpan time = pDisburseDate - pLoan.StartDate;
                    int diffDays = Math.Abs(time.Days);

                    int nbOfDaysInPeriod = pLoan.InstallmentType.NbOfMonths * AVERAGE_NB_OF_DAYS_IN_MONTH + pLoan.InstallmentType.NbOfDays;

                    if (pDisburseDate.CompareTo(pLoan.StartDate) < 0)
                        pLoan.GetInstallment(0).InterestsRepayment += (Convert.ToDecimal(pLoan.InterestRate) * diffDays * pLoan.Amount / (double)nbOfDaysInPeriod);
                    else
                        pLoan.GetInstallment(0).InterestsRepayment -= (Convert.ToDecimal(pLoan.InterestRate) * diffDays * pLoan.Amount / (double)nbOfDaysInPeriod);

                    if (AmountComparer.Compare(pLoan.GetInstallment(0).InterestsRepayment, 0) < 0)
                    {
                        pLoan.GetInstallment(0).InterestsRepayment = 0;
                    }
                    pLoan.GetInstallment(0).InterestsRepayment = Math.Round(pLoan.GetInstallment(0).InterestsRepayment.Value, 2);
                }
            }

            pLoan.Disbursed = true;
            LoanDisbursmentEvent lDE = !pDisableFees
                                           ? new LoanDisbursmentEvent
                                                 {
                                                     Date = pDisburseDate,
                                                     Amount = pLoan.Amount,
//                                                     Commissions = pLoan.CalculateEntryFeesAmount(),
                                                     ClientType = pLoan.ClientType
                                                 }
                                           : new LoanDisbursmentEvent
                                                 {
                                                     Date = pDisburseDate,
                                                     Amount = pLoan.Amount,
                                                     Commissions = null,
                                                     ClientType = pLoan.ClientType
                                                 };
            //_book(pLoan, lDE, pUser);
            pLoan.Events.Add(lDE);
            return lDE;
        }

        public static bool CheckDegradeToBadLoan(Loan pLoan, ApplicationSettings settings, ProvisionTable pProvisionTable, DateTime pDate, bool authorizeSeveralPastDueEventByPeriod, int? pPstDueDays, User pUser)
        {
            DateTime date;
            int pastDueDays;
            if (pPstDueDays.HasValue)
            {
                pastDueDays = pPstDueDays.Value;
                date = pDate.AddDays(pastDueDays - pLoan.GetPastDueDays(pDate));
            }
            else
            {
                date = pDate;
                pastDueDays = pLoan.GetPastDueDays(date);
            }
            //In this case, the loan is not considered as bad loan
            if (pastDueDays < PAST_DUE_MIN_DAYS)
            {
                return false;
            }

            //A loan is only degraded from cash credit to bad loan one time
            OCurrency cashBalance = 0;//pLoan.ChartOfAccounts.GetAccountByNumber(OAccounts.CASH_CREDIT, pLoan.Product.Currency.Id, pLoan, OBookingDirections.Both).Balance;
            if (cashBalance != 0)
            {
                pLoan.BadLoan = true;
            }
            else
            {
                //Downgrading the loan to unrecoverable bad loans is done once for loan with past due days greater than 180
                if (pastDueDays > settings.BadLoanDays)
                {
                    pLoan.BadLoan = true;
                }
            }

            return true;
        }

        public static OCurrency CalculateRemainingInterests(Loan pLoan, DateTime pDate)
        {
            OCurrency amount = 0;
            foreach (Installment installment in pLoan.InstallmentList)
            {
                if (installment.IsRepaid) continue;
                if(installment.ExpectedDate < pDate)
                {
                    amount += installment.InterestsRepayment - installment.PaidInterests;
                }
                else if(installment.ExpectedDate == pDate)
                {
                    amount += installment.InterestsRepayment - installment.PaidInterests;

                    return Math.Round(amount.Value, 2);
                }
                else
                {
                    DateTime date = installment.Number == 1 ? pLoan.StartDate : pLoan.GetInstallment(installment.Number - 2).ExpectedDate;

                    int days;
                    int daysInInstallment = pLoan.NumberOfDaysInTheInstallment(installment.Number, pDate);

                    if (installment.Number != 1)
                    {
                        if(pLoan.GetInstallment(installment.Number - 2).IsRepaid && pLoan.GetInstallment(installment.Number - 2).ExpectedDate > pDate)
                        {
                            days = 0;
                        }
                        else
                        {
                            if (pLoan.GetLastRepaymentDate() > date)
                            {
                                date = pLoan.GetLastRepaymentDate();
                                daysInInstallment = (installment.ExpectedDate - date).Days;
                            }
                            days = (pDate - date).Days;
                        }
                    }
                    else
                        days = (pDate - date).Days;

                    amount += days >= pLoan.NumberOfDaysInTheInstallment(installment.Number, pDate)
                                             ? installment.InterestsRepayment
                                             : installment.InterestsRepayment * (double)days / (double) daysInInstallment;
                    
                    if (installment.PaidInterests > amount)
                        amount = 0;

                    if (installment.PaidInterests < amount)
                        amount -= installment.PaidInterests;

                    return amount.Value;
                }
            }
            return amount.Value;
        }

        private static void _book(Loan pLoan, Event e, User pUser)
        {
            //IEventProcessor eP = new Events.Accrual.EventProcessor(pUser, pLoan.ChartOfAccounts, pLoan);

            //pLoan.Events.Add(e);
            
            //AccountingTransaction mS = eP.FireEvent(e, pLoan.Product.Currency.Id);

            //if (mS != null)
            //{
            //    pLoan.ChartOfAccounts.Book(mS);
            //    pLoan.Bookings = mS.Bookings;
            //}
        }

        //public static void BookFundingLineEvent(FundingLine pFLine, FundingLineEvent e, User pUser)
        //{
        //   IEventProcessor eP = new Events.Accrual.EventProcessor(pUser, pFLine.FundingLineChartOfAccounts);
        //   AccountingTransaction mS = eP.FireEvent(e, pFLine.Currency.Id);
        //   if (mS != null) pFLine.FundingLineChartOfAccounts.Book(mS);
        //}
    }
}
