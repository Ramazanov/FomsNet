using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment
{
    [Serializable]
    public class AutomaticMethod : IInterestRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent, ref OCurrency pInterestPrepayment)
        {
            if (AmountComparer.Compare(pAmountPaid, pInstallment.InterestsRepayment - pInstallment.PaidInterests) > 0)
            {
                OCurrency interestHasToPay = pInstallment.InterestsRepayment - pInstallment.PaidInterests;

                pAmountPaid -= interestHasToPay;
                pInterestEvent += interestHasToPay;
                pInterestPrepayment += interestHasToPay;
                pInstallment.PaidInterests += interestHasToPay;
            }
            else
            {
                pInstallment.PaidInterests += pAmountPaid;
                pInterestEvent += pAmountPaid;
                pInterestPrepayment += pAmountPaid;
                pAmountPaid = 0;
            }
        }
    }
}