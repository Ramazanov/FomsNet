//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Foms.Shared.Settings;

namespace Foms.GUI.Configuration
{
    /// <summary>
    /// Description r�sum�e de FrmUserSettings.
    /// </summary>
    public partial class FrmUserSettings : Form
    {
        /// <summary>
        /// Advanced settings Form.
        /// </summary>
        public FrmUserSettings()
        {
            InitializeComponent();
            cbAutoUpdate.Visible = !TechnicalSettings.UseOnlineMode;
        }

        private void FrmAdmin_Load(object sender, System.EventArgs e)
        {
            LoadParameters();
        }

        private void LoadParameters()
        {
            _SetLanguage();
            cbAutoUpdate.Checked = UserSettings.AutoUpdate;
            txtBackupPath.Text = UserSettings.BackupPath;
            txtExportConsoPath.Text = UserSettings.ExportConsoPath;
        }

        private void _SetLanguage()
        {
            string currentLanguage = UserSettings.Language;
            rdbEnglish.Checked = (currentLanguage == (string)rdbEnglish.Tag);
            rdbRussian.Checked = (currentLanguage == (string)rdbRussian.Tag);
            rdbFrench.Checked = (currentLanguage == (string)rdbFrench.Tag);
            rbKyrgyz.Checked = (currentLanguage == (string)rbKyrgyz.Tag);
            rbTadjik.Checked = (currentLanguage == (string)rbTadjik.Tag);

            if ((!rdbEnglish.Checked) && (!rdbRussian.Checked) && (!rdbFrench.Checked))
            {
                rdbEnglish.Checked = true;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (!TestFolders()) return;

            UserSettings.Language = rdbEnglish.Checked
                                        ? (string) rdbEnglish.Tag
                                        : rdbFrench.Checked ? (string) rdbFrench.Tag
                                        : rdbRussian.Checked ? (string) rdbRussian.Tag
                                        : rbKyrgyz.Checked ? (string) rbKyrgyz.Tag
                                        : rbTadjik.Checked ? (string) rbTadjik.Tag
                                        : (string) rbSpanish.Tag;

            UserSettings.BackupPath = txtBackupPath.Text;
            UserSettings.ExportConsoPath = txtExportConsoPath.Text;
            UserSettings.AutoUpdate = cbAutoUpdate.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool TestFolders()
        {
            bool errorFound = false;
            if (!Directory.Exists(txtBackupPath.Text))
            {
                txtBackupPath.BackColor = Color.Orange;
                errorFound = true;
            }
            else txtBackupPath.BackColor = Color.White;

            if (!Directory.Exists(txtExportConsoPath.Text))
            {
                txtExportConsoPath.BackColor = Color.Orange;
                errorFound = true;
            }
            else txtExportConsoPath.BackColor = Color.White;

            if (errorFound)
                MessageBox.Show("The folder does not exist, please select an existing folder","Error",MessageBoxButtons.OK);
            return (!errorFound);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void llHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://wiki.octopusnetwork.org");
        }

        private void buttonFindBackupPath_Click(object sender, EventArgs e)
        {
            fBDPath.ShowDialog();
            txtBackupPath.Text = fBDPath.SelectedPath;
        }

        private void buttonFindExportPath_Click(object sender, EventArgs e)
        {
            fBDPath.ShowDialog();
            txtExportConsoPath.Text = fBDPath.SelectedPath;
        }
    }
}