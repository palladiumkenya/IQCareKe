// Decompiled with JetBrains decompiler
// Type: IQCare.SCM.frmPatientDrugDispense
// Assembly: IQCare.SCM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 88163121-5E07-40F0-B0F4-79C7822A045C
// Assembly location: C:\HMIS\System\3.5.6\IQCare Management\IQCare.SCM.dll

using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.FormBuilder;
using Interface.SCM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace IQCare.SCM
{
    public class PatientDrugDispenseForm : Form
    {
        private DataSet XMLDS = new DataSet();
        private DataSet XMLPharDS = new DataSet();
        private DataSet thePharmacyMaster = new DataSet();
        private DataTable theExistingDrugs = new DataTable();
        private string theDispensingUnitName = "";
        private string theGenericAbb = "";
        private string theOrderStatus = "";
        private string makeGridEditable = "";
        private string lastdispensedARV = "";
        private string ARVBeingDispensed = "";
        private string ItemInstructions = "";
        private TextBox theReturnQty = new TextBox();
        private int theDispCurrentRow = -1;
        private ImageList imageList1 = new ImageList();
        private IContainer components;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private TabControl tabDispense;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label3;
        private TextBox txtLastName;
        private Label label4;
        private Label label1;
        private TextBox txtPatientIdentification;
        private Label label2;
        private Label label5;
        private Label label6;
        private ComboBox cmbSex;
        private TextBox txtFirstName;
        private ComboBox cmbFacility;
        private DateTimePicker dtpDOB;
        private Button btnFind;
        private DataGridView grdResultView;
        private Label label7;
        private Label label13;
        private Label label16;
        private TextBox textBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView grdDrugDispense;
        private DateTimePicker dateTimePicker2;
        private TabPage tabPage4;
        private DataGridView grdReturnDetail;
        private DataGridView grdReturnOrder;
        private Label label25;
        private Button btnSave;
        private Button btnCancel;
        private Button btnDispenseSubmit;
        private Button button3;
        private Button button4;
        private Panel panel2;
        private Button cmdSave;
        private Button cmdClose;
        private Label lblDispDate;
        private Label label32;
        private Label label31;
        private Label label24;
        private Label lblLstRegimen;
        private Label lblStoreName;
        private Label label22;
        private Label lblPatientName;
        private Label label29;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label23;
        private ComboBox cmbprogram;
        private Label lblPayAmount;
        private Label label28;
        private DateTimePicker dtDispensedDate;
        private GroupBox grpExistingRec;
        private Label label33;
        private Button btnExitingRecClose;
        private DataGridView grdExitingPharDisp;
        private Button btnView;
        private Button btnNew;
        private Panel panel3;
        private GroupBox groupBox2;
        private Label label17;
        private Label label18;
        private Label lblReturnLstRegimen;
        private Label lblReturnIQNumber;
        private Label lblReturnLstDispDate;
        private Label lblReturnPatName;
        private Label label36;
        private Label label37;
        private Label lblReturnStoreName;
        private Label label39;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label lblReturnProgram;
        private Label label15;
        private Label lblReturnDispensedDate;
        private Label label14;
        private DateTimePicker dtpReturnDate;
        private Panel panel4;
        private ListBox lstSearch;
        private TextBox txtBatchNo;
        private TextBox txtItemName;
        private TextBox txtExpirydate;
        private GroupBox grpHivCareTrtPharmacyField;
        private Label label27;
        private Button btnHIVCareTrtPharFld;
        private DateTimePicker NxtAppDate;
        private ComboBox cmbReason;
        private ComboBox cmbRegimenLine;
        private ComboBox cmbProvider;
        private ComboBox cmdPeriodTaken;
        private Label label42;
        private Label label41;
        private GroupBox groupBox4;
        private TextBox txtBSA;
        private TextBox txtHeight;
        private TextBox txtWeight;
        private Label label38;
        private Label label35;
        private Label label34;
        private Label label26;
        private Label label40;
        private Label label44;
        private Label label43;
        private Label label8;
        private Button btnART;
        private Label label46;
        private Button btnClear;
        private Button btnPatientClinicalSummary;
        private ComboBox cmbService;
        private Label label30;
        private Button btnPharmacyNotes;
        private TextBox lblIQNumber;
        private Button btncopy;
        private Button cmdPrintPrescription;
        private Panel pnlGrdDrugDispense;
        private ComboBox cmbGrdDrugDispense;
        private ComboBox cmbGrdDrugDispenseFreq;
        private DateTimePicker dtRefillApp;
        private CheckBox chkPharmacyRefill;
        private GroupBox grpBoxLastDispense;
        private Label lblDispenseLstRegimen;
        private Label label10;
        private Label label11;
        private Label lbllastDisdate;
        private Button btnPrintLabel;
        //private int IntProcess;
        private int thePatientId;
        private int theOrderId;
        private int theReturnOrderId;
        private DateTime theDOB;
        private int theFunded;
        private int theItemId;
        private int theDispensingUnit;
        private int theBatchId;
        private int theAvailQty;
        private int theStrength;
        private int theItemType;
        private int theProphylaxis;
        private Decimal theCostPrice;
        private Decimal theMargin;
        private Decimal theBillAmt;
        private Decimal theSellingPrice;
        private Decimal theConfigSellingPrice;
        private Decimal thePrecribeAmt;
        private double qtyAvailableInBatch;
        private int theCurrentRow;
        private int theprevbatchId;

        public PatientDrugDispenseForm()
        {
            this.InitializeComponent();
            this.grdDrugDispense.CellPainting += new DataGridViewCellPaintingEventHandler(this.grdDrugDispense_CellPainting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientDrugDispenseForm));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grpHivCareTrtPharmacyField = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btnHIVCareTrtPharFld = new System.Windows.Forms.Button();
            this.NxtAppDate = new System.Windows.Forms.DateTimePicker();
            this.cmbReason = new System.Windows.Forms.ComboBox();
            this.cmbRegimenLine = new System.Windows.Forms.ComboBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmdPeriodTaken = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBSA = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbprogram = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lblLstRegimen = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblDispDate = new System.Windows.Forms.Label();
            this.grpExistingRec = new System.Windows.Forms.GroupBox();
            this.grdExitingPharDisp = new System.Windows.Forms.DataGridView();
            this.btnExitingRecClose = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.btnPharmacyNotes = new System.Windows.Forms.Button();
            this.btnPatientClinicalSummary = new System.Windows.Forms.Button();
            this.lstSearch = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnART = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btncopy = new System.Windows.Forms.Button();
            this.lblIQNumber = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.dtDispensedDate = new System.Windows.Forms.DateTimePicker();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.btnDispenseSubmit = new System.Windows.Forms.Button();
            this.lblPayAmount = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtExpirydate = new System.Windows.Forms.TextBox();
            this.pnlGrdDrugDispense = new System.Windows.Forms.Panel();
            this.cmbGrdDrugDispenseFreq = new System.Windows.Forms.ComboBox();
            this.cmbGrdDrugDispense = new System.Windows.Forms.ComboBox();
            this.grdDrugDispense = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.chkPharmacyRefill = new System.Windows.Forms.CheckBox();
            this.dtRefillApp = new System.Windows.Forms.DateTimePicker();
            this.grpBoxLastDispense = new System.Windows.Forms.GroupBox();
            this.lblDispenseLstRegimen = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbllastDisdate = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.grdResultView = new System.Windows.Forms.DataGridView();
            this.btnFind = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSex = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientIdentification = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.tabDispense = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnProgram = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblReturnDispensedDate = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblReturnLstRegimen = new System.Windows.Forms.Label();
            this.lblReturnIQNumber = new System.Windows.Forms.Label();
            this.lblReturnLstDispDate = new System.Windows.Forms.Label();
            this.lblReturnPatName = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblReturnStoreName = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.grdReturnOrder = new System.Windows.Forms.DataGridView();
            this.grdReturnDetail = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrintLabel = new System.Windows.Forms.Button();
            this.cmdPrintPrescription = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2.SuspendLayout();
            this.grpHivCareTrtPharmacyField.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpExistingRec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExitingPharDisp)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlGrdDrugDispense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDrugDispense)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpBoxLastDispense.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultView)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabDispense.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnDetail)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.grpHivCareTrtPharmacyField);
            this.tabPage2.Controls.Add(this.grpExistingRec);
            this.tabPage2.Controls.Add(this.btnPharmacyNotes);
            this.tabPage2.Controls.Add(this.btnPatientClinicalSummary);
            this.tabPage2.Controls.Add(this.lstSearch);
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.btnART);
            this.tabPage2.Controls.Add(this.btnNew);
            this.tabPage2.Controls.Add(this.btnView);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.lblStoreName);
            this.tabPage2.Controls.Add(this.label32);
            this.tabPage2.Controls.Add(this.btnDispenseSubmit);
            this.tabPage2.Controls.Add(this.lblPayAmount);
            this.tabPage2.Controls.Add(this.label28);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.pnlGrdDrugDispense);
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Controls.Add(this.chkPharmacyRefill);
            this.tabPage2.Controls.Add(this.dtRefillApp);
            this.tabPage2.Controls.Add(this.grpBoxLastDispense);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(848, 452);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "pnlSubPanel";
            this.tabPage2.Text = "Dispense";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grpHivCareTrtPharmacyField
            // 
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label44);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label43);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label40);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.btnHIVCareTrtPharFld);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.NxtAppDate);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.cmbReason);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.cmbRegimenLine);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.cmbProvider);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.cmdPeriodTaken);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label42);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label41);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.groupBox4);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label27);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.cmbprogram);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label23);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.lblLstRegimen);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label22);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.label24);
            this.grpHivCareTrtPharmacyField.Controls.Add(this.lblDispDate);
            this.grpHivCareTrtPharmacyField.Location = new System.Drawing.Point(131, 225);
            this.grpHivCareTrtPharmacyField.Name = "grpHivCareTrtPharmacyField";
            this.grpHivCareTrtPharmacyField.Size = new System.Drawing.Size(595, 227);
            this.grpHivCareTrtPharmacyField.TabIndex = 92;
            this.grpHivCareTrtPharmacyField.TabStop = false;
            this.grpHivCareTrtPharmacyField.Visible = false;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(440, 163);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(47, 13);
            this.label44.TabIndex = 80;
            this.label44.Text = "Reason:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(203, 164);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(126, 13);
            this.label43.TabIndex = 79;
            this.label43.Text = "Next Appointement Date:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(216, 118);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(74, 13);
            this.label40.TabIndex = 78;
            this.label40.Text = "Period Taken:";
            // 
            // btnHIVCareTrtPharFld
            // 
            this.btnHIVCareTrtPharFld.Location = new System.Drawing.Point(506, 195);
            this.btnHIVCareTrtPharFld.Name = "btnHIVCareTrtPharFld";
            this.btnHIVCareTrtPharFld.Size = new System.Drawing.Size(75, 23);
            this.btnHIVCareTrtPharFld.TabIndex = 77;
            this.btnHIVCareTrtPharFld.Text = "Close";
            this.btnHIVCareTrtPharFld.UseVisualStyleBackColor = true;
            this.btnHIVCareTrtPharFld.Click += new System.EventHandler(this.btnHIVCareTrtPharFld_Click);
            // 
            // NxtAppDate
            // 
            this.NxtAppDate.CustomFormat = "dd-MMM-yyyy";
            this.NxtAppDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NxtAppDate.Location = new System.Drawing.Point(335, 163);
            this.NxtAppDate.Name = "NxtAppDate";
            this.NxtAppDate.Size = new System.Drawing.Size(99, 20);
            this.NxtAppDate.TabIndex = 76;
            this.NxtAppDate.Tag = "txtText";
            this.NxtAppDate.Value = new System.DateTime(2013, 2, 19, 10, 48, 0, 0);
            this.NxtAppDate.Enter += new System.EventHandler(this.NxtAppDate_Enter);
            // 
            // cmbReason
            // 
            this.cmbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReason.FormattingEnabled = true;
            this.cmbReason.Location = new System.Drawing.Point(490, 160);
            this.cmbReason.Name = "cmbReason";
            this.cmbReason.Size = new System.Drawing.Size(99, 21);
            this.cmbReason.TabIndex = 75;
            this.cmbReason.Tag = "ddlDropDownList";
            // 
            // cmbRegimenLine
            // 
            this.cmbRegimenLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegimenLine.FormattingEnabled = true;
            this.cmbRegimenLine.Location = new System.Drawing.Point(111, 160);
            this.cmbRegimenLine.Name = "cmbRegimenLine";
            this.cmbRegimenLine.Size = new System.Drawing.Size(87, 21);
            this.cmbRegimenLine.TabIndex = 74;
            this.cmbRegimenLine.Tag = "ddlDropDownList";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(490, 118);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(99, 21);
            this.cmbProvider.TabIndex = 73;
            this.cmbProvider.Tag = "ddlDropDownList";
            // 
            // cmdPeriodTaken
            // 
            this.cmdPeriodTaken.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdPeriodTaken.FormattingEnabled = true;
            this.cmdPeriodTaken.Location = new System.Drawing.Point(296, 117);
            this.cmdPeriodTaken.Name = "cmdPeriodTaken";
            this.cmdPeriodTaken.Size = new System.Drawing.Size(87, 21);
            this.cmdPeriodTaken.TabIndex = 72;
            this.cmdPeriodTaken.Tag = "ddlDropDownList";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(17, 163);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(79, 13);
            this.label42.TabIndex = 69;
            this.label42.Tag = "lblLabelRequired";
            this.label42.Text = "*Regimen Line:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(406, 118);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(79, 13);
            this.label41.TabIndex = 68;
            this.label41.Tag = "lblLabelRequired";
            this.label41.Text = "*Drug Provider:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBSA);
            this.groupBox4.Controls.Add(this.txtHeight);
            this.groupBox4.Controls.Add(this.txtWeight);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.label35);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Location = new System.Drawing.Point(19, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(553, 44);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            // 
            // txtBSA
            // 
            this.txtBSA.Enabled = false;
            this.txtBSA.Location = new System.Drawing.Point(424, 16);
            this.txtBSA.MaxLength = 10;
            this.txtBSA.Name = "txtBSA";
            this.txtBSA.Size = new System.Drawing.Size(80, 20);
            this.txtBSA.TabIndex = 67;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(233, 13);
            this.txtHeight.MaxLength = 10;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(80, 20);
            this.txtHeight.TabIndex = 66;
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHeight_KeyPress);
            this.txtHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHeight_KeyUp);
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(56, 16);
            this.txtWeight.MaxLength = 10;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(80, 20);
            this.txtWeight.TabIndex = 65;
            this.txtWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWeight_KeyPress);
            this.txtWeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtWeight_KeyUp);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(516, 15);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 13);
            this.label38.TabIndex = 64;
            this.label38.Tag = "lblLabel";
            this.label38.Text = "M^2:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(387, 15);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(31, 13);
            this.label35.TabIndex = 63;
            this.label35.Tag = "lblLabel";
            this.label35.Text = "BSA:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(181, 12);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(41, 13);
            this.label34.TabIndex = 62;
            this.label34.Tag = "lblLabel";
            this.label34.Text = "Height:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 13);
            this.label26.TabIndex = 61;
            this.label26.Tag = "lblLabel";
            this.label26.Text = "Weight:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(-1, 10);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(717, 20);
            this.label27.TabIndex = 1;
            this.label27.Text = "HIV Care and Treatement Pharmacy Field";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbprogram
            // 
            this.cmbprogram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbprogram.FormattingEnabled = true;
            this.cmbprogram.Location = new System.Drawing.Point(111, 117);
            this.cmbprogram.Name = "cmbprogram";
            this.cmbprogram.Size = new System.Drawing.Size(98, 21);
            this.cmbprogram.TabIndex = 65;
            this.cmbprogram.Tag = "ddlDropDownList";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 118);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(104, 13);
            this.label23.TabIndex = 64;
            this.label23.Tag = "lblLabelRequired";
            this.label23.Text = "*Treatment Program:";
            // 
            // lblLstRegimen
            // 
            this.lblLstRegimen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLstRegimen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLstRegimen.Location = new System.Drawing.Point(400, 47);
            this.lblLstRegimen.Name = "lblLstRegimen";
            this.lblLstRegimen.Size = new System.Drawing.Size(172, 18);
            this.lblLstRegimen.TabIndex = 58;
            this.lblLstRegimen.Tag = "lblLabel";
            this.lblLstRegimen.Text = "LastRegimen";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(269, 47);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(128, 13);
            this.label22.TabIndex = 53;
            this.label22.Tag = "lblLabel";
            this.label22.Text = "Last Regimen Dispensed:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 47);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 13);
            this.label24.TabIndex = 60;
            this.label24.Tag = "lblLabel";
            this.label24.Text = "Last Dispensed Date:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDispDate
            // 
            this.lblDispDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDispDate.AutoSize = true;
            this.lblDispDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDispDate.Location = new System.Drawing.Point(138, 47);
            this.lblDispDate.Name = "lblDispDate";
            this.lblDispDate.Size = new System.Drawing.Size(71, 13);
            this.lblDispDate.TabIndex = 61;
            this.lblDispDate.Tag = "lblLabel";
            this.lblDispDate.Text = "LastDispDate";
            // 
            // grpExistingRec
            // 
            this.grpExistingRec.Controls.Add(this.grdExitingPharDisp);
            this.grpExistingRec.Controls.Add(this.btnExitingRecClose);
            this.grpExistingRec.Controls.Add(this.label33);
            this.grpExistingRec.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grpExistingRec.Location = new System.Drawing.Point(50, 261);
            this.grpExistingRec.Name = "grpExistingRec";
            this.grpExistingRec.Size = new System.Drawing.Size(640, 253);
            this.grpExistingRec.TabIndex = 66;
            this.grpExistingRec.TabStop = false;
            // 
            // grdExitingPharDisp
            // 
            this.grdExitingPharDisp.AllowUserToAddRows = false;
            this.grdExitingPharDisp.AllowUserToDeleteRows = false;
            this.grdExitingPharDisp.AllowUserToResizeColumns = false;
            this.grdExitingPharDisp.AllowUserToResizeRows = false;
            this.grdExitingPharDisp.BackgroundColor = System.Drawing.Color.White;
            this.grdExitingPharDisp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdExitingPharDisp.Location = new System.Drawing.Point(5, 33);
            this.grdExitingPharDisp.Name = "grdExitingPharDisp";
            this.grdExitingPharDisp.ReadOnly = true;
            this.grdExitingPharDisp.Size = new System.Drawing.Size(521, 190);
            this.grdExitingPharDisp.TabIndex = 1;
            this.grdExitingPharDisp.Tag = "dgwDataGridView";
            this.grdExitingPharDisp.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdExitingPharDisp_CellDoubleClick);
            // 
            // btnExitingRecClose
            // 
            this.btnExitingRecClose.BackColor = System.Drawing.SystemColors.Window;
            this.btnExitingRecClose.Location = new System.Drawing.Point(537, 200);
            this.btnExitingRecClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnExitingRecClose.Name = "btnExitingRecClose";
            this.btnExitingRecClose.Size = new System.Drawing.Size(80, 25);
            this.btnExitingRecClose.TabIndex = 46;
            this.btnExitingRecClose.Tag = "btnSingleText";
            this.btnExitingRecClose.Text = "&Close";
            this.btnExitingRecClose.UseVisualStyleBackColor = false;
            this.btnExitingRecClose.Click += new System.EventHandler(this.btnExitingRecClose_Click);
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(2, 9);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(717, 20);
            this.label33.TabIndex = 0;
            this.label33.Text = "View Existing Record";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPharmacyNotes
            // 
            this.btnPharmacyNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPharmacyNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPharmacyNotes.ForeColor = System.Drawing.Color.Blue;
            this.btnPharmacyNotes.Location = new System.Drawing.Point(522, 2);
            this.btnPharmacyNotes.Name = "btnPharmacyNotes";
            this.btnPharmacyNotes.Size = new System.Drawing.Size(120, 22);
            this.btnPharmacyNotes.TabIndex = 98;
            this.btnPharmacyNotes.Text = "Prescription Notes>>";
            this.btnPharmacyNotes.UseVisualStyleBackColor = true;
            this.btnPharmacyNotes.Click += new System.EventHandler(this.btnPharmacyNotes_Click);
            // 
            // btnPatientClinicalSummary
            // 
            this.btnPatientClinicalSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientClinicalSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientClinicalSummary.ForeColor = System.Drawing.Color.Blue;
            this.btnPatientClinicalSummary.Location = new System.Drawing.Point(363, 2);
            this.btnPatientClinicalSummary.Name = "btnPatientClinicalSummary";
            this.btnPatientClinicalSummary.Size = new System.Drawing.Size(158, 22);
            this.btnPatientClinicalSummary.TabIndex = 96;
            this.btnPatientClinicalSummary.Text = "Patient Clinical Summary>>";
            this.btnPatientClinicalSummary.UseVisualStyleBackColor = true;
            this.btnPatientClinicalSummary.Click += new System.EventHandler(this.btnPatientClinicalSummary_Click);
            // 
            // lstSearch
            // 
            this.lstSearch.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSearch.FormattingEnabled = true;
            this.lstSearch.ItemHeight = 14;
            this.lstSearch.Location = new System.Drawing.Point(37, 228);
            this.lstSearch.Name = "lstSearch";
            this.lstSearch.Size = new System.Drawing.Size(55, 4);
            this.lstSearch.TabIndex = 90;
            this.lstSearch.Tag = "";
            this.lstSearch.DoubleClick += new System.EventHandler(this.lstSearch_DoubleClick);
            this.lstSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSearch_KeyUp);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Window;
            this.btnClear.Location = new System.Drawing.Point(411, 152);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 25);
            this.btnClear.TabIndex = 95;
            this.btnClear.Tag = "btnSingleText";
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnART
            // 
            this.btnART.Enabled = false;
            this.btnART.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnART.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnART.ForeColor = System.Drawing.Color.Blue;
            this.btnART.Location = new System.Drawing.Point(245, 2);
            this.btnART.Name = "btnART";
            this.btnART.Size = new System.Drawing.Size(117, 22);
            this.btnART.TabIndex = 93;
            this.btnART.Text = "ARV Treatment  >>";
            this.btnART.UseVisualStyleBackColor = true;
            this.btnART.Click += new System.EventHandler(this.btnART_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.Window;
            this.btnNew.Location = new System.Drawing.Point(0, 1);
            this.btnNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(120, 25);
            this.btnNew.TabIndex = 70;
            this.btnNew.Tag = "btnH25W80Flexi";
            this.btnNew.Text = "New Order";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.SystemColors.Window;
            this.btnView.Location = new System.Drawing.Point(119, 1);
            this.btnView.Margin = new System.Windows.Forms.Padding(0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(120, 25);
            this.btnView.TabIndex = 69;
            this.btnView.Tag = "btnH25W80Flexi";
            this.btnView.Text = "View Existing";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 40);
            this.panel1.TabIndex = 63;
            this.panel1.Tag = "pnlPanel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btncopy);
            this.groupBox1.Controls.Add(this.lblIQNumber);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.dtDispensedDate);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.lblPatientName);
            this.groupBox1.Location = new System.Drawing.Point(4, -5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(836, 44);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "grpGroupBox";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btncopy
            // 
            this.btncopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btncopy.Location = new System.Drawing.Point(416, 13);
            this.btncopy.Name = "btncopy";
            this.btncopy.Size = new System.Drawing.Size(153, 23);
            this.btncopy.TabIndex = 70;
            this.btncopy.TabStop = false;
            this.btncopy.Tag = "lblLabel";
            this.btncopy.Text = "Open Patient Home Page";
            this.btncopy.UseVisualStyleBackColor = true;
            this.btncopy.Click += new System.EventHandler(this.btncopy_Click);
            // 
            // lblIQNumber
            // 
            this.lblIQNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblIQNumber.Location = new System.Drawing.Point(281, 18);
            this.lblIQNumber.Name = "lblIQNumber";
            this.lblIQNumber.ReadOnly = true;
            this.lblIQNumber.Size = new System.Drawing.Size(123, 13);
            this.lblIQNumber.TabIndex = 69;
            this.lblIQNumber.TabStop = false;
            this.lblIQNumber.Tag = "lblLabel";
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(575, 15);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(86, 13);
            this.label46.TabIndex = 68;
            this.label46.Tag = "lblLabel";
            this.label46.Text = "Dispensed Date:";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtDispensedDate
            // 
            this.dtDispensedDate.CustomFormat = "dd-MMM-yyyy";
            this.dtDispensedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDispensedDate.Location = new System.Drawing.Point(670, 13);
            this.dtDispensedDate.Name = "dtDispensedDate";
            this.dtDispensedDate.Size = new System.Drawing.Size(153, 20);
            this.dtDispensedDate.TabIndex = 67;
            this.dtDispensedDate.Tag = "txtText";
            this.dtDispensedDate.ValueChanged += new System.EventHandler(this.dtDispensedDate_ValueChanged);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(191, 18);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 13);
            this.label31.TabIndex = 59;
            this.label31.Tag = "lblLabel";
            this.label31.Text = "IQCare Number:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Click += new System.EventHandler(this.label31_Click);
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(8, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(74, 13);
            this.label29.TabIndex = 55;
            this.label29.Tag = "lblLabel";
            this.label29.Text = "Patient Name:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(82, 16);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(62, 13);
            this.lblPatientName.TabIndex = 57;
            this.lblPatientName.Tag = "lblLabel";
            this.lblPatientName.Text = "Smith, John";
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Location = new System.Drawing.Point(694, 7);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(104, 13);
            this.lblStoreName.TabIndex = 56;
            this.lblStoreName.Tag = "lblLabel";
            this.lblStoreName.Text = "Central Store Limited";
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(660, 25);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 13);
            this.label32.TabIndex = 54;
            this.label32.Tag = "lblLabel";
            this.label32.Text = "Store:";
            // 
            // btnDispenseSubmit
            // 
            this.btnDispenseSubmit.BackColor = System.Drawing.SystemColors.Window;
            this.btnDispenseSubmit.Location = new System.Drawing.Point(350, 152);
            this.btnDispenseSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnDispenseSubmit.Name = "btnDispenseSubmit";
            this.btnDispenseSubmit.Size = new System.Drawing.Size(60, 25);
            this.btnDispenseSubmit.TabIndex = 46;
            this.btnDispenseSubmit.Tag = "btnSingleText";
            this.btnDispenseSubmit.Text = "Submit";
            this.btnDispenseSubmit.UseVisualStyleBackColor = false;
            this.btnDispenseSubmit.Click += new System.EventHandler(this.btnDispenseSubmit_Click);
            // 
            // lblPayAmount
            // 
            this.lblPayAmount.AutoSize = true;
            this.lblPayAmount.Location = new System.Drawing.Point(770, 434);
            this.lblPayAmount.Name = "lblPayAmount";
            this.lblPayAmount.Size = new System.Drawing.Size(28, 13);
            this.lblPayAmount.TabIndex = 65;
            this.lblPayAmount.Tag = "lblLabelRequired";
            this.lblPayAmount.Text = "0.00";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(642, 434);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(114, 13);
            this.label28.TabIndex = 64;
            this.label28.Tag = "lblLabel";
            this.label28.Text = "Total Payable Amount:";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.txtBatchNo);
            this.panel4.Controls.Add(this.txtItemName);
            this.panel4.Controls.Add(this.txtExpirydate);
            this.panel4.Location = new System.Drawing.Point(5, 96);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(839, 46);
            this.panel4.TabIndex = 71;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 97;
            this.label8.Text = "Item Name:";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Enabled = false;
            this.txtBatchNo.Location = new System.Drawing.Point(81, 52);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(141, 20);
            this.txtBatchNo.TabIndex = 88;
            this.txtBatchNo.Tag = "txtTextBox";
            this.txtBatchNo.Visible = false;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(81, 15);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(755, 20);
            this.txtItemName.TabIndex = 82;
            this.txtItemName.Tag = "txtTextBox";
            this.txtItemName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtItemName_KeyUp);
            // 
            // txtExpirydate
            // 
            this.txtExpirydate.Enabled = false;
            this.txtExpirydate.Location = new System.Drawing.Point(111, 77);
            this.txtExpirydate.Name = "txtExpirydate";
            this.txtExpirydate.Size = new System.Drawing.Size(175, 20);
            this.txtExpirydate.TabIndex = 89;
            this.txtExpirydate.Tag = "txtTextBox";
            this.txtExpirydate.Visible = false;
            // 
            // pnlGrdDrugDispense
            // 
            this.pnlGrdDrugDispense.Controls.Add(this.cmbGrdDrugDispenseFreq);
            this.pnlGrdDrugDispense.Controls.Add(this.cmbGrdDrugDispense);
            this.pnlGrdDrugDispense.Controls.Add(this.grdDrugDispense);
            this.pnlGrdDrugDispense.Location = new System.Drawing.Point(0, 188);
            this.pnlGrdDrugDispense.Name = "pnlGrdDrugDispense";
            this.pnlGrdDrugDispense.Size = new System.Drawing.Size(849, 241);
            this.pnlGrdDrugDispense.TabIndex = 99;
            // 
            // cmbGrdDrugDispenseFreq
            // 
            this.cmbGrdDrugDispenseFreq.FormattingEnabled = true;
            this.cmbGrdDrugDispenseFreq.Location = new System.Drawing.Point(5, 38);
            this.cmbGrdDrugDispenseFreq.Name = "cmbGrdDrugDispenseFreq";
            this.cmbGrdDrugDispenseFreq.Size = new System.Drawing.Size(89, 21);
            this.cmbGrdDrugDispenseFreq.TabIndex = 48;
            this.cmbGrdDrugDispenseFreq.Visible = false;
            this.cmbGrdDrugDispenseFreq.SelectedIndexChanged += new System.EventHandler(this.cmbGrdDrugDispenseFreq_SelectedIndexChanged);
            // 
            // cmbGrdDrugDispense
            // 
            this.cmbGrdDrugDispense.FormattingEnabled = true;
            this.cmbGrdDrugDispense.Location = new System.Drawing.Point(3, 9);
            this.cmbGrdDrugDispense.Name = "cmbGrdDrugDispense";
            this.cmbGrdDrugDispense.Size = new System.Drawing.Size(89, 21);
            this.cmbGrdDrugDispense.TabIndex = 47;
            this.cmbGrdDrugDispense.Visible = false;
            this.cmbGrdDrugDispense.SelectedIndexChanged += new System.EventHandler(this.cmbGrdDrugDispense_SelectedIndexChanged);
            // 
            // grdDrugDispense
            // 
            this.grdDrugDispense.AllowUserToAddRows = false;
            this.grdDrugDispense.AllowUserToResizeColumns = false;
            this.grdDrugDispense.AllowUserToResizeRows = false;
            this.grdDrugDispense.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdDrugDispense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDrugDispense.Location = new System.Drawing.Point(0, 0);
            this.grdDrugDispense.Name = "grdDrugDispense";
            this.grdDrugDispense.Size = new System.Drawing.Size(849, 244);
            this.grdDrugDispense.TabIndex = 46;
            this.grdDrugDispense.Tag = "dgwDataGridView";
            this.grdDrugDispense.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrugDispense_CellClick);
            this.grdDrugDispense.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrugDispense_CellContentClick);
            this.grdDrugDispense.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrugDispense_CellDoubleClick);
            this.grdDrugDispense.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrugDispense_CellValueChanged);
            this.grdDrugDispense.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdDrugDispense_EditingControlShowing);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.31991F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.68009F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 749F));
            this.tableLayoutPanel2.Controls.Add(this.label16, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(21, 395);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(834, 38);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Tag = "pnlSubPanelSCM";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-34, 10);
            this.label16.Name = "label16";
            this.label16.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label16.Size = new System.Drawing.Size(109, 17);
            this.label16.TabIndex = 4;
            this.label16.Tag = "lblLabelSCM";
            this.label16.Text = "Last Dispensed Date:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(-17, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1, 20);
            this.textBox2.TabIndex = 6;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(83, 9);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(179, 20);
            this.dateTimePicker2.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 10);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label13.Size = new System.Drawing.Size(1, 17);
            this.label13.TabIndex = 0;
            this.label13.Tag = "lblLabelSCM";
            this.label13.Text = "Last Regimen Dispensed:";
            // 
            // chkPharmacyRefill
            // 
            this.chkPharmacyRefill.AutoSize = true;
            this.chkPharmacyRefill.Location = new System.Drawing.Point(581, 155);
            this.chkPharmacyRefill.Name = "chkPharmacyRefill";
            this.chkPharmacyRefill.Size = new System.Drawing.Size(131, 17);
            this.chkPharmacyRefill.TabIndex = 102;
            this.chkPharmacyRefill.Text = "Pharmacy Refill Date :";
            this.chkPharmacyRefill.UseVisualStyleBackColor = true;
            this.chkPharmacyRefill.CheckedChanged += new System.EventHandler(this.chkPharmacyRefill_CheckedChanged);
            // 
            // dtRefillApp
            // 
            this.dtRefillApp.CustomFormat = "dd-MMM-yyyy";
            this.dtRefillApp.Enabled = false;
            this.dtRefillApp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRefillApp.Location = new System.Drawing.Point(710, 153);
            this.dtRefillApp.Name = "dtRefillApp";
            this.dtRefillApp.Size = new System.Drawing.Size(125, 20);
            this.dtRefillApp.TabIndex = 100;
            this.dtRefillApp.Tag = "txtText";
            this.dtRefillApp.Enter += new System.EventHandler(this.dtRefillApp_Enter);
            // 
            // grpBoxLastDispense
            // 
            this.grpBoxLastDispense.Controls.Add(this.lblDispenseLstRegimen);
            this.grpBoxLastDispense.Controls.Add(this.label10);
            this.grpBoxLastDispense.Controls.Add(this.label11);
            this.grpBoxLastDispense.Controls.Add(this.lbllastDisdate);
            this.grpBoxLastDispense.Location = new System.Drawing.Point(7, 64);
            this.grpBoxLastDispense.Margin = new System.Windows.Forms.Padding(0);
            this.grpBoxLastDispense.Name = "grpBoxLastDispense";
            this.grpBoxLastDispense.Padding = new System.Windows.Forms.Padding(0);
            this.grpBoxLastDispense.Size = new System.Drawing.Size(837, 30);
            this.grpBoxLastDispense.TabIndex = 103;
            this.grpBoxLastDispense.TabStop = false;
            this.grpBoxLastDispense.Tag = "";
            // 
            // lblDispenseLstRegimen
            // 
            this.lblDispenseLstRegimen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDispenseLstRegimen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDispenseLstRegimen.Location = new System.Drawing.Point(395, 10);
            this.lblDispenseLstRegimen.Name = "lblDispenseLstRegimen";
            this.lblDispenseLstRegimen.Size = new System.Drawing.Size(172, 13);
            this.lblDispenseLstRegimen.TabIndex = 63;
            this.lblDispenseLstRegimen.Tag = "lblLabel";
            this.lblDispenseLstRegimen.Text = "LastRegimen";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(261, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 13);
            this.label10.TabIndex = 62;
            this.label10.Tag = "lblLabel";
            this.label10.Text = "Last Regimen Dispensed:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 64;
            this.label11.Tag = "lblLabel";
            this.label11.Text = "Last Dispensed Date:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbllastDisdate
            // 
            this.lbllastDisdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbllastDisdate.AutoSize = true;
            this.lbllastDisdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllastDisdate.Location = new System.Drawing.Point(130, 11);
            this.lbllastDisdate.Name = "lbllastDisdate";
            this.lbllastDisdate.Size = new System.Drawing.Size(71, 13);
            this.lbllastDisdate.TabIndex = 65;
            this.lbllastDisdate.Tag = "lblLabel";
            this.lbllastDisdate.Text = "LastDispDate";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.grdResultView);
            this.tabPage1.Controls.Add(this.btnFind);
            this.tabPage1.Controls.Add(this.flowLayoutPanel3);
            this.tabPage1.Controls.Add(this.flowLayoutPanel2);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(848, 452);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "pnlSubPanel";
            this.tabPage1.Text = "Find Patient ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 48;
            this.label7.Tag = "lblLabel";
            this.label7.Text = "Search Patient Result";
            // 
            // grdResultView
            // 
            this.grdResultView.AllowUserToAddRows = false;
            this.grdResultView.AllowUserToDeleteRows = false;
            this.grdResultView.AllowUserToResizeColumns = false;
            this.grdResultView.AllowUserToResizeRows = false;
            this.grdResultView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResultView.Location = new System.Drawing.Point(-3, 133);
            this.grdResultView.Name = "grdResultView";
            this.grdResultView.ReadOnly = true;
            this.grdResultView.RowHeadersWidth = 60;
            this.grdResultView.Size = new System.Drawing.Size(849, 332);
            this.grdResultView.TabIndex = 47;
            this.grdResultView.Tag = "dgwDataGridView";
            this.grdResultView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResultView_CellContentClick);
            this.grdResultView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdResultView_CellMouseDoubleClick);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFind.Location = new System.Drawing.Point(387, 107);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(60, 25);
            this.btnFind.TabIndex = 46;
            this.btnFind.Tag = "btnSingleText";
            this.btnFind.Text = "&Find";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel3.Controls.Add(this.label5);
            this.flowLayoutPanel3.Controls.Add(this.dtpDOB);
            this.flowLayoutPanel3.Controls.Add(this.label6);
            this.flowLayoutPanel3.Controls.Add(this.cmbSex);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(564, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(280, 80);
            this.flowLayoutPanel3.TabIndex = 2;
            this.flowLayoutPanel3.Tag = "pnlPanel";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(6, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 0;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Date of Birth:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDOB
            // 
            this.dtpDOB.CustomFormat = "dd-MMM-yyyy";
            this.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOB.Location = new System.Drawing.Point(92, 6);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(170, 20);
            this.dtpDOB.TabIndex = 4;
            this.dtpDOB.Tag = "txtTextBox";
            this.dtpDOB.Enter += new System.EventHandler(this.dtpDOB_Enter);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(6, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 2;
            this.label6.Tag = "lblLabel";
            this.label6.Text = "Sex:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSex
            // 
            this.cmbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.Location = new System.Drawing.Point(92, 32);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.Size = new System.Drawing.Size(170, 21);
            this.cmbSex.TabIndex = 3;
            this.cmbSex.Tag = "ddlDropDownList";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.txtLastName);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.txtFirstName);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(283, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(280, 80);
            this.flowLayoutPanel2.TabIndex = 1;
            this.flowLayoutPanel2.Tag = "pnlPanel";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Last Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(92, 6);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(170, 20);
            this.txtLastName.TabIndex = 1;
            this.txtLastName.Tag = "txtTextBox";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 2;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "First Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(92, 32);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(170, 20);
            this.txtFirstName.TabIndex = 3;
            this.txtFirstName.Tag = "txtTextBox";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txtPatientIdentification);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.cmbFacility);
            this.flowLayoutPanel1.Controls.Add(this.label30);
            this.flowLayoutPanel1.Controls.Add(this.cmbService);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 80);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Tag = "pnlPanel";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Patient Identification:";
            // 
            // txtPatientIdentification
            // 
            this.txtPatientIdentification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientIdentification.Location = new System.Drawing.Point(118, 6);
            this.txtPatientIdentification.Name = "txtPatientIdentification";
            this.txtPatientIdentification.Size = new System.Drawing.Size(145, 20);
            this.txtPatientIdentification.TabIndex = 1;
            this.txtPatientIdentification.Tag = "txtTextBox";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 2;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Facility/Satellite:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFacility
            // 
            this.cmbFacility.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(118, 32);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(145, 21);
            this.cmbFacility.TabIndex = 3;
            this.cmbFacility.Tag = "ddlDropDownList";
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.Location = new System.Drawing.Point(6, 63);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(106, 13);
            this.label30.TabIndex = 5;
            this.label30.Tag = "lblLabel";
            this.label30.Text = "Service:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbService
            // 
            this.cmbService.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbService.FormattingEnabled = true;
            this.cmbService.Location = new System.Drawing.Point(118, 59);
            this.cmbService.Name = "cmbService";
            this.cmbService.Size = new System.Drawing.Size(145, 21);
            this.cmbService.TabIndex = 4;
            this.cmbService.Tag = "ddlDropDownList";
            // 
            // tabDispense
            // 
            this.tabDispense.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabDispense.Controls.Add(this.tabPage1);
            this.tabDispense.Controls.Add(this.tabPage2);
            this.tabDispense.Controls.Add(this.tabPage4);
            this.tabDispense.ItemSize = new System.Drawing.Size(100, 30);
            this.tabDispense.Location = new System.Drawing.Point(0, 1);
            this.tabDispense.Multiline = true;
            this.tabDispense.Name = "tabDispense";
            this.tabDispense.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabDispense.RightToLeftLayout = true;
            this.tabDispense.SelectedIndex = 0;
            this.tabDispense.Size = new System.Drawing.Size(856, 490);
            this.tabDispense.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabDispense.TabIndex = 0;
            this.tabDispense.Tag = "pnlPanel";
            this.tabDispense.SelectedIndexChanged += new System.EventHandler(this.tabDispense_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.dtpReturnDate);
            this.tabPage4.Controls.Add(this.lblReturnProgram);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.lblReturnDispensedDate);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.grdReturnOrder);
            this.tabPage4.Controls.Add(this.grdReturnDetail);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(848, 452);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Tag = "pnlSubPanel";
            this.tabPage4.Text = "Return";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(644, 204);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 69;
            this.label14.Tag = "lblLabel";
            this.label14.Text = "Return Date:";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnDate.Location = new System.Drawing.Point(715, 201);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(125, 20);
            this.dtpReturnDate.TabIndex = 68;
            this.dtpReturnDate.Tag = "txtTextBox";
            // 
            // lblReturnProgram
            // 
            this.lblReturnProgram.AutoSize = true;
            this.lblReturnProgram.Location = new System.Drawing.Point(213, 203);
            this.lblReturnProgram.Name = "lblReturnProgram";
            this.lblReturnProgram.Size = new System.Drawing.Size(0, 13);
            this.lblReturnProgram.TabIndex = 67;
            this.lblReturnProgram.Tag = "lblLabel";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(166, 203);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 66;
            this.label15.Tag = "lblLabel";
            this.label15.Text = "Program:";
            // 
            // lblReturnDispensedDate
            // 
            this.lblReturnDispensedDate.AutoSize = true;
            this.lblReturnDispensedDate.Location = new System.Drawing.Point(89, 204);
            this.lblReturnDispensedDate.Name = "lblReturnDispensedDate";
            this.lblReturnDispensedDate.Size = new System.Drawing.Size(0, 13);
            this.lblReturnDispensedDate.TabIndex = 65;
            this.lblReturnDispensedDate.Tag = "lblLabel";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.lblReturnStoreName);
            this.panel3.Controls.Add(this.label39);
            this.panel3.Location = new System.Drawing.Point(1, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(842, 62);
            this.panel3.TabIndex = 64;
            this.panel3.Tag = "pnlPanel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.lblReturnLstRegimen);
            this.groupBox2.Controls.Add(this.lblReturnIQNumber);
            this.groupBox2.Controls.Add(this.lblReturnLstDispDate);
            this.groupBox2.Controls.Add(this.lblReturnPatName);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Location = new System.Drawing.Point(4, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(836, 30);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "grpGroupBox";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(204, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 13);
            this.label17.TabIndex = 59;
            this.label17.Tag = "lblLabel";
            this.label17.Text = "IQCare Number:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(18, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 13);
            this.label18.TabIndex = 55;
            this.label18.Tag = "lblLabel";
            this.label18.Text = "Patient Name:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReturnLstRegimen
            // 
            this.lblReturnLstRegimen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReturnLstRegimen.AutoSize = true;
            this.lblReturnLstRegimen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnLstRegimen.Location = new System.Drawing.Point(756, 10);
            this.lblReturnLstRegimen.Name = "lblReturnLstRegimen";
            this.lblReturnLstRegimen.Size = new System.Drawing.Size(69, 13);
            this.lblReturnLstRegimen.TabIndex = 58;
            this.lblReturnLstRegimen.Tag = "lblLabel";
            this.lblReturnLstRegimen.Text = "LastRegimen";
            // 
            // lblReturnIQNumber
            // 
            this.lblReturnIQNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReturnIQNumber.AutoSize = true;
            this.lblReturnIQNumber.Location = new System.Drawing.Point(285, 10);
            this.lblReturnIQNumber.Name = "lblReturnIQNumber";
            this.lblReturnIQNumber.Size = new System.Drawing.Size(107, 13);
            this.lblReturnIQNumber.TabIndex = 62;
            this.lblReturnIQNumber.Tag = "lblLabel";
            this.lblReturnIQNumber.Text = "IQ-00000XXXXXXXX";
            // 
            // lblReturnLstDispDate
            // 
            this.lblReturnLstDispDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReturnLstDispDate.AutoSize = true;
            this.lblReturnLstDispDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnLstDispDate.Location = new System.Drawing.Point(538, 10);
            this.lblReturnLstDispDate.Name = "lblReturnLstDispDate";
            this.lblReturnLstDispDate.Size = new System.Drawing.Size(71, 13);
            this.lblReturnLstDispDate.TabIndex = 61;
            this.lblReturnLstDispDate.Tag = "lblLabel";
            this.lblReturnLstDispDate.Text = "LastDispDate";
            // 
            // lblReturnPatName
            // 
            this.lblReturnPatName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReturnPatName.AutoSize = true;
            this.lblReturnPatName.Location = new System.Drawing.Point(91, 10);
            this.lblReturnPatName.Name = "lblReturnPatName";
            this.lblReturnPatName.Size = new System.Drawing.Size(62, 13);
            this.lblReturnPatName.TabIndex = 57;
            this.lblReturnPatName.Tag = "lblLabel";
            this.lblReturnPatName.Text = "Smith, John";
            // 
            // label36
            // 
            this.label36.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(630, 10);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(128, 13);
            this.label36.TabIndex = 53;
            this.label36.Tag = "lblLabel";
            this.label36.Text = "Last Regimen Dispensed:";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(430, 10);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(109, 13);
            this.label37.TabIndex = 60;
            this.label37.Tag = "lblLabel";
            this.label37.Text = "Last Dispensed Date:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReturnStoreName
            // 
            this.lblReturnStoreName.AutoSize = true;
            this.lblReturnStoreName.Location = new System.Drawing.Point(414, 8);
            this.lblReturnStoreName.Name = "lblReturnStoreName";
            this.lblReturnStoreName.Size = new System.Drawing.Size(104, 13);
            this.lblReturnStoreName.TabIndex = 56;
            this.lblReturnStoreName.Tag = "lblLabel";
            this.lblReturnStoreName.Text = "Central Store Limited";
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(380, 7);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(35, 13);
            this.label39.TabIndex = 54;
            this.label39.Tag = "lblLabel";
            this.label39.Text = "Store:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(4, 204);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(86, 13);
            this.label25.TabIndex = 49;
            this.label25.Tag = "lblLabel";
            this.label25.Text = "Dispensed Date:";
            // 
            // grdReturnOrder
            // 
            this.grdReturnOrder.AllowUserToAddRows = false;
            this.grdReturnOrder.AllowUserToDeleteRows = false;
            this.grdReturnOrder.AllowUserToResizeColumns = false;
            this.grdReturnOrder.AllowUserToResizeRows = false;
            this.grdReturnOrder.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdReturnOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReturnOrder.Location = new System.Drawing.Point(-1, 66);
            this.grdReturnOrder.Name = "grdReturnOrder";
            this.grdReturnOrder.ReadOnly = true;
            this.grdReturnOrder.Size = new System.Drawing.Size(849, 131);
            this.grdReturnOrder.TabIndex = 48;
            this.grdReturnOrder.Tag = "dgwDataGridView";
            this.grdReturnOrder.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReturnOrder_CellDoubleClick);
            // 
            // grdReturnDetail
            // 
            this.grdReturnDetail.AllowUserToAddRows = false;
            this.grdReturnDetail.AllowUserToDeleteRows = false;
            this.grdReturnDetail.AllowUserToResizeColumns = false;
            this.grdReturnDetail.AllowUserToResizeRows = false;
            this.grdReturnDetail.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdReturnDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReturnDetail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdReturnDetail.Location = new System.Drawing.Point(-1, 222);
            this.grdReturnDetail.Name = "grdReturnDetail";
            this.grdReturnDetail.Size = new System.Drawing.Size(849, 243);
            this.grdReturnDetail.TabIndex = 46;
            this.grdReturnDetail.Tag = "dgwDataGridView";
            this.grdReturnDetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdReturnDetail_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Window;
            this.btnSave.Location = new System.Drawing.Point(642, 22);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 25);
            this.btnSave.TabIndex = 44;
            this.btnSave.Tag = "btnSingleTextSCM";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Window;
            this.btnCancel.Location = new System.Drawing.Point(742, 22);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 25);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Tag = "btnSingleTextSCM";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Window;
            this.button3.Location = new System.Drawing.Point(733, 22);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 25);
            this.button3.TabIndex = 44;
            this.button3.Tag = "btnSingleText";
            this.button3.Text = "&Save";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(791, 22);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 25);
            this.button4.TabIndex = 45;
            this.button4.Tag = "btnSingleText";
            this.button4.Text = "&Close";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPrintLabel);
            this.panel2.Controls.Add(this.cmdPrintPrescription);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdClose);
            this.panel2.Location = new System.Drawing.Point(0, 490);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 47);
            this.panel2.TabIndex = 59;
            this.panel2.Tag = "pnlSubPanel";
            // 
            // btnPrintLabel
            // 
            this.btnPrintLabel.Location = new System.Drawing.Point(729, 13);
            this.btnPrintLabel.Name = "btnPrintLabel";
            this.btnPrintLabel.Size = new System.Drawing.Size(99, 25);
            this.btnPrintLabel.TabIndex = 47;
            this.btnPrintLabel.Tag = "";
            this.btnPrintLabel.Text = "Print &Drug Labels";
            this.btnPrintLabel.UseVisualStyleBackColor = true;
            this.btnPrintLabel.Click += new System.EventHandler(this.btnPrintLabel_Click);
            // 
            // cmdPrintPrescription
            // 
            this.cmdPrintPrescription.BackColor = System.Drawing.SystemColors.Window;
            this.cmdPrintPrescription.Location = new System.Drawing.Point(621, 13);
            this.cmdPrintPrescription.Margin = new System.Windows.Forms.Padding(0);
            this.cmdPrintPrescription.Name = "cmdPrintPrescription";
            this.cmdPrintPrescription.Size = new System.Drawing.Size(108, 25);
            this.cmdPrintPrescription.TabIndex = 46;
            this.cmdPrintPrescription.Tag = "";
            this.cmdPrintPrescription.Text = "&Print Prescription";
            this.cmdPrintPrescription.UseVisualStyleBackColor = false;
            this.cmdPrintPrescription.Click += new System.EventHandler(this.cmdPrintPrescription_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BackColor = System.Drawing.SystemColors.Window;
            this.cmdSave.Location = new System.Drawing.Point(480, 13);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(80, 25);
            this.cmdSave.TabIndex = 44;
            this.cmdSave.Tag = "btnSingleText";
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = false;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.SystemColors.Window;
            this.cmdClose.Location = new System.Drawing.Point(549, 13);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(80, 25);
            this.cmdClose.TabIndex = 45;
            this.cmdClose.Tag = "btnSingleText";
            this.cmdClose.Text = "&Close";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Dispensing Unit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // PatientDrugDispenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1271, 656);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabDispense);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientDrugDispenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Find Patient";
            this.Load += new System.EventHandler(this.PatientDrugDispenseForm_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpHivCareTrtPharmacyField.ResumeLayout(false);
            this.grpHivCareTrtPharmacyField.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpExistingRec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExitingPharDisp)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlGrdDrugDispense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDrugDispense)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpBoxLastDispense.ResumeLayout(false);
            this.grpBoxLastDispense.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultView)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabDispense.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void Init_Form()
        {
            this.imageList1.Images.Add("pic1", Image.FromFile(GblIQCare.GetPath() + "\\printer.jpg"));
            this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = false;
            this.BindCombo();
            this.txtPatientIdentification.Text = "";
            this.txtFirstName.Text = "";
            this.txtLastName.Text = "";
            this.dtpDOB.CustomFormat = " ";
            this.cmbFacility.SelectedValue = (object)GblIQCare.AppLocationId.ToString();
            this.dtDispensedDate.Text = GblIQCare.CurrentDate;
            this.dtpReturnDate.Text = GblIQCare.CurrentDate;
            this.cmbFacility.Enabled = false;
            this.lstSearch.Visible = false;
            this.cmdSave.Visible = false;
            this.cmdPrintPrescription.Visible = false;
            this.btnPrintLabel.Visible = false;
            this.grpExistingRec.Visible = false;
            this.dtRefillApp.CustomFormat = " ";
            this.makeGridEditable = "Yes";
            this.Authentication();
        }

        private void Authentication()
        {
            if (!GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Add, GblIQCare.dtUserRight))
                this.btnSave.Enabled = false;
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Update, GblIQCare.dtUserRight))
                return;
            this.btnSave.Enabled = false;
        }

        private void BindSearchGrid(DataTable theDT)
        {
            this.grdResultView.DataSource = (object)null;
            this.grdResultView.AutoGenerateColumns = false;
            this.grdResultView.Columns.Clear();
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "Last Name";
            viewTextBoxColumn1.DataPropertyName = "lastname";
            viewTextBoxColumn1.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "First Name";
            viewTextBoxColumn2.DataPropertyName = "firstname";
            viewTextBoxColumn2.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = "IQ-Number";
            viewTextBoxColumn3.DataPropertyName = "PatientId";
            viewTextBoxColumn3.Width = 150;
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = "Sex";
            viewTextBoxColumn4.DataPropertyName = "Name";
            viewTextBoxColumn4.Width = 50;
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = "Date of Birth";
            viewTextBoxColumn5.DataPropertyName = "dob";
            viewTextBoxColumn5.Width = 120;
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = "Status";
            viewTextBoxColumn6.DataPropertyName = "status";
            viewTextBoxColumn6.Width = 60;
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = "Patient Location";
            viewTextBoxColumn7.DataPropertyName = "FacilityName";
            viewTextBoxColumn7.Width = 130;
            DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn8.HeaderText = "Ptn_Pk";
            viewTextBoxColumn8.DataPropertyName = "Ptn_Pk";
            viewTextBoxColumn8.Width = 10;
            viewTextBoxColumn8.Visible = false;
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
            this.grdResultView.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
            this.grdResultView.DataSource = (object)theDT;
        }

        private void BindCombo()
        {
            int num1 = (int)this.XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            int num2 = (int)this.XMLPharDS.ReadXml(GblIQCare.GetXMLPath() + "\\DrugMasters.con");
            BindFunctions bindFunctions = new BindFunctions();
            DataTable theDT1 = new DataView(this.XMLDS.Tables["mst_Facility"])
            {
                RowFilter = "(DeleteFlag =0 or DeleteFlag is null)"
            }.ToTable();
            bindFunctions.Win_BindCombo(this.cmbFacility, theDT1, "FacilityName", "FacilityId");
            DataView dataView = new DataView(this.XMLDS.Tables["mst_Decode"]);
            dataView.RowFilter = "CodeId = 4 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDT2 = dataView.ToTable();
            bindFunctions.Win_BindCombo(this.cmbSex, theDT2, "Name", "Id");
            dataView.RowFilter = "CodeId = 33 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDT3 = dataView.ToTable();
            bindFunctions.Win_BindCombo(this.cmbprogram, theDT3, "Name", "Id");
            DataTable theDT4 = new DataView(this.XMLDS.Tables["mst_Provider"])
            {
                RowFilter = "(DeleteFlag =0 or DeleteFlag is null)"
            }.ToTable();
            bindFunctions.Win_BindCombo(this.cmbProvider, theDT4, "Name", "Id");
            DataTable theDT5 = new DataView(this.XMLDS.Tables["mst_RegimenLine"])
            {
                RowFilter = "(DeleteFlag =0 or DeleteFlag is null)"
            }.ToTable();
            bindFunctions.Win_BindCombo(this.cmbRegimenLine, theDT5, "Name", "Id");
            DataTable theDT6 = new DataView(this.XMLDS.Tables["mst_Decode"])
            {
                RowFilter = "CodeId = 26 and (DeleteFlag =0 or DeleteFlag is null)"
            }.ToTable();
            bindFunctions.Win_BindCombo(this.cmbReason, theDT6, "Name", "Id");
            DataTable theDT7 = new DataView(this.XMLDS.Tables["mst_Decode"])
            {
                RowFilter = "CodeId = 31 and (DeleteFlag =0 or DeleteFlag is null) and id in(140,141,142)"
            }.ToTable();
            bindFunctions.Win_BindCombo(this.cmdPeriodTaken, theDT7, "Name", "Id");
            DataTable theDT8 = ((IViewAssociation)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BViewAssociation,BusinessProcess.FormBuilder")).GetMoudleName().Tables[0];
            DataRow row = theDT8.NewRow();
            row["ModuleName"] = (object)"All";
            row["ModuleID"] = (object)0;
            theDT8.Rows.InsertAt(row, 0);
            new BindFunctions().Win_BindCombo(this.cmbService, theDT8, "ModuleName", "ModuleId");
        }

        private void PatientDrugDispenseForm_Load(object sender, EventArgs e)
        {
            new clsCssStyle().setStyle((Control)this);
            this.Init_Form();
            this.tabDispense.SelectedTab = this.tabDispense.TabPages[0];
            this.txtPatientIdentification.Select();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = false;
            string gender = "";
            if (Convert.ToInt32(this.cmbSex.SelectedValue) > 0)
                gender = this.cmbSex.SelectedValue.ToString();
            string str = !(this.dtpDOB.CustomFormat == " ") ? this.dtpDOB.Text : "01-01-1900";
            this.BindSearchGrid(((IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical")).GetPatientSearchResults(Convert.ToInt32(this.cmbFacility.SelectedValue), this.txtLastName.Text, "", this.txtFirstName.Text, this.txtPatientIdentification.Text, gender, Convert.ToDateTime(str), "0", Convert.ToInt32(this.cmbService.SelectedValue)).Tables[0]);
        }

        private void dtpDOB_Enter(object sender, EventArgs e)
        {
            this.dtpDOB.CustomFormat = "dd-MMM-yyyy";
        }

        private void tabDispense_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = false;
            if ((this.tabDispense.TabPages[1].Focus() || this.tabDispense.TabPages[2].Focus()) && this.thePatientId < 1)
            {
                IQCareWindowMsgBox.ShowWindow("PatientNotSelected", (Control)this);
                this.tabDispense.SelectedTab = this.tabDispense.TabPages[0];
            }
            else
            {
                if (this.tabDispense.TabPages[0].Focus())
                {
                    this.thePatientId = 0;
                    this.Text = "Find Patient";
                    this.theOrderId = 0;
                    this.theOrderStatus = "";
                }
                if (this.tabDispense.TabPages[1].Focus())
                {
                    this.lblStoreName.Text = new DataView(this.XMLDS.Tables["Mst_Store"])
                    {
                        RowFilter = ("Id=" + GblIQCare.intStoreId.ToString())
                    }[0]["Name"].ToString();
                    this.cmdSave.Visible = true;
                    this.cmdPrintPrescription.Visible = true;
                    this.btnPrintLabel.Visible = true;
                    this.theItemId = 0;
                    this.theDispensingUnit = 0;
                    this.theBatchId = 0;
                    this.theCostPrice = new Decimal(0);
                    this.theMargin = new Decimal(0);
                    this.theBillAmt = new Decimal(0);
                    this.theDispensingUnitName = "";
                    this.txtItemName.Text = "";
                    this.txtBatchNo.Text = "";
                    this.txtExpirydate.Text = "";
                    this.Text = "Dispense Drugs";
                    if (this.theOrderId > 0 && (this.theOrderStatus == "Already Dispensed Order" || this.theOrderStatus == "Partial Dispense"))
                    {
                        this.makeGridEditable = "No";
                        this.cmdSave.Enabled = false;
                    }
                    else
                    {
                        this.cmdSave.Enabled = true;
                        this.makeGridEditable = "Yes";
                    }
                }
                if (!this.tabDispense.TabPages[2].Focus())
                    return;
                this.lblReturnStoreName.Text = new DataView(this.XMLDS.Tables["Mst_Store"])
                {
                    RowFilter = ("Id=" + GblIQCare.intStoreId.ToString())
                }[0]["Name"].ToString();
                this.cmdSave.Visible = true;
                this.cmdPrintPrescription.Visible = false;
                this.Text = "Return Drugs";
                this.lblReturnDispensedDate.Text = "";
                this.lblReturnProgram.Text = "";
                this.BindDrugReturnGrid(((IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM")).GetPharmacyExistingRecord(this.thePatientId, GblIQCare.intStoreId));
                this.grdReturnDetail.DataSource = (object)false;
                this.grdReturnDetail.Columns.Clear();
                this.cmdSave.Enabled = true;
            }
        }

        private void grdResultView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.clearPopup();
            this.thePatientId = Convert.ToInt32(this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[7].Value);
            this.theDOB = Convert.ToDateTime(this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[4].Value);
            this.tabDispense.SelectedTab = this.tabDispense.TabPages[1];
            this.lblPatientName.Text = this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[0].Value.ToString() + ", " + this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[1].Value.ToString();
            this.lblReturnPatName.Text = this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[0].Value.ToString() + ", " + this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[1].Value.ToString();
            this.lblIQNumber.BackColor = this.BackColor;
            this.lblIQNumber.Text = this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[2].Value.ToString();
            this.lblReturnIQNumber.Text = this.grdResultView.Rows[this.grdResultView.CurrentRow.Index].Cells[2].Value.ToString();
            this.thePharmacyMaster = ((IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM")).GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId);
            if (this.thePharmacyMaster.Tables[0].Rows.Count > 0)
            {
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("LastDispense"))
                {
                    this.lblDispDate.Text = "";
                    this.lblReturnLstDispDate.Text = "";
                    this.lbllastDisdate.Text = "";
                }
                else
                {
                    this.lblDispDate.Text = ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    this.lblReturnLstDispDate.Text = ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    this.lbllastDisdate.Text = ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                }
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("LastRegimen"))
                {
                    this.lblLstRegimen.Text = "";
                    this.lblReturnLstRegimen.Text = "";
                    this.lblDispenseLstRegimen.Text = "";
                    this.lastdispensedARV = "";
                }
                else
                {
                    this.lblLstRegimen.Text = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                    this.lblReturnLstRegimen.Text = this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString();
                    this.lblDispenseLstRegimen.Text = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                    this.lastdispensedARV = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString());
                }
            }
            else
            {
                this.lblDispDate.Text = "";
                this.lblLstRegimen.Text = "";
                this.lblReturnLstRegimen.Text = "";
                this.lblDispenseLstRegimen.Text = "";
                this.lblReturnLstDispDate.Text = "";
                this.lbllastDisdate.Text = "";
                this.lastdispensedARV = "";
            }
            this.BindPharmacyDispenseGrid(this.CreatePharmacyDispenseTable());
            this.txtItemName.Select();
        }

        private DataTable CreatePharmacyDispenseTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ItemId", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("ItemName", System.Type.GetType("System.String"));
            dataTable.Columns.Add("DispensingUnitId", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("DispensingUnitName", System.Type.GetType("System.String"));
            dataTable.Columns.Add("BatchId", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("BatchNo", System.Type.GetType("System.String"));
            dataTable.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("FrequencyId", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("FrequencyName", System.Type.GetType("System.String"));
            dataTable.Columns.Add("ExpiryDate", System.Type.GetType("System.String"));
            dataTable.Columns.Add("QtyDisp", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("CostPrice", System.Type.GetType("System.Decimal"));
            dataTable.Columns.Add("Margin", System.Type.GetType("System.Decimal"));
            dataTable.Columns.Add("SellingPrice", System.Type.GetType("System.Decimal"));
            dataTable.Columns.Add("BillAmount", System.Type.GetType("System.Decimal"));
            dataTable.Columns.Add("Prophylaxis", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("ItemType", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("GenericAbb", System.Type.GetType("System.String"));
            dataTable.Columns.Add("OrderedQuantity", System.Type.GetType("System.String"));
            dataTable.Columns.Add("DataStatus", System.Type.GetType("System.String"));
            dataTable.Columns.Add("Dose", System.Type.GetType("System.String"));
            dataTable.Columns.Add("Duration", System.Type.GetType("System.String"));
            dataTable.Columns.Add("PrintPrescriptionStatus", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("PatientInstructions", System.Type.GetType("System.String"));
            dataTable.Columns.Add("UnitSellingPrice", System.Type.GetType("System.Int32"));
            dataTable.Columns.Add("freqMultiplier", System.Type.GetType("System.Int32"));
            DataColumn[] dataColumnArray = new DataColumn[3]
      {
        dataTable.Columns["ItemId"],
        dataTable.Columns["BatchId"],
        dataTable.Columns["ExpiryDate"]
      };
            dataTable.PrimaryKey = dataColumnArray;
            return dataTable;
        }

        private void BindPharmacyDispenseGrid(DataTable theDT)
        {
            try
            {
                this.grdDrugDispense.DataSource = (object)null;
                this.grdDrugDispense.Columns.Clear();
                this.grdDrugDispense.AutoGenerateColumns = false;
                DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn1.HeaderText = "ItemId";
                viewTextBoxColumn1.DataPropertyName = "ItemId";
                viewTextBoxColumn1.Width = 10;
                viewTextBoxColumn1.Visible = false;
                viewTextBoxColumn1.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn2.HeaderText = "Drug Name";
                viewTextBoxColumn2.DataPropertyName = "ItemName";
                viewTextBoxColumn2.Width = 285;
                viewTextBoxColumn2.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn3.HeaderText = "DispUnitId";
                viewTextBoxColumn3.DataPropertyName = "DispensingUnitId";
                viewTextBoxColumn3.Width = 10;
                viewTextBoxColumn3.Visible = false;
                viewTextBoxColumn3.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn4.HeaderText = "Dispensing Unit";
                viewTextBoxColumn4.DataPropertyName = "DispensingUnitName";
                viewTextBoxColumn4.Width = 80;
                viewTextBoxColumn4.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn5.HeaderText = "BatchId";
                viewTextBoxColumn5.DataPropertyName = "BatchId";
                viewTextBoxColumn5.Width = 10;
                viewTextBoxColumn5.Visible = false;
                viewTextBoxColumn5.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn6.HeaderText = "Batch No";
                viewTextBoxColumn6.DataPropertyName = "BatchNo";
                viewTextBoxColumn6.Name = "BatchNo";
                viewTextBoxColumn6.Width = 80;
                viewTextBoxColumn6.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn7.HeaderText = "Expiry Date";
                viewTextBoxColumn7.DataPropertyName = "ExpiryDate";
                viewTextBoxColumn7.Name = "ExpiryDate";
                viewTextBoxColumn7.Width = 80;
                viewTextBoxColumn7.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn8.HeaderText = "Quantity Dispensed";
                viewTextBoxColumn8.DataPropertyName = "QtyDisp";
                viewTextBoxColumn8.Name = "QtyDisp";
                viewTextBoxColumn8.Width = 60;
                if (!(this.makeGridEditable == "Yes"))
                    viewTextBoxColumn8.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn9 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn9.HeaderText = "CostPrice";
                viewTextBoxColumn9.DataPropertyName = "CostPrice";
                viewTextBoxColumn9.Width = 10;
                viewTextBoxColumn9.Visible = false;
                viewTextBoxColumn9.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn10 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn10.HeaderText = "Margin";
                viewTextBoxColumn10.DataPropertyName = "Margin";
                viewTextBoxColumn10.Width = 10;
                viewTextBoxColumn10.Visible = false;
                viewTextBoxColumn10.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn11 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn11.HeaderText = "Selling Price";
                viewTextBoxColumn11.DataPropertyName = "SellingPrice";
                viewTextBoxColumn11.Name = "SellingPrice";
                viewTextBoxColumn11.Width = 60;
                viewTextBoxColumn11.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn12 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn12.HeaderText = "Bill Amount";
                viewTextBoxColumn12.DataPropertyName = "BillAmount";
                viewTextBoxColumn12.Name = "BillAmount";
                viewTextBoxColumn12.Width = 60;
                viewTextBoxColumn12.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn13 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn13.HeaderText = "StrengthId";
                viewTextBoxColumn13.DataPropertyName = "StrengthId";
                viewTextBoxColumn13.Width = 10;
                viewTextBoxColumn13.Visible = false;
                viewTextBoxColumn13.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn14 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn14.HeaderText = "Frequency";
                viewTextBoxColumn14.DataPropertyName = "FrequencyId";
                viewTextBoxColumn14.Name = "FrequencyId";
                viewTextBoxColumn14.Width = 10;
                viewTextBoxColumn14.Visible = false;
                viewTextBoxColumn14.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn15 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn15.HeaderText = "Frequency";
                viewTextBoxColumn15.DataPropertyName = "FrequencyName";
                viewTextBoxColumn15.Width = 60;
                viewTextBoxColumn15.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn16 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn16.HeaderText = "Quantity Prescribed";
                viewTextBoxColumn16.DataPropertyName = "OrderedQuantity";
                viewTextBoxColumn16.Name = "QtyPres";
                viewTextBoxColumn16.Width = 60;
                viewTextBoxColumn16.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn17 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn17.HeaderText = "DataStatus";
                viewTextBoxColumn17.DataPropertyName = "DataStatus";
                viewTextBoxColumn17.Width = 10;
                viewTextBoxColumn17.Visible = false;
                viewTextBoxColumn17.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn18 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn18.HeaderText = "Dose";
                viewTextBoxColumn18.DataPropertyName = "Dose";
                viewTextBoxColumn18.Name = "Dose";
                viewTextBoxColumn18.Width = 38;
                if (!(this.makeGridEditable == "Yes"))
                    viewTextBoxColumn18.ReadOnly = true;
                DataGridViewTextBoxColumn viewTextBoxColumn19 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn19.HeaderText = "Duration";
                viewTextBoxColumn19.DataPropertyName = "Duration";
                viewTextBoxColumn19.Name = "Duration";
                viewTextBoxColumn19.Width = 60;
                if (!(this.makeGridEditable == "Yes"))
                    viewTextBoxColumn19.ReadOnly = true;
                DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                viewCheckBoxColumn.HeaderText = "Print";
                viewCheckBoxColumn.DataPropertyName = "PrintPrescriptionStatus";
                viewCheckBoxColumn.Width = 25;
                DataGridViewTextBoxColumn viewTextBoxColumn20 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn20.HeaderText = "Patient Instructions";
                viewTextBoxColumn20.DataPropertyName = "PatientInstructions";
                viewTextBoxColumn20.Name = "PatientInstructions";
                viewTextBoxColumn20.Width = 10;
                viewTextBoxColumn20.Visible = false;
                DataGridViewTextBoxColumn viewTextBoxColumn21 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn21.HeaderText = "Unit Selling Price";
                viewTextBoxColumn21.DataPropertyName = "UnitSellingPrice";
                viewTextBoxColumn21.Name = "UnitSellingPrice";
                viewTextBoxColumn21.Width = 10;
                viewTextBoxColumn21.Visible = false;
                DataGridViewTextBoxColumn viewTextBoxColumn22 = new DataGridViewTextBoxColumn();
                viewTextBoxColumn22.HeaderText = "Frequency Multiplier";
                viewTextBoxColumn22.DataPropertyName = "freqMultiplier";
                viewTextBoxColumn22.Name = "freqMultiplier";
                viewTextBoxColumn22.Width = 10;
                viewTextBoxColumn22.Visible = false;
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn18);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn15);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn19);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn16);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn9);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn10);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn11);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn12);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn13);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn14);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn17);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewCheckBoxColumn);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn20);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn21);
                this.grdDrugDispense.Columns.Add((DataGridViewColumn)viewTextBoxColumn22);
                this.grdDrugDispense.DataSource = (object)theDT;
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void lstSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            this.lstSearch_DoubleClick(sender, (EventArgs)e);
        }

        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtItemName.Text != "")
            {
                this.lstSearch.Visible = true;
                this.lstSearch.Width = 900;
                this.lstSearch.Left = this.txtItemName.Left - 80;
                this.lstSearch.Top = this.panel4.Top + this.txtItemName.Top + this.txtItemName.Height;
                this.lstSearch.Height = 300;
                DataView dataView = new DataView(this.thePharmacyMaster.Tables[1]);
                dataView.RowFilter = "DrugName like '%" + this.txtItemName.Text.Trim().ToString().Replace("%", "[%]") + "%'";
                if (dataView.Count > 0)
                    new BindFunctions().Win_BindListBox(this.lstSearch, dataView.ToTable(), "DisplayItem", "DisplayItemId");
                else
                    this.lstSearch.DataSource = (object)null;
            }
            else
                this.lstSearch.Visible = false;
            if (e.KeyCode != Keys.Down)
                return;
            this.lstSearch.Select();
        }

        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstSearch.SelectedValue.ToString() == "")
                return;
            string[] strArray = this.lstSearch.SelectedValue.ToString().Split('-');
            this.theItemId = Convert.ToInt32(strArray.GetValue(0));
            DataView dataView = new DataView(this.thePharmacyMaster.Tables[1]);
            dataView.RowFilter = "Drug_Pk = " + this.theItemId.ToString() + " and BatchId = " + strArray.GetValue(1).ToString() + " and ExpiryDate='" + strArray.GetValue(2).ToString() + "'";
            this.txtItemName.Text = dataView[0]["DrugName"].ToString();
            this.txtBatchNo.Text = dataView[0]["BatchNo"].ToString();
            this.theBatchId = Convert.ToInt32(dataView[0]["BatchId"]);
            this.txtExpirydate.Text = ((DateTime)dataView[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat);
            this.theSellingPrice = Convert.ToDecimal(dataView[0]["SellingPrice"]);
            this.theConfigSellingPrice = Convert.ToDecimal(dataView[0]["ConfigSellingPrice"]);
            this.theCostPrice = Convert.ToDecimal(dataView[0]["CostPrice"]);
            this.theMargin = Convert.ToDecimal(dataView[0]["DispensingMargin"]);
            this.theDispensingUnit = Convert.ToInt32(dataView[0]["DispensingId"]);
            this.theDispensingUnitName = dataView[0]["DispensingUnit"].ToString();
            this.theFunded = Convert.ToInt32(dataView[0]["Funded"]);
            this.theAvailQty = Convert.ToInt32(dataView[0]["AvailQty"]);
            this.theStrength = Convert.ToInt32(dataView[0]["StrengthId"]);
            this.theItemType = Convert.ToInt32(dataView[0]["DrugTypeId"]);
            this.ARVBeingDispensed = dataView[0]["GenericAbb"].ToString();
            this.ItemInstructions = dataView[0]["ItemInstructions"].ToString();
            this.theProphylaxis = this.theItemType != 37 || !(this.cmbprogram.SelectedValue.ToString() == "223") ? 0 : 1;
            if (this.theItemType == 37)
            {
                if (!this.btnART.Enabled)
                {
                    this.btnART.Enabled = true;
                    if (this.lastdispensedARV != "")
                        this.grpBoxLastDispense.Visible = true;
                }
            }
            else if (this.btnART.Enabled)
                this.btnART.Enabled = false;
            this.theGenericAbb = dataView[0]["GenericAbb"].ToString();
            this.lstSearch.Visible = false;
            if (this.theAvailQty == 0)
            {
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("NoAvailQty", (Control)this);
            }
            else
            {
                if (!(this.ARVBeingDispensed != "") || !(this.lastdispensedARV != "") || !(this.lastdispensedARV != this.ARVBeingDispensed))
                    return;
                IQCareWindowMsgBox.ShowWindow("RegimenChangeAlert", (Control)this);
            }
        }

        private string removeRegimenDuplicates(string lastRegimen)
        {
            return Enumerable.Aggregate<string>(Enumerable.Distinct<string>((IEnumerable<string>)lastRegimen.Split('/')), (Func<string, string, string>)((current, next) => current + "/" + next));
        }

        private void btnDispenseSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet dataSet = new DataSet();
                DataTable dataTable1 = new DataView(((IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM")).GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId).Tables[1])
                {
                    RowFilter = ("Drug_Pk = " + this.theItemId.ToString() + " and BatchId = " + this.theBatchId.ToString() + " and ExpiryDate='" + this.txtExpirydate.Text + "'")
                }.ToTable();
                this.qtyAvailableInBatch = dataTable1.Rows[0]["AvailQty"].ToString() == null || dataTable1.Rows[0]["AvailQty"].ToString().Equals("") ? 0.0 : Convert.ToDouble(dataTable1.Rows[0]["AvailQty"].ToString());
                if (!this.ValidateDrugDispense())
                    return;
                if (!this.txtItemName.Enabled)
                {
                    DataTable dataTable2 = (DataTable)this.grdDrugDispense.DataSource;
                    new DataView(dataTable2).RowFilter = "ItemId = " + this.theItemId.ToString() + " and BatchId = " + this.theBatchId.ToString() + " and ExpiryDate='" + this.txtExpirydate.Text + "'";
                    this.txtItemName.Enabled = true;
                    this.BindPharmacyDispenseGrid(dataTable2);
                }
                else
                {
                    DataTable dataTable2 = (DataTable)this.grdDrugDispense.DataSource;
                    if (this.theOrderStatus == "New Order")
                    {
                        DataRow[] dataRowArray1 = dataTable2.Select("ItemId = " + this.theItemId.ToString());
                        if (dataRowArray1.Length > 0)
                        {
                            if (Convert.ToString(dataRowArray1[0]["BatchId"]) != "" && this.theprevbatchId == 0)
                            {
                                if (dataRowArray1[0]["BatchId"].ToString() == this.theBatchId.ToString())
                                {
                                    IQCareWindowMsgBox.ShowWindow("BatchExists", (Control)this);
                                    return;
                                }
                                DataRow[] dataRowArray2 = DataTableExtensions.CopyToDataTable<DataRow>((IEnumerable<DataRow>)dataRowArray1).Select("BatchId = " + this.theBatchId.ToString());
                                if (dataRowArray2.Length > 0)
                                    dataTable2.Rows.Remove(dataRowArray2[0]);
                            }
                            else
                                dataTable2.Rows.Remove(dataRowArray1[0]);
                        }
                    }
                    if (this.theDispCurrentRow > -1 && dataTable2.Rows.Count > 0)
                    {
                        DataView dataView = new DataView(dataTable2);
                        dataView.RowFilter = "ItemId = " + this.theItemId.ToString() + " and BatchId = " + this.theBatchId.ToString() + " and ExpiryDate='" + this.txtExpirydate.Text.Replace("-", " ") + "'";
                        if (dataView.Count > 0)
                        {
                            dataView[0]["Prophylaxis"] = (object)this.theProphylaxis;
                            dataView[0]["ItemType"] = (object)this.theItemType;
                            dataView[0]["GenericAbb"] = (object)this.theGenericAbb;
                            dataView[0]["BillAmount"] = (object)this.theBillAmt;
                            dataView[0]["CostPrice"] = (object)this.theCostPrice;
                            dataView[0]["PatientInstructions"] = (object)this.ItemInstructions;
                        }
                        else
                        {
                            DataRow row = dataTable2.NewRow();
                            row["ItemId"] = (object)this.theItemId;
                            row["ItemName"] = (object)this.txtItemName.Text;
                            row["DispensingUnitId"] = (object)this.theDispensingUnit;
                            row["DispensingUnitName"] = (object)this.theDispensingUnitName;
                            row["BatchId"] = (object)this.theBatchId;
                            row["BatchNo"] = (object)this.txtBatchNo.Text;
                            row["ExpiryDate"] = (object)this.txtExpirydate.Text;
                            row["CostPrice"] = (object)this.theCostPrice;
                            row["Margin"] = (object)this.theMargin;
                            row["BillAmount"] = (object)this.theBillAmt;
                            row["StrengthId"] = (object)this.theStrength;
                            row["Prophylaxis"] = (object)this.theProphylaxis;
                            row["ItemType"] = (object)this.theItemType;
                            row["GenericAbb"] = (object)this.theGenericAbb;
                            row["DataStatus"] = (object)"1";
                            row["PatientInstructions"] = (object)this.ItemInstructions;
                            dataTable2.Rows.Add(row);
                            this.CreatePharmacyDispenseTable();
                            this.BindPharmacyDispenseGrid(dataTable2.Copy());
                        }
                        dataTable2.AcceptChanges();
                        this.BindPharmacyDispenseGrid(dataTable2);
                        this.theDispCurrentRow = -1;
                    }
                    else
                    {
                        DataRow row = dataTable2.NewRow();
                        row["ItemId"] = (object)this.theItemId;
                        row["ItemName"] = (object)this.txtItemName.Text;
                        row["DispensingUnitId"] = (object)this.theDispensingUnit;
                        row["DispensingUnitName"] = (object)this.theDispensingUnitName;
                        row["BatchId"] = (object)this.theBatchId;
                        row["BatchNo"] = (object)this.txtBatchNo.Text;
                        row["ExpiryDate"] = (object)this.txtExpirydate.Text;
                        row["QtyDisp"] = (object)0;
                        row["CostPrice"] = (object)this.theCostPrice;
                        row["Margin"] = (object)this.theMargin;
                        row["SellingPrice"] = (object)0;
                        row["BillAmount"] = (object)this.theBillAmt;
                        row["StrengthId"] = (object)this.theStrength;
                        row["FrequencyId"] = (object)0;
                        row["FrequencyName"] = (object)"";
                        row["Prophylaxis"] = (object)this.theProphylaxis;
                        row["ItemType"] = (object)this.theItemType;
                        row["GenericAbb"] = (object)this.theGenericAbb;
                        row["OrderedQuantity"] = (object)0;
                        row["Dose"] = (object)0;
                        row["Duration"] = (object)0;
                        row["freqMultiplier"] = (object)0;
                        row["UnitSellingPrice"] = dataTable1.Rows[0]["ConfigSellingPrice"].ToString() == null || dataTable1.Rows[0]["ConfigSellingPrice"].ToString().Equals("") ? (object)0 : (object)Convert.ToInt32(dataTable1.Rows[0]["ConfigSellingPrice"].ToString());
                        row["DataStatus"] = (object)"1";
                        row["PatientInstructions"] = (object)this.ItemInstructions;
                        dataTable2.Rows.Add(row);
                        this.CreatePharmacyDispenseTable();
                        this.BindPharmacyDispenseGrid(dataTable2.Copy());
                    }
                }
                this.theItemId = 0;
                this.theDispensingUnit = 0;
                this.theBatchId = 0;
                this.theCostPrice = new Decimal(0);
                this.theMargin = new Decimal(0);
                this.theBillAmt = new Decimal(0);
                this.theDispensingUnitName = "";
                this.txtItemName.Text = "";
                this.txtBatchNo.Text = "";
                this.txtExpirydate.Text = "";
                this.txtItemName.Select();
                this.theprevbatchId = 0;
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private bool ValidateDrugDispense()
        {
            if (!(this.txtItemName.Text.Trim() == ""))
                return true;
            MsgBuilder MessageBuilder = new MsgBuilder();
            MessageBuilder.DataElements["Control"] = "Drug Name";
            int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", MessageBuilder, (Control)this);
            return false;
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_String(e);
        }

        private void txtQtyDispensed_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_Numeric(e);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                if (this.tabDispense.TabPages[1].Focus())
                {
                    DataTable theDT = (DataTable)this.grdDrugDispense.DataSource;
                    string theRegimen = "";
                    int theOrderType = Convert.ToDateTime(this.dtDispensedDate.Text).Year - this.theDOB.Year <= 15 ? 117 : 116;
                    foreach (DataRow dataRow in (InternalDataCollectionBase)this.RemoveDuplicatesRecords(theDT.Copy()).Rows)
                    {
                        if (dataRow["ItemType"].ToString() == "37" && dataRow["Prophylaxis"].ToString() != "1")
                        {
                            if (theRegimen == "" && dataRow["GenericAbb"].ToString() != "")
                                theRegimen += dataRow["GenericAbb"].ToString();
                            else if (theRegimen != "" && dataRow["GenericAbb"].ToString() != "")
                                theRegimen = theRegimen + "/" + dataRow["GenericAbb"].ToString();
                        }
                    }
                    string str = !(this.dtRefillApp.CustomFormat == " ") ? this.dtRefillApp.Text : "01-01-1900";
                    if (this.btnART.Enabled)
                    {
                        if (this.cmbprogram.SelectedValue.ToString() == "0" || this.cmbRegimenLine.SelectedValue.ToString() == "0")
                        {
                            int num = (int)MessageBox.Show("Program or Regimen Line cannot be blank");
                            this.grpHivCareTrtPharmacyField.Visible = true;
                        }
                        else
                        {
                            this.SaveUpdateArt(Convert.ToInt32(drug.SavePharmacyDispense(this.thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId, Convert.ToDateTime(this.dtDispensedDate.Text), theOrderType, Convert.ToInt32(this.cmbprogram.SelectedValue), theRegimen, this.theOrderId, theDT, Convert.ToDateTime(str)).Rows[0]["Ptn_Pharmacy_Pk"].ToString()));
                            IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", (Control)this);
                            DataTable pharmacyDispenseTable = this.CreatePharmacyDispenseTable();
                            this.BindPharmacyDispenseGrid(pharmacyDispenseTable);
                            this.grdDrugDispense.DataSource = (object)pharmacyDispenseTable;
                            this.btnART.Enabled = false;
                        }
                    }
                    else
                    {
                        drug.SavePharmacyDispense(this.thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, GblIQCare.AppUserId, Convert.ToDateTime(this.dtDispensedDate.Text), theOrderType, Convert.ToInt32(this.cmbprogram.SelectedValue), theRegimen, this.theOrderId, theDT, Convert.ToDateTime(str));
                        IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", (Control)this);
                        DataTable pharmacyDispenseTable = this.CreatePharmacyDispenseTable();
                        this.BindPharmacyDispenseGrid(pharmacyDispenseTable);
                        this.grdDrugDispense.DataSource = (object)pharmacyDispenseTable;
                        this.btnART.Enabled = false;
                    }
                    this.theOrderId = 0;
                    this.theOrderStatus = "";
                    this.lblPayAmount.Text = "0.0";
                }
                else if (this.tabDispense.TabPages[2].Focus())
                {
                    if (Convert.ToDateTime(this.dtpReturnDate.Text) < Convert.ToDateTime(this.dtDispensedDate.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturnDate", (Control)this);
                        return;
                    }
                    DataTable theDT = (DataTable)this.grdReturnDetail.DataSource;
                    int num = 0;
                    foreach (DataRow dataRow in (InternalDataCollectionBase)theDT.Rows)
                    {
                        if (Convert.ToInt32(dataRow["ReturnQty"]) > 0)
                        {
                            ++num;
                            dataRow["SellingPrice"] = (object)(Convert.ToDecimal("-" + dataRow["UnitSellingPrice"].ToString()) * (Decimal)Convert.ToInt32(dataRow["ReturnQty"]));
                            dataRow["BillAmount"] = Convert.ToInt32(dataRow["BillAmount"]) <= 0 ? (object)0 : dataRow["SellingPrice"];
                        }
                    }
                    if (num == 0)
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturn", (Control)this);
                        return;
                    }
                    drug.SavePharmacyReturn(this.thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, Convert.ToDateTime(this.dtpReturnDate.Text), GblIQCare.AppUserId, this.theReturnOrderId, theDT);
                    IQCareWindowMsgBox.ShowWindow("PharmacyReturnSave", (Control)this);
                    this.BindDrugReturnGrid(drug.GetPharmacyExistingRecord(this.thePatientId, GblIQCare.intStoreId));
                    this.grdReturnDetail.DataSource = (object)false;
                    this.grdReturnDetail.Columns.Clear();
                    this.btnART.Enabled = false;
                }
                this.thePharmacyMaster = drug.GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId);
                this.grpBoxLastDispense.Visible = false;
                this.chkPharmacyRefill.Checked = false;
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = !(ex.Message.ToString() == "Deleted row information cannot be accessed through the row.") ? ex.Message.ToString() : "You cannot delete a prescribed drug.";
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            return DataTableExtensions.CopyToDataTable<DataRow>(Enumerable.Select<IGrouping<int, DataRow>, DataRow>(Enumerable.GroupBy<DataRow, int>((IEnumerable<DataRow>)DataTableExtensions.AsEnumerable(dt), (Func<DataRow, int>)(r => DataRowExtensions.Field<int>(r, "ItemID"))), (Func<IGrouping<int, DataRow>, DataRow>)(g => Enumerable.First<DataRow>((IEnumerable<DataRow>)g))));
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.grpExistingRec.Visible = true;
            this.grpExistingRec.Left = 68;
            this.grpExistingRec.Top = 56;
            this.grpExistingRec.Width = 702;
            this.grpExistingRec.Height = 280;
            this.BindExitingGrid(((IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM")).GetPharmacyExistingRecord(this.thePatientId, GblIQCare.intStoreId));
        }

        private void BindExitingGrid(DataTable theDT)
        {
            this.grdExitingPharDisp.DataSource = (object)null;
            this.grdExitingPharDisp.Columns.Clear();
            this.grdExitingPharDisp.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "Transaction Date";
            viewTextBoxColumn1.DataPropertyName = "TransactionDate";
            viewTextBoxColumn1.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "Status";
            viewTextBoxColumn2.DataPropertyName = "Status";
            viewTextBoxColumn2.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = "Id";
            viewTextBoxColumn3.DataPropertyName = "Ptn_Pharmacy_Pk";
            viewTextBoxColumn3.Width = 10;
            viewTextBoxColumn3.Visible = false;
            this.grdExitingPharDisp.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            this.grdExitingPharDisp.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            this.grdExitingPharDisp.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            this.grdExitingPharDisp.DataSource = (object)theDT;
        }

        private void BindDrugReturnGrid(DataTable theDT)
        {
            this.grdReturnOrder.DataSource = (object)null;
            this.grdReturnOrder.Columns.Clear();
            this.grdReturnOrder.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "Transaction Date";
            viewTextBoxColumn1.DataPropertyName = "TransactionDate";
            viewTextBoxColumn1.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "Status";
            viewTextBoxColumn2.DataPropertyName = "Status";
            viewTextBoxColumn2.Width = 200;
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = "Id";
            viewTextBoxColumn3.DataPropertyName = "Ptn_Pharmacy_Pk";
            viewTextBoxColumn3.Width = 10;
            viewTextBoxColumn3.Visible = false;
            this.grdReturnOrder.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            this.grdReturnOrder.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            this.grdReturnOrder.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            this.grdReturnOrder.DataSource = (object)theDT;
        }

        private void btnExitingRecClose_Click(object sender, EventArgs e)
        {
            this.grpExistingRec.Visible = false;
        }

        private void grdExitingPharDisp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.clearPopup();
            this.chkPharmacyRefill.Checked = false;
            this.theOrderId = Convert.ToInt32(this.grdExitingPharDisp.Rows[this.grdExitingPharDisp.CurrentRow.Index].Cells[2].Value);
            this.theOrderStatus = this.grdExitingPharDisp.Rows[this.grdExitingPharDisp.CurrentRow.Index].Cells[1].Value.ToString();
            IDrug drug1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataSet existingRecordDetails = drug1.GetPharmacyExistingRecordDetails(this.theOrderId);
            if (existingRecordDetails.Tables[1].Rows.Count > 0)
            {
                if (!existingRecordDetails.Tables[1].Rows[0].IsNull("DispensedByDate"))
                    this.dtDispensedDate.Text = existingRecordDetails.Tables[1].Rows[0]["DispensedByDate"].ToString();
                if (!existingRecordDetails.Tables[1].Rows[0].IsNull("ProgId"))
                    this.cmbprogram.SelectedValue = (object)existingRecordDetails.Tables[1].Rows[0]["ProgId"].ToString();
            }
            Decimal num = new Decimal(0);
            this.theExistingDrugs = existingRecordDetails.Tables[0];
            foreach (DataRow dataRow in (InternalDataCollectionBase)existingRecordDetails.Tables[0].Rows)
            {
                if (!dataRow.IsNull("BillAmount"))
                    num += Convert.ToDecimal(dataRow["BillAmount"]);
                //else
                //    num = num;
            }
            this.lblPayAmount.Text = num.ToString();
            if (this.theOrderId > 0 && (this.theOrderStatus == "Already Dispensed Order" || this.theOrderStatus == "Partial Dispense"))
            {
                this.makeGridEditable = "No";
                this.dtRefillApp.CustomFormat = " ";
            }
            else
                this.makeGridEditable = "Yes";
            this.BindPharmacyDispenseGrid(existingRecordDetails.Tables[0]);
            this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = false;
            if (existingRecordDetails.Tables[0].Rows.Count > 0)
            {
                for (int index = 0; index < existingRecordDetails.Tables[0].Rows.Count; ++index)
                {
                    int ItemID = Convert.ToInt32(existingRecordDetails.Tables[0].Rows[index]["ItemId"].ToString());
                    IDrug drug2 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                    DataSet drugTypeId = drug1.GetDrugTypeID(ItemID);
                    if (drugTypeId.Tables[0].Rows.Count > 0 && drugTypeId.Tables[0].Rows[0]["DrugTypeID"].ToString() == "37")
                    {
                        if (existingRecordDetails.Tables[0].Rows[index]["ItemType"].ToString() == "37")
                            this.ARVBeingDispensed = existingRecordDetails.Tables[0].Rows[index]["GenericAbb"].ToString();
                        this.btnART.Enabled = true;
                        if (this.lastdispensedARV != "")
                            this.grpBoxLastDispense.Visible = true;
                        this.clearPopup();
                        if (this.btnART.Enabled && this.theOrderId > 0)
                        {
                            IDrug drug3 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                            DataSet detailsByDespenced = drug3.GetPharmacyDetailsByDespenced(this.theOrderId);
                            if (detailsByDespenced.Tables[0].Rows.Count > 0)
                            {
                                this.txtWeight.Text = detailsByDespenced.Tables[0].Rows[0]["Weight"].ToString();
                                this.txtHeight.Text = detailsByDespenced.Tables[0].Rows[0]["Height"].ToString();
                                if (this.txtWeight.Text != "" && this.txtHeight.Text != "")
                                    this.txtBSA.Text = Math.Round(Math.Sqrt(Convert.ToDouble(Convert.ToDouble(this.txtWeight.Text) * Convert.ToDouble(this.txtHeight.Text) / 3600.0)), 2).ToString();
                                this.cmbprogram.SelectedValue = (object)detailsByDespenced.Tables[0].Rows[0]["ProgID"].ToString();
                                if (detailsByDespenced.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                                    this.cmdPeriodTaken.SelectedValue = (object)detailsByDespenced.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                                if (detailsByDespenced.Tables[0].Rows[0]["ProviderID"].ToString() != "")
                                    this.cmbProvider.SelectedValue = (object)detailsByDespenced.Tables[0].Rows[0]["ProviderID"].ToString();
                                if (detailsByDespenced.Tables[0].Rows[0]["RegimenLine"].ToString() != "")
                                    this.cmbRegimenLine.SelectedValue = (object)detailsByDespenced.Tables[0].Rows[0]["RegimenLine"].ToString();
                                if (detailsByDespenced.Tables[0].Rows[0]["AppDate"].ToString() != "")
                                {
                                    this.NxtAppDate.Format = DateTimePickerFormat.Custom;
                                    this.NxtAppDate.CustomFormat = "dd-MMM-yyyy";
                                    this.NxtAppDate.Text = detailsByDespenced.Tables[0].Rows[0]["AppDate"].ToString();
                                }
                                else
                                    this.NxtAppDate.CustomFormat = " ";
                                if (detailsByDespenced.Tables[0].Rows[0]["AppReason"].ToString() != "")
                                    this.cmbReason.SelectedValue = (object)detailsByDespenced.Tables[0].Rows[0]["AppReason"].ToString();
                                if (this.cmbprogram.SelectedValue.ToString() == "222")
                                {
                                    int PatientID = this.thePatientId;
                                    drug3.SaveArtData(PatientID, Convert.ToDateTime(this.dtDispensedDate.Text));
                                }
                            }
                        }
                    }
                }
            }
            if (this.theOrderId > 0 && (this.theOrderStatus == "Already Dispensed Order" || this.theOrderStatus == "Partial Dispense"))
            {
                this.makeGridEditable = "No";
                this.cmdSave.Enabled = false;
            }
            else
            {
                this.cmdSave.Enabled = true;
                this.makeGridEditable = "Yes";
            }
            if (this.ARVBeingDispensed != "" && this.lastdispensedARV != "" && this.ARVBeingDispensed != this.lastdispensedARV)
                IQCareWindowMsgBox.ShowWindow("RegimenChangeAlert", (Control)this);
            this.grpExistingRec.Visible = false;
        }

        private void clearPopup()
        {
            this.txtWeight.Text = "";
            this.txtHeight.Text = "";
            this.txtBSA.Text = "";
            this.cmdPeriodTaken.SelectedValue = (object)"0";
            this.cmbProvider.SelectedValue = (object)"0";
            this.cmbRegimenLine.SelectedValue = (object)"0";
            this.cmbReason.SelectedValue = (object)"0";
            this.cmbprogram.SelectedValue = (object)"0";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.thePrecribeAmt = new Decimal(0);
            this.theOrderId = 0;
            DataTable pharmacyDispenseTable = this.CreatePharmacyDispenseTable();
            this.grdDrugDispense.DataSource = (object)pharmacyDispenseTable;
            this.BindPharmacyDispenseGrid(pharmacyDispenseTable);
            this.dtDispensedDate.Text = GblIQCare.CurrentDate;
            this.cmbprogram.SelectedValue = (object)"0";
            this.theItemId = 0;
            this.theDispensingUnit = 0;
            this.theBatchId = 0;
            this.theCostPrice = new Decimal(0);
            this.theMargin = new Decimal(0);
            this.theBillAmt = new Decimal(0);
            this.theDispensingUnitName = "";
            this.txtItemName.Text = "";
            this.txtBatchNo.Text = "";
            this.txtExpirydate.Text = "";
            this.txtItemName.Select();
            this.txtWeight.Text = "";
            this.txtHeight.Text = "";
            this.txtBSA.Text = "";
            this.cmdPeriodTaken.SelectedValue = (object)"0";
            this.cmbProvider.SelectedValue = (object)"0";
            this.cmbRegimenLine.SelectedValue = (object)"0";
            this.cmbReason.SelectedValue = (object)"0";
            this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = false;
            this.cmdSave.Enabled = true;
            this.cmbGrdDrugDispense.Hide();
            this.cmbGrdDrugDispenseFreq.Hide();
            this.makeGridEditable = "Yes";
            this.dtRefillApp.CustomFormat = " ";
            this.chkPharmacyRefill.Checked = false;
            this.lblPayAmount.Text = "0.0";
            this.clearPopup();
        }

        private void grdDrugDispense_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex != 19 || e.RowIndex != -1)
                return;
            e.PaintBackground(e.ClipBounds, false);
            Point location = e.CellBounds.Location;
            int num1 = (e.CellBounds.Width - this.imageList1.ImageSize.Width) / 2;
            int num2 = (e.CellBounds.Height - this.imageList1.ImageSize.Height) / 2;
            location.X += num1;
            location.Y += num2;
            this.imageList1.Draw(e.Graphics, location, 0);
            e.Handled = true;
        }

        private void grdDrugDispense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void grdReturnOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(this.grdReturnOrder.Rows[this.grdReturnOrder.CurrentRow.Index].Cells[1].Value.ToString() != "New Order"))
                return;
            this.theReturnOrderId = Convert.ToInt32(this.grdReturnOrder.Rows[this.grdReturnOrder.CurrentRow.Index].Cells[2].Value);
            DataSet existingRecordDetails = ((IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM")).GetPharmacyExistingRecordDetails(this.theReturnOrderId);
            if (existingRecordDetails.Tables[1].Rows.Count <= 0)
                return;
            if (existingRecordDetails.Tables[1].Rows[0]["DispensedByDate"].ToString() != "")
                this.lblReturnDispensedDate.Text = ((DateTime)existingRecordDetails.Tables[1].Rows[0]["DispensedByDate"]).ToString("dd-MMM-yyyy");
            DataView dataView = new DataView(this.XMLDS.Tables["Mst_Decode"]);
            dataView.RowFilter = "CodeId = 33 and (DeleteFlag = 0 or DeleteFlag is null) and Id = " + existingRecordDetails.Tables[1].Rows[0]["ProgId"].ToString();
            if (dataView.ToTable().Rows.Count > 0)
                this.lblReturnProgram.Text = dataView[0]["Name"].ToString();
            this.BindDrugRetunDetailGrid(existingRecordDetails.Tables[0]);
        }

        private void BindDrugRetunDetailGrid(DataTable theDT)
        {
            this.grdReturnDetail.DataSource = (object)null;
            this.grdReturnDetail.Columns.Clear();
            this.grdReturnDetail.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "ItemId";
            viewTextBoxColumn1.DataPropertyName = "ItemId";
            viewTextBoxColumn1.Width = 10;
            viewTextBoxColumn1.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "Drug Name";
            viewTextBoxColumn2.DataPropertyName = "ItemName";
            viewTextBoxColumn2.Width = 200;
            viewTextBoxColumn2.ReadOnly = true;
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = "DispUnitId";
            viewTextBoxColumn3.DataPropertyName = "DispensingUnitId";
            viewTextBoxColumn3.Width = 10;
            viewTextBoxColumn3.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = "Dispensing Unit";
            viewTextBoxColumn4.DataPropertyName = "DispensingUnitName";
            viewTextBoxColumn4.Width = 80;
            viewTextBoxColumn4.ReadOnly = true;
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = "BatchId";
            viewTextBoxColumn5.DataPropertyName = "BatchId";
            viewTextBoxColumn5.Width = 10;
            viewTextBoxColumn5.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = "Batch No";
            viewTextBoxColumn6.DataPropertyName = "BatchNo";
            viewTextBoxColumn6.Width = 80;
            viewTextBoxColumn6.ReadOnly = true;
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = "Expiry Date";
            viewTextBoxColumn7.DataPropertyName = "ExpiryDate";
            viewTextBoxColumn7.Width = 80;
            viewTextBoxColumn7.ReadOnly = true;
            DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn8.HeaderText = "Quantity";
            viewTextBoxColumn8.DataPropertyName = "QtyDisp";
            viewTextBoxColumn8.Width = 80;
            viewTextBoxColumn8.ReadOnly = true;
            DataGridViewTextBoxColumn viewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn9.HeaderText = "CostPrice";
            viewTextBoxColumn9.DataPropertyName = "CostPrice";
            viewTextBoxColumn9.Width = 10;
            viewTextBoxColumn9.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn10.HeaderText = "Margin";
            viewTextBoxColumn10.DataPropertyName = "Margin";
            viewTextBoxColumn10.Width = 10;
            viewTextBoxColumn10.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn11.HeaderText = "Selling Price";
            viewTextBoxColumn11.DataPropertyName = "SellingPrice";
            viewTextBoxColumn11.Width = 80;
            viewTextBoxColumn11.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn12.HeaderText = "Bill Amount";
            viewTextBoxColumn12.DataPropertyName = "BillAmount";
            viewTextBoxColumn12.Width = 80;
            viewTextBoxColumn12.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn13.HeaderText = "StrengthId";
            viewTextBoxColumn13.DataPropertyName = "StrengthId";
            viewTextBoxColumn13.Width = 10;
            viewTextBoxColumn13.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn14.HeaderText = "Frequency";
            viewTextBoxColumn14.DataPropertyName = "FrequencyId";
            viewTextBoxColumn14.Width = 10;
            viewTextBoxColumn14.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn15.HeaderText = "Frequency";
            viewTextBoxColumn15.DataPropertyName = "FrequencyName";
            viewTextBoxColumn15.Width = 80;
            viewTextBoxColumn15.Visible = false;
            DataGridViewTextBoxColumn viewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn16.HeaderText = "Quantity Return";
            viewTextBoxColumn16.DataPropertyName = "ReturnQty";
            viewTextBoxColumn16.Width = 80;
            DataTable dataTable = new DataView(this.XMLDS.Tables["Mst_Decode"])
            {
                RowFilter = "CodeId = 204 and (DeleteFlag = 0 or DeleteFlag is null)"
            }.ToTable();
            DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
            viewComboBoxColumn.HeaderText = "Return Reason";
            viewComboBoxColumn.ValueMember = "Id";
            viewComboBoxColumn.DisplayMember = "Name";
            viewComboBoxColumn.DataSource = (object)dataTable;
            viewComboBoxColumn.DataPropertyName = "ReturnReason";
            viewComboBoxColumn.Width = 150;
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn15);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn9);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn10);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn11);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn12);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn13);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn14);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewTextBoxColumn16);
            this.grdReturnDetail.Columns.Add((DataGridViewColumn)viewComboBoxColumn);
            this.grdReturnDetail.DataSource = (object)theDT;
        }

        private void grdReturnDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!(e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewTextBoxEditingControl"))
                return;
            this.theCurrentRow = this.grdReturnDetail.CurrentRow.Index;
            this.theReturnQty = (TextBox)e.Control;
            this.theReturnQty.KeyUp += new KeyEventHandler(this.theReturnQty_KeyUp);
            this.theReturnQty.KeyPress += new KeyPressEventHandler(this.theReturnQty_KeyPress);
        }

        private void theReturnQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(this.theReturnQty.Text != "") || Convert.ToInt32(this.grdReturnDetail.Rows[this.theCurrentRow].Cells[7].Value) >= Convert.ToInt32(this.theReturnQty.Text))
                return;
            IQCareWindowMsgBox.ShowWindow("ReturnQtyGrtthanIssue", (Control)this);
            this.theReturnQty.Text = "";
        }

        private void theReturnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_Numeric(e);
        }

        private void grdResultView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnHIVCareTrtPharFld_Click(object sender, EventArgs e)
        {
            this.grpHivCareTrtPharmacyField.Visible = false;
        }

        private void btnART_Click(object sender, EventArgs e)
        {
            this.grpHivCareTrtPharmacyField.Visible = true;
        }

        private void txtDose_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_Numeric(e);
        }

        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_Numeric(e);
        }

        private void txtQtyPrescribed_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_Numeric(e);
        }

        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(this.txtWeight.Text != "") || !(this.txtHeight.Text != ""))
                return;
            this.txtBSA.Text = Math.Round(Math.Sqrt(Convert.ToDouble(Convert.ToDouble(this.txtWeight.Text) * Convert.ToDouble(this.txtHeight.Text) / 3600.0)), 2).ToString();
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_decimal(e);
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            new BindFunctions().Win_decimal(e);
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(this.txtWeight.Text != "") || !(this.txtHeight.Text != ""))
                return;
            this.txtBSA.Text = Math.Round(Math.Sqrt(Convert.ToDouble(Convert.ToDouble(this.txtWeight.Text) * Convert.ToDouble(this.txtHeight.Text) / 3600.0)), 2).ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtItemName.Text = "";
            this.txtBatchNo.Text = "";
            this.txtExpirydate.Text = "";
        }

        private void btnPatientClinicalSummary_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = this.thePatientId;
            ((Control)Activator.CreateInstance(System.Type.GetType("IQCare.SCM.frmPatientClinicalSummary, IQCare.SCM"))).Show();
        }

        private void btnPharmacyNotes_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = this.thePatientId;
            ((Control)Activator.CreateInstance(System.Type.GetType("IQCare.SCM.frmPharmacynotes, IQCare.SCM"))).Show();
        }

        public void SaveUpdateArt(int OrderID)
        {
            IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            string str = !(this.NxtAppDate.CustomFormat == " ") ? (!(this.NxtAppDate.Text == "") ? this.NxtAppDate.Text : "01-01-1900") : "01-01-1900";
            string weight = !(this.txtWeight.Text == "") ? this.txtWeight.Text : "0";
            string height = !(this.txtHeight.Text == "") ? this.txtHeight.Text : "0";
            drug.SaveHivTreatementPharmacyField(OrderID, weight, height, Convert.ToInt32(this.cmbprogram.SelectedValue), Convert.ToInt32(this.cmdPeriodTaken.SelectedValue), Convert.ToInt32(this.cmbProvider.SelectedValue), Convert.ToInt32(this.cmbRegimenLine.SelectedValue), Convert.ToDateTime(str), Convert.ToInt32(this.cmbReason.SelectedValue));
        }

        public void SetPharmacyRefillApp()
        {
        }

        private void NxtAppDate_Enter(object sender, EventArgs e)
        {
            this.NxtAppDate.CustomFormat = "dd-MMM-yyyy";
        }

        private void btncopy_Click(object sender, EventArgs e)
        {
            Utility utility = new Utility();
            Process.Start(new ProcessStartInfo(GblIQCare.weburl() + "?loc=w&iqnum=" + utility.EncodeTo64(this.lblIQNumber.Text) + "&AppName=" + utility.EncodeTo64(GblIQCare.AppUName) + "&apploc=" + utility.EncodeTo64(GblIQCare.AppLocationId.ToString()) + "&sysid=" + utility.EncodeTo64(GblIQCare.SystemId.ToString())));
        }

        private void cmdPrintPrescription_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = new DataSet();
                IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataTable table1 = (DataTable)this.grdDrugDispense.DataSource;
                int num1 = 0;
                foreach (DataRow dataRow in (InternalDataCollectionBase)table1.Rows)
                {
                    if (dataRow["PrintPrescriptionStatus"].ToString() == "1")
                        num1 = 1;
                }
                if (num1 == 0)
                {
                    IQCareWindowMsgBox.ShowWindow("PrintPresCheck", (Control)this);
                }
                else
                {
                    this.tabDispense.TabPages[1].Focus();
                    this.thePharmacyMaster = drug.GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId);
                    this.grpBoxLastDispense.Visible = false;
                    DataTable table2 = new DataView(table1)
                    {
                        RowFilter = "PrintPrescriptionStatus='1'"
                    }.ToTable();
                    table2.TableName = "Table";
                    table2.Columns.Add(new DataColumn("Item Code", typeof(string))
                    {
                        DefaultValue = (object)""
                    });
                    table2.AcceptChanges();
                    dataSet.Tables.Add(table2);
                    int PharmacyID = this.theOrderId;
                    int num2 = (int)this.XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                    new DataView(this.XMLDS.Tables["mst_Facility"]).RowFilter = "FacilityId=" + (object)Convert.ToInt32(GblIQCare.AppLocationId);
                    new DataView(GblIQCare.dtModules).RowFilter = "ModuleId=" + (object)Convert.ToInt32(GblIQCare.AppLocationId);
                    DataSet prescriptionDetails = drug.GetPharmacyPrescriptionDetails(PharmacyID, Convert.ToInt32(this.thePatientId), 0);
                    for (int index = 0; index < prescriptionDetails.Tables.Count; ++index)
                    {
                        DataTable table3 = prescriptionDetails.Tables[index].Copy();
                        int num3 = index + 1;
                        table3.TableName = "Table" + (object)num3;
                        dataSet.Tables.Add(table3);
                    }
                    DataTable table4 = new DataTable();
                    table4.Columns.Add("FacilityImage", System.Type.GetType("System.Byte[]"));
                    DataRow row = table4.NewRow();
                    int num4 = 0;
                    if (File.Exists(GblIQCare.PresentationImagePath() + (dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString() != "" ? dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString() : "")))
                    {
                        FileStream fileStream = new FileStream(GblIQCare.PresentationImagePath() + dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString(), FileMode.Open);
                        BinaryReader binaryReader = new BinaryReader((Stream)fileStream);
                        byte[] numArray1 = new byte[fileStream.Length + 1L];
                        byte[] numArray2 = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));
                        row[0] = (object)numArray2;
                        table4.Rows.Add(row);
                        num4 = 1;
                        binaryReader.Close();
                        fileStream.Close();
                    }
                    dataSet.Tables.Add(table4);
                    //DataTable dataTable = new DataTable();
                    //DataTable personDispensingDrugs = drug.GetPersonDispensingDrugs(prescriptionDetails.Tables[6].Rows[0][0].ToString());
                   // personDispensingDrugs.TableName = "PersonDispensingDrugs";
                    //dataSet.Tables.Add(personDispensingDrugs);
                    dataSet.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\PatientPharmacyPrescription.xml");
                    frmReportViewer frmReportViewer = new frmReportViewer();
                    frmReportViewer.MdiParent = this.MdiParent;
                    frmReportViewer.Location = new Point(0, 0);
                    if (dataSet.Tables[3].Rows[0]["FacilityTemplate"].ToString() == "1")
                    {
                        rptKNHPrescription rptKnhPrescription = new rptKNHPrescription();
                        rptKnhPrescription.SetDataSource(dataSet);
                        rptKnhPrescription.SetParameterValue("EnrollmentID", (object)"");
                        rptKnhPrescription.SetParameterValue("PharmacyID", (object)PharmacyID.ToString());
                        rptKnhPrescription.SetParameterValue("ModuleName", (object)"");
                        rptKnhPrescription.SetParameterValue("Currency", (object)this.getCurrency().ToString());
                        rptKnhPrescription.SetParameterValue("FacilityName", (object)GblIQCare.AppLocation.ToString());
                        rptKnhPrescription.SetParameterValue("Imageflag", (object)num4.ToString());
                        frmReportViewer.crViewer.ReportSource = (object)rptKnhPrescription;
                    }
                    else
                    {
                        rptSimplePrescription simplePrescription = new rptSimplePrescription();
                        simplePrescription.SetDataSource(dataSet);
                        simplePrescription.SetParameterValue("EnrollmentID", (object)"");
                        simplePrescription.SetParameterValue("ModuleName", (object)"");
                        simplePrescription.SetParameterValue("Currency", (object)this.getCurrency().ToString());
                        simplePrescription.SetParameterValue("FacilityName", (object)GblIQCare.AppLocation.ToString());
                        simplePrescription.SetParameterValue("Imageflag", (object)num4.ToString());
                        frmReportViewer.crViewer.ReportSource = (object)simplePrescription;
                    }
                    frmReportViewer.Show();
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private string getCurrency()
        {
            DataSet dataSet = new DataSet();
            int num = (int)dataSet.ReadXml(GblIQCare.GetXMLPath() + "\\Currency.xml");
            string str = new DataView(dataSet.Tables[0])
            {
                RowFilter = ("Id=" + (object)Convert.ToInt32(GblIQCare.AppCountryId))
            }[0]["Name"].ToString();
            return str.Substring(str.LastIndexOf("(") + 1, 3);
        }

        private void label31_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void cmbGrdDrugDispense_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grdDrugDispense.CurrentCell.Value = (object)this.cmbGrdDrugDispense.Text;
            this.cmbGrdDrugDispense.Hide();
            DataView defaultView = this.thePharmacyMaster.Tables[1].DefaultView;
            defaultView.RowFilter = "Drug_Pk = '" + this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "' and BatchQty ='" + this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[5].Value.ToString() + "' and AvailQty is not null";
            DataTable dataTable = defaultView.ToTable();
            if (dataTable.Rows.Count == 0)
                return;
            this.qtyAvailableInBatch = Convert.ToDouble(dataTable.Rows[0][9].ToString());
            this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["ExpiryDate"].Value = (object)((DateTime)dataTable.Rows[0][8]).ToString(GblIQCare.AppDateFormat);
        }

        private void cmbGrdDrugDispenseFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grdDrugDispense.CurrentCell.Value = (object)this.cmbGrdDrugDispenseFreq.Text;
            this.cmbGrdDrugDispenseFreq.Hide();
            DataView defaultView = this.XMLPharDS.Tables["mst_Frequency"].DefaultView;
            int result;
            if (int.TryParse(this.cmbGrdDrugDispenseFreq.SelectedValue.ToString(), out result))
                defaultView.RowFilter = "Id = " + (object)result;
            DataTable dataTable = defaultView.ToTable();
            if (dataTable.Rows.Count != 0)
            {
                this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["freqMultiplier"].Value = (object)Convert.ToInt32(dataTable.Rows[0]["multiplier"].ToString());
                this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["FrequencyId"].Value = (object)Convert.ToInt32(dataTable.Rows[0]["ID"].ToString());
                if (this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[7].Value.ToString() != "" && this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[22].Value.ToString() != "" && this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[9].Value.ToString() != "")
                    this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[10].Value = (object)(Convert.ToDouble(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[7].Value.ToString()) * Convert.ToDouble(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[22].Value.ToString()) * Convert.ToDouble(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[9].Value.ToString()));
            }
            if (!(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value.ToString() != "") || !(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyPres"].Value.ToString() != "") || (Convert.ToDouble(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value.ToString()) <= Convert.ToDouble(this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyPres"].Value.ToString()) || MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel))
                return;
            this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Value = (object)0;
            this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells["QtyDisp"].Selected = true;
        }

        private void grdDrugDispense_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 11 && e.ColumnIndex != 9 && e.ColumnIndex != 7)
                return;
            if (this.grdDrugDispense.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString() != "")
            {
                this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value = !(this.grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Value.ToString() != "") || !(this.grdDrugDispense.Rows[e.RowIndex].Cells["freqMultiplier"].Value.ToString() != "") || !(this.grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Value.ToString() != "") ? (object)0 : (object)(Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["Dose"].Value.ToString()) * Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["freqMultiplier"].Value.ToString()) * Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["Duration"].Value.ToString()));
                if (!(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString() != "") || !(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value.ToString() != ""))
                    return;
                if (Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString()) > Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyPres"].Value.ToString()))
                {
                    if (MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value = (object)0;
                        this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Selected = true;
                        return;
                    }
                }
                else if (Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString()) > this.qtyAvailableInBatch)
                {
                    int num = (int)MessageBox.Show("You have entered Dispensed Qty more than the Qty available in Batch!");
                    this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value = (object)this.qtyAvailableInBatch;
                    return;
                }
                double num1 = Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["UnitSellingPrice"].Value.ToString()) * Convert.ToDouble(this.grdDrugDispense.Rows[e.RowIndex].Cells["QtyDisp"].Value.ToString());
                this.grdDrugDispense.Rows[e.RowIndex].Cells["SellingPrice"].Value = (object)num1.ToString();
                if (this.theFunded == 0)
                {
                    this.grdDrugDispense.Rows[e.RowIndex].Cells["BillAmount"].Value = (object)num1.ToString();
                    this.theBillAmt = Convert.ToDecimal(num1);
                    this.lblPayAmount.Text = (Convert.ToDecimal(this.lblPayAmount.Text) + this.theBillAmt).ToString();
                }
                else
                {
                    this.grdDrugDispense.Rows[e.RowIndex].Cells["BillAmount"].Value = (object)"0";
                    this.theBillAmt = new Decimal(0);
                    this.lblPayAmount.Text = (Convert.ToDecimal(this.lblPayAmount.Text) + this.theBillAmt).ToString();
                }
            }
            else
            {
                int num2 = (int)MessageBox.Show("Batch Number cannot be empty!");
            }
        }

        private void grdDrugDispense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void grdDrugDispense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cmbGrdDrugDispense.Hide();
            this.cmbGrdDrugDispenseFreq.Hide();
            if (this.grdDrugDispense.SelectedCells[0].ColumnIndex == 5 && this.makeGridEditable == "Yes")
            {
                DataView defaultView = this.thePharmacyMaster.Tables[1].DefaultView;
                defaultView.RowFilter = "Drug_Pk = '" + this.grdDrugDispense.Rows[this.grdDrugDispense.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "' and BatchNo <> ''";
                this.cmbGrdDrugDispense.DataSource = (object)defaultView.ToTable();
                this.cmbGrdDrugDispense.DisplayMember = "BatchQty";
                this.cmbGrdDrugDispense.ValueMember = "BatchId";
                this.cmbGrdDrugDispense.Location = this.grdDrugDispense.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                this.cmbGrdDrugDispense.Width = this.grdDrugDispense.CurrentCell.Size.Width;
                this.cmbGrdDrugDispense.Show();
            }
            if (this.grdDrugDispense.SelectedCells[0].ColumnIndex != 8 || !(this.makeGridEditable == "Yes"))
                return;
            this.fill_DrugFreq();
            this.cmbGrdDrugDispenseFreq.Location = this.grdDrugDispense.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            this.cmbGrdDrugDispenseFreq.Width = this.grdDrugDispense.CurrentCell.Size.Width;
            this.cmbGrdDrugDispenseFreq.Show();
        }

        public void fill_DrugFreq()
        {
            this.XMLPharDS.Clear();
            int num = (int)this.XMLPharDS.ReadXml(GblIQCare.GetXMLPath() + "\\DrugMasters.con");
            new BindFunctions().Win_BindCombo(this.cmbGrdDrugDispenseFreq, new DataView(this.XMLPharDS.Tables["mst_Frequency"])
            {
                RowFilter = "(DeleteFlag =0 or DeleteFlag is null)"
            }.ToTable(), "Name", "Id");
        }

        private void grdDrugDispense_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress += new KeyPressEventHandler(this.Control_KeyPress);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (int)e.KeyChar != 46)
                e.Handled = true;
            if ((int)e.KeyChar != 46 || (sender as TextBox).Text.IndexOf('.') <= -1)
                return;
            e.Handled = true;
        }

        private void dtRefillApp_Enter(object sender, EventArgs e)
        {
            this.dtRefillApp.CustomFormat = "dd-MMM-yyyy";
        }

        private void chkPharmacyRefill_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkPharmacyRefill.Checked)
            {
                this.dtRefillApp.CustomFormat = " ";
                this.dtRefillApp.Enabled = false;
            }
            else
            {
                this.dtRefillApp.CustomFormat = "dd-MMM-yyyy";
                this.dtRefillApp.Enabled = true;
            }
        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
           /* GblIQCare.dtPrintLabel = (DataTable)this.grdDrugDispense.DataSource;
            GblIQCare.PatientName = this.lblPatientName.Text;
            GblIQCare.IQNumber = this.lblIQNumber.Text;
            GblIQCare.StoreName = this.lblStoreName.Text;
            if (GblIQCare.dtPrintLabel.Rows.Count > 0 || GblIQCare.dtPrintLabel == null)
            {
                Form form = (Form)Activator.CreateInstance(System.Type.GetType("IQCare.SCM.frmPrintLabel, IQCare.SCM"));
                form.MdiParent = this.MdiParent;
                form.Left = 0;
                form.Top = 0;
                form.Focus();
                form.Show();
            }
            else
            {
                int num = (int)MessageBox.Show("Please select drugs");
            }*/
        }

        private void dtDispensedDate_ValueChanged(object sender, EventArgs e)
        {
            if (!(this.dtDispensedDate.Value > DateTime.Today))
                return;
            int num = (int)MessageBox.Show("Dispensing date cannot be greater than current date.");
            this.dtDispensedDate.Value = DateTime.Today;
        }

       
    }
}
