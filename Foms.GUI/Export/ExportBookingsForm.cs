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
using System.Data;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Foms.GUI.UserControl;
using Foms.Services;
using Foms.Shared;


namespace Foms.GUI
{
    using System.IO;

    /// <summary>
    /// Summary description for ExportTransactions.
    /// </summary>
    public class ExportBookingsForm : Form
    {
        private GroupBox groupBoxExit;
        private SweetButton btnExport;
        private SweetButton btnClose;
        private Label labelTitleRepayment;
        private ListView listViewTransactionsList;
        private TableLayoutPanel tableLayoutPanel1;
        private BackgroundWorker _bwExportWorker;
        private BackgroundWorker _bwExportToFile;
        private BackgroundWorker _bwSelect;
        private ProgressBar progressBarExport;
        private SweetButton btnSelectAll;
        private SweetButton btnDeselectAll;
        private Label labelSlash;
        private Label labelSelected;
        private Label labelTotal;
        private int _selected;
        private int _total;
        private DataTable _dataTable;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        public ExportBookingsForm()
        {
            _selected = _total = 0;
            InitializeComponent();
            _bwSelect = new BackgroundWorker();
            _bwSelect.WorkerSupportsCancellation = true;
            _bwSelect.DoWork += BwSelect_DoWork;
        }

        private void Initialization()
        { 
         //   _dataTable = ServicesProvider.GetInstance().GetAccountingServices().FindElementaryMvtsToExport();
            _total = 0;
        }

        private void InitializeListView()
        {
            listViewTransactionsList.Invoke(new MethodInvoker(
                delegate
                    {
                        listViewTransactionsList.Items.Clear(); 
                        listViewTransactionsList.Visible = false;
                        if (_dataTable != null)
                        {
                            listViewTransactionsList.Columns.Add("Sr #", 60);
                            foreach (DataColumn column in _dataTable.Columns)
                                listViewTransactionsList.Columns.Add(column.ColumnName, 120);

                            for (int i = 0; i <= _dataTable.Rows.Count-1; i++)
                            {
                                ListViewItem item = new ListViewItem((i + 1).ToString());
                                //item.SubItems.Add((i+1).ToString());
                                item.SubItems.AddRange(Array.ConvertAll(_dataTable.Rows[i].ItemArray, p => p.ToString()));
                                item.Tag = _dataTable.Rows[i];
                                listViewTransactionsList.Items.Add(item);
                            }
                        }
                    }
            ));
        }

