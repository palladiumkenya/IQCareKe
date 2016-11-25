namespace IQCare.SCM
{
    partial class frmBatchSummaryByStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchSummaryByStore));
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblProgramID = new System.Windows.Forms.Label();
            this.ddlStore = new System.Windows.Forms.ComboBox();
            this.dgwBatchSummary = new System.Windows.Forms.DataGridView();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwBatchSummary)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(223, 52);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(47, 13);
            this.lblTo.TabIndex = 116;
            this.lblTo.Tag = "lblLabel";
            this.lblTo.Text = "To:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(274, 48);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(123, 20);
            this.dtpTo.TabIndex = 4;
            this.dtpTo.Tag = "txtTextBox";
            // 
            // lblItem
            // 
            this.lblItem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItem.Location = new System.Drawing.Point(414, 11);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(76, 21);
            this.lblItem.TabIndex = 114;
            this.lblItem.Tag = "lblLabel";
            this.lblItem.Text = "Items:";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(3, 52);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(53, 13);
            this.lblFrom.TabIndex = 112;
            this.lblFrom.Tag = "lblLabel";
            this.lblFrom.Text = "From :";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(60, 48);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(122, 20);
            this.dtpFrom.TabIndex = 3;
            this.dtpFrom.Tag = "txtTextBox";
            // 
            // lblProgramID
            // 
            this.lblProgramID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProgramID.Location = new System.Drawing.Point(10, 15);
            this.lblProgramID.Name = "lblProgramID";
            this.lblProgramID.Size = new System.Drawing.Size(42, 13);
            this.lblProgramID.TabIndex = 110;
            this.lblProgramID.Tag = "lblLabel";
            this.lblProgramID.Text = "Store:";
            this.lblProgramID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlStore
            // 
            this.ddlStore.BackColor = System.Drawing.Color.White;
            this.ddlStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ddlStore.Location = new System.Drawing.Point(57, 11);
            this.ddlStore.Name = "ddlStore";
            this.ddlStore.Size = new System.Drawing.Size(337, 21);
            this.ddlStore.TabIndex = 1;
            this.ddlStore.Tag = "ddlDropDownList";
            this.ddlStore.SelectionChangeCommitted += new System.EventHandler(this.ddlStore_SelectionChangeCommitted);
            // 
            // dgwBatchSummary
            // 
            this.dgwBatchSummary.AllowUserToAddRows = false;
            this.dgwBatchSummary.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwBatchSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwBatchSummary.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dgwBatchSummary.Location = new System.Drawing.Point(2, 90);
            this.dgwBatchSummary.Name = "dgwBatchSummary";
            this.dgwBatchSummary.Size = new System.Drawing.Size(848, 364);
            this.dgwBatchSummary.TabIndex = 10;
            this.dgwBatchSummary.Tag = "dgwDataGridView";
            // 
            // lstSearch
            // 
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.Location = new System.Drawing.Point(803, 11);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(29, 17);
            this.lstSearch.TabIndex = 121;
            this.lstSearch.Tag = "lstListBox";
            this.lstSearch.Visible = false;
            this.lstSearch.DoubleClick += new System.EventHandler(this.lstSearch_DoubleClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.SystemColors.Window;
            this.btnSubmit.Location = new System.Drawing.Point(641, 45);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 25);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.Window;
            this.btnExport.Location = new System.Drawing.Point(720, 45);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 25);
            this.btnExport.TabIndex = 6;
            this.btnExport.Tag = "btnH25W80Flexi";
            this.btnExport.Text = "&Export to Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(496, 11);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(301, 20);
            this.txtItemName.TabIndex = 2;
            this.txtItemName.Tag = "txtTextBox";
            this.txtItemName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyUp);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Location = new System.Drawing.Point(-1, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.SystemColors.Window;
            this.btn_Close.Location = new System.Drawing.Point(771, 10);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 25);
            this.btn_Close.TabIndex = 21;
            this.btn_Close.Tag = "btnSingleText";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmBatchSummaryByStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstSearch);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgwBatchSummary);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblProgramID);
            this.Controls.Add(this.ddlStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBatchSummaryByStore";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Batch Summary";
            this.Load += new System.EventHandler(this.frmBatchSummaryByStore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwBatchSummary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblProgramID;
        private System.Windows.Forms.ComboBox ddlStore;
        private System.Windows.Forms.DataGridView dgwBatchSummary;
        private System.Windows.Forms.ListBox lstSearch;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Close;

    }
}