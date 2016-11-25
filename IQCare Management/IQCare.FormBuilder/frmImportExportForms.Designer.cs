namespace IQCare.FormBuilder
{
    partial class frmImportExportForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportExportForms));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ddlFormType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkLstBoxForms = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ddlFormType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbTechArea);
            this.panel1.Controls.Add(this.lblTechArea);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 503);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "pnlPanel";
            // 
            // ddlFormType
            // 
            this.ddlFormType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFormType.FormattingEnabled = true;
            this.ddlFormType.Items.AddRange(new object[] {
            "Forms",
            "Home Pages"});
            this.ddlFormType.Location = new System.Drawing.Point(504, 13);
            this.ddlFormType.Name = "ddlFormType";
            this.ddlFormType.Size = new System.Drawing.Size(289, 21);
            this.ddlFormType.TabIndex = 5;
            this.ddlFormType.Tag = "ddlDropDownList";
            this.ddlFormType.SelectedIndexChanged += new System.EventHandler(this.ddlFormType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Form Type";
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.FormattingEnabled = true;
            this.cmbTechArea.Location = new System.Drawing.Point(142, 13);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(289, 21);
            this.cmbTechArea.TabIndex = 3;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectedIndexChanged += new System.EventHandler(this.cmbTechArea_SelectedIndexChanged);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(47, 16);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechArea.TabIndex = 2;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.chkCheckAll);
            this.groupBox2.Location = new System.Drawing.Point(7, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(840, 391);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "pnlPanel";
            this.groupBox2.Text = "Export";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.chkLstBoxForms);
            this.panel2.Location = new System.Drawing.Point(43, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(779, 310);
            this.panel2.TabIndex = 5;
            this.panel2.Tag = "pnlPanel";
           
            // 
            // chkLstBoxForms
            // 
            this.chkLstBoxForms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkLstBoxForms.CheckOnClick = true;
            this.chkLstBoxForms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstBoxForms.FormattingEnabled = true;
            this.chkLstBoxForms.Location = new System.Drawing.Point(3, 3);
            this.chkLstBoxForms.Name = "chkLstBoxForms";
            this.chkLstBoxForms.Size = new System.Drawing.Size(773, 17);
            this.chkLstBoxForms.TabIndex = 0;
            this.chkLstBoxForms.ThreeDCheckBoxes = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(534, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 26);
            this.btnClose.TabIndex = 4;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(428, 357);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 26);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Tag = "btnH25WFlexi";
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(316, 357);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(83, 26);
            this.btnExport.TabIndex = 2;
            this.btnExport.Tag = "btnH25WFlexi";
            this.btnExport.Text = "Expor&t";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCheckAll.Location = new System.Drawing.Point(119, 335);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(83, 20);
            this.chkCheckAll.TabIndex = 1;
            this.chkCheckAll.Text = "Check All";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            this.chkCheckAll.CheckedChanged += new System.EventHandler(this.chkCheckAll_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "pnlPanel";
            this.groupBox1.Text = "Import";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(469, 19);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(99, 20);
            this.btnImport.TabIndex = 3;
            this.btnImport.Tag = "btnH25WFlexi";
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowse.Location = new System.Drawing.Point(432, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Tag = "";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.AcceptsReturn = true;
            this.txtFileName.Location = new System.Drawing.Point(135, 20);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(291, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Field Name";
            // 
            // frmImportExportForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 509);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportExportForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Import Export Forms";
            this.Load += new System.EventHandler(this.frmImportExportForms_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.CheckedListBox chkLstBoxForms;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbTechArea;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox ddlFormType;
        private System.Windows.Forms.Label label2;
    }
}