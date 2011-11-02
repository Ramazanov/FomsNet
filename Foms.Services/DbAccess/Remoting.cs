using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters;
using Octopus.CoreDomain;
using Octopus.Services.Accounting;
using Octopus.Services.Events;
using Octopus.Services.Rules;
using Octopus.Shared;
using Octopus.Shared.Settings;
using Octopus.Services.Currencies;
using Octopus.Services.Accounting.Sage;
using Octopus.Services.Export;

namespace Octopus.Services
{
    public class Remoting : IServices
    {
        private static bool _exist = false;
        private string _md5 = "";
        private string _userName;
        private string _pass;
        private string _account;
        static private IRemoteOperation _remoteOperation;
        private static Remoting _remoting_unique_instance = null;

        #region Accessors
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        public string DbName
        {
            get { return _account; }
            set { _account = value; }
        }
        #endregion

        private Remoting()
        {
            RemotingConnection();
        }

        public static Remoting GetInstance()
        {
            if (_remoting_unique_instance == null)
                return _remoting_unique_instance = new Remoting();
            else
                return _remoting_unique_instance;
        }

        // Initialise the remoting connection with http channel with binary formatter
        public bool RemotingConnection()
        {
            if (!_exist)
            {
                
                HttpChannel channel = null;
                try
                {
                    IDictionary props = new Hashtable();
                    props["port"] = TechnicalSettings.RemotingServerPort;
                    props.Add("typeFilterLevel", TypeFilterLevel.Full);
                    //props.Add("timeout", 2000);
                    BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                    channel = new HttpChannel(props, clientProvider, null);
                    ChannelServices.RegisterChannel(channel, false);

                    //TcpChannel channel = new TcpChannel();
                    //ChannelServices.RegisterChannel(channel, true);
                    var server = string.Format("http://{0}:{1}/RemoteOperation", TechnicalSettings.RemotingServer,
                                               TechnicalSettings.RemotingServerPort);

                    _remoteOperation = (IRemoteOperation) Activator.GetObject(typeof (IRemoteOperation), server);
                    _exist = _remoteOperation.TestRemoting();

                    //ChannelServices.GetChannelSinkProperties(_remoteOperation)["timeout"] = 0;
                }
                catch(Exception e)
                {
                    _remoting_unique_instance = null;
                    ChannelServices.UnregisterChannel(channel);
                    throw;
                }
            }
            return true;
        }

        private void TimeoutHandler()
        {
            // Ask a new token
            _md5 = _remoteOperation.GetAuthentification(_userName, _pass, _account, "TIMEOUT", "TIMEOUT");
        }
        
        public UserServices GetUserServices()
        {
            if (_remoteOperation != null)
            {
                try
                {
                    return _remoteOperation.GetUserServices(User.CurrentUser);
                }
                catch
                {
                    TimeoutHandler();
                  return _remoteOperation.GetUserServices(User.CurrentUser);
                }
            }
            return null;
        }

        public AccountingServices GetAccountingServices()
        {
            return _remoteOperation.GetAccountingServices(User.CurrentUser);
        }

        public ExchangeRateServices GetExchangeRateServices()
        {
            return _remoteOperation.GetExchangeRateServices(User.CurrentUser);
        }

        public ChartOfAccountsServices GetChartOfAccountsServices()
        {
            return _remoteOperation.GetChartOfAccountsServices(User.CurrentUser);
        }

        public EventProcessorServices GetEventProcessorServices()
        {
            return _remoteOperation.GetEventProcessorServices(User.CurrentUser);
        }

        public StandardBookingServices GetStandardBookingServices()
        {
            return _remoteOperation.GetStandardBookingServices(User.CurrentUser);
        }

        public SavingProductServices GetSavingProductServices()
        {
            //remoteOperation permet de savoir ou se trouve le serveur
            return _remoteOperation.GetSavingProductServices(User.CurrentUser);
        }
        public RegExCheckerServices GetRegExCheckerServices()
        {
            return new RegExCheckerServices(User.CurrentUser);
        }
        public SavingServices GetSavingServices()
        {
            return _remoteOperation.GetSavingServices(User.CurrentUser);
        }

        //public CashReceiptServices GetCashReceiptServices()
        //{
        //    return remoteOperation.GetCashReceiptServices(User.CurrentUser);
        //}

        public ClientServices GetClientServices()
        {
            return _remoteOperation.GetClientServices(User.CurrentUser);
        }

