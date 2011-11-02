using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Accrual
{
    public class Daily : ICalculateInterests
    {
        private User _user;
        private ISavingsContract _saving;

        public Daily(ISavingsContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationDiary(lastClosureDate, pClosureDate))
            {
                lastClosureDate = lastClosureDate.AddDays(1);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate));
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent GetInterests(DateTime closureDate)
        {
            OCurrency interests = 0;
            double interestRate;

            OCurrency amount = _saving.GetBalance(closureDate);

            if (_saving is Saving)
            {
                interestRate = _saving.InterestRate;
                interests = interestRate * amount;
            }
            else if (_saving is SavingDeposit)
            {
                interestRate = _saving.InterestRate * ((SavingDeposit)_saving).NumberPeriods;

                DateTime nextMaturity = ((SavingDeposit)_saving).NextMaturity.Value;
                DateTime lastMaturity = DateCalculationStrategy.GetLastMaturity(nextMaturity,
                    ((SavingDeposit)_saving).Product.Periodicity, ((SavingDeposit)_saving).NumberPeriods);

                int numberOfDays = nextMaturity.Subtract(lastMaturity).Days;
                interestRate = interestRate / numberOfDays;

                if (closureDate < nextMaturity || lastMaturity < _saving.GetLastPostingDate())
                {
                    interests = interestRate * amount;
                }
                else
                {
                    OCurrency interestTotal = _saving.InterestRate * amount * ((SavingDeposit)_saving).NumberPeriods;
                    OCurrency interestAccrued = interestRate * amount * (numberOfDays - 1);
                    interests = interestTotal - interestAccrued;
                }
            }

            return new SavingInterestsAccrualEvent()
            {
                Amount = interests,
                Date = closureDate,
                Fee = 0,
                User = _user, 
                Cancelable = true,
                ProductType = _saving.Product.GetType()
            };
        }
    }
}
