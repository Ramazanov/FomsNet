using System;
using System.Collections.Generic;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Products
{
    [Serializable]
    public class TermDepositProduct : ISavingProduct
    {
        #region ISavingProduct Members
        public int Id { get; set; }
        public bool Delete { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Octopus.Enums.OClientTypes ClientType { get; set; }
        public OCurrency InitialAmountMin { get; set; }
        public OCurrency InitialAmountMax { get; set; }
        public Octopus.Shared.OCurrency BalanceMin { get; set; }
        public Octopus.Shared.OCurrency BalanceMax { get; set; }
        public Octopus.Shared.OCurrency WithdrawingMin { get; set; }
        public Octopus.Shared.OCurrency WithdrawingMax { get; set; }
        public Octopus.Shared.OCurrency DepositMin { get; set; }
        public Octopus.Shared.OCurrency DepositMax { get; set; }
        public Octopus.Shared.OCurrency TransferMin { get; set; }
        public Octopus.Shared.OCurrency TransferMax { get; set; }
        public double? InterestRate { get; set; }
        public double? InterestRateMin { get; set; }
        public double? InterestRateMax { get; set; }
        public Octopus.Shared.OCurrency EntryFeesMin { get; set; }
        public Octopus.Shared.OCurrency EntryFeesMax { get; set; }
        public Octopus.Shared.OCurrency EntryFees { get; set; }
        public Currency Currency { get; set; }   
        #endregion

        public InstallmentType Periodicity { get; set; }
        public int? NumberPeriod { get; set; }
        public int? NumberPeriodMin { get; set; }
        public int? NumberPeriodMax { get; set; }
        public OTermDepositInterestFrequency InterestFrequency { get; set; }
        public OTermDepositFeesType WithdrawalFeesType { get; set; }
        public double? WithdrawalFees { get; set; }
        public double? WithdrawalFeesMin { get; set; }
        public double? WithdrawalFeesMax { get; set; }
        public OCurrency ChequeDepositMin { get; set; }
        public OCurrency ChequeDepositMax { get; set; }

        private bool _hasValue;

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public TermDepositProduct()
        {
            _hasValue = true;
        }

        public bool UseCents
        {
            get
            {
                return null == Currency ? true : Currency.UseCents;
            }
        }

        private List<ProductClientType> productClientTypes = new List<ProductClientType>();

        public List<ProductClientType> ProductClientTypes
        {
            get { return productClientTypes; }
            set { productClientTypes = value; }
        }
    }
}
