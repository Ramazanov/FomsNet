using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    public interface IInterestRepayment
    {
        void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pInterestEvent,ref OCurrency pInterestPrepayment);
    }
}