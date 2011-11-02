using System;
using Foms.CoreDomain.FundingLines;
using Foms.CoreDomain.Products;

namespace Foms.CoreDomain.Contracts.Loans
{
    public interface IContract
    {
        int Id { get; set; }
        string Code { get; set; }
        string BranchCode { get; set; }
        DateTime CreationDate { get; set; }
        DateTime StartDate { get; set; }
        DateTime CloseDate { get; set; }
        bool Rural { get; set; }
        bool Closed { get; set; }
        OContractStatus ContractStatus { get; set; }
        DateTime? CreditCommiteeDate { get; set; }
        string CreditCommiteeComment { get; set; }
        string CreditCommitteeCode { get; set;}
        LoanProduct Product{ get; set;}
        User LoanOfficer { get; set; }
        FundingLine FundingLine { get; set; }
        //Corporate Corporate { get; set; }
        Project Project { get; set; }
    }
}