        public ConsolidationServices GetConsolidationServices()
        {
            return _remoteOperation.GetConsolidationServices(User.CurrentUser);
        }

        public LoanServices GetContractServices()
        {
            return _remoteOperation.GetContractServices(User.CurrentUser);
        }
        public GuaranteeServices GetGuaranteeServices()
        {
            return _remoteOperation.GetGuaranteeServices(User.CurrentUser);
        }

        public DatabaseServices GetDatabaseServices()
        {
            return _remoteOperation.GetDatabaseServices(User.CurrentUser);
        }

        public EconomicActivityServices GetDomainOfApplicationServices()
        {
            return _remoteOperation.GetDomainOfApplicationServices(User.CurrentUser);
        }

        public ApplicationSettingsServices GetApplicationSettingsServices()
        {
            return _remoteOperation.GetApplicationSettingsServices(User.CurrentUser);
        }

        public GraphServices GetGraphServices()
        {
            return _remoteOperation.GetGraphServices(User.CurrentUser);
        }

        public LocationServices GetLocationServices()
        {
            return _remoteOperation.GetLocationServices(User.CurrentUser);
        }

        public PicturesServices GetPicturesServices()
        {
            return _remoteOperation.GetPicturesServices(User.CurrentUser);
        }

        public ProductServices GetProductServices()
        {
            return _remoteOperation.GetProductServices(User.CurrentUser);
        }

        public CollateralProductServices GetCollateralProductServices()
        {
            // TO DO: This could be later implemented for remoting
                       
            //return _remoteOperation.GetCollateralProductServices(User.CurrentUser);
            return null;
        }

        public GuaranteeProductService GetGuaranteeProductServices()
        {
            return _remoteOperation.GetGuaranteeProductService(User.CurrentUser);
        }

        public ReportsServices GetReportsServices()
        {
            return _remoteOperation.GetReportsServices(User.CurrentUser);
        }

        public SettingsImportExportServices GetSettingsImportExportServices()
        {
            return _remoteOperation.GetSettingsImportExportServices(User.CurrentUser);
        }

        public ProjectServices GetProjectServices()
        {
            return _remoteOperation.GetProjectServices(User.CurrentUser);
        }

        public MFIServices GetMFIServices()
        {
            return _remoteOperation.GetMFIServices(User.CurrentUser);
        }

        public ApplicationSettings GetGeneralSettings()
        {
            return _remoteOperation.GetGeneralSettings(User.CurrentUser);
        }

        public FundingLineServices GetFundingLinesServices()
        {
            return _remoteOperation.GetFundingLinesServices(User.CurrentUser);
        }

        public string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName)
        {
            _md5 = _remoteOperation.GetAuthentification(pOctoUsername, pOctoPass, pDbName, pComputerName, pLoginName);
            return _md5;
        }
        public string GetToken()
        {
            return _md5;
        }

        public void RunTimeout()
        {
            if (_remoteOperation != null)
                _remoteOperation.RunTimeout();
        }


        public NonWorkingDateSingleton GetNonWorkingDate()
        {
            return _remoteOperation.GetNonWorkingDate(User.CurrentUser);
        }

        public SQLToolServices GetSQLToolServices()
        {
            return _remoteOperation.GetSQLToolServices(User.CurrentUser);
        }

        public QuestionnaireServices GetQuestionnaireServices()
        {
            return _remoteOperation.GetQuestionnaireServices(User.CurrentUser);
        }

        public CurrencyServices GetCurrencyServices()
        {
            return _remoteOperation.GetCurrencyServices(User.CurrentUser);
        }

        public AccountingRuleServices GetAccountingRuleServices()
        {
            return _remoteOperation.GetAccountingRuleServices(User.CurrentUser);
        }

        public SageServices GetSageServices()
        {
            return _remoteOperation.GetSageServices(User.CurrentUser);
        }
        public RoleServices GetRoleServices()
        {
            return _remoteOperation.GetRoleServices(User.CurrentUser);
        }

        public ExportServices GetExportServices()
        {
            return _remoteOperation.GetExportServices(User.CurrentUser);
        }

        public BranchService GetBranchService()
        {
            return _remoteOperation.GetBranchService(User.CurrentUser);
        }

        #region IServices Members


        public void SuppressAllRemotingInfos(string pComputerName, string pLoginName)
        {
            _remoteOperation.SuppressAllRemotingInfos(User.CurrentUser, pComputerName, pLoginName);
        }

        #endregion

        #region IServices Members

       #endregion
    }
}
