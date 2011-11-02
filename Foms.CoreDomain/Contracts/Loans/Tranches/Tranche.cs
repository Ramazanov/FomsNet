using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.Loans.Tranches
{
    [Serializable]
    public struct TrancheOptions
    {
        public DateTime TrancheDate;
        public OCurrency TrancheAmount;
        public int CountOfNewInstallments;
        public decimal InterestRate;
        public bool ApplyNewInterestOnOLB;
        public int Number;
    }

    [Serializable]
    public class Tranche
    {
        private readonly Loan _currentLoan;
        private readonly ApplicationSettings _generalSettings;
        private TrancheEvent _trancheEvent;

        public Tranche()
        {
        }

        public Tranche(Loan pLoan, ApplicationSettings pGeeneralSettings)
        {
            _currentLoan = pLoan;
            _generalSettings = pGeeneralSettings;
        }

        private void _Tranche_ExtendMaturity(int newInstallments, DateTime pFirstInstallmentDate)
        {
            if (newInstallments > 0)
            {
                List<Installment> listInstallment = new List<Installment>();
                listInstallment.AddRange(_currentLoan.InstallmentList);

                foreach (Installment _installment in _currentLoan.InstallmentList)
                {
                    if (!_installment.IsRepaid)
                    {
                        listInstallment.Remove(_installment);
                    }
                }

                int lastNumber = listInstallment.Count;
                _trancheEvent.StartedFrom = lastNumber;

                for (int i = 1; i <= newInstallments; i++)
                {
                    DateTime expectedDate = _currentLoan.CalculateInstallmentDate(pFirstInstallmentDate, i);
                    Installment installment = new Installment
                    {
                        Number = lastNumber + i,
                        CapitalRepayment = 0,
                        PaidCapital = 0,
                        CommissionsUnpaid = 0,
                        FeesUnpaid = 0,
                        InterestsRepayment = 1,
                        PaidInterests = 0,
                        ExpectedDate = expectedDate
                    };

                    listInstallment.Add(installment);
                }
                _currentLoan.InstallmentList = listInstallment;

                _currentLoan.NbOfInstallments = _currentLoan.InstallmentList.Count;
            }
        }
        
        private void AddFlatTranche(TrancheOptions to)
        {
            OCurrency RemainsInterestAmount = 0;
            OCurrency RemainsAmount = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    if (to.TrancheDate > _currentLoan.InstallmentList[installment.Number - 2].ExpectedDate)
                    {
                        RemainsInterestAmount += installment.InterestsRepayment *
                                                 (to.TrancheDate - _currentLoan.InstallmentList[installment.Number - 2].ExpectedDate).Days /
                                                 _currentLoan.NumberOfDaysInTheInstallment(installment.Number, to.TrancheDate);
                    }

                    RemainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                }
            }

            OCurrency InterestAmount = !to.ApplyNewInterestOnOLB
                                           ? to.TrancheAmount * to.InterestRate +
                                             RemainsInterestAmount / to.CountOfNewInstallments
                                           : (to.TrancheAmount + RemainsAmount) * to.InterestRate +
                                             RemainsInterestAmount / to.CountOfNewInstallments;

            OCurrency GeneralInterestAmount = InterestAmount;

            InterestAmount = _currentLoan.UseCents ? InterestAmount : Math.Round(InterestAmount.Value, 0);

            OCurrency olb = to.TrancheAmount + RemainsAmount;

            _Tranche_ExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.CapitalRepayment = _currentLoan.UseCents
                                                       ? (to.TrancheAmount + RemainsAmount) /
                                                         to.CountOfNewInstallments
                                                       : Math.Round(
                                                             ((to.TrancheAmount + RemainsAmount) /
                                                              to.CountOfNewInstallments).Value, 0);

                    installment.InterestsRepayment = InterestAmount;

                    installment.PaidCapital = 0;
                    installment.PaidInterests = 0;
                    installment.OLB = olb;
                    olb -= installment.CapitalRepayment;
                }
            }

            OCurrency LastInstallmentInterest = _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].InterestsRepayment +
                GeneralInterestAmount * to.CountOfNewInstallments - InterestAmount * to.CountOfNewInstallments;

            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].InterestsRepayment = _currentLoan.UseCents
                                                                                ? LastInstallmentInterest
                                                                                : Math.Round(
                                                                                      LastInstallmentInterest
                                                                                          .Value, 0);
            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment =
                _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment + olb;

            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
        }

        private void AddFixedPrincipalTranche(TrancheOptions to)
        {
            OCurrency RemainsAmount = 0;
            List<Installment> _NotRepaidInstollment = new List<Installment>();
            int numberRemainInstallment = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                _NotRepaidInstollment.Add(installment);

                if (!installment.IsRepaid)
                {
                    RemainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                    numberRemainInstallment++;
                }
            }

            OCurrency olb = to.TrancheAmount;

            OCurrency RemainsInterestAmount = _generalSettings.AccountingProcesses == OAccountingProcesses.Accrual
                                                  ? GenerateEvents.Accrual.CalculateRemainingInterests(_currentLoan, to.TrancheDate)
                                                  : GenerateEvents.Cash.CalculateRemainingInterests(_currentLoan, to.TrancheDate);

            RemainsInterestAmount = _currentLoan.UseCents ? RemainsInterestAmount : Math.Round(RemainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);


            _Tranche_ExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);
            OCurrency RemainingOLB = RemainsAmount;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    if (_NotRepaidInstollment.Count >= installment.Number)
                    {
                        if (!_NotRepaidInstollment[installment.Number - 1].IsRepaid)
                        {
                            installment.CapitalRepayment = _currentLoan.UseCents
                                                               ? to.TrancheAmount / to.CountOfNewInstallments +
                                                                 RemainsAmount / numberRemainInstallment
                                                               : Math.Round(
                                                                     (to.TrancheAmount / to.CountOfNewInstallments +
                                                                      RemainsAmount / numberRemainInstallment).Value, 0,
                                                                     MidpointRounding.AwayFromZero);

                            installment.InterestsRepayment = to.ApplyNewInterestOnOLB
                                           ? (olb + RemainingOLB) * to.InterestRate + RemainsInterestAmount
                                           : olb * to.InterestRate + RemainingOLB * _currentLoan.GivenTranches[to.Number - 1].InterestRate + RemainsInterestAmount;

                            installment.InterestsRepayment = _currentLoan.UseCents
                                                                 ? Math.Round(installment.InterestsRepayment.Value, 2,
                                                                              MidpointRounding.AwayFromZero)
                                                                 : Math.Round(installment.InterestsRepayment.Value, 0,
                                                                              MidpointRounding.AwayFromZero);

                            installment.OLB = olb + RemainingOLB;

                            RemainingOLB -= _currentLoan.UseCents
                                                 ? RemainingOLB / numberRemainInstallment
                                                 : Math.Round(
                                                       (RemainingOLB / numberRemainInstallment).Value, 0,
                                                       MidpointRounding.AwayFromZero);

                            olb -= to.TrancheAmount / to.CountOfNewInstallments;

                            installment.PaidCapital = 0;
                            installment.PaidInterests = 0;
                            RemainsInterestAmount = 0;
                        }
                    }
                    else
                    {
                        installment.CapitalRepayment = _currentLoan.UseCents
                                                           ? to.TrancheAmount / to.CountOfNewInstallments
                                                           : Math.Round(
                                                                 (to.TrancheAmount / to.CountOfNewInstallments).Value, 0,
                                                                 MidpointRounding.AwayFromZero);

                        
                        installment.InterestsRepayment = olb * to.InterestRate;

                        installment.InterestsRepayment = _currentLoan.UseCents
                                     ? installment.InterestsRepayment
                                     : Math.Round(installment.InterestsRepayment.Value, 0,
                                                  MidpointRounding.AwayFromZero);
                        
                        installment.PaidCapital = 0;
                        installment.PaidInterests = 0;
                        installment.OLB = olb;

                        olb -= installment.CapitalRepayment;
                    }
                }
            }
            
            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
        }

        private void AddFixedInstallmentTranche(TrancheOptions to)
        {
            OCurrency RemainsAmount = 0;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    RemainsAmount += installment.CapitalRepayment - installment.PaidCapital;
                }
            }

            OCurrency RemainsInterestAmount = _generalSettings.AccountingProcesses == OAccountingProcesses.Accrual
                                                  ? GenerateEvents.Accrual.CalculateRemainingInterests(_currentLoan, to.TrancheDate)
                                                  : GenerateEvents.Cash.CalculateRemainingInterests(_currentLoan, to.TrancheDate);

            RemainsInterestAmount = _currentLoan.UseCents ? RemainsInterestAmount : Math.Round(RemainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);

            _Tranche_ExtendMaturity(to.CountOfNewInstallments, to.TrancheDate);

            OCurrency olb;
            OCurrency newAmountVPM;
            OCurrency priviousAmountVPM = 0;

            if (to.ApplyNewInterestOnOLB)
            {
                olb = to.TrancheAmount + RemainsAmount;

                newAmountVPM = _currentLoan.UseCents
                                   ? _currentLoan.VPM(olb, to.CountOfNewInstallments, (double)to.InterestRate)
                                   : Math.Round(_currentLoan.VPM(olb, to.CountOfNewInstallments, (double)to.InterestRate).Value, 0, MidpointRounding.AwayFromZero);
                RemainsAmount = 0;
            }
            else
            {
                olb = to.TrancheAmount;

                newAmountVPM = _currentLoan.UseCents
                                   ? _currentLoan.VPM(olb, to.CountOfNewInstallments, (double)to.InterestRate)
                                   : Math.Round(_currentLoan.VPM(olb, to.CountOfNewInstallments, (double)to.InterestRate).Value, 0,
                                                MidpointRounding.AwayFromZero);
                priviousAmountVPM = _currentLoan.UseCents
                                        ? _currentLoan.VPM(RemainsAmount, to.CountOfNewInstallments,
                                              (double)(_currentLoan.GivenTranches[to.Number - 1].InterestRate.Value))
                                        : Math.Round((_currentLoan.VPM(RemainsAmount, to.CountOfNewInstallments,
                                                          (double)(_currentLoan.GivenTranches[to.Number - 1].InterestRate.Value))).Value, 0,
                                                     MidpointRounding.AwayFromZero);
            }

            OCurrency _OLB = olb + RemainsAmount;

            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.InterestsRepayment = _currentLoan.UseCents
                                   ? Math.Round(olb.Value * to.InterestRate, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(olb.Value * to.InterestRate, 0, MidpointRounding.AwayFromZero);

                    OCurrency _interestAmountForPriviousOLB = _currentLoan.UseCents
                                   ? RemainsAmount * _currentLoan.GivenTranches[to.Number - 1].InterestRate
                                   : Math.Round((RemainsAmount * _currentLoan.GivenTranches[to.Number - 1].InterestRate).Value, 0, MidpointRounding.AwayFromZero);

                    installment.CapitalRepayment = newAmountVPM.Value - installment.InterestsRepayment.Value;

                    installment.InterestsRepayment += _currentLoan.UseCents
                                   ? Math.Round(_interestAmountForPriviousOLB.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(_interestAmountForPriviousOLB.Value, 0, MidpointRounding.AwayFromZero);

                    olb -= installment.CapitalRepayment;

                    installment.CapitalRepayment = _currentLoan.UseCents
                                   ? Math.Round(installment.CapitalRepayment.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(installment.CapitalRepayment.Value, 0, MidpointRounding.AwayFromZero);


                    OCurrency PriviousCapital = priviousAmountVPM - _interestAmountForPriviousOLB;

                    installment.CapitalRepayment += _currentLoan.UseCents
                                   ? Math.Round(PriviousCapital.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(PriviousCapital.Value, 0, MidpointRounding.AwayFromZero);


                    RemainsAmount -= PriviousCapital;

                    installment.InterestsRepayment += _currentLoan.UseCents
                                   ? Math.Round(RemainsInterestAmount.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(RemainsInterestAmount.Value, 0, MidpointRounding.AwayFromZero);

                    RemainsInterestAmount = 0;
                    installment.PaidCapital = 0;
                    installment.PaidInterests = 0;
                }
            }

            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - to.CountOfNewInstallments].CapitalRepayment +=
                _currentLoan.UseCents
                    ? Math.Round(olb.Value + RemainsAmount.Value, 2, MidpointRounding.AwayFromZero)
                    : Math.Round(olb.Value + RemainsAmount.Value, 0, MidpointRounding.AwayFromZero);
            
            //OLB calculation
            foreach (Installment installment in _currentLoan.InstallmentList)
            {
                if (!installment.IsRepaid)
                {
                    installment.OLB = _OLB;
                    _OLB -= installment.CapitalRepayment;
                }
            }

            _currentLoan.Amount = _currentLoan.Amount + to.TrancheAmount;
            _currentLoan.InstallmentList[_currentLoan.InstallmentList.Count - 1].CapitalRepayment +=
                _currentLoan.UseCents
                    ? Math.Round(_OLB.Value, 2, MidpointRounding.AwayFromZero)
                    : Math.Round(_OLB.Value, 0, MidpointRounding.AwayFromZero);
        }

        public TrancheEvent AddTranche(TrancheOptions pTO)
        {
            _trancheEvent = new TrancheEvent
                                {
                                    Date = DateTime.Now,
                                    InterestRate = pTO.InterestRate,
                                    Amount = pTO.TrancheAmount,
                                    Maturity = pTO.CountOfNewInstallments,
                                    StartDate = pTO.TrancheDate,
                                    Number = _currentLoan.GivenTranches.Count
                                };

            pTO.Number = _trancheEvent.Number;

            switch (_currentLoan.Product.LoanType)
            {
                case OLoanTypes.Flat:
                    {
                        AddFlatTranche(pTO);
                    }
                    break;

                case OLoanTypes.DecliningFixedPrincipal:
                    {
                        AddFixedPrincipalTranche(pTO);
                    }
                    break;

                case OLoanTypes.DecliningFixedInstallments:
                    {
                        AddFixedInstallmentTranche(pTO);
                    }
                    break;
            }

            return _trancheEvent;
        }
    }
}
