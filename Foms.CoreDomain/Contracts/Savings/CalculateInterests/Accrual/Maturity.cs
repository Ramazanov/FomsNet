using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Accrual
{
    public class Maturity : ICalculateInterests
    {
        private SavingDeposit _saving;
        private User _user;

        public Maturity(SavingDeposit pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pClosureDate)
        {
            List<SavingInterestsAccrualEvent> listInterestsAccrualEvent = new List<SavingInterestsAccrualEvent>();

            DateTime lastAccrualDate = _saving.GetLastAccrualDate();

            while (DateCalculationStrategy.GetNextMaturity(lastAccrualDate, _saving.Product.Periodicity, _saving.NumberPeriods) <= pClosureDate)
            {
                lastAccrualDate = DateCalculationStrategy.GetNextMaturity(lastAccrualDate, _saving.Product.Periodicity, _saving.NumberPeriods);
                listInterestsAccrualEvent.Add(_GetInterests(lastAccrualDate));
            }

            return listInterestsAccrualEvent;
        }

        private SavingInterestsAccrualEvent _GetInterests(DateTime pAccrualDate)
        {
            OCurrency interests;

            double interestRate = _saving.InterestRate * _saving.NumberPeriods;
            OCurrency amount = _saving.GetBalance(pAccrualDate); 

            interests = interestRate * amount;

            return new SavingInterestsAccrualEvent()
            {
                Amount = interests,
                Date = pAccrualDate,
                Fee = 0,
                User = _user, 
                Cancelable = true,
                ProductType = _saving.Product.GetType()
            };
        }
    }
}
