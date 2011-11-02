using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfDay : IPostingInterests
    {
        private Saving _saving;
        private User _user;

        public EndOfDay(Saving pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate;

            while (DateCalculationStrategy.DateCalculationDiary(lastPostingDate, pDate))
            {
                currentPostingDate = lastPostingDate.AddDays(1);
                
                OCurrency interestsToPost = 0;
                foreach (SavingEvent savEvent in _saving.Events)
                    if (savEvent is SavingInterestsAccrualEvent && savEvent.Date <= currentPostingDate && savEvent.Date > lastPostingDate)
                        interestsToPost += savEvent.Amount.Value;

                list.Add(new SavingInterestsPostingEvent()
                {
                    Date = new DateTime(currentPostingDate.Year, currentPostingDate.Month, currentPostingDate.Day),
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", lastPostingDate, currentPostingDate),
                    Amount = interestsToPost,
                    User = _user, 
                    Cancelable = true,
                    ProductType = _saving.Product.GetType()
                });

                lastPostingDate = lastPostingDate.AddDays(1);
            }

            return list;
        }
    }
}
