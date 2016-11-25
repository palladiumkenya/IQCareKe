namespace IQCare.SCM
{
    partial class frmStoreUserLinking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStoreUserLinking));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStore = new System.Windows.Forms.Label();
            this.ddlStore = new System.Windows.Forms.ComboBox();
            this.chkItemList = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStore);
            this.groupBox1.Controls.Add(this.ddlStore);
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 62);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(178, 29);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(35, 13);
            this.lblStore.TabIndex = 35;
            this.lblStore.Tag = "lblLabelRequired";
            this.lblStore.Text = "Store:";
            // 
            // ddlStore
            // 
            this.ddlStore.BackColor = System.Drawing.Color.White;
            this.ddlStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ddlStore.Location = new System.Drawing.Point(216, 25);
            this.ddlStore.Name = "ddlStore";
            this.ddlStore.Size = new System.Drawing.Size(389, 21);
            this.ddlStore.TabIndex = 36;
            this.ddlStore.Tag = "ddlDropDownList";
            this.ddlStore.SelectedIndexChanged += new System.EventHandler(this.ddlStore_SelectedIndexChanged);
            // 
            // chkItemList
            // 
            this.chkItemList.CheckOnClick = true;
            this.chkItemList.FormattingEnabled = true;
            this.chkItemList.Location = new System.Drawing.Point(5, 75);
            this.chkItemList.Name = "chkItemList";
            this.chkItemList.Size = new System.Drawing.Size(841, 364);
            this.chkItemList.Sorted = true;
            this.chkItemList.TabIndex = 66;
            this.chkItemList.SelectedIndexChanged += new System.EventHandler(this.chkItemList_SelectedIndexChanged);
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
            // frmStoreUserLinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chkItemList);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStoreUserLinking";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Store User Linking";
            this.Load += new System.EventHandler(this.frmStoreUserLinking_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.ComboBox ddlStore;
        private System.Windows.Forms.CheckedListBox chkItemList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}