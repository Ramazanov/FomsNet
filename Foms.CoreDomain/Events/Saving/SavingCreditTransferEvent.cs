using System;

namespace Foms.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingCreditTransferEvent : SavingTransferEvent
    {
        public override string Code
        {
            get { return OSavingEvents.CreditTransfer; }
        }

        public override OCurrency GetAmountForBalance()
        {
            return Amount;
        }
    }
}