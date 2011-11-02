using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Interfaces
{
    public interface IFeesRepayment
    {
        void Repay(Installment pInstallment, ref OCurrency pAmountPaid, ref OCurrency pFeesEvent);
    }
}