using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment
{
    [Serializable]
    public class ManualMethod : IInterestRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent, ref OCurrency pInterestPrepayment)
        {
            //Nothing to modify
        }
    }
}