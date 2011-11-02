using System;

namespace Foms.Enums
{
    [Serializable]
    public enum OPaymentMethods
    {
        All = 0,
        Cash = 1,
        Voucher = 2,
        Withdrawal = 3,
        DirectDebit = 4,
        WireTransfer = 5,
        DebitCard = 6
    }
}
