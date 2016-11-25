namespace IQCare.FormBuilder
{
    partial class frmHomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHomePage));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSectionTitle = new System.Windows.Forms.TextBox();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            this.lblHomePageType = new System.Windows.Forms.Label();
            this.cmbHomePageType = new System.Windows.Forms.ComboBox();
            this.lblTechnicalArea = new System.Windows.Forms.Label();
            this.cmbTechnicalArea = new System.Windows.Forms.ComboBox();
            this.dgwQueryDetails = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwQueryDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(349, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 26);
            this.btnSave.TabIndex = 27;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(464, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 26);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(234, 460);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(109, 26);
            this.btnRemove.TabIndex = 38;
            this.btnRemove.Tag = "btnH25WFlexi";
            this.btnRemove.Text = "&Remove Indicator";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(793, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 37;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(796, 169);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(26, 26);
            this.btnUp.TabIndex = 36;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(798, 238);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 26);
            this.btnDown.TabIndex = 35;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSectionTitle);
            this.groupBox1.Controls.Add(this.lblSectionTitle);
            this.groupBox1.Controls.Add(this.lblHomePageType);
            this.groupBox1.Controls.Add(this.cmbHomePageType);
            this.groupBox1.Controls.Add(this.lblTechnicalArea);
            this.groupBox1.Controls.Add(this.cmbTechnicalArea);
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 49);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // txtSectionTitle
            // 
            this.txtSectionTitle.Location = new System.Drawing.Point(94, 18);
            this.txtSectionTitle.Name = "txtSectionTitle";
            this.txtSectionTitle.Size = new System.Drawing.Size(143, 20);
            this.txtSectionTitle.TabIndex = 40;
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.Location = new System.Drawing.Point(9, 21);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(66, 13);
            this.lblSectionTitle.TabIndex = 39;
            this.lblSectionTitle.Tag = "lblLabel";
            this.lblSectionTitle.Text = "Section Title";
            // 
            // lblHomePageType
            // 
            this.lblHomePageType.AutoSize = true;
            this.lblHomePageType.Location = new System.Drawing.Point(245, 21);
            this.lblHomePageType.Name = "lblHomePageType";
            this.lblHomePageType.Size = new System.Drawing.Size(90, 13);
            this.lblHomePageType.TabIndex = 35;
            this.lblHomePageType.Tag = "lblLabel";
            this.lblHomePageType.Text = "Home Page Type";
            // 
            // cmbHomePageType
            // 
            this.cmbHomePageType.BackColor = System.Drawing.Color.White;
            this.cmbHomePageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHomePageType.Items.AddRange(new object[] {
            "Select",
            "Facility Home",
            "Patient Home"});
            this.cmbHomePageType.Location = new System.Drawing.Point(339, 18);
            this.cmbHomePageType.Name = "cmbHomePageType";
            this.cmbHomePageType.Size = new System.Drawing.Size(157, 21);
            this.cmbHomePageType.TabIndex = 36;
            this.cmbHomePageType.Tag = "ddlDropDownList";
            // 
            // lblTechnicalArea
            // 
            this.lblTechnicalArea.AutoSize = true;
            this.lblTechnicalArea.Location = new System.Drawing.Point(507, 23);
            this.lblTechnicalArea.Name = "lblTechnicalArea";
            this.lblTechnicalArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechnicalArea.TabIndex = 37;
            this.lblTechnicalArea.Tag = "lblLabel";
            this.lblTechnicalArea.Text = "Service";
            // 
            // cmbTechnicalArea
            // 
            this.cmbTechnicalArea.BackColor = System.Drawing.Color.White;
            this.cmbTechnicalArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea.Location = new System.Drawing.Point(596, 18);
            this.cmbTechnicalArea.Name = "cmbTechnicalArea";
            this.cmbTechnicalArea.Size = new System.Drawing.Size(157, 21);
            this.cmbTechnicalArea.TabIndex = 38;
            this.cmbTechnicalArea.Tag = "ddlDropDownList";
            // 
            // dgwQueryDetails
            // 
            this.dgwQueryDetails.AllowUserToDeleteRows = false;
            this.dgwQueryDetails.AllowUserToResizeColumns = false;
            this.dgwQueryDetails.AllowUserToResizeRows = false;
            this.dgwQueryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwQueryDetails.Location = new System.Drawing.Point(15, 58);
            this.dgwQueryDetails.Name = "dgwQueryDetails";
            this.dgwQueryDetails.Size = new System.Drawing.Size(764, 396);
            this.dgwQueryDetails.TabIndex = 40;
            this.dgwQueryDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwQueryDetails_CellFormatting);
            this.dgwQueryDetails.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwQueryDetails_RowLeave);
            this.dgwQueryDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwQueryDetails_CellClick);
            this.dgwQueryDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwQueryDetails_DataError);
            // 
            // frmHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 510);
            this.Controls.Add(this.dgwQueryDetails);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHomePage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = " Home Page";
            this.Load += new System.EventHandler(this.frmHomePage_Load);
            this.Activated += new System.EventHandler(this.frmHomePage_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwQueryDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSectionTitle;
        private System.Windows.Forms.Label lblSectionTitle;
        private System.Windows.Forms.Label lblHomePageType;
        private System.Windows.Forms.ComboBox cmbHomePageType;
        private System.Windows.Forms.Label lblTechnicalArea;
        private System.Windows.Forms.ComboBox cmbTechnicalArea;
        private System.Windows.Forms.DataGridView dgwQueryDetails;


    }
}
