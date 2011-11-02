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
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments
{
    /// <summary>
    /// Summary description for CalculateInstallments.
    /// </summary>
    [Serializable]
    public class CalculateInstallments
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cCo;
        private readonly RepayFeesStrategy _methodToRepayFees;
        private readonly RepayCommisionStrategy _methodToRepayCommission;

        private readonly RepayInterestStrategy _methodToRepayInterest;
        private readonly ApplicationSettings _generalSettings;
        private readonly NonWorkingDateSingleton _nWds;

        public List<Installment> PaidIstallments;

        public CalculateInstallments(CreditContractOptions pCCO, Loan pContract, User pUser, ApplicationSettings pGeneralSettings,NonWorkingDateSingleton pNonWorkingDate)
        {
            _generalSettings = pGeneralSettings;
            _nWds = pNonWorkingDate;
            _contract = pContract;
            _cCo = pCCO;
            _methodToRepayFees = new RepayFeesStrategy(pCCO);
            _methodToRepayInterest = new RepayInterestStrategy(pCCO);
            _methodToRepayCommission = new RepayCommisionStrategy(pCCO);
            PaidIstallments = new List<Installment>();
        }

        public void CalculateNewInstallmentsWithLateFees(DateTime pDate)
        {
            if(!_cCo.CancelFees)
            {
                bool firstInstallmentNotRepaid = false;
                for(int i = 0; i< _contract.NbOfInstallments; i++)
                {
                    Installment installment = _contract.GetInstallment(i);
                    
                        if (!installment.IsRepaid && !firstInstallmentNotRepaid)
                        {
                                firstInstallmentNotRepaid = true;
                                int isPaidPenalties = 1;

                                foreach (RepaymentEvent rPayment in _contract.Events.GetRepaymentEvents())
                                {
                                    if ((rPayment.Date == pDate && rPayment.Deleted == false) ||
                                        (installment.AmountHasToPayWithInterest != 0 && installment.PaidCapital != 0))
                                    {
                                        isPaidPenalties = 0;
                                    }

                                    if (installment.Number >= rPayment.InstallmentNumber && rPayment.Penalties != 0 &&
                                        rPayment.Date < pDate && installment.PaidFees != 0)
                                    {
                                        isPaidPenalties = 0;
                                    }
                                }

                             bool doNotCalculateNewFees = false;
                             foreach (RepaymentEvent rPayment in _contract.Events.GetRepaymentEvents())
                             {
                                 if (installment.FeesUnpaid !=0 && rPayment.Date == pDate && rPayment.Deleted == false && installment.PaidInterests == 0 && installment.PaidCapital == 0)
                                 {
                                     doNotCalculateNewFees = true;
                                 }
                             }

                             if (!doNotCalculateNewFees)
                             {
                                 installment.FeesUnpaid += 
                                     CalculationBaseForLateFees.FeesBasedOnOverduePrincipal(_contract, pDate, installment.Number, false, _generalSettings, _nWds)
                                    + CalculationBaseForLateFees.FeesBasedOnOverdueInterest(_contract, pDate, installment.Number, false, _generalSettings, _nWds)
                                    + CalculationBaseForLateFees.FeesBasedOnInitialAmount(_contract, pDate, installment.Number, false, _generalSettings, _nWds)
                                    + CalculationBaseForLateFees.FeesBasedOnOlb(_contract, pDate, installment.Number, false, _generalSettings, _nWds);

                                 installment.FeesUnpaid -= installment.PaidFees * isPaidPenalties;
                             }
                        }
                        else if (!installment.IsRepaid && firstInstallmentNotRepaid)
                        {
                            installment.FeesUnpaid =
                                CalculationBaseForLateFees.FeesBasedOnOverdueInterest(_contract, pDate, installment.Number, false, _generalSettings, _nWds) +
                                CalculationBaseForLateFees.FeesBasedOnOverduePrincipal(_contract, pDate, installment.Number, false, _generalSettings, _nWds);
                        }
                }
            }
            else if (_cCo.CancelFees && (_cCo.ManualFeesAmount + _cCo.ManualCommissionAmount) >= 0)
            {
                for (int i = 0; i < _contract.NbOfInstallments; i++)
                {
                    _contract.GetInstallment(i).FeesUnpaid = 0;
                }
            }
        }

        public OCurrency CalculateCommision(DateTime pDate, int pNumber, OPaymentType pPaymentType, OCurrency amount, ref bool calculated)
        {
            if (_contract.Product.AnticipatedPartialRepaymentPenaltiesBase == OAnticipatedRepaymentPenaltiesBases.PrepaidPrincipal
                && !calculated)
            {
                calculated = true;
                return amount * _contract.AnticipatedPartialRepaymentPenalties / (1 + _contract.AnticipatedPartialRepaymentPenalties);
            }

            if (_contract.Product.AnticipatedTotalRepaymentPenaltiesBase ==
                    OAnticipatedRepaymentPenaltiesBases.RemainingInterest && !calculated)
                {
                    return CalculationBaseForLateFees.AnticipateFeesBasedOnOverdueInterest(
                        _contract, pDate, pNumber, false,
                        _generalSettings, _nWds, pPaymentType);
                }

            return CalculationBaseForLateFees.AnticipateFeesBasedOnOlb(_contract, pDate, pNumber, false,
                                                                       _generalSettings, _nWds, pPaymentType);
        }

        public void CalculateAnticipateRepayment(DateTime pDate, OPaymentType pPaymentType, OCurrency paidAmount)
        {
            Installment installment = null;
            OCurrency interests = 0;
            OCurrency commission = 0;
            Installment priorInstallment;
            bool calculated = false;
            OCurrency aTprComission = 0;
            OCurrency willBePaidAmount = 0;
            //to calculate what will be paid before apr or atr
            foreach (Installment willBePaidInstallment in _contract.InstallmentList)
            {
                if(!willBePaidInstallment.IsRepaid && willBePaidInstallment.ExpectedDate <= pDate)
                {
                    willBePaidAmount += willBePaidInstallment.InterestsRepayment - willBePaidInstallment.PaidInterests;
                    willBePaidAmount += willBePaidInstallment.CapitalRepayment - willBePaidInstallment.PaidCapital;
                    willBePaidAmount += willBePaidInstallment.FeesUnpaid;
                }
            }

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                Installment getInstallment = _contract.GetInstallment(i);

                if (!getInstallment.IsRepaid && getInstallment.ExpectedDate > pDate 
                    || (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null))
                {
                    if (installment == null)
                    {
                        installment = getInstallment;

                        if (_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                        {
                            DateTime expectedDate;
                            //int daysInTheInstallment = _contract.NumberOfDaysInTheInstallment(installment.Number, pDate);

                            int daysInTheInstallment = installment.Number == 1
                                                       ? (installment.ExpectedDate - _contract.StartDate).Days
                                                       : (installment.ExpectedDate -
                                                          _contract.GetInstallment(installment.Number - 2).ExpectedDate)
                                                             .Days;

                            if (i == 0)
                            {
                                expectedDate = _contract.StartDate;
                            }
                            else
                            {
                                priorInstallment = _contract.GetInstallment(i - 1);
                                expectedDate = priorInstallment.ExpectedDate;

                                if(_contract.GetLastRepaymentDate() > expectedDate)
                                {
                                    expectedDate = _contract.GetLastRepaymentDate();
                                    daysInTheInstallment = (installment.ExpectedDate - expectedDate).Days;
                                }
                            }

                            if(getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null)
                            {
                                expectedDate = getInstallment.ExpectedDate;
                            }

                            int daySpan = (pDate - expectedDate).Days < 0 ? 0 : (pDate - expectedDate).Days;

                            if (_contract.EscapedMember != null)
                            {
                                //calculate new interes for the person of the group
                                OCurrency amount = _contract.Product.LoanType == OLoanTypes.Flat
                                                  ? _contract.Amount
                                                  : _contract.GetOlb();

                                if (daySpan != 0)
                                {
                                    interests = (amount*_contract.EscapedMember.LoanShareAmount/_contract.Amount)*
                                                daySpan/
                                                daysInTheInstallment*_contract.InterestRate;
                                }
                                else
                                {
                                    interests = (amount * _contract.EscapedMember.LoanShareAmount / _contract.Amount) * _contract.InterestRate;
                                }
                            }
                            else
                            {
                                interests = installment.InterestsRepayment * daySpan / daysInTheInstallment;
                                if (installment.PaidInterests > interests)
                                {
                                    interests = 0;
                                    installment.PaidInterests = 0;
                                }
                            }

                        }
                        else
                        {
                            if (_contract.EscapedMember != null)
                            {
                                if (installment.ExpectedDate == pDate)
                                {
                                    OCurrency amount = _contract.Product.LoanType == OLoanTypes.Flat
                                                           ? _contract.Amount
                                                           : _contract.GetOlb();

                                    interests = (amount*_contract.EscapedMember.LoanShareAmount/_contract.Amount)*
                                                _contract.InterestRate;
                                }
                            }
                            else
                            {

                                interests = installment.InterestsRepayment == installment.PaidInterests
                                                ? installment.InterestsRepayment
                                                : (installment.ExpectedDate > pDate ? 0 : installment.InterestsRepayment);
                            }
                        }
                    }

                    commission += CalculateCommision(pDate, getInstallment.Number, pPaymentType, paidAmount - interests - willBePaidAmount, ref calculated);

                    if (getInstallment.ExpectedDate == pDate && _contract.EscapedMember != null && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        aTprComission = commission;

                    if (getInstallment.ExpectedDate > pDate && _contract.EscapedMember != null && aTprComission > 0 && _contract.Product.AnticipatedTotalRepaymentPenaltiesBase != OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
                        commission = aTprComission;

                    if (_generalSettings.AccountingProcesses == OAccountingProcesses.Cash)
                    {
                        if (installment.PaidInterests > interests)
                        {
                            interests = 0;
                            installment.PaidInterests = 0;
                        }
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

            if (installment != null)
            {
                if (interests == 0 && installment.PaidInterests == 0)
                    installment.NotPaidYet = true;

                installment.InterestsRepayment = _contract.UseCents ? Math.Round(interests.Value, 2, MidpointRounding.AwayFromZero) : Math.Round(interests.Value, 0, MidpointRounding.AwayFromZero);
                installment.CommissionsUnpaid = _contract.UseCents ? Math.Round(commission.Value, 2, MidpointRounding.AwayFromZero) : Math.Round(commission.Value, 0, MidpointRounding.AwayFromZero);
            }
        }

        public void RepayInstallments(DateTime pDate, ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            Installment installment = null;
            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);
                if (!installment.IsRepaid && installment.ExpectedDate <= pDate)
                {
                    OCurrency interestPrepayment = 0;
                    //commission
                    _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);
                    if (amountPaid == 0) break;

                    //penalty
                    _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);
                    if (amountPaid == 0) break;

                    //interest
                    if (amountPaid == 0) break;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent, ref interestPrepayment);

                    //principal
                    if (amountPaid == 0)
                    {
                        PaidIstallments.Add(installment);
                        break;
                    }

                    if (AmountComparer.Compare(amountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                    {
                        OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                        installment.PaidCapital = installment.CapitalRepayment;
                        amountPaid -= principalHasToPay;
                        principalEvent += principalHasToPay;
                    }
                    else
                    {
                        installment.PaidCapital += amountPaid;
                        principalEvent += amountPaid;
                        amountPaid = 0;
                    }

                    PaidIstallments.Add(installment);

                    if (amountPaid == 0)
                        break;
                }
            }

            if (installment != null)
            for(int i = installment.Number; i < _contract.NbOfInstallments; i++)
            {
                _contract.GetInstallment(i).FeesUnpaid = 0;
            }
        }

        private void CalculateDecliningFixedInstallments(int number, OCurrency interests)
        {
            Installment installment = _contract.GetInstallment(number - 1);
            OCurrency olb = _contract.CalculateActualOlb();
            OCurrency totalOlb = _contract.CalculateActualOlb();
            OCurrency installmentVpm = _contract.VPM(olb,_contract.InstallmentList.Count - number + 1).Value;
            OCurrency interestsRepayment;
            OCurrency capital;
            OCurrency capitalRepayment = olb / (_contract.InstallmentList.Count - number + 1);
            OCurrency _olb = 0;

            installment.CapitalRepayment = capitalRepayment;
            installment.PaidInterests = 0;
            installment.PaidCapital = 0;
            installment.PaidFees = 0;

            for (int i = number + 1; i <= _contract.InstallmentList.Count; i++)
            {
                installment = _contract.GetInstallment(i - 2);
                interestsRepayment = olb * _contract.InterestRate;

                installment.InterestsRepayment = Math.Round(Convert.ToDecimal(interestsRepayment.Value), 2);

                capital = installmentVpm - installment.InterestsRepayment;
                installment.CapitalRepayment = Math.Round(Convert.ToDecimal(capital.Value), 2);
                _olb += Math.Round(installment.CapitalRepayment.Value, 2);
                installment.OLB = olb;

                olb -= installment.CapitalRepayment;

                installment.PaidInterests = 0;
                installment.PaidCapital = 0;
                installment.PaidFees = 0;
            }

            installment = _contract.GetInstallment(number - 1);
            installment.InterestsRepayment = interests;

            installment = _contract.GetInstallment(_contract.InstallmentList.Count - 1);
            interestsRepayment = olb * _contract.InterestRate;
            capital = installmentVpm - interestsRepayment;

            installment.InterestsRepayment = Math.Round(Convert.ToDecimal(interestsRepayment.Value), 2);
            installment.CapitalRepayment = Math.Round(Convert.ToDecimal(capital.Value), 2);
            _olb += Math.Round(installment.CapitalRepayment.Value, 2);
            installment.CapitalRepayment = installment.CapitalRepayment - (_olb - totalOlb);
            installment.OLB = olb;
        }

        private void CalculateFlatInstallments(int number)
        {
            Installment installment = _contract.GetInstallment(number - 1);
            OCurrency olb = _contract.CalculateActualOlb();

            OCurrency olbForInterest = _contract.EscapedMember != null
                                           ? _contract.Amount - _contract.EscapedMember.LoanShareAmount
                                           : _contract.CalculateActualOlb();

            OCurrency capitalRepayment = number == 1
                                             ? olb/(_contract.InstallmentList.Count - _contract.GracePeriod.Value)
                                             : olb/(_contract.InstallmentList.Count - number + 1);
            OCurrency calculatedOlb = 0;

            installment.CapitalRepayment = _contract.GracePeriod < installment.Number
                                               ? olb/(_contract.InstallmentList.Count - number + 1)
                                               : 0;

            installment.PaidInterests = 0;
            installment.PaidCapital = 0;
            installment.PaidFees = 0;

            for (int i = number + 1; i <= _contract.InstallmentList.Count; i++)
            {
                calculatedOlb += _contract.GracePeriod < installment.Number ? Math.Round(capitalRepayment.Value, 2) : 0;
                installment = _contract.GetInstallment(i - 1);
                installment.CapitalRepayment = _contract.GracePeriod < installment.Number ? capitalRepayment : 0;
                installment.InterestsRepayment = olbForInterest * _contract.InterestRate;
                installment.PaidInterests = 0;
                installment.PaidCapital = 0;
                installment.PaidFees = 0;
            }
            calculatedOlb += Math.Round(capitalRepayment.Value, 2);

            installment.CapitalRepayment = installment.CapitalRepayment - (calculatedOlb - olb);
        }

        private void CalculateDecliningFixedPrincipal(int number)
        {
            Installment installment = _contract.GetInstallment(number - 1);
            OCurrency olb = _contract.CalculateActualOlb();
            OCurrency totalOlb = _contract.CalculateActualOlb();
            OCurrency capitalRepayment = number == 1 ? olb / (_contract.InstallmentList.Count - _contract.GracePeriod.Value) : olb / (_contract.InstallmentList.Count - number + 1);
            OCurrency calculatedOlb = 0;

            installment.CapitalRepayment = _contract.GracePeriod < installment.Number
                                               ? capitalRepayment
                                               : 0; 

            installment.PaidInterests = 0;
            
            installment.PaidCapital = 0;
            installment.PaidFees = 0;

            for (int i = number + 1; i <= _contract.InstallmentList.Count; i++)
            {
                calculatedOlb += _contract.GracePeriod < installment.Number ? Math.Round(capitalRepayment.Value, 2) : 0;
                installment = _contract.GetInstallment(i - 1);
                installment.CapitalRepayment = _contract.GracePeriod < installment.Number ? capitalRepayment : 0;
                
                if (number != 1)
                    olb -= installment.CapitalRepayment;

                installment.InterestsRepayment = olb * _contract.InterestRate;

                if (number == 1)
                    olb -= installment.CapitalRepayment;

                installment.PaidInterests = 0;
                installment.PaidCapital = 0;
                installment.PaidFees = 0;
            }

            calculatedOlb += Math.Round(capitalRepayment.Value, 2);
            installment.CapitalRepayment = installment.CapitalRepayment - (calculatedOlb - totalOlb);

        }

        private void CalculateRemainsInstallmentAfterApr(int number, DateTime date)
        {
            Installment installment = _contract.GetInstallment(number - 1);
            Installment preInstallment;
            //DateTime expectedDate;

            if (number != 1)
            {
                preInstallment = _contract.GetInstallment(number - 2);
                //expectedDate = preInstallment.ExpectedDate;
            }
            else
            {
                preInstallment = installment;
                //expectedDate = _contract.StartDate;
            }

            if (preInstallment.IsRepaid || installment.Number == 1)
            {
                OCurrency spanDays = (installment.ExpectedDate - date).Days;
                OCurrency numDaysInMonth = installment.Number == 1
                                                       ? (installment.ExpectedDate - _contract.StartDate).Days
                                                       : (installment.ExpectedDate -
                                                          _contract.GetInstallment(installment.Number - 2).ExpectedDate)
                                                             .Days;
                //_contract.NumberOfDaysInTheInstallment(installment.Number, expectedDate);
                
                if (preInstallment.ExpectedDate == date || _contract.StartDate == date || spanDays >= numDaysInMonth)
                {
                    spanDays = numDaysInMonth;
                }

                OCurrency olb = _contract.Product.LoanType == OLoanTypes.Flat && _contract.EscapedMember != null
                                    ? _contract.Amount - _contract.EscapedMember.LoanShareAmount
                                    : _contract.CalculateActualOlb();

                OCurrency interests;

                if (_generalSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                {
                    //acrrual
                    interests = _contract.EscapedMember != null
                                        ? olb * _contract.InterestRate
                                        : spanDays / numDaysInMonth * olb * _contract.InterestRate;
                }
                else
                {
                    //cash
                    interests = olb * _contract.InterestRate;
                }

                installment.InterestsRepayment = interests;
                
                int startFrom = number;

                if(preInstallment.ExpectedDate == date)
                {
                    installment.InterestsRepayment = olb * _contract.InterestRate;
                }
                
                switch (_contract.Product.LoanType)
                {
                    case OLoanTypes.Flat :
                        {
                            CalculateFlatInstallments(startFrom);
                            break;
                        }

                    case OLoanTypes.DecliningFixedInstallments:
                        {
                            CalculateDecliningFixedInstallments(startFrom, interests);
                            break;
                        }

                    case OLoanTypes.DecliningFixedPrincipal:
                        {
                            CalculateDecliningFixedPrincipal(startFrom);
                            break;
                        }
                }
                
                //treat the installments to round valus
                decimal floatPartOfInterests = 0;
                decimal floatPartOfCapital = 0;

                if (!_contract.UseCents)
                {
                    for (int i = number; i <= _contract.InstallmentList.Count; i++)
                    {
                        installment = i == 0 ? _contract.GetInstallment(i) : _contract.GetInstallment(i - 1);

                        floatPartOfInterests += installment.InterestsRepayment.Value -
                                                Math.Round(Convert.ToDecimal(installment.InterestsRepayment.Value),
                                                           MidpointRounding.AwayFromZero);
                        installment.InterestsRepayment =
                            Math.Round(Convert.ToDecimal(installment.InterestsRepayment.Value),
                                       MidpointRounding.AwayFromZero);

                        floatPartOfCapital += installment.CapitalRepayment.Value -
                                              Math.Round(Convert.ToDecimal(installment.CapitalRepayment.Value),
                                                         MidpointRounding.AwayFromZero);
                        installment.CapitalRepayment = Math.Round(
                            Convert.ToDecimal(installment.CapitalRepayment.Value), MidpointRounding.AwayFromZero);
                    }

                    installment.InterestsRepayment = installment.InterestsRepayment + Math.Round(floatPartOfInterests, 0);
                    installment.CapitalRepayment = installment.CapitalRepayment + Math.Round(floatPartOfCapital, 0);
                }
                else
                {
                    for (int i = number; i <= _contract.InstallmentList.Count; i++)
                    {
                        installment = i == 0 ? _contract.GetInstallment(i) : _contract.GetInstallment(i - 1);

                        installment.InterestsRepayment = Math.Round(installment.InterestsRepayment.Value, 2);

                        floatPartOfCapital += (installment.CapitalRepayment.Value*100 -
                                               Math.Round(Convert.ToDecimal(installment.CapitalRepayment.Value*100), 4,
                                                          MidpointRounding.AwayFromZero))/100;

                        installment.CapitalRepayment = Math.Round(installment.CapitalRepayment.Value, 2,
                                                                  MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        private void CalculateRemainsInstallmentAfterAtr(int number)
        {
            Installment installment;
            if (number != _contract.InstallmentList.Count)
            {
                for (int i = number + 1; i <= _contract.InstallmentList.Count; i++)
                {
                    installment = _contract.GetInstallment(i - 1);
                    installment.CapitalRepayment = 0;
                    installment.InterestsRepayment = 0;
                    installment.PaidInterests = 0;
                    installment.PaidCapital = 0;
                    installment.PaidFees = 0;
                }
            }
        }

        private void RepayPrincipal(int pNumber, ref OCurrency pAmountPaid, ref OCurrency principalEvent, DateTime pDate)
        {
            foreach (Installment installment in _contract.InstallmentList)
            {
                if (installment.Number >= pNumber)
                {
                    if (_contract.EscapedMember != null)
                    {
                        installment.PaidCapital += pAmountPaid;
                        installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                        principalEvent += pAmountPaid;

                        pAmountPaid = 0;
                    }
                    else
                    {
                        if (AmountComparer.Compare(pAmountPaid, installment.CapitalRepayment - installment.PaidCapital) > 0)
                        {
                            OCurrency principalHasToPay = installment.CapitalRepayment - installment.PaidCapital;
                            installment.PaidCapital = installment.CapitalRepayment;
                            pAmountPaid -= principalHasToPay;
                            principalEvent += principalHasToPay;

                            if (installment.ExpectedDate <= pDate)
                                break;
                        }
                        else
                        {
                            installment.PaidCapital += pAmountPaid;
                            installment.PaidCapital = Math.Round(installment.PaidCapital.Value, 2);
                            principalEvent += pAmountPaid;

                            pAmountPaid = 0;

                            if (installment.ExpectedDate <= pDate)
                                break;
                        }
                    }
                }
            }
        }

        public void RepayTotalAnticipateInstallments(DateTime pDate, ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            Installment installment = null;
            
            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);
                if(!installment.IsRepaid)
                {
                    #region 1 if (installment.ExpectedDate > pDate)
                    if (installment.ExpectedDate > pDate)
                    {
                        //commission
                        if (amountPaid == 0) break;
                        _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                        //fee
                        if (amountPaid == 0) break;
                        _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                        //interest
                        OCurrency interestPrepayment = 0;
                        _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,
                                                             ref interestPrepayment);
                        //principal
                        principalEvent += amountPaid;
                                               
                        if (installment.Number != _contract.NbOfInstallments)
                        {
                            OCurrency rpePrincipal = 0;
                            foreach (RepaymentEvent rpe in _contract.Events.GetLoanRepaymentEvents())
                            {
                                if (rpe.InstallmentNumber == installment.Number && !rpe.Deleted)
                                {
                                    rpePrincipal += rpe.Principal;
                                }
                            }

                            if (installment.PaidCapital == rpePrincipal)
                            {
                                rpePrincipal = 0;
                            }

                            installment.PaidCapital = amountPaid + installment.PaidCapital + rpePrincipal;
                            installment.CapitalRepayment = installment.PaidCapital;
                            installment.OLB = 0;
                        }
                        else
                        {
                            installment.PaidCapital = amountPaid;
                        }

                        amountPaid = 0;
                        PaidIstallments.Add(installment);
                        if (amountPaid == 0) break;
                    }
                    #endregion
                    #region 2 if (installment.ExpectedDate <= pDate)
                    if (installment.ExpectedDate <= pDate)
                    {
                        //commission
                        if (amountPaid == 0) break;
                        _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                        //fee
                        if (amountPaid == 0) break;
                        _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                        //interest
                        OCurrency interestPrepayment = 0;
                        _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,
                                                             ref interestPrepayment);
                        //principal
                        RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                        PaidIstallments.Add(installment);
                        if (amountPaid == 0) break;
                    }
                    #endregion
                }

                #region 3 if (installment.IsRepaid && installment.ExpectedDate > pDate && installment.NotPaidYet)
                if (installment.IsRepaid && installment.ExpectedDate > pDate && installment.NotPaidYet)
                {
                    //commission
                    if (amountPaid == 0) break;
                    _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                    //fee
                    if (amountPaid == 0) break;
                    _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                    //interest
                    OCurrency interestPrepayment = 0;
                    _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent, ref interestPrepayment);
                    
                    //principal
                    RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                    PaidIstallments.Add(installment);

                    if (amountPaid == 0) break;
                }
                #endregion
            }

            if (installment != null)
            {
                CalculateRemainsInstallmentAfterAtr(installment.Number);
            }
        }

        public void RepayPartialAnticipateInstallments(DateTime pDate, ref OCurrency amountPaid, ref OCurrency interestEvent, ref OCurrency principalEvent, ref OCurrency feesEvent, ref OCurrency commissionsEvent)
        {
            if (amountPaid == 0) return;
            Installment installment = null;

            for (int i = 0; i < _contract.NbOfInstallments; i++)
            {
                installment = _contract.GetInstallment(i);
                if (installment.IsRepaid && installment.ExpectedDate > pDate && !_cCo.KeepExpectedInstallments)
                {
                    #region 1 (installment.FeesUnpaid != 0)
                    if (installment.FeesUnpaid != 0 || installment.CommissionsUnpaid != 0 || installment.NotPaidYet)
                    {
                        //commission
                        if (amountPaid == 0) break;
                        _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                        //fee
                        if (amountPaid == 0) break;
                        _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                        //interest
                        OCurrency interestPrepayment = 0;
                        _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,
                                                             ref interestPrepayment);

                        //principal
                        RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                        PaidIstallments.Add(installment);

                        if (amountPaid == 0) break;
                    }
                    #endregion
                    #region 2 (installment.CommissionsUnpaid != 0)
                    //if (installment.CommissionsUnpaid != 0)
                    //{
                    //    //commission
                    //    if (amountPaid == 0) break;
                    //    _MethodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                    //    //fee
                    //    if (amountPaid == 0) break;
                    //    _MethodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                    //    //interest
                    //    OCurrency interestPrepayment = 0;
                    //    _MethodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,
                    //                                         ref interestPrepayment);
                    //    if (amountPaid == 0) break;

                    //    //principal
                    //    RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                    //    PaidIstallments.Add(installment);

                    //    if (amountPaid == 0) break;
                    //}
                    //#endregion
                    //#region 3 (installment.NotPaidYet)
                    //if (installment.NotPaidYet)
                    //{
                    //    //commission
                    //    if (amountPaid == 0) break;
                    //    _MethodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                    //    //fee
                    //    if (amountPaid == 0) break;
                    //    _MethodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                    //    //interest
                    //    OCurrency interestPrepayment = 0;
                    //    _MethodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent, ref interestPrepayment);

                    //    //principal
                    //    RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                    //    PaidIstallments.Add(installment);

                    //    if (amountPaid == 0) break;
                    //}
                    #endregion
                }

                if (!installment.IsRepaid && !_cCo.KeepExpectedInstallments)
                {
                    #region 1 (installment.ExpectedDate <= pDate || installment.ExpectedDate > pDate)
                    if (installment.ExpectedDate <= pDate || installment.ExpectedDate > pDate)
                    {
                        //commission
                        if (amountPaid == 0) break;
                        _methodToRepayCommission.RepayCommission(installment, ref amountPaid, ref commissionsEvent);

                        //fee
                        if (amountPaid == 0) break;
                        _methodToRepayFees.RepayFees(installment, ref amountPaid, ref feesEvent);

                        //interest
                        OCurrency interestPrepayment = 0;
                        _methodToRepayInterest.RepayInterest(installment, ref amountPaid, ref interestEvent,
                                                             ref interestPrepayment);

                        RepayPrincipal(installment.Number, ref amountPaid, ref principalEvent, pDate);

                        PaidIstallments.Add(installment);

                        if (amountPaid == 0) break;
                    }
                    #endregion
                }
            }

            if (installment != null)
            {
                CalculateRemainsInstallmentAfterApr(installment.Number, pDate);
                for (int i = installment.Number; i < _contract.NbOfInstallments; i++)
                {
                    _contract.GetInstallment(i).FeesUnpaid = 0;
                }
            }
        }

    }
}