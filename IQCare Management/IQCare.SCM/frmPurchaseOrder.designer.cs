namespace IQCare.SCM
{
    partial class frmPurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseOrder));
            this.ddlAuthorisedBy = new System.Windows.Forms.ComboBox();
            this.lblAuthorizedBy = new System.Windows.Forms.Label();
            this.ddlPreparedBy = new System.Windows.Forms.ComboBox();
            this.lblPreparedBy = new System.Windows.Forms.Label();
            this.dgwItemSubitemDetails = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btndelete = new System.Windows.Forms.Button();
            this.chkRejectedStatus = new System.Windows.Forms.CheckBox();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ddlDestinationStore = new System.Windows.Forms.ComboBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDestinationstore = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.ddlSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwItemSubitemDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddlAuthorisedBy
            // 
            this.ddlAuthorisedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuthorisedBy.FormattingEnabled = true;
            this.ddlAuthorisedBy.Location = new System.Drawing.Point(373, 14);
            this.ddlAuthorisedBy.Name = "ddlAuthorisedBy";
            this.ddlAuthorisedBy.Size = new System.Drawing.Size(168, 21);
            this.ddlAuthorisedBy.TabIndex = 22;
            this.ddlAuthorisedBy.Tag = "ddlDropDownList";
            // 
            // lblAuthorizedBy
            // 
            this.lblAuthorizedBy.AutoSize = true;
            this.lblAuthorizedBy.Location = new System.Drawing.Point(294, 18);
            this.lblAuthorizedBy.Name = "lblAuthorizedBy";
            this.lblAuthorizedBy.Size = new System.Drawing.Size(75, 13);
            this.lblAuthorizedBy.TabIndex = 43;
            this.lblAuthorizedBy.Tag = "lblLabel";
            this.lblAuthorizedBy.Text = "Authorised By:";
            // 
            // ddlPreparedBy
            // 
            this.ddlPreparedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPreparedBy.FormattingEnabled = true;
            this.ddlPreparedBy.Location = new System.Drawing.Point(89, 13);
            this.ddlPreparedBy.Name = "ddlPreparedBy";
            this.ddlPreparedBy.Size = new System.Drawing.Size(168, 21);
            this.ddlPreparedBy.TabIndex = 21;
            this.ddlPreparedBy.Tag = "ddlDropDownList";
            // 
            // lblPreparedBy
            // 
            this.lblPreparedBy.AutoSize = true;
            this.lblPreparedBy.Location = new System.Drawing.Point(15, 16);
            this.lblPreparedBy.Name = "lblPreparedBy";
            this.lblPreparedBy.Size = new System.Drawing.Size(68, 13);
            this.lblPreparedBy.TabIndex = 39;
            this.lblPreparedBy.Tag = "lblLabelRequired";
            this.lblPreparedBy.Text = "Prepared By:";
            // 
            // dgwItemSubitemDetails
            // 
            this.dgwItemSubitemDetails.AllowUserToResizeColumns = false;
            this.dgwItemSubitemDetails.AllowUserToResizeRows = false;
            this.dgwItemSubitemDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwItemSubitemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwItemSubitemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Itemcode,
            this.Units,
            this.UnitQty,
            this.orderQuantity,
            this.PriceUnit,
            this.TotalPrice});
            this.dgwItemSubitemDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwItemSubitemDetails.Location = new System.Drawing.Point(0, 107);
            this.dgwItemSubitemDetails.Name = "dgwItemSubitemDetails";
            this.dgwItemSubitemDetails.Size = new System.Drawing.Size(852, 273);
            this.dgwItemSubitemDetails.TabIndex = 10;
            this.dgwItemSubitemDetails.Tag = "dgwDataGridView";
            this.dgwItemSubitemDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwItemSubitemDetails_CellEndEdit);
            this.dgwItemSubitemDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwItemSubitemDetails_CellFormatting);
            this.dgwItemSubitemDetails.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwItemSubitemDetails_CellLeave);
            this.dgwItemSubitemDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwItemSubitemDetails_CellValueChanged);
            this.dgwItemSubitemDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwItemSubitemDetails_DataError);
            this.dgwItemSubitemDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwItemSubitemDetails_EditingControlShowing);
            this.dgwItemSubitemDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwItemSubitemDetails_KeyDown);
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.Width = 200;
            // 
            // Itemcode
            // 
            this.Itemcode.HeaderText = "Item Code";
            this.Itemcode.Name = "Itemcode";
            this.Itemcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Itemcode.Width = 200;
            // 
            // Units
            // 
            this.Units.HeaderText = "Purchase Units";
            this.Units.Name = "Units";
            this.Units.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitQty
            // 
            this.UnitQty.HeaderText = "Unit Quantity";
            this.UnitQty.Name = "UnitQty";
            // 
            // orderQuantity
            // 
            this.orderQuantity.HeaderText = "Order Quantity";
            this.orderQuantity.Name = "orderQuantity";
            this.orderQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PriceUnit
            // 
            this.PriceUnit.HeaderText = "Price/Unit";
            this.PriceUnit.Name = "PriceUnit";
            this.PriceUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PriceUnit.Width = 83;
            // 
            // TotalPrice
            // 
            this.TotalPrice.HeaderText = "Total Price";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(664, 383);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 57;
            this.lblTotal.Tag = "lblLabel";
            this.lblTotal.Text = "Total:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Btndelete);
            this.panel1.Controls.Add(this.chkRejectedStatus);
            this.panel1.Controls.Add(this.ddlAuthorisedBy);
            this.panel1.Controls.Add(this.lblPreparedBy);
            this.panel1.Controls.Add(this.lblAuthorizedBy);
            this.panel1.Controls.Add(this.ddlPreparedBy);
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 57);
            this.panel1.TabIndex = 20;
            this.panel1.Tag = "lblPanelSCM";
            // 
            // Btndelete
            // 
            this.Btndelete.BackColor = System.Drawing.SystemColors.Window;
            this.Btndelete.Location = new System.Drawing.Point(605, 13);
            this.Btndelete.Margin = new System.Windows.Forms.Padding(0);
            this.Btndelete.Name = "Btndelete";
            this.Btndelete.Size = new System.Drawing.Size(80, 25);
            this.Btndelete.TabIndex = 23;
            this.Btndelete.Tag = "btnSingleText";
            this.Btndelete.Text = "Delete Item";
            this.Btndelete.UseVisualStyleBackColor = false;
            this.Btndelete.Click += new System.EventHandler(this.Btndelete_Click);
            // 
            // chkRejectedStatus
            // 
            this.chkRejectedStatus.AutoSize = true;
            this.chkRejectedStatus.Location = new System.Drawing.Point(742, 16);
            this.chkRejectedStatus.Name = "chkRejectedStatus";
            this.chkRejectedStatus.Size = new System.Drawing.Size(98, 17);
            this.chkRejectedStatus.TabIndex = 24;
            this.chkRejectedStatus.Tag = "chkCheckbox";
            this.chkRejectedStatus.Text = "Order Rejected";
            this.chkRejectedStatus.UseVisualStyleBackColor = true;
            // 
            // lstSearch
            // 
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.Location = new System.Drawing.Point(17, 10);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(29, 17);
            this.lstSearch.TabIndex = 61;
            this.lstSearch.Tag = "lstListBox";
            this.lstSearch.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ddlDestinationStore);
            this.panel3.Controls.Add(this.lblOrderDate);
            this.panel3.Controls.Add(this.lblDestinationstore);
            this.panel3.Controls.Add(this.dtpOrderDate);
            this.panel3.Controls.Add(this.txtOrderNumber);
            this.panel3.Controls.Add(this.lblOrderNumber);
            this.panel3.Controls.Add(this.ddlSupplier);
            this.panel3.Controls.Add(this.lblSupplier);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(852, 96);
            this.panel3.TabIndex = 1;
            this.panel3.Tag = "pnlSubPanelSCM";
            // 
            // ddlDestinationStore
            // 
            this.ddlDestinationStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDestinationStore.FormattingEnabled = true;
            this.ddlDestinationStore.Location = new System.Drawing.Point(578, 55);
            this.ddlDestinationStore.Name = "ddlDestinationStore";
            this.ddlDestinationStore.Size = new System.Drawing.Size(168, 21);
            this.ddlDestinationStore.TabIndex = 8;
            this.ddlDestinationStore.Tag = "ddlDropDownList";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(512, 15);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(62, 13);
            this.lblOrderDate.TabIndex = 44;
            this.lblOrderDate.Tag = "lblLabelRequired";
            this.lblOrderDate.Text = "Order Date:";
            // 
            // lblDestinationstore
            // 
            this.lblDestinationstore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDestinationstore.AutoSize = true;
            this.lblDestinationstore.Location = new System.Drawing.Point(483, 57);
            this.lblDestinationstore.Name = "lblDestinationstore";
            this.lblDestinationstore.Size = new System.Drawing.Size(91, 13);
            this.lblDestinationstore.TabIndex = 42;
            this.lblDestinationstore.Tag = "lblLabel";
            this.lblDestinationstore.Text = "Destination Store:";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderDate.Location = new System.Drawing.Point(578, 13);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(168, 20);
            this.dtpOrderDate.TabIndex = 5;
            this.dtpOrderDate.Tag = "txtTextBoxSCM";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Location = new System.Drawing.Point(181, 13);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(168, 20);
            this.txtOrderNumber.TabIndex = 4;
            this.txtOrderNumber.Tag = "txtTextBox";
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Location = new System.Drawing.Point(102, 15);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(76, 13);
            this.lblOrderNumber.TabIndex = 39;
            this.lblOrderNumber.Tag = "lblLabel";
            this.lblOrderNumber.Text = "Order Number:";
            // 
            // ddlSupplier
            // 
            this.ddlSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSupplier.Location = new System.Drawing.Point(180, 55);
            this.ddlSupplier.Name = "ddlSupplier";
            this.ddlSupplier.Size = new System.Drawing.Size(168, 21);
            this.ddlSupplier.TabIndex = 6;
            this.ddlSupplier.Tag = "ddlDropDownList";
            this.ddlSupplier.SelectedValueChanged += new System.EventHandler(this.ddlSupplier_SelectedValueChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(128, 57);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(48, 13);
            this.lblSupplier.TabIndex = 37;
            this.lblSupplier.Tag = "lblLabel";
            this.lblSupplier.Text = "Supplier:";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.lstSearch);
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 30;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Window;
            this.btnPrint.Location = new System.Drawing.Point(691, 10);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 25);
            this.btnPrint.TabIndex = 32;
            this.btnPrint.Tag = "btnSingleText";
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(611, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 31;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.SystemColors.Window;
            this.btnclose.Location = new System.Drawing.Point(771, 10);
            this.btnclose.Margin = new System.Windows.Forms.Padding(0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(80, 25);
            this.btnclose.TabIndex = 33;
            this.btnclose.Tag = "btnSingleText";
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(704, 383);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(70, 13);
            this.lblTotalAmount.TabIndex = 65;
            this.lblTotalAmount.Tag = "lblLabel";
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Item Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Purchase Units";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit Quantity";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Order Quantity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Price/Unit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 83;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Total Price";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmPurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgwItemSubitemDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPurchaseOrder";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Purchase Order";
            this.Load += new System.EventHandler(this.frmPurchaseOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwItemSubitemDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlAuthorisedBy;
        private System.Windows.Forms.Label lblAuthorizedBy;
        private System.Windows.Forms.ComboBox ddlPreparedBy;
        private System.Windows.Forms.Label lblPreparedBy;
        private System.Windows.Forms.DataGridView dgwItemSubitemDetails;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox ddlDestinationStore;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblDestinationstore;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.ComboBox ddlSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.ListBox lstSearch;
        private System.Windows.Forms.CheckBox chkRejectedStatus;
        private System.Windows.Forms.Button Btndelete;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Units;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}