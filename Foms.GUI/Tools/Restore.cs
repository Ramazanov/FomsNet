//Foms MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
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
// Licence : http://www.Fomsnetwork.org/OverviewLicence.aspx
//
// Website : http://www.Fomsnetwork.org
// Business contact: business(at)Fomsnetwork.org
// Technical contact email : tech(at)Fomsnetwork.org 

using System;
using System.IO;
using System.Windows.Forms;
using Foms.GUI.UserControl;
using Foms.Shared;
using Foms.Shared.Settings;

namespace Foms.GUI.Tools
{
    /// <summary>
    /// Summary description for Restore.
    /// </summary>
    public class frmRestore : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.Label labelSelected;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelFullPath;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private System.ComponentModel.IContainer components;

        public frmRestore()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestore));
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.labelSelected = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelFullPath = new System.Windows.Forms.Label();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.AccessibleDescription = null;
            this.listViewFiles.AccessibleName = null;
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.BackgroundImage = null;
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDate,
            this.columnHeaderSize});
            this.listViewFiles.Font = null;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderDate
            // 
            resources.ApplyResources(this.columnHeaderDate, "columnHeaderDate");
            // 
            // columnHeaderSize
            // 
            resources.ApplyResources(this.columnHeaderSize, "columnHeaderSize");
            // 
            // labelSelected
            // 
            this.labelSelected.AccessibleDescription = null;
            this.labelSelected.AccessibleName = null;
            resources.ApplyResources(this.labelSelected, "labelSelected");
            this.labelSelected.Name = "labelSelected";
            // 
            // labelWarning
            // 
            this.labelWarning.AccessibleDescription = null;
            this.labelWarning.AccessibleName = null;
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.BackColor = System.Drawing.Color.Transparent;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // timerBlink
            // 
            this.timerBlink.Enabled = true;
            this.timerBlink.Interval = 500;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.AccessibleDescription = null;
            this.buttonBrowse.AccessibleName = null;
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonBrowse.Image = global::Foms.GUI.Properties.Resources.theme1_1_search;
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // labelFullPath
            // 
            this.labelFullPath.AccessibleDescription = null;
            this.labelFullPath.AccessibleName = null;
            resources.ApplyResources(this.labelFullPath, "labelFullPath");
            this.labelFullPath.Name = "labelFullPath";
            // 
            // buttonRestore
            // 
            this.buttonRestore.AccessibleDescription = null;
            this.buttonRestore.AccessibleName = null;
            resources.ApplyResources(this.buttonRestore, "buttonRestore");
            this.buttonRestore.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRestore.Image = global::Foms.GUI.Properties.Resources.theme1_1_restore_data;
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.UseVisualStyleBackColor = false;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AccessibleDescription = null;
            this.buttonCancel.AccessibleName = null;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.groupBox1.Controls.Add(this.buttonRestore);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = null;
            this.groupBox2.AccessibleName = null;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundImage = null;
            this.groupBox2.Controls.Add(this.labelFullPath);
            this.groupBox2.Controls.Add(this.labelSelected);
            this.groupBox2.Controls.Add(this.buttonBrowse);
            this.groupBox2.Font = null;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.AccessibleDescription = null;
            this.groupBox3.AccessibleName = null;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            this.groupBox3.Controls.Add(this.labelWarning);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.listViewFiles);
            this.groupBox3.Font = null;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // frmRestore
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRestore";
            this.Load += new System.EventHandler(this.Restore_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private static bool _blinkWarning = true;

        private void Restore_Load(object sender, System.EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(UserSettings.BackupPath);
            foreach (FileInfo fi in di.GetFiles("*.bak"))
            {
                AddFileToListView(fi);
            }
            foreach (FileInfo fi in di.GetFiles("*.zip"))
            {
                AddFileToListView(fi);
            }
            openFileDialog.InitialDirectory = UserSettings.BackupPath;
        }

        private void AddFileToListView(FileInfo fi)
        {
            ListViewItem item = listViewFiles.Items.Add(fi.Name.Replace(".bak", "").Replace(".bak.zip", "").Replace(".zip", ""));
            item.SubItems.Add(fi.CreationTime.ToString("yyyy-MM-dd"));
            item.SubItems.Add(String.Format("{0}KB", fi.Length / 1024));
            if (fi.Name.ToUpper().EndsWith(".ZIP")) item.SubItems.Add("ZIP");
            item.Tag = fi;
        }

        private void listViewFiles_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 1)
            {
                FileInfo selectedFile = (FileInfo)listViewFiles.SelectedItems[0].Tag;
                labelSelected.Text = selectedFile.Name;
                labelFullPath.Text = selectedFile.FullName;
                buttonRestore.Enabled = true;
            }
            else
            {
                labelSelected.Text = "";
                labelFullPath.Text = "";
                buttonRestore.Enabled = false;
            }
        }

        private void timerBlink_Tick(object sender, System.EventArgs e)
        {
            if (_blinkWarning)
            {
                labelWarning.Visible = true;
                timerBlink.Interval = 800;
            }
            else
            {
                labelWarning.Visible = false;
                timerBlink.Interval = 300;
            }
            _blinkWarning = !_blinkWarning;
        }

        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                labelSelected.Text = Path.GetFileName(fileName);
                labelFullPath.Text = fileName;
                buttonRestore.Enabled = true;
            }
        }

        private void buttonRestore_Click(object sender, System.EventArgs e)
        {
            DialogResult result = MessageBox.Show("confirm.Text", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                buttonRestore.Enabled = false;
                buttonCancel.Enabled = false;
                buttonBrowse.Enabled = false;
                labelWarning.Text = "restoring.Text";

                Cursor = Cursors.WaitCursor;
                timerBlink.Enabled = false;
                labelWarning.Visible = true;

                Application.DoEvents();

                //BackupManager backup = new BackupManager(new DatabaseManager(User.CurrentUser),User.CurrentUser);
                string backupFile = labelFullPath.Text;
                bool itsAZip = (backupFile.ToUpper().EndsWith(".ZIP"));
                if (itsAZip) backupFile = ZipHelper.UnzipFile(backupFile);
                //DatabaseServices.BackupOperationStatus status = ServicesProvider.GetInstance().GetDatabaseServices().LoadBackup(backupFile);
                //if (itsAZip) File.Delete(backupFile);
                //if (status.Succeeded)
                //{
                //    labelWarning.Text = "";
                //    MessageBox.Show("success.Text", "", MessageBoxButtons.OK);
                //    Restart.LaunchRestarter();
                //}
                //else
                //{
                //    MessageBox.Show("failed.Text" + "\n\n" + status.RaisedException.Message, "", MessageBoxButtons.OK);
                //    Cursor = Cursors.Default;
                //    buttonRestore.Enabled = true;
                //    buttonCancel.Enabled = true;
                //    buttonBrowse.Enabled = true;
                //}
            }
        }
    }
}
