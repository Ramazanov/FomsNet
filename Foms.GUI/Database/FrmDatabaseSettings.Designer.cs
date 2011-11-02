namespace Foms.GUI.Database
{
    partial class FrmDatabaseSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatabaseSettings));
            this.groupBoxSQLServerConnection = new System.Windows.Forms.GroupBox();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.buttonGetServersList = new System.Windows.Forms.Button();
            this.labelInstallInstructionSQLServer = new System.Windows.Forms.Label();
            this.labelHelpServerName = new System.Windows.Forms.Label();
            this.linkLabelSQLServerInstallInstruction = new System.Windows.Forms.LinkLabel();
            this.cbServerName = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.groupBoxSQLSettings = new System.Windows.Forms.GroupBox();
            this.buttonSQLServerChangeSettings = new System.Windows.Forms.Button();
            this.lbSQLServerSettings = new System.Windows.Forms.Label();
            this.groupBoxDatabaseManagement = new System.Windows.Forms.GroupBox();
            this.labelDetectDatabasesInProgress = new System.Windows.Forms.Label();
            this.buttonSQLServerSettingsShowDetails = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.buttonSetAsDefault = new System.Windows.Forms.Button();
            this.buttonCreateNewDatabase = new System.Windows.Forms.Button();
            this.lbDatabases = new System.Windows.Forms.Label();
            this.listViewDatabases = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBranchCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.groupBoxSaveSettings = new System.Windows.Forms.GroupBox();
            this.btnDatabaseConnection = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControlDatabase = new System.Windows.Forms.TabControl();
            this.tabPageSQLServerConnection = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelServerSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageSQLServerSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelSQLSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageSqlDatabaseSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelDatabaseSettings = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSQLDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.buttonSQLDatabaseSettingsUpgrade = new System.Windows.Forms.Button();
            this.buttonSQLDatabaseSettingsChangeName = new System.Windows.Forms.Button();
            this.lbSQLDatabaseSettingsVersion = new System.Windows.Forms.Label();
            this.lbSQLDatabaseSettingsName = new System.Windows.Forms.Label();
            this.groupBoxSQLDatabaseStructure = new System.Windows.Forms.GroupBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.lbSQLDatabaseSettingsUpgradeNeedfull = new System.Windows.Forms.Label();
            this.tBDatabaseSettingsSchemaResult = new System.Windows.Forms.RichTextBox();
            this.bWDatabasesDetection = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bWDatabaseCreation = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseBackup = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseRestore = new System.ComponentModel.BackgroundWorker();
            this.bWDatabaseUpdate = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxSQLServerConnection.SuspendLayout();
            this.groupBoxSQLSettings.SuspendLayout();
            this.groupBoxDatabaseManagement.SuspendLayout();
            this.groupBoxSaveSettings.SuspendLayout();
            this.tabControlDatabase.SuspendLayout();
            this.tabPageSQLServerConnection.SuspendLayout();
            this.tableLayoutPanelServerSettings.SuspendLayout();
            this.tabPageSQLServerSettings.SuspendLayout();
            this.tableLayoutPanelSQLSettings.SuspendLayout();
            this.tabPageSqlDatabaseSettings.SuspendLayout();
            this.tableLayoutPanelDatabaseSettings.SuspendLayout();
            this.groupBoxSQLDatabaseSettings.SuspendLayout();
            this.groupBoxSQLDatabaseStructure.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSQLServerConnection
            // 
            this.groupBoxSQLServerConnection.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSQLServerConnection.Controls.Add(this.buttonDefault);
            this.groupBoxSQLServerConnection.Controls.Add(this.buttonGetServersList);
            this.groupBoxSQLServerConnection.Controls.Add(this.labelInstallInstructionSQLServer);
            this.groupBoxSQLServerConnection.Controls.Add(this.labelHelpServerName);
            this.groupBoxSQLServerConnection.Controls.Add(this.linkLabelSQLServerInstallInstruction);
            this.groupBoxSQLServerConnection.Controls.Add(this.cbServerName);
            this.groupBoxSQLServerConnection.Controls.Add(this.txtPassword);
            this.groupBoxSQLServerConnection.Controls.Add(this.label9);
            this.groupBoxSQLServerConnection.Controls.Add(this.txtLoginName);
            this.groupBoxSQLServerConnection.Controls.Add(this.label8);
            this.groupBoxSQLServerConnection.Controls.Add(this.label5);
            this.groupBoxSQLServerConnection.Controls.Add(this.label);
            resources.ApplyResources(this.groupBoxSQLServerConnection, "groupBoxSQLServerConnection");
            this.groupBoxSQLServerConnection.Name = "groupBoxSQLServerConnection";
            this.groupBoxSQLServerConnection.TabStop = false;
            // 
            // buttonDefault
            // 
            this.buttonDefault.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonDefault, "buttonDefault");
            this.buttonDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.UseVisualStyleBackColor = false;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // buttonGetServersList
            // 
            this.buttonGetServersList.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonGetServersList, "buttonGetServersList");
            this.buttonGetServersList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonGetServersList.Name = "buttonGetServersList";
            this.buttonGetServersList.UseVisualStyleBackColor = false;
            this.buttonGetServersList.Click += new System.EventHandler(this.buttonGetServersList_Click);
            // 
            // labelInstallInstructionSQLServer
            // 
            resources.ApplyResources(this.labelInstallInstructionSQLServer, "labelInstallInstructionSQLServer");
            this.labelInstallInstructionSQLServer.ForeColor = System.Drawing.Color.Red;
            this.labelInstallInstructionSQLServer.Name = "labelInstallInstructionSQLServer";
            // 
            // labelHelpServerName
            // 
            resources.ApplyResources(this.labelHelpServerName, "labelHelpServerName");
            this.labelHelpServerName.Name = "labelHelpServerName";
            // 
            // linkLabelSQLServerInstallInstruction
            // 
            resources.ApplyResources(this.linkLabelSQLServerInstallInstruction, "linkLabelSQLServerInstallInstruction");
            this.linkLabelSQLServerInstallInstruction.Name = "linkLabelSQLServerInstallInstruction";
            this.linkLabelSQLServerInstallInstruction.TabStop = true;
            this.linkLabelSQLServerInstallInstruction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSQLServerInstallInstruction_LinkClicked);
            // 
            // cbServerName
            // 
            this.cbServerName.FormattingEnabled = true;
            resources.ApplyResources(this.cbServerName, "cbServerName");
            this.cbServerName.Name = "cbServerName";
            this.cbServerName.TextChanged += new System.EventHandler(this.cbServerName_TextChanged);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtLoginName
            // 
            resources.ApplyResources(this.txtLoginName, "txtLoginName");
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.TextChanged += new System.EventHandler(this.txtLoginName_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // groupBoxSQLSettings
            // 
            this.groupBoxSQLSettings.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSQLSettings.Controls.Add(this.buttonSQLServerChangeSettings);
            this.groupBoxSQLSettings.Controls.Add(this.lbSQLServerSettings);
            resources.ApplyResources(this.groupBoxSQLSettings, "groupBoxSQLSettings");
            this.groupBoxSQLSettings.Name = "groupBoxSQLSettings";
            this.groupBoxSQLSettings.TabStop = false;
            // 
            // buttonSQLServerChangeSettings
            // 
            resources.ApplyResources(this.buttonSQLServerChangeSettings, "buttonSQLServerChangeSettings");
            this.buttonSQLServerChangeSettings.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSQLServerChangeSettings.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSQLServerChangeSettings.Name = "buttonSQLServerChangeSettings";
            this.buttonSQLServerChangeSettings.UseVisualStyleBackColor = false;
            this.buttonSQLServerChangeSettings.Click += new System.EventHandler(this.buttonSQLServerChangeSettings_Click);
            // 
            // lbSQLServerSettings
            // 
            resources.ApplyResources(this.lbSQLServerSettings, "lbSQLServerSettings");
            this.lbSQLServerSettings.Name = "lbSQLServerSettings";
            // 
            // groupBoxDatabaseManagement
            // 
            this.groupBoxDatabaseManagement.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDatabaseManagement.Controls.Add(this.labelDetectDatabasesInProgress);
            this.groupBoxDatabaseManagement.Controls.Add(this.buttonSQLServerSettingsShowDetails);
            this.groupBoxDatabaseManagement.Controls.Add(this.buttonRestore);
            this.groupBoxDatabaseManagement.Controls.Add(this.buttonBackup);
            this.groupBoxDatabaseManagement.Controls.Add(this.buttonSetAsDefault);
            this.groupBoxDatabaseManagement.Controls.Add(this.buttonCreateNewDatabase);
            this.groupBoxDatabaseManagement.Controls.Add(this.lbDatabases);
            this.groupBoxDatabaseManagement.Controls.Add(this.listViewDatabases);
            resources.ApplyResources(this.groupBoxDatabaseManagement, "groupBoxDatabaseManagement");
            this.groupBoxDatabaseManagement.Name = "groupBoxDatabaseManagement";
            this.groupBoxDatabaseManagement.TabStop = false;
            // 
            // labelDetectDatabasesInProgress
            // 
            resources.ApplyResources(this.labelDetectDatabasesInProgress, "labelDetectDatabasesInProgress");
            this.labelDetectDatabasesInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labelDetectDatabasesInProgress.Name = "labelDetectDatabasesInProgress";
            // 
            // buttonSQLServerSettingsShowDetails
            // 
            resources.ApplyResources(this.buttonSQLServerSettingsShowDetails, "buttonSQLServerSettingsShowDetails");
            this.buttonSQLServerSettingsShowDetails.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSQLServerSettingsShowDetails.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSQLServerSettingsShowDetails.Name = "buttonSQLServerSettingsShowDetails";
            this.buttonSQLServerSettingsShowDetails.UseVisualStyleBackColor = false;
            this.buttonSQLServerSettingsShowDetails.Click += new System.EventHandler(this.buttonSQLServerSettingsShowDetails_Click);
            // 
            // buttonRestore
            // 
            resources.ApplyResources(this.buttonRestore, "buttonRestore");
            this.buttonRestore.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonRestore.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.UseVisualStyleBackColor = false;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonBackup
            // 
            resources.ApplyResources(this.buttonBackup, "buttonBackup");
            this.buttonBackup.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonBackup.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.UseVisualStyleBackColor = false;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // buttonSetAsDefault
            // 
            resources.ApplyResources(this.buttonSetAsDefault, "buttonSetAsDefault");
            this.buttonSetAsDefault.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSetAsDefault.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSetAsDefault.Name = "buttonSetAsDefault";
            this.buttonSetAsDefault.UseVisualStyleBackColor = false;
            this.buttonSetAsDefault.Click += new System.EventHandler(this.buttonSetAsDefault_Click);
            // 
            // buttonCreateNewDatabase
            // 
            resources.ApplyResources(this.buttonCreateNewDatabase, "buttonCreateNewDatabase");
            this.buttonCreateNewDatabase.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonCreateNewDatabase.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonCreateNewDatabase.Name = "buttonCreateNewDatabase";
            this.buttonCreateNewDatabase.UseVisualStyleBackColor = false;
            this.buttonCreateNewDatabase.Click += new System.EventHandler(this.buttonCreateNewDatabase_Click);
            // 
            // lbDatabases
            // 
            resources.ApplyResources(this.lbDatabases, "lbDatabases");
            this.lbDatabases.Name = "lbDatabases";
            // 
            // listViewDatabases
            // 
            this.listViewDatabases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewDatabases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderBranchCode,
            this.columnHeaderVersion,
            this.columnHeaderSize});
            this.listViewDatabases.FullRowSelect = true;
            this.listViewDatabases.GridLines = true;
            this.listViewDatabases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewDatabases.HideSelection = false;
            resources.ApplyResources(this.listViewDatabases, "listViewDatabases");
            this.listViewDatabases.Name = "listViewDatabases";
            this.listViewDatabases.UseCompatibleStateImageBehavior = false;
            this.listViewDatabases.View = System.Windows.Forms.View.Details;
            this.listViewDatabases.DoubleClick += new System.EventHandler(this.listViewDatabases_DoubleClick);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderBranchCode
            // 
            resources.ApplyResources(this.columnHeaderBranchCode, "columnHeaderBranchCode");
            // 
            // columnHeaderVersion
            // 
            resources.ApplyResources(this.columnHeaderVersion, "columnHeaderVersion");
            // 
            // columnHeaderSize
            // 
            resources.ApplyResources(this.columnHeaderSize, "columnHeaderSize");
            // 
            // groupBoxSaveSettings
            // 
            this.groupBoxSaveSettings.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSaveSettings.Controls.Add(this.btnDatabaseConnection);
            this.groupBoxSaveSettings.Controls.Add(this.buttonExit);
            this.groupBoxSaveSettings.Controls.Add(this.labelResult);
            this.groupBoxSaveSettings.Controls.Add(this.buttonSave);
            resources.ApplyResources(this.groupBoxSaveSettings, "groupBoxSaveSettings");
            this.groupBoxSaveSettings.Name = "groupBoxSaveSettings";
            this.groupBoxSaveSettings.TabStop = false;
            // 
            // btnDatabaseConnection
            // 
            resources.ApplyResources(this.btnDatabaseConnection, "btnDatabaseConnection");
            this.btnDatabaseConnection.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDatabaseConnection.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.btnDatabaseConnection.Name = "btnDatabaseConnection";
            this.btnDatabaseConnection.UseVisualStyleBackColor = false;
            this.btnDatabaseConnection.Click += new System.EventHandler(this.btnDatabaseConnection_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = false;
            // 
            // labelResult
            // 
            resources.ApplyResources(this.labelResult, "labelResult");
            this.labelResult.Name = "labelResult";
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSave.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tabControlDatabase
            // 
            this.tabControlDatabase.Controls.Add(this.tabPageSQLServerConnection);
            this.tabControlDatabase.Controls.Add(this.tabPageSQLServerSettings);
            this.tabControlDatabase.Controls.Add(this.tabPageSqlDatabaseSettings);
            resources.ApplyResources(this.tabControlDatabase, "tabControlDatabase");
            this.tabControlDatabase.Name = "tabControlDatabase";
            this.tabControlDatabase.SelectedIndex = 0;
            // 
            // tabPageSQLServerConnection
            // 
            this.tabPageSQLServerConnection.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.tabPageSQLServerConnection, "tabPageSQLServerConnection");
            this.tabPageSQLServerConnection.Controls.Add(this.tableLayoutPanelServerSettings);
            this.tabPageSQLServerConnection.Name = "tabPageSQLServerConnection";
            this.tabPageSQLServerConnection.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelServerSettings
            // 
            resources.ApplyResources(this.tableLayoutPanelServerSettings, "tableLayoutPanelServerSettings");
            this.tableLayoutPanelServerSettings.Controls.Add(this.groupBoxSQLServerConnection, 0, 0);
            this.tableLayoutPanelServerSettings.Name = "tableLayoutPanelServerSettings";
            // 
            // tabPageSQLServerSettings
            // 
            this.tabPageSQLServerSettings.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.tabPageSQLServerSettings, "tabPageSQLServerSettings");
            this.tabPageSQLServerSettings.Controls.Add(this.tableLayoutPanelSQLSettings);
            this.tabPageSQLServerSettings.Name = "tabPageSQLServerSettings";
            this.tabPageSQLServerSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelSQLSettings
            // 
            resources.ApplyResources(this.tableLayoutPanelSQLSettings, "tableLayoutPanelSQLSettings");
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxSQLSettings, 0, 0);
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxSaveSettings, 0, 2);
            this.tableLayoutPanelSQLSettings.Controls.Add(this.groupBoxDatabaseManagement, 0, 1);
            this.tableLayoutPanelSQLSettings.Name = "tableLayoutPanelSQLSettings";
            // 
            // tabPageSqlDatabaseSettings
            // 
            this.tabPageSqlDatabaseSettings.Controls.Add(this.tableLayoutPanelDatabaseSettings);
            resources.ApplyResources(this.tabPageSqlDatabaseSettings, "tabPageSqlDatabaseSettings");
            this.tabPageSqlDatabaseSettings.Name = "tabPageSqlDatabaseSettings";
            this.tabPageSqlDatabaseSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDatabaseSettings
            // 
            this.tableLayoutPanelDatabaseSettings.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            resources.ApplyResources(this.tableLayoutPanelDatabaseSettings, "tableLayoutPanelDatabaseSettings");
            this.tableLayoutPanelDatabaseSettings.Controls.Add(this.groupBoxSQLDatabaseSettings, 0, 1);
            this.tableLayoutPanelDatabaseSettings.Controls.Add(this.groupBoxSQLDatabaseStructure, 0, 2);
            this.tableLayoutPanelDatabaseSettings.Name = "tableLayoutPanelDatabaseSettings";
            // 
            // groupBoxSQLDatabaseSettings
            // 
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.buttonSQLDatabaseSettingsUpgrade);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.buttonSQLDatabaseSettingsChangeName);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.lbSQLDatabaseSettingsVersion);
            this.groupBoxSQLDatabaseSettings.Controls.Add(this.lbSQLDatabaseSettingsName);
            resources.ApplyResources(this.groupBoxSQLDatabaseSettings, "groupBoxSQLDatabaseSettings");
            this.groupBoxSQLDatabaseSettings.Name = "groupBoxSQLDatabaseSettings";
            this.groupBoxSQLDatabaseSettings.TabStop = false;
            // 
            // buttonSQLDatabaseSettingsUpgrade
            // 
            resources.ApplyResources(this.buttonSQLDatabaseSettingsUpgrade, "buttonSQLDatabaseSettingsUpgrade");
            this.buttonSQLDatabaseSettingsUpgrade.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSQLDatabaseSettingsUpgrade.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSQLDatabaseSettingsUpgrade.Name = "buttonSQLDatabaseSettingsUpgrade";
            this.buttonSQLDatabaseSettingsUpgrade.UseVisualStyleBackColor = false;
            this.buttonSQLDatabaseSettingsUpgrade.Click += new System.EventHandler(this.buttonSQLDatabaseSettingsUpgrade_Click);
            // 
            // buttonSQLDatabaseSettingsChangeName
            // 
            resources.ApplyResources(this.buttonSQLDatabaseSettingsChangeName, "buttonSQLDatabaseSettingsChangeName");
            this.buttonSQLDatabaseSettingsChangeName.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonSQLDatabaseSettingsChangeName.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonSQLDatabaseSettingsChangeName.Name = "buttonSQLDatabaseSettingsChangeName";
            this.buttonSQLDatabaseSettingsChangeName.UseVisualStyleBackColor = false;
            this.buttonSQLDatabaseSettingsChangeName.Click += new System.EventHandler(this.buttonSQLDatabaseSettingsChangeName_Click);
            // 
            // lbSQLDatabaseSettingsVersion
            // 
            resources.ApplyResources(this.lbSQLDatabaseSettingsVersion, "lbSQLDatabaseSettingsVersion");
            this.lbSQLDatabaseSettingsVersion.Name = "lbSQLDatabaseSettingsVersion";
            // 
            // lbSQLDatabaseSettingsName
            // 
            resources.ApplyResources(this.lbSQLDatabaseSettingsName, "lbSQLDatabaseSettingsName");
            this.lbSQLDatabaseSettingsName.Name = "lbSQLDatabaseSettingsName";
            // 
            // groupBoxSQLDatabaseStructure
            // 
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.buttonContinue);
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.lbSQLDatabaseSettingsUpgradeNeedfull);
            this.groupBoxSQLDatabaseStructure.Controls.Add(this.tBDatabaseSettingsSchemaResult);
            resources.ApplyResources(this.groupBoxSQLDatabaseStructure, "groupBoxSQLDatabaseStructure");
            this.groupBoxSQLDatabaseStructure.Name = "groupBoxSQLDatabaseStructure";
            this.groupBoxSQLDatabaseStructure.TabStop = false;
            // 
            // buttonContinue
            // 
            resources.ApplyResources(this.buttonContinue, "buttonContinue");
            this.buttonContinue.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonContinue.ForeColor = System.Drawing.Color.Red;
            this.buttonContinue.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.UseVisualStyleBackColor = false;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // lbSQLDatabaseSettingsUpgradeNeedfull
            // 
            resources.ApplyResources(this.lbSQLDatabaseSettingsUpgradeNeedfull, "lbSQLDatabaseSettingsUpgradeNeedfull");
            this.lbSQLDatabaseSettingsUpgradeNeedfull.Name = "lbSQLDatabaseSettingsUpgradeNeedfull";
            // 
            // tBDatabaseSettingsSchemaResult
            // 
            this.tBDatabaseSettingsSchemaResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tBDatabaseSettingsSchemaResult, "tBDatabaseSettingsSchemaResult");
            this.tBDatabaseSettingsSchemaResult.Name = "tBDatabaseSettingsSchemaResult";
            // 
            // bWDatabasesDetection
            // 
            this.bWDatabasesDetection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDetectDatabases_DoWork);
            this.bWDatabasesDetection.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDetectDatabases_RunWorkerCompleted);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // bWDatabaseCreation
            // 
            this.bWDatabaseCreation.WorkerSupportsCancellation = true;
            this.bWDatabaseCreation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseCreation_DoWork);
            this.bWDatabaseCreation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseCreation_RunWorkerCompleted);
            // 
            // bWDatabaseBackup
            // 
            this.bWDatabaseBackup.WorkerSupportsCancellation = true;
            this.bWDatabaseBackup.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseBackup_DoWork);
            this.bWDatabaseBackup.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseBackup_RunWorkerCompleted);
            // 
            // bWDatabaseRestore
            // 
            this.bWDatabaseRestore.WorkerSupportsCancellation = true;
            this.bWDatabaseRestore.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseRestore_DoWork);
            this.bWDatabaseRestore.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseRestore_RunWorkerCompleted);
            // 
            // bWDatabaseUpdate
            // 
            this.bWDatabaseUpdate.WorkerReportsProgress = true;
            this.bWDatabaseUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWDatabaseUpdate_DoWork);
            this.bWDatabaseUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWDatabaseUpdate_RunWorkerCompleted);
            this.bWDatabaseUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWDatabaseUpdate_ProgressChanged);
            // 
            // FrmDatabaseSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris_180;
            this.Controls.Add(this.tabControlDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDatabaseSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDatabaseSettings_FormClosed);
            this.groupBoxSQLServerConnection.ResumeLayout(false);
            this.groupBoxSQLServerConnection.PerformLayout();
            this.groupBoxSQLSettings.ResumeLayout(false);
            this.groupBoxSQLSettings.PerformLayout();
            this.groupBoxDatabaseManagement.ResumeLayout(false);
            this.groupBoxDatabaseManagement.PerformLayout();
            this.groupBoxSaveSettings.ResumeLayout(false);
            this.groupBoxSaveSettings.PerformLayout();
            this.tabControlDatabase.ResumeLayout(false);
            this.tabPageSQLServerConnection.ResumeLayout(false);
            this.tableLayoutPanelServerSettings.ResumeLayout(false);
            this.tabPageSQLServerSettings.ResumeLayout(false);
            this.tableLayoutPanelSQLSettings.ResumeLayout(false);
            this.tabPageSqlDatabaseSettings.ResumeLayout(false);
            this.tableLayoutPanelDatabaseSettings.ResumeLayout(false);
            this.groupBoxSQLDatabaseSettings.ResumeLayout(false);
            this.groupBoxSQLDatabaseSettings.PerformLayout();
            this.groupBoxSQLDatabaseStructure.ResumeLayout(false);
            this.groupBoxSQLDatabaseStructure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSQLServerConnection;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox cbServerName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabelSQLServerInstallInstruction;
        private System.Windows.Forms.GroupBox groupBoxSQLSettings;
        private System.Windows.Forms.Button buttonSQLServerChangeSettings;
        private System.Windows.Forms.Label lbSQLServerSettings;
        private System.Windows.Forms.GroupBox groupBoxDatabaseManagement;
        private System.Windows.Forms.Button buttonCreateNewDatabase;
        private System.Windows.Forms.Label lbDatabases;
        private System.Windows.Forms.ListView listViewDatabases;
        private System.Windows.Forms.Button buttonSetAsDefault;
        private System.Windows.Forms.GroupBox groupBoxSaveSettings;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabControl tabControlDatabase;
        private System.Windows.Forms.TabPage tabPageSQLServerConnection;
        private System.Windows.Forms.TabPage tabPageSQLServerSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSQLSettings;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.ComponentModel.BackgroundWorker bWDatabasesDetection;
        private System.Windows.Forms.Label labelDetectDatabasesInProgress;
        private System.Windows.Forms.ColumnHeader columnHeaderBranchCode;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tabPageSqlDatabaseSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatabaseSettings;
        private System.Windows.Forms.GroupBox groupBoxSQLDatabaseSettings;
        private System.Windows.Forms.Label lbSQLDatabaseSettingsVersion;
        private System.Windows.Forms.Label lbSQLDatabaseSettingsName;
        private System.Windows.Forms.Button buttonSQLDatabaseSettingsUpgrade;
        private System.Windows.Forms.Button buttonSQLDatabaseSettingsChangeName;
        private System.Windows.Forms.Button buttonSQLServerSettingsShowDetails;
        private System.Windows.Forms.GroupBox groupBoxSQLDatabaseStructure;
        private System.Windows.Forms.Label lbSQLDatabaseSettingsUpgradeNeedfull;
        private System.ComponentModel.BackgroundWorker bWDatabaseCreation;
        private System.ComponentModel.BackgroundWorker bWDatabaseBackup;
        private System.ComponentModel.BackgroundWorker bWDatabaseRestore;
        private System.ComponentModel.BackgroundWorker bWDatabaseUpdate;
        private System.Windows.Forms.RichTextBox tBDatabaseSettingsSchemaResult;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelServerSettings;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button btnDatabaseConnection;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label labelHelpServerName;
        private System.Windows.Forms.Label labelInstallInstructionSQLServer;
        private System.Windows.Forms.Button buttonGetServersList;
        private System.Windows.Forms.Button buttonDefault;
    }
}