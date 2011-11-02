using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class EndOfWeek : IPostingInterests
    {
        private Saving _saving;
        private User _user;
        private int _weekEndDay2;

        public EndOfWeek(Saving pSaving, User pUser, int pWeekEndDay2)
        {
            _saving = pSaving;
            _user = pUser;
            _weekEndDay2 = pWeekEndDay2;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate;

            while (DateCalculationStrategy.DateCalculationWeekly(lastPostingDate, pDate, _weekEndDay2))
            {
                currentPostingDate = DateCalculationStrategy.GetNextWeekly(lastPostingDate, _weekEndDay2);
                
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

                lastPostingDate = currentPostingDate;
            }

            return list;
        }
    }
}
