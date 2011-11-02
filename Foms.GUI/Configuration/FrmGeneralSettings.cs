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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Foms.CoreDomain.Online;
using Foms.Enums;
 
using Foms.ExceptionsHandler;
using Foms.ExceptionsHandler.Exceptions.GeneralSettingExceptions;
using Foms.GUI.UserControl;
using Foms.Services;
using Foms.Shared;
 

namespace Foms.GUI.Configuration
{
    /// <summary>
    /// Summary description for GeneralSettings.
    /// </summary>
    public class FrmGeneralSettings : Form
    {
        private Button buttonCancel;
        private DictionaryEntry entry;
        private IContainer components;
        private GroupBox groupBox1;
        private SplitContainer splitContainer1;
        private Button butImport;
        private Button butExport;
        private TabControl tabControlGeneralSettings;
        private TabPage tabPage1;
        private ListView listViewGeneralParameters;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private GroupBox groupBox3;
        private CheckedListBox checkedListBoxPendingSavings;
        private CheckedListBox checkedListBoxPendingRepayment;
        private ComboBox comboBoxSavings;
        private ComboBox comboBoxValue;
        private GroupBox groupBoxValue;
        private RadioButton radioButtonNo;
        private RadioButton radioButtonYes;
        private TextBox textBoxGeneralParameterValue;
        private TextBox textBoxGeneralParameterName;
        private Label label9;
        private Label label10;
        private Button buttonUpdate;
        private OpenFileDialog openFileDialog;
        
