namespace IQCare.FormBuilder
{
    partial class frmViewAssociation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewAssociation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTechnicalArea = new System.Windows.Forms.Label();
            this.cmbTechnicalArea1 = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgwViewAssociation = new System.Windows.Forms.DataGridView();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwViewAssociation)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTechnicalArea);
            this.panel1.Controls.Add(this.cmbTechnicalArea1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.dgwViewAssociation);
            this.panel1.Controls.Add(this.btnShowAll);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtFieldName);
            this.panel1.Controls.Add(this.lblFieldName);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 515);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "pnlPanel";
            // 
            // lblTechnicalArea
            // 
            this.lblTechnicalArea.AutoSize = true;
            this.lblTechnicalArea.Location = new System.Drawing.Point(357, 30);
            this.lblTechnicalArea.Name = "lblTechnicalArea";
            this.lblTechnicalArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechnicalArea.TabIndex = 7;
            this.lblTechnicalArea.Tag = "lblLabel";
            this.lblTechnicalArea.Text = "Service";
            // 
            // cmbTechnicalArea1
            // 
            this.cmbTechnicalArea1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea1.DropDownWidth = 157;
            this.cmbTechnicalArea1.FormattingEnabled = true;
            this.cmbTechnicalArea1.Location = new System.Drawing.Point(439, 26);
            this.cmbTechnicalArea1.Name = "cmbTechnicalArea1";
            this.cmbTechnicalArea1.Size = new System.Drawing.Size(157, 21);
            this.cmbTechnicalArea1.TabIndex = 6;
            this.cmbTechnicalArea1.Tag = "ddlDropDownList";
            this.cmbTechnicalArea1.SelectionChangeCommitted += new System.EventHandler(this.cmbTechnicalArea1_SelectionChangeCommitted);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(376, 474);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgwViewAssociation
            // 
            this.dgwViewAssociation.AllowUserToAddRows = false;
            this.dgwViewAssociation.AllowUserToDeleteRows = false;
            this.dgwViewAssociation.AllowUserToResizeColumns = false;
            this.dgwViewAssociation.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgwViewAssociation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwViewAssociation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwViewAssociation.Location = new System.Drawing.Point(31, 64);
            this.dgwViewAssociation.Name = "dgwViewAssociation";
            this.dgwViewAssociation.ReadOnly = true;
            this.dgwViewAssociation.Size = new System.Drawing.Size(800, 390);
            this.dgwViewAssociation.TabIndex = 4;
            this.dgwViewAssociation.Tag = "dgwDataGridView";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(700, 27);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(75, 23);
            this.btnShowAll.TabIndex = 3;
            this.btnShowAll.Tag = "btnH25WFlexi";
            this.btnShowAll.Text = "Show All ";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(619, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Tag = "btnSingleText";
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(121, 27);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(216, 20);
            this.txtFieldName.TabIndex = 1;
            this.txtFieldName.Tag = "txtTextBox";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(46, 29);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 0;
            this.lblFieldName.Tag = "lblLabel";
            this.lblFieldName.Text = "Field Name";
            // 
            // frmViewAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 514);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewAssociation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Form-Field Association";
            this.Load += new System.EventHandler(this.frmViewAssociation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwViewAssociation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgwViewAssociation;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTechnicalArea;
        private System.Windows.Forms.ComboBox cmbTechnicalArea1;

    }
}