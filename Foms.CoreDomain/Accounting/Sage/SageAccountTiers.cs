using System;

namespace Foms.CoreDomain.Accounting.Sage
{
    [Serializable]
    public class SageAccountTiers
    {
        public int ContractId { get; set; }
        public string ContractCode { get; set; }
        public DateTime DisbursmentDate { get; set; }
        public int ClientId { get; set; }
        public OClientTypes  ClientType { get; set; }
        public string ClientName { get; set; }
        public int LoanOfficerId { get; set; }
        public string LoanOfficerName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Account CollectiveAccount { get; set; }
    }
}
