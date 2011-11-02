namespace Foms.GUI.Tools
{
    partial class frmDatabaseMaintenance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatabaseMaintenance));
            this.labelInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDatabaseUsage = new System.Windows.Forms.Label();
            this.butShrinkDatabase = new System.Windows.Forms.Button();
            this.timerDatabaseSize = new System.Windows.Forms.Timer(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonRunSQL = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            resources.ApplyResources(this.labelInfo, "labelInfo");
            this.labelInfo.Name = "labelInfo";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblDatabaseUsage);
            this.groupBox2.Controls.Add(this.butShrinkDatabase);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblDatabaseUsage
            // 
            resources.ApplyResources(this.lblDatabaseUsage, "lblDatabaseUsage");
            this.lblDatabaseUsage.Name = "lblDatabaseUsage";
            // 
            // butShrinkDatabase
            // 
            this.butShrinkDatabase.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.butShrinkDatabase, "butShrinkDatabase");
            this.butShrinkDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butShrinkDatabase.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.butShrinkDatabase.Name = "butShrinkDatabase";
            this.butShrinkDatabase.UseVisualStyleBackColor = false;
            this.butShrinkDatabase.Click += new System.EventHandler(this.butShrinkDatabase_Click);
            // 
            // timerDatabaseSize
            // 
            this.timerDatabaseSize.Interval = 1000;
            this.timerDatabaseSize.Tick += new System.EventHandler(this.timerDatabaseSize_Tick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonCancel.Image = global::Foms.GUI.Properties.Resources.theme1_1_bouton_close;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.buttonRunSQL);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // buttonRunSQL
            // 
            this.buttonRunSQL.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonRunSQL, "buttonRunSQL");
            this.buttonRunSQL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.buttonRunSQL.Image = global::Foms.GUI.Properties.Resources.thame1_1_database;
            this.buttonRunSQL.Name = "buttonRunSQL";
            this.buttonRunSQL.UseVisualStyleBackColor = false;
            this.buttonRunSQL.Click += new System.EventHandler(this.buttonRunSQL_Click);
            // 
            // frmDatabaseMaintenance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_gris;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDatabaseMaintenance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDatabaseMaintenance_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDatabaseUsage;
        private System.Windows.Forms.Button butShrinkDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerDatabaseSize;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonRunSQL;
    }
}