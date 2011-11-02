﻿//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
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
using Foms.CoreDomain.Accounting.Interfaces;
using Foms.CoreDomain.Contracts.Loans;
using Foms.CoreDomain.Contracts.Savings;
using Foms.CoreDomain.FundingLines;

namespace Foms.CoreDomain.Accounting
{
    [Serializable]
    public class ContractChartOfAccounts : IChartOfAccounts
    {
        private List<Account> _accounts;
        private AccountingRuleCollection _accountingRules;

        public ContractChartOfAccounts(int pCurrencyID)
		{
            _accounts = new List<Account>();
            ContractDefaultAccounts = new List<Account>();
            _FillDefaultAccountsList(pCurrencyID);
		}
        public ContractChartOfAccounts(List<Account> pAccounts)
        {
            _accounts = _CopyEachAccount(pAccounts);
            ContractDefaultAccounts = DefaultAccounts.DefaultAccount(_accounts[0].CurrencyId);
            
        }

        public AccountingRuleCollection AccountingRuleCollection
        {
            get { return _accountingRules; }
            set { _accountingRules = value; }
        }

        public void AddAccount(Account account)
		{
			_accounts.Add(account);
		}
        public void SetActualAccountsList(List<Account> actualAccounts)
        {
            _accounts = actualAccounts;
        }
        public void FillAccountsList()
        {
            _accounts = ContractDefaultAccounts;
        }
        public Account GetAccountByNumber(string pNumber, int pCurrencyId)
        {
            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        public Account GetAccountByNumber(string pNumber, int pCurrencyId, ISavingsContract pSavingsContract, OBookingDirections pBookingDirection)
        {
            if (_accountingRules != null)
            {
                var specificAccount = _accountingRules.GetSpecificAccount(pNumber, pSavingsContract, pBookingDirection);
                if (specificAccount != null)
                    pNumber = specificAccount.Number;
            }

            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        public Account GetAccountByNumber(string pNumber, int pCurrencyId, IContract pContract, OBookingDirections pBookingDirection)
        {
            if (_accountingRules != null)
            {
                var specificAccount = _accountingRules.GetSpecificAccount(pNumber, pContract, pBookingDirection);
                if (specificAccount != null)
                    pNumber = specificAccount.Number;
            }

            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        public Account GetAccountByNumber(string pNumber, int pCurrencyId, FundingLine pFundingLine, OBookingDirections pBookingDirection)
        {
            if (_accountingRules != null)
            {
                var specificAccount = _accountingRules.GetSpecificAccount(pNumber, pFundingLine, pBookingDirection);
                if (specificAccount != null)
                    pNumber = specificAccount.Number;
            }

            return _getAccountByNumber(pNumber, pCurrencyId);
        }

        private Account _getAccountByNumber(string pNumber, int pCurrencyId)
        {
            Account a = new Account();
            foreach (Account account in _accounts)
            {
                if (account.Number == pNumber && account.CurrencyId == pCurrencyId)
                    a = account;
                
            }
            return a;//_accounts.FirstOrDefault(item => item.Number.Equals(pNumber) && item.CurrencyId == pCurrencyId);
        }

        private void _FillDefaultAccountsList(int pCurrencyID)
        {
            ContractDefaultAccounts = DefaultAccounts.DefaultAccount(pCurrencyID);
            FillAccountsList();
        }
		/// <summary>
		/// This method is required to book a movement set
		/// Each elementary mvt is differently booked if it is a debit or a credit movement
		/// </summary>
		/// <param name="movementSet">the movement set to book</param>
		public void Book(AccountingTransaction movementSet)
		{
			foreach(Booking Booking in movementSet.Bookings)
			{
                _getAccountByNumber(Booking.DebitAccount.Number, Booking.DebitAccount.CurrencyId).Debit(Booking.Amount);
                _getAccountByNumber(Booking.CreditAccount.Number, Booking.CreditAccount.CurrencyId).Credit(Booking.Amount);
			}
		}

		public void UnBook(AccountingTransaction movementSet)
		{
			foreach(Booking Booking in movementSet.Bookings)
			{
                _getAccountByNumber(Booking.CreditAccount.Number, Booking.CreditAccount.CurrencyId).Debit(Booking.Amount);
                _getAccountByNumber(Booking.DebitAccount.Number, Booking.DebitAccount.CurrencyId).Credit(Booking.Amount);
			}
		}

        public List<Account> Accounts
		{
			get
			{
                _accounts.Sort((x,y)=> x.Number.CompareTo(y.Number));
				return _accounts;
			}
			set {_accounts = value;}
		}

        public ContractChartOfAccounts Copy()
        {
            ContractChartOfAccounts chartOfAccounts = new ContractChartOfAccounts(_accounts);
            chartOfAccounts._accountingRules = _accountingRules;
            //foreach (Account account in _accounts)
            //{
            //    chartOfAccounts.GetAccountByNumber(account.Number, account.Currency_Id).Balance = account.Balance;
            //    chartOfAccounts.GetAccountByNumber(account.Number, account.Currency_Id).StockBalance = account.StockBalance;
            //}
         //   chartOfAccounts.Accounts = _accounts;
            return chartOfAccounts;
        }
        private List<Account> _CopyEachAccount(List<Account> pAccounts)
        {
            List < Account > newAccounts = new List<Account>();

            foreach (Account account in pAccounts)
            {
                Account newAccount = new Account();
                newAccount = account.Copy();
                //newAccount.Balance = 0;
                //newAccount.StockBalance = 0;
                newAccounts.Add(newAccount);
            }
            return newAccounts;
        }
        public List<Account> ContractDefaultAccounts { get; private set; }

    }
}
