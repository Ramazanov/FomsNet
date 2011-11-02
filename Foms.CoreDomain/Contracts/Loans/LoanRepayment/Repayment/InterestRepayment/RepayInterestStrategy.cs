using System;
using Foms.CoreDomain.Contracts.Loans.Installments;
using Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment.InterestRepayment
{
    [Serializable]
    public class RepayInterestStrategy
    {
        private readonly IInterestRepayment _methodToCalculateInterest;
        public RepayInterestStrategy(CreditContractOptions pCCO)
        {
            if (pCCO.CancelInterests && pCCO.KeepExpectedInstallments)
                _methodToCalculateInterest = new ManualMethod();
            else
                _methodToCalculateInterest = new AutomaticMethod();
        }

        public void RepayInterest(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent,ref OCurrency pInterestPrepayment)
        {
            _methodToCalculateInterest.Repay(pInstallment, ref pAmountPaid, ref pInterestEvent,ref pInterestPrepayment);
        }
    }
}