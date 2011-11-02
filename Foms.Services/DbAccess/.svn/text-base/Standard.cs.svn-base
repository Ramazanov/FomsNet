using System;
using System.Collections.Generic;

using Octopus.Services.Accounting;
using Octopus.Services.Currencies;
using Octopus.Services.Events;
using Octopus.Services.Rules;
using Octopus.Shared;
using Octopus.Shared.Settings;
using Reports = Octopus.Enums.OReports;
using Octopus.CoreDomain;
using Octopus.Services.Accounting.Sage;
using Octopus.Services.Export;

namespace Octopus.Services
{
    public class Standard : IServices
    {
      
        
        public UserServices GetUserServices()
        {
            Console.WriteLine("UserServices coté client");
            return new UserServices(User.CurrentUser);
        }

        public AccountingServices GetAccountingServices()
        {
            Console.WriteLine("AccountingServices coté client");
            return new AccountingServices(User.CurrentUser);
        }
        public RoleServices GetRoleServices()
        {
            Console.WriteLine("RoleServices coté client");
            return new RoleServices(User.CurrentUser);
        }
        public RegExCheckerServices GetRegExCheckerServices()
        {
            Console.WriteLine("RegExCheckerServices coté client");
            return new RegExCheckerServices(User.CurrentUser);
        }
        public ExchangeRateServices GetExchangeRateServices()
        {
            Console.WriteLine("ExchangeRateServices coté client");
            return new ExchangeRateServices(User.CurrentUser);
        }

        //Permet d'initialiser le service "SavingProduct" en local
        public SavingProductServices GetSavingProductServices()
        {
            Console.WriteLine("SavingProductServices coté client");
            return new SavingProductServices(User.CurrentUser);
        }

        public SavingServices GetSavingServices()
        {
            Console.WriteLine("SavingServices coté client");
            return new SavingServices(User.CurrentUser);
        }

        public ChartOfAccountsServices GetChartOfAccountsServices()
        {
            Console.WriteLine("GlobalAccountingParametersServices coté client");
            return new ChartOfAccountsServices(User.CurrentUser);
        }

        public StandardBookingServices GetStandardBookingServices()
        {
            Console.WriteLine("StandardBookingServices coté client");
            return new StandardBookingServices(User.CurrentUser);
        }

        public EventProcessorServices GetEventProcessorServices()
        {
            Console.WriteLine("EventProcessorServices coté client");
            return new EventProcessorServices(User.CurrentUser);
        }

        //public CashReceiptServices GetCashReceiptServices()
        //{
        //    Console.WriteLine("CashReceiptServices coté client");
        //    return new CashReceiptServices(User.CurrentUser);
        //}

        public ClientServices GetClientServices()
        {
            Console.WriteLine("ClientServices coté client");
            return new ClientServices(User.CurrentUser);
        }

        public ConsolidationServices GetConsolidationServices()
        {
            Console.WriteLine("ConsolidationServices coté client");
            return new ConsolidationServices(User.CurrentUser);
        }

        public LoanServices GetContractServices()
        {
            Console.WriteLine("ContractServices coté client");
            return new LoanServices(User.CurrentUser);
        }

        public GuaranteeServices GetGuaranteeServices()
        {
            Console.WriteLine("GuaranteeServices coté client");
            return new GuaranteeServices(User.CurrentUser);
        }

        public DatabaseServices GetDatabaseServices()
        {
            Console.WriteLine("DatabaseServices coté client");
            return new DatabaseServices(); 
        }

        public EconomicActivityServices GetDomainOfApplicationServices()
        {
            Console.WriteLine("DomainOfApplicationServices coté client");
            return new EconomicActivityServices(User.CurrentUser);
        }

        public ApplicationSettingsServices GetApplicationSettingsServices()
        {
            Console.WriteLine("GeneralSettingsServices coté client");
            return new ApplicationSettingsServices(User.CurrentUser);
        }

        public GraphServices GetGraphServices()
        {
            Console.WriteLine("GraphServices coté client");
            return new GraphServices(User.CurrentUser);
        }

        public LocationServices GetLocationServices()
        {
            return new LocationServices(User.CurrentUser);
        }

        public PicturesServices GetPicturesServices()
        {
            return new PicturesServices(User.CurrentUser);
        }

        public ProductServices GetProductServices()
        {
            return new  ProductServices(User.CurrentUser);
        }

        public CollateralProductServices GetCollateralProductServices()
        {
            return new CollateralProductServices(User.CurrentUser);
        }

        public GuaranteeProductService GetGuaranteeProductServices()
        {
            return new GuaranteeProductService(User.CurrentUser);
        }

          public ReportsServices GetReportsServices()
        {
            return new ReportsServices(User.CurrentUser);
        }

          public MFIServices GetMFIServices()
          {
              return new MFIServices(User.CurrentUser);
          }

        public SettingsImportExportServices GetSettingsImportExportServices()
        {
            return new SettingsImportExportServices(User.CurrentUser);
        }

        public ProjectServices GetProjectServices()
        {
            return new ProjectServices(User.CurrentUser);
        }

        public string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName)
        {
            throw new NotImplementedException();
        }

        public void RunTimeout()
        {
            throw new NotImplementedException();
        }

        public string GetToken()
        {
            return "";
        }

     
        public ApplicationSettings GetGeneralSettings()
        {
            return ApplicationSettings.GetInstance("");
        }

        public FundingLineServices GetFundingLinesServices()
        {
            return new FundingLineServices(User.CurrentUser);
        }

        public SQLToolServices GetSQLToolServices()
        {
            return new SQLToolServices(User.CurrentUser);
        }

        public CurrencyServices GetCurrencyServices()
        {
            return new CurrencyServices(User.CurrentUser);
        }

        public QuestionnaireServices GetQuestionnaireServices()
        {
            return new QuestionnaireServices(User.CurrentUser);
        }

        public AccountingRuleServices GetAccountingRuleServices()
        {
            return new AccountingRuleServices(User.CurrentUser);
        }

        public SageServices GetSageServices()
        {
            return new SageServices(User.CurrentUser);
        }

        public ExportServices GetExportServices()
        {
            return new ExportServices(User.CurrentUser);
        }

        public BranchService GetBranchService()
        {
            return new BranchService(User.CurrentUser);
        }

        #region IServices Members

        public NonWorkingDateSingleton GetNonWorkingDate()
        {
            return NonWorkingDateSingleton.GetInstance(User.CurrentUser.Md5);
        }

        #endregion

        #region IServices Members


        public void SuppressAllRemotingInfos(string pComputerName, string pLoginName)
        {
        }

        #endregion

        #region IServices Members

       #endregion
    }
}
