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
using System.Linq;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Clients;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests;
using Foms.CoreDomain.Events.Saving;
using Foms.CoreDomain.Products;

namespace Foms.CoreDomain.Contracts.Savings
{
    public delegate void AccountAtMaturityHandler(SavingDeposit savingDeposit, DateTime date, User user);

	[Serializable]
	public class SavingDeposit : SavingsContract
	{
        public event AccountAtMaturityHandler AccountAtMaturity;

        private int _numberPeriods;
        private OSavingsRollover _rollover;
        private ISavingsContract _transferAccount;
        private double _withdrawalFees;
        private DateTime? _nextMaturity;

        public OCurrency Fees { get; set; }

        public int NumberPeriods
        {
            get { return _numberPeriods; }
            set { _numberPeriods = value; }
        }

        public OSavingsRollover Rollover
        {
            get { return _rollover; }
            set { _rollover = value; }
        }

        public ISavingsContract TransferAccount
        {
            get { return _transferAccount; }
            set { _transferAccount = value; }
        }

        public double WithdrawalFees
        {
            get { return _withdrawalFees; }
            set { _withdrawalFees = value; }
        }

        public DateTime? NextMaturity
        {
            get { return _nextMaturity; }
            set { _nextMaturity = value; }
        }

        new public TermDepositProduct Product 
        {
            get { return (TermDepositProduct)base.Product; }
            set { base.Product = value; } 
        }

        public SavingDeposit(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser)
        {
            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
        }

        public SavingDeposit(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, TermDepositProduct pProduct)
        {
            _events = new List<SavingEvent>();
            base.Product = pProduct;
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
        }

        public SavingDeposit(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, DateTime pCreationDate, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;

            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
        }

        public SavingDeposit(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, 
            DateTime pCreationDate, TermDepositProduct pProduct, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;
            base.Product = pProduct;

            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
        }

        public override List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, OCurrency pEntryFees, User pUser)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            SavingInitialDepositEvent initialEvent = new SavingInitialDepositEvent
            {
                Amount = pInitialAmount,
                Date = pCreationDate,
                Description = "First deposit",
                User = pUser,
                Fee = pEntryFees,
                ProductType = typeof(SavingBookProduct)
            };

            _events.Add(initialEvent);
            events.Add(initialEvent);

            CreationDate = pCreationDate;

