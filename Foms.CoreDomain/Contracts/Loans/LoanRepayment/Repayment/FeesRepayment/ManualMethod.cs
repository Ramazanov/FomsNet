using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class ManualMethod : IFeesRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            //Nothing to modify
            pInstallment.PaidFees += pFeesEvent;
            pAmountPaid -= pFeesEvent;
            pFeesEvent = 0;
        }
    }
}