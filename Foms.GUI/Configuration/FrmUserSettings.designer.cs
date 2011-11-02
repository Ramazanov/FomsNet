﻿using System.Windows.Forms;

namespace Foms.GUI.Configuration
{
    public partial class FrmUserSettings
    {
        private GroupBox groupBoxFolders;
        private Label label1;
        private TextBox txtBackupPath;
        private TextBox txtExportConsoPath;
        private Label label2;
        private Button btnClose;
        private SaveFileDialog saveDiag;
        private GroupBox groupBox4;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private Button buttonSave;
        private LinkLabel llHelp;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private RadioButton rdbFrench;
        private RadioButton rdbRussian;
        private RadioButton rdbEnglish;
        private CheckBox cbAutoUpdate;
        private RadioButton rbKyrgyz;
        private PictureBox pictureBox4;
        private RadioButton rbTadjik;
        private PictureBox pictureBox5;
        private FolderBrowserDialog fBDPath;
        private Button buttonFindBackupPath;
        private Button buttonFindExportPath;
        private RadioButton rbSpanish;
        private PictureBox pictureBox6;

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

        #region Code génér?par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserSettings));
            this.groupBoxFolders = new System.Windows.Forms.GroupBox();
            this.buttonFindExportPath = new System.Windows.Forms.Button();
            this.buttonFindBackupPath = new System.Windows.Forms.Button();
            this.txtExportConsoPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveDiag = new System.Windows.Forms.SaveFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbTadjik = new System.Windows.Forms.RadioButton();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.rbKyrgyz = new System.Windows.Forms.RadioButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.rdbFrench = new System.Windows.Forms.RadioButton();
            this.rdbRussian = new System.Windows.Forms.RadioButton();
            this.rbSpanish = new System.Windows.Forms.RadioButton();
            this.rdbEnglish = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.llHelp = new System.Windows.Forms.LinkLabel();
            this.cbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.fBDPath = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxFolders.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxFolders
            // 
            this.groupBoxFolders.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxFolders.Controls.Add(this.buttonFindExportPath);
            this.groupBoxFolders.Controls.Add(this.buttonFindBackupPath);
            this.groupBoxFolders.Controls.Add(this.txtExportConsoPath);
            this.groupBoxFolders.Controls.Add(this.label2);
            this.groupBoxFolders.Controls.Add(this.txtBackupPath);
            this.groupBoxFolders.Controls.Add(this.label1);
            this.groupBoxFolders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.groupBoxFolders, "groupBoxFolders");
            this.groupBoxFolders.Name = "groupBoxFolders";
            this.groupBoxFolders.TabStop = false;
            // 
            // buttonFindExportPath
            // 
            resources.ApplyResources(this.buttonFindExportPath, "buttonFindExportPath");
            this.buttonFindExportPath.Name = "buttonFindExportPath";
            this.buttonFindExportPath.UseVisualStyleBackColor = true;
            this.buttonFindExportPath.Click += new System.EventHandler(this.buttonFindExportPath_Click);
            // 
            // buttonFindBackupPath
            // 
            resources.ApplyResources(this.buttonFindBackupPath, "buttonFindBackupPath");
            this.buttonFindBackupPath.Name = "buttonFindBackupPath";
            this.buttonFindBackupPath.UseVisualStyleBackColor = true;
            this.buttonFindBackupPath.Click += new System.EventHandler(this.buttonFindBackupPath_Click);
            // 
            // txtExportConsoPath
            // 
            resources.ApplyResources(this.txtExportConsoPath, "txtExportConsoPath");
            this.txtExportConsoPath.Name = "txtExportConsoPath";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBackupPath
            // 
            resources.ApplyResources(this.txtBackupPath, "txtBackupPath");
            this.txtBackupPath.Name = "txtBackupPath";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // saveDiag
            // 
            this.saveDiag.DefaultExt = "udl";
            this.saveDiag.FileName = "octopus.udl";
            resources.ApplyResources(this.saveDiag, "saveDiag");
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.rbTadjik);
            this.groupBox4.Controls.Add(this.pictureBox5);
            this.groupBox4.Controls.Add(this.rbKyrgyz);
            this.groupBox4.Controls.Add(this.pictureBox4);
            this.groupBox4.Controls.Add(this.rdbFrench);
            this.groupBox4.Controls.Add(this.rdbRussian);
            this.groupBox4.Controls.Add(this.rbSpanish);
            this.groupBox4.Controls.Add(this.rdbEnglish);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.pictureBox6);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // rbTadjik
            // 
            resources.ApplyResources(this.rbTadjik, "rbTadjik");
            this.rbTadjik.ForeColor = System.Drawing.Color.Gray;
            this.rbTadjik.Name = "rbTadjik";
            this.rbTadjik.Tag = "tg-Cyrl-TJ";
            // 
            // pictureBox5
            // 
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // rbKyrgyz
            // 
            resources.ApplyResources(this.rbKyrgyz, "rbKyrgyz");
            this.rbKyrgyz.Name = "rbKyrgyz";
            this.rbKyrgyz.Tag = "ky-KG";
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // rdbFrench
            // 
            resources.ApplyResources(this.rdbFrench, "rdbFrench");
            this.rdbFrench.Name = "rdbFrench";
            this.rdbFrench.Tag = "fr";
            // 
            // rdbRussian
            // 
            resources.ApplyResources(this.rdbRussian, "rdbRussian");
            this.rdbRussian.Name = "rdbRussian";
            this.rdbRussian.Tag = "ru-RU";
            // 
            // rbSpanish
            // 
            resources.ApplyResources(this.rbSpanish, "rbSpanish");
            this.rbSpanish.Name = "rbSpanish";
            this.rbSpanish.Tag = "es-ES";
            // 
            // rdbEnglish
            // 
            this.rdbEnglish.Checked = true;
            resources.ApplyResources(this.rdbEnglish, "rdbEnglish");
            this.rdbEnglish.Name = "rdbEnglish";
            this.rdbEnglish.TabStop = true;
            this.rdbEnglish.Tag = "en-US";
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Image = global::Foms.GUI.Properties.Resources.ru_small;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Foms.GUI.Properties.Resources.Spanish_Flag1;
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Foms.GUI.Properties.Resources.en_small;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Foms.GUI.Properties.Resources.fr_small;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // llHelp
            // 
            resources.ApplyResources(this.llHelp, "llHelp");
            this.llHelp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.llHelp.Name = "llHelp";
            this.llHelp.TabStop = true;
            this.llHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llHelp_LinkClicked);
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.cbAutoUpdate, "cbAutoUpdate");
            this.cbAutoUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.UseVisualStyleBackColor = false;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_save;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmUserSettings
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            this.Controls.Add(this.cbAutoUpdate);
            this.Controls.Add(this.llHelp);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxFolders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUserSettings";
            this.Load += new System.EventHandler(this.FrmAdmin_Load);
            this.groupBoxFolders.ResumeLayout(false);
            this.groupBoxFolders.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
