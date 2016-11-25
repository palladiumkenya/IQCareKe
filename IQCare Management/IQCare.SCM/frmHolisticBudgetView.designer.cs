namespace IQCare.SCM
{
    partial class frmHolisticBudgetView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHolisticBudgetView));
            this.dgwHolisticBudgetView = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ddlCalendarYear = new System.Windows.Forms.ComboBox();
            this.ddlCostCategory = new System.Windows.Forms.ComboBox();
            this.lbldonorPayer = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwHolisticBudgetView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwHolisticBudgetView
            // 
            this.dgwHolisticBudgetView.AllowUserToAddRows = false;
            this.dgwHolisticBudgetView.AllowUserToDeleteRows = false;
            this.dgwHolisticBudgetView.AllowUserToResizeColumns = false;
            this.dgwHolisticBudgetView.AllowUserToResizeRows = false;
            this.dgwHolisticBudgetView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwHolisticBudgetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwHolisticBudgetView.Location = new System.Drawing.Point(0, 84);
            this.dgwHolisticBudgetView.Name = "dgwHolisticBudgetView";
            this.dgwHolisticBudgetView.Size = new System.Drawing.Size(852, 372);
            this.dgwHolisticBudgetView.TabIndex = 10;
            this.dgwHolisticBudgetView.Tag = "dgwDataGridView";
            this.dgwHolisticBudgetView.GotFocus += new System.EventHandler(this.dgwHolisticBudgetView_GotFocus);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(525, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabel";
            this.lblStatus.Text = "Calendar Year:";
            // 
            // ddlCalendarYear
            // 
            this.ddlCalendarYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCalendarYear.FormattingEnabled = true;
            this.ddlCalendarYear.Location = new System.Drawing.Point(618, 14);
            this.ddlCalendarYear.Name = "ddlCalendarYear";
            this.ddlCalendarYear.Size = new System.Drawing.Size(152, 21);
            this.ddlCalendarYear.TabIndex = 3;
            this.ddlCalendarYear.Tag = "ddlDropDownList";
            this.ddlCalendarYear.SelectedIndexChanged += new System.EventHandler(this.ddlCalendarYear_SelectedIndexChanged);
            // 
            // ddlCostCategory
            // 
            this.ddlCostCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCostCategory.FormattingEnabled = true;
            this.ddlCostCategory.Location = new System.Drawing.Point(147, 14);
            this.ddlCostCategory.Name = "ddlCostCategory";
            this.ddlCostCategory.Size = new System.Drawing.Size(168, 21);
            this.ddlCostCategory.TabIndex = 2;
            this.ddlCostCategory.Tag = "ddlDropDownList";
            this.ddlCostCategory.SelectedIndexChanged += new System.EventHandler(this.ddlCostCategory_SelectedIndexChanged);
            // 
            // lbldonorPayer
            // 
            this.lbldonorPayer.AutoSize = true;
            this.lbldonorPayer.Location = new System.Drawing.Point(46, 17);
            this.lbldonorPayer.Name = "lbldonorPayer";
            this.lbldonorPayer.Size = new System.Drawing.Size(76, 13);
            this.lbldonorPayer.TabIndex = 39;
            this.lbldonorPayer.Tag = "lblLabel";
            this.lbldonorPayer.Text = "Cost Category:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCurrency);
            this.panel1.Controls.Add(this.ddlCalendarYear);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lbldonorPayer);
            this.panel1.Controls.Add(this.ddlCostCategory);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 78);
            this.panel1.TabIndex = 1;
            this.panel1.Tag = "pnlPanel";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(735, 50);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(59, 13);
            this.lblCurrency.TabIndex = 47;
            this.lblCurrency.Tag = "lblLabelRequired";
            this.lblCurrency.Text = "lblCurrency";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(766, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 25);
            this.btnClose.TabIndex = 21;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmHolisticBudgetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgwHolisticBudgetView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHolisticBudgetView";
            this.Tag = "frmForm";
            this.Text = "Configure Budget";
            this.Load += new System.EventHandler(this.frmHolisticBudgetView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwHolisticBudgetView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwHolisticBudgetView;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbldonorPayer;
        private System.Windows.Forms.ComboBox ddlCostCategory;
        private System.Windows.Forms.ComboBox ddlCalendarYear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Button btnClose;
    }
}