namespace IQCare.SCM
{
    partial class frmSupplierItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupplierItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.ddlItemSubType = new System.Windows.Forms.ComboBox();
            this.lblSubItem = new System.Windows.Forms.Label();
            this.ddlItemType = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblItemType = new System.Windows.Forms.Label();
            this.ddlSupplierName = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkItemList = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSelectItems = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.ddlItemSubType);
            this.panel1.Controls.Add(this.lblSubItem);
            this.panel1.Controls.Add(this.ddlItemType);
            this.panel1.Controls.Add(this.lblSupplier);
            this.panel1.Controls.Add(this.lblItemType);
            this.panel1.Controls.Add(this.ddlSupplierName);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 72);
            this.panel1.TabIndex = 63;
            this.panel1.Tag = "pnlPanel";
           
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.SystemColors.Window;
            this.btnView.Location = new System.Drawing.Point(383, 41);
            this.btnView.Margin = new System.Windows.Forms.Padding(0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 25);
            this.btnView.TabIndex = 46;
            this.btnView.Tag = "btnSingleText";
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ddlItemSubType
            // 
            this.ddlItemSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItemSubType.FormattingEnabled = true;
            this.ddlItemSubType.Location = new System.Drawing.Point(686, 14);
            this.ddlItemSubType.Name = "ddlItemSubType";
            this.ddlItemSubType.Size = new System.Drawing.Size(152, 21);
            this.ddlItemSubType.TabIndex = 45;
            this.ddlItemSubType.Tag = "ddlDropDownListSCM";
            this.ddlItemSubType.SelectionChangeCommitted += new System.EventHandler(this.ddlItemSubType_SelectionChangeCommitted);
            this.ddlItemSubType.SelectedIndexChanged += new System.EventHandler(this.ddlItemSubType_SelectedIndexChanged);
            // 
            // lblSubItem
            // 
            this.lblSubItem.AutoSize = true;
            this.lblSubItem.Location = new System.Drawing.Point(605, 16);
            this.lblSubItem.Name = "lblSubItem";
            this.lblSubItem.Size = new System.Drawing.Size(79, 13);
            this.lblSubItem.TabIndex = 35;
            this.lblSubItem.Tag = "lblLabel";
            this.lblSubItem.Text = "Item Sub Type:";
            // 
            // ddlItemType
            // 
            this.ddlItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItemType.FormattingEnabled = true;
            this.ddlItemType.Location = new System.Drawing.Point(383, 14);
            this.ddlItemType.Name = "ddlItemType";
            this.ddlItemType.Size = new System.Drawing.Size(168, 21);
            this.ddlItemType.TabIndex = 44;
            this.ddlItemType.Tag = "ddlDropDownList";
            this.ddlItemType.SelectionChangeCommitted += new System.EventHandler(this.ddlItemType_SelectionChangeCommitted);
            this.ddlItemType.SelectedIndexChanged += new System.EventHandler(this.ddlItemType_SelectedIndexChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(11, 16);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(79, 13);
            this.lblSupplier.TabIndex = 39;
            this.lblSupplier.Tag = "lblLabelRequired";
            this.lblSupplier.Text = "Supplier Name:";
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(323, 16);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(57, 13);
            this.lblItemType.TabIndex = 43;
            this.lblItemType.Tag = "lblLabelRequired";
            this.lblItemType.Text = "Item Type:";
            // 
            // ddlSupplierName
            // 
            this.ddlSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSupplierName.FormattingEnabled = true;
            this.ddlSupplierName.Location = new System.Drawing.Point(94, 14);
            this.ddlSupplierName.Name = "ddlSupplierName";
            this.ddlSupplierName.Size = new System.Drawing.Size(168, 21);
            this.ddlSupplierName.TabIndex = 42;
            this.ddlSupplierName.Tag = "ddlDropDownListSCM";
            this.ddlSupplierName.SelectionChangeCommitted += new System.EventHandler(this.ddlSupplierName_SelectionChangeCommitted);
            this.ddlSupplierName.SelectedIndexChanged += new System.EventHandler(this.ddlSupplierName_SelectedIndexChanged);
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
            // chkItemList
            // 
            this.chkItemList.CheckOnClick = true;
            this.chkItemList.FormattingEnabled = true;
            this.chkItemList.Location = new System.Drawing.Point(0, 93);
            this.chkItemList.Name = "chkItemList";
            this.chkItemList.Size = new System.Drawing.Size(852, 364);
            this.chkItemList.TabIndex = 65;
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
            this.panel2.TabIndex = 64;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // lblSelectItems
            // 
            this.lblSelectItems.AutoSize = true;
            this.lblSelectItems.Location = new System.Drawing.Point(26, 77);
            this.lblSelectItems.Name = "lblSelectItems";
            this.lblSelectItems.Size = new System.Drawing.Size(51, 13);
            this.lblSelectItems.TabIndex = 66;
            this.lblSelectItems.Tag = "lblLabelRequired";
            this.lblSelectItems.Text = "Select All";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(5, 78);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 67;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // frmSupplierItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.lblSelectItems);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkItemList);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSupplierItem";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Supplier-Item Linking";
            this.Load += new System.EventHandler(this.frmSupplierItem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ddlItemSubType;
        private System.Windows.Forms.Label lblSubItem;
        private System.Windows.Forms.ComboBox ddlItemType;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.ComboBox ddlSupplierName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckedListBox chkItemList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSelectItems;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnView;
    }
}