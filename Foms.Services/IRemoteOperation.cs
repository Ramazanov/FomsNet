using Foms.CoreDomain;
using Foms.Shared;
using Foms.Shared.Settings;

namespace Foms.Services
{
    public interface IRemoteOperation
    {
 
        DatabaseServices GetDatabaseServices(User pUser);
 
        NonWorkingDateSingleton GetNonWorkingDate(User pUser);
        ApplicationSettings GetGeneralSettings(User pUser);
     
        
        bool TestRemoting();

        void SuppressAllRemotingInfos(User pUser, string pComputerName, string pLoginName);

        string GetAuthentification(string login, string pass, string account, string pComputerName, string pLoginName);
        void RunTimeout();
    }
}
