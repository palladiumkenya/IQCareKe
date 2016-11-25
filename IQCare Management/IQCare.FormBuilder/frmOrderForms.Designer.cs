namespace IQCare.FormBuilder
{
    partial class frmOrderForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderForms));
            this.pnlManageSection = new System.Windows.Forms.Panel();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lstForms = new System.Windows.Forms.ListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.pnlManageSection.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlManageSection
            // 
            this.pnlManageSection.Controls.Add(this.cmbTechArea);
            this.pnlManageSection.Controls.Add(this.lblTechArea);
            this.pnlManageSection.Controls.Add(this.label1);
            this.pnlManageSection.Controls.Add(this.btnSubmit);
            this.pnlManageSection.Controls.Add(this.btnUp);
            this.pnlManageSection.Controls.Add(this.lstForms);
            this.pnlManageSection.Controls.Add(this.btnDown);
            this.pnlManageSection.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlManageSection.Location = new System.Drawing.Point(-2, 0);
            this.pnlManageSection.Name = "pnlManageSection";
            this.pnlManageSection.Size = new System.Drawing.Size(279, 306);
            this.pnlManageSection.TabIndex = 7;
            this.pnlManageSection.Tag = "pnlPanel";
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
            this.cmbTechArea.Location = new System.Drawing.Point(88, 29);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(181, 21);
            this.cmbTechArea.TabIndex = 15;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectedIndexChanged += new System.EventHandler(this.cmbTechArea_SelectedIndexChanged);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(3, 29);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(79, 13);
            this.lblTechArea.TabIndex = 14;
            this.lblTechArea.Text = "Service";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(88, 271);
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
            this.btnUp.Location = new System.Drawing.Point(243, 110);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(26, 26);
            this.btnUp.TabIndex = 2;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lstForms
            // 
            this.lstForms.FormattingEnabled = true;
            this.lstForms.Location = new System.Drawing.Point(3, 73);
            this.lstForms.Name = "lstForms";
            this.lstForms.Size = new System.Drawing.Size(231, 186);
            this.lstForms.TabIndex = 0;
            this.lstForms.Tag = "lstListBox";
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(243, 177);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(26, 26);
            this.btnDown.TabIndex = 1;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // frmOrderForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 306);
            this.Controls.Add(this.pnlManageSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrderForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Manage Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmOrderForms_Load);
            this.pnlManageSection.ResumeLayout(false);
            this.pnlManageSection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlManageSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ListBox lstForms;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.ComboBox cmbTechArea;
    }
}