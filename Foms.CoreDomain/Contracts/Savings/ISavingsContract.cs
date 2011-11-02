using System;
using System.Collections.Generic;
using Foms.CoreDomain.Accounting;
using Foms.CoreDomain.Clients;
using Foms.CoreDomain.Events.Saving;
using Foms.CoreDomain.Products;

namespace Foms.CoreDomain.Contracts.Savings
{
    public interface ISavingsContract : ICloneable
    {
        int Id { get; set; }
        string Code { get; set; }
        ISavingProduct Product { get; set; }
        DateTime CreationDate { get; set; }
        DateTime? ClosedDate { get; set; }
        double InterestRate { get; set; }
        OSavingsStatus Status { get; set; }
        User User { get; }
        List<SavingEvent> Events { get; }
        OCurrency InitialAmount { get; }
        OCurrency EntryFees { get; }
        ContractChartOfAccounts ChartOfAccounts { get; set; }
        IClient Client { get; set; }
        User SavingsOfficer { get; set; }

        OCurrency GetBalance();
        OCurrency GetBalance(DateTime date);
        string GetFmtBalance(bool showCurrency);
        string GetFmtBalance(DateTime date, bool showCurrency);

        bool HasPendingEvents();
        bool HasCancelableEvents();
        SavingEvent GetCancelableEvent();
        SavingEvent CancelLastEvent();
        void CancelEvent(SavingEvent pSavingEvent);

        List<SavingEvent> FirstDeposit(OCurrency pInitialAmount, DateTime pCreationDate, OCurrency pEntryFees, User pUser);

        OCurrency GetBalanceMin(DateTime pDate);
        List<SavingEvent> Withdraw(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees);
        List<SavingEvent> Deposit(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees, bool isPending, OSavingsMethods savingsMethod, int? pendingEventId);
        SavingCreditOperationEvent SpecialOperationCredit(OCurrency amount, DateTime date, string description, User user);
        SavingDebitOperationEvent SpecialOperationDebit(OCurrency amount, DateTime date, string description, User user);
        
        List<SavingEvent> DebitTransfer(OCurrency pAmount, ISavingsContract pCreditAccount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees);
        SavingCreditTransferEvent CreditTransfer(OCurrency pAmount, ISavingsContract pDebitAccount, DateTime pDate, string pDescription, User pUser);
        List<SavingEvent> ChargeOverdraftFee(OCurrency pAmount, DateTime pDate, string pDescription, User pUser, bool pIsDesactivateFees);

        List<SavingEvent> Closure(DateTime pDate, User user);
        List<SavingEvent> SimulateClose(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees);
        List<SavingEvent> Close(DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees);
        List<SavingEvent> Reopen(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, bool pIsDesactivateFees);
        List<SavingEvent> RefusePendingDeposit(OCurrency pAmount, DateTime pDate, User pUser, string pDescription, OSavingsMethods method, int? pendingEventId);
        
        DateTime GetLastPostingDate();
        DateTime GetLastAccrualDate();
        List<SavingInterestsAccrualEvent> CalculateInterest(DateTime pDate, User pUser);
        List<SavingInterestsPostingEvent> PostingInterests(DateTime pDate, User pUser);
        string GenerateSavingCode(Client pClient, int pSavingsCount, string pCodeTemplate, string pImfCode, string pBranchCode);
    }
}