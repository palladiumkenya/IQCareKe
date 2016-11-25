namespace IQCare.SCM
{
    partial class frmDonorProgramLinking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonorProgramLinking));
            this.pnlUpper = new System.Windows.Forms.Panel();
            this.pnlbtnsubmit = new System.Windows.Forms.Panel();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.TopPanel_Donor = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.ddlProgramName = new System.Windows.Forms.ComboBox();
            this.ddlDonorName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dgwDonorProgramLinking = new System.Windows.Forms.DataGridView();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.pnlUpper.SuspendLayout();
            this.pnlbtnsubmit.SuspendLayout();
            this.TopPanel_Donor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDonorProgramLinking)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlUpper
            // 
            this.pnlUpper.BackColor = System.Drawing.SystemColors.Window;
            this.pnlUpper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUpper.Controls.Add(this.pnlbtnsubmit);
            this.pnlUpper.Controls.Add(this.TopPanel_Donor);
            this.pnlUpper.Controls.Add(this.dgwDonorProgramLinking);
            this.pnlUpper.Controls.Add(this.pnlGrid);
            this.pnlUpper.Location = new System.Drawing.Point(2, 4);
            this.pnlUpper.Name = "pnlUpper";
            this.pnlUpper.Size = new System.Drawing.Size(848, 449);
            this.pnlUpper.TabIndex = 0;
            this.pnlUpper.Tag = "pnlPanel";
            // 
            // pnlbtnsubmit
            // 
            this.pnlbtnsubmit.Controls.Add(this.btn_Submit);
            this.pnlbtnsubmit.Location = new System.Drawing.Point(378, 91);
            this.pnlbtnsubmit.Name = "pnlbtnsubmit";
            this.pnlbtnsubmit.Size = new System.Drawing.Size(91, 28);
            this.pnlbtnsubmit.TabIndex = 6;
            // 
            // btn_Submit
            // 
            this.btn_Submit.Location = new System.Drawing.Point(3, 3);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(80, 25);
            this.btn_Submit.TabIndex = 6;
            this.btn_Submit.Tag = "btnSingleText";
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = false;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // TopPanel_Donor
            // 
            this.TopPanel_Donor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel_Donor.Controls.Add(this.dtpEndDate);
            this.TopPanel_Donor.Controls.Add(this.ddlProgramName);
            this.TopPanel_Donor.Controls.Add(this.ddlDonorName);
            this.TopPanel_Donor.Controls.Add(this.label6);
            this.TopPanel_Donor.Controls.Add(this.label8);
            this.TopPanel_Donor.Controls.Add(this.label11);
            this.TopPanel_Donor.Controls.Add(this.label5);
            this.TopPanel_Donor.Controls.Add(this.dtpStartDate);
            this.TopPanel_Donor.Location = new System.Drawing.Point(7, 3);
            this.TopPanel_Donor.Name = "TopPanel_Donor";
            this.TopPanel_Donor.Size = new System.Drawing.Size(835, 86);
            this.TopPanel_Donor.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(544, 40);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(123, 20);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.Tag = "txtTextBox";
            // 
            // ddlProgramName
            // 
            this.ddlProgramName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProgramName.FormattingEnabled = true;
            this.ddlProgramName.Location = new System.Drawing.Point(544, 8);
            this.ddlProgramName.Name = "ddlProgramName";
            this.ddlProgramName.Size = new System.Drawing.Size(250, 21);
            this.ddlProgramName.TabIndex = 2;
            this.ddlProgramName.Tag = "ddlDropDownList";
            // 
            // ddlDonorName
            // 
            this.ddlDonorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDonorName.FormattingEnabled = true;
            this.ddlDonorName.Location = new System.Drawing.Point(114, 8);
            this.ddlDonorName.Name = "ddlDonorName";
            this.ddlDonorName.Size = new System.Drawing.Size(250, 21);
            this.ddlDonorName.TabIndex = 1;
            this.ddlDonorName.Tag = "ddlDropDownList";
            this.ddlDonorName.SelectedIndexChanged += new System.EventHandler(this.ddlDonorName_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(438, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 5;
            this.label6.Tag = "lblLabelRequired";
            this.label6.Text = "Funding End Date:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 1;
            this.label8.Tag = "lblLabelRequired";
            this.label8.Text = "Donor Name:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(454, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 13;
            this.label11.Tag = "lblLabelRequired";
            this.label11.Text = "Program Name:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 4;
            this.label5.Tag = "lblLabelRequired";
            this.label5.Text = "Funding Start Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(114, 40);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(132, 20);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.Tag = "txtTextBox";
            // 
            // dgwDonorProgramLinking
            // 
            this.dgwDonorProgramLinking.AllowUserToAddRows = false;
            this.dgwDonorProgramLinking.AllowUserToResizeColumns = false;
            this.dgwDonorProgramLinking.AllowUserToResizeRows = false;
            this.dgwDonorProgramLinking.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwDonorProgramLinking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDonorProgramLinking.Location = new System.Drawing.Point(7, 125);
            this.dgwDonorProgramLinking.Name = "dgwDonorProgramLinking";
            this.dgwDonorProgramLinking.Size = new System.Drawing.Size(835, 314);
            this.dgwDonorProgramLinking.TabIndex = 60;
            this.dgwDonorProgramLinking.Tag = "dgwDataGridView";
            this.dgwDonorProgramLinking.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwDonorProgramLinking_CellClick);
            this.dgwDonorProgramLinking.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwDonorProgramLinking_KeyDown);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Location = new System.Drawing.Point(3, 118);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(840, 324);
            this.pnlGrid.TabIndex = 61;
            // 
            // pnl_Footer
            // 
            this.pnl_Footer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnl_Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.pnl_Footer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Footer.Controls.Add(this.btn_Save);
            this.pnl_Footer.Controls.Add(this.btn_Close);
            this.pnl_Footer.Location = new System.Drawing.Point(0, 457);
            this.pnl_Footer.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(852, 47);
            this.pnl_Footer.TabIndex = 68;
            this.pnl_Footer.Tag = "pnlSubPanel";
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.SystemColors.Window;
            this.btn_Save.Location = new System.Drawing.Point(692, 10);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 25);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Tag = "btnSingleText";
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.SystemColors.Window;
            this.btn_Close.Location = new System.Drawing.Point(771, 10);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 25);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Tag = "btnSingleText";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmDonorProgramLinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.pnl_Footer);
            this.Controls.Add(this.pnlUpper);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDonorProgramLinking";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Donor Program Linking";
            this.Load += new System.EventHandler(this.frmDonorProgramLinking_Load);
            this.pnlUpper.ResumeLayout(false);
            this.pnlbtnsubmit.ResumeLayout(false);
            this.TopPanel_Donor.ResumeLayout(false);
            this.TopPanel_Donor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDonorProgramLinking)).EndInit();
            this.pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlUpper;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox ddlProgramName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgwDonorProgramLinking;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Panel pnl_Footer;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ComboBox ddlDonorName;
        private System.Windows.Forms.Panel TopPanel_Donor;
        private System.Windows.Forms.Panel pnlbtnsubmit;
        private System.Windows.Forms.Panel pnlGrid;

    }
}