        public FrmGeneralSettings()
        {
            InitializeComponent();
            InitializeListViewGeneralParameters();

        }

       
        private void InitializeListViewGeneralParameters()
        {
            listViewGeneralParameters.Items.Clear();
            Hashtable settings = ServicesProvider.GetInstance().GetGeneralSettings().DbParamList;
            ArrayList keys = new ArrayList();
            keys.AddRange(settings.Keys);
            keys.Sort();
            foreach (object key in keys)
            {
                DictionaryEntry val = new DictionaryEntry(key, settings[key]);
                ListViewItem listViewItem = new ListViewItem(val.Key.ToString());
                if (val.Value != null)
                {
                    if (val.Key.ToString() == OGeneralSettings.DISABLEFUTUREREPAYMENTS ||
                        val.Key.ToString() == OGeneralSettings.CITYMANDATORY ||
                        val.Key.ToString() == OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE ||
                        val.Key.ToString() == OGeneralSettings.CITYOPENVALUE ||
                        val.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLELOANS ||
                        val.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLEGROUPS ||
                        val.Key.ToString() == OGeneralSettings.OLBBEFOREREPAYMENT ||
                        val.Key.ToString() == OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS ||
                        val.Key.ToString() == OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE ||
                        val.Key.ToString() == OGeneralSettings.USEPROJECTS ||
                        val.Key.ToString() == OGeneralSettings.ENFORCE_ID_PATTERN||
                        val.Key.ToString() == OGeneralSettings.ID_WILD_CHAR_CHECK ||
                        val.Key.ToString() == OGeneralSettings.INCREMENTALDURINGDAYOFF ||
                        val.Key.ToString() == OGeneralSettings.INTERESTS_ALSO_CREDITED_IN_FL ||
                        val.Key.ToString() == OGeneralSettings.CHECK_BIC_CODE)
                    {
                        listViewItem.SubItems.Add(val.Value.ToString().Trim() == "1" ? "True" : "False");
                    }
                    else if(val.Key.ToString() == OGeneralSettings.ACCOUNTINGPROCESS)
                    {
                        listViewItem.SubItems.Add(val.Value.ToString().Trim() == "1" ? "Cash" : "Accrual");
                    }
                    else
                        listViewItem.SubItems.Add(val.Value.ToString());
                }
                else
                    listViewItem.SubItems.Add("-");

                listViewItem.Tag = val;
                listViewGeneralParameters.Items.Add(listViewItem);
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneralSettings));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butImport = new System.Windows.Forms.Button();
            this.butExport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxGeneralParameterName = new System.Windows.Forms.TextBox();
            this.textBoxGeneralParameterValue = new System.Windows.Forms.TextBox();
            this.groupBoxValue = new System.Windows.Forms.GroupBox();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.comboBoxValue = new System.Windows.Forms.ComboBox();
            this.comboBoxSavings = new System.Windows.Forms.ComboBox();
            this.checkedListBoxPendingRepayment = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxPendingSavings = new System.Windows.Forms.CheckedListBox();
            this.listViewGeneralParameters = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlGeneralSettings = new System.Windows.Forms.TabControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxValue.SuspendLayout();
            this.tabControlGeneralSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlGeneralSettings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.butImport);
            this.groupBox1.Controls.Add(this.butExport);
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // butImport
            // 
            resources.ApplyResources(this.butImport, "butImport");
            this.butImport.BackColor = System.Drawing.Color.Gainsboro;
            this.butImport.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.butImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butImport.Image = global::Foms.GUI.Properties.Resources.theme1_1_import;
            this.butImport.Name = "butImport";
            this.butImport.UseVisualStyleBackColor = false;
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // butExport
            // 
            resources.ApplyResources(this.butExport, "butExport");
            this.butExport.BackColor = System.Drawing.Color.Gainsboro;
            this.butExport.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.butExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butExport.Image = global::Foms.GUI.Properties.Resources.theme1_1_export;
            this.butExport.Name = "butExport";
            this.butExport.UseVisualStyleBackColor = false;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.listViewGeneralParameters);
            this.tabPage1.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            this.groupBox3.Controls.Add(this.checkedListBoxPendingSavings);
            this.groupBox3.Controls.Add(this.checkedListBoxPendingRepayment);
            this.groupBox3.Controls.Add(this.comboBoxSavings);
            this.groupBox3.Controls.Add(this.comboBoxValue);
            this.groupBox3.Controls.Add(this.groupBoxValue);
            this.groupBox3.Controls.Add(this.textBoxGeneralParameterValue);
            this.groupBox3.Controls.Add(this.textBoxGeneralParameterName);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.buttonUpdate);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonUpdate.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonUpdate.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_save;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label9.Name = "label9";
            // 
            // textBoxGeneralParameterName
            // 
            resources.ApplyResources(this.textBoxGeneralParameterName, "textBoxGeneralParameterName");
            this.textBoxGeneralParameterName.Name = "textBoxGeneralParameterName";
            // 
            // textBoxGeneralParameterValue
            // 
            resources.ApplyResources(this.textBoxGeneralParameterValue, "textBoxGeneralParameterValue");
            this.textBoxGeneralParameterValue.Name = "textBoxGeneralParameterValue";
            this.textBoxGeneralParameterValue.TextChanged += new System.EventHandler(this.textBoxGeneralParameterValue_TextChanged);
            // 
            // groupBoxValue
            // 
            this.groupBoxValue.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxValue.Controls.Add(this.radioButtonNo);
            this.groupBoxValue.Controls.Add(this.radioButtonYes);
            resources.ApplyResources(this.groupBoxValue, "groupBoxValue");
            this.groupBoxValue.Name = "groupBoxValue";
            this.groupBoxValue.TabStop = false;
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.Checked = true;
            resources.ApplyResources(this.radioButtonYes, "radioButtonYes");
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.TabStop = true;
            // 
            // radioButtonNo
            // 
            resources.ApplyResources(this.radioButtonNo, "radioButtonNo");
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.CheckedChanged += new System.EventHandler(this.radioButtonNo_CheckedChanged);
            // 
            // comboBoxValue
            // 
            this.comboBoxValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValue.FormattingEnabled = true;
            this.comboBoxValue.Items.AddRange(new object[] {
            resources.GetString("comboBoxValue.Items"),
            resources.GetString("comboBoxValue.Items1")});
            resources.ApplyResources(this.comboBoxValue, "comboBoxValue");
            this.comboBoxValue.Name = "comboBoxValue";
            this.comboBoxValue.SelectionChangeCommitted += new System.EventHandler(this.comboBoxValue_SelectionChangeCommitted);
            // 
            // comboBoxSavings
            // 
            this.comboBoxSavings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSavings.FormattingEnabled = true;
            this.comboBoxSavings.Items.AddRange(new object[] {
            resources.GetString("comboBoxSavings.Items"),
            resources.GetString("comboBoxSavings.Items1")});
            resources.ApplyResources(this.comboBoxSavings, "comboBoxSavings");
            this.comboBoxSavings.Name = "comboBoxSavings";
            // 
            // checkedListBoxPendingRepayment
            // 
            this.checkedListBoxPendingRepayment.CheckOnClick = true;
            this.checkedListBoxPendingRepayment.FormattingEnabled = true;
            this.checkedListBoxPendingRepayment.Items.AddRange(new object[] {
            resources.GetString("checkedListBoxPendingRepayment.Items")});
            resources.ApplyResources(this.checkedListBoxPendingRepayment, "checkedListBoxPendingRepayment");
            this.checkedListBoxPendingRepayment.Name = "checkedListBoxPendingRepayment";
            // 
            // checkedListBoxPendingSavings
            // 
            this.checkedListBoxPendingSavings.CheckOnClick = true;
            this.checkedListBoxPendingSavings.FormattingEnabled = true;
            this.checkedListBoxPendingSavings.Items.AddRange(new object[] {
            resources.GetString("checkedListBoxPendingSavings.Items")});
            resources.ApplyResources(this.checkedListBoxPendingSavings, "checkedListBoxPendingSavings");
            this.checkedListBoxPendingSavings.Name = "checkedListBoxPendingSavings";
            // 
            // listViewGeneralParameters
            // 
            this.listViewGeneralParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            resources.ApplyResources(this.listViewGeneralParameters, "listViewGeneralParameters");
            this.listViewGeneralParameters.FullRowSelect = true;
            this.listViewGeneralParameters.GridLines = true;
            this.listViewGeneralParameters.MultiSelect = false;
            this.listViewGeneralParameters.Name = "listViewGeneralParameters";
            this.listViewGeneralParameters.UseCompatibleStateImageBehavior = false;
            this.listViewGeneralParameters.View = System.Windows.Forms.View.Details;
            this.listViewGeneralParameters.Click += new System.EventHandler(this.listViewGeneralParameters_Click);
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // tabControlGeneralSettings
            // 
            this.tabControlGeneralSettings.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControlGeneralSettings, "tabControlGeneralSettings");
            this.tabControlGeneralSettings.Multiline = true;
            this.tabControlGeneralSettings.Name = "tabControlGeneralSettings";
            this.tabControlGeneralSettings.SelectedIndex = 0;
            this.tabControlGeneralSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // FrmGeneralSettings
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmGeneralSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmGeneralSettings_FormClosed);
            this.Load += new System.EventHandler(this.FrmGeneralSettings_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxValue.ResumeLayout(false);
            this.tabControlGeneralSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

     
        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

      
        private void listViewGeneralParameters_Click(object sender, EventArgs e)
        {
            entry = (DictionaryEntry)listViewGeneralParameters.SelectedItems[0].Tag;
            InitializeControls();
            InitializeGeneralParameterValue();
        }

        private void InitializeControls()
        {
            if (entry.Key.ToString() == OGeneralSettings.DISABLEFUTUREREPAYMENTS ||
                entry.Key.ToString() == OGeneralSettings.CITYMANDATORY ||
                entry.Key.ToString() == OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE ||
                entry.Key.ToString() == OGeneralSettings.CITYOPENVALUE ||
                entry.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLELOANS ||
                entry.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLEGROUPS ||
                entry.Key.ToString() == OGeneralSettings.OLBBEFOREREPAYMENT ||
                entry.Key.ToString() == OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS ||
                entry.Key.ToString() == OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE ||
                entry.Key.ToString() == OGeneralSettings.USEPROJECTS ||
                entry.Key.ToString() == OGeneralSettings.ENFORCE_ID_PATTERN ||
                entry.Key.ToString() == OGeneralSettings.ID_WILD_CHAR_CHECK ||
                entry.Key.ToString() == OGeneralSettings.INCREMENTALDURINGDAYOFF ||
                entry.Key.ToString() == OGeneralSettings.INTERESTS_ALSO_CREDITED_IN_FL ||
                entry.Key.ToString() == OGeneralSettings.CHECK_BIC_CODE)
            {
                groupBoxValue.Visible = true;
                comboBoxValue.Visible = false;
                comboBoxSavings.Visible = false;
                textBoxGeneralParameterValue.Visible = false;
                checkedListBoxPendingRepayment.Visible = false;
                checkedListBoxPendingSavings.Visible = false;
            }
            else if(entry.Key.ToString() == OGeneralSettings.ACCOUNTINGPROCESS)
            {
                groupBoxValue.Visible = false;
                comboBoxValue.Visible = true;
                comboBoxSavings.Visible = false;
                textBoxGeneralParameterValue.Visible = false;
                checkedListBoxPendingRepayment.Visible = false;
                checkedListBoxPendingSavings.Visible = false;
            }
            else if (entry.Key.ToString() == OGeneralSettings.SAVINGS_CODE_TEMPLATE)
            {
                groupBoxValue.Visible = false;
                comboBoxValue.Visible = false;
                comboBoxSavings.Visible = true;
                textBoxGeneralParameterValue.Visible = false;
            }
            else if (entry.Key.ToString() == OGeneralSettings.PENDING_REPAYMENT_MODE)
            {
                groupBoxValue.Visible = false;
                comboBoxValue.Visible = false;
                comboBoxSavings.Visible = false;
                textBoxGeneralParameterValue.Visible = false;
                checkedListBoxPendingRepayment.Visible = true;
                checkedListBoxPendingSavings.Visible = false;
            }
            else if (entry.Key.ToString() == OGeneralSettings.PENDING_SAVINGS_MODE)
            {
                groupBoxValue.Visible = false;
                comboBoxValue.Visible = false;
                comboBoxSavings.Visible = false;
                textBoxGeneralParameterValue.Visible = false;
                checkedListBoxPendingRepayment.Visible = false;
                checkedListBoxPendingSavings.Visible = true;
            }
            else
            {
                groupBoxValue.Visible = false;
                comboBoxValue.Visible = false;
                comboBoxSavings.Visible = false;
                checkedListBoxPendingRepayment.Visible = false;
                checkedListBoxPendingSavings.Visible = false;
                textBoxGeneralParameterValue.Visible = true;
            }
            
        }

        private void InitializeGeneralParameterValue()
        {
            textBoxGeneralParameterName.Text = entry.Key.ToString();
            if (entry.Key.ToString() == OGeneralSettings.LATEDAYSAFTERACCRUALCEASES)
            {
                textBoxGeneralParameterValue.Text = entry.Value == null ? "-" : entry.Value.ToString();
                textBoxGeneralParameterValue.Enabled = true;
            }
            else if (entry.Value == null)
            {
                textBoxGeneralParameterValue.Text = String.Empty;
                textBoxGeneralParameterValue.Enabled = false;
            }
            else if (entry.Key.ToString() == OGeneralSettings.DISABLEFUTUREREPAYMENTS ||
                     entry.Key.ToString() == OGeneralSettings.CITYMANDATORY ||
                     entry.Key.ToString() == OGeneralSettings.PAYFIRSTINSTALLMENTREALVALUE ||
                     entry.Key.ToString() == OGeneralSettings.CITYOPENVALUE ||
                     entry.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLELOANS||
                     entry.Key.ToString() == OGeneralSettings.ALLOWSMULTIPLEGROUPS ||
                     entry.Key.ToString() == OGeneralSettings.OLBBEFOREREPAYMENT ||
                     entry.Key.ToString() == OGeneralSettings.CALCULATIONLATEFEESDURINGPUBLICHOLIDAYS ||
                     entry.Key.ToString() == OGeneralSettings.DONOTSKIPWEEKENDSININSTALLMENTSDATE ||
                     entry.Key.ToString() == OGeneralSettings.USEPROJECTS ||
                     entry.Key.ToString() == OGeneralSettings.ENFORCE_ID_PATTERN ||
                     entry.Key.ToString() == OGeneralSettings.ID_WILD_CHAR_CHECK || 
                     entry.Key.ToString() == OGeneralSettings.INCREMENTALDURINGDAYOFF ||
                     entry.Key.ToString() == OGeneralSettings.INTERESTS_ALSO_CREDITED_IN_FL ||
                     entry.Key.ToString() == OGeneralSettings.CHECK_BIC_CODE)
            {
                radioButtonYes.Checked = entry.Value.ToString() == "1";
                radioButtonNo.Checked = entry.Value.ToString() == "0";
            }
            else if(entry.Key.ToString() == OGeneralSettings.ACCOUNTINGPROCESS)
            {
                comboBoxValue.Text = entry.Value.ToString() == "1" ? "Cash" : "Accrual";
            }
            else if (entry.Key.ToString() == OGeneralSettings.SAVINGS_CODE_TEMPLATE)
            {
                comboBoxSavings.Text = entry.Value.ToString();
            }
            else if (entry.Key.ToString() == OGeneralSettings.PENDING_REPAYMENT_MODE)
            {
                foreach (int checkedIndice in checkedListBoxPendingRepayment.CheckedIndices)
                { checkedListBoxPendingRepayment.SetItemChecked(checkedIndice, false); }
                foreach (var item in entry.Value.ToString().Split(','))
                {
                    
                }
            }
            else if (entry.Key.ToString() == OGeneralSettings.PENDING_SAVINGS_MODE)
            {
                foreach (int checkedIndice in checkedListBoxPendingSavings.CheckedIndices)
                { checkedListBoxPendingSavings.SetItemChecked(checkedIndice, false); }
                foreach (var item in entry.Value.ToString().Split(','))
                {
                    
                }
            }
            else
            {
                textBoxGeneralParameterValue.Text = entry.Value.ToString();
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            ServicesProvider.GetInstance().GetGeneralSettings().UpdateParameter(entry.Key.ToString(), entry.Value);
            //ServicesProvider.GetInstance().GetApplicationSettingsServices().UpdateSelectedParameter(entry.Key.ToString(), entry.Value);
            InitializeListViewGeneralParameters();
        }

        private void textBoxGeneralParameterValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (entry.Key.ToString() == OGeneralSettings.BRANCHCODE)
                {
                    // Branch code can only contain chars
                    string value = textBoxGeneralParameterValue.Text;
                    for (int i = 0; i < value.Length; i++)
                    {
                        char c = value[i];
                        if (
                            !(((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')) ||
                              (c == '_')))
                        {
                            throw new GeneralSettingException(GeneralSettingEnumException.OnlyChar);
                        }
                        if (i == value.Length - 1)
                            entry.Value = textBoxGeneralParameterValue.Text;
                    }
                }
                if (entry.Key.ToString() == OGeneralSettings.BRANCHNAME)
                {
                    // Branch name can only contain chars
                    string value = textBoxGeneralParameterValue.Text;
                    for (int i = 0; i < value.Length; i++)
                    {
                        char c = value[i];
                        if (
                            !(((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')) ||
                              (c == '_')))
                        {
                            throw new GeneralSettingException(GeneralSettingEnumException.OnlyChar);
                        }
                        if (i == value.Length - 1)
                            entry.Value = textBoxGeneralParameterValue.Text;
                    }
                }
                else if (entry.Key.ToString() == OGeneralSettings.IMF_CODE)
                {
                    // Branch name can only contain chars
                    string value = textBoxGeneralParameterValue.Text;
                    for (int i = 0; i < value.Length; i++)
                    {
                        char c = value[i];
                        if (
                            !(((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || ((c >= '0') && (c <= '9')) ||
                              (c == '_')))
                        {
                            throw new GeneralSettingException(GeneralSettingEnumException.OnlyChar);
                        }
                        if (i == value.Length - 1)
                            entry.Value = textBoxGeneralParameterValue.Text;
                    }
                }

                else if (entry.Key.ToString() == OGeneralSettings.GROUPMINMEMBERS ||
                         entry.Key.ToString() == OGeneralSettings.WEEKENDDAY1 ||
                         entry.Key.ToString() == OGeneralSettings.WEEKENDDAY2 || 
                         entry.Key.ToString() == OGeneralSettings.CEASE_LAIE_DAYS)
                {
                    if (textBoxGeneralParameterValue.Text != String.Empty)
                    {
                        try
                        {
                            entry.Value = Convert.ToInt32(textBoxGeneralParameterValue.Text);
                        }
                        catch
                        {
                            throw new GeneralSettingException(GeneralSettingEnumException.OnlyInt);
                        }
                    }
                    else
                        entry.Value = null;
                }
                else if (entry.Key.ToString() == OGeneralSettings.LATEDAYSAFTERACCRUALCEASES)
                {
                    if (textBoxGeneralParameterValue.Text == @"-")
                        entry.Value = null;
                    else
                    {
                        try
                        {
                            entry.Value = Convert.ToInt32(textBoxGeneralParameterValue.Text);
                        }
                        catch
                        {
                            textBoxGeneralParameterValue.Text = @"-";
                            entry.Value = null;
                            throw new GeneralSettingException(GeneralSettingEnumException.OnlyIntAndUnderscore);
                        }
                    }
                }
                else
                {
                    entry.Value = textBoxGeneralParameterValue.Text;
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
                if (entry.Key.ToString() != OGeneralSettings.LATEDAYSAFTERACCRUALCEASES)
                {
                    textBoxGeneralParameterValue.Text = String.Empty;
                    entry.Value = String.Empty;
                }
            }
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
                entry.Value = true;
            else
                entry.Value = false;
        }


        private void butExport_Click(object sender, EventArgs e)
        {
           // Form frm = new frmSettingsImportExport();
       //     frm.ShowDialog();
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = @"Octopus Settings files|*.Settings";
            openFileDialog.FileName = @"Octopus.Settings";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
           //     Form frm = new frmSettingsImportExport(fileName);
           //     frm.ShowDialog();
            }
        }


        private void comboBoxValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            entry.Value = comboBoxValue.SelectedItem.ToString() == "Cash" ? 1: 2;
        }

        private void FrmGeneralSettings_Load(object sender, EventArgs e)
        {
          
            comboBoxValue.Location = new Point(96, 71);
        }

        private void FrmGeneralSettings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


    }
}