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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Foms.CoreDomain;
using Foms.Enums;
using Foms.ExceptionsHandler;
using Foms.Extensions;
using Foms.GUI;
using Foms.GUI.Configuration;
using Foms.GUI.Database;
using Foms.GUI.Tools;
using Foms.GUI.UserControl;
using Foms.Services;
using Foms.Shared;
using Foms.Shared.Settings;
using Re = System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using Foms.CoreDomain.Export.Files;

namespace Foms.GUI
{
    public partial class LotrasmicMainWindowForm : SweetBaseForm
    {


        private List<MenuObject> _menuItems;


        public LotrasmicMainWindowForm()
        {
            InitializeComponent();
            _menuItems = new List<MenuObject>();
            _menuItems = ServicesProvider.GetInstance().GetRoleServices().GetMenuList();
            
           _InitializeTracer();
            _DisplayWinFormDetails();
        }

        private void _InitializeTracer()
        {
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Trace.AutoFlush = true;
            Trace.WriteLine("Octopus has started");
        }

        private void _DisplayWinFormDetails()
        {           
            _InitializeUserRights();
 

            _DisplayDetails();
            _InitializeContractCurrencies();
            _CheckForUpdate();
            
        }

        private void _InitializeContractCurrencies()
        {
            //mnuChartOfAccounts.DropDownItems.Clear();
            //List<Currency> _currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            //if (_currencies.Count > 1)
            //{
            //    ToolStripMenuItem mnuCurrency;
            //    mnuChartOfAccounts.Click -= mnuChartOfAccounts_Click;

            //    foreach (Currency cur in _currencies)
            //    {
            //        mnuCurrency = new ToolStripMenuItem();
            //        mnuCurrency.Text = cur.Name;
            //        mnuCurrency.Tag = cur;
            //        mnuCurrency.Click += (mnuContractCurrency_Clicked);
            //        mnuChartOfAccounts.DropDownItems.Add(mnuCurrency);
            //    }
            //    mnuCurrency = new ToolStripMenuItem();
            //    Currency pivottedValue = new Currency
            //                                 {
            //                                     Name = MultiLanguageStrings.GetString(Ressource.LotrasmicMainWindowForm, "ConsolidatedCurrencies.Text"),
            //                                     Code = MultiLanguageStrings.GetString(Ressource.LotrasmicMainWindowForm, "ConsolidatedCurrencies.Text"),
            //                                     IsPivot = false,
            //                                     IsSwapped = false,
            //                                     Id = -1
            //                                 };
            //    mnuCurrency.Text = pivottedValue.Name;
            //    mnuCurrency.Tag = pivottedValue;
            //    mnuCurrency.Click += (mnuContractCurrency_Clicked);
            //    mnuChartOfAccounts.DropDownItems.Add(mnuCurrency);
            //}
            //else
            //{
                
            //}

        }

        private void  mnuContractCurrency_Clicked(object sender, EventArgs e)
        {
            //List<Currency> _currencies = ServicesProvider.GetInstance().GetCurrencyServices().FindAllCurrencies();
            //if (_currencies.Count > 1)
            _InitializeContractCurrencies();
            //Currency cur = (Currency) ((ToolStripMenuItem) sender).Tag;
            //InitializeChartOfAccountsForm(cur.Id);
        }

