namespace IQCare.FormBuilder
{
    partial class frmModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgwModuleDetails = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwModuleDetails)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgwModuleDetails);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1059, 438);
            this.panel1.TabIndex = 3;
            // 
            // dgwModuleDetails
            // 
            this.dgwModuleDetails.AllowUserToAddRows = false;
            this.dgwModuleDetails.AllowUserToDeleteRows = false;
            this.dgwModuleDetails.AllowUserToResizeColumns = false;
            this.dgwModuleDetails.AllowUserToResizeRows = false;
            this.dgwModuleDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwModuleDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgwModuleDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwModuleDetails.Location = new System.Drawing.Point(3, 3);
            this.dgwModuleDetails.Name = "dgwModuleDetails";
            this.dgwModuleDetails.Size = new System.Drawing.Size(1053, 432);
            this.dgwModuleDetails.TabIndex = 0;
            this.dgwModuleDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwModuleDetails_CellClick);
            this.dgwModuleDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwModuleDetails_CellContentClick);
            this.dgwModuleDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwModuleDetails_CellDoubleClick);
            this.dgwModuleDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwModuleDetails_CellFormatting);
            this.dgwModuleDetails.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgwModuleDetails_CellPainting);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnCreate);
            this.panel2.Location = new System.Drawing.Point(3, 446);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 62);
            this.panel2.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(349, 26);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(141, 21);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "btnH25WFlexi";
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(506, 26);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 21);
            this.btnClose.TabIndex = 1;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(191, 26);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(141, 21);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Tag = "btnH25WFlexi";
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 511);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Manage Service";
            this.Load += new System.EventHandler(this.frmModule_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwModuleDetails)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgwModuleDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
    }
}

     