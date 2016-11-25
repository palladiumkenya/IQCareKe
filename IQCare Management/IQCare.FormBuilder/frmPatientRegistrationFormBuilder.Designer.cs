namespace IQCare.FormBuilder
{
    partial class frmPatientRegistrationFormBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientRegistrationFormBuilder));
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddCustomField = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnManageSection = new System.Windows.Forms.Button();
            this.btnAddSection = new System.Windows.Forms.Button();
            this.pnlMainPanel = new System.Windows.Forms.Panel();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPanel
            // 
            this.pnlPanel.Controls.Add(this.btnClose);
            this.pnlPanel.Controls.Add(this.btnAddCustomField);
            this.pnlPanel.Controls.Add(this.btnSave);
            this.pnlPanel.Controls.Add(this.btnManageSection);
            this.pnlPanel.Controls.Add(this.btnAddSection);
            this.pnlPanel.Controls.Add(this.pnlMainPanel);
            this.pnlPanel.Controls.Add(this.txtFormName);
            this.pnlPanel.Controls.Add(this.label1);
            this.pnlPanel.Location = new System.Drawing.Point(-1, 0);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(858, 501);
            this.pnlPanel.TabIndex = 1;
            this.pnlPanel.Tag = "pnlPanel";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(405, 473);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 21);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddCustomField
            // 
            this.btnAddCustomField.Location = new System.Drawing.Point(478, 445);
            this.btnAddCustomField.Name = "btnAddCustomField";
            this.btnAddCustomField.Size = new System.Drawing.Size(141, 22);
            this.btnAddCustomField.TabIndex = 6;
            this.btnAddCustomField.Tag = "btnH25WFlexi";
            this.btnAddCustomField.Text = "Manage &Fields";
            this.btnAddCustomField.UseVisualStyleBackColor = true;
            this.btnAddCustomField.Click += new System.EventHandler(this.btnAddCustomField_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(258, 473);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 21);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnManageSection
            // 
            this.btnManageSection.Location = new System.Drawing.Point(331, 446);
            this.btnManageSection.Name = "btnManageSection";
            this.btnManageSection.Size = new System.Drawing.Size(141, 21);
            this.btnManageSection.TabIndex = 4;
            this.btnManageSection.Tag = "btnH25WFlexi";
            this.btnManageSection.Text = "&Manage Section";
            this.btnManageSection.UseVisualStyleBackColor = true;
            this.btnManageSection.Click += new System.EventHandler(this.btnManageSection_Click);
            // 
            // btnAddSection
            // 
            this.btnAddSection.Location = new System.Drawing.Point(184, 446);
            this.btnAddSection.Name = "btnAddSection";
            this.btnAddSection.Size = new System.Drawing.Size(141, 21);
            this.btnAddSection.TabIndex = 3;
            this.btnAddSection.Tag = "btnH25WFlexi";
            this.btnAddSection.Text = "&Add Section";
            this.btnAddSection.UseVisualStyleBackColor = true;
            this.btnAddSection.Click += new System.EventHandler(this.btnAddSection_Click);
            // 
            // pnlMainPanel
            // 
            this.pnlMainPanel.AutoScroll = true;
            this.pnlMainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMainPanel.Location = new System.Drawing.Point(10, 29);
            this.pnlMainPanel.Name = "pnlMainPanel";
            this.pnlMainPanel.Size = new System.Drawing.Size(838, 402);
            this.pnlMainPanel.TabIndex = 2;
            this.pnlMainPanel.Tag = "pnlSubPanel1Css";
            // 
            // txtFormName
            // 
            this.txtFormName.Enabled = false;
            this.txtFormName.Location = new System.Drawing.Point(80, 3);
            this.txtFormName.MaxLength = 50;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(226, 20);
            this.txtFormName.TabIndex = 1;
            this.txtFormName.Tag = "txtTextBox";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Form Name :";
            // 
            // frmPatientRegistrationFormBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 501);
            this.Controls.Add(this.pnlPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientRegistrationFormBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "IQCare Patient Registration Form Builder";
            this.Load += new System.EventHandler(this.frmPatientRegistrationFormBuilder_Load);
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddCustomField;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnManageSection;
        private System.Windows.Forms.Button btnAddSection;
        private System.Windows.Forms.Panel pnlMainPanel;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Label label1;

    }
}