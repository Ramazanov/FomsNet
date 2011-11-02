using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.FundingLines;

namespace Foms.CoreDomain.Products
{
    public interface IProduct
    {
        int Id { get; set; }
        bool Delete { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        char ClientType { get; set; }
        OProductTypes ProductType { get; }
        FundingLine FundingLine { get; set; }
        Currency Currency { get; set; }
        OCurrency Amount { get; set; }
        OCurrency AmountMin { get; set; }
        OCurrency AmountMax { get; set; }
        double? InterestRate { get; set; }
        double? InterestRateMin { get; set; }
        double? InterestRateMax { get; set; }
    }
}
