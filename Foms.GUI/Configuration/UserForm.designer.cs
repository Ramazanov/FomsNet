﻿using System.ComponentModel;
using System.Windows.Forms;
using Foms.GUI.UserControl;

namespace Foms.GUI.Configuration
{
    public partial class UserForm
    {
        private Label lblRole;
        private TextBox txbPassword;
        private TextBox txbFirstname;
        private TextBox txbLastname;
        private TextBox txbUsername;
        private Label lblPassword;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblUsername;
        private ComboBox cmbRoles;
        private Container components = null;
        private GroupBox gbAddUser;
        private ListView lvUsers;
        private ColumnHeader colUsername;
        private ColumnHeader colRole;
        private ColumnHeader colFirstname;
        private TextBox txbMail;
        private Label lblMail;
        private ColumnHeader cHMail;
        private TextBox txbConfirmPassword;
        private Label lbComfirmPassword;
        private ColumnHeader colLastname;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.lvUsers = new System.Windows.Forms.ListView();
            this.colUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRole = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFirstname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLastname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cHMail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabButtons = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new Foms.GUI.UserControl.SweetButton();
            this.btnSave = new Foms.GUI.UserControl.SweetButton();
            this.btnDelete = new Foms.GUI.UserControl.SweetButton();
            this.gbAddUser = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.txbMail = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txbConfirmPassword = new System.Windows.Forms.TextBox();
            this.txbUsername = new System.Windows.Forms.TextBox();
            this.lbComfirmPassword = new System.Windows.Forms.Label();
            this.txbLastname = new System.Windows.Forms.TextBox();
            this.txbFirstname = new System.Windows.Forms.TextBox();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbAddUser.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvUsers
            // 
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUsername,
            this.colRole,
            this.colFirstname,
            this.colLastname,
            this.cHMail});
            resources.ApplyResources(this.lvUsers, "lvUsers");
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.GridLines = true;
            this.lvUsers.HideSelection = false;
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.SelectedIndexChanged += new System.EventHandler(this.OnSelectedUserChanged);
            this.lvUsers.Click += new System.EventHandler(this.listViewUsers_Click);
            // 
            // colUsername
            // 
            resources.ApplyResources(this.colUsername, "colUsername");
            // 
            // colRole
            // 
            resources.ApplyResources(this.colRole, "colRole");
            // 
            // colFirstname
            // 
            resources.ApplyResources(this.colFirstname, "colFirstname");
            // 
            // colLastname
            // 
            resources.ApplyResources(this.colLastname, "colLastname");
            // 
            // cHMail
            // 
            resources.ApplyResources(this.cHMail, "cHMail");
            // 
            // tabButtons
            // 
            resources.ApplyResources(this.tabButtons, "tabButtons");
            this.tabButtons.Name = "tabButtons";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.btnNew);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNew.Icon = Foms.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnNew.Menu = null;
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Icon = Foms.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnSave.Menu = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.Icon = Foms.GUI.UserControl.SweetButton.ButtonIcon.None;
            this.btnDelete.Menu = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.OnDeleteClick);
            // 
            // gbAddUser
            // 
            this.gbAddUser.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            resources.ApplyResources(this.gbAddUser, "gbAddUser");
            this.gbAddUser.Controls.Add(this.tableLayoutPanel);
            this.gbAddUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.gbAddUser.Name = "gbAddUser";
            this.gbAddUser.TabStop = false;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.cmbRoles, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.txbMail, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.lblRole, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.txbConfirmPassword, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.txbUsername, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lbComfirmPassword, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txbLastname, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.txbFirstname, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.lblMail, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.lblPassword, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txbPassword, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblFirstName, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.lblLastName, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.lblUsername, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // cmbRoles
            // 
            this.cmbRoles.DisplayMember = "Role.RoleName";
            this.cmbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.cmbRoles, "cmbRoles");
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.SelectionChangeCommitted += new System.EventHandler(this.comboBoxRoles_SelectionChangeCommitted);
            // 
            // txbMail
            // 
            resources.ApplyResources(this.txbMail, "txbMail");
            this.txbMail.Name = "txbMail";
            this.txbMail.TextChanged += new System.EventHandler(this.textBoxMail_TextChanged);
            // 
            // lblRole
            // 
            this.lblRole.BackColor = System.Drawing.Color.Transparent;
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.lblRole, "lblRole");
            this.lblRole.Name = "lblRole";
            // 
            // txbConfirmPassword
            // 
            resources.ApplyResources(this.txbConfirmPassword, "txbConfirmPassword");
            this.txbConfirmPassword.Name = "txbConfirmPassword";
            // 
            // txbUsername
            // 
            resources.ApplyResources(this.txbUsername, "txbUsername");
            this.txbUsername.Name = "txbUsername";
            this.txbUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
            // 
            // lbComfirmPassword
            // 
            this.lbComfirmPassword.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbComfirmPassword, "lbComfirmPassword");
            this.lbComfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lbComfirmPassword.Name = "lbComfirmPassword";
            // 
            // txbLastname
            // 
            resources.ApplyResources(this.txbLastname, "txbLastname");
            this.txbLastname.Name = "txbLastname";
            this.txbLastname.TextChanged += new System.EventHandler(this.textBoxLastname_TextChanged);
            // 
            // txbFirstname
            // 
            resources.ApplyResources(this.txbFirstname, "txbFirstname");
            this.txbFirstname.Name = "txbFirstname";
            this.txbFirstname.TextChanged += new System.EventHandler(this.textBoxFirstname_TextChanged);
            // 
            // lblMail
            // 
            this.lblMail.BackColor = System.Drawing.Color.Transparent;
            this.lblMail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.lblMail, "lblMail");
            this.lblMail.Name = "lblMail";
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblPassword.Name = "lblPassword";
            // 
            // txbPassword
            // 
            resources.ApplyResources(this.txbPassword, "txbPassword");
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            this.txbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
            // 
            // lblFirstName
            // 
            this.lblFirstName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblFirstName, "lblFirstName");
            this.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblFirstName.Name = "lblFirstName";
            // 
            // lblLastName
            // 
            this.lblLastName.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblLastName, "lblLastName");
            this.lblLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblLastName.Name = "lblLastName";
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblUsername, "lblUsername");
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.lblUsername.Name = "lblUsername";
            // 
            // UserForm
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lvUsers);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tabButtons);
            this.Controls.Add(this.gbAddUser);
            this.Name = "UserForm";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.Controls.SetChildIndex(this.gbAddUser, 0);
            this.Controls.SetChildIndex(this.tabButtons, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.lvUsers, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbAddUser.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tabButtons;
        private FlowLayoutPanel flowLayoutPanel1;
        private SweetButton btnNew;
        private SweetButton btnSave;
        private SweetButton btnDelete;
        private TableLayoutPanel tableLayoutPanel;
    }
}
