namespace IQCare.SCM
{
    partial class frmViewGoodReceiveNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewGoodReceiveNote));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgwGRN = new System.Windows.Forms.DataGridView();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwGRN)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.btnclose);
            this.pnlBottom.Location = new System.Drawing.Point(0, 457);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(852, 47);
            this.pnlBottom.TabIndex = 61;
            this.pnlBottom.Tag = "pnlSubPanel";
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
            // dgwGRN
            // 
            this.dgwGRN.AllowUserToDeleteRows = false;
            this.dgwGRN.AllowUserToResizeColumns = false;
            this.dgwGRN.AllowUserToResizeRows = false;
            this.dgwGRN.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwGRN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwGRN.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwGRN.Location = new System.Drawing.Point(0, 0);
            this.dgwGRN.Name = "dgwGRN";
            this.dgwGRN.Size = new System.Drawing.Size(852, 452);
            this.dgwGRN.TabIndex = 62;
            this.dgwGRN.Tag = "dgwDataGridView";
            this.dgwGRN.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwGRN_CellClick);
            // 
            // frmViewGoodReceiveNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.dgwGRN);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewGoodReceiveNote";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "View Good Receive Note";
            this.Load += new System.EventHandler(this.frmViewGoodReceiveNote_Load);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwGRN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView dgwGRN;
    }
}