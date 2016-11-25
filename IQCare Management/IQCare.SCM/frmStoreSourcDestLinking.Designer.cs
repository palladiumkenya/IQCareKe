namespace IQCare.SCM
{
    partial class frmStoreSourcDestLinking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStoreSourcDestLinking));
            this.dgwStoreName = new System.Windows.Forms.DataGridView();
            this.ColSourceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDestinationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbDestinationStore = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbSourceStore = new System.Windows.Forms.ComboBox();
            this.lblsource = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStoreName)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwStoreName
            // 
            this.dgwStoreName.AllowUserToAddRows = false;
            this.dgwStoreName.AllowUserToDeleteRows = false;
            this.dgwStoreName.AllowUserToResizeColumns = false;
            this.dgwStoreName.AllowUserToResizeRows = false;
            this.dgwStoreName.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwStoreName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwStoreName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSourceID,
            this.ColSourceName,
            this.ColDestId,
            this.ColDestinationName});
            this.dgwStoreName.Location = new System.Drawing.Point(12, 148);
            this.dgwStoreName.Name = "dgwStoreName";
            this.dgwStoreName.Size = new System.Drawing.Size(828, 305);
            this.dgwStoreName.TabIndex = 75;
            this.dgwStoreName.Tag = "dgwDataGridView";
            this.dgwStoreName.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwStoreName_CellClick);
            // 
            // ColSourceID
            // 
            this.ColSourceID.HeaderText = "SourceID";
            this.ColSourceID.Name = "ColSourceID";
            this.ColSourceID.Visible = false;
            this.ColSourceID.Width = 50;
            // 
            // ColSourceName
            // 
            this.ColSourceName.HeaderText = "Source Store";
            this.ColSourceName.Name = "ColSourceName";
            this.ColSourceName.ReadOnly = true;
            this.ColSourceName.Width = 400;
            // 
            // ColDestId
            // 
            this.ColDestId.HeaderText = "DestinID";
            this.ColDestId.Name = "ColDestId";
            this.ColDestId.ReadOnly = true;
            this.ColDestId.Visible = false;
            this.ColDestId.Width = 50;
            // 
            // ColDestinationName
            // 
            this.ColDestinationName.HeaderText = "Destination Store";
            this.ColDestinationName.Name = "ColDestinationName";
            this.ColDestinationName.ReadOnly = true;
            this.ColDestinationName.Width = 400;
            // 
            // cmbDestinationStore
            // 
            this.cmbDestinationStore.BackColor = System.Drawing.Color.White;
            this.cmbDestinationStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestinationStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmbDestinationStore.Location = new System.Drawing.Point(107, 24);
            this.cmbDestinationStore.Name = "cmbDestinationStore";
            this.cmbDestinationStore.Size = new System.Drawing.Size(280, 21);
            this.cmbDestinationStore.TabIndex = 38;
            this.cmbDestinationStore.Tag = "ddlDropDownList";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 28);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(91, 13);
            this.lblStatus.TabIndex = 37;
            this.lblStatus.Tag = "lblLabelRequired";
            this.lblStatus.Text = "Destination Store:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(12, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbSourceStore);
            this.splitContainer1.Panel1.Controls.Add(this.lblsource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblStatus);
            this.splitContainer1.Panel2.Controls.Add(this.cmbDestinationStore);
            this.splitContainer1.Size = new System.Drawing.Size(828, 76);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 76;
            // 
            // cmbSourceStore
            // 
            this.cmbSourceStore.BackColor = System.Drawing.Color.White;
            this.cmbSourceStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceStore.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmbSourceStore.Location = new System.Drawing.Point(91, 24);
            this.cmbSourceStore.Name = "cmbSourceStore";
            this.cmbSourceStore.Size = new System.Drawing.Size(302, 21);
            this.cmbSourceStore.TabIndex = 39;
            this.cmbSourceStore.Tag = "ddlDropDownList";
            this.cmbSourceStore.SelectionChangeCommitted += new System.EventHandler(this.cmbSourceStore_SelectionChangeCommitted);
            // 
            // lblsource
            // 
            this.lblsource.AutoSize = true;
            this.lblsource.Location = new System.Drawing.Point(16, 28);
            this.lblsource.Name = "lblsource";
            this.lblsource.Size = new System.Drawing.Size(72, 13);
            this.lblsource.TabIndex = 38;
            this.lblsource.Tag = "lblLabelRequired";
            this.lblsource.Text = "Source Store:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(392, 117);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(60, 25);
            this.btnSubmit.TabIndex = 77;
            this.btnSubmit.Tag = "btnSingleTextSCM";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
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
            this.panel2.TabIndex = 78;
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
            // frmStoreSourcDestLinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dgwStoreName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStoreSourcDestLinking";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Source Destination Store Linking";
            this.Load += new System.EventHandler(this.frmStoreSourcDestLinking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwStoreName)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwStoreName;
        private System.Windows.Forms.ComboBox cmbDestinationStore;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblsource;
        private System.Windows.Forms.ComboBox cmbSourceStore;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSourceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDestId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDestinationName;
    }
}