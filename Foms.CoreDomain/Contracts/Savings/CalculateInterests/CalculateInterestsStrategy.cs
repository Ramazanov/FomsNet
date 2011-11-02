using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class CalculateInterestsStrategy : ICalculateInterests
    {
        private readonly ICalculateInterests _ici;

        public CalculateInterestsStrategy(ISavingsContract pSaving, User pUser, int pWeekEndDay2)
        {
            if (pSaving is Saving)
            {
                if (((Saving)pSaving).Product.InterestBase == OSavingInterestBase.Daily)
                    _ici = new Accrual.Daily(pSaving, pUser);
                else if (((Saving)pSaving).Product.InterestBase == OSavingInterestBase.Monthly)
                {
                    if (((Saving)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                        _ici = new Accrual.MinimalAmount.Monthly((Saving)pSaving, pUser);
                }
                else if (((Saving)pSaving).Product.InterestBase == OSavingInterestBase.Weekly)
                {
                    if (((Saving)pSaving).Product.CalculAmountBase == OSavingCalculAmountBase.MinimalAmount)
                        _ici = new Accrual.MinimalAmount.Weekly((Saving)pSaving, pUser, pWeekEndDay2);
                }
            }
            else if (pSaving is SavingDeposit)
            {
                if (((SavingDeposit)pSaving).Product.InterestFrequency == OTermDepositInterestFrequency.Daily)
                    _ici = new Accrual.Daily(pSaving, pUser);
                else if (((SavingDeposit)pSaving).Product.InterestFrequency == OTermDepositInterestFrequency.Maturity)
                    _ici = new Accrual.Maturity((SavingDeposit)pSaving, pUser);
            }
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(System.DateTime closureDate)
        {
            return _ici.CalculateInterest(closureDate);
        }
    }
}
