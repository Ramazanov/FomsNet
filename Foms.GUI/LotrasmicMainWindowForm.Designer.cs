
using System.Windows.Forms;
using Foms.GUI.UserControl;
using Foms.Enums;
using Foms.Services;
using Foms.CoreDomain;

namespace Foms.GUI
{
    partial class LotrasmicMainWindowForm
    {
        private System.Windows.Forms.ToolStripMenuItem mnuClients;
        private System.Windows.Forms.ToolStripMenuItem mnuContracts;
        private System.Windows.Forms.ToolStripMenuItem mnuAccounting;
        private System.Windows.Forms.ToolStripMenuItem mnuNewClient;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchClient;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchContract;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuNewPerson;
        private System.Windows.Forms.ToolStripMenuItem mnuNewGroup;
        private System.Windows.Forms.ImageList imageListAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuMonthlyClosure;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportTransaction;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddUser;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem menuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAboutOctopus;
        private System.Windows.Forms.ToolStripMenuItem menuItemApplicationDate;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdvancedSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuDatamanagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemDatabaseControlPanel;
        private System.Windows.Forms.ToolStripMenuItem menuItemDatabaseMaintenance;
        private ToolStripSeparator toolStripSeparatorConfig1;
        private ToolStrip mainStripToolBar;
        private ToolStripButton toolBarButtonSearchPerson;
        private ToolStripButton toolBarButtonSearchContract;
        private ToolStripSplitButton toolBarButNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolBarButtonPerson;
        private ToolStripMenuItem toolBarButtonNewGroup;
        private ToolStripButton toolBarButtonReports;
        private StatusStrip mainStatusBar;
        private CollapsibleSplitter splitter6;
        private ToolStripLabel toolBarLblVersion;
        private ToolStripStatusLabel mainStatusBarLblUserName;
        private ToolStripStatusLabel mainStatusBarLblDate;
        private ToolStripStatusLabel mainStatusBarLblUpdateVersion;
        private ToolStripLabel toolStripLabel1;
        private ToolStripStatusLabel toolStripStatusLblBranchCode;
        private ToolStripStatusLabel mainStatusBarLblInfo;
        private ToolStripMenuItem customizableFieldsToolStripMenuItem;
        private ToolStripMenuItem octopusForumToolStripMenuItem;


