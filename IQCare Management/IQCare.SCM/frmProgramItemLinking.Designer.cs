namespace IQCare.SCM
{
    partial class frmProgramItemLinking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgramItemLinking));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.ddlItemSubType = new System.Windows.Forms.ComboBox();
            this.lblSubItem = new System.Windows.Forms.Label();
            this.ddlItemType = new System.Windows.Forms.ComboBox();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.lblItemType = new System.Windows.Forms.Label();
            this.ddlProgramName = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkItemList = new System.Windows.Forms.CheckedListBox();
            this.lblSelectItems = new System.Windows.Forms.Label();
            this.chkPrg = new System.Windows.Forms.CheckBox();
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
            this.panel1.Controls.Add(this.lblProgramName);
            this.panel1.Controls.Add(this.lblItemType);
            this.panel1.Controls.Add(this.ddlProgramName);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 71);
            this.panel1.TabIndex = 57;
            this.panel1.Tag = "pnlPanel";
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.SystemColors.Window;
            this.btnView.Location = new System.Drawing.Point(383, 39);
            this.btnView.Margin = new System.Windows.Forms.Padding(0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 25);
            this.btnView.TabIndex = 47;
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
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Location = new System.Drawing.Point(11, 16);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(80, 13);
            this.lblProgramName.TabIndex = 39;
            this.lblProgramName.Tag = "lblLabelRequired";
            this.lblProgramName.Text = "Program Name:";
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
            // ddlProgramName
            // 
            this.ddlProgramName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProgramName.FormattingEnabled = true;
            this.ddlProgramName.Location = new System.Drawing.Point(94, 14);
            this.ddlProgramName.Name = "ddlProgramName";
            this.ddlProgramName.Size = new System.Drawing.Size(168, 21);
            this.ddlProgramName.TabIndex = 42;
            this.ddlProgramName.Tag = "ddlDropDownListSCM";
            this.ddlProgramName.SelectedIndexChanged += new System.EventHandler(this.ddlProgramName_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Location = new System.Drawing.Point(0, 460);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 44);
            this.panel2.TabIndex = 60;
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
            // chkItemList
            // 
            this.chkItemList.CheckOnClick = true;
            this.chkItemList.FormattingEnabled = true;
            this.chkItemList.Location = new System.Drawing.Point(0, 93);
            this.chkItemList.Name = "chkItemList";
            this.chkItemList.Size = new System.Drawing.Size(852, 364);
            this.chkItemList.TabIndex = 61;
            // 
            // lblSelectItems
            // 
            this.lblSelectItems.AutoSize = true;
            this.lblSelectItems.Location = new System.Drawing.Point(17, 76);
            this.lblSelectItems.Name = "lblSelectItems";
            this.lblSelectItems.Size = new System.Drawing.Size(63, 13);
            this.lblSelectItems.TabIndex = 67;
            this.lblSelectItems.Tag = "lblLabelRequired";
            this.lblSelectItems.Text = "Select Item:";
            // 
            // chkPrg
            // 
            this.chkPrg.AutoSize = true;
            this.chkPrg.Location = new System.Drawing.Point(3, 75);
            this.chkPrg.Name = "chkPrg";
            this.chkPrg.Size = new System.Drawing.Size(15, 14);
            this.chkPrg.TabIndex = 68;
            this.chkPrg.UseVisualStyleBackColor = true;
            this.chkPrg.CheckedChanged += new System.EventHandler(this.chkPrg_CheckedChanged);
            // 
            // frmProgramItemLinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.chkPrg);
            this.Controls.Add(this.lblSelectItems);
            this.Controls.Add(this.chkItemList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProgramItemLinking";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Program-Item Linking";
            this.Load += new System.EventHandler(this.frmProgramItemLinking_Load);
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
        private System.Windows.Forms.Label lblProgramName;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.ComboBox ddlProgramName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckedListBox chkItemList;
        private System.Windows.Forms.Label lblSelectItems;
        private System.Windows.Forms.CheckBox chkPrg;
        private System.Windows.Forms.Button btnView;
    }
}