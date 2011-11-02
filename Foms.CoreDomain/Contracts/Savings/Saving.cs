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
using System.Diagnostics;
using System.Linq;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Clients;
using Foms.CoreDomain.Contracts.Loans;
using Foms.CoreDomain.Contracts.Savings.CalculateInterests;
using Foms.CoreDomain.Events.Saving;
using Foms.CoreDomain.Products;

namespace Foms.CoreDomain.Contracts.Savings
{
    [Serializable]
	public class Saving : SavingsContract
    {
        private List<Loan> _loans;

        new public SavingBookProduct Product 
        {
            get { return (SavingBookProduct)base.Product; }
            set { base.Product = value; } 
        }

        public OCurrency FlatWithdrawFees { get; set; }
        public double? RateWithdrawFees { get; set; }

        public OCurrency FlatTransferFees { get; set; }
        public double? RateTransferFees { get; set; }

        public OCurrency DepositFees { get; set; }
        public OCurrency ChequeDepositFees { get; set; }
        
        public OCurrency OverdraftFees { get; set; }
        public bool InOverdraft { get; set; }

        public OCurrency ManagementFees { get; set; }
        public double? AgioFees { get; set; }

        public OCurrency CloseFees { get; set; }
        public OCurrency ReopenFees { get; set; }

        public Saving(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser)
        {
            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
            _loans = new List<Loan>();
        }

        public Saving(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, SavingBookProduct pProduct)
        {
            base.Product = pProduct;

            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
            _loans = new List<Loan>();
        }

        public Saving(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, DateTime pCreationDate, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;

            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
            _loans = new List<Loan>();
        }

        public Saving(ApplicationSettings pApplicationSettings, ChartOfAccounts pChartOfAccounts, User pUser, 
            DateTime pCreationDate, SavingBookProduct pProduct, IClient pClient)
        {
            Client = pClient;
            CreationDate = pCreationDate;
            base.Product = pProduct;

            _events = new List<SavingEvent>();
            _chartOfAccounts = _FillChartOfAccounts(pChartOfAccounts);
            _applicationSettings = pApplicationSettings;
            _user = pUser;
            _loans = new List<Loan>();
        }

        public List<Loan> Loans
        {
            get { return _loans; }
            set { _loans = value; }
        }

        public void AddLoan(Loan loanAccount)
        {
            _loans.Add(loanAccount);
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

        public override List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser,
            bool pIsDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            SavingEvent savingEvent;

            if (isPending)
            {
                savingEvent = new SavingPendingDepositEvent();
            }
            else
            {
                savingEvent = new SavingDepositEvent();
            }

            savingEvent.Amount = pAmount;
            savingEvent.Date = pDate;
            savingEvent.Description = pDescription;
            savingEvent.User = pUser;
            savingEvent.Cancelable = true;
            savingEvent.IsPending = isPending;
            savingEvent.SavingsMethod = savingsMethod;
            savingEvent.PendingEventId = pendingEventId;
            savingEvent.ProductType = typeof(SavingBookProduct);

            _events.Add(savingEvent);
            events.Add(savingEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                savingEvent.Fee = savingsMethod == OSavingsMethods.Cheque ? ChequeDepositFees : DepositFees;

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
                Fee = 0m,
                ProductType = typeof(SavingBookProduct)
            };
            _events.Add(withdrawEvent);
            events.Add(withdrawEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                withdrawEvent.Fee = Product.WithdrawFeesType == OSavingsFeesType.Flat ? FlatWithdrawFees : pAmount * RateWithdrawFees.Value;

            return events;
        }

        public override List<SavingEvent> ChargeOverdraftFee(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();

            SavingOverdraftFeeEvent overdraftFeeEvent = new SavingOverdraftFeeEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                ProductType = typeof(SavingBookProduct)
            };
            _events.Add(overdraftFeeEvent);
            events.Add(overdraftFeeEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                overdraftFeeEvent.Fee = OverdraftFees;

            return events;
        }

        public override List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, 
            string pDescription, User pUser, bool pIsDesactivateFees)
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
                ProductType = typeof(SavingBookProduct)
            }; 
            events.Add(transferEvent);
            _events.Add(transferEvent);

