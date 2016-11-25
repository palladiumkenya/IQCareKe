namespace IQCare.SCM
{
    partial class frmItemMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemMaster));
            this.lblArvAbbre = new System.Windows.Forms.Label();
            this.lblDispesningUnit = new System.Windows.Forms.Label();
            this.cmbDispensingUnit = new System.Windows.Forms.ComboBox();
            this.txtArvAbbrevstion = new System.Windows.Forms.TextBox();
            this.lblgernericName = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblItemType = new System.Windows.Forms.Label();
            this.txtItemType = new System.Windows.Forms.TextBox();
            this.lblLionccode = new System.Windows.Forms.Label();
            this.txtItemSubtype = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtGeneric = new System.Windows.Forms.TextBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFDACode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTradeName = new System.Windows.Forms.TextBox();
            this.lblItemIdentifiers = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDispenseUnitPrice = new System.Windows.Forms.TextBox();
            this.lblOutSrcLocation = new System.Windows.Forms.Label();
            this.txtPurchaseUnitPrice = new System.Windows.Forms.TextBox();
            this.lblpurchaseUnitPrice = new System.Windows.Forms.Label();
            this.cmbPurchaseUnit = new System.Windows.Forms.ComboBox();
            this.lblpurchaseUnit = new System.Windows.Forms.Label();
            this.txteditsellingprice = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.txtUnitQty = new System.Windows.Forms.TextBox();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.lblUnitQuantity = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblDispensingMargin = new System.Windows.Forms.Label();
            this.txtdespensingMargin = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxStock = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblArvAbbre
            // 
            this.lblArvAbbre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblArvAbbre.AutoSize = true;
            this.lblArvAbbre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblArvAbbre.Location = new System.Drawing.Point(25, 85);
            this.lblArvAbbre.Name = "lblArvAbbre";
            this.lblArvAbbre.Size = new System.Drawing.Size(94, 13);
            this.lblArvAbbre.TabIndex = 37;
            this.lblArvAbbre.Tag = "lblLabel";
            this.lblArvAbbre.Text = "ARV Abbreviation:";
            // 
            // lblDispesningUnit
            // 
            this.lblDispesningUnit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDispesningUnit.AutoSize = true;
            this.lblDispesningUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDispesningUnit.Location = new System.Drawing.Point(46, 85);
            this.lblDispesningUnit.Name = "lblDispesningUnit";
            this.lblDispesningUnit.Size = new System.Drawing.Size(84, 13);
            this.lblDispesningUnit.TabIndex = 29;
            this.lblDispesningUnit.Tag = "lblLabelRequired";
            this.lblDispesningUnit.Text = "Dispensing Unit:";
            // 
            // cmbDispensingUnit
            // 
            this.cmbDispensingUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDispensingUnit.FormattingEnabled = true;
            this.cmbDispensingUnit.Items.AddRange(new object[] {
            "--Select--",
            "Active",
            "In-Active"});
            this.cmbDispensingUnit.Location = new System.Drawing.Point(139, 82);
            this.cmbDispensingUnit.Name = "cmbDispensingUnit";
            this.cmbDispensingUnit.Size = new System.Drawing.Size(264, 21);
            this.cmbDispensingUnit.TabIndex = 12;
            this.cmbDispensingUnit.Tag = "ddlDropDownList";
            // 
            // txtArvAbbrevstion
            // 
            this.txtArvAbbrevstion.Enabled = false;
            this.txtArvAbbrevstion.Location = new System.Drawing.Point(121, 81);
            this.txtArvAbbrevstion.Name = "txtArvAbbrevstion";
            this.txtArvAbbrevstion.Size = new System.Drawing.Size(262, 20);
            this.txtArvAbbrevstion.TabIndex = 13;
            this.txtArvAbbrevstion.Tag = "txtTextBox";
            this.txtArvAbbrevstion.Text = " ";
            // 
            // lblgernericName
            // 
            this.lblgernericName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblgernericName.AutoSize = true;
            this.lblgernericName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblgernericName.Location = new System.Drawing.Point(52, 51);
            this.lblgernericName.Name = "lblgernericName";
            this.lblgernericName.Size = new System.Drawing.Size(78, 13);
            this.lblgernericName.TabIndex = 22;
            this.lblgernericName.Tag = "lblLabel";
            this.lblgernericName.Text = "Generic Name:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblItemType);
            this.splitContainer1.Panel1.Controls.Add(this.txtItemType);
            this.splitContainer1.Panel1.Tag = "pnlPanel";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblLionccode);
            this.splitContainer1.Panel2.Controls.Add(this.txtItemSubtype);
            this.splitContainer1.Panel2.Tag = "pnlPanel";
            this.splitContainer1.Size = new System.Drawing.Size(852, 50);
            this.splitContainer1.SplitterDistance = 434;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.Tag = "pnlSubPanel";
            // 
            // lblItemType
            // 
            this.lblItemType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(73, 18);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(57, 13);
            this.lblItemType.TabIndex = 49;
            this.lblItemType.Tag = "lblLabel";
            this.lblItemType.Text = "Item Type:";
            // 
            // txtItemType
            // 
            this.txtItemType.Enabled = false;
            this.txtItemType.Location = new System.Drawing.Point(139, 16);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(262, 20);
            this.txtItemType.TabIndex = 6;
            this.txtItemType.Tag = "txtTextBox";
            // 
            // lblLionccode
            // 
            this.lblLionccode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLionccode.AutoSize = true;
            this.lblLionccode.Location = new System.Drawing.Point(40, 19);
            this.lblLionccode.Name = "lblLionccode";
            this.lblLionccode.Size = new System.Drawing.Size(79, 13);
            this.lblLionccode.TabIndex = 50;
            this.lblLionccode.Tag = "lblLabel";
            this.lblLionccode.Text = "Item Sub-Type:";
            // 
            // txtItemSubtype
            // 
            this.txtItemSubtype.Enabled = false;
            this.txtItemSubtype.Location = new System.Drawing.Point(121, 16);
            this.txtItemSubtype.Name = "txtItemSubtype";
            this.txtItemSubtype.Size = new System.Drawing.Size(262, 20);
            this.txtItemSubtype.TabIndex = 7;
            this.txtItemSubtype.Tag = "txtTextBox";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(0, 74);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtGeneric);
            this.splitContainer2.Panel1.Controls.Add(this.lblItemCode);
            this.splitContainer2.Panel1.Controls.Add(this.lblDispesningUnit);
            this.splitContainer2.Panel1.Controls.Add(this.cmbDispensingUnit);
            this.splitContainer2.Panel1.Controls.Add(this.txtItemCode);
            this.splitContainer2.Panel1.Controls.Add(this.lblgernericName);
            this.splitContainer2.Panel1.Tag = "pnlPanel";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.txtFDACode);
            this.splitContainer2.Panel2.Controls.Add(this.lblArvAbbre);
            this.splitContainer2.Panel2.Controls.Add(this.txtArvAbbrevstion);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.txtTradeName);
            this.splitContainer2.Panel2.Tag = "pnlPanel";
            this.splitContainer2.Size = new System.Drawing.Size(852, 116);
            this.splitContainer2.SplitterDistance = 433;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            this.splitContainer2.Tag = "pnlSubPanel";
            // 
            // txtGeneric
            // 
            this.txtGeneric.Enabled = false;
            this.txtGeneric.Location = new System.Drawing.Point(139, 47);
            this.txtGeneric.Name = "txtGeneric";
            this.txtGeneric.Size = new System.Drawing.Size(262, 20);
            this.txtGeneric.TabIndex = 10;
            this.txtGeneric.Tag = "txtTextBox";
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Location = new System.Drawing.Point(72, 17);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(58, 13);
            this.lblItemCode.TabIndex = 49;
            this.lblItemCode.Tag = "lblLabel";
            this.lblItemCode.Text = "Item Code:";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(139, 13);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(262, 20);
            this.txtItemCode.TabIndex = 8;
            this.txtItemCode.TabStop = false;
            this.txtItemCode.Tag = "txtTextBox";
            this.txtItemCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemCode_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 53;
            this.label7.Tag = "lblLabel";
            this.label7.Text = "FDA Code:";
            // 
            // txtFDACode
            // 
            this.txtFDACode.Location = new System.Drawing.Point(121, 12);
            this.txtFDACode.Name = "txtFDACode";
            this.txtFDACode.Size = new System.Drawing.Size(262, 20);
            this.txtFDACode.TabIndex = 9;
            this.txtFDACode.Tag = "txtTextBox";
            this.txtFDACode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFDACode_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 50;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Trade/Item Name:";
            // 
            // txtTradeName
            // 
            this.txtTradeName.Enabled = false;
            this.txtTradeName.Location = new System.Drawing.Point(121, 47);
            this.txtTradeName.Name = "txtTradeName";
            this.txtTradeName.Size = new System.Drawing.Size(262, 20);
            this.txtTradeName.TabIndex = 11;
            this.txtTradeName.Tag = "txtTextBox";
            // 
            // lblItemIdentifiers
            // 
            this.lblItemIdentifiers.AutoSize = true;
            this.lblItemIdentifiers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemIdentifiers.Location = new System.Drawing.Point(3, 57);
            this.lblItemIdentifiers.Name = "lblItemIdentifiers";
            this.lblItemIdentifiers.Size = new System.Drawing.Size(98, 13);
            this.lblItemIdentifiers.TabIndex = 59;
            this.lblItemIdentifiers.Tag = "";
            this.lblItemIdentifiers.Text = "Item Indentifiers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 60;
            this.label2.Tag = "";
            this.label2.Text = "Cost Information";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Location = new System.Drawing.Point(0, 215);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dtpEffectiveDate);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.txtDispenseUnitPrice);
            this.splitContainer3.Panel1.Controls.Add(this.lblOutSrcLocation);
            this.splitContainer3.Panel1.Controls.Add(this.txtPurchaseUnitPrice);
            this.splitContainer3.Panel1.Controls.Add(this.lblpurchaseUnitPrice);
            this.splitContainer3.Panel1.Controls.Add(this.cmbPurchaseUnit);
            this.splitContainer3.Panel1.Controls.Add(this.lblpurchaseUnit);
            this.splitContainer3.Panel1.Tag = "pnlPanel";
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txteditsellingprice);
            this.splitContainer3.Panel2.Controls.Add(this.label9);
            this.splitContainer3.Panel2.Controls.Add(this.label8);
            this.splitContainer3.Panel2.Controls.Add(this.txtSellingPrice);
            this.splitContainer3.Panel2.Controls.Add(this.txtUnitQty);
            this.splitContainer3.Panel2.Controls.Add(this.cmbManufacturer);
            this.splitContainer3.Panel2.Controls.Add(this.lblUnitQuantity);
            this.splitContainer3.Panel2.Controls.Add(this.lblManufacturer);
            this.splitContainer3.Panel2.Controls.Add(this.lblDispensingMargin);
            this.splitContainer3.Panel2.Controls.Add(this.txtdespensingMargin);
            this.splitContainer3.Panel2.Tag = "pnlPanel";
            this.splitContainer3.Size = new System.Drawing.Size(852, 148);
            this.splitContainer3.SplitterDistance = 432;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 3;
            this.splitContainer3.TabStop = false;
            this.splitContainer3.Tag = "pnlSubPanel";
            // 
            // dtpEffectiveDate
            // 
            this.dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveDate.Location = new System.Drawing.Point(139, 110);
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.Size = new System.Drawing.Size(262, 20);
            this.dtpEffectiveDate.TabIndex = 20;
            this.dtpEffectiveDate.Tag = "txtTextBoxSCM";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(52, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 33;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Effective Date:";
            // 
            // txtDispenseUnitPrice
            // 
            this.txtDispenseUnitPrice.Enabled = false;
            this.txtDispenseUnitPrice.Location = new System.Drawing.Point(139, 78);
            this.txtDispenseUnitPrice.Name = "txtDispenseUnitPrice";
            this.txtDispenseUnitPrice.Size = new System.Drawing.Size(262, 20);
            this.txtDispenseUnitPrice.TabIndex = 18;
            this.txtDispenseUnitPrice.Tag = "txtTextBox";
            this.txtDispenseUnitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDispenseUnitPrice_KeyPress);
            // 
            // lblOutSrcLocation
            // 
            this.lblOutSrcLocation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOutSrcLocation.AutoSize = true;
            this.lblOutSrcLocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOutSrcLocation.Location = new System.Drawing.Point(19, 81);
            this.lblOutSrcLocation.Name = "lblOutSrcLocation";
            this.lblOutSrcLocation.Size = new System.Drawing.Size(111, 13);
            this.lblOutSrcLocation.TabIndex = 31;
            this.lblOutSrcLocation.Tag = "lblLabelRequired";
            this.lblOutSrcLocation.Text = "Dispensing Unit Price:";
            // 
            // txtPurchaseUnitPrice
            // 
            this.txtPurchaseUnitPrice.Location = new System.Drawing.Point(139, 46);
            this.txtPurchaseUnitPrice.Name = "txtPurchaseUnitPrice";
            this.txtPurchaseUnitPrice.Size = new System.Drawing.Size(262, 20);
            this.txtPurchaseUnitPrice.TabIndex = 16;
            this.txtPurchaseUnitPrice.Tag = "txtTextBox";
            this.txtPurchaseUnitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPurchaseUnitPrice_KeyPress);
            this.txtPurchaseUnitPrice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseUnitPrice_KeyUp);
            // 
            // lblpurchaseUnitPrice
            // 
            this.lblpurchaseUnitPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblpurchaseUnitPrice.AutoSize = true;
            this.lblpurchaseUnitPrice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblpurchaseUnitPrice.Location = new System.Drawing.Point(26, 49);
            this.lblpurchaseUnitPrice.Name = "lblpurchaseUnitPrice";
            this.lblpurchaseUnitPrice.Size = new System.Drawing.Size(104, 13);
            this.lblpurchaseUnitPrice.TabIndex = 29;
            this.lblpurchaseUnitPrice.Tag = "lblLabelRequired";
            this.lblpurchaseUnitPrice.Text = "Purchase Unit Price:";
            // 
            // cmbPurchaseUnit
            // 
            this.cmbPurchaseUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurchaseUnit.FormattingEnabled = true;
            this.cmbPurchaseUnit.Items.AddRange(new object[] {
            "--Select--",
            "Active",
            "In-Active"});
            this.cmbPurchaseUnit.Location = new System.Drawing.Point(139, 11);
            this.cmbPurchaseUnit.Name = "cmbPurchaseUnit";
            this.cmbPurchaseUnit.Size = new System.Drawing.Size(262, 21);
            this.cmbPurchaseUnit.TabIndex = 14;
            this.cmbPurchaseUnit.Tag = "ddlDropDownList";
            // 
            // lblpurchaseUnit
            // 
            this.lblpurchaseUnit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblpurchaseUnit.AutoSize = true;
            this.lblpurchaseUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblpurchaseUnit.Location = new System.Drawing.Point(53, 15);
            this.lblpurchaseUnit.Name = "lblpurchaseUnit";
            this.lblpurchaseUnit.Size = new System.Drawing.Size(77, 13);
            this.lblpurchaseUnit.TabIndex = 27;
            this.lblpurchaseUnit.Tag = "lblLabelRequired";
            this.lblpurchaseUnit.Text = "Purchase Unit:";
            // 
            // txteditsellingprice
            // 
            this.txteditsellingprice.Location = new System.Drawing.Point(306, 109);
            this.txteditsellingprice.Name = "txteditsellingprice";
            this.txteditsellingprice.Size = new System.Drawing.Size(80, 20);
            this.txteditsellingprice.TabIndex = 46;
            this.txteditsellingprice.Tag = "txtTextBox";
            this.txteditsellingprice.Visible = false;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(223, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 45;
            this.label9.Tag = "lblLabel";
            this.label9.Text = "Selling Price:";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(10, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 44;
            this.label8.Tag = "lblLabel";
            this.label8.Text = "Calculated  Selling Price:";
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Enabled = false;
            this.txtSellingPrice.Location = new System.Drawing.Point(140, 109);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(77, 20);
            this.txtSellingPrice.TabIndex = 21;
            this.txtSellingPrice.Tag = "txtTextBox";
            this.txtSellingPrice.Text = " ";
            // 
            // txtUnitQty
            // 
            this.txtUnitQty.Location = new System.Drawing.Point(123, 14);
            this.txtUnitQty.Name = "txtUnitQty";
            this.txtUnitQty.Size = new System.Drawing.Size(262, 20);
            this.txtUnitQty.TabIndex = 15;
            this.txtUnitQty.Tag = "txtTextBox";
            this.txtUnitQty.Text = " ";
            this.txtUnitQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitQty_KeyPress);
            this.txtUnitQty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUnitQty_KeyUp);
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(125, 49);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(264, 21);
            this.cmbManufacturer.TabIndex = 17;
            this.cmbManufacturer.Tag = "ddlDropDownList";
            // 
            // lblUnitQuantity
            // 
            this.lblUnitQuantity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitQuantity.AutoSize = true;
            this.lblUnitQuantity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUnitQuantity.Location = new System.Drawing.Point(0, 18);
            this.lblUnitQuantity.Name = "lblUnitQuantity";
            this.lblUnitQuantity.Size = new System.Drawing.Size(119, 13);
            this.lblUnitQuantity.TabIndex = 41;
            this.lblUnitQuantity.Tag = "lblLabelRequired";
            this.lblUnitQuantity.Text = "Purchase Unit Quantity:";
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblManufacturer.Location = new System.Drawing.Point(46, 50);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(73, 13);
            this.lblManufacturer.TabIndex = 40;
            this.lblManufacturer.Tag = "lblLabelRequired";
            this.lblManufacturer.Text = "Manufacturer:";
            // 
            // lblDispensingMargin
            // 
            this.lblDispensingMargin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDispensingMargin.AutoSize = true;
            this.lblDispensingMargin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDispensingMargin.Location = new System.Drawing.Point(10, 82);
            this.lblDispensingMargin.Name = "lblDispensingMargin";
            this.lblDispensingMargin.Size = new System.Drawing.Size(108, 13);
            this.lblDispensingMargin.TabIndex = 35;
            this.lblDispensingMargin.Tag = "lblLabelRequired";
            this.lblDispensingMargin.Text = "Dispensing Margin% :";
            // 
            // txtdespensingMargin
            // 
            this.txtdespensingMargin.Location = new System.Drawing.Point(124, 79);
            this.txtdespensingMargin.Name = "txtdespensingMargin";
            this.txtdespensingMargin.Size = new System.Drawing.Size(262, 20);
            this.txtdespensingMargin.TabIndex = 19;
            this.txtdespensingMargin.Tag = "txtTextBox";
            this.txtdespensingMargin.Text = " ";
            this.txtdespensingMargin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdespensingMargin_KeyPress);
            this.txtdespensingMargin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtdespensingMargin_KeyUp);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer4.Location = new System.Drawing.Point(0, 404);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label4);
            this.splitContainer4.Panel1.Controls.Add(this.txtMinStock);
            this.splitContainer4.Panel1.Tag = "pnlPanel";
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label5);
            this.splitContainer4.Panel2.Controls.Add(this.txtMaxStock);
            this.splitContainer4.Panel2.Tag = "pnlPanel";
            this.splitContainer4.Size = new System.Drawing.Size(852, 50);
            this.splitContainer4.SplitterDistance = 433;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 5;
            this.splitContainer4.TabStop = false;
            this.splitContainer4.Tag = "pnlSubPanel";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 49;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "Reorder Level:";
            // 
            // txtMinStock
            // 
            this.txtMinStock.Location = new System.Drawing.Point(139, 13);
            this.txtMinStock.Name = "txtMinStock";
            this.txtMinStock.Size = new System.Drawing.Size(262, 20);
            this.txtMinStock.TabIndex = 22;
            this.txtMinStock.Tag = "txtTextBox";
            this.txtMinStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinStock_KeyPress);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 50;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Maximum Stock Unit: ";
            // 
            // txtMaxStock
            // 
            this.txtMaxStock.Location = new System.Drawing.Point(121, 13);
            this.txtMaxStock.Name = "txtMaxStock";
            this.txtMaxStock.Size = new System.Drawing.Size(262, 20);
            this.txtMaxStock.TabIndex = 23;
            this.txtMaxStock.Tag = "txtTextBox";
            this.txtMaxStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxStock_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 386);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 63;
            this.label6.Tag = "";
            this.label6.Text = "Stock Information";
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
            this.panel2.TabIndex = 25;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(692, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 26;
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
            this.btnClose.TabIndex = 27;
            this.btnClose.Tag = "btnSingleText";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus.Location = new System.Drawing.Point(272, 372);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 66;
            this.lblStatus.Tag = "lblLabel";
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "In-Active"});
            this.cmbStatus.Location = new System.Drawing.Point(314, 369);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(267, 21);
            this.cmbStatus.TabIndex = 4;
            this.cmbStatus.Tag = "ddlDropDownList";
            // 
            // frmItemMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(852, 504);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblItemIdentifiers);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmItemMaster";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Item Master";
            this.Load += new System.EventHandler(this.frmItemMaster_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDispesningUnit;
        private System.Windows.Forms.ComboBox cmbDispensingUnit;
        private System.Windows.Forms.TextBox txtArvAbbrevstion;
        private System.Windows.Forms.Label lblgernericName;
        private System.Windows.Forms.Label lblArvAbbre;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.TextBox txtItemType;
        private System.Windows.Forms.Label lblLionccode;
        private System.Windows.Forms.TextBox txtItemSubtype;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTradeName;
        private System.Windows.Forms.Label lblItemIdentifiers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDispenseUnitPrice;
        private System.Windows.Forms.Label lblOutSrcLocation;
        private System.Windows.Forms.TextBox txtPurchaseUnitPrice;
        private System.Windows.Forms.Label lblpurchaseUnitPrice;
        private System.Windows.Forms.ComboBox cmbPurchaseUnit;
        private System.Windows.Forms.Label lblpurchaseUnit;
        private System.Windows.Forms.Label lblUnitQuantity;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblDispensingMargin;
        private System.Windows.Forms.TextBox txtdespensingMargin;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMinStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaxStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFDACode;
        private System.Windows.Forms.TextBox txtGeneric;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.TextBox txtUnitQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txteditsellingprice;
        private System.Windows.Forms.Label label9;
    }
}