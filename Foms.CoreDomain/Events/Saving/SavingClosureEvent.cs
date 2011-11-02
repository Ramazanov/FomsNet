using System;

namespace Foms.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingClosureEvent : SavingEvent
    {
        public override string Code
        {
            get { return OSavingEvents.SavingClosure; }
        }

        public override string Description { get; set; }
    }
}