        /// <summary>
        /// Nettoyage des ressources utilisées.
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

        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotrasmicMainWindowForm));
            this.mnuClients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewVillage = new System.Windows.Forms.ToolStripMenuItem();
            this.newCorporateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContracts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchContract = new System.Windows.Forms.ToolStripMenuItem();
            this.reasignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccounting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExportTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonthlyClosure = new System.Windows.Forms.ToolStripMenuItem();
            this.creditClosureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingClosureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImportCustomizableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorConfig1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemApplicationDate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdvancedSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.customizableFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizableExportImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCustomizableExportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCustomizableImportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExistingCustomizableFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatamanagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDatabaseControlPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDatabaseMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAboutOctopus = new System.Windows.Forms.ToolStripMenuItem();
            this.octopusForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionnaireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wIKIHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListAlert = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mView = new System.Windows.Forms.ToolStripMenuItem();
            this.miAuditTrail = new System.Windows.Forms.ToolStripMenuItem();
            this.miReports = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainStripToolBar = new System.Windows.Forms.ToolStrip();
            this.toolBarButNew = new System.Windows.Forms.ToolStripSplitButton();
            this.toolBarButtonPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarButtonNewGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tbbtnNewVillage = new System.Windows.Forms.ToolStripMenuItem();
            this.corporateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBarButtonSearchPerson = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonSearchContract = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonReports = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButtonSearchProject = new System.Windows.Forms.ToolStripButton();
            this.toolBarLblVersion = new System.Windows.Forms.ToolStripLabel();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.mainStatusBarLblUpdateVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblBranchCode = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusBarLblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblDB = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwAlerts = new System.ComponentModel.BackgroundWorker();
            this.nIUpdateAvailable = new System.Windows.Forms.NotifyIcon(this.components);
            this.openCustomizableFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitter6 = new Foms.GUI.UserControl.CollapsibleSplitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabFilter = new System.Windows.Forms.TableLayoutPanel();
            this.chkPendingSavings = new System.Windows.Forms.CheckBox();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.chkLateLoans = new System.Windows.Forms.CheckBox();
            this.chkPendingLoans = new System.Windows.Forms.CheckBox();
            this.chkOverdraftSavings = new System.Windows.Forms.CheckBox();
            this.alertBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainMenu.SuspendLayout();
            this.mainStripToolBar.SuspendLayout();
            this.mainStatusBar.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.tabFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuClients
            // 
            this.mnuClients.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewClient,
            this.mnuSearchClient});
            this.mnuClients.Name = "mnuClients";
            resources.ApplyResources(this.mnuClients, "mnuClients");
            // 
            // mnuNewClient
            // 
            this.mnuNewClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewPerson,
            this.mnuNewGroup,
            this.mnuNewVillage,
            this.newCorporateToolStripMenuItem});
            resources.ApplyResources(this.mnuNewClient, "mnuNewClient");
            this.mnuNewClient.Name = "mnuNewClient";
            // 
            // mnuNewPerson
            // 
            resources.ApplyResources(this.mnuNewPerson, "mnuNewPerson");
            this.mnuNewPerson.Name = "mnuNewPerson";
            this.mnuNewPerson.Click += new System.EventHandler(this.mnuNewPerson_Click);
            // 
            // mnuNewGroup
            // 
            resources.ApplyResources(this.mnuNewGroup, "mnuNewGroup");
            this.mnuNewGroup.Name = "mnuNewGroup";
            // 
            // mnuNewVillage
            // 
            resources.ApplyResources(this.mnuNewVillage, "mnuNewVillage");
            this.mnuNewVillage.Name = "mnuNewVillage";
            // 
            // newCorporateToolStripMenuItem
            // 
            this.newCorporateToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_import;
            this.newCorporateToolStripMenuItem.Name = "newCorporateToolStripMenuItem";
            resources.ApplyResources(this.newCorporateToolStripMenuItem, "newCorporateToolStripMenuItem");
            // 
            // mnuSearchClient
            // 
            resources.ApplyResources(this.mnuSearchClient, "mnuSearchClient");
            this.mnuSearchClient.Name = "mnuSearchClient";
            // 
            // mnuContracts
            // 
            this.mnuContracts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSearchContract,
            this.reasignToolStripMenuItem});
            this.mnuContracts.Name = "mnuContracts";
            resources.ApplyResources(this.mnuContracts, "mnuContracts");
            // 
            // mnuSearchContract
            // 
            resources.ApplyResources(this.mnuSearchContract, "mnuSearchContract");
            this.mnuSearchContract.Name = "mnuSearchContract";
            // 
            // reasignToolStripMenuItem
            // 
            resources.ApplyResources(this.reasignToolStripMenuItem, "reasignToolStripMenuItem");
            this.reasignToolStripMenuItem.Name = "reasignToolStripMenuItem";
            // 
            // mnuAccounting
            // 
            this.mnuAccounting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExportTransaction,
            this.mnuMonthlyClosure,
            this.exportImportCustomizableToolStripMenuItem});
            this.mnuAccounting.Name = "mnuAccounting";
            resources.ApplyResources(this.mnuAccounting, "mnuAccounting");
            // 
            // menuItemExportTransaction
            // 
            resources.ApplyResources(this.menuItemExportTransaction, "menuItemExportTransaction");
            this.menuItemExportTransaction.Name = "menuItemExportTransaction";
            this.menuItemExportTransaction.Click += new System.EventHandler(this.menuItemExportTransaction_Click);
            // 
            // mnuMonthlyClosure
            // 
            this.mnuMonthlyClosure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditClosureToolStripMenuItem,
            this.savingClosureToolStripMenuItem});
            resources.ApplyResources(this.mnuMonthlyClosure, "mnuMonthlyClosure");
            this.mnuMonthlyClosure.Name = "mnuMonthlyClosure";
            // 
            // creditClosureToolStripMenuItem
            // 
            this.creditClosureToolStripMenuItem.Name = "creditClosureToolStripMenuItem";
            resources.ApplyResources(this.creditClosureToolStripMenuItem, "creditClosureToolStripMenuItem");
            // 
            // savingClosureToolStripMenuItem
            // 
            this.savingClosureToolStripMenuItem.Name = "savingClosureToolStripMenuItem";
            resources.ApplyResources(this.savingClosureToolStripMenuItem, "savingClosureToolStripMenuItem");
            // 
            // exportImportCustomizableToolStripMenuItem
            // 
            this.exportImportCustomizableToolStripMenuItem.Name = "exportImportCustomizableToolStripMenuItem";
            resources.ApplyResources(this.exportImportCustomizableToolStripMenuItem, "exportImportCustomizableToolStripMenuItem");
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddUser,
            this.rolesToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparatorConfig1,
            this.menuItemApplicationDate,
            this.menuItemSetting,
            this.menuItemAdvancedSettings,
            this.customizableFieldsToolStripMenuItem,
            this.customizableExportImportToolStripMenuItem});
            this.mnuConfiguration.Name = "mnuConfiguration";
            resources.ApplyResources(this.mnuConfiguration, "mnuConfiguration");
            // 
            // menuItemAddUser
            // 
            resources.ApplyResources(this.menuItemAddUser, "menuItemAddUser");
            this.menuItemAddUser.Name = "menuItemAddUser";
            this.menuItemAddUser.Click += new System.EventHandler(this.menuItemAddUser_Click);
            // 
            // rolesToolStripMenuItem
            // 
            resources.ApplyResources(this.rolesToolStripMenuItem, "rolesToolStripMenuItem");
            this.rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            this.rolesToolStripMenuItem.Click += new System.EventHandler(this.rolesToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_doc;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            // 
            // toolStripSeparatorConfig1
            // 
            this.toolStripSeparatorConfig1.Name = "toolStripSeparatorConfig1";
            resources.ApplyResources(this.toolStripSeparatorConfig1, "toolStripSeparatorConfig1");
            // 
            // menuItemApplicationDate
            // 
            resources.ApplyResources(this.menuItemApplicationDate, "menuItemApplicationDate");
            this.menuItemApplicationDate.Name = "menuItemApplicationDate";
            // 
            // menuItemSetting
            // 
            resources.ApplyResources(this.menuItemSetting, "menuItemSetting");
            this.menuItemSetting.Name = "menuItemSetting";
            // 
            // menuItemAdvancedSettings
            // 
            resources.ApplyResources(this.menuItemAdvancedSettings, "menuItemAdvancedSettings");
            this.menuItemAdvancedSettings.Name = "menuItemAdvancedSettings";
            // 
            // customizableFieldsToolStripMenuItem
            // 
            this.customizableFieldsToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_doc;
            this.customizableFieldsToolStripMenuItem.Name = "customizableFieldsToolStripMenuItem";
            resources.ApplyResources(this.customizableFieldsToolStripMenuItem, "customizableFieldsToolStripMenuItem");
            // 
            // customizableExportImportToolStripMenuItem
            // 
            this.customizableExportImportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCustomizableExportFileToolStripMenuItem,
            this.createCustomizableImportFileToolStripMenuItem,
            this.openExistingCustomizableFileToolStripMenuItem});
            this.customizableExportImportToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_doc;
            this.customizableExportImportToolStripMenuItem.Name = "customizableExportImportToolStripMenuItem";
            resources.ApplyResources(this.customizableExportImportToolStripMenuItem, "customizableExportImportToolStripMenuItem");
            // 
            // createCustomizableExportFileToolStripMenuItem
            // 
            this.createCustomizableExportFileToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_new;
            this.createCustomizableExportFileToolStripMenuItem.Name = "createCustomizableExportFileToolStripMenuItem";
            resources.ApplyResources(this.createCustomizableExportFileToolStripMenuItem, "createCustomizableExportFileToolStripMenuItem");
            // 
            // createCustomizableImportFileToolStripMenuItem
            // 
            this.createCustomizableImportFileToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_new;
            this.createCustomizableImportFileToolStripMenuItem.Name = "createCustomizableImportFileToolStripMenuItem";
            resources.ApplyResources(this.createCustomizableImportFileToolStripMenuItem, "createCustomizableImportFileToolStripMenuItem");
            // 
            // openExistingCustomizableFileToolStripMenuItem
            // 
            resources.ApplyResources(this.openExistingCustomizableFileToolStripMenuItem, "openExistingCustomizableFileToolStripMenuItem");
            this.openExistingCustomizableFileToolStripMenuItem.Name = "openExistingCustomizableFileToolStripMenuItem";
            // 
            // mnuDatamanagement
            // 
            this.mnuDatamanagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDatabaseControlPanel,
            this.menuItemDatabaseMaintenance});
            this.mnuDatamanagement.Name = "mnuDatamanagement";
            resources.ApplyResources(this.mnuDatamanagement, "mnuDatamanagement");
            // 
            // menuItemDatabaseControlPanel
            // 
            this.menuItemDatabaseControlPanel.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            resources.ApplyResources(this.menuItemDatabaseControlPanel, "menuItemDatabaseControlPanel");
            this.menuItemDatabaseControlPanel.Name = "menuItemDatabaseControlPanel";
            this.menuItemDatabaseControlPanel.Click += new System.EventHandler(this.menuItemBackupData_Click);
            // 
            // menuItemDatabaseMaintenance
            // 
            resources.ApplyResources(this.menuItemDatabaseMaintenance, "menuItemDatabaseMaintenance");
            this.menuItemDatabaseMaintenance.Name = "menuItemDatabaseMaintenance";
            this.menuItemDatabaseMaintenance.Click += new System.EventHandler(this.menuItemDatabaseMaintenance_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Name = "mnuWindow";
            resources.ApplyResources(this.mnuWindow, "mnuWindow");
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAboutOctopus,
            this.octopusForumToolStripMenuItem,
            this.questionnaireToolStripMenuItem,
            this.userGuideToolStripMenuItem,
            this.wIKIHelpToolStripMenuItem});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // menuItemAboutOctopus
            // 
            resources.ApplyResources(this.menuItemAboutOctopus, "menuItemAboutOctopus");
            this.menuItemAboutOctopus.Name = "menuItemAboutOctopus";
            this.menuItemAboutOctopus.Click += new System.EventHandler(this.menuItemAboutOctopus_Click);
            // 
            // octopusForumToolStripMenuItem
            // 
            this.octopusForumToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_pastille_personne;
            this.octopusForumToolStripMenuItem.Name = "octopusForumToolStripMenuItem";
            resources.ApplyResources(this.octopusForumToolStripMenuItem, "octopusForumToolStripMenuItem");
            this.octopusForumToolStripMenuItem.Click += new System.EventHandler(this.octopusForumToolStripMenuItem_Click);
            // 
            // questionnaireToolStripMenuItem
            // 
            this.questionnaireToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_doc;
            this.questionnaireToolStripMenuItem.Name = "questionnaireToolStripMenuItem";
            resources.ApplyResources(this.questionnaireToolStripMenuItem, "questionnaireToolStripMenuItem");
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.theme1_1_doc;
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            resources.ApplyResources(this.userGuideToolStripMenuItem, "userGuideToolStripMenuItem");
            // 
            // wIKIHelpToolStripMenuItem
            // 
            this.wIKIHelpToolStripMenuItem.Image = global::Foms.GUI.Properties.Resources.languages;
            this.wIKIHelpToolStripMenuItem.Name = "wIKIHelpToolStripMenuItem";
            resources.ApplyResources(this.wIKIHelpToolStripMenuItem, "wIKIHelpToolStripMenuItem");
            // 
            // imageListAlert
            // 
            this.imageListAlert.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAlert.ImageStream")));
            this.imageListAlert.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListAlert.Images.SetKeyName(0, "");
            this.imageListAlert.Images.SetKeyName(1, "");
            this.imageListAlert.Images.SetKeyName(2, "");
            this.imageListAlert.Images.SetKeyName(3, "");
            this.imageListAlert.Images.SetKeyName(4, "");
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClients,
            this.mnuContracts,
            this.mnuAccounting,
            this.mnuConfiguration,
            this.mView,
            this.mnuDatamanagement,
            this.mnuWindow,
            this.mnuHelp});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.MdiWindowListItem = this.mnuWindow;
            this.mainMenu.Name = "mainMenu";
            // 
            // mView
            // 
            this.mView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAuditTrail,
            this.miReports});
            this.mView.Name = "mView";
            resources.ApplyResources(this.mView, "mView");
            // 
            // miAuditTrail
            // 
            this.miAuditTrail.Name = "miAuditTrail";
            resources.ApplyResources(this.miAuditTrail, "miAuditTrail");
            // 
            // miReports
            // 
            this.miReports.Name = "miReports";
            resources.ApplyResources(this.miReports, "miReports");
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // mainStripToolBar
            // 
            this.mainStripToolBar.BackColor = System.Drawing.SystemColors.Control;
            this.mainStripToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainStripToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButNew,
            this.toolStripSeparator1,
            this.toolBarButtonSearchPerson,
            this.toolBarButtonSearchContract,
            this.toolBarButtonReports,
            this.toolStripLabel1,
            this.toolStripButtonSearchProject});
            resources.ApplyResources(this.mainStripToolBar, "mainStripToolBar");
            this.mainStripToolBar.Name = "mainStripToolBar";
            // 
            // toolBarButNew
            // 
            this.toolBarButNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButtonPerson,
            this.toolBarButtonNewGroup,
            this.tbbtnNewVillage,
            this.corporateToolStripMenuItem});
            resources.ApplyResources(this.toolBarButNew, "toolBarButNew");
            this.toolBarButNew.Name = "toolBarButNew";
            this.toolBarButNew.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // toolBarButtonPerson
            // 
            resources.ApplyResources(this.toolBarButtonPerson, "toolBarButtonPerson");
            this.toolBarButtonPerson.Name = "toolBarButtonPerson";
            this.toolBarButtonPerson.Click += new System.EventHandler(this.toolBarButtonPerson_Click);
            // 
            // toolBarButtonNewGroup
            // 
            resources.ApplyResources(this.toolBarButtonNewGroup, "toolBarButtonNewGroup");
            this.toolBarButtonNewGroup.Name = "toolBarButtonNewGroup";
            // 
            // tbbtnNewVillage
            // 
            resources.ApplyResources(this.tbbtnNewVillage, "tbbtnNewVillage");
            this.tbbtnNewVillage.Name = "tbbtnNewVillage";
            // 
            // corporateToolStripMenuItem
            // 
            resources.ApplyResources(this.corporateToolStripMenuItem, "corporateToolStripMenuItem");
            this.corporateToolStripMenuItem.Name = "corporateToolStripMenuItem";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolBarButtonSearchPerson
            // 
            resources.ApplyResources(this.toolBarButtonSearchPerson, "toolBarButtonSearchPerson");
            this.toolBarButtonSearchPerson.Name = "toolBarButtonSearchPerson";
            // 
            // toolBarButtonSearchContract
            // 
            resources.ApplyResources(this.toolBarButtonSearchContract, "toolBarButtonSearchContract");
            this.toolBarButtonSearchContract.Name = "toolBarButtonSearchContract";
            // 
            // toolBarButtonReports
            // 
            this.toolBarButtonReports.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolBarButtonReports, "toolBarButtonReports");
            this.toolBarButtonReports.Name = "toolBarButtonReports";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // toolStripButtonSearchProject
            // 
            resources.ApplyResources(this.toolStripButtonSearchProject, "toolStripButtonSearchProject");
            this.toolStripButtonSearchProject.Name = "toolStripButtonSearchProject";
            // 
            // toolBarLblVersion
            // 
            this.toolBarLblVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolBarLblVersion, "toolBarLblVersion");
            this.toolBarLblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(156)))));
            this.toolBarLblVersion.Name = "toolBarLblVersion";
            // 
            // mainStatusBar
            // 
            resources.ApplyResources(this.mainStatusBar, "mainStatusBar");
            this.mainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusBarLblUpdateVersion,
            this.mainStatusBarLblUserName,
            this.mainStatusBarLblDate,
            this.toolStripStatusLblBranchCode,
            this.mainStatusBarLblInfo,
            this.toolStripStatusLblDB});
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.mainStatusBar.ShowItemToolTips = true;
            this.mainStatusBar.SizingGrip = false;
            // 
            // mainStatusBarLblUpdateVersion
            // 
            resources.ApplyResources(this.mainStatusBarLblUpdateVersion, "mainStatusBarLblUpdateVersion");
            this.mainStatusBarLblUpdateVersion.Name = "mainStatusBarLblUpdateVersion";
            this.mainStatusBarLblUpdateVersion.Spring = true;
            // 
            // mainStatusBarLblUserName
            // 
            this.mainStatusBarLblUserName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            resources.ApplyResources(this.mainStatusBarLblUserName, "mainStatusBarLblUserName");
            this.mainStatusBarLblUserName.Name = "mainStatusBarLblUserName";
            // 
            // mainStatusBarLblDate
            // 
            this.mainStatusBarLblDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            resources.ApplyResources(this.mainStatusBarLblDate, "mainStatusBarLblDate");
            this.mainStatusBarLblDate.Name = "mainStatusBarLblDate";
            // 
            // toolStripStatusLblBranchCode
            // 
            this.toolStripStatusLblBranchCode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblBranchCode.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblBranchCode.Name = "toolStripStatusLblBranchCode";
            resources.ApplyResources(this.toolStripStatusLblBranchCode, "toolStripStatusLblBranchCode");
            // 
            // mainStatusBarLblInfo
            // 
            this.mainStatusBarLblInfo.BackColor = System.Drawing.Color.White;
            this.mainStatusBarLblInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.mainStatusBarLblInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            resources.ApplyResources(this.mainStatusBarLblInfo, "mainStatusBarLblInfo");
            this.mainStatusBarLblInfo.ForeColor = System.Drawing.Color.Black;
            this.mainStatusBarLblInfo.Name = "mainStatusBarLblInfo";
            // 
            // toolStripStatusLblDB
            // 
            this.toolStripStatusLblDB.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLblDB.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLblDB.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.toolStripStatusLblDB.Name = "toolStripStatusLblDB";
            resources.ApplyResources(this.toolStripStatusLblDB, "toolStripStatusLblDB");
            // 
            // bwAlerts
            // 
            this.bwAlerts.WorkerSupportsCancellation = true;
            // 
            // nIUpdateAvailable
            // 
            this.nIUpdateAvailable.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.nIUpdateAvailable, "nIUpdateAvailable");
            // 
            // openCustomizableFileDialog
            // 
            resources.ApplyResources(this.openCustomizableFileDialog, "openCustomizableFileDialog");
            // 
            // splitter6
            // 
            this.splitter6.AnimationDelay = 20;
            this.splitter6.AnimationStep = 20;
            this.splitter6.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.splitter6.ControlToHide = this.panelLeft;
            this.splitter6.ExpandParentForm = false;
            resources.ApplyResources(this.splitter6, "splitter6");
            this.splitter6.Name = "splitter6";
            this.splitter6.TabStop = false;
            this.splitter6.UseAnimations = false;
            this.splitter6.VisualStyle = Foms.GUI.UserControl.VisualStyles.Mozilla;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelLeft.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Controls.Add(this.tabFilter);
            this.panelLeft.Name = "panelLeft";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Name = "lblTitle";
            // 
            // tabFilter
            // 
            resources.ApplyResources(this.tabFilter, "tabFilter");
            this.tabFilter.Controls.Add(this.chkPendingSavings, 0, 4);
            this.tabFilter.Controls.Add(this.tbFilter, 1, 0);
            this.tabFilter.Controls.Add(this.lblFilter, 0, 0);
            this.tabFilter.Controls.Add(this.chkLateLoans, 0, 1);
            this.tabFilter.Controls.Add(this.chkPendingLoans, 0, 2);
            this.tabFilter.Controls.Add(this.chkOverdraftSavings, 0, 3);
            this.tabFilter.Name = "tabFilter";
            // 
            // chkPendingSavings
            // 
            resources.ApplyResources(this.chkPendingSavings, "chkPendingSavings");
            this.chkPendingSavings.Checked = true;
            this.chkPendingSavings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkPendingSavings, 2);
            this.chkPendingSavings.Name = "chkPendingSavings";
            this.chkPendingSavings.UseVisualStyleBackColor = true;
            // 
            // tbFilter
            // 
            this.tbFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            resources.ApplyResources(this.tbFilter, "tbFilter");
            this.tbFilter.Name = "tbFilter";
            // 
            // lblFilter
            // 
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Name = "lblFilter";
            // 
            // chkLateLoans
            // 
            resources.ApplyResources(this.chkLateLoans, "chkLateLoans");
            this.chkLateLoans.Checked = true;
            this.chkLateLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkLateLoans, 2);
            this.chkLateLoans.Name = "chkLateLoans";
            this.chkLateLoans.UseVisualStyleBackColor = true;
            // 
            // chkPendingLoans
            // 
            resources.ApplyResources(this.chkPendingLoans, "chkPendingLoans");
            this.chkPendingLoans.Checked = true;
            this.chkPendingLoans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkPendingLoans, 2);
            this.chkPendingLoans.Name = "chkPendingLoans";
            this.chkPendingLoans.UseVisualStyleBackColor = true;
            // 
            // chkOverdraftSavings
            // 
            resources.ApplyResources(this.chkOverdraftSavings, "chkOverdraftSavings");
            this.chkOverdraftSavings.Checked = true;
            this.chkOverdraftSavings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabFilter.SetColumnSpan(this.chkOverdraftSavings, 2);
            this.chkOverdraftSavings.Name = "chkOverdraftSavings";
            this.chkOverdraftSavings.UseVisualStyleBackColor = true;
            // 
            // LotrasmicMainWindowForm
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitter6);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.mainStripToolBar);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "LotrasmicMainWindowForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LotrasmicMainWindowForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainStripToolBar.ResumeLayout(false);
            this.mainStripToolBar.PerformLayout();
            this.mainStatusBar.ResumeLayout(false);
            this.mainStatusBar.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.tabFilter.ResumeLayout(false);
            this.tabFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alertBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private ToolStripStatusLabel toolStripStatusLblDB;
        private ToolStripMenuItem reasignToolStripMenuItem;


        private ToolStripMenuItem corporateToolStripMenuItem;
        private ToolStripMenuItem newCorporateToolStripMenuItem;
        private ToolStripButton toolStripButtonSearchProject;
        private ToolStripMenuItem questionnaireToolStripMenuItem;
        private ToolStripMenuItem tbbtnNewVillage;
        private ToolStripMenuItem mnuNewVillage;
        private System.ComponentModel.BackgroundWorker bwAlerts;
        private ToolStripMenuItem mView;
        private ToolStripMenuItem miAuditTrail;
        private ToolStripMenuItem creditClosureToolStripMenuItem;
        private ToolStripMenuItem savingClosureToolStripMenuItem;
        private Panel panelLeft;
        private Label lblTitle;
        private NotifyIcon nIUpdateAvailable;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem customizableExportImportToolStripMenuItem;
        private ToolStripMenuItem createCustomizableExportFileToolStripMenuItem;
        private ToolStripMenuItem createCustomizableImportFileToolStripMenuItem;
        private ToolStripMenuItem openExistingCustomizableFileToolStripMenuItem;
        private OpenFileDialog openCustomizableFileDialog;
        private ToolStripMenuItem exportImportCustomizableToolStripMenuItem;
        private BindingSource alertBindingSource;
        private ToolStripMenuItem userGuideToolStripMenuItem;
        private ToolStripMenuItem wIKIHelpToolStripMenuItem;
        private ToolStripMenuItem miReports;
        private Label lblFilter;
        private TextBox tbFilter;
        private CheckBox chkLateLoans;
        private TableLayoutPanel tabFilter;
        private ToolStripMenuItem changePasswordToolStripMenuItem;
        private CheckBox chkPendingLoans;
        private CheckBox chkOverdraftSavings;
        private CheckBox chkPendingSavings;


    }
}
