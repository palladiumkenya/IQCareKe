namespace IQCare.SCM
{
    partial class frmAdjustStockLevel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdjustStockLevel));
            this.basePanel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgwStockLevelDetails = new System.Windows.Forms.DataGridView();
            this.ddlPreparedBy = new System.Windows.Forms.ComboBox();
            this.lblPreparedby = new System.Windows.Forms.Label();
            this.ddlAuthoriseBy = new System.Windows.Forms.ComboBox();
            this.lblAuthorizedBy = new System.Windows.Forms.Label();
            this.ddlAdjustmentReason = new System.Windows.Forms.ComboBox();
            this.lblAdjReason = new System.Windows.Forms.Label();
            this.chkAdjReason = new System.Windows.Forms.CheckBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pnl_ButtonSubmit = new System.Windows.Forms.Panel();
            this.pnlTopPanel = new System.Windows.Forms.Panel();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblDateFeild = new System.Windows.Forms.Label();
            this.ddlStoreName = new System.Windows.Forms.ComboBox();
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.pnl_PreparedBy = new System.Windows.Forms.Panel();
            this.chkUpdateStockFlag = new System.Windows.Forms.CheckBox();
            this.basePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockLevelDetails)).BeginInit();
            this.pnl_ButtonSubmit.SuspendLayout();
            this.pnlTopPanel.SuspendLayout();
            this.pnl_PreparedBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // basePanel
            // 
            this.basePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.basePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.basePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.basePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.basePanel.Controls.Add(this.btnSave);
            this.basePanel.Controls.Add(this.btnClose);
            this.basePanel.Location = new System.Drawing.Point(0, 457);
            this.basePanel.Margin = new System.Windows.Forms.Padding(0);
            this.basePanel.Name = "basePanel";
            this.basePanel.Size = new System.Drawing.Size(934, 47);
            this.basePanel.TabIndex = 40;
            this.basePanel.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 41;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(771, 10);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 42;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgwStockLevelDetails
            // 
            this.dgwStockLevelDetails.AllowUserToAddRows = false;
            this.dgwStockLevelDetails.AllowUserToDeleteRows = false;
            this.dgwStockLevelDetails.AllowUserToResizeColumns = false;
            this.dgwStockLevelDetails.AllowUserToResizeRows = false;
            this.dgwStockLevelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwStockLevelDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwStockLevelDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwStockLevelDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwStockLevelDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgwStockLevelDetails.Location = new System.Drawing.Point(0, 118);
            this.dgwStockLevelDetails.Name = "dgwStockLevelDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwStockLevelDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgwStockLevelDetails.Size = new System.Drawing.Size(934, 300);
            this.dgwStockLevelDetails.TabIndex = 20;
            this.dgwStockLevelDetails.Tag = "dgwDataGridView";
            this.dgwStockLevelDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwStockLevelDetails_CellEndEdit);
            this.dgwStockLevelDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwStockLevelDetails_EditingControlShowing);
            // 
            // ddlPreparedBy
            // 
            this.ddlPreparedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPreparedBy.FormattingEnabled = true;
            this.ddlPreparedBy.Location = new System.Drawing.Point(134, 6);
            this.ddlPreparedBy.Name = "ddlPreparedBy";
            this.ddlPreparedBy.Size = new System.Drawing.Size(262, 21);
            this.ddlPreparedBy.TabIndex = 31;
            this.ddlPreparedBy.Tag = "ddlDropDownList";
            // 
            // lblPreparedby
            // 
            this.lblPreparedby.AutoSize = true;
            this.lblPreparedby.Location = new System.Drawing.Point(64, 11);
            this.lblPreparedby.Name = "lblPreparedby";
            this.lblPreparedby.Size = new System.Drawing.Size(68, 13);
            this.lblPreparedby.TabIndex = 1;
            this.lblPreparedby.Tag = "lblLabelRequired";
            this.lblPreparedby.Text = "Prepared By:";
            // 
            // ddlAuthoriseBy
            // 
            this.ddlAuthoriseBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuthoriseBy.FormattingEnabled = true;
            this.ddlAuthoriseBy.Location = new System.Drawing.Point(577, 6);
            this.ddlAuthoriseBy.Name = "ddlAuthoriseBy";
            this.ddlAuthoriseBy.Size = new System.Drawing.Size(262, 21);
            this.ddlAuthoriseBy.TabIndex = 32;
            this.ddlAuthoriseBy.Tag = "ddlDropDownList";
            // 
            // lblAuthorizedBy
            // 
            this.lblAuthorizedBy.AutoSize = true;
            this.lblAuthorizedBy.Location = new System.Drawing.Point(493, 11);
            this.lblAuthorizedBy.Name = "lblAuthorizedBy";
            this.lblAuthorizedBy.Size = new System.Drawing.Size(81, 13);
            this.lblAuthorizedBy.TabIndex = 50;
            this.lblAuthorizedBy.Tag = "lblLabelRequired";
            this.lblAuthorizedBy.Text = "Authorized  By :";
            // 
            // ddlAdjustmentReason
            // 
            this.ddlAdjustmentReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAdjustmentReason.FormattingEnabled = true;
            this.ddlAdjustmentReason.ItemHeight = 13;
            this.ddlAdjustmentReason.Location = new System.Drawing.Point(135, 89);
            this.ddlAdjustmentReason.Name = "ddlAdjustmentReason";
            this.ddlAdjustmentReason.Size = new System.Drawing.Size(262, 21);
            this.ddlAdjustmentReason.TabIndex = 12;
            this.ddlAdjustmentReason.Tag = "ddlDropDownList";
            this.ddlAdjustmentReason.SelectedValueChanged += new System.EventHandler(this.ddlAdjustmentReason_SelectedValueChanged);
            // 
            // lblAdjReason
            // 
            this.lblAdjReason.AutoSize = true;
            this.lblAdjReason.Location = new System.Drawing.Point(30, 93);
            this.lblAdjReason.Name = "lblAdjReason";
            this.lblAdjReason.Size = new System.Drawing.Size(102, 13);
            this.lblAdjReason.TabIndex = 68;
            this.lblAdjReason.Tag = "lblLabelRequired";
            this.lblAdjReason.Text = "Adjustment Reason:";
            // 
            // chkAdjReason
            // 
            this.chkAdjReason.AutoSize = true;
            this.chkAdjReason.Location = new System.Drawing.Point(419, 92);
            this.chkAdjReason.Name = "chkAdjReason";
            this.chkAdjReason.Size = new System.Drawing.Size(193, 17);
            this.chkAdjReason.TabIndex = 13;
            this.chkAdjReason.Tag = "chkCheckbox";
            this.chkAdjReason.Text = "Apply adjustment reason to all items";
            this.chkAdjReason.UseVisualStyleBackColor = true;
            this.chkAdjReason.CheckedChanged += new System.EventHandler(this.chkAdjReason_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(401, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pnl_ButtonSubmit
            // 
            this.pnl_ButtonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_ButtonSubmit.Controls.Add(this.btnSubmit);
            this.pnl_ButtonSubmit.Location = new System.Drawing.Point(0, 58);
            this.pnl_ButtonSubmit.Name = "pnl_ButtonSubmit";
            this.pnl_ButtonSubmit.Size = new System.Drawing.Size(934, 56);
            this.pnl_ButtonSubmit.TabIndex = 10;
            this.pnl_ButtonSubmit.Tag = "pnlPanel";
            // 
            // pnlTopPanel
            // 
            this.pnlTopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTopPanel.Controls.Add(this.lblStore);
            this.pnlTopPanel.Controls.Add(this.lblDateFeild);
            this.pnlTopPanel.Controls.Add(this.ddlStoreName);
            this.pnlTopPanel.Controls.Add(this.dtpEffectiveDate);
            this.pnlTopPanel.Location = new System.Drawing.Point(0, 1);
            this.pnlTopPanel.Name = "pnlTopPanel";
            this.pnlTopPanel.Size = new System.Drawing.Size(934, 52);
            this.pnlTopPanel.TabIndex = 1;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(63, 18);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(66, 13);
            this.lblStore.TabIndex = 1;
            this.lblStore.Tag = "lblLabelRequired";
            this.lblStore.Text = "Store Name:";
            // 
            // lblDateFeild
            // 
            this.lblDateFeild.AutoSize = true;
            this.lblDateFeild.Location = new System.Drawing.Point(511, 18);
            this.lblDateFeild.Name = "lblDateFeild";
            this.lblDateFeild.Size = new System.Drawing.Size(62, 13);
            this.lblDateFeild.TabIndex = 50;
            this.lblDateFeild.Tag = "lblLabel";
            this.lblDateFeild.Text = "As Of Date:";
            // 
            // ddlStoreName
            // 
            this.ddlStoreName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStoreName.FormattingEnabled = true;
            this.ddlStoreName.Location = new System.Drawing.Point(134, 15);
            this.ddlStoreName.Name = "ddlStoreName";
            this.ddlStoreName.Size = new System.Drawing.Size(262, 21);
            this.ddlStoreName.TabIndex = 2;
            this.ddlStoreName.Tag = "ddlDropDownList";
            this.ddlStoreName.SelectionChangeCommitted += new System.EventHandler(this.ddlStoreName_SelectionChangeCommitted);
            // 
            // dtpEffectiveDate
            // 
            this.dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpEffectiveDate.Enabled = false;
            this.dtpEffectiveDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveDate.Location = new System.Drawing.Point(577, 15);
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.Size = new System.Drawing.Size(262, 20);
            this.dtpEffectiveDate.TabIndex = 3;
            this.dtpEffectiveDate.Tag = "txtTextBox";
            // 
            // pnl_PreparedBy
            // 
            this.pnl_PreparedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_PreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_PreparedBy.Controls.Add(this.lblPreparedby);
            this.pnl_PreparedBy.Controls.Add(this.ddlPreparedBy);
            this.pnl_PreparedBy.Controls.Add(this.lblAuthorizedBy);
            this.pnl_PreparedBy.Controls.Add(this.ddlAuthoriseBy);
            this.pnl_PreparedBy.Location = new System.Drawing.Point(0, 420);
            this.pnl_PreparedBy.Name = "pnl_PreparedBy";
            this.pnl_PreparedBy.Size = new System.Drawing.Size(934, 34);
            this.pnl_PreparedBy.TabIndex = 30;
            // 
            // chkUpdateStockFlag
            // 
            this.chkUpdateStockFlag.AutoSize = true;
            this.chkUpdateStockFlag.Checked = true;
            this.chkUpdateStockFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateStockFlag.Location = new System.Drawing.Point(708, 92);
            this.chkUpdateStockFlag.Name = "chkUpdateStockFlag";
            this.chkUpdateStockFlag.Size = new System.Drawing.Size(115, 17);
            this.chkUpdateStockFlag.TabIndex = 14;
            this.chkUpdateStockFlag.Tag = "chkCheckbox";
            this.chkUpdateStockFlag.Text = "Adjust Stock Level";
            this.chkUpdateStockFlag.UseVisualStyleBackColor = true;
            this.chkUpdateStockFlag.Visible = false;
            // 
            // frmAdjustStockLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(934, 504);
            this.Controls.Add(this.pnl_PreparedBy);
            this.Controls.Add(this.pnlTopPanel);
            this.Controls.Add(this.chkAdjReason);
            this.Controls.Add(this.ddlAdjustmentReason);
            this.Controls.Add(this.lblAdjReason);
            this.Controls.Add(this.chkUpdateStockFlag);
            this.Controls.Add(this.pnl_ButtonSubmit);
            this.Controls.Add(this.dgwStockLevelDetails);
            this.Controls.Add(this.basePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdjustStockLevel";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Adjust Stock Levels";
            this.Load += new System.EventHandler(this.frmAdjustStockLevel_Load);
            this.basePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockLevelDetails)).EndInit();
            this.pnl_ButtonSubmit.ResumeLayout(false);
            this.pnlTopPanel.ResumeLayout(false);
            this.pnlTopPanel.PerformLayout();
            this.pnl_PreparedBy.ResumeLayout(false);
            this.pnl_PreparedBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel basePanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgwStockLevelDetails;
        private System.Windows.Forms.ComboBox ddlPreparedBy;
        private System.Windows.Forms.Label lblPreparedby;
        private System.Windows.Forms.Label lblAuthorizedBy;
        private System.Windows.Forms.ComboBox ddlAuthoriseBy;
        private System.Windows.Forms.ComboBox ddlAdjustmentReason;
        private System.Windows.Forms.Label lblAdjReason;
        private System.Windows.Forms.CheckBox chkAdjReason;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel pnl_ButtonSubmit;
        private System.Windows.Forms.Panel pnlTopPanel;
        private System.Windows.Forms.ComboBox ddlStoreName;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.Label lblDateFeild;
        private System.Windows.Forms.Panel pnl_PreparedBy;
        private System.Windows.Forms.CheckBox chkUpdateStockFlag;
    }
}