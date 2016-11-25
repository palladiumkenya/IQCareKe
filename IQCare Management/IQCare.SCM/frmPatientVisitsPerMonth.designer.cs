namespace IQCare.SCM
{
    partial class frmPatientVisitsPerMonth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientVisitsPerMonth));
            this.dgwVisits = new System.Windows.Forms.DataGridView();
            this.colMonthName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConsultFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ddlProgramYear = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwVisits)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwVisits
            // 
            this.dgwVisits.AllowUserToAddRows = false;
            this.dgwVisits.AllowUserToDeleteRows = false;
            this.dgwVisits.AllowUserToResizeColumns = false;
            this.dgwVisits.AllowUserToResizeRows = false;
            this.dgwVisits.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwVisits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMonthName,
            this.Id,
            this.ColNumber,
            this.colAdminFee,
            this.colConsultFee});
            this.dgwVisits.Location = new System.Drawing.Point(0, 84);
            this.dgwVisits.Name = "dgwVisits";
            this.dgwVisits.Size = new System.Drawing.Size(852, 372);
            this.dgwVisits.TabIndex = 10;
            this.dgwVisits.Tag = "dgwDataGridView";
            this.dgwVisits.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgwVisits_DataBindingComplete);
            this.dgwVisits.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwVisits_DataError);
            this.dgwVisits.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwVisits_RowValidated);
            // 
            // colMonthName
            // 
            this.colMonthName.DataPropertyName = "MonthName";
            this.colMonthName.HeaderText = "Month";
            this.colMonthName.Name = "colMonthName";
            this.colMonthName.ReadOnly = true;
            this.colMonthName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colMonthName.Width = 250;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ColNumber
            // 
            this.ColNumber.DataPropertyName = "Visits";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.ColNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColNumber.HeaderText = "Number per Month";
            this.ColNumber.Name = "ColNumber";
            this.ColNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColNumber.Width = 200;
            // 
            // colAdminFee
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.colAdminFee.DefaultCellStyle = dataGridViewCellStyle2;
            this.colAdminFee.HeaderText = "Administrative Fee (calculated)";
            this.colAdminFee.Name = "colAdminFee";
            this.colAdminFee.ReadOnly = true;
            this.colAdminFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAdminFee.Width = 250;
            // 
            // colConsultFee
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.colConsultFee.DefaultCellStyle = dataGridViewCellStyle3;
            this.colConsultFee.HeaderText = "Consultation Fee (calculated)";
            this.colConsultFee.Name = "colConsultFee";
            this.colConsultFee.ReadOnly = true;
            this.colConsultFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colConsultFee.Width = 247;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(41, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabelRequired";
            this.lblStatus.Text = "Calendar Year:";
            // 
            // ddlProgramYear
            // 
            this.ddlProgramYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProgramYear.FormattingEnabled = true;
            this.ddlProgramYear.Location = new System.Drawing.Point(121, 30);
            this.ddlProgramYear.Name = "ddlProgramYear";
            this.ddlProgramYear.Size = new System.Drawing.Size(152, 21);
            this.ddlProgramYear.TabIndex = 2;
            this.ddlProgramYear.Tag = "ddlDropDownListSCM";
            this.ddlProgramYear.SelectedIndexChanged += new System.EventHandler(this.ddlProgramYear_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelCurrency);
            this.panel1.Controls.Add(this.ddlProgramYear);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 78);
            this.panel1.TabIndex = 1;
            this.panel1.Tag = "pnlPanel";
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Location = new System.Drawing.Point(735, 33);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(49, 13);
            this.labelCurrency.TabIndex = 46;
            this.labelCurrency.Tag = "lblLabelRequired";
            this.labelCurrency.Text = "Currency";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSave.Location = new System.Drawing.Point(692, 10);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 25);
            this.buttonSave.TabIndex = 21;
            this.buttonSave.Tag = "btnSingleText";
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.SystemColors.Window;
            this.buttonClose.Location = new System.Drawing.Point(771, 10);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(80, 25);
            this.buttonClose.TabIndex = 22;
            this.buttonClose.Tag = "btnSingleText";
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // frmPatientVisitsPerMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgwVisits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientVisitsPerMonth";
            this.Tag = "frmForm";
            this.Text = "Patient Visits per Month";
            this.Load += new System.EventHandler(this.frmConfigureBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwVisits)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwVisits;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox ddlProgramYear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelCurrency;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMonthName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConsultFee;
    }
}