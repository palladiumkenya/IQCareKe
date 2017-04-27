namespace IQCare.SCM
{
    partial class frmStockSummaryLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockSummaryLine));
            this.lblItem = new System.Windows.Forms.Label();
            this.dgwStockSummary = new System.Windows.Forms.DataGridView();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockSummary)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblItem
            // 
            this.lblItem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItem.Location = new System.Drawing.Point(27, 11);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(76, 21);
            this.lblItem.TabIndex = 106;
            this.lblItem.Tag = "lblLabel";
            this.lblItem.Text = "Items:";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblItem.Visible = false;
            // 
            // dgwStockSummary
            // 
            this.dgwStockSummary.AllowUserToAddRows = false;
            this.dgwStockSummary.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwStockSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwStockSummary.Location = new System.Drawing.Point(2, 51);
            this.dgwStockSummary.Name = "dgwStockSummary";
            this.dgwStockSummary.Size = new System.Drawing.Size(966, 403);
            this.dgwStockSummary.TabIndex = 68;
            this.dgwStockSummary.Tag = "dgwDataGridView";
            // 
            // lstSearch
            // 
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.Location = new System.Drawing.Point(527, 11);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(29, 17);
            this.lstSearch.TabIndex = 112;
            this.lstSearch.Tag = "lstListBox";
            this.lstSearch.Visible = false;
            this.lstSearch.DoubleClick += new System.EventHandler(this.lstSearch_DoubleClick);
            this.lstSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSearch_KeyUp);
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(147, 11);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(342, 20);
            this.txtItemName.TabIndex = 111;
            this.txtItemName.Tag = "txtTextBox";
            this.txtItemName.Visible = false;
            this.txtItemName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyUp);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.Window;
            this.btnExport.Location = new System.Drawing.Point(845, 11);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 25);
            this.btnExport.TabIndex = 114;
            this.btnExport.Tag = "btnH25W80Flexi";
            this.btnExport.Text = "&Export to Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Location = new System.Drawing.Point(2, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(978, 47);
            this.panel2.TabIndex = 115;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.SystemColors.Window;
            this.btn_Close.Location = new System.Drawing.Point(885, 12);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 25);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Tag = "btnSingleText";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmStockSummaryLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(980, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstSearch);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.dgwStockSummary);
            this.Controls.Add(this.lblItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockSummaryLine";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Stock Summary Line List";
            this.Load += new System.EventHandler(this.frmStockSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockSummary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.DataGridView dgwStockSummary;
        private System.Windows.Forms.ListBox lstSearch;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Close;
    }
}