using System;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Contracts.Loans;
using Foms.CoreDomain.FundingLines;
using Foms.CoreDomain.Products;

namespace Foms.CoreDomain.Contracts.Guarantees
{
    [Serializable]
    public class Guarantee : IContract
    {
        GuaranteeProduct _guaranteesPackage;

        public string CreditCommitteeCode{ get; set;}
        public string Banque{ get; set;}

       public Guarantee()
        {

        }

        public Guarantee(User pUser, ApplicationSettings pGeneralSettings, NonWorkingDateSingleton pNwds, ProvisionTable pPt)
        {
        }

        public GuaranteeProduct Guaranteespackage
        {
            get { return _guaranteesPackage; }
            set { _guaranteesPackage = value; }
        }
        public OCurrency Amount { get; set; }
        public OCurrency AmountLimit { get; set; }

        public OCurrency AmountGuaranted { get; set; }

        public double? GuaranteeFees { get; set; }

        public OClientTypes ClientType { get; set; }

        public bool Activated { get; set; }


        public string GenerateGuaranteeCode()
        {
            string year = StartDate.Year.ToString().Substring(2, 2);
            int subStringLength = _guaranteesPackage.Name.Length < 4 ? _guaranteesPackage.Name.Length : 4;
            string guaranteePackageName = _guaranteesPackage.Name.Substring(0, subStringLength).Trim().ToUpper();
            return string.Format("{0}/{1}/{2}/{3}/{4}-", "G", BranchCode , year, Id, guaranteePackageName);
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string BranchCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public bool Rural { get; set; }
        public bool Closed { get; set; }
        public OContractStatus ContractStatus { get; set; }
        public DateTime? CreditCommiteeDate { get; set; }
        public string CreditCommiteeComment { get; set; }
        public string CreditCommiteeCode { get; set; }

        public Project Project { get; set; }

        public LoanProduct Product { get; set; }
        public User LoanOfficer { get; set; }
        public FundingLine FundingLine { get; set; }
    }
}