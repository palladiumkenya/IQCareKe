namespace IQCare.SCM
{
    partial class frmReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
            this.pnlStockLedger = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblProgramID = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.ddlStore = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoStockLedger = new System.Windows.Forms.RadioButton();
            this.basePanel = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlStockLedger.SuspendLayout();
            this.panel2.SuspendLayout();
            this.basePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlStockLedger
            // 
            this.pnlStockLedger.Controls.Add(this.lblTo);
            this.pnlStockLedger.Controls.Add(this.lblProgramID);
            this.pnlStockLedger.Controls.Add(this.dtpTo);
            this.pnlStockLedger.Controls.Add(this.ddlStore);
            this.pnlStockLedger.Controls.Add(this.lblFrom);
            this.pnlStockLedger.Controls.Add(this.dtpFrom);
            this.pnlStockLedger.Location = new System.Drawing.Point(348, 112);
            this.pnlStockLedger.Name = "pnlStockLedger";
            this.pnlStockLedger.Size = new System.Drawing.Size(498, 108);
            this.pnlStockLedger.TabIndex = 10;
            this.pnlStockLedger.Visible = false;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(294, 62);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(47, 13);
            this.lblTo.TabIndex = 121;
            this.lblTo.Tag = "lblLabel";
            this.lblTo.Text = "To:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProgramID
            // 
            this.lblProgramID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProgramID.Location = new System.Drawing.Point(88, 19);
            this.lblProgramID.Name = "lblProgramID";
            this.lblProgramID.Size = new System.Drawing.Size(42, 13);
            this.lblProgramID.TabIndex = 112;
            this.lblProgramID.Tag = "lblLabel";
            this.lblProgramID.Text = "Store:";
            this.lblProgramID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(345, 58);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(123, 20);
            this.dtpTo.TabIndex = 13;
            this.dtpTo.Tag = "txtTextBox";
            // 
            // ddlStore
            // 
            this.ddlStore.BackColor = System.Drawing.Color.White;
            this.ddlStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ddlStore.Location = new System.Drawing.Point(132, 16);
            this.ddlStore.Name = "ddlStore";
            this.ddlStore.Size = new System.Drawing.Size(337, 21);
            this.ddlStore.TabIndex = 11;
            this.ddlStore.Tag = "ddlDropDownList";
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(77, 62);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(53, 13);
            this.lblFrom.TabIndex = 119;
            this.lblFrom.Tag = "lblLabel";
            this.lblFrom.Text = "From :";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(133, 58);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(130, 20);
            this.dtpFrom.TabIndex = 12;
            this.dtpFrom.Tag = "txtTextBox";
           
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdoStockLedger);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(342, 451);
            this.panel2.TabIndex = 1;
            // 
            // rdoStockLedger
            // 
            this.rdoStockLedger.AutoSize = true;
            this.rdoStockLedger.Location = new System.Drawing.Point(29, 18);
            this.rdoStockLedger.Name = "rdoStockLedger";
            this.rdoStockLedger.Size = new System.Drawing.Size(89, 17);
            this.rdoStockLedger.TabIndex = 2;
            this.rdoStockLedger.TabStop = true;
            this.rdoStockLedger.Text = "Stock Ledger";
            this.rdoStockLedger.UseVisualStyleBackColor = true;
            this.rdoStockLedger.CheckedChanged += new System.EventHandler(this.rdoStockLedger_CheckedChanged);
            // 
            // basePanel
            // 
            this.basePanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.basePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.basePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.basePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.basePanel.Controls.Add(this.btnSubmit);
            this.basePanel.Controls.Add(this.btnClose);
            this.basePanel.Location = new System.Drawing.Point(0, 457);
            this.basePanel.Margin = new System.Windows.Forms.Padding(0);
            this.basePanel.Name = "basePanel";
            this.basePanel.Size = new System.Drawing.Size(852, 47);
            this.basePanel.TabIndex = 20;
            this.basePanel.Tag = "pnlSubPanel";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.SystemColors.Window;
            this.btnSubmit.Location = new System.Drawing.Point(692, 10);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 21;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
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
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.basePanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlStockLedger);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReports";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.frmStockLedger_Load);
            this.pnlStockLedger.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.basePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlStockLedger;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblProgramID;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox ddlStore;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoStockLedger;
        private System.Windows.Forms.Panel basePanel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnClose;
    }
}