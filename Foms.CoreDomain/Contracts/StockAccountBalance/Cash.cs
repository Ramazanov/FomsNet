using System;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Contracts.Loans;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Events;

namespace Foms.CoreDomain.Contracts.StockAccountBalance
{
    [Serializable]
    public static class Cash
    {
        public static OCurrency GetStockBalance(Loan pCredit, ApplicationSettings settings, ProvisionTable pProvisionningTable, string pAccountNumber)
        {
            OCurrency balance = 0;
            return balance;
        }

        private static OCurrency AccruedInterestsRescheduleLoanBalance(Loan pCredit)
        {
            return 0;
        }

        private static OCurrency _AccruedInterestsLoanBalance(Loan pCredit)
        {
            return 0;
        }

        private static OCurrency _DeferredIncomeBalance(Loan pCredit)
        {
            return 0;
        }

        private static OCurrency _LoanLossBalance(Loan pCredit, int writeOffDays)
        {
            if (pCredit.WrittenOff || pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return pCredit.GetOlb();
            return 0;
        }

        private static OCurrency _LoanLossReserveBalance(Loan pCredit,ProvisionTable pProvisionningTable, int writeOffDays)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return balance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return balance;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (installment.ExpectedDate >= TimeProvider.Today)
                    break;
                balance += (installment.InterestsRepayment - installment.PaidInterests);
            }

            balance += pCredit.GetUnpaidLatePenalties(TimeProvider.Today);
            
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            balance += pCredit.GetOlb() * Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(pastDueDays).Rate);
            
            OCurrency realBalance = 0;
            
            return balance > realBalance ? balance : realBalance;
        }

        private static OCurrency _InterestsPastDueLoanBalance(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return balance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return balance;
            foreach (Installment installment in pCredit.InstallmentList)
            {
                if (installment.ExpectedDate > TimeProvider.Today)
                    break;
                balance += (installment.InterestsRepayment - installment.PaidInterests);
            }

            return balance;
        }

        private static OCurrency _PenaltiesPastDueLoansBalanceAsset(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0) return balance;
            if (pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return balance;

            balance += pCredit.GetUnpaidLatePenalties(TimeProvider.Today);
            
            return balance;
        }

        private static OCurrency _InterestOnCashCreditBalance(Loan pCredit)
        {
            if (pCredit.Rescheduled) return 0;

            OCurrency balance = 0;
            balance += pCredit.GetPaidInterest();
            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance -= e.Interests;
            }
            return balance;

        }

        private static OCurrency _UnrecoverableBadLoanBalance(Loan pCredit, int badLoanDays, int writeOffDays)
        {
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            if (pastDueDays > badLoanDays && pastDueDays <= writeOffDays)
                return pCredit.GetOlb();
            else
                return 0;
        }

        private static OCurrency _InterestOnRescheduledloanBalance(Loan pCredit)
        {
            if (!pCredit.Rescheduled) return 0;
            
            OCurrency balance = 0;
            balance += pCredit.GetPaidInterest();
            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance -= e.Interests;
            }
            return balance;
        }

        private static OCurrency _InterestOnBadloanBalance(Loan pCredit)
        {
            OCurrency balance = 0;

            if (!pCredit.Disbursed) return 0;
            
            if(pCredit.GetPastDueDays(TimeProvider.Today) > 0)
            {
                foreach (Installment installment in pCredit.InstallmentList)
                {
                    if (installment.ExpectedDate > TimeProvider.Today)
                        break;
                    balance += (installment.InterestsRepayment - installment.PaidInterests);
                }
            }

            OCurrency realBalance = 0;
            
            return balance > realBalance ? balance : realBalance;
        }

        private static OCurrency _CommissionsBalance(Loan pCredit)
        {
            return pCredit.GetPaidCommissions();
        }

        private static OCurrency _ProvisionWriteOffBalance(Loan pCredit, int writeOffDays)
        {
            OCurrency balance = 0;
            if (pCredit.WrittenOff || pCredit.GetPastDueDays(TimeProvider.Today) > writeOffDays) return pCredit.GetOlb();

            foreach (BadLoanRepaymentEvent e in pCredit.Events.GetEventsByType(typeof(BadLoanRepaymentEvent)))
            {
                balance += e.AccruedProvision;
            }
            return balance;
        }

        private static OCurrency _LiabilitiesLoanLossBalance(Loan pCredit,ProvisionTable pProvisionningTable)
        {
            if (pCredit.Disbursed && !pCredit.BadLoan)
                if (pCredit.GetPastDueDays(TimeProvider.Today) == 0)
                    return pCredit.GetOlb() * Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate);
                else
                    return 0;
            else
                return 0;
        }

        private static OCurrency _ExpensesLoanLossBalance(Loan pCredit,ProvisionTable pProvisionningTable)
        {
            if (pCredit.Disbursed && !pCredit.AllInstallmentsRepaid && !pCredit.WrittenOff)
                return pCredit.Amount * Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate);
            else
                return 0;
        }

        private static OCurrency _IncomeLoanLossBalance(Loan pCredit, ProvisionTable pProvisionningTable)
        {
            OCurrency olb = pCredit.GetOlb();
            OCurrency rate = Convert.ToDecimal(pProvisionningTable.GetProvisiningRateByNbOfDays(0).Rate);
            if (pCredit.GetPastDueDays(TimeProvider.Today) != 0) olb = 0;
            if (pCredit.WrittenOff) return 0;

            if (!pCredit.Disbursed) return 0;

            if (pCredit.AllInstallmentsRepaid) return 0;

            if (!pCredit.BadLoan) return (pCredit.Amount - olb) * rate;

            else return (pCredit.Amount) * rate;
        }

        private static OCurrency _BadLoanBalance(Loan pCredit, int badLoanDays)
        {
            int pastDueDays = pCredit.GetPastDueDays(TimeProvider.Today);
            if (pastDueDays <= badLoanDays && pastDueDays != 0)
                return pCredit.GetOlb();
            else
                return 0;
        }

        private static OCurrency _RescheduledLoanBalance(Loan pCredit)
        {
            if (pCredit.Rescheduled)
                return pCredit.GetOlb();
            else
                return 0;
        }

        private static OCurrency _CashStockBalance(Loan pCredit)
        {
            OCurrency balance = 0;
            if (!pCredit.Disbursed) return balance;

            balance += pCredit.GetPaidFees();
            balance += pCredit.GetPaidInterest();
            balance += pCredit.GetPaidPrincipal();
            
            return balance - pCredit.Amount;
        }

        private static OCurrency _CashCreditBalance(Loan pCredit)
        {
            if (!pCredit.Disbursed || pCredit.Rescheduled) return 0;

            if (pCredit.GetPastDueDays(TimeProvider.Today) == 0)
                return pCredit.GetOlb();
            else
                return 0;

        }
    }
}