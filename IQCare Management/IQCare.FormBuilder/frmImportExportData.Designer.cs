namespace IQCare.FormBuilder
{
    partial class frmImportExportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportExportData));
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbImpLocation = new System.Windows.Forms.ComboBox();
            this.dtFrmDate = new System.Windows.Forms.DateTimePicker();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoExport = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.rdoImport = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbExpLocation = new System.Windows.Forms.ComboBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.grpImport.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 12;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Location:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(377, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 29);
            this.btnClose.TabIndex = 13;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(255, 47);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(100, 20);
            this.dtToDate.TabIndex = 14;
            this.dtToDate.Tag = "lblLabel";
            // 
            // cmbImpLocation
            // 
            this.cmbImpLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImpLocation.FormattingEnabled = true;
            this.cmbImpLocation.Location = new System.Drawing.Point(67, 47);
            this.cmbImpLocation.Name = "cmbImpLocation";
            this.cmbImpLocation.Size = new System.Drawing.Size(280, 21);
            this.cmbImpLocation.TabIndex = 11;
            this.cmbImpLocation.Tag = "ddlDropDownList";
            // 
            // dtFrmDate
            // 
            this.dtFrmDate.CustomFormat = "dd-MMM-yyyy";
            this.dtFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrmDate.Location = new System.Drawing.Point(79, 47);
            this.dtFrmDate.Name = "dtFrmDate";
            this.dtFrmDate.Size = new System.Drawing.Size(100, 20);
            this.dtFrmDate.TabIndex = 13;
            this.dtFrmDate.Tag = "lblLabel";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(135, 82);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(99, 27);
            this.btnImport.TabIndex = 3;
            this.btnImport.Tag = "btnH25WFlexi";
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.AcceptsReturn = true;
            this.txtFileName.Location = new System.Drawing.Point(67, 18);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(280, 20);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Tag = "txtTextBox";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "To Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 11;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "From Date:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowse.Location = new System.Drawing.Point(351, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Tag = "";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // grpImport
            // 
            this.grpImport.Controls.Add(this.label5);
            this.grpImport.Controls.Add(this.cmbImpLocation);
            this.grpImport.Controls.Add(this.btnImport);
            this.grpImport.Controls.Add(this.btnBrowse);
            this.grpImport.Controls.Add(this.txtFileName);
            this.grpImport.Controls.Add(this.label1);
            this.grpImport.Location = new System.Drawing.Point(7, 70);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(403, 129);
            this.grpImport.TabIndex = 11;
            this.grpImport.TabStop = false;
            this.grpImport.Tag = "pnlPanel";
            this.grpImport.Text = "Import Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "File:";
            // 
            // rdoExport
            // 
            this.rdoExport.AutoSize = true;
            this.rdoExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rdoExport.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rdoExport.Location = new System.Drawing.Point(242, 17);
            this.rdoExport.Name = "rdoExport";
            this.rdoExport.Size = new System.Drawing.Size(81, 17);
            this.rdoExport.TabIndex = 1;
            this.rdoExport.TabStop = true;
            this.rdoExport.Tag = "lblLabel";
            this.rdoExport.Text = "Export Data";
            this.rdoExport.UseVisualStyleBackColor = true;
            this.rdoExport.CheckedChanged += new System.EventHandler(this.rdoExport_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(167, 85);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(99, 27);
            this.btnExport.TabIndex = 2;
            this.btnExport.Tag = "btnH25WFlexi";
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rdoImport
            // 
            this.rdoImport.AutoSize = true;
            this.rdoImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rdoImport.Location = new System.Drawing.Point(118, 17);
            this.rdoImport.Name = "rdoImport";
            this.rdoImport.Size = new System.Drawing.Size(80, 17);
            this.rdoImport.TabIndex = 0;
            this.rdoImport.TabStop = true;
            this.rdoImport.Tag = "lblLabel";
            this.rdoImport.Text = "Import Data";
            this.rdoImport.UseVisualStyleBackColor = true;
            this.rdoImport.CheckedChanged += new System.EventHandler(this.rdoImport_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoExport);
            this.groupBox3.Controls.Add(this.rdoImport);
            this.groupBox3.Location = new System.Drawing.Point(5, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(841, 51);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "pnlPanel";
            this.groupBox3.Text = "Select the Operation";
            // 
            // grpExport
            // 
            this.grpExport.Controls.Add(this.dtToDate);
            this.grpExport.Controls.Add(this.dtFrmDate);
            this.grpExport.Controls.Add(this.label3);
            this.grpExport.Controls.Add(this.label2);
            this.grpExport.Controls.Add(this.label4);
            this.grpExport.Controls.Add(this.cmbExpLocation);
            this.grpExport.Controls.Add(this.btnExport);
            this.grpExport.Location = new System.Drawing.Point(441, 70);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(403, 129);
            this.grpExport.TabIndex = 12;
            this.grpExport.TabStop = false;
            this.grpExport.Tag = "pnlPanel";
            this.grpExport.Text = "Export Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 10;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "Location:";
            // 
            // cmbExpLocation
            // 
            this.cmbExpLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpLocation.FormattingEnabled = true;
            this.cmbExpLocation.Location = new System.Drawing.Point(77, 16);
            this.cmbExpLocation.Name = "cmbExpLocation";
            this.cmbExpLocation.Size = new System.Drawing.Size(280, 21);
            this.cmbExpLocation.TabIndex = 9;
            this.cmbExpLocation.Tag = "ddlDropDownList";
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "*.bak";
            this.openFile.FileName = "openFileDialog1";
            this.openFile.Filter = "SQL Backup|*.bak";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(2, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(850, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "Database Operations mentioned over here are bulky in nature. Kindly be patience f" +
                "or 10 to 15 minutes";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmImportExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 258);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpImport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportExportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "ImportExportData";
            this.Load += new System.EventHandler(this.frmImportExportData_Load);
            this.grpImport.ResumeLayout(false);
            this.grpImport.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpExport.ResumeLayout(false);
            this.grpExport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.ComboBox cmbImpLocation;
        private System.Windows.Forms.DateTimePicker dtFrmDate;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox grpImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoExport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RadioButton rdoImport;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbExpLocation;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Label label6;

    }
}