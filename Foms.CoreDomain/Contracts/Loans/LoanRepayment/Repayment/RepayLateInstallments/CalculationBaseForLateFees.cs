using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.RepayLateInstallments
{
    [Serializable]
    public static class CalculationBaseForLateFees
    {
        private static int CountDaysOff(DateTime beginDate, DateTime endDate, ApplicationSettings pGeneralSettings, 
            NonWorkingDateSingleton pNonWorkingDate)
        {
            var dt = new DateTime();
            int countDayOff = 0;

            if (!pGeneralSettings.IsCalculationLateFeesDuringHolidays)
            {
                //count day off
                for (int day = 1; day <= (endDate - beginDate).Days; day++)
                {
                    //week end
                    if (!pGeneralSettings.DoNotSkipNonWorkingDays)
                    if (((int)(dt.DayOfWeek) == pGeneralSettings.WeekEndDay1) || ((int)(dt.DayOfWeek) == pGeneralSettings.WeekEndDay2))
                        countDayOff++;
                    
                    //holidays
                    countDayOff += pNonWorkingDate.PublicHolidays.Keys.Count(publicHoliday => dt == publicHoliday);
                    dt = beginDate.AddDays(day);
                }
            }

            return countDayOff;
        }

        private static int _CalculatePastDueWithGeneralParameterForRepayment(int pDays, ApplicationSettings pGeneralSettings)
        {
            if (pGeneralSettings.LateDaysAfterAccrualCeases.HasValue)
            {
                if (pDays > pGeneralSettings.LateDaysAfterAccrualCeases.Value)
                    return pGeneralSettings.LateDaysAfterAccrualCeases.Value;
                
                return pDays;
            }
            return pDays;
        }

        public static OCurrency FeesBasedOnInitialAmount(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure,
            ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            if (pContract.NonRepaymentPenalties.InitialAmount != 0)
            {
                
                int pastDueDays = pForClosure ? pContract.CalculatePastDueForClosure(pDate) : pContract.CalculatePastDueSinceLastRepayment(pDate);
                pastDueDays = _CalculatePastDueWithGeneralParameterForRepayment(pastDueDays, pGeneralSettings);
                if (pContract.GracePeriodOfLateFees >= pastDueDays)
                {
                    pastDueDays = 0;
                }
                OCurrency fees = pContract.Amount * Convert.ToDecimal(pContract.NonRepaymentPenalties.InitialAmount) * (double)pastDueDays;
                

                return pContract.UseCents ? Math.Round(fees.Value, 2) : Math.Round(fees.Value, 0);
            }
            return 0;
        }

        public static OCurrency FeesBasedOnOlb(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure,
            ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            if (pContract.NonRepaymentPenalties.OLB != 0)
            {
                OCurrency fees = pForClosure
                    ? _CalculateNonRepaymentPenaltiesForClosure(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Olb,
                    pGeneralSettings, pNonWorkingDate)
                    : _CalculateNonRepaymentPenalties(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Olb,
                    pGeneralSettings, pNonWorkingDate);
                
                return pContract.UseCents ? Math.Round(fees.Value, 2) : Math.Round(fees.Value, 0);
            }
            return 0;
        }

        public static OCurrency FeesBasedOnOverduePrincipal(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure, 
            ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            if (pContract.NonRepaymentPenalties.OverDuePrincipal != 0)
            {
                OCurrency fees = pForClosure
                    ? _CalculateNonRepaymentPenaltiesForClosure(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Principal, 
                    pGeneralSettings, pNonWorkingDate)
                    : _CalculateNonRepaymentPenalties(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Principal, 
                    pGeneralSettings, pNonWorkingDate);

                return pContract.UseCents ? Math.Round(fees.Value, 2) : Math.Round(fees.Value, 0);
            }
            return 0;
        }

        public static OCurrency FeesBasedOnOverdueInterest(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            if (pContract.NonRepaymentPenalties.OverDueInterest != 0)
            {
                OCurrency fees = pForClosure
                    ? _CalculateNonRepaymentPenaltiesForClosure(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Interest, 
                    pGeneralSettings, pNonWorkingDate)
                    : _CalculateNonRepaymentPenalties(pContract, pDate, pInstallmentNumber, OLateRepaymentTypes.Interest, 
                    pGeneralSettings, pNonWorkingDate);
                
                return pContract.UseCents ? Math.Round(fees.Value, 2) : Math.Round(fees.Value, 0);
            }
            return 0;
        }

        public static OCurrency AnticipateFeesBasedOnOlb(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate, OPaymentType pPaymentType)
        {
            OCurrency fees = AnticipateCalculateFeesBasedOnOlb(pContract, pDate, pInstallmentNumber, pPaymentType);
            
            return pContract.UseCents ? Math.Round(fees.Value, 2) : Math.Round(fees.Value, 0);
        }

        public static OCurrency AnticipateFeesBasedOnOverdueInterest(Loan pContract, DateTime pDate, int pInstallmentNumber, bool pForClosure, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate, OPaymentType pPaymentType)
        {
            OCurrency fees = _AnticipateCalculateNonRepaymentPenalties(pContract, pDate, pInstallmentNumber, pPaymentType, pGeneralSettings);

            return fees.Value;
        }

        public static OCurrency CalculateAnticipateRepayment(Loan pContract, DateTime pDate, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate, OPaymentType pPaymentType)
        {
            Installment pInstallment = null;
            OCurrency interests = 0;
            Installment priorInstallment;
            OCurrency olb = 0;
            OCurrency fees = 0;
            OCurrency ammountHasToPay = 0;

            for (int i = 0; i < pContract.NbOfInstallments; i++)
            {
                Installment installment = pContract.GetInstallment(i);

                if (!installment.IsRepaid && installment.ExpectedDate > pDate)
                {
                    if (pInstallment == null)
                    {
                        pInstallment = installment;

                        if (olb == 0)
                        {
                            olb = pContract.CalculateExpectedOlb(pInstallment.Number, false);
                        }

                        //is case APR on the first installment
                        DateTime expectedDate;

                        if (i == 0)
                        {
                            expectedDate = pContract.StartDate;
                        }
                        else
                        {
                            priorInstallment = pContract.GetInstallment(i - 1);
                            expectedDate = priorInstallment.ExpectedDate;
                        }

                        int daySpan = (pDate - expectedDate).Days < 0 ? 0 : (pDate - expectedDate).Days;
                        int daysInMonth = DateTime.DaysInMonth(pDate.Year, pDate.Month);

                        if (pGeneralSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                        {
                            interests = pInstallment.InterestsRepayment * daySpan / daysInMonth;
                        }
                        else
                        {
                            interests = pInstallment.ExpectedDate > pDate ? 0 : pInstallment.InterestsRepayment;
                        }
                    }

                    fees += CalculateCommision(pContract, pDate, installment.Number, pPaymentType, pGeneralSettings);
                }

                if (!installment.IsRepaid && installment.ExpectedDate <= pDate)
                {
                    ammountHasToPay += installment.InterestsRepayment + installment.PrincipalHasToPay;
                }
            }

            if (interests != 0)
                interests = pContract.UseCents ? Math.Round(interests.Value, 2) : Math.Round(interests.Value, 0);

            OCurrency amount = interests + fees + ammountHasToPay;

            return amount;
        }

        #region Private methods
        private static OCurrency _CalculateNonRepaymentPenaltiesForClosure(Loan pContract, DateTime pDate, int pInstallmentNumber,
            OLateRepaymentTypes repaymentType, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            Installment installment = pContract.GetInstallment(pInstallmentNumber - 1);
            if (pDate <= installment.ExpectedDate) return 0;
            
            DateTime baseDate = DateTime.MinValue;

            DateTime dateForInstallment = installment.PaidDate.HasValue ? installment.PaidDate.Value : installment.ExpectedDate;

            if (baseDate == DateTime.MinValue)
            {
                baseDate = dateForInstallment;
            }
            else
            {
                if (baseDate < dateForInstallment)
                    baseDate = dateForInstallment;
            }

            int pstDueDays = (pDate - baseDate).Days < 0 ? 0 : (pDate - baseDate).Days;
            pstDueDays = pstDueDays - CountDaysOff(baseDate, pDate, pGeneralSettings, pNonWorkingDate);
            
            if(pContract.GracePeriodOfLateFees >= pstDueDays)
            {
                pstDueDays = 0;
            }

            double penalties;
            OCurrency amountHasToPay;

            if (repaymentType == OLateRepaymentTypes.Principal)
            {
                amountHasToPay = installment.PrincipalHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDuePrincipal;
            }
            else if (repaymentType == OLateRepaymentTypes.Interest)
            {
                amountHasToPay = installment.InterestHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDueInterest;
            }
            else if (repaymentType == OLateRepaymentTypes.Olb)
            {
                amountHasToPay = installment.OLB;
                penalties = pContract.NonRepaymentPenalties.OLB;
            }
            else if (repaymentType == OLateRepaymentTypes.Amount)
            {
                amountHasToPay = installment.Amount;
                penalties = pContract.NonRepaymentPenalties.InitialAmount;
            }
            else // Fixed method of late repayments for future implementation; Principal is set for now
            {
                amountHasToPay = installment.PrincipalHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDuePrincipal;
            }

            return amountHasToPay * penalties * pstDueDays;
        }

        private static OCurrency _CalculateNonRepaymentPenalties(Loan pContract, DateTime pDate, int installmentNumber,
            OLateRepaymentTypes repaymentType, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNonWorkingDate)
        {
            Installment installment = pContract.GetInstallment(installmentNumber - 1);

            DateTime date = installment.ExpectedDate;

            foreach (RepaymentEvent rPayment in pContract.Events.GetRepaymentEvents())
            {
                if (rPayment.Date == pDate && rPayment.Deleted == false && installmentNumber == rPayment.InstallmentNumber)
                {
                    date = pDate;
                }

                if (rPayment.InstallmentNumber <= installment.Number && rPayment.Deleted == false && rPayment.Date != pDate && pDate > installment.ExpectedDate && installment.PaidFees != 0)
                {
                    date = rPayment.Date <= installment.ExpectedDate ? installment.ExpectedDate : rPayment.Date;
                }
            }

            int nbOfDays = (pDate - date).Days < 0 ? 0 : (pDate - date).Days;
            nbOfDays = nbOfDays - CountDaysOff(date, pDate, pGeneralSettings, pNonWorkingDate);
            nbOfDays = _CalculatePastDueWithGeneralParameterForRepayment(nbOfDays, pGeneralSettings);

            if (pContract.GracePeriodOfLateFees >= nbOfDays)
            {
                nbOfDays = 0;
            }

            double penalties;
            OCurrency amountHasToPay;

            if (repaymentType == OLateRepaymentTypes.Principal)
            {
                amountHasToPay = installment.PrincipalHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDuePrincipal;
            }
            else if (repaymentType == OLateRepaymentTypes.Interest)
            {
                amountHasToPay = installment.InterestHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDueInterest;
            }
            else if (repaymentType == OLateRepaymentTypes.Olb)
            {
                amountHasToPay = installment.OLB;
                penalties = pContract.NonRepaymentPenalties.OLB;
            }
            else if (repaymentType == OLateRepaymentTypes.Amount)
            {
                amountHasToPay = installment.Amount;
                penalties = pContract.NonRepaymentPenalties.InitialAmount;
            }
            else // Fixed method of late repayments for future implementation; Principal is set for now
            {
                amountHasToPay = installment.PrincipalHasToPay;
                penalties = pContract.NonRepaymentPenalties.OverDuePrincipal;
            }

            return amountHasToPay * penalties * nbOfDays;
        }

        private static OCurrency _AnticipateCalculateNonRepaymentPenalties(Loan pContract, DateTime pDate, int installmentNumber, 
            OPaymentType pPaymentType, ApplicationSettings pGeneralSettings)
        {
            Installment installment = pContract.GetInstallment(installmentNumber - 1);
            Installment preInstallment;
            DateTime expectedDate;

            if (installmentNumber != 1)
            {
                preInstallment = pContract.GetInstallment(installmentNumber - 2);
                expectedDate = preInstallment.ExpectedDate;
            }
            else
            {
                preInstallment = pContract.GetInstallment(installmentNumber - 1);
                expectedDate = pContract.StartDate;
            }

            OCurrency amountHasToPay;
            //calculation of remaining interest
            if (((preInstallment.IsRepaid) || (installmentNumber == 1)) || (!preInstallment.IsRepaid && preInstallment.ExpectedDate < pDate))
            {
                int days = (pDate - expectedDate).Days < 0 ? pContract.NumberOfDaysInTheInstallment(installment.Number, pDate) : pContract.NumberOfDaysInTheInstallment(installment.Number, pDate) - (pDate - expectedDate).Days;
                
                if (pGeneralSettings.AccountingProcesses == OAccountingProcesses.Accrual)
                {
                    amountHasToPay = installment.InterestsRepayment / pContract.NumberOfDaysInTheInstallment(installment.Number, pDate) * days;
                    
                    if (pContract.EscapedMember != null)
                    {
                        amountHasToPay = installment.InterestsRepayment * pContract.EscapedMember.LoanShareAmount / pContract.Amount;
                        amountHasToPay = (installment.InterestsRepayment - amountHasToPay) * pContract.EscapedMember.LoanShareAmount / pContract.Amount;
                    }
                }
                else
                {
                    amountHasToPay = installment.InterestsRepayment - installment.PaidInterests;
                    if (pContract.EscapedMember != null)
                    {
                        amountHasToPay = amountHasToPay * pContract.EscapedMember.LoanShareAmount / pContract.Amount;
                    }
                }
            }
            else
            {
                amountHasToPay = installment.InterestsRepayment;
                if (pContract.EscapedMember != null)
                {
                    amountHasToPay = amountHasToPay * pContract.EscapedMember.LoanShareAmount / pContract.Amount;
                }
            }

            if (OPaymentType.TotalPayment == pPaymentType || pContract.EscapedMember != null)
                return amountHasToPay.Value * Convert.ToDecimal(pContract.AnticipatedTotalRepaymentPenalties);

            return amountHasToPay.Value * Convert.ToDecimal(pContract.AnticipatedPartialRepaymentPenalties);
        }

        private static OCurrency AnticipateCalculateFeesBasedOnOlb(Loan pContract, DateTime date, int installmentNumber, OPaymentType paymentType)
        {
            Installment preInstallment = installmentNumber != 1 
                ? pContract.GetInstallment(installmentNumber - 2) 
                : pContract.GetInstallment(installmentNumber - 1);

            OCurrency amountHasToPay;

            if (((preInstallment.IsRepaid) || (installmentNumber == 1)) || (!preInstallment.IsRepaid && preInstallment.ExpectedDate <= date))
            {
                if (pContract.EscapedMember != null)
                {
                    //calculate olb for the person of the group
                    amountHasToPay = (pContract.CalculateExpectedOlb(installmentNumber, false) *
                                 pContract.EscapedMember.LoanShareAmount / pContract.Amount);
                }
                else
                {
                    amountHasToPay = pContract.CalculateExpectedOlb(installmentNumber, false) - pContract.GetInstallment(installmentNumber - 1).PaidCapital;
                }

            }
            else
            {
                amountHasToPay = 0;
            }

            if (paymentType == OPaymentType.TotalPayment || pContract.EscapedMember != null)
                return amountHasToPay.Value * Convert.ToDecimal(pContract.AnticipatedTotalRepaymentPenalties);

            return amountHasToPay.Value * Convert.ToDecimal(pContract.AnticipatedPartialRepaymentPenalties);
        }

        private static OCurrency CalculateCommision(Loan pContract, DateTime pDate, int number, OPaymentType pPaymentType, ApplicationSettings pGeneralSettings)
        {
            OCurrency commision;

            if (pContract.Product.AnticipatedTotalRepaymentPenaltiesBase == OAnticipatedRepaymentPenaltiesBases.RemainingInterest)
            {
                commision = _AnticipateCalculateNonRepaymentPenalties(pContract, pDate, number, pPaymentType, pGeneralSettings);
            }
            else
            {
                commision = AnticipateCalculateFeesBasedOnOlb(pContract, pDate, number, pPaymentType);
            }
            return commision;
        }

        #endregion
    }
}