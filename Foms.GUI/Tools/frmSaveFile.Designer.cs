namespace Foms.GUI.Tools
{
    partial class frmSaveFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveFile));
            this.tbPath = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timerInput = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblWarning = new System.Windows.Forms.Label();
            this.butDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.AccessibleDescription = null;
            this.tbPath.AccessibleName = null;
            resources.ApplyResources(this.tbPath, "tbPath");
            this.tbPath.BackgroundImage = null;
            this.tbPath.Font = null;
            this.tbPath.Name = "tbPath";
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // butCancel
            // 
            this.butCancel.AccessibleDescription = null;
            this.butCancel.AccessibleName = null;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = false;
            // 
            // butSave
            // 
            this.butSave.AccessibleDescription = null;
            this.butSave.AccessibleName = null;
            resources.ApplyResources(this.butSave, "butSave");
            this.butSave.BackColor = System.Drawing.Color.Gainsboro;
            this.butSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butSave.Name = "butSave";
            this.butSave.UseVisualStyleBackColor = false;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label3.Name = "label3";
            // 
            // button2
            // 
            this.button2.AccessibleDescription = null;
            this.button2.AccessibleName = null;
            resources.ApplyResources(this.button2, "button2");
            this.button2.BackColor = System.Drawing.Color.Gainsboro;
            this.button2.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.button2.Image = global::Foms.GUI.Properties.Resources.theme1_1_search;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.label1.Name = "label1";
            // 
            // tbFileName
            // 
            this.tbFileName.AccessibleDescription = null;
            this.tbFileName.AccessibleName = null;
            resources.ApplyResources(this.tbFileName, "tbFileName");
            this.tbFileName.BackgroundImage = null;
            this.tbFileName.Font = null;
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.TextChanged += new System.EventHandler(this.tbFileName_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = null;
            this.lblTitle.AccessibleName = null;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Name = "lblTitle";
            // 
            // timerInput
            // 
            this.timerInput.Interval = 300;
            this.timerInput.Tick += new System.EventHandler(this.timerInput_Tick);
            // 
            // folderBrowserDialog
            // 
            resources.ApplyResources(this.folderBrowserDialog, "folderBrowserDialog");
            // 
            // lblWarning
            // 
            this.lblWarning.AccessibleDescription = null;
            this.lblWarning.AccessibleName = null;
            resources.ApplyResources(this.lblWarning, "lblWarning");
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblWarning.Font = null;
            this.lblWarning.Name = "lblWarning";
            // 
            // butDefault
            // 
            this.butDefault.AccessibleDescription = null;
            this.butDefault.AccessibleName = null;
            resources.ApplyResources(this.butDefault, "butDefault");
            this.butDefault.BackColor = System.Drawing.Color.Gainsboro;
            this.butDefault.BackgroundImage = global::Foms.GUI.Properties.Resources.theme1_1_fond_bouton;
            this.butDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(56)))));
            this.butDefault.Name = "butDefault";
            this.butDefault.UseVisualStyleBackColor = false;
            // 
            // frmSaveFile
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butDefault);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Font = null;
            this.Name = "frmSaveFile";
            this.Load += new System.EventHandler(this.frmSaveFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Timer timerInput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button butDefault;
    }
}