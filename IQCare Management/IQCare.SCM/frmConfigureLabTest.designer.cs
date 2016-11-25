namespace IQCare.SCM
{
    partial class frmConfigureLabTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigureLabTest));
            this.lblLionccode = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtlionccode = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this.cmbTestLocation = new System.Windows.Forms.ComboBox();
            this.lblTestLocation = new System.Windows.Forms.Label();
            this.lblItemIdentifiers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtsellingprice = new System.Windows.Forms.TextBox();
            this.lblSelling = new System.Windows.Forms.Label();
            this.txtMarginpect = new System.Windows.Forms.TextBox();
            this.lblMargin = new System.Windows.Forms.Label();
            this.txtOutSrcLoc = new System.Windows.Forms.TextBox();
            this.lblOutSrcLocation = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btsave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLionccode
            // 
            this.lblLionccode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLionccode.AutoSize = true;
            this.lblLionccode.Location = new System.Drawing.Point(35, 19);
            this.lblLionccode.Name = "lblLionccode";
            this.lblLionccode.Size = new System.Drawing.Size(76, 13);
            this.lblLionccode.TabIndex = 50;
            this.lblLionccode.Tag = "lblLabel";
            this.lblLionccode.Text = "LOINC Code:  ";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Enabled = false;
            this.txtDisplayName.Location = new System.Drawing.Point(140, 16);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(262, 20);
            this.txtDisplayName.TabIndex = 51;
            this.txtDisplayName.Tag = "txtTextBox";
            // 
            // txtlionccode
            // 
            this.txtlionccode.Location = new System.Drawing.Point(120, 16);
            this.txtlionccode.Name = "txtlionccode";
            this.txtlionccode.Size = new System.Drawing.Size(262, 20);
            this.txtlionccode.TabIndex = 1;
            this.txtlionccode.Tag = "txtTextBox";
            this.txtlionccode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlionccode_KeyPress);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(13, 19);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(111, 13);
            this.lblDisplayName.TabIndex = 49;
            this.lblDisplayName.Tag = "lblLabel";
            this.lblDisplayName.Text = "IQCare Display Name:";
            // 
            // dtpEffectiveDate
            // 
            this.dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveDate.Location = new System.Drawing.Point(123, 45);
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.Size = new System.Drawing.Size(262, 20);
            this.dtpEffectiveDate.TabIndex = 5;
            this.dtpEffectiveDate.Tag = "txtTextBoxSCM";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 24;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Effective Date:";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(123, 14);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(262, 20);
            this.txtCost.TabIndex = 3;
            this.txtCost.Tag = "txtTextBox";
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            this.txtCost.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCost_KeyUp);
            // 
            // lblCost
            // 
            this.lblCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(74, 17);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(31, 13);
            this.lblCost.TabIndex = 20;
            this.lblCost.Tag = "lblLabelRequired";
            this.lblCost.Text = "Cost:";
            // 
            // cmbTestLocation
            // 
            this.cmbTestLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTestLocation.Location = new System.Drawing.Point(142, 14);
            this.cmbTestLocation.Name = "cmbTestLocation";
            this.cmbTestLocation.Size = new System.Drawing.Size(262, 21);
            this.cmbTestLocation.TabIndex = 2;
            this.cmbTestLocation.Tag = "ddlDropDownList";
            this.cmbTestLocation.SelectionChangeCommitted += new System.EventHandler(this.cmbTestLocation_SelectionChangeCommitted);
            // 
            // lblTestLocation
            // 
            this.lblTestLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTestLocation.AutoSize = true;
            this.lblTestLocation.Location = new System.Drawing.Point(49, 17);
            this.lblTestLocation.Name = "lblTestLocation";
            this.lblTestLocation.Size = new System.Drawing.Size(75, 13);
            this.lblTestLocation.TabIndex = 18;
            this.lblTestLocation.Tag = "lblLabelRequired";
            this.lblTestLocation.Text = "Test Location:";
            // 
            // lblItemIdentifiers
            // 
            this.lblItemIdentifiers.AutoSize = true;
            this.lblItemIdentifiers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemIdentifiers.Location = new System.Drawing.Point(0, 2);
            this.lblItemIdentifiers.Name = "lblItemIdentifiers";
            this.lblItemIdentifiers.Size = new System.Drawing.Size(91, 13);
            this.lblItemIdentifiers.TabIndex = 51;
            this.lblItemIdentifiers.Tag = "";
            this.lblItemIdentifiers.Text = "Item Identifiers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 52;
            this.label1.Tag = "";
            this.label1.Text = "Cost Information";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 18);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblDisplayName);
            this.splitContainer1.Panel1.Controls.Add(this.txtDisplayName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblLionccode);
            this.splitContainer1.Panel2.Controls.Add(this.txtlionccode);
            this.splitContainer1.Size = new System.Drawing.Size(852, 58);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 55;
            this.splitContainer1.Tag = "pnlSubPanel";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(0, 107);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtsellingprice);
            this.splitContainer2.Panel1.Controls.Add(this.lblSelling);
            this.splitContainer2.Panel1.Controls.Add(this.txtMarginpect);
            this.splitContainer2.Panel1.Controls.Add(this.lblMargin);
            this.splitContainer2.Panel1.Controls.Add(this.lblTestLocation);
            this.splitContainer2.Panel1.Controls.Add(this.cmbTestLocation);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtOutSrcLoc);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.lblOutSrcLocation);
            this.splitContainer2.Panel2.Controls.Add(this.txtCost);
            this.splitContainer2.Panel2.Controls.Add(this.lblCost);
            this.splitContainer2.Panel2.Controls.Add(this.dtpEffectiveDate);
            this.splitContainer2.Size = new System.Drawing.Size(852, 119);
            this.splitContainer2.SplitterDistance = 435;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 56;
            this.splitContainer2.Tag = "pnlSubPanel";
            // 
            // txtsellingprice
            // 
            this.txtsellingprice.Enabled = false;
            this.txtsellingprice.Location = new System.Drawing.Point(142, 81);
            this.txtsellingprice.Name = "txtsellingprice";
            this.txtsellingprice.Size = new System.Drawing.Size(262, 20);
            this.txtsellingprice.TabIndex = 30;
            this.txtsellingprice.TabStop = false;
            this.txtsellingprice.Tag = "txtTextBox";
            this.txtsellingprice.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblSelling
            // 
            this.lblSelling.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSelling.AutoSize = true;
            this.lblSelling.Location = new System.Drawing.Point(56, 83);
            this.lblSelling.Name = "lblSelling";
            this.lblSelling.Size = new System.Drawing.Size(68, 13);
            this.lblSelling.TabIndex = 31;
            this.lblSelling.Tag = "lblLabel";
            this.lblSelling.Text = "Selling Price:";
            // 
            // txtMarginpect
            // 
            this.txtMarginpect.Location = new System.Drawing.Point(142, 48);
            this.txtMarginpect.Name = "txtMarginpect";
            this.txtMarginpect.Size = new System.Drawing.Size(262, 20);
            this.txtMarginpect.TabIndex = 4;
            this.txtMarginpect.Tag = "txtTextBox";
            this.txtMarginpect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMarginpect_KeyPress);
            this.txtMarginpect.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMarginpect_KeyUp);
            // 
            // lblMargin
            // 
            this.lblMargin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(71, 49);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(53, 13);
            this.lblMargin.TabIndex = 29;
            this.lblMargin.Tag = "lblLabelRequired";
            this.lblMargin.Text = "Margin %:";
            // 
            // txtOutSrcLoc
            // 
            this.txtOutSrcLoc.Location = new System.Drawing.Point(123, 81);
            this.txtOutSrcLoc.Name = "txtOutSrcLoc";
            this.txtOutSrcLoc.Size = new System.Drawing.Size(262, 20);
            this.txtOutSrcLoc.TabIndex = 6;
            this.txtOutSrcLoc.Tag = "txtTextBox";
            // 
            // lblOutSrcLocation
            // 
            this.lblOutSrcLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOutSrcLocation.AutoSize = true;
            this.lblOutSrcLocation.Location = new System.Drawing.Point(2, 83);
            this.lblOutSrcLocation.Name = "lblOutSrcLocation";
            this.lblOutSrcLocation.Size = new System.Drawing.Size(103, 13);
            this.lblOutSrcLocation.TabIndex = 34;
            this.lblOutSrcLocation.Tag = "lblLabel";
            this.lblOutSrcLocation.Text = "Outsource Location:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Window;
            this.button3.Location = new System.Drawing.Point(733, 22);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 25);
            this.button3.TabIndex = 44;
            this.button3.Tag = "btnSingleText";
            this.button3.Text = "&Save";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(791, 22);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 25);
            this.button4.TabIndex = 45;
            this.button4.Tag = "btnSingleText";
            this.button4.Text = "&Close";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btsave);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 59;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btsave
            // 
            this.btsave.BackColor = System.Drawing.SystemColors.Window;
            this.btsave.Location = new System.Drawing.Point(692, 10);
            this.btsave.Margin = new System.Windows.Forms.Padding(0);
            this.btsave.Name = "btsave";
            this.btsave.Size = new System.Drawing.Size(80, 25);
            this.btsave.TabIndex = 8;
            this.btsave.Tag = "btnSingleText";
            this.btsave.Text = "&Save";
            this.btsave.UseVisualStyleBackColor = false;
            this.btsave.Click += new System.EventHandler(this.btsave_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.Location = new System.Drawing.Point(771, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 25);
            this.button2.TabIndex = 9;
            this.button2.Tag = "btnSingleText";
            this.button2.Text = "&Close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(291, 254);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 35;
            this.lblStatus.Tag = "lblLabel";
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "In-Active"});
            this.cmbStatus.Location = new System.Drawing.Point(343, 250);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(262, 21);
            this.cmbStatus.TabIndex = 57;
            this.cmbStatus.Tag = "ddlDropDownList";
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // frmConfigureLabTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblItemIdentifiers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigureLabTest";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Configure Lab Test";
            this.Load += new System.EventHandler(this.frmConfigureLabTest_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLionccode;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtlionccode;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.ComboBox cmbTestLocation;
        private System.Windows.Forms.Label lblTestLocation;
        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblItemIdentifiers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btsave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtMarginpect;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.TextBox txtsellingprice;
        private System.Windows.Forms.Label lblSelling;
        private System.Windows.Forms.TextBox txtOutSrcLoc;
        private System.Windows.Forms.Label lblOutSrcLocation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
    }
}