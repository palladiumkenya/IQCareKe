namespace IQCare.SCM
{
    partial class frmSetUserStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetUserStore));
            this.lblUser = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblStore = new System.Windows.Forms.Label();
            this.ddlStoreName = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(21, 9);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(63, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Tag = "lblLabel";
            this.lblUser.Text = "User Name:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(96, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(35, 13);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Tag = "lblLabel";
            this.lblUserName.Text = "label1";
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(18, 47);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(122, 13);
            this.lblStore.TabIndex = 2;
            this.lblStore.Text = "Destination Store Name:";
            // 
            // ddlStoreName
            // 
            this.ddlStoreName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStoreName.FormattingEnabled = true;
            this.ddlStoreName.Items.AddRange(new object[] {
            "--Select--",
            "Active",
            "Passive"});
            this.ddlStoreName.Location = new System.Drawing.Point(150, 44);
            this.ddlStoreName.Name = "ddlStoreName";
            this.ddlStoreName.Size = new System.Drawing.Size(197, 21);
            this.ddlStoreName.TabIndex = 44;
            this.ddlStoreName.Tag = "ddlDropDownList";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(71, 81);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 45;
            this.btnOK.Tag = "btnSingleText";
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(150, 81);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 46;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSetUserStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(461, 121);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ddlStoreName);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetUserStore";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Set User Store";
            this.Load += new System.EventHandler(this.frmSetUserStore_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.ComboBox ddlStoreName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
    }
}