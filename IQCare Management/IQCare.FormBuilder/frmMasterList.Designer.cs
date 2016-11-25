namespace IQCare.FormBuilder
{
    partial class frmMasterList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMasterList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnaddupdate = new System.Windows.Forms.Button();
            this.txtSectionTitle = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbHomePageType = new System.Windows.Forms.ComboBox();
            this.dgwMasterListDetails = new System.Windows.Forms.DataGridView();
            this.Items = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwMasterListDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnaddupdate);
            this.groupBox1.Controls.Add(this.txtSectionTitle);
            this.groupBox1.Controls.Add(this.lblItemName);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.cmbHomePageType);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 49);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // btnaddupdate
            // 
            this.btnaddupdate.Location = new System.Drawing.Point(626, 17);
            this.btnaddupdate.Name = "btnaddupdate";
            this.btnaddupdate.Size = new System.Drawing.Size(109, 21);
            this.btnaddupdate.TabIndex = 41;
            this.btnaddupdate.Tag = "btnH25WFlexi";
            this.btnaddupdate.Text = "Add/Update";
            this.btnaddupdate.UseVisualStyleBackColor = true;
            // 
            // txtSectionTitle
            // 
            this.txtSectionTitle.Location = new System.Drawing.Point(94, 18);
            this.txtSectionTitle.Name = "txtSectionTitle";
            this.txtSectionTitle.Size = new System.Drawing.Size(224, 20);
            this.txtSectionTitle.TabIndex = 40;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(9, 21);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(58, 13);
            this.lblItemName.TabIndex = 39;
            this.lblItemName.Tag = "lblLabel";
            this.lblItemName.Text = "Item Name";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(340, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabel";
            this.lblStatus.Text = "Status";
            // 
            // cmbHomePageType
            // 
            this.cmbHomePageType.BackColor = System.Drawing.Color.White;
            this.cmbHomePageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHomePageType.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbHomePageType.Items.AddRange(new object[] {
            "Select",
            "Facility Home",
            "Patient Home"});
            this.cmbHomePageType.Location = new System.Drawing.Point(409, 17);
            this.cmbHomePageType.Name = "cmbHomePageType";
            this.cmbHomePageType.Size = new System.Drawing.Size(157, 21);
            this.cmbHomePageType.TabIndex = 36;
            this.cmbHomePageType.Tag = "ddlDropDownList";
            // 
            // dgwMasterListDetails
            // 
            this.dgwMasterListDetails.AllowUserToDeleteRows = false;
            this.dgwMasterListDetails.AllowUserToResizeColumns = false;
            this.dgwMasterListDetails.AllowUserToResizeRows = false;
            this.dgwMasterListDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwMasterListDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Items,
            this.Status});
            this.dgwMasterListDetails.Location = new System.Drawing.Point(12, 78);
            this.dgwMasterListDetails.Name = "dgwMasterListDetails";
            this.dgwMasterListDetails.Size = new System.Drawing.Size(764, 396);
            this.dgwMasterListDetails.TabIndex = 41;
            this.dgwMasterListDetails.Tag = "dgwDataGridView";
            // 
            // Items
            // 
            this.Items.HeaderText = "Items";
            this.Items.Name = "Items";
            this.Items.Width = 450;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 271;
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(799, 193);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(26, 26);
            this.btnUp.TabIndex = 44;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(799, 311);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 26);
            this.btnDown.TabIndex = 45;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(796, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 46;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // frmMasterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 504);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.dgwMasterListDetails);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMasterList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "frmMasterList";
            this.Load += new System.EventHandler(this.frmMasterList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwMasterListDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSectionTitle;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbHomePageType;
        private System.Windows.Forms.Button btnaddupdate;
        private System.Windows.Forms.DataGridView dgwMasterListDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Items;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label1;
    }
}