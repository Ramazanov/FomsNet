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
    [Serializable]
    public abstract class SavingsContract : ISavingsContract
    {
        public int Id { get; set;}
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public double InterestRate { get; set; }
        public ISavingProduct Product { get; set; }
        public OSavingsStatus Status { get; set; }
        public IClient Client { get; set; }
        public User SavingsOfficer { get; set; }

        protected ApplicationSettings _applicationSettings;
        protected List<SavingEvent> _events;
        protected ContractChartOfAccounts _chartOfAccounts;
        protected User _user;

        public User User
        {
            get { return _user; }
        }

        public List<SavingEvent> Events
        {
            get { return _events; }
            protected set { _events = value; }
        }

        public override string ToString()
        {
            return Code;
        }

        public OCurrency InitialAmount
        {
            get 
            {
                SavingInitialDepositEvent initialDepositEvent = (SavingInitialDepositEvent)Events.FirstOrDefault(item => item is SavingInitialDepositEvent);
                return (initialDepositEvent != null) ? initialDepositEvent.Amount : null;
            }
        }

        public OCurrency EntryFees
        {
            get
            {
                SavingInitialDepositEvent initialDepositEvent = (SavingInitialDepositEvent)Events.FirstOrDefault(item => item is SavingInitialDepositEvent);
                return (initialDepositEvent != null) ? initialDepositEvent.Fee : null;
            }
        }

        public ContractChartOfAccounts ChartOfAccounts
        {
            get
            {
                return _chartOfAccounts;
            }
            set
            {
                _chartOfAccounts = value;
            }
        }

        public OCurrency GetBalance()
        {
            return GetBalance(TimeProvider.Now);
        }

        public bool HasPendingEvents()
        {
            return Events.Any(savingEvent => savingEvent.IsPending);
        }

        public OCurrency GetBalance(DateTime date)
        {
            OCurrency retval = 0m;

            foreach (SavingEvent e in Events)
            {
                if (!(e is SavingPendingDepositEvent) && !(e is SavingPendingDepositRefusedEvent))
                {
                    retval += e.GetBalanceChunk(date);
                }
            }

            return retval;
        }

        public string GetFmtBalance(bool showCurrency)
        {
            return GetFmtBalance(TimeProvider.Now, showCurrency);
        }

        public string GetFmtBalance(DateTime date, bool showCurrency)
        {
            OCurrency balance = GetBalance(date);
            bool useCents = Product.Currency.UseCents;
            string fmtBalance = balance.GetFormatedValue(useCents);
            
            if (!showCurrency) 
                return fmtBalance;

            string currency = Product.Currency.Code;
            return string.Format("{0} {1}", fmtBalance, currency);
        }

        public SavingClosureEvent GenerateClosureEvent()
        {
            SavingClosureEvent scle = new SavingClosureEvent
            {
                Amount = 0m,
                Cancelable = false,
                Date = TimeProvider.Now,
                Description = "Closure event",
                Fee = 0m,
                User = _user,
                ProductType = Product.GetType()
            };
            return scle;
        }

        public OCurrency GetBalanceMin(DateTime pDate)
        {
            OCurrency amount = GetBalance(pDate);
            List<SavingEvent> diaryEvents = Events.Where(item => item.Date.Equals(pDate) && item.Deleted == false &&
                    (item.Code == OSavingEvents.Deposit || item.Code == OSavingEvents.Withdraw))
                    .Reverse().ToList();

            OCurrency minimalAmount = amount;

            foreach (SavingEvent savingEvent in diaryEvents)
            {
                if (savingEvent is SavingWithdrawEvent)
                    amount += savingEvent.Amount;
                else
                    amount -= savingEvent.Amount;

                if (amount < minimalAmount)
                    minimalAmount = amount;
            }

            return minimalAmount;
        }

        public virtual List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, OCurrency pEntryFees, User pUser)
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

        public virtual List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingWithdrawEvent withdrawEvent = new SavingWithdrawEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                ProductType = Product.GetType()
            };
            events.Add(withdrawEvent);
            _events.Add(withdrawEvent);

            return events;
        }

        public virtual List<SavingEvent> ChargeOverdraftFee(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
        {
            List<SavingEvent> events = new List<SavingEvent>();
            SavingOverdraftFeeEvent overdraftFeeEvent = new SavingOverdraftFeeEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Cancelable = true,
                ProductType = Product.GetType()
            };
            events.Add(overdraftFeeEvent);
            _events.Add(overdraftFeeEvent);

            return events;
        }

        public virtual List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser,
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

            return events;
        }

        public virtual SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user)
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

        public virtual SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user)
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

        public virtual List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees)
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
                ProductType = Product.GetType()
            };
            events.Add(transferEvent);
            _events.Add(transferEvent);

            return events;
        }

        public SavingCreditTransferEvent CreditTransfer(OCurrency pAmount, ISavingsContract pDebitAccount, DateTime pDate, string pDescription, User pUser)
        {
            SavingCreditTransferEvent transferEvent = new SavingCreditTransferEvent
            {
                Amount = pAmount,
                Date = pDate,
                Description = pDescription,
                User = pUser,
                Fee = 0,
                Cancelable = false,
                RelatedContractCode = pDebitAccount.Code,
                ProductType = Product.GetType()
            };
            _events.Add(transferEvent);

            return transferEvent;
        }

        public SavingEvent GetCancelableEvent()
        {
            if (0 == Events.Count) return null;

            SavingEvent evt = Events.OrderBy(item => item.Date).Last(item => !item.Deleted);

            return evt.Cancelable ? evt : null;
        }

        public bool HasCancelableEvents()
        {
            return GetCancelableEvent() != null;
        }

        public void CancelEvent(SavingEvent pSavingEvent)
        {
            pSavingEvent.Deleted = true;
        }

        public SavingEvent CancelLastEvent()
        {
            SavingEvent retval = GetCancelableEvent();
            if (null == retval) return null;

            CancelEvent(retval);
            return retval;
        }

        public virtual List<SavingEvent> Closure(DateTime date, User user)
        {
            SavingEvent evt = GenerateClosureEvent();
            _events.Add(evt);
            List<SavingEvent> retval = new List<SavingEvent> {evt};
            return retval;
        }

        public virtual List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

        public virtual List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, int? pendingEventId)
        {
            throw new NotImplementedException();
        }

        public virtual List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

        public virtual List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees)
        {
            throw new NotImplementedException();
        }

        protected OCurrency PostPayableAmount(DateTime pDate, User pUser)
        {
            OCurrency interestsToPost;

            if (this is Saving)
                interestsToPost = ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS,
                                    Product == null || Product.Currency == null ? 1 : Product.Currency.Id, this, OBookingDirections.Both).Balance;
            else
                interestsToPost = ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT,
                                   Product == null || Product.Currency == null ? 1 : Product.Currency.Id, this, OBookingDirections.Both).Balance;

            return interestsToPost > 0 ? interestsToPost : 0;
        }

        protected SavingInterestsPostingEvent _PostPayableInterests(DateTime pDate, User pUser)
        {
            OCurrency interestsToPost;

            if (this is Saving)
                interestsToPost = ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS,
                                    Product == null || Product.Currency == null ? 1 : Product.Currency.Id, this, OBookingDirections.Both).Balance;
            else
                interestsToPost = ChartOfAccounts.GetAccountByNumber(OAccounts.ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT,
                                   Product == null || Product.Currency == null ? 1 : Product.Currency.Id, this, OBookingDirections.Both).Balance;

            if (interestsToPost > 0)
            {
                SavingInterestsPostingEvent postingEvent = new SavingInterestsPostingEvent()
                {
                    Date = pDate,
                    Amount = interestsToPost,
                    User = pUser,
                    Cancelable = true,
                    Description = string.Format("Posting interests for period : {0:d} to {1:d}", GetLastPostingDate(), pDate),
                    ProductType = Product.GetType()
                };

                _events.Add(postingEvent);
                return postingEvent;
            }

            return null;
        }

        public DateTime GetLastPostingDate()
        {
            //SavingInterestsPostingEvent lastPosting = (SavingInterestsPostingEvent)_events.OrderByDescending(item => item.Date).FirstOrDefault(item => item is SavingInterestsPostingEvent && !item.Deleted);
            SavingEvent lastPosting = _events.OrderByDescending(item => item.Date).FirstOrDefault(item => (item is SavingInterestsPostingEvent || item is SavingClosureEvent) && !item.Deleted);
            return (lastPosting == null) ? CreationDate : lastPosting.Date;
        }

        public DateTime GetLastAccrualDate()
        {
            //SavingInterestsAccrualEvent lastClosure = (SavingInterestsAccrualEvent)_events.OrderByDescending(item => item.Date).FirstOrDefault(item => item is SavingInterestsAccrualEvent && !item.Deleted);
            SavingEvent lastClosure = _events.OrderByDescending(item => item.Date).FirstOrDefault(item => (item is SavingInterestsAccrualEvent || item is SavingClosureEvent) && !item.Deleted);
            return (lastClosure == null) ? CreationDate : lastClosure.Date;
        }

        public List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pDate, User pUser)
        {
            CalculateInterestsStrategy cis = new CalculateInterestsStrategy(this, pUser, _applicationSettings.WeekEndDay2);
            return cis.CalculateInterest(pDate);
        }

        public List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate, User pUser)
        {
            PostingInterestsStrategy pis = new PostingInterestsStrategy(this, pUser, _applicationSettings.WeekEndDay2);
            return pis.PostingInterests(pDate);
        }

        public string GenerateSavingCode(Client pClient, int pSavingsCount, string pCodeTemplate, string pImfCode, string pBranchCode)
        {
            switch (pCodeTemplate)
            {
                case "BC/YY/PC-PS/CN-ID":
                    {
                        string clientName = (pClient is Person) ? ((Person)pClient).LastName : pClient.Name;
                        clientName = clientName.Replace(" ", "");
                        string productCode = Product.Code.Replace(" ", "");
                        Code = "S/{0}/{1}/{2}-{3}/{4}-{5}";
                        Code = string.Format(Code,
                                             pBranchCode,
                                             CreationDate.Year,
                                             productCode.Substring(0, Math.Min(productCode.Length, 5)).ToUpper(),
                                             pSavingsCount + 1,
                                             clientName.Substring(0, Math.Min(clientName.Length, 4)).ToUpper(),
                                             pClient.Id);
                        break;
                    }
                case "IC/BC/CS/ID":
                    {
                        string clientCode = pClient.Id.ToString().PadLeft(5, '0');
                        string savingsCount = (pSavingsCount + 1).ToString().PadLeft(2, '0');
                        Code = string.Format("{0}/{1}/{2}/{3}", pImfCode, pBranchCode, savingsCount, clientCode);
                        int verificationCode = GenerateVerificationCode(Code);
                        Code += "/" + verificationCode.ToString().PadLeft(2, '0');
                        break;
                    }
            }
            return Code;
        }

        public static int GenerateVerificationCode(string savingsCode)
        {
            savingsCode = savingsCode.Replace("/", "");
            string newCode = string.Empty;

            for (int i = 0; i < savingsCode.Length; i++)
            {
                char car = savingsCode[i];
                if (char.IsLetter(car))
                {
                    newCode += ((int)char.ToUpper(car)).ToString();
                }
                else
                    newCode += car;
            }

            return (int)(Convert.ToDecimal(newCode) % 97);
        }

        protected ContractChartOfAccounts _FillChartOfAccounts(ChartOfAccounts pChartOfAccounts)
        {
            ContractChartOfAccounts contractChartOfAccounts;
            if (pChartOfAccounts.UniqueAccounts.Count > 0)
                contractChartOfAccounts = new ContractChartOfAccounts(pChartOfAccounts.UniqueAccounts.
                    Select(item => new Account(item.Number, item.LocalNumber, item.Label, 0, item.TypeCode, item.DebitPlus, item.AccountCategory, Product != null && Product.Currency != null ? Product.Currency.Id : 1)).ToList());
            else
                contractChartOfAccounts = new ContractChartOfAccounts(pChartOfAccounts.DefaultAccounts.ToList());

            contractChartOfAccounts.AccountingRuleCollection = pChartOfAccounts.AccountingRuleCollection;

            return contractChartOfAccounts;
        }

        protected SavingEvent EvaluateSavingsEvent(SavingEvent e)
        {
            if (e is SavingInterestsAccrualEvent || e is SavingInterestsPostingEvent)
            {
                if (e.Amount > 0)
                {
                    e.Amount = Product.Currency.UseCents
                                   ? Math.Round(e.Amount.Value, 2, MidpointRounding.AwayFromZero)
                                   : Math.Round(e.Amount.Value, 0, MidpointRounding.AwayFromZero);
                    if (e.Amount > 0)
                        return e;
                }
            }

            if(e is SavingAgioEvent || e is SavingManagementFeeEvent)
            {
                if (e.Fee > 0)
                {
                    e.Fee = Product.Currency.UseCents ? Math.Round(e.Fee.Value, 2, MidpointRounding.AwayFromZero) :
                        Math.Round(e.Fee.Value, 0, MidpointRounding.AwayFromZero);
                    if (e.Fee > 0) 
                        return e;
                }
            }

            //return filteredEvents;
            return null;
        }

        protected List<SavingEvent> _AddSavingEvent(List<SavingInterestsAccrualEvent> pEvents)
        {
            List<SavingEvent> retval = new List<SavingEvent>();
            List<SavingEvent> events = pEvents.ConvertAll<SavingEvent>(delegate(SavingInterestsAccrualEvent e) { return e; });
            
            foreach (SavingEvent e in events)
            {
                SavingEvent savEvent = EvaluateSavingsEvent(e);
                if (savEvent != null)
                {
                    _events.Add(savEvent);
                    retval.Add(savEvent);
                }
            }

            return retval;
        }

        protected List<SavingEvent> _AddSavingEvent(List<SavingInterestsPostingEvent> pEvents)
        {
            List<SavingEvent> retval = new List<SavingEvent>();
            List<SavingEvent> events = pEvents.ConvertAll<SavingEvent>(delegate(SavingInterestsPostingEvent e) { return e; });
            
            foreach (SavingEvent e in events)
            {
                SavingEvent savEvent = EvaluateSavingsEvent(e);
                if (savEvent != null)
                {
                    _events.Add(e);
                    retval.Add(savEvent);
                }
            }

            return retval;
        }

        public SavingEvent _AddSavingEvent(SavingEvent e)
        {
            SavingEvent savEvent = EvaluateSavingsEvent(e);
            if (savEvent != null)
            {
                _events.Add(e);
            }

            return savEvent;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
