using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class AutomaticMethod : IFeesRepayment
    {
        public void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            if (AmountComparer.Compare(pAmountPaid, pInstallment.FeesUnpaid) > 0)
            {
                pFeesEvent += pInstallment.FeesUnpaid;
                pAmountPaid -= pInstallment.FeesUnpaid;
                pInstallment.PaidFees += pInstallment.FeesUnpaid;
                pInstallment.FeesUnpaid = 0;

            }
            else
            {
                pFeesEvent += pAmountPaid;
                pInstallment.PaidFees += pAmountPaid;
                pInstallment.FeesUnpaid -= pAmountPaid;
                pAmountPaid = 0;
            }
        }
    }
}