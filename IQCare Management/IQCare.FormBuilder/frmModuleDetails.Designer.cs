namespace IQCare.FormBuilder
{
    partial class frmModuleDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModuleDetails));
            this.pnlModuleDetails = new System.Windows.Forms.Panel();
            this.dgwFieldDetails = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlFieldDetails = new System.Windows.Forms.Panel();
            this.txtstartingnumber = new System.Windows.Forms.TextBox();
            this.lblstartingnumber = new System.Windows.Forms.Label();
            this.txtlabel = new System.Windows.Forms.TextBox();
            this.lblIdentifierLabel = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.btnSumit = new System.Windows.Forms.Button();
            this.pnlModuleName = new System.Windows.Forms.Panel();
            this.chkEnroll = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textDisplayName = new System.Windows.Forms.TextBox();
            this.lblModuleName = new System.Windows.Forms.Label();
            this.txtModuleName = new System.Windows.Forms.TextBox();
            this.btnBusinessRule = new System.Windows.Forms.Button();
            this.pnlModuleDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).BeginInit();
            this.pnlFieldDetails.SuspendLayout();
            this.pnlModuleName.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlModuleDetails
            // 
            this.pnlModuleDetails.Controls.Add(this.dgwFieldDetails);
            this.pnlModuleDetails.Controls.Add(this.btnClose);
            this.pnlModuleDetails.Controls.Add(this.btnSave);
            this.pnlModuleDetails.Controls.Add(this.pnlFieldDetails);
            this.pnlModuleDetails.Controls.Add(this.pnlModuleName);
            this.pnlModuleDetails.Location = new System.Drawing.Point(1, 1);
            this.pnlModuleDetails.Name = "pnlModuleDetails";
            this.pnlModuleDetails.Size = new System.Drawing.Size(1009, 506);
            this.pnlModuleDetails.TabIndex = 0;
            this.pnlModuleDetails.Tag = "pnlPanel";
            // 
            // dgwFieldDetails
            // 
            this.dgwFieldDetails.AllowUserToResizeColumns = false;
            this.dgwFieldDetails.AllowUserToResizeRows = false;
            this.dgwFieldDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwFieldDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwFieldDetails.Location = new System.Drawing.Point(13, 222);
            this.dgwFieldDetails.Name = "dgwFieldDetails";
            this.dgwFieldDetails.Size = new System.Drawing.Size(941, 236);
            this.dgwFieldDetails.TabIndex = 8;
            this.dgwFieldDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFieldDetails_CellDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(442, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 21);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(243, 477);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 21);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlFieldDetails
            // 
            this.pnlFieldDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFieldDetails.Controls.Add(this.txtstartingnumber);
            this.pnlFieldDetails.Controls.Add(this.lblstartingnumber);
            this.pnlFieldDetails.Controls.Add(this.txtlabel);
            this.pnlFieldDetails.Controls.Add(this.lblIdentifierLabel);
            this.pnlFieldDetails.Controls.Add(this.lblFieldType);
            this.pnlFieldDetails.Controls.Add(this.txtFieldName);
            this.pnlFieldDetails.Controls.Add(this.lblFieldName);
            this.pnlFieldDetails.Controls.Add(this.cmbFieldType);
            this.pnlFieldDetails.Controls.Add(this.btnSumit);
            this.pnlFieldDetails.Location = new System.Drawing.Point(13, 95);
            this.pnlFieldDetails.Name = "pnlFieldDetails";
            this.pnlFieldDetails.Size = new System.Drawing.Size(941, 121);
            this.pnlFieldDetails.TabIndex = 5;
            this.pnlFieldDetails.Tag = "";
            // 
            // txtstartingnumber
            // 
            this.txtstartingnumber.Location = new System.Drawing.Point(616, 62);
            this.txtstartingnumber.MaxLength = 10;
            this.txtstartingnumber.Name = "txtstartingnumber";
            this.txtstartingnumber.Size = new System.Drawing.Size(100, 20);
            this.txtstartingnumber.TabIndex = 6;
            this.txtstartingnumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstartingnumber_KeyPress);
            // 
            // lblstartingnumber
            // 
            this.lblstartingnumber.AutoSize = true;
            this.lblstartingnumber.Location = new System.Drawing.Point(510, 65);
            this.lblstartingnumber.Name = "lblstartingnumber";
            this.lblstartingnumber.Size = new System.Drawing.Size(83, 13);
            this.lblstartingnumber.TabIndex = 14;
            this.lblstartingnumber.Tag = "lblLabel";
            this.lblstartingnumber.Text = "Starting Number";
            // 
            // txtlabel
            // 
            this.txtlabel.Location = new System.Drawing.Point(144, 36);
            this.txtlabel.MaxLength = 50;
            this.txtlabel.Name = "txtlabel";
            this.txtlabel.Size = new System.Drawing.Size(340, 20);
            this.txtlabel.TabIndex = 4;
            // 
            // lblIdentifierLabel
            // 
            this.lblIdentifierLabel.AutoSize = true;
            this.lblIdentifierLabel.Location = new System.Drawing.Point(12, 39);
            this.lblIdentifierLabel.Name = "lblIdentifierLabel";
            this.lblIdentifierLabel.Size = new System.Drawing.Size(76, 13);
            this.lblIdentifierLabel.TabIndex = 12;
            this.lblIdentifierLabel.Tag = "lblLabel";
            this.lblIdentifierLabel.Text = "Identifier Label";
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(12, 65);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(74, 13);
            this.lblFieldType.TabIndex = 8;
            this.lblFieldType.Tag = "lblLabel";
            this.lblFieldType.Text = "Identifier Type";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(144, 10);
            this.txtFieldName.MaxLength = 50;
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(340, 20);
            this.txtFieldName.TabIndex = 3;
            this.txtFieldName.Tag = "txtTextBox";
            this.txtFieldName.Leave += new System.EventHandler(this.txtFieldName_Leave);
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(12, 10);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(78, 13);
            this.lblFieldName.TabIndex = 9;
            this.lblFieldName.Tag = "lblLabel";
            this.lblFieldName.Text = "Identifier Name";
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldType.FormattingEnabled = true;
            this.cmbFieldType.Location = new System.Drawing.Point(144, 62);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(340, 21);
            this.cmbFieldType.TabIndex = 5;
            this.cmbFieldType.Tag = "ddlDropDownList";
            this.cmbFieldType.SelectedIndexChanged += new System.EventHandler(this.cmbFieldType_SelectedIndexChanged);
            // 
            // btnSumit
            // 
            this.btnSumit.Location = new System.Drawing.Point(230, 89);
            this.btnSumit.Name = "btnSumit";
            this.btnSumit.Size = new System.Drawing.Size(141, 21);
            this.btnSumit.TabIndex = 7;
            this.btnSumit.Tag = "btnH25WFlexi";
            this.btnSumit.Text = "Submit";
            this.btnSumit.UseVisualStyleBackColor = true;
            this.btnSumit.Click += new System.EventHandler(this.btnSumit_Click);
            // 
            // pnlModuleName
            // 
            this.pnlModuleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModuleName.Controls.Add(this.btnBusinessRule);
            this.pnlModuleName.Controls.Add(this.chkEnroll);
            this.pnlModuleName.Controls.Add(this.label2);
            this.pnlModuleName.Controls.Add(this.textDisplayName);
            this.pnlModuleName.Controls.Add(this.lblModuleName);
            this.pnlModuleName.Controls.Add(this.txtModuleName);
            this.pnlModuleName.Location = new System.Drawing.Point(12, 13);
            this.pnlModuleName.Name = "pnlModuleName";
            this.pnlModuleName.Size = new System.Drawing.Size(942, 76);
            this.pnlModuleName.TabIndex = 4;
            this.pnlModuleName.Tag = "";
            // 
            // chkEnroll
            // 
            this.chkEnroll.AutoSize = true;
            this.chkEnroll.Location = new System.Drawing.Point(540, 16);
            this.chkEnroll.Name = "chkEnroll";
            this.chkEnroll.Size = new System.Drawing.Size(110, 17);
            this.chkEnroll.TabIndex = 5;
            this.chkEnroll.Text = "Can Enroll Patient";
            this.chkEnroll.UseVisualStyleBackColor = true;
            this.chkEnroll.CheckedChanged += new System.EventHandler(this.chkEnroll_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Tag = "lblDisplayName";
            this.label2.Text = "Display Name";
            // 
            // textDisplayName
            // 
            this.textDisplayName.AcceptsReturn = true;
            this.textDisplayName.Location = new System.Drawing.Point(144, 45);
            this.textDisplayName.MaxLength = 50;
            this.textDisplayName.Name = "textDisplayName";
            this.textDisplayName.Size = new System.Drawing.Size(340, 20);
            this.textDisplayName.TabIndex = 2;
            this.textDisplayName.Tag = "txtDisplayName";
            this.textDisplayName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textDisplayName_KeyPress);
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.Location = new System.Drawing.Point(12, 16);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(74, 13);
            this.lblModuleName.TabIndex = 0;
            this.lblModuleName.Tag = "lblLabel";
            this.lblModuleName.Text = "Service Name";
            // 
            // txtModuleName
            // 
            this.txtModuleName.AcceptsReturn = true;
            this.txtModuleName.Location = new System.Drawing.Point(144, 16);
            this.txtModuleName.MaxLength = 50;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.Size = new System.Drawing.Size(340, 20);
            this.txtModuleName.TabIndex = 1;
            this.txtModuleName.Tag = "txtTextBox";
            this.txtModuleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModuleName_KeyPress);
            this.txtModuleName.Leave += new System.EventHandler(this.txtModuleName_Leave);
            // 
            // btnBusinessRule
            // 
            this.btnBusinessRule.Location = new System.Drawing.Point(540, 45);
            this.btnBusinessRule.Name = "btnBusinessRule";
            this.btnBusinessRule.Size = new System.Drawing.Size(191, 21);
            this.btnBusinessRule.TabIndex = 8;
            this.btnBusinessRule.Tag = "btnH25SABR";
            this.btnBusinessRule.Text = "Service Area Business Rules";
            this.btnBusinessRule.UseVisualStyleBackColor = true;
            this.btnBusinessRule.Click += new System.EventHandler(this.btnBusinessRule_Click);
            // 
            // frmModuleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 511);
            this.Controls.Add(this.pnlModuleDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModuleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Service ";
            this.Load += new System.EventHandler(this.frmModuleDetails_Load);
            this.pnlModuleDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).EndInit();
            this.pnlFieldDetails.ResumeLayout(false);
            this.pnlFieldDetails.PerformLayout();
            this.pnlModuleName.ResumeLayout(false);
            this.pnlModuleName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlModuleDetails;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlFieldDetails;
        private System.Windows.Forms.Panel pnlModuleName;
        private System.Windows.Forms.TextBox txtModuleName;
        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.Button btnSumit;
        private System.Windows.Forms.ComboBox cmbFieldType;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.DataGridView dgwFieldDetails;
        private System.Windows.Forms.TextBox txtlabel;
        private System.Windows.Forms.Label lblIdentifierLabel;
        private System.Windows.Forms.TextBox txtstartingnumber;
        private System.Windows.Forms.Label lblstartingnumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDisplayName;
        private System.Windows.Forms.CheckBox chkEnroll;
        private System.Windows.Forms.Button btnBusinessRule;
    }
}