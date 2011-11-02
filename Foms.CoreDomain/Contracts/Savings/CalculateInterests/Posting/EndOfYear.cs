using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfYear : IPostingInterests
    {
        private ISavingsContract _saving;
        private User _user;

        public EndOfYear(ISavingsContract pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate;

            while (DateCalculationStrategy.DateCalculationYearly(lastPostingDate, pDate))
            {
                currentPostingDate = new DateTime(lastPostingDate.AddYears(1).Year, 01, 01);
                
                //OCurrency interestsToPost = _saving.InterestRate * _saving.GetBalance(currentPostingDate);
                OCurrency interestsToPost = 0;
                foreach (SavingEvent savEvent in _saving.Events)
                    if (savEvent is SavingInterestsAccrualEvent && savEvent.Date <= currentPostingDate && savEvent.Date > lastPostingDate)
                        interestsToPost += savEvent.Amount.Value;

                list.Add(new SavingInterestsPostingEvent()
                {
                    Date = currentPostingDate,
                    Amount = interestsToPost,
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", lastPostingDate, currentPostingDate),
                    User = _user,
                    Cancelable = true,
                    ProductType = _saving.Product.GetType()
                });
                
                lastPostingDate = lastPostingDate.AddYears(1);
            }
            
            return list;
        }
    }
}
