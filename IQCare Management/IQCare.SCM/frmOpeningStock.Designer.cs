namespace IQCare.SCM
{
    partial class frmOpeningStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpeningStock));
            this.dgwOpeningStock = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.txtOpeningQty = new System.Windows.Forms.TextBox();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.Address = new System.Windows.Forms.Label();
            this.txtDispensingUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProgramID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ddlStore = new System.Windows.Forms.ComboBox();
            this.lblAsofDate = new System.Windows.Forms.Label();
            this.dtpTransdate = new System.Windows.Forms.DateTimePicker();
            this.lstSearchBatch = new System.Windows.Forms.ListBox();
            this.txtBatchName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgwOpeningStock)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwOpeningStock
            // 
            this.dgwOpeningStock.AllowUserToAddRows = false;
            this.dgwOpeningStock.AllowUserToDeleteRows = false;
            this.dgwOpeningStock.AllowUserToResizeColumns = false;
            this.dgwOpeningStock.AllowUserToResizeRows = false;
            this.dgwOpeningStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dgwOpeningStock.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwOpeningStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwOpeningStock.Location = new System.Drawing.Point(2, 113);
            this.dgwOpeningStock.Name = "dgwOpeningStock";
            this.dgwOpeningStock.Size = new System.Drawing.Size(914, 340);
            this.dgwOpeningStock.TabIndex = 10;
            this.dgwOpeningStock.Tag = "dgwDataGridView";
            this.dgwOpeningStock.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwOpeningStock_CellClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(2, 456);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(914, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 21;
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
            this.btnClose.TabIndex = 22;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(540, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 103;
            this.label3.Tag = "lblLabelRequired";
            this.label3.Text = "Opening Quantity:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(540, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 102;
            this.label1.Tag = "lblLabelRequired";
            this.label1.Text = "Expiry Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiryDate.Location = new System.Drawing.Point(647, 32);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(123, 20);
            this.dtpExpiryDate.TabIndex = 4;
            this.dtpExpiryDate.Tag = "txtTextBox";
            // 
            // txtOpeningQty
            // 
            this.txtOpeningQty.Location = new System.Drawing.Point(647, 61);
            this.txtOpeningQty.MaxLength = 200;
            this.txtOpeningQty.Name = "txtOpeningQty";
            this.txtOpeningQty.Size = new System.Drawing.Size(123, 20);
            this.txtOpeningQty.TabIndex = 6;
            this.txtOpeningQty.Tag = "txtTextBox";
            this.txtOpeningQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpeningQty_KeyPress);
            // 
            // lstSearch
            // 
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.Location = new System.Drawing.Point(426, 35);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(35, 17);
            this.lstSearch.TabIndex = 99;
            this.lstSearch.Tag = "lstListBox";
            this.lstSearch.Visible = false;
            this.lstSearch.DoubleClick += new System.EventHandler(this.lstSearch_DoubleClick);
            this.lstSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSearch_KeyUp);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(83, 35);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(451, 20);
            this.txtItemName.TabIndex = 3;
            this.txtItemName.Tag = "txtTextBox";
            this.txtItemName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyUp);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(812, 82);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Address
            // 
            this.Address.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Address.Location = new System.Drawing.Point(11, 69);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(58, 13);
            this.Address.TabIndex = 95;
            this.Address.Tag = "lblLabelRequired";
            this.Address.Text = "Batch No:";
            this.Address.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDispensingUnit
            // 
            this.txtDispensingUnit.Enabled = false;
            this.txtDispensingUnit.Location = new System.Drawing.Point(647, 5);
            this.txtDispensingUnit.MaxLength = 200;
            this.txtDispensingUnit.Name = "txtDispensingUnit";
            this.txtDispensingUnit.Size = new System.Drawing.Size(123, 20);
            this.txtDispensingUnit.TabIndex = 2;
            this.txtDispensingUnit.Tag = "txtTextBox";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(541, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 93;
            this.label2.Tag = "lblLabelRequired";
            this.label2.Text = "Dispensing Unit:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProgramID
            // 
            this.lblProgramID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProgramID.Location = new System.Drawing.Point(11, 11);
            this.lblProgramID.Name = "lblProgramID";
            this.lblProgramID.Size = new System.Drawing.Size(72, 13);
            this.lblProgramID.TabIndex = 92;
            this.lblProgramID.Tag = "lblLabelRequired";
            this.lblProgramID.Text = "Store Name:";
            this.lblProgramID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.Location = new System.Drawing.Point(11, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(72, 13);
            this.lblStatus.TabIndex = 90;
            this.lblStatus.Tag = "lblLabelRequired";
            this.lblStatus.Text = "Item Name:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ddlStore
            // 
            this.ddlStore.BackColor = System.Drawing.Color.White;
            this.ddlStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ddlStore.Location = new System.Drawing.Point(83, 7);
            this.ddlStore.Name = "ddlStore";
            this.ddlStore.Size = new System.Drawing.Size(336, 21);
            this.ddlStore.TabIndex = 1;
            this.ddlStore.Tag = "ddlDropDownList";
            this.ddlStore.SelectionChangeCommitted += new System.EventHandler(this.ddlStore_SelectionChangeCommitted);
            // 
            // lblAsofDate
            // 
            this.lblAsofDate.Location = new System.Drawing.Point(540, 91);
            this.lblAsofDate.Name = "lblAsofDate";
            this.lblAsofDate.Size = new System.Drawing.Size(100, 13);
            this.lblAsofDate.TabIndex = 105;
            this.lblAsofDate.Tag = "lblLabelRequired";
            this.lblAsofDate.Text = "As of Date:";
            this.lblAsofDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTransdate
            // 
            this.dtpTransdate.CustomFormat = "dd-MMM-yyyy";
            this.dtpTransdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransdate.Location = new System.Drawing.Point(647, 87);
            this.dtpTransdate.Name = "dtpTransdate";
            this.dtpTransdate.Size = new System.Drawing.Size(123, 20);
            this.dtpTransdate.TabIndex = 7;
            this.dtpTransdate.Tag = "txtTextBox";
            // 
            // lstSearchBatch
            // 
            this.lstSearchBatch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstSearchBatch.FormattingEnabled = true;
            this.lstSearchBatch.Location = new System.Drawing.Point(457, 66);
            this.lstSearchBatch.Name = "lstSearchBatch";
            this.lstSearchBatch.Size = new System.Drawing.Size(34, 17);
            this.lstSearchBatch.TabIndex = 106;
            this.lstSearchBatch.Tag = "lstListBox";
            this.lstSearchBatch.Visible = false;
            this.lstSearchBatch.DoubleClick += new System.EventHandler(this.lstSearchBatch_DoubleClick);
            this.lstSearchBatch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSearchBatch_KeyUp);
            // 
            // txtBatchName
            // 
            this.txtBatchName.Location = new System.Drawing.Point(83, 63);
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Size = new System.Drawing.Size(336, 20);
            this.txtBatchName.TabIndex = 5;
            this.txtBatchName.Tag = "txtTextBox";
            this.txtBatchName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBatchName_KeyUp);
            // 
            // frmOpeningStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(916, 504);
            this.Controls.Add(this.txtBatchName);
            this.Controls.Add(this.lstSearchBatch);
            this.Controls.Add(this.lblAsofDate);
            this.Controls.Add(this.dtpTransdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.txtOpeningQty);
            this.Controls.Add(this.lstSearch);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.Address);
            this.Controls.Add(this.txtDispensingUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProgramID);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.ddlStore);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgwOpeningStock);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpeningStock";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Opening Stock";
            this.Load += new System.EventHandler(this.frmOpeningStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwOpeningStock)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwOpeningStock;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.TextBox txtOpeningQty;
        private System.Windows.Forms.ListBox lstSearch;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label Address;
        private System.Windows.Forms.TextBox txtDispensingUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProgramID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox ddlStore;
        private System.Windows.Forms.Label lblAsofDate;
        private System.Windows.Forms.DateTimePicker dtpTransdate;
        private System.Windows.Forms.ListBox lstSearchBatch;
        private System.Windows.Forms.TextBox txtBatchName;
    }
}