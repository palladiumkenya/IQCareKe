using System.Drawing;
using System;
using System.Windows.Forms;
namespace IQCare.FormBuilder
{
    partial class frmFormBuilderOld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormBuilderOld));
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.btnformbusinessrules = new System.Windows.Forms.Button();
            this.txtTabCaptionPlaceHolder = new System.Windows.Forms.TextBox();
            this.btnManageTab = new System.Windows.Forms.Button();
            this.btnAddTab = new System.Windows.Forms.Button();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddCustomField = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabFormBuilder = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rdoMultipleVisit = new System.Windows.Forms.RadioButton();
            this.rdoSingleVisit = new System.Windows.Forms.RadioButton();
            this.chkSignature = new System.Windows.Forms.CheckBox();
            this.pnlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPanel
            // 
            this.pnlPanel.AutoScroll = true;
            this.pnlPanel.Controls.Add(this.btnformbusinessrules);
            this.pnlPanel.Controls.Add(this.chkSignature);
            this.pnlPanel.Controls.Add(this.txtTabCaptionPlaceHolder);
            this.pnlPanel.Controls.Add(this.btnManageTab);
            this.pnlPanel.Controls.Add(this.btnAddTab);
            this.pnlPanel.Controls.Add(this.rdoMultipleVisit);
            this.pnlPanel.Controls.Add(this.rdoSingleVisit);
            this.pnlPanel.Controls.Add(this.cmbTechArea);
            this.pnlPanel.Controls.Add(this.lblTechArea);
            this.pnlPanel.Controls.Add(this.btnClose);
            this.pnlPanel.Controls.Add(this.btnAddCustomField);
            this.pnlPanel.Controls.Add(this.btnSave);
            this.pnlPanel.Controls.Add(this.txtFormName);
            this.pnlPanel.Controls.Add(this.label1);
            this.pnlPanel.Controls.Add(this.tabFormBuilder);
            this.pnlPanel.Location = new System.Drawing.Point(2, 1);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(1004, 574);
            this.pnlPanel.TabIndex = 0;
            this.pnlPanel.Tag = "pnlPanel";
            // 
            // btnformbusinessrules
            // 
            this.btnformbusinessrules.Location = new System.Drawing.Point(814, 11);
            this.btnformbusinessrules.Name = "btnformbusinessrules";
            this.btnformbusinessrules.Size = new System.Drawing.Size(163, 23);
            this.btnformbusinessrules.TabIndex = 16;
            this.btnformbusinessrules.Text = "Form Business Rules";
            this.btnformbusinessrules.UseVisualStyleBackColor = true;
            this.btnformbusinessrules.Click += new System.EventHandler(this.btnformbusinessrules_Click);
            // 
            // txtTabCaptionPlaceHolder
            // 
            this.txtTabCaptionPlaceHolder.Location = new System.Drawing.Point(629, 442);
            this.txtTabCaptionPlaceHolder.MaxLength = 50;
            this.txtTabCaptionPlaceHolder.Name = "txtTabCaptionPlaceHolder";
            this.txtTabCaptionPlaceHolder.Size = new System.Drawing.Size(226, 20);
            this.txtTabCaptionPlaceHolder.TabIndex = 15;
            this.txtTabCaptionPlaceHolder.Tag = "txtTextBox";
            this.txtTabCaptionPlaceHolder.Visible = false;
            this.txtTabCaptionPlaceHolder.Leave += new System.EventHandler(this.txtTabCaptionPlaceHolder_Leave);
            // 
            // btnManageTab
            // 
            this.btnManageTab.Location = new System.Drawing.Point(267, 540);
            this.btnManageTab.Name = "btnManageTab";
            this.btnManageTab.Size = new System.Drawing.Size(106, 20);
            this.btnManageTab.TabIndex = 14;
            this.btnManageTab.Tag = "btnH25WFlexi";
            this.btnManageTab.Text = "Manage &Tabs";
            this.btnManageTab.UseVisualStyleBackColor = true;
            this.btnManageTab.Click += new System.EventHandler(this.btnManageTab_Click);
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new System.Drawing.Point(158, 540);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(106, 20);
            this.btnAddTab.TabIndex = 13;
            this.btnAddTab.Tag = "btnH25WFlexi";
            this.btnAddTab.Text = "&Add Tab";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.FormattingEnabled = true;
            this.cmbTechArea.Location = new System.Drawing.Point(367, 3);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(274, 21);
            this.cmbTechArea.TabIndex = 9;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectionChangeCommitted += new System.EventHandler(this.cmbTechArea_SelectionChangeCommitted);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(312, 6);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(49, 13);
            this.lblTechArea.TabIndex = 8;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service :";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(485, 540);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 20);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddCustomField
            // 
            this.btnAddCustomField.Location = new System.Drawing.Point(594, 540);
            this.btnAddCustomField.Name = "btnAddCustomField";
            this.btnAddCustomField.Size = new System.Drawing.Size(106, 20);
            this.btnAddCustomField.TabIndex = 6;
            this.btnAddCustomField.Tag = "btnH25WFlexi";
            this.btnAddCustomField.Text = "Manage &Fields";
            this.btnAddCustomField.UseVisualStyleBackColor = true;
            this.btnAddCustomField.Click += new System.EventHandler(this.btnAddCustomField_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(376, 540);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 20);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(80, 3);
            this.txtFormName.MaxLength = 50;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(226, 20);
            this.txtFormName.TabIndex = 1;
            this.txtFormName.Tag = "txtTextBox";
            this.txtFormName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormName_KeyPress);
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
            // tabFormBuilder
            // 
            this.tabFormBuilder.Location = new System.Drawing.Point(4, 51);
            this.tabFormBuilder.Name = "tabFormBuilder";
            this.tabFormBuilder.SelectedIndex = 0;
            this.tabFormBuilder.Size = new System.Drawing.Size(986, 483);
            this.tabFormBuilder.TabIndex = 12;
            this.tabFormBuilder.Tag = "";
            this.tabFormBuilder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabFormBuilder_MouseDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 100);
            this.tabPage2.TabIndex = 0;
            // 
            // rdoMultipleVisit
            // 
            this.rdoMultipleVisit.AutoSize = true;
            this.rdoMultipleVisit.Location = new System.Drawing.Point(735, 2);
            this.rdoMultipleVisit.Name = "rdoMultipleVisit";
            this.rdoMultipleVisit.Size = new System.Drawing.Size(83, 17);
            this.rdoMultipleVisit.TabIndex = 11;
            this.rdoMultipleVisit.TabStop = true;
            this.rdoMultipleVisit.Tag = "rdoButton";
            this.rdoMultipleVisit.Text = "Multiple Visit";
            this.rdoMultipleVisit.UseVisualStyleBackColor = true;
            // 
            // rdoSingleVisit
            // 
            this.rdoSingleVisit.AutoSize = true;
            this.rdoSingleVisit.Location = new System.Drawing.Point(659, 2);
            this.rdoSingleVisit.Name = "rdoSingleVisit";
            this.rdoSingleVisit.Size = new System.Drawing.Size(76, 17);
            this.rdoSingleVisit.TabIndex = 10;
            this.rdoSingleVisit.TabStop = true;
            this.rdoSingleVisit.Tag = "rdoButton";
            this.rdoSingleVisit.Text = "Single Visit";
            this.rdoSingleVisit.UseVisualStyleBackColor = true;
            // 
            // chkSignature
            // 
            this.chkSignature.AutoSize = true;
            this.chkSignature.Location = new System.Drawing.Point(659, 28);
            this.chkSignature.Name = "chkSignature";
            this.chkSignature.Size = new System.Drawing.Size(135, 17);
            this.chkSignature.TabIndex = 16;
            this.chkSignature.Text = "Signature on each Tab";
            this.chkSignature.UseVisualStyleBackColor = true;
            // 
            // frmFormBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 576);
            this.Controls.Add(this.pnlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormBuilderOld";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "IQCare Form Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFormBuilder_FormClosing);
            this.Load += new System.EventHandler(this.frmFormBuilder_Load);
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Button btnAddCustomField;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.ComboBox cmbTechArea;
        private System.Windows.Forms.Button btnAddTab;
        private System.Windows.Forms.TabControl tabFormBuilder;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnManageTab;
        private System.Windows.Forms.TextBox txtTabCaptionPlaceHolder;
        private Button btnformbusinessrules;
        private Form theForm;
        private CheckBox chkSignature;
        private RadioButton rdoMultipleVisit;
        private RadioButton rdoSingleVisit;

    }
}