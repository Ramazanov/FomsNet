using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Foms.CoreDomain.Database;
using Foms.GUI.Properties;
using Foms.GUI.Tools;
using Foms.GUI.UserControl;
using Foms.Shared.Settings;
using Foms.Services;

namespace Foms.GUI.Database
{
    public partial class FrmDatabaseSettings : Form
    {
        private bool _badServerName;
        private bool _badLoginName;
        private bool _badPasswordName;
        private List<SqlDatabaseSettings> _sqlDatabases;
        private bool _exitApplicationIfClose;
        private readonly bool _showBackupRestoreButtons;
        
        private struct DatabaseOperation
        {
            public SqlDatabaseSettings Settings;
            public string File;
        }
        
        public FrmDatabaseSettings(FrmDatabaseSettingsEnum pDatabaseSettingsEnum, bool pExitApplicationIfClose, bool pShowBackupRestoreButtons)
        {
            InitializeComponent();
            _exitApplicationIfClose = pExitApplicationIfClose;
            _showBackupRestoreButtons = pShowBackupRestoreButtons;
            cbServerName.Text = TechnicalSettings.DatabaseServerName;
            txtLoginName.Text = TechnicalSettings.DatabaseLoginName;
            txtPassword.Text = TechnicalSettings.DatabasePassword;

            _InitializeForm(pDatabaseSettingsEnum);
        }

