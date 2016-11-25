namespace IQCare.SCM
{
    partial class frmConfigureBudget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigureBudget));
            this.dgwBudgetConfig = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ddlProgramYear = new System.Windows.Forms.ComboBox();
            this.ddlProgram = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlDonorPayer = new System.Windows.Forms.ComboBox();
            this.lbldonorPayer = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
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
            this.dgwBudgetConfig.Location = new System.Drawing.Point(0, 84);
            this.dgwBudgetConfig.Name = "dgwBudgetConfig";
            this.dgwBudgetConfig.Size = new System.Drawing.Size(852, 372);
            this.dgwBudgetConfig.TabIndex = 10;
            this.dgwBudgetConfig.Tag = "dgwDataGridView";
            this.dgwBudgetConfig.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwBudgetConfig_CellDoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(606, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabelRequired";
            this.lblStatus.Text = "Program Year:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(382, 38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 25);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Tag = "btnSingleText";
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ddlProgramYear
            // 
            this.ddlProgramYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProgramYear.FormattingEnabled = true;
            this.ddlProgramYear.Location = new System.Drawing.Point(686, 11);
            this.ddlProgramYear.Name = "ddlProgramYear";
            this.ddlProgramYear.Size = new System.Drawing.Size(152, 21);
            this.ddlProgramYear.TabIndex = 4;
            this.ddlProgramYear.Tag = "ddlDropDownList";
            // 
            // ddlProgram
            // 
            this.ddlProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProgram.FormattingEnabled = true;
            this.ddlProgram.Location = new System.Drawing.Point(361, 11);
            this.ddlProgram.Name = "ddlProgram";
            this.ddlProgram.Size = new System.Drawing.Size(168, 21);
            this.ddlProgram.TabIndex = 3;
            this.ddlProgram.Tag = "ddlDropDownList";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 43;
            this.label1.Tag = "lblLabelRequired";
            this.label1.Text = "Program:";
            // 
            // ddlDonorPayer
            // 
            this.ddlDonorPayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDonorPayer.FormattingEnabled = true;
            this.ddlDonorPayer.Location = new System.Drawing.Point(85, 11);
            this.ddlDonorPayer.Name = "ddlDonorPayer";
            this.ddlDonorPayer.Size = new System.Drawing.Size(168, 21);
            this.ddlDonorPayer.TabIndex = 2;
            this.ddlDonorPayer.Tag = "ddlDropDownList";
            this.ddlDonorPayer.SelectedIndexChanged += new System.EventHandler(this.ddlDonorPayer_SelectedIndexChanged);
            // 
            // lbldonorPayer
            // 
            this.lbldonorPayer.AutoSize = true;
            this.lbldonorPayer.Location = new System.Drawing.Point(8, 14);
            this.lbldonorPayer.Name = "lbldonorPayer";
            this.lbldonorPayer.Size = new System.Drawing.Size(71, 13);
            this.lbldonorPayer.TabIndex = 39;
            this.lbldonorPayer.Tag = "lblLabelRequired";
            this.lbldonorPayer.Text = "Donor/Payer:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCurrency);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.ddlProgramYear);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.ddlProgram);
            this.panel1.Controls.Add(this.lbldonorPayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlDonorPayer);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 78);
            this.panel1.TabIndex = 1;
            this.panel1.Tag = "pnlPanel";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(735, 50);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(59, 13);
            this.lblCurrency.TabIndex = 47;
            this.lblCurrency.Tag = "lblLabelRequired";
            this.lblCurrency.Text = "lblCurrency";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 20;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Window;
            this.btnDelete.Location = new System.Drawing.Point(686, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 25);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Tag = "btnSingleText";
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(766, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 25);
            this.btnClose.TabIndex = 22;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmConfigureBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgwBudgetConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigureBudget";
            this.Tag = "frmForm";
            this.Text = "Configure Budget";
            this.Load += new System.EventHandler(this.frmConfigureBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwBudgetConfig)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwBudgetConfig;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbldonorPayer;
        private System.Windows.Forms.ComboBox ddlDonorPayer;
        private System.Windows.Forms.ComboBox ddlProgramYear;
        private System.Windows.Forms.ComboBox ddlProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblCurrency;
    }
}