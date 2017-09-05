namespace IQCare.SCM
{
    partial class frmInterRequisitionIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInterRequisitionIssue));
            this.ddlAuthorisedBy = new System.Windows.Forms.ComboBox();
            this.lblAuthorizedBy = new System.Windows.Forms.Label();
            this.ddlPreparedBy = new System.Windows.Forms.ComboBox();
            this.lblPreparedBy = new System.Windows.Forms.Label();
            this.dgwItemSubitemDetails = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuedQuantityDU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btndelete = new System.Windows.Forms.Button();
            this.chkRejectedStatus = new System.Windows.Forms.CheckBox();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ddlDestinationStore = new System.Windows.Forms.ComboBox();
            this.lblSourceStore = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.ddlSourceStore = new System.Windows.Forms.ComboBox();
            this.lblDestinationstore = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblOrderNumber = new System.Windows.Forms.Label();
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
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgwItemSubitemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwItemSubitemDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgwItemSubitemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwItemSubitemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.ItemCode,
            this.Units,
            this.UnitQuantity,
            this.OrderQuantity,
            this.IssuedQuantity,
            this.IssuedQuantityDU,
            this.Price,
            this.TotPrice,
            this.ExpiryDate,
            this.BatchName,
            this.BatchID,
            this.AvailableQTY});
            this.dgwItemSubitemDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwItemSubitemDetails.Location = new System.Drawing.Point(0, 100);
            this.dgwItemSubitemDetails.Name = "dgwItemSubitemDetails";
            this.dgwItemSubitemDetails.Size = new System.Drawing.Size(1034, 273);
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
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemId";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ItemCode
            // 
            this.ItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.FillWeight = 1F;
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.Visible = false;
            // 
            // Units
            // 
            this.Units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Units.DataPropertyName = "Units";
            this.Units.FillWeight = 28.0222F;
            this.Units.HeaderText = "Purchase Units";
            this.Units.Name = "Units";
            this.Units.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitQuantity
            // 
            this.UnitQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitQuantity.DataPropertyName = "UnitQuantity";
            this.UnitQuantity.FillWeight = 28.0222F;
            this.UnitQuantity.HeaderText = "Unit Qty";
            this.UnitQuantity.Name = "UnitQuantity";
            // 
            // OrderQuantity
            // 
            this.OrderQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OrderQuantity.DataPropertyName = "OrderQuantity";
            this.OrderQuantity.FillWeight = 28.0222F;
            this.OrderQuantity.HeaderText = "Order Qty";
            this.OrderQuantity.Name = "OrderQuantity";
            this.OrderQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IssuedQuantity
            // 
            this.IssuedQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IssuedQuantity.DataPropertyName = "IssuedQuantity";
            this.IssuedQuantity.FillWeight = 28.0222F;
            this.IssuedQuantity.HeaderText = "Issued Qty - Purchasing Unit";
            this.IssuedQuantity.Name = "IssuedQuantity";
            this.IssuedQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IssuedQuantityDU
            // 
            this.IssuedQuantityDU.DataPropertyName = "IssuedQuantityDU";
            this.IssuedQuantityDU.HeaderText = "IssuedQty - Disp Unit";
            this.IssuedQuantityDU.Name = "IssuedQuantityDU";
            this.IssuedQuantityDU.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Price.DataPropertyName = "Price";
            this.Price.FillWeight = 28.0222F;
            this.Price.HeaderText = "Price/Unit";
            this.Price.Name = "Price";
            this.Price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotPrice
            // 
            this.TotPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotPrice.DataPropertyName = "TotPrice";
            this.TotPrice.FillWeight = 28.0222F;
            this.TotPrice.HeaderText = "Total Price";
            this.TotPrice.Name = "TotPrice";
            this.TotPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExpiryDate.DataPropertyName = "ExpiryDate";
            this.ExpiryDate.FillWeight = 28.0222F;
            this.ExpiryDate.HeaderText = "Expiry Date";
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            // 
            // BatchName
            // 
            this.BatchName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BatchName.DataPropertyName = "BatchName";
            this.BatchName.FillWeight = 28.0222F;
            this.BatchName.HeaderText = "Batch #";
            this.BatchName.Name = "BatchName";
            this.BatchName.ReadOnly = true;
            // 
            // BatchID
            // 
            this.BatchID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BatchID.DataPropertyName = "BatchID";
            this.BatchID.FillWeight = 1F;
            this.BatchID.HeaderText = "BatchID";
            this.BatchID.Name = "BatchID";
            this.BatchID.ReadOnly = true;
            this.BatchID.Visible = false;
            // 
            // AvailableQTY
            // 
            this.AvailableQTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AvailableQTY.DataPropertyName = "AvailableQTY";
            this.AvailableQTY.FillWeight = 28.0222F;
            this.AvailableQTY.HeaderText = "Available Qty";
            this.AvailableQTY.Name = "AvailableQTY";
            this.AvailableQTY.ReadOnly = true;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(846, 383);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 13);
            this.lblTotal.TabIndex = 57;
            this.lblTotal.Tag = "lblLabel";
            this.lblTotal.Text = "Total:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Btndelete);
            this.panel1.Controls.Add(this.chkRejectedStatus);
            this.panel1.Controls.Add(this.ddlAuthorisedBy);
            this.panel1.Controls.Add(this.lblPreparedBy);
            this.panel1.Controls.Add(this.lblAuthorizedBy);
            this.panel1.Controls.Add(this.ddlPreparedBy);
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 57);
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
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ddlDestinationStore);
            this.panel3.Controls.Add(this.lblSourceStore);
            this.panel3.Controls.Add(this.lblOrderDate);
            this.panel3.Controls.Add(this.ddlSourceStore);
            this.panel3.Controls.Add(this.lblDestinationstore);
            this.panel3.Controls.Add(this.dtpOrderDate);
            this.panel3.Controls.Add(this.txtOrderNumber);
            this.panel3.Controls.Add(this.lblOrderNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1034, 70);
            this.panel3.TabIndex = 1;
            this.panel3.Tag = "pnlSubPanelSCM";
            // 
            // ddlDestinationStore
            // 
            this.ddlDestinationStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDestinationStore.FormattingEnabled = true;
            this.ddlDestinationStore.Location = new System.Drawing.Point(587, 44);
            this.ddlDestinationStore.Name = "ddlDestinationStore";
            this.ddlDestinationStore.Size = new System.Drawing.Size(168, 21);
            this.ddlDestinationStore.TabIndex = 8;
            this.ddlDestinationStore.Tag = "ddlDropDownList";
            // 
            // lblSourceStore
            // 
            this.lblSourceStore.AutoSize = true;
            this.lblSourceStore.Location = new System.Drawing.Point(121, 51);
            this.lblSourceStore.Name = "lblSourceStore";
            this.lblSourceStore.Size = new System.Drawing.Size(72, 13);
            this.lblSourceStore.TabIndex = 47;
            this.lblSourceStore.Tag = "lblLabel";
            this.lblSourceStore.Text = "Source Store:";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(519, 19);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(62, 13);
            this.lblOrderDate.TabIndex = 44;
            this.lblOrderDate.Tag = "lblLabelRequired";
            this.lblOrderDate.Text = "Order Date:";
            // 
            // ddlSourceStore
            // 
            this.ddlSourceStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSourceStore.FormattingEnabled = true;
            this.ddlSourceStore.Location = new System.Drawing.Point(201, 44);
            this.ddlSourceStore.Name = "ddlSourceStore";
            this.ddlSourceStore.Size = new System.Drawing.Size(168, 21);
            this.ddlSourceStore.TabIndex = 7;
            this.ddlSourceStore.Tag = "ddlDropDownList";
            this.ddlSourceStore.SelectedValueChanged += new System.EventHandler(this.ddlSourceStore_SelectedValueChanged);
            // 
            // lblDestinationstore
            // 
            this.lblDestinationstore.AutoSize = true;
            this.lblDestinationstore.Location = new System.Drawing.Point(492, 47);
            this.lblDestinationstore.Name = "lblDestinationstore";
            this.lblDestinationstore.Size = new System.Drawing.Size(91, 13);
            this.lblDestinationstore.TabIndex = 42;
            this.lblDestinationstore.Tag = "lblLabel";
            this.lblDestinationstore.Text = "Destination Store:";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOrderDate.Location = new System.Drawing.Point(587, 13);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(168, 20);
            this.dtpOrderDate.TabIndex = 5;
            this.dtpOrderDate.Tag = "txtTextBoxSCM";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Location = new System.Drawing.Point(201, 13);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(168, 20);
            this.txtOrderNumber.TabIndex = 4;
            this.txtOrderNumber.Tag = "txtTextBox";
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Location = new System.Drawing.Point(121, 19);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(76, 13);
            this.lblOrderNumber.TabIndex = 39;
            this.lblOrderNumber.Tag = "lblLabel";
            this.lblOrderNumber.Text = "Order Number:";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.lstSearch);
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 467);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1034, 37);
            this.panel2.TabIndex = 30;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.SystemColors.Window;
            this.btnPrint.Location = new System.Drawing.Point(873, 10);
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
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(793, 10);
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
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.BackColor = System.Drawing.SystemColors.Window;
            this.btnclose.Location = new System.Drawing.Point(953, 10);
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
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(886, 383);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(82, 13);
            this.lblTotalAmount.TabIndex = 65;
            this.lblTotalAmount.Tag = "lblLabel";
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Units";
            this.dataGridViewTextBoxColumn1.FillWeight = 1F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Item Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "BatchID";
            this.dataGridViewTextBoxColumn2.FillWeight = 1F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Purchase Units";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Units";
            this.dataGridViewTextBoxColumn3.FillWeight = 1F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit Quantity";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "OrderQuantity";
            this.dataGridViewTextBoxColumn4.FillWeight = 1F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Order Quantity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IssuedQuantity";
            this.dataGridViewTextBoxColumn5.FillWeight = 1F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Price/Unit";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ExpiryDate";
            this.dataGridViewTextBoxColumn6.FillWeight = 1F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Total Price";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ExpiryDate";
            this.dataGridViewTextBoxColumn7.FillWeight = 1F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Expiry Date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "BatchName";
            this.dataGridViewTextBoxColumn8.FillWeight = 1F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Batch No.";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "AvailableQTY";
            this.dataGridViewTextBoxColumn9.FillWeight = 1F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Available Qty";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "AvailableQTY";
            this.dataGridViewTextBoxColumn10.FillWeight = 1F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Available Qty";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "AvailableQTY";
            this.dataGridViewTextBoxColumn11.FillWeight = 1F;
            this.dataGridViewTextBoxColumn11.HeaderText = "Available Qty";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "AvailableQTY";
            this.dataGridViewTextBoxColumn12.FillWeight = 1F;
            this.dataGridViewTextBoxColumn12.HeaderText = "Available Qty";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // frmInterRequistionIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1034, 504);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgwItemSubitemDetails);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInterRequistionIssue";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Counter Requisition And Issue Voucher";
            this.Load += new System.EventHandler(this.FormLoad);
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
        private System.Windows.Forms.Label lblSourceStore;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.ComboBox ddlSourceStore;
        private System.Windows.Forms.Label lblDestinationstore;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.ListBox lstSearch;
        private System.Windows.Forms.CheckBox chkRejectedStatus;
        private System.Windows.Forms.Button Btndelete;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Units;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuedQuantityDU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableQTY;
    }
}