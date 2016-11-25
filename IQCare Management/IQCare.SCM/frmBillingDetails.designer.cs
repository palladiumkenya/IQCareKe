namespace IQCare.SCM
{
    partial class frmBillingDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillingDetails));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbItemType = new System.Windows.Forms.GroupBox();
            this.lstItemType = new System.Windows.Forms.ListBox();
            this.ddlBillable = new System.Windows.Forms.ComboBox();
            this.lblBillable = new System.Windows.Forms.Label();
            this.lstAvailableItems = new System.Windows.Forms.ListBox();
            this.lstSelectedItems = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.grbDetails = new System.Windows.Forms.GroupBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grbItemType.SuspendLayout();
            this.grbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 67;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 44;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnClose.Location = new System.Drawing.Point(771, 10);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 45;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grbItemType);
            this.panel1.Controls.Add(this.ddlBillable);
            this.panel1.Controls.Add(this.lblBillable);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 138);
            this.panel1.TabIndex = 66;
            this.panel1.Tag = "pnlPanel";
            // 
            // grbItemType
            // 
            this.grbItemType.Controls.Add(this.lstItemType);
            this.grbItemType.Location = new System.Drawing.Point(508, 14);
            this.grbItemType.Name = "grbItemType";
            this.grbItemType.Size = new System.Drawing.Size(339, 119);
            this.grbItemType.TabIndex = 70;
            this.grbItemType.TabStop = false;
            this.grbItemType.Text = "ItemType";
            // 
            // lstItemType
            // 
            this.lstItemType.FormattingEnabled = true;
            this.lstItemType.Location = new System.Drawing.Point(6, 18);
            this.lstItemType.Name = "lstItemType";
            this.lstItemType.Size = new System.Drawing.Size(325, 95);
            this.lstItemType.TabIndex = 69;
            this.lstItemType.SelectedIndexChanged += new System.EventHandler(this.lstItemType_SelectedIndexChanged);
            // 
            // ddlBillable
            // 
            this.ddlBillable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBillable.FormattingEnabled = true;
            this.ddlBillable.Location = new System.Drawing.Point(129, 11);
            this.ddlBillable.Name = "ddlBillable";
            this.ddlBillable.Size = new System.Drawing.Size(225, 21);
            this.ddlBillable.TabIndex = 44;
            this.ddlBillable.Tag = "ddlDropDownList";
            this.ddlBillable.SelectedIndexChanged += new System.EventHandler(this.ddlBillable_SelectedIndexChanged);
            // 
            // lblBillable
            // 
            this.lblBillable.AutoSize = true;
            this.lblBillable.Location = new System.Drawing.Point(14, 14);
            this.lblBillable.Name = "lblBillable";
            this.lblBillable.Size = new System.Drawing.Size(43, 13);
            this.lblBillable.TabIndex = 43;
            this.lblBillable.Tag = "lblLabelRequired";
            this.lblBillable.Text = "Billable:";
            // 
            // lstAvailableItems
            // 
            this.lstAvailableItems.FormattingEnabled = true;
            this.lstAvailableItems.Location = new System.Drawing.Point(15, 19);
            this.lstAvailableItems.Name = "lstAvailableItems";
            this.lstAvailableItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvailableItems.Size = new System.Drawing.Size(340, 290);
            this.lstAvailableItems.TabIndex = 68;
            // 
            // lstSelectedItems
            // 
            this.lstSelectedItems.FormattingEnabled = true;
            this.lstSelectedItems.Location = new System.Drawing.Point(509, 20);
            this.lstSelectedItems.Name = "lstSelectedItems";
            this.lstSelectedItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectedItems.Size = new System.Drawing.Size(339, 290);
            this.lstSelectedItems.TabIndex = 69;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.SystemColors.Window;
            this.btnSelectAll.Font = new System.Drawing.Font("Snap ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.ForeColor = System.Drawing.Color.Green;
            this.btnSelectAll.Location = new System.Drawing.Point(384, 105);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 37);
            this.btnSelectAll.TabIndex = 71;
            this.btnSelectAll.Tag = "btnSingleText";
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.SystemColors.Window;
            this.btnUnselectAll.Font = new System.Drawing.Font("Snap ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnselectAll.ForeColor = System.Drawing.Color.Red;
            this.btnUnselectAll.Location = new System.Drawing.Point(384, 157);
            this.btnUnselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(80, 35);
            this.btnUnselectAll.TabIndex = 73;
            this.btnUnselectAll.Tag = "btnSingleText";
            this.btnUnselectAll.Text = "<<";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // grbDetails
            // 
            this.grbDetails.Controls.Add(this.btnUnselectAll);
            this.grbDetails.Controls.Add(this.btnSelectAll);
            this.grbDetails.Controls.Add(this.lstSelectedItems);
            this.grbDetails.Controls.Add(this.lstAvailableItems);
            this.grbDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbDetails.Location = new System.Drawing.Point(0, 144);
            this.grbDetails.Name = "grbDetails";
            this.grbDetails.Size = new System.Drawing.Size(848, 313);
            this.grbDetails.TabIndex = 74;
            this.grbDetails.TabStop = false;
            this.grbDetails.Text = "Details";
            // 
            // frmBillingDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.grbDetails);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBillingDetails";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Billables Details";
            this.Load += new System.EventHandler(this.frmBillingDetails_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grbItemType.ResumeLayout(false);
            this.grbDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ddlBillable;
        private System.Windows.Forms.Label lblBillable;
        private System.Windows.Forms.ListBox lstAvailableItems;
        private System.Windows.Forms.ListBox lstSelectedItems;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.GroupBox grbItemType;
        private System.Windows.Forms.ListBox lstItemType;
        private System.Windows.Forms.GroupBox grbDetails;
    }
}