            if (Status != OSavingsStatus.Closed && !pIsDesactivateFees)
                    transferEvent.Fee = Product.TransferFeesType == OSavingsFeesType.Flat ? FlatTransferFees : pAmount * RateTransferFees.Value;

            return events;
        }

        public override List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, 
            int? pendingEventId)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingPendingDepositRefusedEvent refuseEvent = new SavingPendingDepositRefusedEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0m,
                SavingsMethod = method,
                PendingEventId = pendingEventId,
                ProductType = typeof(SavingBookProduct)
            };

            events.Add(refuseEvent);
            _events.Add(refuseEvent);

            return events;
        }

        public override List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingReopenEvent reopenEvent = new SavingReopenEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = false,
                Fee = 0m,
                ProductType = typeof(SavingBookProduct)
            };

            if (!pIsDesactivateFees) reopenEvent.Fee = ReopenFees;

            events.Clear();
            events.Add(reopenEvent);

            Status = OSavingsStatus.Active;
            ClosedDate = null;
            return events;
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
                Fee = 0m,
                ProductType = typeof(SavingBookProduct)
            };

            if (!pIsDesactivateFees) closeEvent.Fee = CloseFees;

            listEvents.Clear();
            listEvents.Add(closeEvent);
            Status = OSavingsStatus.Closed;
            ClosedDate = pDate;

            return listEvents;
        }

        public override List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            return Close(pDate, pUser, pDescription, pIsDesactivateFees);
        }

        public override SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user)
        {
            SavingCreditOperationEvent spEvent = new SavingCreditOperationEvent
            {
                Amount = amount,
                Date = date,
                Description = description,
                User = user,
                Cancelable = true,
                ProductType = typeof(SavingBookProduct)
            };

            _events.Add(spEvent);
            return spEvent;
        }

        public override SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user)
        {
            SavingDebitOperationEvent spEvent = new SavingDebitOperationEvent
            {
                Amount = amount,
                Date = date,
                Description = description,
                User = user,
                Cancelable = true,
                ProductType = typeof(SavingBookProduct)
            };

            _events.Add(spEvent);
            return spEvent;
        }

        protected DateTime GetLastManagementEventFeeDate()
        {
            SavingEvent savingEvent = _events.OrderByDescending(item => item.Date).
                FirstOrDefault(item => (item is SavingManagementFeeEvent || item is SavingClosureEvent) && !item.Deleted);
            return (null == savingEvent) ? CreationDate : savingEvent.Date;
        }

        private List<SavingEvent> GenerateManagementFeeEvent(DateTime prevDate, DateTime nextDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            const string desc = "Management fee of {0:.00} for {1:dd.MM.yyyy} - {2:dd.MM.yyyy}";
            object[] items = new object[] { ManagementFees.GetFormatedValue(true), prevDate, nextDate };

            SavingManagementFeeEvent smfe = new SavingManagementFeeEvent
            {
                Amount = 0m,
                Cancelable = true,
                Date = nextDate,
                Description = string.Format(desc, items),
                Fee = ManagementFees.HasValue ? ManagementFees : 0m,
                User = _user,
                ProductType = Product.GetType()
            };
            retval.Add(smfe);
            _AddSavingEvent(smfe);

            OCurrency balance = GetBalance(nextDate);
            if (balance < 0)
            {
                if (!InOverdraft)
                {
                    SavingOverdraftFeeEvent overdraftFeeEvent = new SavingOverdraftFeeEvent
                    {
                        Amount = 0m,
                        Date = nextDate,
                        Description = "Overdraft fee event",
                        User = _user,
                        Cancelable = false,
                        Fee = OverdraftFees,
                        ProductType = typeof(SavingBookProduct)
                    };

                    _AddSavingEvent(overdraftFeeEvent);
                    retval.Add(overdraftFeeEvent);
                    InOverdraft = true;
                }
            }

            return retval;
        }

        private DateTime GetLastAgioEventDate()
        {
            SavingEvent savingEvent = _events.OrderByDescending(item => item.Date).
                FirstOrDefault(item =>(item is SavingAgioEvent || item is SavingClosureEvent) && !item.Deleted);
            return (null == savingEvent) ? CreationDate : savingEvent.Date;
        }

        private SavingAgioEvent GenerateAgioEvent(DateTime prevDate, DateTime nextDate)
        {
            OCurrency balance = GetBalance(nextDate);
            if (balance < 0)
            {
                const string desc = "Agio of {0} for {1:dd.MM.yyyy} - {2:dd.MM.yyyy}";
                object[] items = new object[] { Math.Abs((balance * AgioFees.Value).Value), prevDate, nextDate };

                SavingAgioEvent savingAgioEvent = new SavingAgioEvent
                {
                    Amount = 0m,
                    Cancelable = true,
                    Date = nextDate,
                    Description = string.Format(desc, items),
                    Fee = Math.Abs((balance * AgioFees.Value).Value),
                    User = _user,
                    ProductType = Product.GetType()
                };

                _AddSavingEvent(savingAgioEvent);
                return savingAgioEvent;
            }

            return null;
        }

        public override List<SavingEvent> Closure(DateTime date, User user)
        {
            List<SavingEvent> retval;
            switch (Product.InterestFrequency)
            {
                case OSavingInterestFrequency.EndOfYear:
                    retval = PostingEndOfYear(date, user);
                    break;
                case OSavingInterestFrequency.EndOfMonth:
                    retval = PostingEndOfMonth(date, user);
                    break;
                case OSavingInterestFrequency.EndOfWeek:
                    retval = PostingEndOfWeek(date, user);
                    break;
                case OSavingInterestFrequency.EndOfDay:
                    retval = PostingEndOfDay(date, user);
                    break;
                default:
                    Debug.Fail("Savings closure: debug fail!");
                    retval = new List<SavingEvent>();
                    break;
            }

            retval.AddRange(ManagementFeesCharge(date));
            /*SavingManagementFeeEvent smfe;
            while ((smfe = GenerateManagementFeeEvent(date)) != null)
            {
                Events.Add(smfe);
                retval.Add(smfe);
            }*/

            retval.AddRange(AgioFeesCharge(date));
            /*SavingAgioEvent savingAgioEvent;
            while ((savingAgioEvent = GenerateAgioEvent(date)) != null)
            {  
                Events.Add(savingAgioEvent);
                retval.Add(savingAgioEvent);
            }*/

            retval.AddRange(base.Closure(date, user));
            return retval;
        }

        private List<SavingEvent> AgioFeesCharge(DateTime closureDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            DateTime prevDate = GetLastAgioEventDate();
            DateTime nextDate = prevDate;
            nextDate = nextDate.AddMonths(Product.AgioFeesFreq.NbOfMonths);
            nextDate = nextDate.AddDays(Product.AgioFeesFreq.NbOfDays);

            while (nextDate <= closureDate)
            {
                SavingAgioEvent savingAgioEvent = GenerateAgioEvent(prevDate, nextDate);
                if (savingAgioEvent != null)
                    if (savingAgioEvent.Fee > 0)
                        retval.Add(savingAgioEvent);

                prevDate = nextDate;
                nextDate = nextDate.AddMonths(Product.AgioFeesFreq.NbOfMonths);
                nextDate = nextDate.AddDays(Product.AgioFeesFreq.NbOfDays);
            }
            
            return retval;
        }

        private List<SavingEvent> ManagementFeesCharge(DateTime closureDate)
        {
            List<SavingEvent> retval = new List<SavingEvent>();

            DateTime prevDate = GetLastManagementEventFeeDate();
            DateTime nextDate = prevDate;
            nextDate = nextDate.AddMonths(Product.ManagementFeeFreq.NbOfMonths);
            nextDate = nextDate.AddDays(Product.ManagementFeeFreq.NbOfDays);

            while (nextDate <= closureDate)
            {
                List<SavingEvent> list = GenerateManagementFeeEvent(prevDate, nextDate);
                if (list != null)
                {
                    if (list[0].Fee > 0) retval.Add(list[0]);
                    if (list.Count > 1 && list[1].Fee > 0) retval.Add(list[1]);
                }

                prevDate = nextDate;
                nextDate = nextDate.AddMonths(Product.ManagementFeeFreq.NbOfMonths);
                nextDate = nextDate.AddDays(Product.ManagementFeeFreq.NbOfDays);
            }

            /*
            List<SavingEvent> list;
            while ((list = GenerateManagementFeeEvent(pDate)) != null)
            {
                retval.Add(list[0]);
                if (list.Count > 1) retval.Add(list[1]);
            }*/

            return retval;
        }

        public DateTime GetNextWeekly(DateTime pDate)
        {
            int weekEndDay2 = _applicationSettings.WeekEndDay2 == 6 ? 1 : _applicationSettings.WeekEndDay2 + 1;

            return pDate.AddDays((weekEndDay2 <= (int)pDate.DayOfWeek) ? 7 - ((int)pDate.DayOfWeek - weekEndDay2)
                   : weekEndDay2 - (int)pDate.DayOfWeek);
        }

        private List<SavingEvent> PostingEndOfDay(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationDiary(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddDays(1);

                events.AddRange(_AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day), pUser)));
                events.AddRange(_AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day), pUser)));
            }

            return events;
        }

        private List<SavingEvent> PostingEndOfWeek(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationWeekly(lastPostingDate, pDate, _applicationSettings.WeekEndDay2))
            {
                lastPostingDate = DateCalculationStrategy.GetNextWeekly(lastPostingDate, _applicationSettings.WeekEndDay2);

                events.AddRange(_AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day), pUser)));
                events.AddRange(_AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, lastPostingDate.Day), pUser)));
            }

            events.AddRange(_AddSavingEvent(CalculateInterest(pDate, pUser)));

            return events;
        }

        private List<SavingEvent> PostingEndOfMonth(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationMonthly(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddMonths(1);

                events.AddRange(_AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, lastPostingDate.Month, 01), pUser)));
                events.AddRange(_AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, lastPostingDate.Month, 01), pUser)));
            }

            if ((Product).InterestBase != OSavingInterestBase.Monthly)
                events.AddRange(_AddSavingEvent(CalculateInterest(pDate, pUser)));

            return events;
        }

        private List<SavingEvent> PostingEndOfYear(DateTime pDate, User pUser)
        {
            DateTime lastPostingDate = GetLastPostingDate();
            List<SavingEvent> events = new List<SavingEvent>();

            while (DateCalculationStrategy.DateCalculationYearly(lastPostingDate, pDate))
            {
                lastPostingDate = lastPostingDate.AddYears(1);

                events.AddRange(_AddSavingEvent(CalculateInterest(new DateTime(lastPostingDate.Year, 01, 01), pUser)));
                events.AddRange(_AddSavingEvent(PostingInterests(new DateTime(lastPostingDate.Year, 01, 01), pUser)));
            }

            events.AddRange(_AddSavingEvent(CalculateInterest(pDate, pUser)));
            return events;
        }

	    public bool UseCents
	    {
	        get
	        {
	            return null == Product ? true : Product.UseCents;
	        }
	    }

        public new bool HasPendingEvents()
        {
            return Events.Any(savingEvent => savingEvent.IsPending);
        }

        #region ICloneable Members

        public override object Clone()
        {
            Saving saving = (Saving)MemberwiseClone();
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