        private void bwCheckVersion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /*VersionServicePortTypeClient softwareUpdate = new VersionServicePortTypeClient("VersionServicePort");
                string version = softwareUpdate.GetVersion(TechnicalSettings.SoftwareVersion);
                if (TechnicalSettings.SoftwareVersion != version)
                {
                    nIUpdateAvailable.Tag = softwareUpdate.GetUpdateLink(string.Empty);
                    nIUpdateAvailable.ShowBalloonTip(5000, "Software update",
                                                     "\nThe version " + version +
                                                     " is available for download.\nClick here to download",
                                                     ToolTipIcon.Info);
                }*/
                Guid guid = (Guid) e.Argument;
                string url = "http://www.octopusnetwork.org/info/getversion.php?guid=";
                url += guid + "&version=" + TechnicalSettings.SoftwareVersion;
                string buildNumber;
                try
                {
                    StreamReader bn = new StreamReader(Path.Combine(Application.StartupPath, "BuildLabel.txt"));
                    buildNumber = bn.ReadLine();
                    if (string.IsNullOrEmpty(buildNumber)) buildNumber = "NA";
                }
                catch
                {
                    buildNumber = "debug";
                }
                url += "." + buildNumber;
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "octopus";
                request.Timeout = 20000;
                WebResponse response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }
        }

        private void bwCheckVersionAtSourceForge_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                const string source = "http://sourceforge.net/projects/omfs/feed?filter=file";
                //const string source = "http://www.octopusnetwork.org/version.html";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(source);
                request.Method = "GET";
                request.Timeout = 20000;
                WebResponse response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                response.Close();

                Re.Match m = Re.Regex.Match(result, @"<a\shref=""(.*)"">.*_(.*)\.msi<\/a\>", Re.RegexOptions.Multiline);
                if (m.Success)
                {
                    string version = m.Groups[2].ToString();
                    string url = "http://sourceforge.net" + m.Groups[1];
                    e.Result = new object[] {version, url};
                    return;
                }
                (sender as BackgroundWorker).CancelAsync();
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
                (sender as BackgroundWorker).CancelAsync();
            }
        }

        private void bwCheckVersionAtSourceForge_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            try
            {
                object[] result = e.Result as object[];
                string version = result[0].ToString();
                //string url = result[1].ToString();
                string url = "http://wiki.octopusnetwork.org/display/Release/Home";
                //int versionRemote = int.Parse(result[0].ToString().Replace(".", ""));
                //int versionLocal = int.Parse(TechnicalSettings.SoftwareVersion.Replace("v", "").Replace(".", ""));

                string remote = version;
                string local = TechnicalSettings.SoftwareVersion.Replace("v", "");

                if (remote.Length > local.Length)
                    for (int i = 1; i <= remote.Length - local.Length; i++)
                        local += ".0";
                else
                    for (int i = 1; i <= local.Length - remote.Length; i++)
                        remote += ".0";

                string[] versionNumbersRemote = remote.Split('.');
                string[] versionNumbersLocal = local.Split('.');

                bool show = false;
                for (int i = 0; i < versionNumbersLocal.Length; i++)
                    if (int.Parse(versionNumbersRemote[i]) > int.Parse(versionNumbersLocal[i]))
                    {
                        show = true;
                        break;
                    }

                if (show)
                {
                    nIUpdateAvailable.Visible = true;
                    nIUpdateAvailable.Tag = url;
                    nIUpdateAvailable.ShowBalloonTip(8000, string.Format("OMFS version {0} available", version),
                        string.Format("Click here to download version {0} of\nOctopus Microfinance Suite.", version), ToolTipIcon.Info);
                }
            }
            catch {}
        }

        private void _CheckForUpdate()
        {
            if (UserSettings.AutoUpdate)
            {
               // Guid? guid = ServicesProvider.GetInstance().GetApplicationSettingsServices().GetGuid();
         
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = false;
                bw.DoWork += bwCheckVersion_DoWork;
         

                #if !DEBUG
                BackgroundWorker bwSF = new BackgroundWorker();
                bwSF.WorkerSupportsCancellation = true;
                bwSF.DoWork += bwCheckVersionAtSourceForge_DoWork;
                bwSF.RunWorkerCompleted += bwCheckVersionAtSourceForge_RunWorkerCompleted;
                bwSF.RunWorkerAsync();
                #endif
            }
        }

        private void _DisplayDetails()
        {
            mainStatusBarLblUserName.Text = String.Format("{0} ({1})", User.CurrentUser.FirstName, User.CurrentUser.UserRole);
            toolStripStatusLblDB.Text = String.Format(" {0}", TechnicalSettings.DatabaseName);
             
            //mainStatusBarLblUserName.ForeColor = Color.Red;

            toolBarLblVersion.Text = String.Format("Octopus {0}", TechnicalSettings.SoftwareVersion);

        }

        private void _InitializeUserRights()
        {
            foreach (ToolStripMenuItem mi in MainMenuStrip.Items)
            {
                if (mi is ToolStripSeparator) continue;
                Role _role = User.CurrentUser.UserRole;
                MenuObject foundMO = GetMenuObject(mi.Text);
                mi.Enabled = _role.IsMenuAllowed(foundMO);
                mi.Tag = foundMO;
                _InitializeMenuChildren(mi, _role);
            }
        }
        private void _InitializeMenuChildren(ToolStripMenuItem pMenuItem, Role pRole)
        {
            if (!pMenuItem.HasDropDownItems)
            {
                return;
            }
            foreach (Object tsmi in pMenuItem.DropDownItems)
            {
                if (! (tsmi is ToolStripMenuItem))
                    continue;

                ToolStripMenuItem tsmi_menu = (ToolStripMenuItem)tsmi;
                MenuObject foundMO = GetMenuObject(tsmi_menu.Text);
                tsmi_menu.Enabled = pRole.IsMenuAllowed(foundMO);
                tsmi_menu.Tag = foundMO;
                _InitializeMenuChildren(tsmi_menu, pRole);
            }
            return;

        }
        private MenuObject GetMenuObject(string pText)
        {
            MenuObject foundObject = _menuItems.Find(item => item == pText.Trim());
            if (foundObject == null)
                foundObject = ServicesProvider.GetInstance().GetRoleServices().AddNewMenu(pText.Trim());
            return foundObject;
        }
 

        public void SetInfoMessage(string pMessage)
        {
            mainStatusBarLblInfo.Text = pMessage;
        }

        private void mnuNewPerson_Click(object sender, EventArgs e)
        {

        }

        private void menuItemExportTransaction_Click(object sender, EventArgs e)
        {
            Form exportTransactions = new ExportBookingsForm { MdiParent = this };

            exportTransactions.Show();
       
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            mainStatusBarLblDate.ForeColor = TimeProvider.IsUsingSystemTime ? Color.Black : Color.Red;
            mainStatusBarLblDate.Text = String.Format("{0} {1}", TimeProvider.Today.ToString("dd/MM/yyyy"),
                                                      TimeProvider.Now.ToLongTimeString());
        }

        private void menuItemAboutOctopus_Click(object sender, EventArgs e)
        {
            AboutOctopus aboutOctopus = new AboutOctopus();
            aboutOctopus.ShowDialog();
        }


        private void menuItemBackupData_Click(object sender, System.EventArgs e)
        {
            FrmDatabaseSettings frmDatabaseSettings = new FrmDatabaseSettings(FrmDatabaseSettingsEnum.SqlServerSettings, false, true);
            frmDatabaseSettings.ShowDialog();
        }

        private void menuItemDatabaseMaintenance_Click(object sender, EventArgs e)
        {
            Form form = new frmDatabaseMaintenance();
            form.ShowDialog();
        }

        private void toolBarButtonPerson_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            toolBarButNew.ShowDropDown();
        }




        private void octopusForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.octopusnetwork.org/forum");
        }



        private void LotrasmicMainWindowForm_Load(object sender, EventArgs e)
        {

            LogUser();
            //_LoadReports();
 


        }

        private void LogUser()
        {

        }

        private void menuItemAddUser_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm { MdiParent = this };
            userForm.Show();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoles rolesForm = new FrmRoles(this) { MdiParent = this };
            rolesForm.Show();
        }

    }
}