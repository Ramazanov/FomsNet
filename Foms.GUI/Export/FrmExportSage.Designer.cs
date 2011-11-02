namespace Foms.GUI.Accounting.Sage
{
    partial class FrmExportSage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportSage));
            this.splitContainerSage = new System.Windows.Forms.SplitContainer();
            this.tabControlExportations = new System.Windows.Forms.TabControl();
            this.tabPageAccountTiers = new System.Windows.Forms.TabPage();
            this.splitContainerAccountTiers = new System.Windows.Forms.SplitContainer();
            this.dateTimePickerEndDateAccountTiers = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBeginDateAccountTiers = new System.Windows.Forms.DateTimePicker();
            this.labelBeginDateAccountTiers = new System.Windows.Forms.Label();
            this.labelEndDateAccountTiers = new System.Windows.Forms.Label();
            this.groupBoxExportAccountsTiers = new System.Windows.Forms.GroupBox();
            this.labelSlashAccountTiers = new System.Windows.Forms.Label();
            this.labelSelectedAccountTiers = new System.Windows.Forms.Label();
            this.labelTotalAccountTiers = new System.Windows.Forms.Label();
            this.progressBarExport = new System.Windows.Forms.ProgressBar();
            this.listViewAccountsTiers = new System.Windows.Forms.ListView();
            this.columnHeaderContractCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClientType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderClientName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLoanOfficer = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDisbursedDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAccountTiers = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAccountTiersTitle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCollectiveAccount = new System.Windows.Forms.ColumnHeader();
            this.tabPageBookings = new System.Windows.Forms.TabPage();
            this.splitContainerBookings = new System.Windows.Forms.SplitContainer();
            this.checkBoxShowExportedBookings = new System.Windows.Forms.CheckBox();
            this.dateTimePickerEndDateBookings = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBeginDateBookings = new System.Windows.Forms.DateTimePicker();
            this.labelBeginDateBookings = new System.Windows.Forms.Label();
            this.labelEndDateBookings = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSlashBookings = new System.Windows.Forms.Label();
            this.labelSelectedBookings = new System.Windows.Forms.Label();
            this.labelTotalBookings = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listViewBookings = new System.Windows.Forms.ListView();
            this.columnHeaderBookingJournalCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingContractCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingClientName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingAccountTiers = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingAccount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingAmount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingPartNumber = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingReference = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBookingDirection = new System.Windows.Forms.ColumnHeader();
            this.saveFileDialogAccountTiers = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogBookings = new System.Windows.Forms.SaveFileDialog();
            this._buttonExit = new System.Windows.Forms.Button();
            this._labelTitle = new System.Windows.Forms.Label();
            this.buttonRefreshAccountTiers = new System.Windows.Forms.Button();
            this.btnDeselectAllAccountTiers = new System.Windows.Forms.Button();
            this.btnSelectAllAccountTiers = new System.Windows.Forms.Button();
            this.buttonExportAccountTiers = new System.Windows.Forms.Button();
            this.buttonRefreshBookings = new System.Windows.Forms.Button();
            this.buttonDeselectAllBookings = new System.Windows.Forms.Button();
            this.buttonSelectAllBookings = new System.Windows.Forms.Button();
            this.buttonExportBookings = new System.Windows.Forms.Button();
            this.splitContainerSage.Panel1.SuspendLayout();
            this.splitContainerSage.Panel2.SuspendLayout();
            this.splitContainerSage.SuspendLayout();
            this.tabControlExportations.SuspendLayout();
            this.tabPageAccountTiers.SuspendLayout();
            this.splitContainerAccountTiers.Panel1.SuspendLayout();
            this.splitContainerAccountTiers.Panel2.SuspendLayout();
            this.splitContainerAccountTiers.SuspendLayout();
            this.groupBoxExportAccountsTiers.SuspendLayout();
            this.tabPageBookings.SuspendLayout();
            this.splitContainerBookings.Panel1.SuspendLayout();
            this.splitContainerBookings.Panel2.SuspendLayout();
            this.splitContainerBookings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerSage
            // 
            this.splitContainerSage.AccessibleDescription = null;
            this.splitContainerSage.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage, "splitContainerSage");
            this.splitContainerSage.BackgroundImage = null;
            this.splitContainerSage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerSage.Font = null;
            this.splitContainerSage.Name = "splitContainerSage";
            // 
            // splitContainerSage.Panel1
            // 
            this.splitContainerSage.Panel1.AccessibleDescription = null;
            this.splitContainerSage.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage.Panel1, "splitContainerSage.Panel1");
            this.splitContainerSage.Panel1.BackgroundImage = null;
            this.splitContainerSage.Panel1.Controls.Add(this._buttonExit);
            this.splitContainerSage.Panel1.Controls.Add(this._labelTitle);
            this.splitContainerSage.Panel1.Font = null;
            // 
            // splitContainerSage.Panel2
            // 
            this.splitContainerSage.Panel2.AccessibleDescription = null;
            this.splitContainerSage.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerSage.Panel2, "splitContainerSage.Panel2");
            this.splitContainerSage.Panel2.BackgroundImage = null;
            this.splitContainerSage.Panel2.Controls.Add(this.tabControlExportations);
            this.splitContainerSage.Panel2.Font = null;
            // 
            // tabControlExportations
            // 
            this.tabControlExportations.AccessibleDescription = null;
            this.tabControlExportations.AccessibleName = null;
            resources.ApplyResources(this.tabControlExportations, "tabControlExportations");
            this.tabControlExportations.BackgroundImage = null;
            this.tabControlExportations.Controls.Add(this.tabPageAccountTiers);
            this.tabControlExportations.Controls.Add(this.tabPageBookings);
            this.tabControlExportations.Font = null;
            this.tabControlExportations.Name = "tabControlExportations";
            this.tabControlExportations.SelectedIndex = 0;
            // 
            // tabPageAccountTiers
            // 
            this.tabPageAccountTiers.AccessibleDescription = null;
            this.tabPageAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.tabPageAccountTiers, "tabPageAccountTiers");
            this.tabPageAccountTiers.BackgroundImage = null;
            this.tabPageAccountTiers.Controls.Add(this.splitContainerAccountTiers);
            this.tabPageAccountTiers.Font = null;
            this.tabPageAccountTiers.Name = "tabPageAccountTiers";
            this.tabPageAccountTiers.UseVisualStyleBackColor = true;
            // 
            // splitContainerAccountTiers
            // 
            this.splitContainerAccountTiers.AccessibleDescription = null;
            this.splitContainerAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers, "splitContainerAccountTiers");
            this.splitContainerAccountTiers.BackgroundImage = null;
            this.splitContainerAccountTiers.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerAccountTiers.Font = null;
            this.splitContainerAccountTiers.Name = "splitContainerAccountTiers";
            // 
            // splitContainerAccountTiers.Panel1
            // 
            this.splitContainerAccountTiers.Panel1.AccessibleDescription = null;
            this.splitContainerAccountTiers.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers.Panel1, "splitContainerAccountTiers.Panel1");
            this.splitContainerAccountTiers.Panel1.BackgroundImage = null;
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.dateTimePickerEndDateAccountTiers);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.dateTimePickerBeginDateAccountTiers);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.labelBeginDateAccountTiers);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.labelEndDateAccountTiers);
            this.splitContainerAccountTiers.Panel1.Controls.Add(this.buttonRefreshAccountTiers);
            this.splitContainerAccountTiers.Panel1.Font = null;
            // 
            // splitContainerAccountTiers.Panel2
            // 
            this.splitContainerAccountTiers.Panel2.AccessibleDescription = null;
            this.splitContainerAccountTiers.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAccountTiers.Panel2, "splitContainerAccountTiers.Panel2");
            this.splitContainerAccountTiers.Panel2.BackgroundImage = null;
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.groupBoxExportAccountsTiers);
            this.splitContainerAccountTiers.Panel2.Controls.Add(this.listViewAccountsTiers);
            this.splitContainerAccountTiers.Panel2.Font = null;
            // 
            // dateTimePickerEndDateAccountTiers
            // 
            this.dateTimePickerEndDateAccountTiers.AccessibleDescription = null;
            this.dateTimePickerEndDateAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerEndDateAccountTiers, "dateTimePickerEndDateAccountTiers");
            this.dateTimePickerEndDateAccountTiers.BackgroundImage = null;
            this.dateTimePickerEndDateAccountTiers.CalendarFont = null;
            this.dateTimePickerEndDateAccountTiers.CustomFormat = null;
            this.dateTimePickerEndDateAccountTiers.Name = "dateTimePickerEndDateAccountTiers";
            // 
            // dateTimePickerBeginDateAccountTiers
            // 
            this.dateTimePickerBeginDateAccountTiers.AccessibleDescription = null;
            this.dateTimePickerBeginDateAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerBeginDateAccountTiers, "dateTimePickerBeginDateAccountTiers");
            this.dateTimePickerBeginDateAccountTiers.BackgroundImage = null;
            this.dateTimePickerBeginDateAccountTiers.CalendarFont = null;
            this.dateTimePickerBeginDateAccountTiers.CustomFormat = null;
            this.dateTimePickerBeginDateAccountTiers.Name = "dateTimePickerBeginDateAccountTiers";
            // 
            // labelBeginDateAccountTiers
            // 
            this.labelBeginDateAccountTiers.AccessibleDescription = null;
            this.labelBeginDateAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.labelBeginDateAccountTiers, "labelBeginDateAccountTiers");
            this.labelBeginDateAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelBeginDateAccountTiers.Name = "labelBeginDateAccountTiers";
            // 
            // labelEndDateAccountTiers
            // 
            this.labelEndDateAccountTiers.AccessibleDescription = null;
            this.labelEndDateAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.labelEndDateAccountTiers, "labelEndDateAccountTiers");
            this.labelEndDateAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelEndDateAccountTiers.Name = "labelEndDateAccountTiers";
            // 
            // groupBoxExportAccountsTiers
            // 
            this.groupBoxExportAccountsTiers.AccessibleDescription = null;
            this.groupBoxExportAccountsTiers.AccessibleName = null;
            resources.ApplyResources(this.groupBoxExportAccountsTiers, "groupBoxExportAccountsTiers");
            this.groupBoxExportAccountsTiers.BackgroundImage = null;
            this.groupBoxExportAccountsTiers.Controls.Add(this.labelSlashAccountTiers);
            this.groupBoxExportAccountsTiers.Controls.Add(this.labelSelectedAccountTiers);
            this.groupBoxExportAccountsTiers.Controls.Add(this.labelTotalAccountTiers);
            this.groupBoxExportAccountsTiers.Controls.Add(this.btnDeselectAllAccountTiers);
            this.groupBoxExportAccountsTiers.Controls.Add(this.btnSelectAllAccountTiers);
            this.groupBoxExportAccountsTiers.Controls.Add(this.progressBarExport);
            this.groupBoxExportAccountsTiers.Controls.Add(this.buttonExportAccountTiers);
            this.groupBoxExportAccountsTiers.Name = "groupBoxExportAccountsTiers";
            this.groupBoxExportAccountsTiers.TabStop = false;
            // 
            // labelSlashAccountTiers
            // 
            this.labelSlashAccountTiers.AccessibleDescription = null;
            this.labelSlashAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.labelSlashAccountTiers, "labelSlashAccountTiers");
            this.labelSlashAccountTiers.Font = null;
            this.labelSlashAccountTiers.Name = "labelSlashAccountTiers";
            // 
            // labelSelectedAccountTiers
            // 
            this.labelSelectedAccountTiers.AccessibleDescription = null;
            this.labelSelectedAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.labelSelectedAccountTiers, "labelSelectedAccountTiers");
            this.labelSelectedAccountTiers.Font = null;
            this.labelSelectedAccountTiers.Name = "labelSelectedAccountTiers";
            // 
            // labelTotalAccountTiers
            // 
            this.labelTotalAccountTiers.AccessibleDescription = null;
            this.labelTotalAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.labelTotalAccountTiers, "labelTotalAccountTiers");
            this.labelTotalAccountTiers.Font = null;
            this.labelTotalAccountTiers.Name = "labelTotalAccountTiers";
            // 
            // progressBarExport
            // 
            this.progressBarExport.AccessibleDescription = null;
            this.progressBarExport.AccessibleName = null;
            resources.ApplyResources(this.progressBarExport, "progressBarExport");
            this.progressBarExport.BackgroundImage = null;
            this.progressBarExport.Font = null;
            this.progressBarExport.Name = "progressBarExport";
            // 
            // listViewAccountsTiers
            // 
            this.listViewAccountsTiers.AccessibleDescription = null;
            this.listViewAccountsTiers.AccessibleName = null;
            resources.ApplyResources(this.listViewAccountsTiers, "listViewAccountsTiers");
            this.listViewAccountsTiers.BackgroundImage = null;
            this.listViewAccountsTiers.CheckBoxes = true;
            this.listViewAccountsTiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderContractCode,
            this.columnHeaderClientType,
            this.columnHeaderClientName,
            this.columnHeaderLoanOfficer,
            this.columnHeaderDisbursedDate,
            this.columnHeaderAccountTiers,
            this.columnHeaderAccountTiersTitle,
            this.columnHeaderCollectiveAccount});
            this.listViewAccountsTiers.Font = null;
            this.listViewAccountsTiers.FullRowSelect = true;
            this.listViewAccountsTiers.GridLines = true;
            this.listViewAccountsTiers.Name = "listViewAccountsTiers";
            this.listViewAccountsTiers.UseCompatibleStateImageBehavior = false;
            this.listViewAccountsTiers.View = System.Windows.Forms.View.Details;
            this.listViewAccountsTiers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewAccountsTiers_ItemChecked);
            // 
            // columnHeaderContractCode
            // 
            resources.ApplyResources(this.columnHeaderContractCode, "columnHeaderContractCode");
            // 
            // columnHeaderClientType
            // 
            resources.ApplyResources(this.columnHeaderClientType, "columnHeaderClientType");
            // 
            // columnHeaderClientName
            // 
            resources.ApplyResources(this.columnHeaderClientName, "columnHeaderClientName");
            // 
            // columnHeaderLoanOfficer
            // 
            resources.ApplyResources(this.columnHeaderLoanOfficer, "columnHeaderLoanOfficer");
            // 
            // columnHeaderDisbursedDate
            // 
            resources.ApplyResources(this.columnHeaderDisbursedDate, "columnHeaderDisbursedDate");
            // 
            // columnHeaderAccountTiers
            // 
            resources.ApplyResources(this.columnHeaderAccountTiers, "columnHeaderAccountTiers");
            // 
            // columnHeaderAccountTiersTitle
            // 
            resources.ApplyResources(this.columnHeaderAccountTiersTitle, "columnHeaderAccountTiersTitle");
            // 
            // columnHeaderCollectiveAccount
            // 
            resources.ApplyResources(this.columnHeaderCollectiveAccount, "columnHeaderCollectiveAccount");
            // 
            // tabPageBookings
            // 
            this.tabPageBookings.AccessibleDescription = null;
            this.tabPageBookings.AccessibleName = null;
            resources.ApplyResources(this.tabPageBookings, "tabPageBookings");
            this.tabPageBookings.BackgroundImage = null;
            this.tabPageBookings.Controls.Add(this.splitContainerBookings);
            this.tabPageBookings.Font = null;
            this.tabPageBookings.Name = "tabPageBookings";
            this.tabPageBookings.UseVisualStyleBackColor = true;
            // 
            // splitContainerBookings
            // 
            this.splitContainerBookings.AccessibleDescription = null;
            this.splitContainerBookings.AccessibleName = null;
            resources.ApplyResources(this.splitContainerBookings, "splitContainerBookings");
            this.splitContainerBookings.BackgroundImage = null;
            this.splitContainerBookings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerBookings.Font = null;
            this.splitContainerBookings.Name = "splitContainerBookings";
            // 
            // splitContainerBookings.Panel1
            // 
            this.splitContainerBookings.Panel1.AccessibleDescription = null;
            this.splitContainerBookings.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerBookings.Panel1, "splitContainerBookings.Panel1");
            this.splitContainerBookings.Panel1.BackgroundImage = null;
            this.splitContainerBookings.Panel1.Controls.Add(this.checkBoxShowExportedBookings);
            this.splitContainerBookings.Panel1.Controls.Add(this.dateTimePickerEndDateBookings);
            this.splitContainerBookings.Panel1.Controls.Add(this.dateTimePickerBeginDateBookings);
            this.splitContainerBookings.Panel1.Controls.Add(this.labelBeginDateBookings);
            this.splitContainerBookings.Panel1.Controls.Add(this.labelEndDateBookings);
            this.splitContainerBookings.Panel1.Controls.Add(this.buttonRefreshBookings);
            this.splitContainerBookings.Panel1.Font = null;
            // 
            // splitContainerBookings.Panel2
            // 
            this.splitContainerBookings.Panel2.AccessibleDescription = null;
            this.splitContainerBookings.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerBookings.Panel2, "splitContainerBookings.Panel2");
            this.splitContainerBookings.Panel2.BackgroundImage = null;
            this.splitContainerBookings.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerBookings.Panel2.Controls.Add(this.listViewBookings);
            this.splitContainerBookings.Panel2.Font = null;
            // 
            // checkBoxShowExportedBookings
            // 
            this.checkBoxShowExportedBookings.AccessibleDescription = null;
            this.checkBoxShowExportedBookings.AccessibleName = null;
            resources.ApplyResources(this.checkBoxShowExportedBookings, "checkBoxShowExportedBookings");
            this.checkBoxShowExportedBookings.BackgroundImage = null;
            this.checkBoxShowExportedBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.checkBoxShowExportedBookings.Name = "checkBoxShowExportedBookings";
            this.checkBoxShowExportedBookings.UseVisualStyleBackColor = true;
            this.checkBoxShowExportedBookings.CheckedChanged += new System.EventHandler(this.checkBoxShowExportedBookings_CheckedChanged);
            // 
            // dateTimePickerEndDateBookings
            // 
            this.dateTimePickerEndDateBookings.AccessibleDescription = null;
            this.dateTimePickerEndDateBookings.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerEndDateBookings, "dateTimePickerEndDateBookings");
            this.dateTimePickerEndDateBookings.BackgroundImage = null;
            this.dateTimePickerEndDateBookings.CalendarFont = null;
            this.dateTimePickerEndDateBookings.CustomFormat = null;
            this.dateTimePickerEndDateBookings.Name = "dateTimePickerEndDateBookings";
            // 
            // dateTimePickerBeginDateBookings
            // 
            this.dateTimePickerBeginDateBookings.AccessibleDescription = null;
            this.dateTimePickerBeginDateBookings.AccessibleName = null;
            resources.ApplyResources(this.dateTimePickerBeginDateBookings, "dateTimePickerBeginDateBookings");
            this.dateTimePickerBeginDateBookings.BackgroundImage = null;
            this.dateTimePickerBeginDateBookings.CalendarFont = null;
            this.dateTimePickerBeginDateBookings.CustomFormat = null;
            this.dateTimePickerBeginDateBookings.Name = "dateTimePickerBeginDateBookings";
            // 
            // labelBeginDateBookings
            // 
            this.labelBeginDateBookings.AccessibleDescription = null;
            this.labelBeginDateBookings.AccessibleName = null;
            resources.ApplyResources(this.labelBeginDateBookings, "labelBeginDateBookings");
            this.labelBeginDateBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelBeginDateBookings.Name = "labelBeginDateBookings";
            // 
            // labelEndDateBookings
            // 
            this.labelEndDateBookings.AccessibleDescription = null;
            this.labelEndDateBookings.AccessibleName = null;
            resources.ApplyResources(this.labelEndDateBookings, "labelEndDateBookings");
            this.labelEndDateBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.labelEndDateBookings.Name = "labelEndDateBookings";
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.labelSlashBookings);
            this.groupBox1.Controls.Add(this.labelSelectedBookings);
            this.groupBox1.Controls.Add(this.labelTotalBookings);
            this.groupBox1.Controls.Add(this.buttonDeselectAllBookings);
            this.groupBox1.Controls.Add(this.buttonSelectAllBookings);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.buttonExportBookings);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelSlashBookings
            // 
            this.labelSlashBookings.AccessibleDescription = null;
            this.labelSlashBookings.AccessibleName = null;
            resources.ApplyResources(this.labelSlashBookings, "labelSlashBookings");
            this.labelSlashBookings.Font = null;
            this.labelSlashBookings.Name = "labelSlashBookings";
            // 
            // labelSelectedBookings
            // 
            this.labelSelectedBookings.AccessibleDescription = null;
            this.labelSelectedBookings.AccessibleName = null;
            resources.ApplyResources(this.labelSelectedBookings, "labelSelectedBookings");
            this.labelSelectedBookings.Font = null;
            this.labelSelectedBookings.Name = "labelSelectedBookings";
            // 
            // labelTotalBookings
            // 
            this.labelTotalBookings.AccessibleDescription = null;
            this.labelTotalBookings.AccessibleName = null;
            resources.ApplyResources(this.labelTotalBookings, "labelTotalBookings");
            this.labelTotalBookings.Font = null;
            this.labelTotalBookings.Name = "labelTotalBookings";
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleDescription = null;
            this.progressBar1.AccessibleName = null;
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.BackgroundImage = null;
            this.progressBar1.Font = null;
            this.progressBar1.Name = "progressBar1";
            // 
            // listViewBookings
            // 
            this.listViewBookings.AccessibleDescription = null;
            this.listViewBookings.AccessibleName = null;
            resources.ApplyResources(this.listViewBookings, "listViewBookings");
            this.listViewBookings.BackgroundImage = null;
            this.listViewBookings.CheckBoxes = true;
            this.listViewBookings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBookingJournalCode,
            this.columnHeaderBookingContractCode,
            this.columnHeaderBookingClientName,
            this.columnHeaderBookingDate,
            this.columnHeaderBookingType,
            this.columnHeaderBookingAccountTiers,
            this.columnHeaderBookingAccount,
            this.columnHeaderBookingAmount,
            this.columnHeaderBookingPartNumber,
            this.columnHeaderBookingReference,
            this.columnHeaderBookingDirection});
            this.listViewBookings.Font = null;
            this.listViewBookings.FullRowSelect = true;
            this.listViewBookings.GridLines = true;
            this.listViewBookings.Name = "listViewBookings";
            this.listViewBookings.UseCompatibleStateImageBehavior = false;
            this.listViewBookings.View = System.Windows.Forms.View.Details;
            this.listViewBookings.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewBookings_ItemChecked);
            // 
            // columnHeaderBookingJournalCode
            // 
            resources.ApplyResources(this.columnHeaderBookingJournalCode, "columnHeaderBookingJournalCode");
            // 
            // columnHeaderBookingContractCode
            // 
            resources.ApplyResources(this.columnHeaderBookingContractCode, "columnHeaderBookingContractCode");
            // 
            // columnHeaderBookingClientName
            // 
            resources.ApplyResources(this.columnHeaderBookingClientName, "columnHeaderBookingClientName");
            // 
            // columnHeaderBookingDate
            // 
            resources.ApplyResources(this.columnHeaderBookingDate, "columnHeaderBookingDate");
            // 
            // columnHeaderBookingType
            // 
            resources.ApplyResources(this.columnHeaderBookingType, "columnHeaderBookingType");
            // 
            // columnHeaderBookingAccountTiers
            // 
            resources.ApplyResources(this.columnHeaderBookingAccountTiers, "columnHeaderBookingAccountTiers");
            // 
            // columnHeaderBookingAccount
            // 
            resources.ApplyResources(this.columnHeaderBookingAccount, "columnHeaderBookingAccount");
            // 
            // columnHeaderBookingAmount
            // 
            resources.ApplyResources(this.columnHeaderBookingAmount, "columnHeaderBookingAmount");
            // 
            // columnHeaderBookingPartNumber
            // 
            resources.ApplyResources(this.columnHeaderBookingPartNumber, "columnHeaderBookingPartNumber");
            // 
            // columnHeaderBookingReference
            // 
            resources.ApplyResources(this.columnHeaderBookingReference, "columnHeaderBookingReference");
            // 
            // columnHeaderBookingDirection
            // 
            resources.ApplyResources(this.columnHeaderBookingDirection, "columnHeaderBookingDirection");
            // 
            // saveFileDialogAccountTiers
            // 
            this.saveFileDialogAccountTiers.DefaultExt = "txt";
            this.saveFileDialogAccountTiers.FileName = "account_tiers.txt";
            resources.ApplyResources(this.saveFileDialogAccountTiers, "saveFileDialogAccountTiers");
            // 
            // saveFileDialogBookings
            // 
            this.saveFileDialogBookings.DefaultExt = "txt";
            this.saveFileDialogBookings.FileName = "bookings.txt";
            resources.ApplyResources(this.saveFileDialogBookings, "saveFileDialogBookings");
            // 
            // _buttonExit
            // 
            this._buttonExit.AccessibleDescription = null;
            this._buttonExit.AccessibleName = null;
            resources.ApplyResources(this._buttonExit, "_buttonExit");
            this._buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            this._buttonExit.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this._buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this._buttonExit.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this._buttonExit.Name = "_buttonExit";
            this._buttonExit.UseVisualStyleBackColor = false;
            this._buttonExit.Click += new System.EventHandler(this._buttonExit_Click);
            // 
            // _labelTitle
            // 
            this._labelTitle.AccessibleDescription = null;
            this._labelTitle.AccessibleName = null;
            resources.ApplyResources(this._labelTitle, "_labelTitle");
            this._labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._labelTitle.ForeColor = System.Drawing.Color.White;
            this._labelTitle.Image = global::Foms.GUI.Properties.Resources.theme1_1_pastille_contrat;
            this._labelTitle.Name = "_labelTitle";
            this._labelTitle.Click += new System.EventHandler(this._labelTitle_Click);
            // 
            // buttonRefreshAccountTiers
            // 
            this.buttonRefreshAccountTiers.AccessibleDescription = null;
            this.buttonRefreshAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.buttonRefreshAccountTiers, "buttonRefreshAccountTiers");
            this.buttonRefreshAccountTiers.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonRefreshAccountTiers.Font = null;
            this.buttonRefreshAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRefreshAccountTiers.Name = "buttonRefreshAccountTiers";
            this.buttonRefreshAccountTiers.UseVisualStyleBackColor = true;
            this.buttonRefreshAccountTiers.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // btnDeselectAllAccountTiers
            // 
            this.btnDeselectAllAccountTiers.AccessibleDescription = null;
            this.btnDeselectAllAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.btnDeselectAllAccountTiers, "btnDeselectAllAccountTiers");
            this.btnDeselectAllAccountTiers.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnDeselectAllAccountTiers.Font = null;
            this.btnDeselectAllAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnDeselectAllAccountTiers.Name = "btnDeselectAllAccountTiers";
            this.btnDeselectAllAccountTiers.UseVisualStyleBackColor = true;
            this.btnDeselectAllAccountTiers.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAllAccountTiers
            // 
            this.btnSelectAllAccountTiers.AccessibleDescription = null;
            this.btnSelectAllAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.btnSelectAllAccountTiers, "btnSelectAllAccountTiers");
            this.btnSelectAllAccountTiers.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.btnSelectAllAccountTiers.Font = null;
            this.btnSelectAllAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.btnSelectAllAccountTiers.Name = "btnSelectAllAccountTiers";
            this.btnSelectAllAccountTiers.UseVisualStyleBackColor = true;
            this.btnSelectAllAccountTiers.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // buttonExportAccountTiers
            // 
            this.buttonExportAccountTiers.AccessibleDescription = null;
            this.buttonExportAccountTiers.AccessibleName = null;
            resources.ApplyResources(this.buttonExportAccountTiers, "buttonExportAccountTiers");
            this.buttonExportAccountTiers.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonExportAccountTiers.Font = null;
            this.buttonExportAccountTiers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExportAccountTiers.Image = global::Foms.GUI.Properties.Resources.theme1_1_export;
            this.buttonExportAccountTiers.Name = "buttonExportAccountTiers";
            this.buttonExportAccountTiers.UseVisualStyleBackColor = false;
            this.buttonExportAccountTiers.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonRefreshBookings
            // 
            this.buttonRefreshBookings.AccessibleDescription = null;
            this.buttonRefreshBookings.AccessibleName = null;
            resources.ApplyResources(this.buttonRefreshBookings, "buttonRefreshBookings");
            this.buttonRefreshBookings.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonRefreshBookings.Font = null;
            this.buttonRefreshBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRefreshBookings.Name = "buttonRefreshBookings";
            this.buttonRefreshBookings.UseVisualStyleBackColor = true;
            this.buttonRefreshBookings.Click += new System.EventHandler(this.buttonRefreshBookings_Click);
            // 
            // buttonDeselectAllBookings
            // 
            this.buttonDeselectAllBookings.AccessibleDescription = null;
            this.buttonDeselectAllBookings.AccessibleName = null;
            resources.ApplyResources(this.buttonDeselectAllBookings, "buttonDeselectAllBookings");
            this.buttonDeselectAllBookings.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonDeselectAllBookings.Font = null;
            this.buttonDeselectAllBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDeselectAllBookings.Name = "buttonDeselectAllBookings";
            this.buttonDeselectAllBookings.UseVisualStyleBackColor = true;
            this.buttonDeselectAllBookings.Click += new System.EventHandler(this.buttonDeselectAllBookings_Click);
            // 
            // buttonSelectAllBookings
            // 
            this.buttonSelectAllBookings.AccessibleDescription = null;
            this.buttonSelectAllBookings.AccessibleName = null;
            resources.ApplyResources(this.buttonSelectAllBookings, "buttonSelectAllBookings");
            this.buttonSelectAllBookings.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.buttonSelectAllBookings.Font = null;
            this.buttonSelectAllBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonSelectAllBookings.Name = "buttonSelectAllBookings";
            this.buttonSelectAllBookings.UseVisualStyleBackColor = true;
            this.buttonSelectAllBookings.Click += new System.EventHandler(this.buttonSelectAllBookings_Click);
            // 
            // buttonExportBookings
            // 
            this.buttonExportBookings.AccessibleDescription = null;
            this.buttonExportBookings.AccessibleName = null;
            resources.ApplyResources(this.buttonExportBookings, "buttonExportBookings");
            this.buttonExportBookings.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonExportBookings.Font = null;
            this.buttonExportBookings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExportBookings.Image = global::Foms.GUI.Properties.Resources.theme1_1_export;
            this.buttonExportBookings.Name = "buttonExportBookings";
            this.buttonExportBookings.UseVisualStyleBackColor = false;
            this.buttonExportBookings.Click += new System.EventHandler(this.buttonExportBookings_Click);
            // 
            // FrmExportSage
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.splitContainerSage);
            this.Font = null;
            this.Name = "FrmExportSage";
            this.splitContainerSage.Panel1.ResumeLayout(false);
            this.splitContainerSage.Panel2.ResumeLayout(false);
            this.splitContainerSage.ResumeLayout(false);
            this.tabControlExportations.ResumeLayout(false);
            this.tabPageAccountTiers.ResumeLayout(false);
            this.splitContainerAccountTiers.Panel1.ResumeLayout(false);
            this.splitContainerAccountTiers.Panel1.PerformLayout();
            this.splitContainerAccountTiers.Panel2.ResumeLayout(false);
            this.splitContainerAccountTiers.ResumeLayout(false);
            this.groupBoxExportAccountsTiers.ResumeLayout(false);
            this.groupBoxExportAccountsTiers.PerformLayout();
            this.tabPageBookings.ResumeLayout(false);
            this.splitContainerBookings.Panel1.ResumeLayout(false);
            this.splitContainerBookings.Panel1.PerformLayout();
            this.splitContainerBookings.Panel2.ResumeLayout(false);
            this.splitContainerBookings.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerSage;
        private System.Windows.Forms.Button _buttonExit;
        private System.Windows.Forms.Label _labelTitle;
        private System.Windows.Forms.TabControl tabControlExportations;
        private System.Windows.Forms.TabPage tabPageAccountTiers;
        private System.Windows.Forms.TabPage tabPageBookings;
        private System.Windows.Forms.SplitContainer splitContainerAccountTiers;
        private System.Windows.Forms.ListView listViewAccountsTiers;
        private System.Windows.Forms.ColumnHeader columnHeaderContractCode;
        private System.Windows.Forms.ColumnHeader columnHeaderClientType;
        private System.Windows.Forms.ColumnHeader columnHeaderClientName;
        private System.Windows.Forms.ColumnHeader columnHeaderLoanOfficer;
        private System.Windows.Forms.ColumnHeader columnHeaderDisbursedDate;
        private System.Windows.Forms.ColumnHeader columnHeaderAccountTiers;
        private System.Windows.Forms.Label labelBeginDateAccountTiers;
        private System.Windows.Forms.ColumnHeader columnHeaderCollectiveAccount;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDateAccountTiers;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginDateAccountTiers;
        private System.Windows.Forms.Label labelEndDateAccountTiers;
        private System.Windows.Forms.GroupBox groupBoxExportAccountsTiers;
        private System.Windows.Forms.Label labelSlashAccountTiers;
        private System.Windows.Forms.Label labelSelectedAccountTiers;
        private System.Windows.Forms.Label labelTotalAccountTiers;
        private System.Windows.Forms.Button btnDeselectAllAccountTiers;
        private System.Windows.Forms.Button btnSelectAllAccountTiers;
        private System.Windows.Forms.ProgressBar progressBarExport;
        private System.Windows.Forms.Button buttonExportAccountTiers;
        private System.Windows.Forms.Button buttonRefreshAccountTiers;
        private System.Windows.Forms.SaveFileDialog saveFileDialogAccountTiers;
        private System.Windows.Forms.ColumnHeader columnHeaderAccountTiersTitle;
        private System.Windows.Forms.SplitContainer splitContainerBookings;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDateBookings;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginDateBookings;
        private System.Windows.Forms.Label labelBeginDateBookings;
        private System.Windows.Forms.Label labelEndDateBookings;
        private System.Windows.Forms.Button buttonRefreshBookings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelSlashBookings;
        private System.Windows.Forms.Label labelSelectedBookings;
        private System.Windows.Forms.Label labelTotalBookings;
        private System.Windows.Forms.Button buttonDeselectAllBookings;
        private System.Windows.Forms.Button buttonSelectAllBookings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonExportBookings;
        private System.Windows.Forms.ListView listViewBookings;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingContractCode;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingClientName;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingDate;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingAccountTiers;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingAccount;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingPartNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingDirection;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingReference;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingType;
        private System.Windows.Forms.SaveFileDialog saveFileDialogBookings;
        private System.Windows.Forms.CheckBox checkBoxShowExportedBookings;
        private System.Windows.Forms.ColumnHeader columnHeaderBookingJournalCode;
    }
}