using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Accrual.MinimalAmount
{
    public class Weekly : ICalculateInterests
    {
        private Saving _saving;
        private User _user;
        private int _weekEndDay2;

        public Weekly(Saving pSaving, User pUser, int pWeekEndDay2)
        {
            _saving = pSaving;
            _user = pUser;
            _weekEndDay2 = pWeekEndDay2;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationWeekly(lastClosureDate, pClosureDate, _weekEndDay2))
            {
                DateTime accrualDate = DateCalculationStrategy.GetNextWeekly(lastClosureDate, _weekEndDay2);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));
                lastClosureDate = accrualDate;
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent GetInterests(DateTime pLastClosureDate, DateTime pClosureDate)
        {
            OCurrency interests;
            double interestRate = _saving.InterestRate;

            OCurrency minimalAmount = _saving.GetBalanceMin(pLastClosureDate);
            DateTime currentDate = pLastClosureDate.AddDays(1); 

            while (currentDate < pClosureDate)
            {
                OCurrency amountAtCurrentDate = _saving.GetBalanceMin(currentDate);

                if (minimalAmount > amountAtCurrentDate)
                    minimalAmount = amountAtCurrentDate;

                currentDate = currentDate.AddDays(1);
            }

            interests = interestRate * minimalAmount;

            return new SavingInterestsAccrualEvent()
            {
                Amount = interests,
                Date = pClosureDate,
                Fee = 0,
                User = _user, 
                Cancelable = true,
                ProductType = _saving.Product.GetType()
            };
        }
    }
}
