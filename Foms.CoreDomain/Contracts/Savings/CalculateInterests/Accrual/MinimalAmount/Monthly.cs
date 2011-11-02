using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Accrual.MinimalAmount
{
    public class Monthly : ICalculateInterests
    {
        private Saving _saving;
        private User _user;

        public Monthly(Saving pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastClosureDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.DateCalculationMonthly(lastClosureDate, pClosureDate))
            {

                DateTime accrualDate = new DateTime(lastClosureDate.AddMonths(1).Year, lastClosureDate.AddMonths(1).Month, 01);
                listInterestsAccrualEvent.Add(GetInterests(lastClosureDate, accrualDate));

                lastClosureDate = lastClosureDate.AddMonths(1);
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
