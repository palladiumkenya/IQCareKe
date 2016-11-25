namespace IQCare.FormBuilder
{
    partial class frmCareEndedList
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
            this.pnlCareEnded = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.BtnManage = new System.Windows.Forms.Button();
            this.dgwFormDetails = new System.Windows.Forms.DataGridView();
            this.cmbTechnicalArea = new System.Windows.Forms.ComboBox();
            this.lblTechnicalArea = new System.Windows.Forms.Label();
            this.cmbFormStatus = new System.Windows.Forms.ComboBox();
            this.lblFormStatus = new System.Windows.Forms.Label();
            this.pnlCareEnded.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCareEnded
            // 
            this.pnlCareEnded.Controls.Add(this.btnCancel);
            this.pnlCareEnded.Controls.Add(this.btnAdd);
            this.pnlCareEnded.Controls.Add(this.BtnManage);
            this.pnlCareEnded.Controls.Add(this.dgwFormDetails);
            this.pnlCareEnded.Controls.Add(this.cmbTechnicalArea);
            this.pnlCareEnded.Controls.Add(this.lblTechnicalArea);
            this.pnlCareEnded.Controls.Add(this.cmbFormStatus);
            this.pnlCareEnded.Controls.Add(this.lblFormStatus);
            this.pnlCareEnded.Location = new System.Drawing.Point(3, 2);
            this.pnlCareEnded.Name = "pnlCareEnded";
            this.pnlCareEnded.Size = new System.Drawing.Size(855, 506);
            this.pnlCareEnded.TabIndex = 21;
            this.pnlCareEnded.Tag = "pnlPanel";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(482, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(222, 457);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 23);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Tag = "btnH25WFlexi";
            this.btnAdd.Text = "Create New Form";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // BtnManage
            // 
            this.BtnManage.Location = new System.Drawing.Point(331, 457);
            this.BtnManage.Name = "BtnManage";
            this.BtnManage.Size = new System.Drawing.Size(148, 24);
            this.BtnManage.TabIndex = 21;
            this.BtnManage.Tag = "btnH25WFlexi";
            this.BtnManage.Text = "Manage CareEnded Fields";
            this.BtnManage.UseVisualStyleBackColor = true;
            this.BtnManage.Click += new System.EventHandler(this.BtnManage_Click);
            // 
            // dgwFormDetails
            // 
            this.dgwFormDetails.AllowUserToAddRows = false;
            this.dgwFormDetails.AllowUserToDeleteRows = false;
            this.dgwFormDetails.AllowUserToResizeColumns = false;
            this.dgwFormDetails.AllowUserToResizeRows = false;
            this.dgwFormDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwFormDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwFormDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwFormDetails.Location = new System.Drawing.Point(10, 64);
            this.dgwFormDetails.Name = "dgwFormDetails";
            this.dgwFormDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwFormDetails.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgwFormDetails.Size = new System.Drawing.Size(823, 368);
            this.dgwFormDetails.TabIndex = 20;
            this.dgwFormDetails.Tag = "dgwDataGridView";
            this.dgwFormDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellDoubleClick_1);
            this.dgwFormDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFormDetails_CellFormatting_1);
            this.dgwFormDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellClick);
            // 
            // cmbTechnicalArea
            // 
            this.cmbTechnicalArea.BackColor = System.Drawing.Color.White;
            this.cmbTechnicalArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea.FormattingEnabled = true;
            this.cmbTechnicalArea.Location = new System.Drawing.Point(628, 32);
            this.cmbTechnicalArea.Name = "cmbTechnicalArea";
            this.cmbTechnicalArea.Size = new System.Drawing.Size(167, 21);
            this.cmbTechnicalArea.TabIndex = 19;
            this.cmbTechnicalArea.Tag = "ddlDropDownList";
            this.cmbTechnicalArea.SelectedValueChanged += new System.EventHandler(this.cmbTechnicalArea_SelectedValueChanged);
            // 
            // lblTechnicalArea
            // 
            this.lblTechnicalArea.AutoSize = true;
            this.lblTechnicalArea.Location = new System.Drawing.Point(543, 35);
            this.lblTechnicalArea.Name = "lblTechnicalArea";
            this.lblTechnicalArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechnicalArea.TabIndex = 18;
            this.lblTechnicalArea.Tag = "lblLabel";
            this.lblTechnicalArea.Text = "Service";
            // 
            // cmbFormStatus
            // 
            this.cmbFormStatus.BackColor = System.Drawing.Color.White;
            this.cmbFormStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormStatus.FormattingEnabled = true;
            this.cmbFormStatus.Items.AddRange(new object[] {
            "All",
            "Published",
            "UnPublished"});
            this.cmbFormStatus.Location = new System.Drawing.Point(112, 29);
            this.cmbFormStatus.Name = "cmbFormStatus";
            this.cmbFormStatus.Size = new System.Drawing.Size(167, 21);
            this.cmbFormStatus.TabIndex = 17;
            this.cmbFormStatus.Tag = "ddlDropDownList";
            this.cmbFormStatus.SelectedValueChanged += new System.EventHandler(this.cmbFormStatus_SelectedValueChanged);
            // 
            // lblFormStatus
            // 
            this.lblFormStatus.AutoSize = true;
            this.lblFormStatus.Location = new System.Drawing.Point(43, 32);
            this.lblFormStatus.Name = "lblFormStatus";
            this.lblFormStatus.Size = new System.Drawing.Size(63, 13);
            this.lblFormStatus.TabIndex = 16;
            this.lblFormStatus.Tag = "lblLabel";
            this.lblFormStatus.Text = "Form Status";
            // 
            // frmCareEndedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 510);
            this.Controls.Add(this.pnlCareEnded);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCareEndedList";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Care Ended List";
            this.Load += new System.EventHandler(this.frmCareEndedList_Load);
            this.pnlCareEnded.ResumeLayout(false);
            this.pnlCareEnded.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCareEnded;
        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.ComboBox cmbFormStatus;
        private System.Windows.Forms.Label lblTechnicalArea;
        private System.Windows.Forms.ComboBox cmbTechnicalArea;
        private System.Windows.Forms.DataGridView dgwFormDetails;
        private System.Windows.Forms.Button BtnManage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
    }
}