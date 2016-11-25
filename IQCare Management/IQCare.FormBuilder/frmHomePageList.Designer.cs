namespace IQCare.FormBuilder
{
    partial class frmHomePageList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbFormStatus = new System.Windows.Forms.ComboBox();
            this.lblFormStatus = new System.Windows.Forms.Label();
            this.lblTechnicalArea = new System.Windows.Forms.Label();
            this.pnlHomePage = new System.Windows.Forms.Panel();
            this.cmbTechnicalArea1 = new System.Windows.Forms.ComboBox();
            this.dgwFormDetails = new System.Windows.Forms.DataGridView();
            this.pnlHomePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(454, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 31);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(297, 465);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 31);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Tag = "btnH25WFlexi";
            this.btnAdd.Text = "&New Home Page";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbFormStatus
            // 
            this.cmbFormStatus.BackColor = System.Drawing.Color.White;
            this.cmbFormStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormStatus.Items.AddRange(new object[] {
            "All",
            "Published",
            "Un-Published"});
            this.cmbFormStatus.Location = new System.Drawing.Point(167, 31);
            this.cmbFormStatus.Name = "cmbFormStatus";
            this.cmbFormStatus.Size = new System.Drawing.Size(157, 21);
            this.cmbFormStatus.TabIndex = 17;
            this.cmbFormStatus.Tag = "ddlDropDownList";
            this.cmbFormStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFormStatus_SelectedIndexChanged);
            // 
            // lblFormStatus
            // 
            this.lblFormStatus.AutoSize = true;
            this.lblFormStatus.Location = new System.Drawing.Point(76, 34);
            this.lblFormStatus.Name = "lblFormStatus";
            this.lblFormStatus.Size = new System.Drawing.Size(63, 13);
            this.lblFormStatus.TabIndex = 16;
            this.lblFormStatus.Tag = "lblLabel";
            this.lblFormStatus.Text = "Form Status";
            // 
            // lblTechnicalArea
            // 
            this.lblTechnicalArea.AutoSize = true;
            this.lblTechnicalArea.Location = new System.Drawing.Point(484, 34);
            this.lblTechnicalArea.Name = "lblTechnicalArea";
            this.lblTechnicalArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechnicalArea.TabIndex = 18;
            this.lblTechnicalArea.Tag = "lblLabel";
            this.lblTechnicalArea.Text = "Service";
            // 
            // pnlHomePage
            // 
            this.pnlHomePage.Controls.Add(this.cmbTechnicalArea1);
            this.pnlHomePage.Controls.Add(this.btnAdd);
            this.pnlHomePage.Controls.Add(this.btnCancel);
            this.pnlHomePage.Controls.Add(this.lblFormStatus);
            this.pnlHomePage.Controls.Add(this.cmbFormStatus);
            this.pnlHomePage.Controls.Add(this.lblTechnicalArea);
            this.pnlHomePage.Controls.Add(this.dgwFormDetails);
            this.pnlHomePage.Location = new System.Drawing.Point(2, 2);
            this.pnlHomePage.Name = "pnlHomePage";
            this.pnlHomePage.Size = new System.Drawing.Size(848, 542);
            this.pnlHomePage.TabIndex = 21;
            this.pnlHomePage.Tag = "pnlPanel";
            // 
            // cmbTechnicalArea1
            // 
            this.cmbTechnicalArea1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea1.FormattingEnabled = true;
            this.cmbTechnicalArea1.Location = new System.Drawing.Point(574, 30);
            this.cmbTechnicalArea1.Name = "cmbTechnicalArea1";
            this.cmbTechnicalArea1.Size = new System.Drawing.Size(167, 21);
            this.cmbTechnicalArea1.TabIndex = 19;
            this.cmbTechnicalArea1.SelectedIndexChanged += new System.EventHandler(this.cmbTechnicalArea1_SelectedIndexChanged);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwFormDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgwFormDetails.Location = new System.Drawing.Point(3, 58);
            this.dgwFormDetails.Name = "dgwFormDetails";
            this.dgwFormDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwFormDetails.Size = new System.Drawing.Size(842, 401);
            this.dgwFormDetails.TabIndex = 0;
            this.dgwFormDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellDoubleClick);
            this.dgwFormDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFormDetails_CellFormatting);
            this.dgwFormDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFormDetails_CellClick);
            // 
            // frmHomePageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 510);
            this.Controls.Add(this.pnlHomePage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHomePageList";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = " Home Page List";
            this.Load += new System.EventHandler(this.frmHomePageList_Load);
            this.pnlHomePage.ResumeLayout(false);
            this.pnlHomePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFormDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbFormStatus;
        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.Label lblTechnicalArea;
        private System.Windows.Forms.Panel pnlHomePage;
        private System.Windows.Forms.DataGridView dgwFormDetails;
        private System.Windows.Forms.ComboBox cmbTechnicalArea1;
    }
}
