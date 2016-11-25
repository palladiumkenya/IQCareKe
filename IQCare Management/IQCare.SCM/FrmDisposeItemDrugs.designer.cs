namespace IQCare.SCM
{
    partial class frmDisposeItemDrugs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisposeItemDrugs));
            this.lblstore = new System.Windows.Forms.Label();
            this.dgwDisposeItem = new System.Windows.Forms.DataGridView();
            this.lblasofDate = new System.Windows.Forms.Label();
            this.dtpAsofDate = new System.Windows.Forms.DateTimePicker();
            this.ddlStore = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDisposeItem)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblstore
            // 
            this.lblstore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblstore.AutoSize = true;
            this.lblstore.Location = new System.Drawing.Point(33, 12);
            this.lblstore.Name = "lblstore";
            this.lblstore.Size = new System.Drawing.Size(35, 13);
            this.lblstore.TabIndex = 61;
            this.lblstore.Tag = "lblLabelRequired";
            this.lblstore.Text = "Store:";
            // 
            // dgwDisposeItem
            // 
            this.dgwDisposeItem.AllowUserToAddRows = false;
            this.dgwDisposeItem.AllowUserToDeleteRows = false;
            this.dgwDisposeItem.AllowUserToResizeColumns = false;
            this.dgwDisposeItem.AllowUserToResizeRows = false;
            this.dgwDisposeItem.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwDisposeItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDisposeItem.Location = new System.Drawing.Point(0, 77);
            this.dgwDisposeItem.Name = "dgwDisposeItem";
            this.dgwDisposeItem.Size = new System.Drawing.Size(852, 379);
            this.dgwDisposeItem.TabIndex = 5;
            this.dgwDisposeItem.Tag = "dgwDataGridView";
            // 
            // lblasofDate
            // 
            this.lblasofDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblasofDate.AutoSize = true;
            this.lblasofDate.Location = new System.Drawing.Point(499, 12);
            this.lblasofDate.Name = "lblasofDate";
            this.lblasofDate.Size = new System.Drawing.Size(62, 13);
            this.lblasofDate.TabIndex = 60;
            this.lblasofDate.Tag = "lblLabel";
            this.lblasofDate.Text = "As Of Date:";
            // 
            // dtpAsofDate
            // 
            this.dtpAsofDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpAsofDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAsofDate.Location = new System.Drawing.Point(567, 8);
            this.dtpAsofDate.Name = "dtpAsofDate";
            this.dtpAsofDate.Size = new System.Drawing.Size(261, 20);
            this.dtpAsofDate.TabIndex = 2;
            this.dtpAsofDate.Tag = "txtTextBoxSCM";
            // 
            // ddlStore
            // 
            this.ddlStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStore.FormattingEnabled = true;
            this.ddlStore.Location = new System.Drawing.Point(72, 8);
            this.ddlStore.Name = "ddlStore";
            this.ddlStore.Size = new System.Drawing.Size(338, 21);
            this.ddlStore.TabIndex = 1;
            this.ddlStore.Tag = "ddlDropDownListSCM";
            this.ddlStore.SelectionChangeCommitted += new System.EventHandler(this.ddlStore_SelectionChangeCommitted);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 10;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 11;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(771, 10);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 25);
            this.button4.TabIndex = 12;
            this.button4.Tag = "btnSingleText";
            this.button4.Text = "&Close";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Location = new System.Drawing.Point(407, 46);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(80, 25);
            this.btn_Submit.TabIndex = 3;
            this.btn_Submit.Tag = "btnSingleText";
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = false;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // frmDisposeItemDrugs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblstore);
            this.Controls.Add(this.dgwDisposeItem);
            this.Controls.Add(this.lblasofDate);
            this.Controls.Add(this.dtpAsofDate);
            this.Controls.Add(this.ddlStore);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDisposeItemDrugs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Dispose Items";
            this.Load += new System.EventHandler(this.FrmDisposeItemDrugs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwDisposeItem)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblstore;
        private System.Windows.Forms.DataGridView dgwDisposeItem;
        private System.Windows.Forms.Label lblasofDate;
        private System.Windows.Forms.DateTimePicker dtpAsofDate;
        private System.Windows.Forms.ComboBox ddlStore;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_Submit;
    }
}