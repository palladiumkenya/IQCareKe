namespace IQCare.SCM
{
    partial class frmViewPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewPurchaseOrder));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgwPurchaseOrder = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPurchaseOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 60;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.Window;
            this.btnNew.Location = new System.Drawing.Point(692, 10);
            this.btnNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 25);
            this.btnNew.TabIndex = 44;
            this.btnNew.Tag = "btnSingleText";
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.Window;
            this.btnclose.Location = new System.Drawing.Point(771, 10);
            this.btnclose.Margin = new System.Windows.Forms.Padding(0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(80, 25);
            this.btnclose.TabIndex = 45;
            this.btnclose.Tag = "btnSingleText";
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // dgwPurchaseOrder
            // 
            this.dgwPurchaseOrder.AllowUserToDeleteRows = false;
            this.dgwPurchaseOrder.AllowUserToResizeColumns = false;
            this.dgwPurchaseOrder.AllowUserToResizeRows = false;
            this.dgwPurchaseOrder.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwPurchaseOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPurchaseOrder.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwPurchaseOrder.Location = new System.Drawing.Point(0, 2);
            this.dgwPurchaseOrder.Name = "dgwPurchaseOrder";
            this.dgwPurchaseOrder.Size = new System.Drawing.Size(852, 452);
            this.dgwPurchaseOrder.TabIndex = 61;
            this.dgwPurchaseOrder.Tag = "dgwDataGridView";
            this.dgwPurchaseOrder.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwPurchaseOrder_CellFormatting);
            this.dgwPurchaseOrder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwPurchaseOrder_CellClick);
            // 
            // frmViewPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.dgwPurchaseOrder);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewPurchaseOrder";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            
            this.Load += new System.EventHandler(this.frmViewPurchaseOrder_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPurchaseOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView dgwPurchaseOrder;

    }
}