            return events;
        }

        public override List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingWithdrawEvent withdrawEvent = new SavingWithdrawEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                ProductType = typeof(TermDepositProduct)
            };
            events.Add(withdrawEvent);
            _events.Add(withdrawEvent);
            events.AddRange(_calculateWithdrawFees(pDate, pUser, withdrawEvent, pIsDesactivateFees));

            return events;
        }

        private List<SavingEvent> _calculateWithdrawFees(DateTime pDate, User pUser, SavingEvent pSavingEvent, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            OCurrency fees;
            OCurrency interestsToPost = this.ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT,
                    this.Product == null || this.Product.Currency == null ? 1 : this.Product.Currency.Id, this, OBookingDirections.Both).Balance;

            if (interestsToPost > 0)
            {
                SavingInterestsPostingEvent interestsPostingEvent = new SavingInterestsPostingEvent()
                {
                    Date = pDate,
                    Amount = interestsToPost,
                    User = _user,
                    Cancelable = true,
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", this.GetLastPostingDate(), pDate),
                    ProductType = typeof(TermDepositProduct)
                };

                events.Add(interestsPostingEvent);
                _events.Add(interestsPostingEvent);
            }

            if (!pIsDesactivateFees)
            {
                if (Product.WithdrawalFeesType == OTermDepositFeesType.OnInterests)
                {
                    fees = interestsToPost * WithdrawalFees;
                }
                else
                {
                    fees = pSavingEvent.Amount * WithdrawalFees;
                }

                ((ISavingsFees)pSavingEvent).Fee = fees;
            }

            return events;
        }

        public override List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingDebitTransferEvent transferEvent = new SavingDebitTransferEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                RelatedContractCode = pCreditAccount.Code,
                ProductType = typeof(TermDepositProduct)
            };
            events.Add(transferEvent);
            _events.Add(transferEvent);
            events.AddRange(_calculateWithdrawFees(pDate, pUser, transferEvent, pIsDesactivateFees));

            return events;
        }

        public override List<SavingEvent> SimulateClose(DateTime date, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            List<SavingEvent> listEvents = Closure(date, pUser);
            SavingInterestsPostingEvent postingEvent = _PostPayableInterests(date, pUser);
            if (postingEvent != null)
                listEvents.Add(postingEvent);

            SavingWithdrawEvent withdrawEvent = new SavingWithdrawEvent
            {
                Amount = 0,
                Date = date,
                Description = "Simulate Close - " + pDescription,
                User = pUser,
                Cancelable = true,
                ProductType = typeof(TermDepositProduct)
            };

            if (!pIsDesactivateFees)
            {
                if (date != DateCalculationStrategy.GetLastMaturity(NextMaturity.Value, Product.Periodicity, NumberPeriods))
                {
                    OCurrency fees;

                    if (Product.WithdrawalFeesType == OTermDepositFeesType.OnInterests)
                    {
                        fees = postingEvent != null ? postingEvent.Amount * WithdrawalFees : 0;
                    }
                    else
                    {
                        fees = GetBalance(date) - (GetBalance(date) / (1 + WithdrawalFees));
                        //fees = Balance * WithdrawalFees;
                    }

                    withdrawEvent.Fee = fees;
                }
            }

            _events.Add(withdrawEvent);
            listEvents.Add(withdrawEvent);

            Status = OSavingsStatus.Closed;
            ClosedDate = date;

            return listEvents;
        }

        public override List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, int? pendingEventId)
        {
            throw new NotImplementedException();
        }

        public override List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

        public override List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            List<SavingEvent> listEvents = Closure(pDate, pUser);
            SavingInterestsPostingEvent postingEvent = _PostPayableInterests(pDate, pUser);
            if (postingEvent != null) listEvents.Add(postingEvent);
            OCurrency amountToPost = listEvents.Where(item => item is SavingInterestsPostingEvent).Sum(item => item.Amount.Value);

            SavingCloseEvent closeEvent = new SavingCloseEvent
            {
                Amount = amountToPost,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0,
                ProductType = typeof(SavingDeposit)
            };

            listEvents.Clear();
            listEvents.Add(closeEvent);

            Status = OSavingsStatus.Closed;
            ClosedDate = pDate;

            return listEvents;
        }

        public override List<SavingEvent> ChargeOverdraftFee(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

	    public override List<SavingEvent> Closure(DateTime date, User user)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            while (NextMaturity <= date)
            {
                if (!(Rollover == OSavingsRollover.None && DateCalculationStrategy.GetLastMaturity(NextMaturity.Value, Product.Periodicity, NumberPeriods) != CreationDate))
                {
                    events.AddRange(_AddSavingEvent(CalculateInterest(NextMaturity.Value, user)));
                    events.AddRange(_AddSavingEvent(PostingInterests(NextMaturity.Value, user)));

                    DateTime transferDate = NextMaturity.GetValueOrDefault();
                    NextMaturity = DateCalculationStrategy.GetNextMaturity(NextMaturity.Value, Product.Periodicity, NumberPeriods);

                    if (AccountAtMaturity != null && Rollover != OSavingsRollover.PrincipalAndInterests)
                        AccountAtMaturity(this, transferDate, user);
                }
                else
                    break;
            }

            if (!(Rollover == OSavingsRollover.None && GetLastPostingDate() != null))
                events.AddRange(_AddSavingEvent(CalculateInterest(date, user)));

            events.AddRange(base.Closure(date, user));

            return events;
        }

        public bool HasPendingEvents()
        {
            return Events.Any(savingEvent => savingEvent.IsPending);
        }

	    #region ICloneable Members

        public override object Clone()
        {
            SavingDeposit saving = (SavingDeposit)MemberwiseClone();
            saving.Events = new List<SavingEvent>();
            saving.Events.AddRange(Events);
            saving.ChartOfAccounts = new ContractChartOfAccounts(saving.Product.Currency.Id);
            foreach (Account account in ChartOfAccounts.Accounts)
            {
                Account accountCopy = saving.ChartOfAccounts.Accounts.FirstOrDefault(item => item.Number == account.Number);
                if (accountCopy != null)
                    accountCopy.Balance = account.Balance;
            }
            return saving;
        }

        #endregion
    }
}
