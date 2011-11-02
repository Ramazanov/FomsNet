using System;
using System.Collections.Generic;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Posting
{
    public class Maturity : IPostingInterests
    {
        private SavingDeposit _saving;
        private User _user;

        public Maturity(SavingDeposit pSaving, User pUser)
        {
            _saving = pSaving;
            _user = pUser;
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate)
        {
            List<SavingInterestsPostingEvent> list = new List<SavingInterestsPostingEvent>();

            DateTime lastPostingDate = _saving.GetLastPostingDate();
            DateTime currentPostingDate = _saving.NextMaturity.Value;

            while (currentPostingDate <= pDate)
            {
                currentPostingDate = _saving.NextMaturity.Value;

                OCurrency interestsToPost = _saving.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT,
                    _saving.Product == null || _saving.Product.Currency == null ? 1 : _saving.Product.Currency.Id, _saving, OBookingDirections.Both).Balance;

                list.Add(new SavingInterestsPostingEvent()
                {
                    Date = currentPostingDate,
                    Amount = interestsToPost,
                    User = _user, 
                    Cancelable = true, 
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", lastPostingDate, currentPostingDate),
                    ProductType = _saving.Product.GetType()
                });

                lastPostingDate = currentPostingDate;
                currentPostingDate = DateCalculationStrategy.GetNextMaturity(currentPostingDate, _saving.Product.Periodicity, _saving.NumberPeriods);
            }

            return list;
        }
    }
}
