using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Loans.CalculateInstallments.Interfaces;
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Loans.CalculateInstallments.Flat
{
    /// <summary>
    /// Summary description for FlatStrategy.
    /// </summary>
    [Serializable]
    public class FlatStrategy : ICalculateInstallments
    {
        //private readonly ICalculateInstallments _iCS;
        private readonly Loan _contract;
        private readonly int RoundingPoint;
        private readonly OCurrency _initialOLBOfContractBeforeRescheduling;
        private readonly ApplicationSettings GeneralSettings;
        
        public FlatStrategy(DateTime pStartDate, Loan pContract, ApplicationSettings pGeneralSettings, OCurrency pInitialOLBOfContractBeforeRescheduling)
        {
            RoundingPoint = pContract.UseCents ? 2 : 0;

            _initialOLBOfContractBeforeRescheduling = pInitialOLBOfContractBeforeRescheduling;
            _contract = pContract;
            GeneralSettings = pGeneralSettings;
        }

        public List<Installment> CalculateInstallments(bool changeDate)
        {
            List<Installment> schedule = new List<Installment>();
            OCurrency olb = _contract.Amount;
            OCurrency totalAmount = _contract.Amount;

            OCurrency totalInterest = _contract.GracePeriod.HasValue && !_contract.Product.ChargeInterestWithinGracePeriod
                                ? Math.Round(_contract.Amount.Value * Convert.ToDecimal(_contract.InterestRate) * (_contract.NbOfInstallments - _contract.GracePeriod.Value), RoundingPoint, MidpointRounding.AwayFromZero)
                                : Math.Round(_contract.Amount.Value * Convert.ToDecimal(_contract.InterestRate) * _contract.NbOfInstallments, RoundingPoint, MidpointRounding.AwayFromZero);

            int nbOfInstallmentWithoutGracePeriod = _contract.GracePeriod.HasValue ? _contract.NbOfInstallments - _contract.GracePeriod.Value : _contract.NbOfInstallments;

            OCurrency sumOfPrincipal = 0;
            OCurrency sumOfInterest = 0;
            int installmentNumberWithoutGracePeriodForCapital = 0;
            int installmentNumberWithoutGracePeriodForInterest = 0;

            if (!_contract.Rescheduled)
            {
                for (int number = 1; number <= _contract.NbOfInstallments; number++)
                {
                    Installment installment = new Installment { Number = number, FeesUnpaid = 0 };

                    if (changeDate)
                    {
                        installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                    }

                    installment.OLB = olb;
                    if (_contract.GracePeriod.HasValue && number <= _contract.GracePeriod)
                    {
                        installment.CapitalRepayment = 0;
                        if (_contract.Product.ChargeInterestWithinGracePeriod)
                        {
                            installment.InterestsRepayment = Math.Round((totalInterest.Value - sumOfInterest.Value) / (_contract.NbOfInstallments - installmentNumberWithoutGracePeriodForInterest), RoundingPoint);
                            sumOfInterest += installment.InterestsRepayment;
                            installmentNumberWithoutGracePeriodForInterest++;
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                            installmentNumberWithoutGracePeriodForInterest++;
                        }
                    }
                    else
                    {
                        installment.CapitalRepayment = decimal.Round((totalAmount.Value - sumOfPrincipal.Value) / (nbOfInstallmentWithoutGracePeriod - installmentNumberWithoutGracePeriodForCapital), RoundingPoint);
                        sumOfPrincipal += installment.CapitalRepayment;
                        installmentNumberWithoutGracePeriodForCapital++;

                        installment.InterestsRepayment = Math.Round((totalInterest.Value - sumOfInterest.Value) / (_contract.NbOfInstallments - installmentNumberWithoutGracePeriodForInterest), RoundingPoint);
                        sumOfInterest += installment.InterestsRepayment;
                        installmentNumberWithoutGracePeriodForInterest++;
                    }

                    if (installment.Number == 1 && GeneralSettings.PayFirstInterestRealValue)
                    {
                        installment.InterestsRepayment = Math.Round(installment.InterestsRepayment.Value / _contract.NbOfDaysInTheInstallment * (_contract.FirstInstallmentDate - _contract.StartDate).Days, RoundingPoint);
                    }

                    olb -= installment.CapitalRepayment.Value;

                    schedule.Add(installment);
                }
            }
            else
            {
                for (int number = 1; number <= _contract.NbOfInstallments; number++)
                {
                    Installment installment = new Installment { Number = number, FeesUnpaid = 0 };

                    if (changeDate)
                    {
                        installment.ExpectedDate = _contract.CalculateInstallmentDate(_contract.AlignDisbursementDate, installment.Number);
                    }

                    installment.OLB = olb;
                    if (_contract.GracePeriod.HasValue && number <= _contract.GracePeriod)
                    {
                        installment.CapitalRepayment = 0;
                        if (_contract.Product.ChargeInterestWithinGracePeriod)
                        {
                            installment.InterestsRepayment = _initialOLBOfContractBeforeRescheduling.Value * Convert.ToDecimal(_contract.InterestRate);
                            // decimal.Round((totalInterest.Value - sumOfInterest.Value) / (_contract.NbOfInstallments - installmentNumberWithoutGracePeriodForInterest), 2);
                        }
                        else
                        {
                            installment.InterestsRepayment = 0;
                        }
                    }
                    else
                    {
                        installment.CapitalRepayment = Math.Round((totalAmount.Value - sumOfPrincipal.Value) / (nbOfInstallmentWithoutGracePeriod - installmentNumberWithoutGracePeriodForCapital), RoundingPoint);
                        sumOfPrincipal += installment.CapitalRepayment;
                        installmentNumberWithoutGracePeriodForCapital++;

                        installment.InterestsRepayment = _initialOLBOfContractBeforeRescheduling.Value * Convert.ToDecimal(_contract.InterestRate);
                        //installment.InterestsRepayment = decimal.Round((totalInterest.Value - sumOfInterest.Value) / (_contract.NbOfInstallments - installmentNumberWithoutGracePeriodForInterest), 2);
                    }

                    olb -= installment.CapitalRepayment.Value;

                    if (installment.Number == 1 && GeneralSettings.PayFirstInterestRealValue)
                    {
                        installment.InterestsRepayment = Math.Round(installment.InterestsRepayment.Value / _contract.NbOfDaysInTheInstallment * (_contract.FirstInstallmentDate - _contract.StartDate).Days, RoundingPoint);
                    }

                    schedule.Add(installment);
                }
            }

            return schedule;
            //return _iCS.CalculateInstallments(changeDate);
        }
    }
}