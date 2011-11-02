using System;
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Contracts.Loans.LoanRepayment.Repayment
{
    [Serializable]
    public class RepaymentMethod
    {
        private readonly Loan _contract;
        private readonly CreditContractOptions _cco;

        public RepaymentMethod(Loan pContract, CreditContractOptions pCCO)
        {
            _contract = pContract;
            _cco = pCCO;
        }

        public void Repay(ref OCurrency pAmountPaid, ref OCurrency pFeesEvent, ref OCurrency pCommissionsEvent, ref OCurrency pInterestEvent)
        {
            if (_cco.CancelFees && (_cco.ManualFeesAmount + _cco.ManualCommissionAmount) >= 0)
            {
                pCommissionsEvent = _cco.ManualCommissionAmount;
                pFeesEvent = _cco.ManualFeesAmount;

                if (AmountComparer.Compare(pAmountPaid, _cco.ManualFeesAmount + _cco.ManualCommissionAmount) > 0)
                {
                    //pAmountPaid -= _cco.ManualFeesAmount + _cco.ManualCommissionAmount;
                }
                else
                    pAmountPaid = 0;
            }

            if (_cco.KeepExpectedInstallments)
            {
                if (_cco.CancelInterests && _cco.ManualInterestsAmount >= 0)
                {
                    OCurrency manualInterestAmount = _cco.ManualInterestsAmount;
                    for(int i = 0;i<_contract.NbOfInstallments;i++)
                    {
                        Installment installment = _contract.GetInstallment(i);
                        if(!installment.IsRepaid && pAmountPaid > 0 && manualInterestAmount > 0)
                        {
                            OCurrency interestHasToPay = installment.InterestsRepayment - installment.PaidInterests;
                            if (AmountComparer.Compare(manualInterestAmount,interestHasToPay) > 0)
                            {
                                pInterestEvent += interestHasToPay;
                                installment.PaidInterests = installment.InterestsRepayment;
                                pAmountPaid -= interestHasToPay;
                                manualInterestAmount -= interestHasToPay;
                            }
                            else
                            {
                                pInterestEvent += manualInterestAmount;
                                installment.PaidInterests += manualInterestAmount;
                                pAmountPaid -= manualInterestAmount;
                                manualInterestAmount = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}