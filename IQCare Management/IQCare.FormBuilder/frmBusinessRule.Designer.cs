namespace IQCare.FormBuilder
{
    partial class frmBusinessRule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusinessRule));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkLstBox = new System.Windows.Forms.CheckedListBox();
            this.pnlBusinessRule = new System.Windows.Forms.Panel();
            this.pnlNumeric = new System.Windows.Forms.Panel();
            this.txtNumeric = new System.Windows.Forms.TextBox();
            this.lblNumeric = new System.Windows.Forms.Label();
            this.pnlDate2 = new System.Windows.Forms.Panel();
            this.txtDate2 = new System.Windows.Forms.TextBox();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.pnlDate1 = new System.Windows.Forms.Panel();
            this.txtDate1 = new System.Windows.Forms.TextBox();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.txtMinAgeRange = new System.Windows.Forms.TextBox();
            this.txtMaxAgeRange = new System.Windows.Forms.TextBox();
            this.ddlRegimenType = new System.Windows.Forms.ComboBox();
            this.txtMaxVal = new System.Windows.Forms.TextBox();
            this.txtMinVal = new System.Windows.Forms.TextBox();
            this.txtMaxNormalVal = new System.Windows.Forms.TextBox();
            this.txtMinNormalVal = new System.Windows.Forms.TextBox();
            this.pnlBusinessRule.SuspendLayout();
            this.pnlNumeric.SuspendLayout();
            this.pnlDate2.SuspendLayout();
            this.pnlDate1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(97, 198);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(188, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Tag = "btnSingleText";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkLstBox
            // 
            this.chkLstBox.CheckOnClick = true;
            this.chkLstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstBox.FormattingEnabled = true;
            this.chkLstBox.Location = new System.Drawing.Point(0, 0);
            this.chkLstBox.Name = "chkLstBox";
            this.chkLstBox.Size = new System.Drawing.Size(371, 191);
            this.chkLstBox.TabIndex = 8;
            this.chkLstBox.Tag = "";
            this.chkLstBox.ThreeDCheckBoxes = true;
            this.chkLstBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedList_ItemCheck);
            // 
            // pnlBusinessRule
            // 
            this.pnlBusinessRule.Controls.Add(this.txtMinNormalVal);
            this.pnlBusinessRule.Controls.Add(this.txtMaxNormalVal);
            this.pnlBusinessRule.Controls.Add(this.pnlNumeric);
            this.pnlBusinessRule.Controls.Add(this.pnlDate2);
            this.pnlBusinessRule.Controls.Add(this.pnlDate1);
            this.pnlBusinessRule.Controls.Add(this.txtMinAgeRange);
            this.pnlBusinessRule.Controls.Add(this.txtMaxAgeRange);
            this.pnlBusinessRule.Controls.Add(this.ddlRegimenType);
            this.pnlBusinessRule.Controls.Add(this.txtMaxVal);
            this.pnlBusinessRule.Controls.Add(this.txtMinVal);
            this.pnlBusinessRule.Controls.Add(this.chkLstBox);
            this.pnlBusinessRule.Controls.Add(this.btnCancel);
            this.pnlBusinessRule.Controls.Add(this.btnSubmit);
            this.pnlBusinessRule.Location = new System.Drawing.Point(0, 0);
            this.pnlBusinessRule.Name = "pnlBusinessRule";
            this.pnlBusinessRule.Size = new System.Drawing.Size(374, 236);
            this.pnlBusinessRule.TabIndex = 9;
            this.pnlBusinessRule.Tag = "pnlPanel";
            // 
            // pnlNumeric
            // 
            this.pnlNumeric.Controls.Add(this.txtNumeric);
            this.pnlNumeric.Controls.Add(this.lblNumeric);
            this.pnlNumeric.Location = new System.Drawing.Point(129, 152);
            this.pnlNumeric.Name = "pnlNumeric";
            this.pnlNumeric.Size = new System.Drawing.Size(170, 26);
            this.pnlNumeric.TabIndex = 16;
            this.pnlNumeric.Visible = false;
            // 
            // txtNumeric
            // 
            this.txtNumeric.Location = new System.Drawing.Point(75, 3);
            this.txtNumeric.Name = "txtNumeric";
            this.txtNumeric.Size = new System.Drawing.Size(90, 20);
            this.txtNumeric.TabIndex = 1;
            // 
            // lblNumeric
            // 
            this.lblNumeric.AutoSize = true;
            this.lblNumeric.Location = new System.Drawing.Point(2, 7);
            this.lblNumeric.Name = "lblNumeric";
            this.lblNumeric.Size = new System.Drawing.Size(75, 13);
            this.lblNumeric.TabIndex = 0;
            this.lblNumeric.Text = "Numeric Label";
            // 
            // pnlDate2
            // 
            this.pnlDate2.Controls.Add(this.txtDate2);
            this.pnlDate2.Controls.Add(this.lblDate2);
            this.pnlDate2.Location = new System.Drawing.Point(129, 123);
            this.pnlDate2.Name = "pnlDate2";
            this.pnlDate2.Size = new System.Drawing.Size(170, 25);
            this.pnlDate2.TabIndex = 15;
            this.pnlDate2.Visible = false;
            // 
            // txtDate2
            // 
            this.txtDate2.Location = new System.Drawing.Point(74, 2);
            this.txtDate2.Name = "txtDate2";
            this.txtDate2.Size = new System.Drawing.Size(90, 20);
            this.txtDate2.TabIndex = 1;
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(1, 6);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(68, 13);
            this.lblDate2.TabIndex = 0;
            this.lblDate2.Text = "Date 2 Label";
            // 
            // pnlDate1
            // 
            this.pnlDate1.Controls.Add(this.txtDate1);
            this.pnlDate1.Controls.Add(this.lblDate1);
            this.pnlDate1.Location = new System.Drawing.Point(129, 95);
            this.pnlDate1.Name = "pnlDate1";
            this.pnlDate1.Size = new System.Drawing.Size(170, 25);
            this.pnlDate1.TabIndex = 14;
            this.pnlDate1.Visible = false;
            // 
            // txtDate1
            // 
            this.txtDate1.Location = new System.Drawing.Point(74, 2);
            this.txtDate1.Name = "txtDate1";
            this.txtDate1.Size = new System.Drawing.Size(90, 20);
            this.txtDate1.TabIndex = 1;
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(1, 6);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(68, 13);
            this.lblDate1.TabIndex = 0;
            this.lblDate1.Text = "Date 1 Label";
            // 
            // txtMinAgeRange
            // 
            this.txtMinAgeRange.Location = new System.Drawing.Point(137, 0);
            this.txtMinAgeRange.Name = "txtMinAgeRange";
            this.txtMinAgeRange.Size = new System.Drawing.Size(35, 20);
            this.txtMinAgeRange.TabIndex = 13;
            this.txtMinAgeRange.Tag = "txtTextBox";
            this.txtMinAgeRange.Visible = false;
            this.txtMinAgeRange.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinAgeRange_Validating);
            // 
            // txtMaxAgeRange
            // 
            this.txtMaxAgeRange.Location = new System.Drawing.Point(205, 0);
            this.txtMaxAgeRange.Name = "txtMaxAgeRange";
            this.txtMaxAgeRange.Size = new System.Drawing.Size(37, 20);
            this.txtMaxAgeRange.TabIndex = 12;
            this.txtMaxAgeRange.Tag = "txtTextBox";
            this.txtMaxAgeRange.Visible = false;
            this.txtMaxAgeRange.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxAgeRange_Validating);
            // 
            // ddlRegimenType
            // 
            this.ddlRegimenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRegimenType.FormattingEnabled = true;
            this.ddlRegimenType.Location = new System.Drawing.Point(132, 12);
            this.ddlRegimenType.Name = "ddlRegimenType";
            this.ddlRegimenType.Size = new System.Drawing.Size(145, 21);
            this.ddlRegimenType.TabIndex = 11;
            this.ddlRegimenType.Visible = false;
            // 
            // txtMaxVal
            // 
            this.txtMaxVal.Location = new System.Drawing.Point(132, 49);
            this.txtMaxVal.Name = "txtMaxVal";
            this.txtMaxVal.Size = new System.Drawing.Size(84, 20);
            this.txtMaxVal.TabIndex = 10;
            this.txtMaxVal.Tag = "txtTextBox";
            this.txtMaxVal.Visible = false;
            this.txtMaxVal.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxVal_Validating);
            // 
            // txtMinVal
            // 
            this.txtMinVal.Location = new System.Drawing.Point(133, 72);
            this.txtMinVal.Name = "txtMinVal";
            this.txtMinVal.Size = new System.Drawing.Size(84, 20);
            this.txtMinVal.TabIndex = 9;
            this.txtMinVal.Tag = "txtTextBox";
            this.txtMinVal.Visible = false;
            this.txtMinVal.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinVal_Validating);
            // 
            // txtMaxNormalVal
            // 
            this.txtMaxNormalVal.Location = new System.Drawing.Point(132, 39);
            this.txtMaxNormalVal.Name = "txtMaxNormalVal";
            this.txtMaxNormalVal.Size = new System.Drawing.Size(84, 20);
            this.txtMaxNormalVal.TabIndex = 17;
            this.txtMaxNormalVal.Tag = "txtTextBox";
            this.txtMaxNormalVal.Visible = false;
            this.txtMaxNormalVal.Validating += new System.ComponentModel.CancelEventHandler(this.txtMaxNormalVal_Validating);
            // 
            // txtMinNormalVal
            // 
            this.txtMinNormalVal.Location = new System.Drawing.Point(134, 84);
            this.txtMinNormalVal.Name = "txtMinNormalVal";
            this.txtMinNormalVal.Size = new System.Drawing.Size(84, 20);
            this.txtMinNormalVal.TabIndex = 18;
            this.txtMinNormalVal.Tag = "txtTextBox";
            this.txtMinNormalVal.Visible = false;
            this.txtMinNormalVal.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinNormalVal_Validating);
            // 
            // frmBusinessRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(371, 230);
            this.Controls.Add(this.pnlBusinessRule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBusinessRule";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Business Rule";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBusinessRule_FormClosed);
            this.Load += new System.EventHandler(this.frmBusinessRule_Load);
            this.pnlBusinessRule.ResumeLayout(false);
            this.pnlBusinessRule.PerformLayout();
            this.pnlNumeric.ResumeLayout(false);
            this.pnlNumeric.PerformLayout();
            this.pnlDate2.ResumeLayout(false);
            this.pnlDate2.PerformLayout();
            this.pnlDate1.ResumeLayout(false);
            this.pnlDate1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox chkLstBox;
        private System.Windows.Forms.Panel pnlBusinessRule;
        private System.Windows.Forms.TextBox txtMaxVal;
        private System.Windows.Forms.TextBox txtMinVal;
        private System.Windows.Forms.ComboBox ddlRegimenType;
        private System.Windows.Forms.TextBox txtMinAgeRange;
        private System.Windows.Forms.TextBox txtMaxAgeRange;
        private System.Windows.Forms.Panel pnlNumeric;
        private System.Windows.Forms.TextBox txtNumeric;
        private System.Windows.Forms.Label lblNumeric;
        private System.Windows.Forms.Panel pnlDate2;
        private System.Windows.Forms.TextBox txtDate2;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Panel pnlDate1;
        private System.Windows.Forms.TextBox txtDate1;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.TextBox txtMinNormalVal;
        private System.Windows.Forms.TextBox txtMaxNormalVal;

    }
}