        public void BwExportWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewTransactionsList.Invoke(new MethodInvoker(delegate { listViewTransactionsList.Visible = true; } ));
            progressBarExport.Invoke(new MethodInvoker(delegate
               {
                   progressBarExport.Visible = false;
                   progressBarExport.Value = 0;
               }
            ));
            Activate();
            if (e.Result != null)
            {
                if (e.Result.Equals("success"))
                {
                    btnExport.Invoke(new MethodInvoker(delegate { btnExport.Enabled = true; }));
                    btnSelectAll.Invoke(new MethodInvoker(delegate { btnSelectAll.Enabled = true; }));
                    btnDeselectAll.Invoke(new MethodInvoker(delegate { btnDeselectAll.Enabled = true; }));
                }
            }
            else
            {
               // MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "NotFound.Text"));
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportBookingsForm));
            this.groupBoxExit = new System.Windows.Forms.GroupBox();
            this.labelSlash = new System.Windows.Forms.Label();
            this.labelSelected = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.btnDeselectAll = new Foms.GUI.UserControl.SweetButton();
            this.btnSelectAll = new Foms.GUI.UserControl.SweetButton();
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.btnExport = new Foms.GUI.UserControl.SweetButton();
            this.btnClose = new Foms.GUI.UserControl.SweetButton();
            this.listViewTransactionsList = new System.Windows.Forms.ListView();
            this.labelTitleRepayment = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxExit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxExit
            // 
            this.groupBoxExit.BackColor = System.Drawing.Color.White;
            this.groupBoxExit.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.groupBoxExit, "groupBoxExit");
            this.groupBoxExit.Controls.Add(this.labelSlash);
            this.groupBoxExit.Controls.Add(this.labelSelected);
            this.groupBoxExit.Controls.Add(this.labelTotal);
            this.groupBoxExit.Controls.Add(this.btnDeselectAll);
            this.groupBoxExit.Controls.Add(this.btnSelectAll);
            this.groupBoxExit.Controls.Add(this.progressBarExport);
            this.groupBoxExit.Controls.Add(this.btnExport);
            this.groupBoxExit.Name = "groupBoxExit";
            this.groupBoxExit.TabStop = false;
            // 
            // labelSlash
            // 
            resources.ApplyResources(this.labelSlash, "labelSlash");
            this.labelSlash.BackColor = System.Drawing.Color.Transparent;
            this.labelSlash.Name = "labelSlash";
            // 
            // labelSelected
            // 
            resources.ApplyResources(this.labelSelected, "labelSelected");
            this.labelSelected.BackColor = System.Drawing.Color.Transparent;
            this.labelSelected.Name = "labelSelected";
            // 
            // labelTotal
            // 
            resources.ApplyResources(this.labelTotal, "labelTotal");
            this.labelTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelTotal.Name = "labelTotal";
            // 
            // btnDeselectAll
            // 
            resources.ApplyResources(this.btnDeselectAll, "btnDeselectAll");
            this.btnDeselectAll.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnDeselectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeselectAll.Menu = null;
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSelectAll.Icon = Foms.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnSelectAll.Menu = null;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // progressBarExport
            // 
            resources.ApplyResources(this.progressBarExport, "progressBarExport");
            this.progressBarExport.Name = "progressBarExport";
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.BackColor = System.Drawing.Color.Gainsboro;
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnExport.Icon = Foms.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnExport.Image = global::Foms.GUI.Properties.Resources.theme1_1_export;
            this.btnExport.Menu = null;
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnClose.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Menu = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // listViewTransactionsList
            // 
            this.listViewTransactionsList.CheckBoxes = true;
            resources.ApplyResources(this.listViewTransactionsList, "listViewTransactionsList");
            this.listViewTransactionsList.FullRowSelect = true;
            this.listViewTransactionsList.GridLines = true;
            this.listViewTransactionsList.Name = "listViewTransactionsList";
            this.listViewTransactionsList.UseCompatibleStateImageBehavior = false;
            this.listViewTransactionsList.View = System.Windows.Forms.View.Details;
            this.listViewTransactionsList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewTransactionsList_ItemChecked);
            // 
            // labelTitleRepayment
            // 
            this.labelTitleRepayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.labelTitleRepayment, "labelTitleRepayment");
            this.labelTitleRepayment.ForeColor = System.Drawing.Color.White;
            this.labelTitleRepayment.Image = global::Foms.GUI.Properties.Resources.theme1_1_pastille_option;
            this.labelTitleRepayment.Name = "labelTitleRepayment";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.labelTitleRepayment, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listViewTransactionsList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxExit, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ExportBookingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExportBookingsForm";
            this.Load += new System.EventHandler(this.ExportBookings_Load);
            this.groupBoxExit.ResumeLayout(false);
            this.groupBoxExit.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void buttonExport_Click(object sender, EventArgs e)
        {		   
            progressBarExport.Visible = true;
            btnSelectAll.Enabled = false; 
            btnDeselectAll.Enabled = false;
            progressBarExport.Maximum = listViewTransactionsList.Items.Count;
            progressBarExport.Step = 1;
            progressBarExport.Minimum = 0;
            progressBarExport.Value = 0;
            string fileName = ExportFile._SaveTextToNewPath();
            if (string.IsNullOrEmpty(fileName))
                return;
            _bwExportToFile = new BackgroundWorker {WorkerReportsProgress = true};
            _bwExportToFile.WorkerSupportsCancellation = true;
            _bwExportToFile.DoWork += BwExportToFile_DoWork;
            _bwExportToFile.RunWorkerCompleted += BwExportToFile_WorkCompleted;
            btnExport.Enabled = false;

            _bwExportToFile.RunWorkerAsync(fileName);

        }

        private void BwExportToFile_WorkCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               progressBarExport.Visible = false;
                                                               progressBarExport.Minimum = 0;
                                                               progressBarExport.Maximum = 100;
                                                               progressBarExport.Value = 5;
                                                           }
                                         ));

            if(args.Error!=null || args.Cancelled)
            {
                //MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "ExportCancelled.Text") + @"   " + args.Error);
                return;
            } 

           // MessageBox.Show(MultiLanguageStrings.GetString(Ressource.ExportBookingsForm, "ExportFinished.Text"));
            Invoke(new MethodInvoker(delegate { _bwExportWorker.RunWorkerAsync(); }));
        }

        private void BwExportToFile_DoWork(object sender, DoWorkEventArgs args)
        {
            ListView.ListViewItemCollection _items = null;
            listViewTransactionsList.Invoke(new MethodInvoker(delegate { _items = listViewTransactionsList.Items; } ));

            progressBarExport.Invoke(new MethodInvoker(delegate
            {
                progressBarExport.Minimum = 0;
                progressBarExport.Value = 0;
                progressBarExport.Step = 1;
                progressBarExport.Maximum = 2;
            }
            ));

            StreamWriter writer = new StreamWriter(args.Argument.ToString(), false, System.Text.Encoding.Unicode);

            for (int i = 0; i < _items.Count; i++)
            {
                progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
                bool isChecked = false;
                listViewTransactionsList.Invoke(new MethodInvoker(delegate { isChecked = _items[i].Checked; }));

                if (isChecked)
                {
                    DataRow curRow = null;
                    listViewTransactionsList.Invoke(new MethodInvoker(delegate { curRow = (DataRow)_items[i].Tag; }));

                    string[] arr = new String[curRow.Table.Columns.Count];
                    for (int j = 0; j < curRow.Table.Columns.Count; j++)
                        arr[j] = curRow.ItemArray[j].ToString();

                    writer.WriteLine(string.Join(";", arr));
                }
            }
            writer.Close();

            progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
            progressBarExport.Invoke(new MethodInvoker(delegate { progressBarExport.PerformStep(); } ));
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (_bwExportWorker!=null) 
                if (_bwExportWorker.IsBusy)
                    _bwExportWorker.CancelAsync();
            if(_bwExportToFile!=null)
                if(_bwExportToFile.IsBusy)
                    _bwExportToFile.CancelAsync();
            Close();
        }

        private void ExportBookings_Load(object sender, EventArgs e)
        {
            progressBarExport.Value = 5;
            progressBarExport.Maximum = 100;
            btnExport.Enabled = btnSelectAll.Enabled  = btnDeselectAll.Enabled =  false; 

            _bwExportWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _bwExportWorker.WorkerSupportsCancellation = true;
            _bwExportWorker.DoWork += BwExportWorker_DoWork;
            _bwExportWorker.RunWorkerCompleted += BwExportWorker_WorkCompleted;
            _bwExportWorker.RunWorkerAsync();
        }

        private void BwExportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Invoke(new MethodInvoker(delegate {
                                                  Initialization();
                                                  labelTotal.Text = _total.ToString();
                                                  labelSelected.Text = "0";
                                                  progressBarExport.Value = 15;
                                              }));
            InitializeListView();
            string isSuccess = "failure";
            listViewTransactionsList.Invoke(new MethodInvoker(delegate
                { isSuccess = listViewTransactionsList.Items.Count > 0 ? "success" : "failure"; }));
            e.Result = isSuccess;
            _bwExportWorker.ReportProgress(100, e.Result);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            progressBarExport.Minimum = 0;
            progressBarExport.Value = 0;
            progressBarExport.Step = 1;
            progressBarExport.Maximum = listViewTransactionsList.Items.Count;
            progressBarExport.Visible = true;

            _bwSelect.RunWorkerAsync("1");
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            progressBarExport.Minimum = 0;
            progressBarExport.Value = 0;
            progressBarExport.Step = 1;
            progressBarExport.Maximum = listViewTransactionsList.Items.Count;
            progressBarExport.Visible = true;
            _bwSelect.RunWorkerAsync("0");
        }

        private void BwSelect_DoWork(object sender, DoWorkEventArgs e)
        {
            bool toSelect = e.Argument.Equals("1") ? true : false;
            int counter = 0;
            _selected = 0;

            listViewTransactionsList.Invoke(new MethodInvoker(delegate{counter = listViewTransactionsList.Items.Count;}));
            for (int i = 0; i < counter; i++)
            {
                listViewTransactionsList.Invoke(new MethodInvoker(delegate{listViewTransactionsList.Items[i].Checked = toSelect; _selected++;} ));

                progressBarExport.Invoke(new MethodInvoker(delegate {progressBarExport.PerformStep();}));
            }

            listViewTransactionsList.Invalidate();
            progressBarExport.Invoke(new MethodInvoker(delegate
                                                           {
                                                               progressBarExport.Visible = false;
                                                               labelSelected.Text = toSelect
                                                                                        ? _selected.ToString()
                                                                                        : "0";

                                                           }
                                         ));

        }

        private void listViewTransactionsList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(e.Item.Checked)
                _selected++;

            else
                _selected--;
            _selected = _selected < 0 ? 0 : _selected;
            labelSelected.Text = _selected.ToString();
        }
    }
}