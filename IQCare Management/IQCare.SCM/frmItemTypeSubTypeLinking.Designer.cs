namespace IQCare.SCM
{
    partial class frmItemTypeSubTypeLinking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemTypeSubTypeLinking));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ddlItemType = new System.Windows.Forms.ComboBox();
            this.lblItemType = new System.Windows.Forms.Label();
            this.chkSubItemTypeList = new System.Windows.Forms.CheckedListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSubitemType = new System.Windows.Forms.Label();
            this.chktype = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ddlItemType);
            this.panel1.Controls.Add(this.lblItemType);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 55);
            this.panel1.TabIndex = 58;
            this.panel1.Tag = "pnlPanel";
            // 
            // ddlItemType
            // 
            this.ddlItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItemType.FormattingEnabled = true;
            this.ddlItemType.Location = new System.Drawing.Point(267, 12);
            this.ddlItemType.Name = "ddlItemType";
            this.ddlItemType.Size = new System.Drawing.Size(381, 21);
            this.ddlItemType.TabIndex = 44;
            this.ddlItemType.Tag = "ddlDropDownList";
            this.ddlItemType.SelectedIndexChanged += new System.EventHandler(this.ddlItemType_SelectedIndexChanged);
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(190, 15);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(57, 13);
            this.lblItemType.TabIndex = 43;
            this.lblItemType.Tag = "lblLabelRequired";
            this.lblItemType.Text = "Item Type:";
            // 
            // chkSubItemTypeList
            // 
            this.chkSubItemTypeList.CheckOnClick = true;
            this.chkSubItemTypeList.FormattingEnabled = true;
            this.chkSubItemTypeList.Location = new System.Drawing.Point(2, 80);
            this.chkSubItemTypeList.Name = "chkSubItemTypeList";
            this.chkSubItemTypeList.Size = new System.Drawing.Size(850, 364);
            this.chkSubItemTypeList.TabIndex = 63;
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
            this.panel2.TabIndex = 62;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // lblSubitemType
            // 
            this.lblSubitemType.AutoSize = true;
            this.lblSubitemType.Location = new System.Drawing.Point(21, 62);
            this.lblSubitemType.Name = "lblSubitemType";
            this.lblSubitemType.Size = new System.Drawing.Size(112, 13);
            this.lblSubitemType.TabIndex = 64;
            this.lblSubitemType.Tag = "lblLabelRequired";
            this.lblSubitemType.Text = "Select Sub Item Type:";
            // 
            // chktype
            // 
            this.chktype.AutoSize = true;
            this.chktype.Location = new System.Drawing.Point(6, 62);
            this.chktype.Name = "chktype";
            this.chktype.Size = new System.Drawing.Size(15, 14);
            this.chktype.TabIndex = 65;
            this.chktype.UseVisualStyleBackColor = true;
            this.chktype.CheckedChanged += new System.EventHandler(this.chktype_CheckedChanged);
            // 
            // frmItemTypeSubTypeLinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.chktype);
            this.Controls.Add(this.lblSubitemType);
            this.Controls.Add(this.chkSubItemTypeList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmItemTypeSubTypeLinking";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "ItemType Sub-Type Linking";
            this.Load += new System.EventHandler(this.frmItemType_SubTypeLinking_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ddlItemType;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.CheckedListBox chkSubItemTypeList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSubitemType;
        private System.Windows.Forms.CheckBox chktype;
    }
}