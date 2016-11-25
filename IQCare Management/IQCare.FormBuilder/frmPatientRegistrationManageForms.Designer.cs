namespace IQCare.FormBuilder
{
    partial class frmPatientRegistrationManageForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientRegistrationManageForms));
            this.pnlManageForms = new System.Windows.Forms.Panel();
            this.cmbFormStatus = new System.Windows.Forms.ComboBox();
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
            this.pnlManageForms.Controls.Add(this.cmbFormStatus);
            this.pnlManageForms.Controls.Add(this.btnCancel);
            this.pnlManageForms.Controls.Add(this.btnPreview);
            this.pnlManageForms.Controls.Add(this.btnEdit);
            this.pnlManageForms.Controls.Add(this.btnAdd);
            this.pnlManageForms.Controls.Add(this.lblFormStatus);
            this.pnlManageForms.Controls.Add(this.dgwFormDetails);
            this.pnlManageForms.Location = new System.Drawing.Point(-1, -1);
            this.pnlManageForms.Name = "pnlManageForms";
            this.pnlManageForms.Size = new System.Drawing.Size(855, 506);
            this.pnlManageForms.TabIndex = 1;
            this.pnlManageForms.Tag = "pnlPanel";
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
            this.dgwFormDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwFormDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwFormDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgwFormDetails.Location = new System.Drawing.Point(12, 52);
            this.dgwFormDetails.Name = "dgwFormDetails";
            this.dgwFormDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwFormDetails.Size = new System.Drawing.Size(823, 368);
            this.dgwFormDetails.TabIndex = 0;
            this.dgwFormDetails.Tag = "";
            this.dgwFormDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellDoubleClick);
            this.dgwFormDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFormDetails_CellFormatting);
            this.dgwFormDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellClick);
            // 
            // frmPatientRegistrationManageForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.pnlManageForms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientRegistrationManageForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Registration Form";
            this.Load += new System.EventHandler(this.frmPatientRegManageForms_Load);
            this.pnlManageForms.ResumeLayout(false);
            this.pnlManageForms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlManageForms;
        private System.Windows.Forms.ComboBox cmbFormStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.DataGridView dgwFormDetails;
    }
}