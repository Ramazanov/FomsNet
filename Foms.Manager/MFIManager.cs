using System.Data.SqlClient;
using Foms.CoreDomain;

namespace Foms.Manager
{
    public class MFIManager : Manager
    {
        private User _user;
        public MFIManager(User pUser)
            : base(pUser)
        {
            _user = pUser;
        }

        public MFIManager(string testDB) : base(testDB) { }

        public MFIManager(string testDB, User pUser)
            : base(testDB)
        {
            _user = pUser;
        }

        public MFI SelectMFI()
        {
            MFI mfi = new MFI();

            string sqlText = "SELECT * FROM [MFI]";
            SqlCommand select = new SqlCommand(sqlText, DefaultConnection);

            using (SqlDataReader reader = select.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    mfi = new MFI();
                    mfi.Name = DatabaseHelper.GetString("name", reader);
                    mfi.Login = DatabaseHelper.GetString("login", reader);
                    mfi.Password = DatabaseHelper.GetString("password", reader);
                }
            }
            return mfi;
        }

        public bool UpdateMFI(MFI pMFI)
        {
            if (SelectMFI().Login != null)
            {
                string sqlText = @"UPDATE [MFI] SET [name]=@name, [login]=@login, [password]=@password";

                using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
                {
                    DatabaseHelper.InsertStringVarCharParam("@name", sqlCommand, pMFI.Name);
                    DatabaseHelper.InsertStringVarCharParam("@login", sqlCommand, pMFI.Login);
                    DatabaseHelper.InsertStringVarCharParam("@password", sqlCommand, pMFI.Password);
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }

            return false;
        }

        public bool CreateMFI(MFI pMFI)
        {
            if (SelectMFI().Login == null)
            {
                string sqlText = "INSERT INTO [MFI] ([name], [login], [password]) VALUES(@name,@login,@password)";

                SqlCommand insert = new SqlCommand(sqlText, DefaultConnection);

                DatabaseHelper.InsertStringVarCharParam("@name", insert, pMFI.Name);
                DatabaseHelper.InsertStringVarCharParam("@login", insert, pMFI.Login);
                DatabaseHelper.InsertStringVarCharParam("@password", insert, pMFI.Password);

                insert.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public void DeleteMFI()
        {
            if (SelectMFI()!=null)
            {
                string sqlText = "DELETE FROM [MFI]";
                SqlCommand delete = new SqlCommand(sqlText, DefaultConnection);

                delete.ExecuteNonQuery();
            }
        }
    }
}
