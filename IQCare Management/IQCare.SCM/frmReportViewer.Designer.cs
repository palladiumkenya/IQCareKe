namespace IQCare.SCM
{
	partial class frmReportViewer
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
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.crViewer.AutoScroll = true;
            this.crViewer.AutoSize = true;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Location = new System.Drawing.Point(11, 12);
            this.crViewer.Name = "crViewer";
            this.crViewer.SelectionFormula = "";
            this.crViewer.ShowGroupTreeButton = false;
            this.crViewer.Size = new System.Drawing.Size(827, 499);
            this.crViewer.TabIndex = 0;
            this.crViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crViewer.ViewTimeSelectionFormula = "";
            this.crViewer.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(396, 517);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 545);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.crViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmReportViewer";
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.Button btnExit;

    }
}