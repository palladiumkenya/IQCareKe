namespace IQCare.FormBuilder
{
    partial class frmRptFieldValidations
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
            this.lblTechArea = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.cmbTechnicalArea = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tvCustomlist = new System.Windows.Forms.TreeView();
            this.pnlCustomList = new System.Windows.Forms.Panel();
            this.pnlCustomList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(9, 22);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(85, 13);
            this.lblTechArea.TabIndex = 6;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service :";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(318, 479);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(114, 23);
            this.btnclose.TabIndex = 9;
            this.btnclose.Tag = "btnSingleText";
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // cmbTechnicalArea
            // 
            this.cmbTechnicalArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea.FormattingEnabled = true;
            this.cmbTechnicalArea.Location = new System.Drawing.Point(104, 18);
            this.cmbTechnicalArea.Name = "cmbTechnicalArea";
            this.cmbTechnicalArea.Size = new System.Drawing.Size(216, 21);
            this.cmbTechnicalArea.TabIndex = 12;
            this.cmbTechnicalArea.Tag = "ddlDropDownList";
            this.cmbTechnicalArea.SelectionChangeCommitted += new System.EventHandler(this.cmbTechnicalArea_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(753, 431);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "pnlPanel";
            // 
            // tvCustomlist
            // 
            this.tvCustomlist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tvCustomlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvCustomlist.ForeColor = System.Drawing.Color.Blue;
            this.tvCustomlist.HotTracking = true;
            this.tvCustomlist.LineColor = System.Drawing.Color.Blue;
            this.tvCustomlist.Location = new System.Drawing.Point(4, 4);
            this.tvCustomlist.Name = "tvCustomlist";
            this.tvCustomlist.ShowLines = false;
            this.tvCustomlist.Size = new System.Drawing.Size(736, 404);
            this.tvCustomlist.TabIndex = 1;
            this.tvCustomlist.Tag = "treeView";
            // 
            // pnlCustomList
            // 
            this.pnlCustomList.Controls.Add(this.tvCustomlist);
            this.pnlCustomList.Location = new System.Drawing.Point(9, 55);
            this.pnlCustomList.Name = "pnlCustomList";
            this.pnlCustomList.Size = new System.Drawing.Size(745, 413);
            this.pnlCustomList.TabIndex = 4;
            this.pnlCustomList.Tag = "pnlPanel";
            // 
            // frmRptFieldValidations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(761, 512);
            this.Controls.Add(this.cmbTechnicalArea);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.lblTechArea);
            this.Controls.Add(this.pnlCustomList);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRptFieldValidations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmform";
            this.Text = "Report Field Validation";
            this.Load += new System.EventHandler(this.frmRptFieldValidations_Load);
            this.pnlCustomList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.ComboBox cmbTechnicalArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tvCustomlist;
        private System.Windows.Forms.Panel pnlCustomList;
    }
}