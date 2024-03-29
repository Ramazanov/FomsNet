//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright � 2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System.Collections.Generic;
using System.Data.SqlClient;
using Foms.CoreDomain;

namespace Foms.Manager
{
    public class UserManager : Manager
    {
        //private readonly User _user = new User();

        public UserManager(User pUser) : base(pUser) 
        {
            //_user = pUser; 
        }

        public UserManager(string testDb) : base(testDb) { }

        public UserManager(string testDb,User pUser) : base(testDb)
        {
            //_user = pUser;
        }

        public int AddUser(User pUser)
        {
            const string sqlText = @"INSERT INTO [Users] (
                                       [deleted], 
                                       [role_code], 
                                       [user_name], 
                                       [user_pass], 
                                       [first_name], 
                                       [last_name], 
                                       [mail]) 
                                     VALUES(
                                       @deleted, 
                                       @roleCode, 
                                       @username, 
                                       @userpass, 
                                       @firstname,
                                       @lastname, 
                                       @mail) 
                                     SELECT SCOPE_IDENTITY()";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
            {
                DatabaseHelper.InsertBooleanParam("@deleted", sqlCommand, false);
                _SetUser(sqlCommand, pUser);
                pUser.Id = int.Parse(sqlCommand.ExecuteScalar().ToString());
                _SaveUsersRole(pUser.Id, pUser.UserRole.Id);
            }
            return pUser.Id;
        }

        public void UpdateUser(User pUser)
        {
            const string sqlText = @"UPDATE [Users] 
                                     SET [user_name] = @username, 
                                       [user_pass] = @userpass, 
                                       [role_code] = @roleCode, 
                                       [first_name] = @firstname, 
                                       [last_name] = @lastname, 
                                       [mail] = @mail
                                     WHERE [id] = @userId";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
            {
                DatabaseHelper.InsertInt32Param("@userId", sqlCommand, pUser.Id);
                _SetUser(sqlCommand, pUser);
                sqlCommand.ExecuteNonQuery();
                _UpdateUsersRole(pUser.Id, pUser.UserRole.Id);
            }
        }

        private void _SaveUsersRole(int pUserId, int pRoleId)
        {
            const string sqlText = @"INSERT INTO [UserRole]([role_id], [user_id]) 
                                   VALUES(@role_id, @user_id)";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
            {
                DatabaseHelper.InsertInt32Param("@role_id", sqlCommand, pRoleId);
                DatabaseHelper.InsertInt32Param("@user_id", sqlCommand, pUserId);
                sqlCommand.ExecuteScalar();
            }
        }

        private void _UpdateUsersRole(int pUserId, int pRoleId)
        {
            const string sqlText = @"UPDATE [UserRole] 
                                    SET [role_id] = @role_id
                                    WHERE [user_id] = @user_id";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
            {
                DatabaseHelper.InsertInt32Param("@role_id", sqlCommand, pRoleId);
                DatabaseHelper.InsertInt32Param("@user_id", sqlCommand, pUserId);
                sqlCommand.ExecuteScalar();
            }
        }

        private static User _GetUser(SqlDataReader pReader)
        {
            User user = new User
                            {
                                Id = DatabaseHelper.GetInt32("user_id", pReader),
                                UserName = DatabaseHelper.GetString("user_name", pReader),
                                FirstName = DatabaseHelper.GetString("first_name", pReader),
                                LastName = DatabaseHelper.GetString("last_name", pReader),
                                Mail = DatabaseHelper.GetString("mail", pReader),
                                IsDeleted = DatabaseHelper.GetBoolean("deleted", pReader),
                                HasContract = (DatabaseHelper.GetInt32("contract_count", pReader) == 0 ? false : true),
                            };
            user.SetRole(DatabaseHelper.GetString("role_code", pReader));
            
            user.UserRole = new Role
                            {
                                RoleName = DatabaseHelper.GetString("role_name", pReader),
                                Id = DatabaseHelper.GetInt32("role_id", pReader)
                            };      

            return user;
        }

        private static void _SetUser(SqlCommand sqlCommand, User pUser)
        {
            DatabaseHelper.InsertStringNVarCharParam("@username", sqlCommand, pUser.UserName);
            DatabaseHelper.InsertStringNVarCharParam("@userpass", sqlCommand, pUser.Password);
            DatabaseHelper.InsertStringVarCharParam("@roleCode", sqlCommand, pUser.UserRole.ToString());
            DatabaseHelper.InsertStringNVarCharParam("@firstname", sqlCommand, pUser.FirstName);
            DatabaseHelper.InsertStringNVarCharParam("@lastname", sqlCommand, pUser.LastName);
            DatabaseHelper.InsertStringNVarCharParam("@mail", sqlCommand, pUser.Mail);
        }

