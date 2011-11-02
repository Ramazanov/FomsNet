using System;

namespace Foms.CoreDomain.Accounting.Sage
{
    [Serializable]
    public class SageBooking : ICloneable
    {
        public int MovementSetId { get; set; }
        public int ContractId { get; set; }
        public string ContractCode { get; set; }
        public string JournalCode { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string AccountTiers { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string PartNumber { get; set; }
        public string BookingType { get; set; }
        public string Reference { get; set; }
        public OCurrency Amount { get; set; }
        public Account Account { get; set; }
        public OBookingDirections Direction { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
