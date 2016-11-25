namespace IQCare.SCM
{
    partial class frmLabItemDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabItemDetails));
            this.dgwItemSubitemDetails = new System.Windows.Forms.DataGridView();
            this.SubtestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOINCCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Labtest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarginPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffectiveDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLabTest = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwItemSubitemDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwItemSubitemDetails
            // 
            this.dgwItemSubitemDetails.AllowUserToDeleteRows = false;
            this.dgwItemSubitemDetails.AllowUserToResizeColumns = false;
            this.dgwItemSubitemDetails.AllowUserToResizeRows = false;
            this.dgwItemSubitemDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwItemSubitemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwItemSubitemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubtestId,
            this.LOINCCode,
            this.Labtest,
            this.LabLocation,
            this.LabCost,
            this.MarginPerc,
            this.EffectiveDate,
            this.Status});
            this.dgwItemSubitemDetails.Location = new System.Drawing.Point(0, 58);
            this.dgwItemSubitemDetails.Name = "dgwItemSubitemDetails";
            this.dgwItemSubitemDetails.Size = new System.Drawing.Size(852, 398);
            this.dgwItemSubitemDetails.TabIndex = 43;
            this.dgwItemSubitemDetails.Tag = "dgwDataGridView";
            this.dgwItemSubitemDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwItemSubitemDetails_CellClick);
            // 
            // SubtestId
            // 
            this.SubtestId.HeaderText = "SubTestId";
            this.SubtestId.MinimumWidth = 2;
            this.SubtestId.Name = "SubtestId";
            this.SubtestId.Width = 5;
            // 
            // LOINCCode
            // 
            this.LOINCCode.HeaderText = "LOINC Code";
            this.LOINCCode.Name = "LOINCCode";
            // 
            // Labtest
            // 
            this.Labtest.HeaderText = "Lab Test";
            this.Labtest.Name = "Labtest";
            this.Labtest.Width = 184;
            // 
            // LabLocation
            // 
            this.LabLocation.HeaderText = "Location";
            this.LabLocation.Name = "LabLocation";
            this.LabLocation.Width = 174;
            // 
            // LabCost
            // 
            this.LabCost.HeaderText = "Cost";
            this.LabCost.Name = "LabCost";
            this.LabCost.Width = 95;
            // 
            // MarginPerc
            // 
            this.MarginPerc.HeaderText = "Margin %";
            this.MarginPerc.Name = "MarginPerc";
            this.MarginPerc.Width = 83;
            // 
            // EffectiveDate
            // 
            this.EffectiveDate.HeaderText = "Effective Date";
            this.EffectiveDate.Name = "EffectiveDate";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 60;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLabTest);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 54);
            this.panel1.TabIndex = 2;
            this.panel1.Tag = "pnlSubPanelSCM";
            // 
            // txtLabTest
            // 
            this.txtLabTest.Location = new System.Drawing.Point(213, 18);
            this.txtLabTest.Name = "txtLabTest";
            this.txtLabTest.Size = new System.Drawing.Size(345, 20);
            this.txtLabTest.TabIndex = 46;
            this.txtLabTest.Tag = "txtTextBox";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(156, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 45;
            this.label8.Tag = "lblLabel";
            this.label8.Text = "Lab Tests:";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 59;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.Window;
            this.btnclose.Location = new System.Drawing.Point(771, 10);
            this.btnclose.Margin = new System.Windows.Forms.Padding(0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(80, 25);
            this.btnclose.TabIndex = 45;
            this.btnclose.Tag = "btnSingleText";
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "SubTestId";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "LOINC Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Lab Test";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 184;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Location";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 154;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 95;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Margin %";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 83;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Effective Date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Status";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Window;
            this.btnSearch.Location = new System.Drawing.Point(580, 15);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 48;
            this.btnSearch.Tag = "btnSingleText";
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmLabItemDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgwItemSubitemDetails);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLabItemDetails";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Add /Update Lab Test";
            this.Load += new System.EventHandler(this.frmLabItemDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwItemSubitemDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwItemSubitemDetails;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLabTest;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubtestId;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOINCCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Labtest;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarginPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffectiveDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button btnSearch;
    }
}