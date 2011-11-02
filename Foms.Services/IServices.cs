using Foms.Shared;
using Foms.Shared.Settings;

namespace Foms.Services
{
    public interface IServices
    {
        UserServices GetUserServices();
        RoleServices GetRoleServices();
        DatabaseServices GetDatabaseServices();
        NonWorkingDateSingleton GetNonWorkingDate();
        ApplicationSettings GetGeneralSettings();
        SQLToolServices GetSQLToolServices();

        void SuppressAllRemotingInfos(string pComputerName, string pLoginName);

        string GetAuthentification(string pOctoUsername, string pOctoPass, string pDbName, string pComputerName, string pLoginName);
        void RunTimeout();
        string GetToken();
    }
}
