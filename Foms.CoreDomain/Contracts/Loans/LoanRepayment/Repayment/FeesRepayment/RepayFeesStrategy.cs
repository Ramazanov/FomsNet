using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.FeesRepayment
{
    [Serializable]
    public class RepayFeesStrategy
    {
        private readonly IFeesRepayment _methodToCalculateFees;
        public RepayFeesStrategy(CreditContractOptions pCCO)
        {
            // || pCCO.KeepExpectedInstallments
            if (!pCCO.CancelFees)
                _methodToCalculateFees = new AutomaticMethod();
            else
                _methodToCalculateFees = new ManualMethod();
        }

        public void RepayFees(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent)
        {
            _methodToCalculateFees.Repay(pInstallment,ref pAmountPaid, ref pFeesEvent);
        }
    }
}