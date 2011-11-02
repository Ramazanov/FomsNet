using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests
{
    public class PostingInterestsStrategy : IPostingInterests
    {
        private IPostingInterests _ipi;

        public PostingInterestsStrategy(ISavingsContract pSaving, User pUser, int pWeekEndDay2)
        {
            if (pSaving is Saving)
            {
                if (((Saving)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfYear)
                    _ipi = new Posting.EndOfYear(pSaving, pUser);
                else if (((Saving)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfMonth)
                    _ipi = new Posting.EndOfMonth((Saving)pSaving, pUser);
                else if (((Saving)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfWeek)
                    _ipi = new Posting.EndOfWeek((Saving)pSaving, pUser, pWeekEndDay2);
                else if (((Saving)pSaving).Product.InterestFrequency == OSavingInterestFrequency.EndOfDay)
                    _ipi = new Posting.EndOfDay((Saving)pSaving, pUser);
            }
            else if (pSaving is SavingDeposit)
            {
                _ipi = new Posting.Maturity((SavingDeposit)pSaving, pUser);
            }
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate)
        {
            return _ipi.PostingInterests(pDate);
        }
    }
}
