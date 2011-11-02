using System.Data.SqlClient;
using Foms.CoreDomain.Online;

namespace Foms.DatabaseConnection
{
    public class UserRemotingContext
    {
        private Token _token = null;
        private SqlConnection _connection = null;
        private SqlConnection _secondaryConnection = null;
      
        public Token Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public SqlConnection SecondaryConnection
        {
            get { return _secondaryConnection; }
            set { _secondaryConnection = value; }
        }

    }
}
