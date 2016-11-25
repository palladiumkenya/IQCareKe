namespace IQCare.SCM
{
    partial class frmConfigureBudgetDetail
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
            this.dgwBudgetConfig = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbldonorPayer = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtProgramYear = new System.Windows.Forms.TextBox();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.txtDonor = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblLabelRequired = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgwBudgetConfig)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwBudgetConfig
            // 
            this.dgwBudgetConfig.AllowUserToAddRows = false;
            this.dgwBudgetConfig.AllowUserToDeleteRows = false;
            this.dgwBudgetConfig.AllowUserToResizeColumns = false;
            this.dgwBudgetConfig.AllowUserToResizeRows = false;
            this.dgwBudgetConfig.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwBudgetConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwBudgetConfig.Location = new System.Drawing.Point(0, 78);
            this.dgwBudgetConfig.Name = "dgwBudgetConfig";
            this.dgwBudgetConfig.Size = new System.Drawing.Size(852, 376);
            this.dgwBudgetConfig.TabIndex = 53;
            this.dgwBudgetConfig.Tag = "dgwDataGridView";
            this.dgwBudgetConfig.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwBudgetConfig_RowEnter);
            this.dgwBudgetConfig.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgwBudgetConfig_CellValidating);
            this.dgwBudgetConfig.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwBudgetConfig_CellEndEdit);
            this.dgwBudgetConfig.GotFocus += new System.EventHandler(this.dgwBudgetConfig_GotFocus);
            this.dgwBudgetConfig.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwBudgetConfig_CellContentClick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(538, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabel";
            this.lblStatus.Text = "Program Year:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 43;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Program:";
            // 
            // lbldonorPayer
            // 
            this.lbldonorPayer.AutoSize = true;
            this.lbldonorPayer.Location = new System.Drawing.Point(25, 12);
            this.lbldonorPayer.Name = "lbldonorPayer";
            this.lbldonorPayer.Size = new System.Drawing.Size(71, 13);
            this.lbldonorPayer.TabIndex = 39;
            this.lbldonorPayer.Tag = "lblLabel";
            this.lbldonorPayer.Text = "Donor/Payer:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCurrency);
            this.panel1.Controls.Add(this.txtProgramYear);
            this.panel1.Controls.Add(this.txtProgram);
            this.panel1.Controls.Add(this.txtDonor);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lbldonorPayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 78);
            this.panel1.TabIndex = 56;
            this.panel1.Tag = "pnlPanel";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(751, 49);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(59, 13);
            this.lblCurrency.TabIndex = 50;
            this.lblCurrency.Tag = "lblLabelRequired";
            this.lblCurrency.Text = "lblCurrency";
            // 
            // txtProgramYear
            // 
            this.txtProgramYear.Enabled = false;
            this.txtProgramYear.Location = new System.Drawing.Point(630, 12);
            this.txtProgramYear.Name = "txtProgramYear";
            this.txtProgramYear.Size = new System.Drawing.Size(138, 20);
            this.txtProgramYear.TabIndex = 49;
            // 
            // txtProgram
            // 
            this.txtProgram.Enabled = false;
            this.txtProgram.Location = new System.Drawing.Point(346, 12);
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(157, 20);
            this.txtProgram.TabIndex = 48;
            // 
            // txtDonor
            // 
            this.txtDonor.Enabled = false;
            this.txtDonor.Location = new System.Drawing.Point(102, 12);
            this.txtDonor.Name = "txtDonor";
            this.txtDonor.Size = new System.Drawing.Size(141, 20);
            this.txtDonor.TabIndex = 47;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(-122, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(974, 47);
            this.panel2.TabIndex = 59;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(806, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 25);
            this.btnSave.TabIndex = 47;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(888, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 25);
            this.btnClose.TabIndex = 45;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblLabelRequired
            // 
            this.lblLabelRequired.AutoSize = true;
            this.lblLabelRequired.Location = new System.Drawing.Point(628, 381);
            this.lblLabelRequired.Name = "lblLabelRequired";
            this.lblLabelRequired.Size = new System.Drawing.Size(69, 13);
            this.lblLabelRequired.TabIndex = 60;
            this.lblLabelRequired.Tag = "lblLabelRequired";
            this.lblLabelRequired.Text = "lblNoFunding";
            // 
            // frmConfigureBudgetDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.lblLabelRequired);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgwBudgetConfig);
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigureBudgetDetail";
            this.Tag = "frmForm";
            this.Text = "Configure Budget";
            this.Load += new System.EventHandler(this.frmConfigureBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwBudgetConfig)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwBudgetConfig;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbldonorPayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtProgramYear;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.TextBox txtDonor;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblLabelRequired;
    }
}