        private void _InitializeForm(FrmDatabaseSettingsEnum pFrmDatabaseSettingsEnum)
        {
            labelResult.Text = string.Empty;
            if (pFrmDatabaseSettingsEnum == FrmDatabaseSettingsEnum.SqlServerConnection)
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
                tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
                _InitializeTabPageServerConnection();
            }
            else if(pFrmDatabaseSettingsEnum == FrmDatabaseSettingsEnum.SqlServerSettings)
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
                _InitializeTabPageSqlServerSettings();
            }
            else
            {
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
                _InitializeTabPageSqlDatabaseSettings(TechnicalSettings.DatabaseName);
            }
        }

        private void _InitializeTabPageServerConnection()
        {
            buttonSave.Visible = false;
            btnDatabaseConnection.Visible = true;
            tableLayoutPanelServerSettings.Controls.Add(groupBoxSaveSettings, 0, 2);
        }

        private void buttonGetServersList_Click(object sender, EventArgs e)
        {
            _DetectServers();
        }

        private void _DetectServers()
        {
            Cursor = Cursors.WaitCursor;

            var sie = new SQLInfoEnumerator();
            cbServerName.Items.Clear();
            cbServerName.Items.AddRange(sie.EnumerateSQLServers());

            Cursor = Cursors.Default;
        }

        private void cbServerName_TextChanged(object sender, EventArgs e)
        {
            cbServerName.BackColor = Color.White;
            _badServerName = false;
            if (string.IsNullOrEmpty(cbServerName.Text))
            {
                cbServerName.BackColor = Color.Red;
                _badServerName = true;
            }
        }

        private void txtLoginName_TextChanged(object sender, EventArgs e)
        {
            txtLoginName.BackColor = Color.White;
            _badLoginName = false;
            if (string.IsNullOrEmpty(txtLoginName.Text))
            {
                txtLoginName.BackColor = Color.Red;
                _badLoginName = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            _badPasswordName = false;
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.BackColor = Color.Red;
                _badPasswordName = true;
            }
        }

        private void btnDatabaseConnection_Click(object sender, EventArgs e)
        {
            if (_badLoginName || _badPasswordName || _badServerName)
                MessageBox.Show("","Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                TechnicalSettings.DatabaseServerName = cbServerName.Text;
                TechnicalSettings.DatabaseLoginName = txtLoginName.Text;
                TechnicalSettings.DatabasePassword = txtPassword.Text;
                _CheckDatabaseSettings();
            }
        }

        private void _CheckDatabaseSettings()
        {
            if (ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLServerConnection())
            {
                tabControlDatabase.TabPages.Add(tabPageSQLServerSettings);
                tabControlDatabase.TabPages.Remove(tabPageSQLServerConnection);
                _InitializeTabPageSqlServerSettings();
            }
            else
                MessageBox.Show("ConnectionWasNotMade.Text", "Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void _InitializeTabPageSqlDatabaseSettings(string pDatabaseName)
        {
            SqlDatabaseSettings sqlDatabaseSettings = ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabaseSettings(pDatabaseName);
            _InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
        }

        private void _InitializeTabPageSqlDatabaseSettings(SqlDatabaseSettings pSqlDatabaseSettings)
        {
            lbSQLServerSettings.Text = string.Format(" {0}: {1}     {2}:  {3}     {4}:  ****", 
                "Server.Text", TechnicalSettings.DatabaseServerName,
                "Login.Text", TechnicalSettings.DatabaseLoginName,
                "Password.Text");

            lbSQLDatabaseSettingsName.Text = string.Format("{0}:  {1}            {2}:  {3}            {4}:  {5}", 
                "Database.Text", pSqlDatabaseSettings.Name,
                 "BranchCode.Text", pSqlDatabaseSettings.BranchCode,
                 "Size.Text", pSqlDatabaseSettings.Size);
            lbSQLDatabaseSettingsVersion.Text = string.Format("{0}:  {1}",  "Version.Text", pSqlDatabaseSettings.Version);

            buttonSave.Visible = true;
            btnDatabaseConnection.Visible = false;
            if (pSqlDatabaseSettings.Version != TechnicalSettings.SoftwareVersion)
            {
                tBDatabaseSettingsSchemaResult.Text = "";
                lbSQLDatabaseSettingsUpgradeNeedfull.Visible = true;
                lbSQLDatabaseSettingsUpgradeNeedfull.Text = "UpgradeYourDatabase.Text";
                buttonSQLDatabaseSettingsUpgrade.Text = string.Format("{0} {1}","обновить до ", TechnicalSettings.SoftwareVersion);
                buttonSQLDatabaseSettingsUpgrade.Enabled = true;
                buttonSQLDatabaseSettingsUpgrade.Tag = pSqlDatabaseSettings;
            }
            else
            {
                lbSQLDatabaseSettingsUpgradeNeedfull.Visible = false;
                buttonSQLDatabaseSettingsUpgrade.Text = "Upgrade.Text";
                buttonSQLDatabaseSettingsUpgrade.Enabled = false;
                _CheckDatabaseStructure(pSqlDatabaseSettings.Name);
            }

            labelResult.Text = string.Empty;
            tableLayoutPanelDatabaseSettings.Controls.Add(groupBoxSQLSettings, 0, 0);
            tableLayoutPanelDatabaseSettings.Controls.Add(groupBoxSaveSettings, 0, 3);
        }

        private void _CheckDatabaseStructure(string pDatabaseName)
        {
   
        }

        private void _InitializeTabPageSqlServerSettings()
        {
            lbSQLServerSettings.Text = string.Format("{0}:  {1}     {2}:  {3}     {4}:  ****",
               "Server.Text", TechnicalSettings.DatabaseServerName, 
                    "Login.Text",TechnicalSettings.DatabaseLoginName,
                   "Password");
            tableLayoutPanelSQLSettings.Controls.Add(groupBoxSQLSettings, 0, 0);
            tableLayoutPanelSQLSettings.Controls.Add(groupBoxSaveSettings, 0, 2);
            labelResult.Text = string.Empty;

            buttonBackup.Visible = _showBackupRestoreButtons;
            buttonRestore.Visible = _showBackupRestoreButtons;

            buttonSave.Visible = true;
            btnDatabaseConnection.Visible = false;
            buttonSQLServerChangeSettings.Enabled = false;
            bWDatabasesDetection.RunWorkerAsync();
            _DisplayDatabases();
        }

        private void _DisplayDatabases()
        {
            if (_sqlDatabases == null)
            {
                groupBoxDatabaseManagement.Enabled = false;
                labelDetectDatabasesInProgress.Visible = true;
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                listViewDatabases.Items.Clear();
                lbDatabases.Text = string.Format("{0} ({1})",
                    "Databases",
                    _sqlDatabases.Count);

                _sqlDatabases.Sort((x,y) => x.Name.CompareTo(y.Name));
                foreach (SqlDatabaseSettings sqlDatabase in _sqlDatabases)
                {
                    ListViewItem item = new ListViewItem(sqlDatabase.Name);
                    item.SubItems.Add(sqlDatabase.BranchCode);
                    item.SubItems.Add(sqlDatabase.Version);
                    item.SubItems.Add(sqlDatabase.Size);
                    item.Tag = sqlDatabase;
                    if (sqlDatabase.Name == TechnicalSettings.DatabaseName)
                    {
                        item.BackColor = Color.Green;
                        listViewDatabases.Items.Insert(0, item);
                        item.Selected = true;
                    }
                    else
                        listViewDatabases.Items.Add(item);
                }

            }
            Cursor = Cursors.Default;
            listViewDatabases.Focus();
        }

        private void linkLabelSQLServerInstallInstruction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.Fomsnetwork.org/dmdocuments/Foms_Installation_Guide-EN.pdf");
        }

        private void FrmDatabaseSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(_exitApplicationIfClose) Environment.Exit(0);
        }

        private void listViewDatabases_DoubleClick(object sender, EventArgs e)
        {
            _ShowDatabaseDetails();
        }

        private void buttonSetAsDefault_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = string.Format("SelectInList.Text"); 
                return;
            }

            TechnicalSettings.DatabaseName = listViewDatabases.SelectedItems[0].Text;
            _DisplayDatabases();
            _exitApplicationIfClose = true;
            labelResult.ForeColor = Color.Black;
            labelResult.Text = string.Format("{0} {1}", TechnicalSettings.DatabaseName, "DefaultDatabase.Text");
        }

        private void buttonSQLServerChangeSettings_Click(object sender, EventArgs e)
        {
            tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
            tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
            tabControlDatabase.TabPages.Add(tabPageSQLServerConnection);
            _InitializeTabPageServerConnection();
            _exitApplicationIfClose = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_exitApplicationIfClose)
            {
                Restart.LaunchRestarter();
            }
            else
            {
                Close();
            }
        }

        private void buttonBackup_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = 
                    string.Format("SelectInList.Text");
                return;
            }

            folderBrowserDialog.SelectedPath = UserSettings.BackupPath;
            if (DialogResult.OK != folderBrowserDialog.ShowDialog()) return;

            UserSettings.BackupPath = folderBrowserDialog.SelectedPath;

            var sqlDatabase = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;

            groupBoxDatabaseManagement.Enabled = false;
            labelResult.ForeColor = Color.Black;
            labelResult.Text =
                string.Format("{0} {1} {2} ", "BackUpDatabase",
                              sqlDatabase.Name, "InProgress");

            bWDatabaseBackup.RunWorkerAsync(sqlDatabase);
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = string.Format("SelectInList.Text");
                return;
            }

            openFileDialog.FileName = "";
            if (DialogResult.OK != openFileDialog.ShowDialog()) return;
            SqlDatabaseSettings sqlDatabase = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;
            buttonRestore.Tag = sqlDatabase;

            DatabaseOperation dbo = new DatabaseOperation {Settings = sqlDatabase, File = openFileDialog.FileName};

            _exitApplicationIfClose = true;
            groupBoxDatabaseManagement.Enabled = false;
            groupBoxSQLSettings.Enabled = false;
            buttonSave.Enabled = false;
            labelResult.ForeColor = Color.Black;
            labelResult.Text = string.Format("{0} {1} {2}",
                                             "RestoreDatabase.Text.Text",
                                             sqlDatabase.Name,
                                             "InProgress.Text");

            bWDatabaseRestore.RunWorkerAsync(dbo);
        }

        private void buttonSQLServerSettingsShowDetails_Click(object sender, EventArgs e)
        {
            _ShowDatabaseDetails();
        }

        private void _ShowDatabaseDetails()
        {
            if (listViewDatabases.SelectedItems.Count == 0)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = string.Format( "SelectInList.Text");
                return;
            }

            SqlDatabaseSettings sqlDatabaseSettings = (SqlDatabaseSettings) listViewDatabases.SelectedItems[0].Tag;
            tabControlDatabase.TabPages.Remove(tabPageSQLServerSettings);
            tabControlDatabase.TabPages.Add(tabPageSqlDatabaseSettings);
            _InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
        }

        private void buttonSQLDatabaseSettingsChangeName_Click(object sender, EventArgs e)
        {
            tabControlDatabase.TabPages.Remove(tabPageSqlDatabaseSettings);
            tabControlDatabase.TabPages.Add(tabPageSQLServerSettings);
            _InitializeTabPageSqlServerSettings();
            _exitApplicationIfClose = true;
        }

        private void FrmDatabaseSettings_UpdateDatabaseEvent(string pCurrentDatabase, string pExpectedDatabase)
        {
            if (pCurrentDatabase == string.Empty && pExpectedDatabase == string.Empty)
                bWDatabaseUpdate.ReportProgress(1, string.Format("UpdatingDatabase.Text"));
            else
                bWDatabaseUpdate.ReportProgress(1, string.Format("{0} {1} {2} {3}", "UpdateDBFrom.Text", pCurrentDatabase,
                    "To.Text",
                    pExpectedDatabase));
        }

        private void buttonSQLDatabaseSettingsUpgrade_Click(object sender, EventArgs e)
        {
            groupBoxSQLDatabaseSettings.Enabled = false;
            groupBoxSQLSettings.Enabled = false;
            buttonSave.Enabled = false;
            _exitApplicationIfClose = true;
            lbSQLDatabaseSettingsUpgradeNeedfull.Visible = false;
            groupBoxSQLDatabaseStructure.Enabled = false;
            labelResult.Text = "UpgradeDatabaseInProgress.Text";
            var sqlDatabaseSettings = (SqlDatabaseSettings)buttonSQLDatabaseSettingsUpgrade.Tag;
            bWDatabaseUpdate.RunWorkerAsync(sqlDatabaseSettings);
        }

        public List<string> FileSearch(string pDir, string pFile)
        {
            List<string> listFiles = new List<string>();
            try
            {
                foreach (string f in Directory.GetFiles(pDir, pFile))
                {
                    listFiles.Add(f);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return listFiles;
        }
        
        private void buttonCreateNewDatabase_Click(object sender, EventArgs e)
        {
            var frmDatabaseName = new FrmDatabaseName();
            if (DialogResult.OK != frmDatabaseName.ShowDialog()) return;

            if (FileSearch(ServicesProvider.GetInstance().GetDatabaseServices().GetDatabaseDefaultPath(), frmDatabaseName.Result + ".mdf").Count > 0 || FileSearch(ServicesProvider.GetInstance().GetDatabaseServices().GetDatabaseDefaultPath(), frmDatabaseName.Result + "_log.ldf").Count > 0)
            {
                labelResult.Text = "FileException.Text";
            }
            else
            {
                groupBoxDatabaseManagement.Enabled = false;
                labelResult.ForeColor = Color.Black;
                labelResult.Text = "DatabaseCreation.Text";
                bWDatabaseCreation.RunWorkerAsync(frmDatabaseName.Result);
            }
        }

        private void backgroundWorkerDetectDatabases_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            buttonSQLServerChangeSettings.Enabled = true;
            groupBoxDatabaseManagement.Enabled = true;
            labelDetectDatabasesInProgress.Visible = false;
            _DisplayDatabases();
        }

        private void backgroundWorkerDetectDatabases_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _sqlDatabases = ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabasesSettings(
                    TechnicalSettings.DatabaseServerName, TechnicalSettings.DatabaseLoginName,
                    TechnicalSettings.DatabasePassword);
        }

        private void bWDatabaseCreation_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;
            labelDetectDatabasesInProgress.Visible = false;
            if(e.Result == null)
            {
                labelResult.ForeColor = Color.Black;
                labelResult.Text = "DatabaseCreationCancelled.Text";
            }
            else if ((bool)e.Result)
            {
                labelResult.ForeColor = Color.Black;
                labelResult.Text = string.Format("{0} {1} {2}", "Database.Text", TechnicalSettings.DatabaseName, "Created.Text");
                _DisplayDatabases();
                _exitApplicationIfClose = true;
            }
            else
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text ="CreationFailed.Text";
            }
        }

        private void bWDatabaseCreation_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string name = e.Argument.ToString();
            e.Result = ServicesProvider.GetInstance().GetDatabaseServices().CreateDatabase(name, TechnicalSettings.SoftwareVersion, UserSettings.GetUpdatePath);
            if (!(bool)e.Result) return;

            TechnicalSettings.DatabaseName = name;
            _sqlDatabases.Add(new SqlDatabaseSettings { Name = TechnicalSettings.DatabaseName, Version = TechnicalSettings.SoftwareVersion, Size = "-" });
        }

        private void bWDatabaseBackup_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var sqlDatabaseSettings = (SqlDatabaseSettings) e.Argument;
            e.Result = ServicesProvider.GetInstance().GetDatabaseServices().Backup(sqlDatabaseSettings.Name,
                    sqlDatabaseSettings.Version, sqlDatabaseSettings.BranchCode, UserSettings.BackupPath);
        }

        private void bWDatabaseBackup_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;

            if (e.Result == null)
            {
                labelResult.ForeColor = Color.Black;
                labelResult.Text = "BackupCancelled.Text";
            }
            else if (e.Result.ToString() != string.Empty)
            {
                labelResult.ForeColor = Color.Black;
                labelResult.Text = string.Format("FileBackedUp.Text", e.Result);
            }
            else
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = "BackupCancelled.Text";
            }
        }

        private void bWDatabaseRestore_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var dbo = (DatabaseOperation) e.Argument;
            e.Result = ServicesProvider.GetInstance().GetDatabaseServices().Restore(dbo.File, dbo.Settings.Name);
        }

        private void bWDatabaseRestore_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxDatabaseManagement.Enabled = true;
            labelDetectDatabasesInProgress.Visible = false;
            groupBoxSQLSettings.Enabled = true;
            buttonSave.Enabled = true;

            if (e.Result == null)
            {
                labelResult.ForeColor = Color.Black;
                labelResult.Text = "RestoreCancelled.Text";
            }
            else if ((bool) e.Result)
            {
                labelResult.ForeColor = Color.Black;
                var sqlDatabase = (SqlDatabaseSettings)buttonRestore.Tag;
                labelResult.Text = string.Format(" {0} {1} {2}", "RestoreDatabase.Text", sqlDatabase.Name, "Successful.Text");
                _sqlDatabases.Remove(sqlDatabase);
                _sqlDatabases.Add(ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabaseSettings(sqlDatabase.Name));
                _DisplayDatabases();
            }
            else
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text ="RestoreFailed.Text";
            }
        }

        private void bWDatabaseUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //We must create a databaseService here, otherwise we can't invoking the event
            DatabaseServices databaseServices = ServicesProvider.GetInstance().GetDatabaseServices();
            databaseServices.UpdateDatabaseEvent += new DatabaseServices.ExecuteUpgradeSqlDatabaseFile(FrmDatabaseSettings_UpdateDatabaseEvent);

            var sqlDatabaseSettings = (SqlDatabaseSettings) e.Argument;
            e.Result = databaseServices.UpdateDatabase(TechnicalSettings.SoftwareVersion, sqlDatabaseSettings.Name, UserSettings.GetUpdatePath);
        }

        private void bWDatabaseUpdate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            groupBoxSQLDatabaseSettings.Enabled = true;
            groupBoxSQLSettings.Enabled = true;
            buttonSave.Enabled = true;
            groupBoxSQLDatabaseStructure.Enabled = true;
            if((bool)e.Result)
            {
                var sqlDatabaseSettings = (SqlDatabaseSettings)buttonSQLDatabaseSettingsUpgrade.Tag;
                labelResult.ForeColor = Color.Black;
                labelResult.Text = string.Format("{0} {1} {2}", "UpgradeDatabase.Text", sqlDatabaseSettings.Name, "Successful.Text");
                _sqlDatabases.Remove(sqlDatabaseSettings);
                sqlDatabaseSettings = ServicesProvider.GetInstance().GetDatabaseServices().GetSQLDatabaseSettings(sqlDatabaseSettings.Name);
                _sqlDatabases.Add(sqlDatabaseSettings);
                _InitializeTabPageSqlDatabaseSettings(sqlDatabaseSettings);
            }
            else
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = "UpgradeFailed.Text";
            }
        }

        private void bWDatabaseUpdate_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            labelResult.Text = e.UserState.ToString();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (_exitApplicationIfClose)
            {
                TechnicalSettings.UseDebugMode = true;
                Restart.LaunchRestarter();
            }
            else
            {
                Close();
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            cbServerName.Text = Environment.MachineName + "\\SQLEXPRESS";
        }
        
    }
}