        public void DeleteUser(User pUser)
        {
            const string sqlText = "UPDATE [Users] SET deleted = 1 WHERE [id] = @userId";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection))
            {
                DatabaseHelper.InsertInt32Param("@userId", sqlCommand, pUser.Id);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public User SelectUser(int pUserId,bool pIncludeDeletedUser, SqlTransaction pSqlTransac)
        {
            const string selectUser =     @"SELECT [Users].[id] as user_id, 
                                                   [user_name], 
                                                   [user_pass], 
                                                   [role_code], 
                                                   [first_name], 
                                                   [last_name], 
                                                   [mail],
                                                   [Users].[deleted], 
                                                   [Roles].[id] as role_id, 
                                                   [Roles].[code] AS role_name,
                                                   [Roles].[role_of_loan],
                                                   [Roles].[role_of_saving],
                                                   (SELECT COUNT(a.id) 
                                                   FROM  (SELECT Credit.id, loanofficer_id 
                                                          FROM Credit 
                                                          GROUP BY  Credit.id, loanofficer_id ) a 
                                                   WHERE a.loanofficer_id = Users.id) AS contract_count 
                                            FROM [Users] INNER JOIN UserRole on UserRole.user_id = Users.id
                                            INNER JOIN Roles ON Roles.id = UserRole.role_id
                                            WHERE 1 = 1 ";

            string sqlText = selectUser + @" AND [Users].[id] = @id ";
                             
            if (!pIncludeDeletedUser)
                sqlText += @" AND [Users].[deleted] = 0";

            sqlText += @" GROUP BY [Users].[id],
                                   [Users].[deleted],
                                   [user_name],
                                   [user_pass],
                                   [role_code],
                                   [first_name],
                                   [last_name],
                                   [mail],                                   
                                   [Roles].id, 
                                   [Roles].code,
                                   [Roles].[role_of_loan],
                                   [Roles].[role_of_saving]";

            using (SqlCommand sqlCommand = new SqlCommand(sqlText, DefaultConnection, pSqlTransac))
            {
                DatabaseHelper.InsertInt32Param("@id", sqlCommand, pUserId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return _GetUser(reader);
                        }
                    }
                }
                return null;
            }
        }

        public User SelectUser(int pUserId,bool pIncludeDeletedUser)
        {
            return SelectUser(pUserId, pIncludeDeletedUser, null);
        }

        public List<User> SelectAll()
        {
            const string q = @"SELECT 
                                 id, 
                                 deleted, 
                                 user_name, 
                                 first_name,
                                 last_name, 
                                 user_pass,
                                 mail
                             FROM dbo.Users AS u";

            List<User> users = new List<User>();
            OctopusCommand c = new OctopusCommand(q, DefaultConnection);
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return users;

                while (r.Read())
                {
                    User u = new User
                    {
                        Id = r.GetInt("id"),
                        FirstName = r.GetString("first_name"),
                        LastName = r.GetString("last_name"),
                        IsDeleted = r.GetBool("deleted"),
                        UserName = r.GetString("user_name"),
                        Password = r.GetString("user_pass"),
                        Mail = r.GetString("mail")

                    };
                    users.Add(u);
                }
            }
            return users;
        }

        public Dictionary<int, List<int>> SelectSubordinateRel()
        {
            const string q = @"SELECT user_id, subordinate_id
                               FROM dbo.UsersSubordinates
                               ORDER BY user_id";

            Dictionary<int, List<int>> retval = new Dictionary<int, List<int>>();
            OctopusCommand c = new OctopusCommand(q, DefaultConnection);
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return retval;

                int currentId = 0;
                while (r.Read())
                {
                    int userId = r.GetInt("user_id");
                    if (currentId != userId)
                    {
                        currentId = userId;
                        retval.Add(currentId, new List<int>());
                    }
                    retval[currentId].Add(r.GetInt("subordinate_id"));
                }
            }
            return retval;
        }

        public Dictionary<int, List<int>> SelectBranchRel()
        {
            const string q = @"SELECT user_id, branch_id
            FROM dbo.UsersBranches
            ORDER BY user_id";
            Dictionary<int, List<int>> retval = new Dictionary<int, List<int>>();
            OctopusCommand c = new OctopusCommand(q, DefaultConnection);
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return retval;

                while (r.Read())
                {
                    int userId = r.GetInt("user_id");
                    if (!retval.ContainsKey(userId)) retval.Add(userId, new List<int>());
                    retval[userId].Add(r.GetInt("branch_id"));
                }
            }
            return retval;
        }

        private void SaveSubordinates(User user)
        {
            OctopusCommand c = new OctopusCommand();
            c.CommandText = @"DELETE FROM dbo.UsersSubordinates
                              WHERE user_id = @id";
            c.Connection = DefaultConnection;
            c.AddParam("id", user.Id);
            c.ExecuteNonQuery();

            if (0 == user.SubordinateCount) return;

            List<string> subIds = new List<string>();
            foreach (User sub in user.Subordinates)
            {
                subIds.Add(sub.Id.ToString());
            }

            c.CommandText = @"INSERT INTO dbo.UsersSubordinates
                             (user_id, subordinate_id)
                             SELECT @id, number 
                             FROM dbo.IntListToTable(@list)";
            c.AddParam("@list", string.Join(",", subIds.ToArray()));
            c.ExecuteNonQuery();
        }

        private void SaveBranches(User user)
        {
            OctopusCommand c = new OctopusCommand();
            c.CommandText = @"DELETE FROM dbo.UsersBranches
            WHERE user_id = @id";
            c.Connection = DefaultConnection;
            c.AddParam("@id", user.Id);
            c.ExecuteNonQuery();

            if (0 == user.BranchCount) return;

            List<string> ids = new List<string>();
            foreach (Branch b in user.Branches)
            {
                ids.Add(b.Id.ToString());
            }
            c.CommandText = @"INSERT INTO dbo.UsersBranches
            (user_id, branch_id)
            SELECT @id, number
            FROM dbo.IntListToTable(@list)";
            c.AddParam("@list", string.Join(",", ids.ToArray()));
            c.ExecuteNonQuery();
        }

        public void Save(User user)
        {
            SaveSubordinates(user);
            SaveBranches(user);
        }
    }
}