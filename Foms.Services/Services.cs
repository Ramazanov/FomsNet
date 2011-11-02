using System;
using System.Data.SqlClient;
using System.Security.Permissions;
using Foms.CoreDomain;
using Foms.DatabaseConnection;
using Foms.Shared.Settings;

namespace Foms.Services
{
    
    public class Services : ContextBoundObject
    {
        private readonly ConnectionManager _connectionManager = null;
        private readonly User _user;
        
        protected Services(User pUser)
        {
            _user = pUser;
            _connectionManager = ConnectionManager.GetInstance();
        }

        protected Services()
        {
            _user = null;
            _connectionManager = null;
        }


        protected SqlConnection DefaultConnection
        {
            get
            {
                return _connectionManager == null
                           ? null
                           : _connectionManager.SqlConnection;

            }
        }

 



 
    }
}
