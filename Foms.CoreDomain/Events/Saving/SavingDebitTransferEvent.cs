using System;

namespace Foms.CoreDomain.Events.Saving
{
    [Serializable]
    public class SavingDebitTransferEvent : SavingTransferEvent
    {
        public override string Code
        {
            get { return OSavingEvents.DebitTransfer; }
        }

        public override OCurrency GetAmountForBalance()
        {
            return (decimal)-1 * Amount;
        }

        public override OCurrency GetFeeForBalance()
        {
            return (decimal)-1 * Fee;
        }
    }
}