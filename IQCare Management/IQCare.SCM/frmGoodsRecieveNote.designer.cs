namespace IQCare.SCM
{
    partial class frmGoodsRecieveNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoodsRecieveNote));
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.txtFreight = new System.Windows.Forms.TextBox();
            this.lblFreight = new System.Windows.Forms.Label();
            this.ddlDestinationStore = new System.Windows.Forms.ComboBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDestinationstore = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.ddlSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.dgwPOItems = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.lstSearchBatch = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.brnClose = new System.Windows.Forms.Button();
            this.dgwGRNItems = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPOItems)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwGRNItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtTax);
            this.panel3.Controls.Add(this.lblTax);
            this.panel3.Controls.Add(this.txtFreight);
            this.panel3.Controls.Add(this.lblFreight);
            this.panel3.Controls.Add(this.ddlDestinationStore);
            this.panel3.Controls.Add(this.lblOrderDate);
            this.panel3.Controls.Add(this.lblDestinationstore);
            this.panel3.Controls.Add(this.dtpOrderDate);
            this.panel3.Controls.Add(this.txtOrderNumber);
            this.panel3.Controls.Add(this.lblOrder);
            this.panel3.Controls.Add(this.ddlSupplier);
            this.panel3.Controls.Add(this.lblSupplier);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(852, 92);
            this.panel3.TabIndex = 68;
            this.panel3.Tag = "pnlSubPanelSCM";
            // 
            // txtTax
            // 
            this.txtTax.Location = new System.Drawing.Point(550, 63);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(168, 20);
            this.txtTax.TabIndex = 8;
            this.txtTax.Tag = "txtOrderNumber";
            this.txtTax.Visible = false;
            // 
            // lblTax
            // 
            this.lblTax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTax.AutoSize = true;
            this.lblTax.Location = new System.Drawing.Point(516, 67);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(28, 13);
            this.lblTax.TabIndex = 51;
            this.lblTax.Tag = "lblLabel";
            this.lblTax.Text = "Tax:";
            this.lblTax.Visible = false;
            // 
            // txtFreight
            // 
            this.txtFreight.Location = new System.Drawing.Point(179, 63);
            this.txtFreight.Name = "txtFreight";
            this.txtFreight.Size = new System.Drawing.Size(168, 20);
            this.txtFreight.TabIndex = 50;
            this.txtFreight.Tag = "txtOrderNumber";
            this.txtFreight.Visible = false;
            // 
            // lblFreight
            // 
            this.lblFreight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFreight.AutoSize = true;
            this.lblFreight.Location = new System.Drawing.Point(131, 67);
            this.lblFreight.Name = "lblFreight";
            this.lblFreight.Size = new System.Drawing.Size(42, 13);
            this.lblFreight.TabIndex = 49;
            this.lblFreight.Tag = "lblLabel";
            this.lblFreight.Text = "Freight:";
            this.lblFreight.Visible = false;
            // 
            // ddlDestinationStore
            // 
            this.ddlDestinationStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDestinationStore.FormattingEnabled = true;
            this.ddlDestinationStore.Items.AddRange(new object[] {
            "--Select--",
            "Active",
            "Passive"});
            this.ddlDestinationStore.Location = new System.Drawing.Point(546, 35);
            this.ddlDestinationStore.Name = "ddlDestinationStore";
            this.ddlDestinationStore.Size = new System.Drawing.Size(168, 21);
            this.ddlDestinationStore.TabIndex = 7;
            this.ddlDestinationStore.Tag = "ddlDropDownList";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(478, 11);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(62, 13);
            this.lblOrderDate.TabIndex = 44;
            this.lblOrderDate.Tag = "lblLabel";
            this.lblOrderDate.Text = "Order Date:";
            // 
            // lblDestinationstore
            // 
            this.lblDestinationstore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDestinationstore.AutoSize = true;
            this.lblDestinationstore.Location = new System.Drawing.Point(449, 38);
            this.lblDestinationstore.Name = "lblDestinationstore";
            this.lblDestinationstore.Size = new System.Drawing.Size(91, 13);
            this.lblDestinationstore.TabIndex = 42;
            this.lblDestinationstore.Tag = "lblLabel";
            this.lblDestinationstore.Text = "Destination Store:";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderDate.Location = new System.Drawing.Point(546, 8);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(168, 20);
            this.dtpOrderDate.TabIndex = 4;
            this.dtpOrderDate.Tag = "txtTextBoxSCM";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Location = new System.Drawing.Point(177, 8);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(168, 20);
            this.txtOrderNumber.TabIndex = 3;
            this.txtOrderNumber.Tag = "txtOrderNumber";
            // 
            // lblOrder
            // 
            this.lblOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrder.AutoSize = true;
            this.lblOrder.Location = new System.Drawing.Point(98, 11);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(76, 13);
            this.lblOrder.TabIndex = 39;
            this.lblOrder.Tag = "lblLabel";
            this.lblOrder.Text = "Order Number:";
            // 
            // ddlSupplier
            // 
            this.ddlSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSupplier.FormattingEnabled = true;
            this.ddlSupplier.Location = new System.Drawing.Point(177, 35);
            this.ddlSupplier.Name = "ddlSupplier";
            this.ddlSupplier.Size = new System.Drawing.Size(168, 21);
            this.ddlSupplier.TabIndex = 5;
            this.ddlSupplier.Tag = "ddlDropDownList";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(125, 38);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(48, 13);
            this.lblSupplier.TabIndex = 37;
            this.lblSupplier.Tag = "lblLabel";
            this.lblSupplier.Text = "Supplier:";
            // 
            // dgwPOItems
            // 
            this.dgwPOItems.AllowUserToDeleteRows = false;
            this.dgwPOItems.AllowUserToResizeColumns = false;
            this.dgwPOItems.AllowUserToResizeRows = false;
            this.dgwPOItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwPOItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPOItems.Location = new System.Drawing.Point(0, 98);
            this.dgwPOItems.Name = "dgwPOItems";
            this.dgwPOItems.Size = new System.Drawing.Size(852, 187);
            this.dgwPOItems.TabIndex = 20;
            this.dgwPOItems.Tag = "dgwDataGridView";
            this.dgwPOItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwPOItems_CellFormatting);
            this.dgwPOItems.Click += new System.EventHandler(this.dgwPOItems_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnprint);
            this.panel2.Controls.Add(this.lstSearchBatch);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.brnClose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 40;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.SystemColors.Window;
            this.btnprint.Location = new System.Drawing.Point(691, 10);
            this.btnprint.Margin = new System.Windows.Forms.Padding(0);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(80, 25);
            this.btnprint.TabIndex = 45;
            this.btnprint.Tag = "btnSingleText";
            this.btnprint.Text = "&Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // lstSearchBatch
            // 
            this.lstSearchBatch.FormattingEnabled = true;
            this.lstSearchBatch.Location = new System.Drawing.Point(342, 10);
            this.lstSearchBatch.Name = "lstSearchBatch";
            this.lstSearchBatch.Size = new System.Drawing.Size(34, 17);
            this.lstSearchBatch.TabIndex = 107;
            this.lstSearchBatch.Tag = "lstListBox";
            this.lstSearchBatch.Visible = false;
            this.lstSearchBatch.DoubleClick += new System.EventHandler(this.lstSearchBatch_DoubleClick);
            this.lstSearchBatch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSearchBatch_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(611, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 44;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // brnClose
            // 
            this.brnClose.BackColor = System.Drawing.SystemColors.Window;
            this.brnClose.Location = new System.Drawing.Point(771, 10);
            this.brnClose.Margin = new System.Windows.Forms.Padding(0);
            this.brnClose.Name = "brnClose";
            this.brnClose.Size = new System.Drawing.Size(80, 25);
            this.brnClose.TabIndex = 46;
            this.brnClose.Tag = "btnSingleText";
            this.brnClose.Text = "&Close";
            this.brnClose.UseVisualStyleBackColor = false;
            this.brnClose.Click += new System.EventHandler(this.brnClose_Click);
            // 
            // dgwGRNItems
            // 
            this.dgwGRNItems.AllowUserToDeleteRows = false;
            this.dgwGRNItems.AllowUserToResizeColumns = false;
            this.dgwGRNItems.AllowUserToResizeRows = false;
            this.dgwGRNItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwGRNItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwGRNItems.Location = new System.Drawing.Point(0, 291);
            this.dgwGRNItems.Name = "dgwGRNItems";
            this.dgwGRNItems.Size = new System.Drawing.Size(852, 163);
            this.dgwGRNItems.TabIndex = 30;
            this.dgwGRNItems.Tag = "dgwDataGridView";
            this.dgwGRNItems.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgwGRNItems_CellBeginEdit);
            this.dgwGRNItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwGRNItems_CellEndEdit);
            this.dgwGRNItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwGRNItems_CellFormatting);
            this.dgwGRNItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwGRNItems_CellValueChanged);
            this.dgwGRNItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwGRNItems_EditingControlShowing);
            // 
            // frmGoodsRecieveNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.dgwGRNItems);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgwPOItems);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGoodsRecieveNote";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Goods Recieve Note";
            this.Load += new System.EventHandler(this.frmGoodsRecieveNote_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPOItems)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwGRNItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox ddlDestinationStore;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDestinationstore;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.ComboBox ddlSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.DataGridView dgwPOItems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button brnClose;
        private System.Windows.Forms.TextBox txtFreight;
        private System.Windows.Forms.Label lblFreight;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.DataGridView dgwGRNItems;
        private System.Windows.Forms.ListBox lstSearchBatch;
        private System.Windows.Forms.Button btnprint;
    }
}