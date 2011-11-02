//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Foms.CoreDomain.Database;
using Foms.DatabaseConnection;
using Foms.Manager.Database;
using Foms.Shared;

namespace Foms.Services
{
    public class DatabaseServices 
    {
        private const string UPDATEDATABASE = "Database_Update_{0}_to_{1}.sql";
        private const string CREATEDATABASE = "CreateDatabase_{0}.sql";
        private const string INITIAL_DATAS = "InitialData.sql";
        private const string INITIAL_ACCOUNTING_RULES = "AccountingRules.sql";
        private const string UPGRADE_SCHEMA_FILE_NAME = "OCTOPUS_Upgrade_Schema.xml";


        public bool CheckSQLServerConnection()
        {
            return ConnectionManager.CheckSQLServerConnection();
        }

        public bool CheckSQLDatabaseConnection()
        {
            return ConnectionManager.CheckSQLDatabaseConnection();
        }

        public bool CheckSQLDatabaseVersion(string pExpectedVersion, string pDatabase)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();

                string actualVersion = ""; // DatabaseManager.GetDatabaseVersion(pDatabase, connection);

                connection.Close();
                return pExpectedVersion == actualVersion ? true : false;
                           
            }
            catch(Exception)
            {
                connection.Close();
                throw;
            }
        }



        public delegate void ExecuteUpgradeSqlDatabaseFile(string pCurrentDatabase, string pExpectedDatabase);
        public event ExecuteUpgradeSqlDatabaseFile UpdateDatabaseEvent;

        private static void _LoadUserDefinedFunctions(string database, SqlConnection conn)
        {
            throw new Exception("функция не объявлена");
            string src = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            src = Path.Combine(src, "Update");
            src = Path.Combine(src, "DropAndCreateUDF.sql");
            string dest = Path.GetTempFileName();
            StreamReader sr = new StreamReader(src);
            string dllPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dllPath = Path.Combine(dllPath, "Octopus.Description.Number.dll");
            string body = sr.ReadToEnd();
            sr.Close();
            body = string.Format(body, database, dllPath);
            StreamWriter sw = new StreamWriter(dest);
            sw.Write(body);
            sw.Close();
            //DatabaseManager.ExecuteScript(dest, database, conn);
            File.Delete(dest);
        }

        public bool UpdateDatabase(string pExpectedVersion, string pDatabaseName, string pScriptPath)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
             /*  // string currentVersion = DatabaseManager.GetDatabaseVersion(pDatabaseName, connection);
                
                //List<SqlUpdateDatabaseScript> scripts = _GetScriptsToUpgradeDatabase(pExpectedVersion, currentVersion);
               // foreach (SqlUpdateDatabaseScript script in scripts)
                {
                    if (UpdateDatabaseEvent != null)
                        UpdateDatabaseEvent(script.Current, script.Expected);

                    string createSqlfile = Path.Combine(pScriptPath, string.Format(UPDATEDATABASE, script.Current, script.Expected));
                    DatabaseManager.ExecuteScript(createSqlfile, pDatabaseName, connection);
                }

                if (UpdateDatabaseEvent != null)
                    UpdateDatabaseEvent("", "");
                DatabaseManager.ExecuteScript(_GetSqlObjects(), pDatabaseName, connection);

                try
                {
                    _LoadUserDefinedFunctions(pDatabaseName, connection);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cannot load Octopus.Description.Number.dll: " + e.Message);
                }
                */
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show("A database migration problem occured. This can be due to a bug or to a change made in your data model. Please try again using the option 'Create new database' and contact your support.",
                    "Upgrading error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
                return false;//throw;
            }
        }

        private bool _createDatabase(string pDatabaseName, string pDatabaseVersion, string pScriptPath, SqlConnection pSqlConnection)
        {
            try
            {
                pSqlConnection.Open();
                //DatabaseManager.CreateDatabase(pDatabaseName, pSqlConnection);

                string createSqlfile = Path.Combine(pScriptPath, string.Format(CREATEDATABASE, pDatabaseVersion));
               // DatabaseManager.ExecuteScript(createSqlfile, pDatabaseName, pSqlConnection);
                //DatabaseManager.ExecuteScript(Path.Combine(pScriptPath, INITIAL_DATAS), pDatabaseName, pSqlConnection);
                //DatabaseManager.ExecuteScript(Path.Combine(pScriptPath, INITIAL_ACCOUNTING_RULES), pDatabaseName, pSqlConnection);
               // DatabaseManager.ExecuteScript(_GetSqlObjects(), pDatabaseName, pSqlConnection);

                try
                {
                    _LoadUserDefinedFunctions(pDatabaseName, pSqlConnection);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cannot load Octopus.Description.Number.dll: " + e.Message);
                }

                pSqlConnection.Close();
                return true;
            }
            catch
            {
                pSqlConnection.Close();
                throw;
            }
        }

        public bool CreateDatabase(string pDatabaseName, string pDatabaseVersion, string pScriptPath)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            return _createDatabase(pDatabaseName, pDatabaseVersion, pScriptPath, connection);
        }

        public string GetDatabaseDefaultPath()
        {
            throw new Exception("функция не объявлена");
            string path = "";

            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                
                try
                {
                    //path = DatabaseManager.GetDatabasePath(connection) + @"\Data";
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }

                connection.Close();
            }
            catch
            {
                connection.Close();
                throw;
            }
            return path;
        }

        public bool CreateDatabase(string pAccountName, string pDatabaseName, string pLogin, string pPassword, string pVersion, string pScriptPath)
        {
            throw new Exception("функция не объявлена");
            //SqlConnection connection = DatabaseConnection.Remoting.GetSqlConnectionOnMaster();
            //_createDatabase(pDatabaseName, pVersion, pScriptPath, connection);
            //_addDatabaseToAccounts(pAccountName, pDatabaseName, pLogin, pPassword, connection);
            throw new Exception("функция не объявлена");
            return false;
        }






        public SqlDatabaseSettings GetSQLDatabaseSettings(string pDatabaseName)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SqlDatabaseSettings sqlDatabase = _GetDatabaseInfos(pDatabaseName, connection);

                connection.Close();
                return sqlDatabase;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public List<SqlDatabaseSettings> GetSQLDatabasesSettings(string pDatabaseServerName, string pDatabaseLoginName, string pDatabasePassword)
        {
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                SQLInfoEnumerator sie = new SQLInfoEnumerator
                {
                    SQLServer = pDatabaseServerName,
                    Username = pDatabaseLoginName,
                    Password = pDatabasePassword
                };

                List<SqlDatabaseSettings> list = new List<SqlDatabaseSettings>();
                foreach (string database in sie.EnumerateSQLServersDatabases())
                {
                    SqlDatabaseSettings sqlDatabase = _GetDatabaseInfos(database, connection);
                    if (sqlDatabase == null) continue;

                    list.Add(sqlDatabase);
                }

                connection.Close();
                return list;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }



        public bool ExecuteScript(string pScriptPath, string pDatabase, string pServerName, string pLoginName, string pPassword)
        {
            throw new Exception("функция не объявлена");
            string sqlConnection = String.Format(@"user id={0};password={1};data source={2};persist security info=False;initial catalog={3};connection timeout=10",
                pLoginName, pPassword, pServerName, pDatabase);

            SqlConnection connection = new SqlConnection(sqlConnection);
            try
            {
                connection.Open();
                //DatabaseManager.ExecuteScript(pScriptPath, pDatabase, connection);
                connection.Close();
                return true;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }



        private static SqlDatabaseSettings _GetDatabaseInfos(string pDatabaseName, SqlConnection pSqlConnection)
        {

            SqlDatabaseSettings sqlDatabase = new SqlDatabaseSettings { Name = pDatabaseName };
            string version = DatabaseManager.GetDatabaseVersion(pDatabaseName, pSqlConnection);

            if (string.IsNullOrEmpty(version)) return null;

            sqlDatabase.Version = version;
            sqlDatabase.Size = DatabaseManager.GetDatabaseSize(pDatabaseName, pSqlConnection);


            return sqlDatabase;
        }

        public string Backup(string pDatabaseName,string pDatabaseVersion, string pBranchCode,string pBackupPath)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                string fileName = 
                    _FindAvailableName(String.Format("Octopus-{0}-{1}-{2}-@-{3}.bak", pDatabaseVersion, pDatabaseName, 
                    pBranchCode, TimeProvider.Today.ToString("ddMMyyyy")));
                //BackupManager.Backup(fileName, pBackupPath, pDatabaseName, connection);

                connection.Close();
                return fileName;
            }
            catch(Exception)
            {
                connection.Close();
                throw;
            }
        }

        public bool Restore(string pBackupPath, string pDatabase)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            //bool rawName = true;
            //int suffix = 1;
            try
            {
                connection.Open();

                string dataDirectory = GetDatabaseDefaultPath();
                
                //BackupManager.Restore(pBackupPath, pDatabase, dataDirectory, connection);

                connection.Close();
                return true;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        private static string _FindAvailableName(string pFileName)
        {
            throw new Exception("функция не объявлена");
            bool available = false;
            int counter = -1;
            string name = pFileName;
            while (!available)
            {
                counter++;
                if (counter > 0) name += string.Format(" ({0})", counter);
                available = (!_IsThisNameAlreadyUsed(name));
            }
            return name;
        }

        private static bool _IsThisNameAlreadyUsed(string pName)
        {
            throw new Exception("функция не объявлена");
            //string fileName = Path.Combine(UserSettings.BackupPath, pName);
            //return (File.Exists(fileName + ".bak")) || (File.Exists(fileName + ".bak.zip"));
            return true;
        }

    
        /// <summary>
        /// Dump all the necessary database objects (views, functions, and stored procedures)
        /// into mdb file.
        /// </summary>
        public void DumpObjects(string pDatabaseName)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                //DatabaseManager.DumpObjects(pDatabaseName, connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public void ShrinkDatabase()
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection;
            try
            {
                connection.Open();
                //DatabaseManager.ShrinkDatabase(TechnicalSettings.DatabaseName, connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }

        public void SaveDatabaseDiagramsInXml(bool pBool,string pDatabaseName)
        {
            throw new Exception("функция не объявлена");
            SqlConnection connection = ConnectionManager.GeneralSqlConnection; 
            try
            {
                connection.Open();
                //CompareDatabase.SaveDatabaseDiagramsInXml(pBool,pDatabaseName,connection);
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
        }
    }
}
