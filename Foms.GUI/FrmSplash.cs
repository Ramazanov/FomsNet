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
using System.Windows.Forms;
using Foms.CoreDomain;
using Foms.GUI.Database;
using Foms.GUI.Login;
using Foms.GUI.UserControl;
using Foms.Services;
using Foms.Shared.Settings;
using Microsoft.Win32;
using Foms.Services;

namespace Foms.GUI
{
    public partial class FrmSplash : SweetBaseForm
    {
        private bool _settingsAreOk;
        private bool _sqlServerConnectionIsOk;
        private bool _sqlDatabaseConnectionIsOk;
        private bool _sqlDatabaseVersionIsOk;
        private bool _sqlDatabaseSchemaIsOk;
 
        private bool _environnementIsOk;
        private bool _applicationSettingsAreOk;
        private bool _accountingSettingsAreOk;
        private string _user;
        private string _password;

        public FrmSplash(string pUserName, string pPassword)
        {
            InitializeComponent();
            _user = pUserName;
            _password = pPassword;
        }

        private void FrmSplash_Shown(object sender, EventArgs e)
        {
            bWOneToSeven.RunWorkerAsync();
        }

        private void _CheckOctopusConfiguration(bool pOneToSeven)
        {
            if(pOneToSeven)
            {
                if (!_settingsAreOk) _CheckTechnicalSettings();
                if (!_sqlServerConnectionIsOk) _CheckSQLServerConnection();
                if (!_sqlDatabaseConnectionIsOk) _CheckSQLDatabaseConnection();
                if (!_sqlDatabaseVersionIsOk) _CheckDatabaseVersion();
                if (!_sqlDatabaseSchemaIsOk) _CheckDatabaseSchema();
                if (!_environnementIsOk) _CheckEnvironnement();
            }
            else
            {
                if (!_applicationSettingsAreOk) 
                    _CheckApplicationSettings();
                if (!_accountingSettingsAreOk)
                    _CheckGeneralSettings();
            }
        }

        private void _CheckApplicationSettings()
        {
            bWSeventToEight.ReportProgress(8, "CheckApplicationSettings.Text");
 

            _applicationSettingsAreOk = true;
        }
        
        private void _CheckGeneralSettings()
        {
            bWSeventToEight.ReportProgress(9, "CheckAccountingSettings.Text");
 
            _accountingSettingsAreOk = true;
        }
        
        private void _LoadLoginForm()
        {
                FrmLogin login = new FrmLogin(_user, _password);
                login.ShowDialog();
        }

        private void _CheckEnvironnement()
        {
            bWOneToSeven.ReportProgress(7, "CheckRunningEnvironnement.Text");
            _environnementIsOk = true;
        }
        
       
        private void _CheckDatabaseSchema()
        {
            bWOneToSeven.ReportProgress(5, "CheckDatabaseSchema.Text");
 
            _sqlDatabaseSchemaIsOk = true;
        }

        private void _CheckDatabaseVersion()
        {
            /*
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLDatabaseVersion(TechnicalSettings.SoftwareVersion, TechnicalSettings.DatabaseName))
            {
                // Automatic backup of database
                DialogResult dialogResult = MessageBox.Show(
                        "BackupProcess.Text", "Backup.Text", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);

                
                if (dialogResult == DialogResult.OK)
                {
                    bWOneToSeven.ReportProgress(4, "Backup.Text" + " Path: " + UserSettings.BackupPath);
                    ServicesProvider.GetInstance().GetDatabaseServices().Backup(TechnicalSettings.DatabaseName,
                                                            TechnicalSettings.SoftwareVersion,
                                                            "Upgrade",
                                                            UserSettings.BackupPath);
                }

                bWOneToSeven.ReportProgress(4, "CheckDatabaseVersion.Text");
                _DatabaseUpdateScript();
            }
            */
            _sqlDatabaseVersionIsOk = true;
        }

        private void _CheckSQLDatabaseConnection()
        {
            bWOneToSeven.ReportProgress(3, "CheckDatabaseConnection.Text");
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLDatabaseConnection())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerSettings);

            _sqlDatabaseConnectionIsOk = true;
        }

        private void _CheckSQLServerConnection()
        {
            bWOneToSeven.ReportProgress(2, "CheckSQLServerConnection.Text");
            if (!ServicesProvider.GetInstance().GetDatabaseServices().CheckSQLServerConnection())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerConnection);

            _sqlServerConnectionIsOk = true;
        }

        private void _CheckTechnicalSettings()
        {
            bWOneToSeven.ReportProgress(1, "CheckTechnicalSettings.Text");
            if (!TechnicalSettings.CheckSettings())
                _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum.SqlServerConnection);

            _settingsAreOk = true;
        }

        private void _LoadDatabaseSettingsForm(FrmDatabaseSettingsEnum pFrmDatabaseSettingsEnum)
        {
            FrmDatabaseSettings databaseSettings = new FrmDatabaseSettings(pFrmDatabaseSettingsEnum,true,false);
            databaseSettings.ShowDialog();

            _CheckOctopusConfiguration(true);
        }

        private void _DatabaseUpdateScript()
        {
            oPBarMicroProgression.Value = 0;
            oPBMacroProgression.Step = 10;

            DatabaseServices databaseServices = ServicesProvider.GetInstance().GetDatabaseServices();

            databaseServices.UpdateDatabaseEvent += FrmSplash_UpdateDatabaseEvent;

            databaseServices.UpdateDatabase(TechnicalSettings.SoftwareVersion, TechnicalSettings.DatabaseName, UserSettings.GetUpdatePath);

            _CheckOctopusConfiguration(true);
        }

        private void FrmSplash_UpdateDatabaseEvent(string pCurrentDatabase, string pExpectedDatabase)
        {
            string updateText = pCurrentDatabase == string.Empty && pExpectedDatabase == string.Empty
                                                 ? string.Format("UpdateDetails.Text")
                                                 : string.Format(("UpdateFrom.Text"), pCurrentDatabase,
                                                                 pExpectedDatabase);
            bWOneToSeven.ReportProgress(24, updateText); 
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _CheckOctopusConfiguration(true);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 24)
            {
                oPBarMicroProgression.Text = e.UserState.ToString();
                oPBarMicroProgression.PerformStep();
            }
            else
            {
                oPBMacroProgression.Text = string.Format("{0} / 9", e.ProgressPercentage);
                labelConfigurationValue.Text = e.UserState.ToString();
                oPBMacroProgression.PerformStep();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Hide();
            _LoadLoginForm();

            if (User.CurrentUser.Id == 0)
            {
                Close();
                Application.Exit();
                return;
            }
            WindowState = FormWindowState.Normal;
            bWSeventToEight.RunWorkerAsync();
            Show();
        }

        private void bWSeventToEight_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _CheckOctopusConfiguration(false);
        }

        private void bWSeventToEight_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            oPBMacroProgression.Text = string.Format("{0} / 9", e.ProgressPercentage);
            labelConfigurationValue.Text = e.UserState.ToString();
            oPBMacroProgression.PerformStep();
        }

        private void bWSeventToEight_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}