using System;
using Foms.Shared;
using Foms.Shared.Settings;
using Foms.CoreDomain;

namespace Foms.Services
{
    public class Standard : IServices
    {

        public UserServices GetUserServices()
        {
            Console.WriteLine("UserServices cot� client");
            return new UserServices(User.CurrentUser);
        }

        public RoleServices GetRoleServices()
        {
            Console.WriteLine("RoleServices cot� client");
            return new RoleServices(User.CurrentUser);
        }

        public DatabaseServices GetDatabaseServices()
        {
            Console.WriteLine("DatabaseServices cot� client");
            return new DatabaseServices(); 
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


        public SQLToolServices GetSQLToolServices()
        {
            return new SQLToolServices(User.CurrentUser);
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
