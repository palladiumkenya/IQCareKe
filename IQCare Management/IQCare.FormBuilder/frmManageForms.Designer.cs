namespace IQCare.FormBuilder
{
    partial class frmManageForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageForms));
            this.pnlManageForms = new System.Windows.Forms.Panel();
            this.btnLnkServiceArea = new System.Windows.Forms.Button();
            this.btnOrderForms = new System.Windows.Forms.Button();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.cmbFormStatus = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblFormStatus = new System.Windows.Forms.Label();
            this.dgwFormDetails = new System.Windows.Forms.DataGridView();
            this.pnlManageForms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlManageForms
            // 
            this.pnlManageForms.Controls.Add(this.btnLnkServiceArea);
            this.pnlManageForms.Controls.Add(this.btnOrderForms);
            this.pnlManageForms.Controls.Add(this.cmbTechArea);
            this.pnlManageForms.Controls.Add(this.lblTechArea);
            this.pnlManageForms.Controls.Add(this.cmbFormStatus);
            this.pnlManageForms.Controls.Add(this.btnDelete);
            this.pnlManageForms.Controls.Add(this.btnCancel);
            this.pnlManageForms.Controls.Add(this.btnPreview);
            this.pnlManageForms.Controls.Add(this.btnEdit);
            this.pnlManageForms.Controls.Add(this.btnAdd);
            this.pnlManageForms.Controls.Add(this.lblFormStatus);
            this.pnlManageForms.Controls.Add(this.dgwFormDetails);
            this.pnlManageForms.Location = new System.Drawing.Point(0, 1);
            this.pnlManageForms.Name = "pnlManageForms";
            this.pnlManageForms.Size = new System.Drawing.Size(962, 506);
            this.pnlManageForms.TabIndex = 0;
            this.pnlManageForms.Tag = "pnlPanel";
            // 
            // btnLnkServiceArea
            // 
            this.btnLnkServiceArea.Location = new System.Drawing.Point(598, 467);
            this.btnLnkServiceArea.Name = "btnLnkServiceArea";
            this.btnLnkServiceArea.Size = new System.Drawing.Size(130, 23);
            this.btnLnkServiceArea.TabIndex = 16;
            this.btnLnkServiceArea.Tag = "btnH25WFlexi";
            this.btnLnkServiceArea.Text = "&Link to Service Areas";
            this.btnLnkServiceArea.UseVisualStyleBackColor = true;
            this.btnLnkServiceArea.Click += new System.EventHandler(this.btnLnkServiceArea_Click);
            // 
            // btnOrderForms
            // 
            this.btnOrderForms.Location = new System.Drawing.Point(262, 467);
            this.btnOrderForms.Name = "btnOrderForms";
            this.btnOrderForms.Size = new System.Drawing.Size(106, 23);
            this.btnOrderForms.TabIndex = 15;
            this.btnOrderForms.Tag = "btnH25WFlexi";
            this.btnOrderForms.Text = "&Order Forms";
            this.btnOrderForms.UseVisualStyleBackColor = true;
            this.btnOrderForms.Click += new System.EventHandler(this.btnOrderForms_Click);
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.BackColor = System.Drawing.Color.White;
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.Items.AddRange(new object[] {
            "In Process",
            "Published",
            "Unpublished",
            "All"});
            this.cmbTechArea.Location = new System.Drawing.Point(522, 25);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(181, 21);
            this.cmbTechArea.TabIndex = 14;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectedIndexChanged += new System.EventHandler(this.cmbTechArea_SelectedIndexChanged);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(427, 28);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(43, 13);
            this.lblTechArea.TabIndex = 13;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service";
            // 
            // cmbFormStatus
            // 
            this.cmbFormStatus.BackColor = System.Drawing.Color.White;
            this.cmbFormStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormStatus.Items.AddRange(new object[] {
            "In Process",
            "Published",
            "Unpublished",
            "All"});
            this.cmbFormStatus.Location = new System.Drawing.Point(187, 25);
            this.cmbFormStatus.Name = "cmbFormStatus";
            this.cmbFormStatus.Size = new System.Drawing.Size(181, 21);
            this.cmbFormStatus.TabIndex = 12;
            this.cmbFormStatus.Tag = "ddlDropDownList";
            this.cmbFormStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFormStatus_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(486, 467);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(106, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Tag = "btnH25WFlexi";
            this.btnDelete.Text = "&Delete Form";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(374, 467);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(486, 438);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(106, 23);
            this.btnPreview.TabIndex = 9;
            this.btnPreview.Tag = "btnH25WFlexi";
            this.btnPreview.Text = "Pre&view Form";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(374, 438);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 23);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Tag = "btnH25WFlexi";
            this.btnEdit.Text = "&Edit Form";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(262, 438);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Tag = "btnH25WFlexi";
            this.btnAdd.Text = "C&reate New Form";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblFormStatus
            // 
            this.lblFormStatus.AutoSize = true;
            this.lblFormStatus.Location = new System.Drawing.Point(102, 28);
            this.lblFormStatus.Name = "lblFormStatus";
            this.lblFormStatus.Size = new System.Drawing.Size(63, 13);
            this.lblFormStatus.TabIndex = 2;
            this.lblFormStatus.Tag = "lblLabel";
            this.lblFormStatus.Text = "Form Status";
            // 
            // dgwFormDetails
            // 
            this.dgwFormDetails.AllowUserToDeleteRows = false;
            this.dgwFormDetails.AllowUserToResizeColumns = false;
            this.dgwFormDetails.AllowUserToResizeRows = false;
            this.dgwFormDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwFormDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwFormDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwFormDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwFormDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgwFormDetails.Location = new System.Drawing.Point(12, 52);
            this.dgwFormDetails.Name = "dgwFormDetails";
            this.dgwFormDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwFormDetails.Size = new System.Drawing.Size(947, 368);
            this.dgwFormDetails.TabIndex = 0;
            this.dgwFormDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellClick);
            this.dgwFormDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellDoubleClick);
            this.dgwFormDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFormDetails_CellFormatting);
            // 
            // frmManageForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 510);
            this.Controls.Add(this.pnlManageForms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Forms Details";
            this.Load += new System.EventHandler(this.frmManageForms_Load);
            this.pnlManageForms.ResumeLayout(false);
            this.pnlManageForms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlManageForms;
        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbFormStatus;
        private System.Windows.Forms.DataGridView dgwFormDetails;
        private System.Windows.Forms.ComboBox cmbTechArea;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.Button btnOrderForms;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLnkServiceArea;

    }
}