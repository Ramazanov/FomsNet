using System;
using System.Collections.Generic;
using Foms.CoreDomain.Events.Saving;

namespace Foms.CoreDomain.Contracts.Savings.CalculateInterests.Interfaces
{
    public interface IPostingInterests
    {
        List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate);
    }
}
