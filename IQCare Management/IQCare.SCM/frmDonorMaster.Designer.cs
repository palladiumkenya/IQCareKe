namespace IQCare.SCM
{
    partial class frmDonorMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonorMaster));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDonorShortName = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.Address = new System.Windows.Forms.Label();
            this.txtDonorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDonorID = new System.Windows.Forms.TextBox();
            this.lblProgramID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgwDonorList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDonorList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDonorShortName);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.Address);
            this.groupBox1.Controls.Add(this.txtDonorName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDonorID);
            this.groupBox1.Controls.Add(this.lblProgramID);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.ddlStatus);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 83);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // txtDonorShortName
            // 
            this.txtDonorShortName.Location = new System.Drawing.Point(329, 45);
            this.txtDonorShortName.MaxLength = 200;
            this.txtDonorShortName.Name = "txtDonorShortName";
            this.txtDonorShortName.Size = new System.Drawing.Size(228, 20);
            this.txtDonorShortName.TabIndex = 4;
            this.txtDonorShortName.Tag = "txtTextBox";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(611, 44);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(80, 25);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Address
            // 
            this.Address.AutoSize = true;
            this.Address.Location = new System.Drawing.Point(234, 52);
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(98, 13);
            this.Address.TabIndex = 45;
            this.Address.Tag = "lblLabelRequired";
            this.Address.Text = "Donor Short Name:";
            // 
            // txtDonorName
            // 
            this.txtDonorName.Location = new System.Drawing.Point(329, 18);
            this.txtDonorName.MaxLength = 200;
            this.txtDonorName.Name = "txtDonorName";
            this.txtDonorName.Size = new System.Drawing.Size(364, 20);
            this.txtDonorName.TabIndex = 2;
            this.txtDonorName.Tag = "txtTextBox";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 42;
            this.label2.Tag = "lblLabelRequired";
            this.label2.Text = "Donor Name:";
            // 
            // txtDonorID
            // 
            this.txtDonorID.Location = new System.Drawing.Point(73, 18);
            this.txtDonorID.MaxLength = 50;
            this.txtDonorID.Name = "txtDonorID";
            this.txtDonorID.Size = new System.Drawing.Size(148, 20);
            this.txtDonorID.TabIndex = 1;
            this.txtDonorID.Tag = "txtTextBox";
            this.txtDonorID.TextChanged += new System.EventHandler(this.txtDonorID_TextChanged);
            // 
            // lblProgramID
            // 
            this.lblProgramID.AutoSize = true;
            this.lblProgramID.Location = new System.Drawing.Point(13, 23);
            this.lblProgramID.Name = "lblProgramID";
            this.lblProgramID.Size = new System.Drawing.Size(53, 13);
            this.lblProgramID.TabIndex = 39;
            this.lblProgramID.Tag = "lblLabelRequired";
            this.lblProgramID.Text = "Donor ID:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 49);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabelRequired";
            this.lblStatus.Text = "Status:";
            // 
            // ddlStatus
            // 
            this.ddlStatus.BackColor = System.Drawing.Color.White;
            this.ddlStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ddlStatus.Location = new System.Drawing.Point(73, 46);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(148, 21);
            this.ddlStatus.TabIndex = 3;
            this.ddlStatus.Tag = "ddlDropDownList";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 60;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 56;
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
            this.btnClose.TabIndex = 57;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgwDonorList
            // 
            this.dgwDonorList.AllowUserToAddRows = false;
            this.dgwDonorList.AllowUserToDeleteRows = false;
            this.dgwDonorList.AllowUserToResizeColumns = false;
            this.dgwDonorList.AllowUserToResizeRows = false;
            this.dgwDonorList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwDonorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDonorList.Location = new System.Drawing.Point(5, 97);
            this.dgwDonorList.Name = "dgwDonorList";
            this.dgwDonorList.Size = new System.Drawing.Size(764, 352);
            this.dgwDonorList.TabIndex = 50;
            this.dgwDonorList.Tag = "dgwDataGridView";
            this.dgwDonorList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwDonorList_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(798, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 74;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(801, 298);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 26);
            this.btnDown.TabIndex = 55;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(801, 180);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(26, 26);
            this.btnUp.TabIndex = 54;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // frmDonorMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.dgwDonorList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDonorMaster";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Donor Master";
            this.Load += new System.EventHandler(this.frmDonorMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwDonorList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDonorShortName;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label Address;
        private System.Windows.Forms.TextBox txtDonorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDonorID;
        private System.Windows.Forms.Label lblProgramID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgwDonorList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
    }
}