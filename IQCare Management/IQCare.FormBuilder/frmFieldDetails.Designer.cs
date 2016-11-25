namespace IQCare.FormBuilder
{
    partial class frmFieldDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFieldDetails));
            this.pnlFieldDetails = new System.Windows.Forms.Panel();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnadd = new System.Windows.Forms.Button();
            this.dgwFieldDetails = new System.Windows.Forms.DataGridView();
            this.btnshowall = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtfieldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFieldDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFieldDetails
            // 
            this.pnlFieldDetails.Controls.Add(this.dgwFieldDetails);
            this.pnlFieldDetails.Controls.Add(this.cmbTechArea);
            this.pnlFieldDetails.Controls.Add(this.label2);
            this.pnlFieldDetails.Controls.Add(this.btndelete);
            this.pnlFieldDetails.Controls.Add(this.btnclose);
            this.pnlFieldDetails.Controls.Add(this.btnsave);
            this.pnlFieldDetails.Controls.Add(this.btnadd);
            this.pnlFieldDetails.Controls.Add(this.btnshowall);
            this.pnlFieldDetails.Controls.Add(this.btnSearch);
            this.pnlFieldDetails.Controls.Add(this.txtfieldName);
            this.pnlFieldDetails.Controls.Add(this.label1);
            this.pnlFieldDetails.Location = new System.Drawing.Point(-1, 1);
            this.pnlFieldDetails.Name = "pnlFieldDetails";
            this.pnlFieldDetails.Size = new System.Drawing.Size(1052, 510);
            this.pnlFieldDetails.TabIndex = 0;
            this.pnlFieldDetails.Tag = "pnlPanel";
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.FormattingEnabled = true;
            this.cmbTechArea.Location = new System.Drawing.Point(635, 30);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(216, 21);
            this.cmbTechArea.TabIndex = 11;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectionChangeCommitted += new System.EventHandler(this.cmbTechArea_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 10;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Service :";
            // 
            // btndelete
            // 
            this.btndelete.Location = new System.Drawing.Point(554, 478);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(75, 23);
            this.btndelete.TabIndex = 9;
            this.btndelete.Tag = "btnSingleText";
            this.btndelete.Text = "&Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(457, 478);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 8;
            this.btnclose.Tag = "btnSingleText";
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(359, 478);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 7;
            this.btnsave.Tag = "btnSingleText";
            this.btnsave.Text = "&Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(206, 478);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(106, 23);
            this.btnadd.TabIndex = 6;
            this.btnadd.Tag = "btnH25W80Flexi";
            this.btnadd.Text = "&Add Custom Field";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // dgwFieldDetails
            // 
            this.dgwFieldDetails.AllowUserToAddRows = false;
            this.dgwFieldDetails.AllowUserToDeleteRows = false;
            this.dgwFieldDetails.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgwFieldDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwFieldDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwFieldDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwFieldDetails.Location = new System.Drawing.Point(13, 57);
            this.dgwFieldDetails.Name = "dgwFieldDetails";
            this.dgwFieldDetails.Size = new System.Drawing.Size(1036, 402);
            this.dgwFieldDetails.TabIndex = 5;
            this.dgwFieldDetails.Tag = "dgwDataGridView";
            this.dgwFieldDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFieldDetails_CellClick);
            this.dgwFieldDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwFieldDetails_CellFormatting);
            this.dgwFieldDetails.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgwFieldDetails_CellMouseClick);
            this.dgwFieldDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwFieldDetails_CellValueChanged);
            this.dgwFieldDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwFieldDetails_DataError);
            this.dgwFieldDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwFieldDetails_EditingControlShowing);
            this.dgwFieldDetails.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgwFieldDetails_RowValidating);
            // 
            // btnshowall
            // 
            this.btnshowall.Location = new System.Drawing.Point(399, 28);
            this.btnshowall.Name = "btnshowall";
            this.btnshowall.Size = new System.Drawing.Size(75, 23);
            this.btnshowall.TabIndex = 4;
            this.btnshowall.Tag = "btnH25WFlexi";
            this.btnshowall.Text = "S&how All";
            this.btnshowall.UseVisualStyleBackColor = true;
            this.btnshowall.Click += new System.EventHandler(this.btnshowall_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(318, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Tag = "btnSingleText";
            this.btnSearch.Text = "S&earch";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtfieldName
            // 
            this.txtfieldName.Location = new System.Drawing.Point(96, 28);
            this.txtfieldName.Name = "txtfieldName";
            this.txtfieldName.Size = new System.Drawing.Size(216, 20);
            this.txtfieldName.TabIndex = 2;
            this.txtfieldName.Tag = "txtTextBox";
            this.txtfieldName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfieldName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Field Name :";
            // 
            // frmFieldDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 512);
            this.Controls.Add(this.pnlFieldDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFieldDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Field Details";
            this.Load += new System.EventHandler(this.frmFieldDetails_Load);
            this.pnlFieldDetails.ResumeLayout(false);
            this.pnlFieldDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwFieldDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFieldDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgwFieldDetails;
        private System.Windows.Forms.Button btnshowall;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtfieldName;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.ComboBox cmbTechArea;
        private System.Windows.Forms.Label label2;
    }
}