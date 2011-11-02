//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.CalculateFeesForAnticipatedRepayment.BaseToCalculateFeesForAnticipatedRepayment;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.MaxRepayment
{
    /// <summary>
    /// Summary description for MaximumAmountAuthorizedToRepayStrategy.
    /// </summary>
    [Serializable]
    public class CalculateMaximumAmountToRepayStrategy
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cCo;
        private readonly User _user;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWDS;

        public CalculateMaximumAmountToRepayStrategy(CreditContractOptions pCCo, Loan pContract, User pUser, 
            ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _user = pUser;
            _generalSettings = pGeneralSettings;
            _nWDS = pNonWorkingDate;

            _contract = pContract;
            _cCo = pCCo;
            _user = pUser;
        }

        public OCurrency CalculateMaximumAmountAuthorizedToRepay(DateTime pDate)
        {
            OCurrency amount = 0;
            //capital
            amount += _contract.CalculateActualOlb();

            //interest
            if (_cCo.CancelInterests)
                amount += _cCo.ManualInterestsAmount;

            else
            {
                //calculate Remaining Interests when loan is calculated on it
                if (_cCo.LoansType == OLoanTypes.Flat && !_cCo.KeepExpectedInstallments)
                {
                    amount += _contract.CalculateRemainingInterests(pDate);
                }
                else if (_cCo.LoansType != OLoanTypes.Flat && _cCo.LoansType != OLoanTypes.DecliningFixedPrincipal &&
                         !_cCo.KeepExpectedInstallments)
                {
                    amount += _contract.CalculateRemainingInterests(pDate);
                }
                else
                {
                    if (_cCo.LoansType == OLoanTypes.DecliningFixedPrincipal && !_cCo.KeepExpectedInstallments)
                    {
                        DateTime? installmentDate = null;

                        foreach (Installment installment in _contract.InstallmentList)
                        {
                            if (!installment.IsRepaid && installmentDate == null &&
                                (installment.ExpectedDate - pDate).Days > 0)
                                installmentDate = installment.ExpectedDate;
                        }

                        if (installmentDate > pDate)
                            amount += _contract.CalculateRemainingInterests(pDate);
                        else
                            amount += _contract.CalculateRemainingInterests();
                    }
                    else
                        amount += _contract.CalculateRemainingInterests();
                }
            }

            //commission
            if (_cCo.CancelFees)
            {
                amount += _cCo.ManualFeesAmount;
                amount += _cCo.ManualCommissionAmount;
            }
            else
                amount += _CalculateLateAndAnticipatedFees(pDate);

            return _contract.UseCents ? amount.Value : Math.Round(amount.Value, 0 , MidpointRounding.AwayFromZero);
        }

        public OCurrency CalculateMaximumAmountForEscapedMember(DateTime pDate)
        {
            Installment installment = null;
            OCurrency interests = 0;
            OCurrency commission = 0;
            OCurrency aTprComission = 0;
            
            Installment priorInstallment;
            bool calculated = false;
            Loan contract = _contract.Copy();

            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment getInstallment = contract.GetInstallment(i);

                if (!getInstallment.IsRepaid && getInstallment.ExpectedDate > pDate || (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null))
                {
                    if (installment == null)
                    {
                        installment = getInstallment;

                        if (_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                        {
                            DateTime expectedDate;
                            int daysInTheInstallment = contract.NumberOfDaysInTheInstallment(installment.Number, pDate);

                            if (i == 0)
                            {
                                expectedDate = contract.StartDate;
                            }
                            else
                            {
                                priorInstallment = contract.GetInstallment(i - 1);
                                expectedDate = priorInstallment.ExpectedDate;

                                if (contract.GetLastRepaymentDate() > expectedDate)
                                {
                                    expectedDate = contract.GetLastRepaymentDate();
                                    daysInTheInstallment = (installment.ExpectedDate - expectedDate).Days;
                                }
                            }

                            if (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null)
                            {
                                expectedDate = getInstallment.ExpectedDate;
                            }

                            int daySpan = (pDate - expectedDate).Days < 0 ? 0 : (pDate - expectedDate).Days;

                            if (contract.EscapedMember != null)
                            {
                                //calculate new interes for the person of the group
                                OCurrency amount = contract.Product.LoanType == OLoanTypes.Flat
                                                  ? contract.Amount
                                                  : contract.GetOlb();

                                if (daySpan != 0)
                                {
                                    interests = (amount*contract.EscapedMember.LoanShareAmount/contract.Amount)*daySpan/
                                                daysInTheInstallment*contract.InterestRate;
                                }
                                else
                                {
                                    interests = (amount*contract.EscapedMember.LoanShareAmount/contract.Amount)*
                                                contract.InterestRate;
                                }
                            }
                            else
                            {
                                interests = installment.InterestsRepayment * daySpan / daysInTheInstallment;
                            }

                        }
                        else
                        {
                            interests = installment.InterestsRepayment == installment.PaidInterests
                                            ? installment.InterestsRepayment
                                            : (installment.ExpectedDate > pDate ? 0 : installment.InterestsRepayment);

                            if (contract.EscapedMember != null)
                            {
                                interests = interests*contract.EscapedMember.LoanShareAmount/contract.Amount;
                            }
                        }
                    }

                    commission +=
                        new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user,
                                                                                  _generalSettings, _nWDS).
                            CalculateCommision(pDate, getInstallment.Number, OPaymentType.TotalPayment, 0, ref calculated);

                    if (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        aTprComission = commission;

                    if (getInstallment.ExpectedDate > pDate && _contract.EscapedMember != null && aTprComission > 0 && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        commission = aTprComission;

                    if (_cCo.ManualFeesAmount > 0)
                    {
                        commission = _cCo.ManualFeesAmount;
                    }

                    foreach (RepaymentEvent rPayment in _contract.Events.GetRepaymentEvents())
                    {
                        if (rPayment.Date == pDate && installment.Number == rPayment.InstallmentNumber)
                        {
                            installment.FeesUnpaid = 0;
                        }
                    }
                }
            }

            if (contract.EscapedMember != null)
            {
                OCurrency amount = interests + commission +
                                   contract.GetOlb()*contract.EscapedMember.LoanShareAmount/contract.Amount;
                return _contract.UseCents
                           ? Math.Round(amount.Value, 2, MidpointRounding.AwayFromZero)
                           : Math.Round(amount.Value, 0, MidpointRounding.AwayFromZero);
            }
             return 0;
        }

        private OCurrency _CalculateLateAndAnticipatedFees(DateTime pDate)
        {
            OCurrency fees = 0;
            Loan contract = _contract.Copy();
            new Repayment.RepayLateInstallments.CalculateInstallments(_cCo, contract, _user, _generalSettings,_nWDS).CalculateNewInstallmentsWithLateFees(pDate);
            for (int i = 0; i < contract.NbOfInstallments; i++)
            {
                Installment installment = contract.GetInstallment(i);
                if (!installment.IsRepaid && installment.ExpectedDate <= pDate)
                {
                    fees += installment.FeesUnpaid;
                    installment.PaidCapital = installment.CapitalRepayment;
                    installment.PaidInterests = installment.InterestsRepayment;
                }
            }
            if (!_cCo.KeepExpectedInstallments)
                fees += new CalculationBaseForAnticipatedFees(_cCo, contract).CalculateFees(pDate);

            return _contract.UseCents ? Math.Round(fees.Value, 2, MidpointRounding.AwayFromZero) : Math.Round(fees.Value, 0, MidpointRounding.AwayFromZero);
        }
    }
}