﻿using System;
using System.Diagnostics;

// TODO: merge this class with the same for loans
namespace Foms.CoreDomain.SearchResult
{
    [Serializable]
    public class SavingSearchResult
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ContractCode { get; set; }
        public OClientTypes ClientType { get; set; }
        public OSavingsStatus Status { get; set; }
        public string ClientTypeCode { get; set; }
        public string ClientName { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string ContractType { get; set; }
        public User LoanOfficer { get; set; }

        public bool IsViewableBy(User user)
        {
            Debug.Assert(LoanOfficer != null, "Loan officer is null.");
            return LoanOfficer.Equals(user)
                ? true
                : user.HasAsSubordinate(LoanOfficer);
        }
    }
}