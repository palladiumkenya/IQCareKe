namespace IQCare.FormBuilder
{
    partial class frmManageFBTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageFBTab));
            this.pnlManageSection = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lstTabPos = new System.Windows.Forms.ListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.pnlManageSection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlManageSection
            // 
            this.pnlManageSection.Controls.Add(this.label1);
            this.pnlManageSection.Controls.Add(this.btnRemove);
            this.pnlManageSection.Controls.Add(this.btnSubmit);
            this.pnlManageSection.Controls.Add(this.btnUp);
            this.pnlManageSection.Controls.Add(this.lstTabPos);
            this.pnlManageSection.Controls.Add(this.btnDown);
            this.pnlManageSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlManageSection.Location = new System.Drawing.Point(-3, 0);
            this.pnlManageSection.Name = "pnlManageSection";
            this.pnlManageSection.Size = new System.Drawing.Size(272, 233);
            this.pnlManageSection.TabIndex = 8;
            this.pnlManageSection.Tag = "pnlPanel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(25, 185);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Tag = "btnH25WFlexi";
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(106, 185);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(73, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Tag = "btnH25WFlexi";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(216, 36);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(26, 26);
            this.btnUp.TabIndex = 2;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lstTabPos
            // 
            this.lstTabPos.FormattingEnabled = true;
            this.lstTabPos.Location = new System.Drawing.Point(3, 6);
            this.lstTabPos.Name = "lstTabPos";
            this.lstTabPos.Size = new System.Drawing.Size(207, 173);
            this.lstTabPos.TabIndex = 0;
            this.lstTabPos.Tag = "lstListBox";
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(216, 108);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 26);
            this.btnDown.TabIndex = 1;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // frmManageFBTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 233);
            this.Controls.Add(this.pnlManageSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageFBTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Manage Tab";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmManageFBTab_Load);
            this.pnlManageSection.ResumeLayout(false);
            this.pnlManageSection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlManageSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ListBox lstTabPos;
        private System.Windows.Forms.Button btnDown;
    }
}