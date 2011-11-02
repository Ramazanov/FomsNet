using Octopus.Services.Accounting;
using Octopus.Services.Currencies;
using Octopus.Services.Events;
using Octopus.Services.Rules;
using Octopus.Shared;
using Octopus.Shared.Settings;
using Octopus.Services.Accounting.Sage;
using Octopus.Services.Export;

namespace Octopus.Services
{
    public interface IServices
    {
        UserServices GetUserServices();
        AccountingServices GetAccountingServices();
        ExchangeRateServices GetExchangeRateServices();
        ChartOfAccountsServices GetChartOfAccountsServices();
        EventProcessorServices GetEventProcessorServices();
        ClientServices GetClientServices();
        ConsolidationServices GetConsolidationServices();
        LoanServices GetContractServices();
        GuaranteeServices GetGuaranteeServices();
        DatabaseServices GetDatabaseServices();
        EconomicActivityServices GetDomainOfApplicationServices();
        ApplicationSettingsServices GetApplicationSettingsServices();
        GraphServices GetGraphServices();
        LocationServices GetLocationServices();
        PicturesServices GetPicturesServices();
        ProductServices GetProductServices();
        CollateralProductServices GetCollateralProductServices();
        GuaranteeProductService GetGuaranteeProductServices();
        ReportsServices GetReportsServices();
        SettingsImportExportServices GetSettingsImportExportServices();
        ProjectServices GetProjectServices();
        NonWorkingDateSingleton GetNonWorkingDate();
        ApplicationSettings GetGeneralSettings();
        FundingLineServices GetFundingLinesServices();
        MFIServices GetMFIServices();
        SQLToolServices GetSQLToolServices();
        QuestionnaireServices GetQuestionnaireServices();
        SavingProductServices GetSavingProductServices();
        SavingServices GetSavingServices();
        StandardBookingServices GetStandardBookingServices();
        CurrencyServices GetCurrencyServices();
        RegExCheckerServices GetRegExCheckerServices();
        AccountingRuleServices GetAccountingRuleServices();
        SageServices GetSageServices();
        RoleServices GetRoleServices();
        ExportServices GetExportServices();
        BranchService GetBranchService();

        void SuppressAllRemotingInfos(string pComputerName, string pLoginName);

        string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName);
        void RunTimeout();
        string GetToken();
    }
}
