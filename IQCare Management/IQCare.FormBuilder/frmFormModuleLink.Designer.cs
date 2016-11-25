namespace IQCare.FormBuilder
{
    partial class frmFormModuleLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormModuleLink));
            this.cmbModuleName = new System.Windows.Forms.ComboBox();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.chklistFormName = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlModuleName = new System.Windows.Forms.Panel();
            this.pnlFormName = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlModuleName.SuspendLayout();
            this.pnlFormName.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbModuleName
            // 
            this.cmbModuleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModuleName.FormattingEnabled = true;
            this.cmbModuleName.Items.AddRange(new object[] {
            ""});
            this.cmbModuleName.Location = new System.Drawing.Point(3, 12);
            this.cmbModuleName.Name = "cmbModuleName";
            this.cmbModuleName.Size = new System.Drawing.Size(296, 21);
            this.cmbModuleName.TabIndex = 1;
            this.cmbModuleName.Tag = "ddlDropDownList";
            this.cmbModuleName.SelectedIndexChanged += new System.EventHandler(this.cmbModuleName_SelectedIndexChanged);
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuleName.Location = new System.Drawing.Point(20, 22);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(147, 15);
            this.lblModuleName.TabIndex = 0;
            this.lblModuleName.Text = "Selected Service Area";
            // 
            // chklistFormName
            // 
            this.chklistFormName.CheckOnClick = true;
            this.chklistFormName.FormattingEnabled = true;
            this.chklistFormName.Location = new System.Drawing.Point(9, 12);
            this.chklistFormName.Name = "chklistFormName";
            this.chklistFormName.Size = new System.Drawing.Size(365, 304);
            this.chklistFormName.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(286, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 21);
            this.btnSave.TabIndex = 17;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(418, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 21);
            this.btnClose.TabIndex = 18;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlModuleName
            // 
            this.pnlModuleName.Controls.Add(this.cmbModuleName);
            this.pnlModuleName.Location = new System.Drawing.Point(23, 40);
            this.pnlModuleName.Name = "pnlModuleName";
            this.pnlModuleName.Size = new System.Drawing.Size(325, 52);
            this.pnlModuleName.TabIndex = 19;
            // 
            // pnlFormName
            // 
            this.pnlFormName.Controls.Add(this.chklistFormName);
            this.pnlFormName.Location = new System.Drawing.Point(430, 40);
            this.pnlFormName.Name = "pnlFormName";
            this.pnlFormName.Size = new System.Drawing.Size(385, 324);
            this.pnlFormName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(427, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Link Forms";
            // 
            // frmFormModuleLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 431);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlFormName);
            this.Controls.Add(this.lblModuleName);
            this.Controls.Add(this.pnlModuleName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormModuleLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Special Form Linking";
            this.Load += new System.EventHandler(this.frmModulelink_Load);
            this.pnlModuleName.ResumeLayout(false);
            this.pnlFormName.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbModuleName;
        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.CheckedListBox chklistFormName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlModuleName;
        private System.Windows.Forms.Panel pnlFormName;
        private System.Windows.Forms.Label label1;
    }
}