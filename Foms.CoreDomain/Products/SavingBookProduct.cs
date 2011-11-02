//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.Collections.Generic;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Contracts.Loans.Installments;

namespace Foms.CoreDomain.Products
{
	[Serializable]
	public class SavingBookProduct : ISavingProduct
    {
        #region ISavingProduct Members
        public int Id { get; set; }
		public bool Delete { get; set; }
		public string Name { get; set; }
        public string Code { get; set; }
		public OClientTypes ClientType { get; set; }
		public OProductTypes ProductType { get { return OProductTypes.Saving; } }
        public OCurrency InitialAmountMin { get; set; }
        public OCurrency InitialAmountMax { get; set; }
		public OCurrency BalanceMin { get; set; }
		public OCurrency BalanceMax { get; set; }
		public OCurrency WithdrawingMin { get; set; }
		public OCurrency WithdrawingMax { get; set; }
		public OCurrency DepositMin { get; set; }
		public OCurrency DepositMax { get; set; }
        public OCurrency TransferMin { get; set; }
        public OCurrency TransferMax { get; set; }
		public double? InterestRate { get; set; }
		public double? InterestRateMin { get; set; }
		public double? InterestRateMax { get; set; }
        public OCurrency EntryFeesMin { get; set; }
        public OCurrency EntryFeesMax { get; set; }
        public OCurrency EntryFees { get; set; }
        public Currency Currency { get; set; }
        #endregion

        public OSavingInterestBase InterestBase { get; set; }
        public OSavingInterestFrequency InterestFrequency { get; set; }
        public OSavingCalculAmountBase? CalculAmountBase { get; set; }

        public OSavingsFeesType WithdrawFeesType { get; set; }
        public OCurrency FlatWithdrawFeesMin { get; set; }
        public OCurrency FlatWithdrawFeesMax { get; set; }
        public OCurrency FlatWithdrawFees { get; set; }
        public double? RateWithdrawFeesMin { get; set; }
        public double? RateWithdrawFeesMax { get; set; }
        public double? RateWithdrawFees { get; set; }

        public OSavingsFeesType TransferFeesType { get; set; }
        public OCurrency FlatTransferFeesMin { get; set; }
        public OCurrency FlatTransferFeesMax { get; set; }
        public OCurrency FlatTransferFees { get; set; }
        public double? RateTransferFeesMin { get; set; }
        public double? RateTransferFeesMax { get; set; }
        public double? RateTransferFees { get; set; }

        public OCurrency DepositFees { get; set; }
        public OCurrency DepositFeesMin { get; set; }
        public OCurrency DepositFeesMax { get; set; }

        public OCurrency ChequeDepositFees { get; set; }
        public OCurrency ChequeDepositFeesMin { get; set; }
        public OCurrency ChequeDepositFeesMax { get; set; }

        public OCurrency ChequeDepositMin { get; set; }
        public OCurrency ChequeDepositMax { get; set; }

        public OCurrency CloseFees { get; set; }
        public OCurrency CloseFeesMin { get; set; }
        public OCurrency CloseFeesMax { get; set; }

        public OCurrency ManagementFees { get; set; }
        public OCurrency ManagementFeesMin { get; set; }
        public OCurrency ManagementFeesMax { get; set; }
        public InstallmentType ManagementFeeFreq { get; set; }

        public OCurrency OverdraftFees { get; set; }
        public OCurrency OverdraftFeesMin { get; set; }
        public OCurrency OverdraftFeesMax { get; set; }

        public double? AgioFees { get; set; }
        public double? AgioFeesMin { get; set; }
        public double? AgioFeesMax { get; set; }
        public InstallmentType AgioFeesFreq { get; set; }

        public OCurrency ReopenFees { get; set; }
        public OCurrency ReopenFeesMin { get; set; }
        public OCurrency ReopenFeesMax { get; set; }

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

        public OPackageMode PackageMode { get; set; }
    }
}
