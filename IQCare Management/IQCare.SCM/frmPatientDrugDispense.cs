using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Entities.Billing;
using Interface.Clinical;
using Interface.FormBuilder;
using Interface.SCM;
using System.Configuration;
using System.Collections.Generic;
using Interface.Billing;

namespace IQCare.SCM
{
    /// <summary>
    ///
    /// </summary>
    public partial class frmPatientDrugDispense : Form
    {
        #region "Variables"

        //////////////////////////
        /// <summary>
        /// The image list1
        /// </summary>
        private ImageList imageList1 = new ImageList();
        private string makeGridEditable = "";
        private string lastdispensedARV = "";
        private string ARVBeingDispensed = "";
        /// <summary>
        /// The int process
        /// </summary>
        //private Int32 IntProcess = 0;

        private bool priceBundled = false;

        /// <summary>
        /// The avail qty
        /// </summary>
        private Int32 theAvailQty = 0;

        /// <summary>
        /// The batch identifier
        /// </summary>
        private Int32 theBatchId = 0;

        /// <summary>
        /// The bill amt
        /// </summary>
        private decimal theBillAmt = 0;

        /// <summary>
        /// The configuration selling price
        /// </summary>
        private decimal theConfigSellingPrice = 0;

        /// <summary>
        /// The cost price
        /// </summary>
        private decimal theCostPrice = 0;

        /// <summary>
        /// The current row
        /// </summary>
        private Int32 theCurrentRow = 0;

        /// <summary>
        /// The disp current row
        /// </summary>
        private Int32 theDispCurrentRow = -1;

        /// <summary>
        /// The dispensing unit
        /// </summary>
        private Int32 theDispensingUnit = 0;

        /// <summary>
        /// The dispensing unit name
        /// </summary>
        private string theDispensingUnitName = "";

        /// <summary>
        /// The dob
        /// </summary>
        private DateTime theDOB;

        /// <summary>
        /// The existing drugs
        /// </summary>
        private DataTable theExistingDrugs = new DataTable();

        /// <summary>
        /// Dispensing Variables
        /// </summary>
        private Int32 theFunded = 0;

        /// <summary>
        /// The generic abb
        /// </summary>
        private string theGenericAbb = "";

        /// <summary>
        /// The item identifier
        /// </summary>
        private Int32 theItemId = 0;

        /// <summary>
        /// The item type
        /// </summary>
        private Int32 theItemType = 0;

        private int theItemTypeId = 0;

        /// <summary>
        /// The margin
        /// </summary>
        private decimal theMargin = 0;

        /// <summary>
        /// The order identifier
        /// </summary>
        private Int32 theOrderId = 0;

        /// <summary>
        /// The order status
        /// </summary>
        private String theOrderStatus = "";

        /// <summary>
        /// The patient identifier
        /// </summary>
        private Int32 thePatientId = 0;

        /// <summary>
        /// The pharmacy master
        /// </summary>
        private DataSet thePharmacyMaster = new DataSet();

        /// <summary>
        /// The precribe amt
        /// </summary>
        private decimal thePrecribeAmt = 0;

        /// <summary>
        /// The theprevbatch identifier
        /// </summary>
        private int theprevbatchId = 0;

        /// <summary>
        /// The prophylaxis
        /// </summary>
        private int theProphylaxis = 0;

        /// <summary>
        /// The return order identifier
        /// </summary>
        private int theReturnOrderId = 0;

        ////////////
        /// <summary>
        /// GridControls
        /// </summary>
        private TextBox theReturnQty = new TextBox();

        /// <summary>
        /// The selling price
        /// </summary>
        private decimal theSellingPrice = 0;

        /// <summary>
        /// The strength
        /// </summary>
        private int theStrength = 0;

        /// <summary>
        /// The XMLDS
        /// </summary>
        private DataSet XMLDS = new DataSet(); /// for All Masters ////

        /// <summary>
        /// The XML phar ds
        /// </summary>
        private DataSet XMLPharDS = new DataSet(); ///for Pharmacy Masters///

        #endregion "Variables"

        /// <summary>
        /// The default date time picker format
        /// </summary>
        private DateTimePickerFormat defaultDateTimePickerFormat = DateTimePickerFormat.Custom;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmPatientDrugDispense"/> class.
        /// </summary>
        public frmPatientDrugDispense()
        {
            InitializeComponent();
            grdDrugDispense.CellPainting += new DataGridViewCellPaintingEventHandler(grdDrugDispense_CellPainting);
            dtRefillApp.Format = dtpDOB.Format = DateTimePickerFormat.Custom;
            dtRefillApp.CustomFormat = dtpDOB.CustomFormat = " ";
        }

        /// <summary>
        /// Saves the update art.
        /// </summary>
        /// <param name="OrderID">The order identifier.</param>
        public void SaveUpdateArt(int OrderID)
        {
            {
               
                IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                string str = !(this.NxtAppDate.CustomFormat == " ") ? (!(this.NxtAppDate.Text == "") ? this.NxtAppDate.Text : "01-01-1900") : "01-01-1900";
                string weight = !(this.txtWeight.Text == "") ? this.txtWeight.Text : "0";
                string height = !(this.txtHeight.Text == "") ? this.txtHeight.Text : "0";
                drug.SaveHivTreatementPharmacyField(OrderID, weight, height, Convert.ToInt32(this.cmbprogram.SelectedValue), Convert.ToInt32(this.cmdPeriodTaken.SelectedValue), Convert.ToInt32(this.cmbProvider.SelectedValue), Convert.ToInt32(this.cmbRegimenLine.SelectedValue), Convert.ToDateTime(str), Convert.ToInt32(this.cmbReason.SelectedValue));

                return;


            }
        }

        /// <summary>
        /// Authentications this instance.
        /// </summary>
        private void Authentication()
        {
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Binds the combo.
        /// </summary>
        private void BindCombo()
        {
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            XMLPharDS.ReadXml(GblIQCare.GetXMLPath() + "\\DrugMasters.con");
            BindFunctions theBindManager = new BindFunctions();

            DataView theDV = new DataView(XMLDS.Tables["mst_Facility"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theFacilityDT = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbFacility, theFacilityDT, "FacilityName", "FacilityId");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 4 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theGender = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbSex, theGender, "Name", "Id");

            theDV.RowFilter = "CodeId = 33 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable thePharProg = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbprogram, thePharProg, "Name", "Id");

            theDV = new DataView(XMLPharDS.Tables["mst_Frequency"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDT = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbFrequency, theDT, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_Provider"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTProvider = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbProvider, theDTProvider, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_RegimenLine"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTRegimenLine = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbRegimenLine, theDTRegimenLine, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 26 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theDTReason = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbReason, theDTReason, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["mst_Decode"]);
            theDV.RowFilter = "CodeId = 31 and (DeleteFlag =0 or DeleteFlag is null) and id in(140,141,142)";
            DataTable thePeriodTaken = theDV.ToTable();
            theBindManager.Win_BindCombo(cmdPeriodTaken, thePeriodTaken, "Name", "Id");

            IViewAssociation objViewAssociation = (IViewAssociation)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BViewAssociation,BusinessProcess.FormBuilder");
            DataSet objDsViewAssociation = objViewAssociation.GetMoudleName();
            DataTable dt;
            dt = objDsViewAssociation.Tables[0];
            DataRow drAddSelect;
            drAddSelect = dt.NewRow();
            drAddSelect["ModuleName"] = "All";
            drAddSelect["ModuleID"] = 0;
            dt.Rows.InsertAt(drAddSelect, 0);
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbService, dt, "ModuleName", "ModuleId");
        }

        /// <summary>
        /// Binds the drug retun detail grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindDrugRetunDetailGrid(DataTable theDT)
        {
            grdReturnDetail.DataSource = null;
            grdReturnDetail.Columns.Clear();
            grdReturnDetail.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "ItemId";
            theCol1.DataPropertyName = "ItemId";
            theCol1.Width = 10;
            theCol1.Visible = false;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "Drug Name";
            theCol2.DataPropertyName = "ItemName";
            theCol2.Width = 200;
            theCol2.ReadOnly = true;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "DispUnitId";
            theCol3.DataPropertyName = "DispensingUnitId";
            theCol3.Width = 10;
            theCol3.Visible = false;

            DataGridViewTextBoxColumn theCol4 = new DataGridViewTextBoxColumn();
            theCol4.HeaderText = "Dispensing Unit";
            theCol4.DataPropertyName = "DispensingUnitName";
            theCol4.Width = 80;
            theCol4.ReadOnly = true;

            DataGridViewTextBoxColumn theCol5 = new DataGridViewTextBoxColumn();
            theCol5.HeaderText = "BatchId";
            theCol5.DataPropertyName = "BatchId";
            theCol5.Width = 10;
            theCol5.Visible = false;

            DataGridViewTextBoxColumn theCol6 = new DataGridViewTextBoxColumn();
            theCol6.HeaderText = "Batch No";
            theCol6.DataPropertyName = "BatchNo";
            theCol6.Width = 80;
            theCol6.ReadOnly = true;

            DataGridViewTextBoxColumn theCol7 = new DataGridViewTextBoxColumn();
            theCol7.HeaderText = "Expiry Date";
            theCol7.DataPropertyName = "ExpiryDate";
            theCol7.Width = 80;
            theCol7.ReadOnly = true;

            DataGridViewTextBoxColumn theCol8 = new DataGridViewTextBoxColumn();
            theCol8.HeaderText = "Quantity";
            theCol8.DataPropertyName = "QtyDisp";
            theCol8.Width = 80;
            theCol8.ReadOnly = true;

            DataGridViewTextBoxColumn theCol9 = new DataGridViewTextBoxColumn();
            theCol9.HeaderText = "CostPrice";
            theCol9.DataPropertyName = "CostPrice";
            theCol9.Width = 10;
            theCol9.Visible = false;

            DataGridViewTextBoxColumn theCol10 = new DataGridViewTextBoxColumn();
            theCol10.HeaderText = "Margin";
            theCol10.DataPropertyName = "Margin";
            theCol10.Width = 10;
            theCol10.Visible = false;

            DataGridViewTextBoxColumn theCol11 = new DataGridViewTextBoxColumn();
            theCol11.HeaderText = "Selling Price";
            theCol11.DataPropertyName = "SellingPrice";
            theCol11.Width = 80;
            theCol11.Visible = false;

            DataGridViewTextBoxColumn theCol12 = new DataGridViewTextBoxColumn();
            theCol12.HeaderText = "Bill Amount";
            theCol12.DataPropertyName = "BillAmount";
            theCol12.Width = 80;
            theCol12.Visible = false;

            DataGridViewTextBoxColumn theCol13 = new DataGridViewTextBoxColumn();
            theCol13.HeaderText = "StrengthId";
            theCol13.DataPropertyName = "StrengthId";
            theCol13.Width = 10;
            theCol13.Visible = false;

            DataGridViewTextBoxColumn theCol14 = new DataGridViewTextBoxColumn();
            theCol14.HeaderText = "Frequency";
            theCol14.DataPropertyName = "FrequencyId";
            theCol14.Width = 10;
            theCol14.Visible = false;

            DataGridViewTextBoxColumn theCol15 = new DataGridViewTextBoxColumn();
            theCol15.HeaderText = "Frequency";
            theCol15.DataPropertyName = "FrequencyName";
            theCol15.Width = 80;
            theCol15.Visible = false;

            DataGridViewTextBoxColumn theCol16 = new DataGridViewTextBoxColumn();
            theCol16.HeaderText = "Quantity Return";
            theCol16.DataPropertyName = "ReturnQty";
            theCol16.Width = 80;

            DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId = 204 and (DeleteFlag = 0 or DeleteFlag is null)";
            DataTable theReturnReasonDT = theDV.ToTable();
            DataGridViewComboBoxColumn theCol17 = new DataGridViewComboBoxColumn();
            theCol17.HeaderText = "Return Reason";
            theCol17.ValueMember = "Id";
            theCol17.DisplayMember = "Name";
            theCol17.DataSource = theReturnReasonDT;
            theCol17.DataPropertyName = "ReturnReason";
            theCol17.Width = 150;

            grdReturnDetail.Columns.Add(theCol1);
            grdReturnDetail.Columns.Add(theCol2);
            grdReturnDetail.Columns.Add(theCol3);
            grdReturnDetail.Columns.Add(theCol4);
            grdReturnDetail.Columns.Add(theCol5);
            grdReturnDetail.Columns.Add(theCol6);
            grdReturnDetail.Columns.Add(theCol7);
            grdReturnDetail.Columns.Add(theCol8);
            grdReturnDetail.Columns.Add(theCol15);
            grdReturnDetail.Columns.Add(theCol9);
            grdReturnDetail.Columns.Add(theCol10);
            grdReturnDetail.Columns.Add(theCol11);
            grdReturnDetail.Columns.Add(theCol12);
            grdReturnDetail.Columns.Add(theCol13);
            grdReturnDetail.Columns.Add(theCol14);
            grdReturnDetail.Columns.Add(theCol16);
            grdReturnDetail.Columns.Add(theCol17);

            grdReturnDetail.DataSource = theDT;
        }

        /// <summary>
        /// Binds the drug return grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindDrugReturnGrid(DataTable theDT)
        {
            grdReturnOrder.DataSource = null;
            grdReturnOrder.Columns.Clear();
            grdReturnOrder.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn theCol1 = new DataGridViewTextBoxColumn();
            theCol1.HeaderText = "Transaction Date";
           theCol1.Name= theCol1.DataPropertyName = "TransactionDate";
            theCol1.Width = 200;

            DataGridViewTextBoxColumn theCol2 = new DataGridViewTextBoxColumn();
            theCol2.HeaderText = "Status";
          theCol2.Name=  theCol2.DataPropertyName = "Status";
            theCol2.Width = 200;

            DataGridViewTextBoxColumn theCol3 = new DataGridViewTextBoxColumn();
            theCol3.HeaderText = "Id";
          theCol3.Name=  theCol3.DataPropertyName = "Ptn_Pharmacy_Pk";
            theCol3.Width = 10;
            theCol3.Visible = false;

            grdReturnOrder.Columns.Add(theCol1);
            grdReturnOrder.Columns.Add(theCol2);
            grdReturnOrder.Columns.Add(theCol3);
            grdReturnOrder.DataSource = theDT;
        }

        /// <summary>
        /// Binds the exiting grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindExitingGrid(DataTable theDT)
        {
            grdExitingPharDisp.DataSource = null;
            grdExitingPharDisp.Columns.Clear();
            grdExitingPharDisp.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn colTransactionDate = new DataGridViewTextBoxColumn();
            colTransactionDate.HeaderText = "Transaction Date";
            colTransactionDate.Name = colTransactionDate.DataPropertyName = "TransactionDate";
            colTransactionDate.DefaultCellStyle.Format = "dd-MMM-yyyy";
            colTransactionDate.Width = 200;

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.HeaderText = "Status";
            colStatus.Name = colStatus.DataPropertyName = "Status";
            colStatus.Width = 200;

            DataGridViewTextBoxColumn colOrderId = new DataGridViewTextBoxColumn();
            colOrderId.HeaderText = "Id";
            colOrderId.Name = colOrderId.DataPropertyName = "Ptn_Pharmacy_Pk";
            colOrderId.Width = 10;
            colOrderId.Visible = false;

            grdExitingPharDisp.Columns.Add(colTransactionDate);
            grdExitingPharDisp.Columns.Add(colStatus);
            grdExitingPharDisp.Columns.Add(colOrderId);
            // theDT.DefaultView.Sort = "TransactionDate Desc";
            grdExitingPharDisp.DataSource = theDT;
        }

        /// <summary>
        /// Binds the pharmacy dispense grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindPharmacyDispenseGrid(DataTable theDT)
        {
            try
            {
                grdDrugDispense.DataSource = null;
                grdDrugDispense.Columns.Clear();
                grdDrugDispense.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn colItemId = new DataGridViewTextBoxColumn();
                colItemId.HeaderText = "ItemId";
                colItemId.Name = colItemId.DataPropertyName = "ItemId";
                colItemId.Width = 5;
                colItemId.Visible = false;
                colItemId.ReadOnly = true;

                DataGridViewTextBoxColumn colDrugName = new DataGridViewTextBoxColumn();
                colDrugName.HeaderText = "Drug Name";
                colDrugName.Name = colDrugName.DataPropertyName = "ItemName";
                colDrugName.Width = 325;
                colDrugName.ReadOnly = true;

                DataGridViewTextBoxColumn colDispenseUnitId = new DataGridViewTextBoxColumn();
                colDispenseUnitId.HeaderText = "DispUnitId";
                colDispenseUnitId.Name = colDispenseUnitId.DataPropertyName = "DispensingUnitId";
                colDispenseUnitId.Width = 5;
                colDispenseUnitId.Visible = false;
                colDispenseUnitId.ReadOnly = true;

                DataGridViewTextBoxColumn colDispenseUnit = new DataGridViewTextBoxColumn();
                colDispenseUnit.HeaderText = "Dispensing Unit";
                colDispenseUnit.Name = colDispenseUnit.DataPropertyName = "DispensingUnitName";
                colDispenseUnit.Width = 80;
                colDispenseUnit.ReadOnly = true;

                DataGridViewTextBoxColumn colBatchId = new DataGridViewTextBoxColumn();
                colBatchId.HeaderText = "BatchId";
                colBatchId.Name = colBatchId.DataPropertyName = "BatchId";
                colBatchId.Width = 5;
                colBatchId.Visible = false;
                colBatchId.ReadOnly = true;

                DataGridViewTextBoxColumn colBatch = new DataGridViewTextBoxColumn();
                colBatch.HeaderText = "Batch No";
                colBatch.Name = colBatch.DataPropertyName = "BatchNo";
                colBatch.Width = 100;
                colBatch.ReadOnly = true;

                DataGridViewTextBoxColumn colExpiryDate = new DataGridViewTextBoxColumn();
                colExpiryDate.HeaderText = "Expiry Date";
                colExpiryDate.DataPropertyName = "ExpiryDate";
                colExpiryDate.Width = 100;
                colExpiryDate.ReadOnly = true;

                DataGridViewTextBoxColumn colQtyDispensed = new DataGridViewTextBoxColumn();
                colQtyDispensed.HeaderText = "Qty Dispensed";
                colExpiryDate.Name = colQtyDispensed.DataPropertyName = "QtyDisp";
                colQtyDispensed.Width = 80;
                if (!(this.makeGridEditable == "Yes"))
                    colQtyDispensed.ReadOnly = true;

                DataGridViewTextBoxColumn colCostPrice = new DataGridViewTextBoxColumn();
                colCostPrice.HeaderText = "CostPrice";
                colCostPrice.Name = colCostPrice.DataPropertyName = "CostPrice";
                colCostPrice.Width = 5;
                colCostPrice.Visible = false;
                colCostPrice.ReadOnly = true;

                DataGridViewTextBoxColumn colMargin = new DataGridViewTextBoxColumn();
                colMargin.HeaderText = "Margin";
                colMargin.Name = colMargin.DataPropertyName = "Margin";
                colMargin.Width = 5;
                colMargin.Visible = false;
                colMargin.ReadOnly = true;

                DataGridViewTextBoxColumn colSellingPrice = new DataGridViewTextBoxColumn();
                colSellingPrice.HeaderText = "Selling Price";
                colSellingPrice.Name = colSellingPrice.DataPropertyName = "SellingPrice";
                colSellingPrice.Width = 80;
                colSellingPrice.ReadOnly = true;

                DataGridViewTextBoxColumn colBillAmount = new DataGridViewTextBoxColumn();
                colBillAmount.HeaderText = "Bill Amount";
                colBillAmount.Name = colBillAmount.DataPropertyName = "BillAmount";
                colBillAmount.Width = 80;
                colBillAmount.ReadOnly = true;

                DataGridViewTextBoxColumn colStrengthId = new DataGridViewTextBoxColumn();
                colStrengthId.HeaderText = "StrengthId";
                colStrengthId.Name = colStrengthId.DataPropertyName = "StrengthId";
                colStrengthId.Width = 5;
                colStrengthId.Visible = false;
                colStrengthId.ReadOnly = true;

                DataGridViewTextBoxColumn colFrequencyId = new DataGridViewTextBoxColumn();
                colFrequencyId.HeaderText = "Frequency";
                colFrequencyId.Name = colFrequencyId.DataPropertyName = "FrequencyId";
                colFrequencyId.Width = 5;
                colFrequencyId.Visible = false;
                colFrequencyId.ReadOnly = true;

                DataGridViewTextBoxColumn colFrequency = new DataGridViewTextBoxColumn();
                colFrequency.HeaderText = "Frequency";
                colFrequency.Name = colFrequency.DataPropertyName = "FrequencyName";
                colFrequency.Width = 85;
                colFrequency.ReadOnly = true;

                //todo

                DataGridViewTextBoxColumn colQtyPrescribed = new DataGridViewTextBoxColumn();
                colQtyPrescribed.HeaderText = "Qty Prescribed";
                colQtyPrescribed.Name = colQtyPrescribed.DataPropertyName = "OrderedQuantity";
                colQtyPrescribed.Width = 80;
                colQtyPrescribed.ReadOnly = true;

                DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
                colStatus.HeaderText = "DataStatus";
                colStatus.Name = colStatus.DataPropertyName = "DataStatus";
                colStatus.Width = 5;
                colStatus.Visible = false;
                colStatus.ReadOnly = true;

                DataGridViewTextBoxColumn colDose = new DataGridViewTextBoxColumn();
                colDose.HeaderText = "Dose";
                colDose.Name = colDose.DataPropertyName = "Dose";
                colDose.Width = 50;
                if (!(this.makeGridEditable == "Yes"))
                    colDose.ReadOnly = true;

                DataGridViewTextBoxColumn colDuration = new DataGridViewTextBoxColumn();
                colDuration.HeaderText = "Duration (days)";
                colDuration.Name = colDuration.DataPropertyName = "Duration";
                colDuration.Width = 60;
                if (!(this.makeGridEditable == "Yes"))
                    colDuration.ReadOnly = true;

                DataGridViewCheckBoxColumn colPrint = new DataGridViewCheckBoxColumn();
                colPrint.HeaderText = "Print";
                colPrint.Name = colPrint.DataPropertyName = "PrintPrescriptionStatus";
                colPrint.Width = 50;

                DataGridViewTextBoxColumn colInstruction = new DataGridViewTextBoxColumn();
                colInstruction.HeaderText = "Patient Instructions";
                colInstruction.Name = colInstruction.DataPropertyName = "PatientInstructions";
                colInstruction.Width = 5;
                colInstruction.Visible = false;

                DataGridViewTextBoxColumn colWhyPartial = new DataGridViewTextBoxColumn();
                colWhyPartial.HeaderText = "Why Partial";
                colWhyPartial.Name = colWhyPartial.DataPropertyName = "WhyPartial";
                colWhyPartial.Width = 2;
                colWhyPartial.Visible = false;

                //DataGridViewImageColumn theCol22 = new DataGridViewImageColumn();
                //theCol22.Width = 25;
                //theCol22.Image = Image.FromFile(GblIQCare.GetPath() + "\\No_16x.ico");

                grdDrugDispense.Columns.Add(colItemId);
                grdDrugDispense.Columns.Add(colDrugName);
                grdDrugDispense.Columns.Add(colDispenseUnitId);
                grdDrugDispense.Columns.Add(colDispenseUnit);
                grdDrugDispense.Columns.Add(colBatchId);
                grdDrugDispense.Columns.Add(colBatch);
                grdDrugDispense.Columns.Add(colExpiryDate);
                // Dose
                grdDrugDispense.Columns.Add(colDose);

                grdDrugDispense.Columns.Add(colFrequencyId);
                grdDrugDispense.Columns.Add(colDuration);

                grdDrugDispense.Columns.Add(colQtyPrescribed);
                grdDrugDispense.Columns.Add(colQtyDispensed);
                // add
                grdDrugDispense.Columns.Add(colCostPrice);
                grdDrugDispense.Columns.Add(colMargin);
                grdDrugDispense.Columns.Add(colSellingPrice);
                grdDrugDispense.Columns.Add(colBillAmount);
                grdDrugDispense.Columns.Add(colStrengthId);
                grdDrugDispense.Columns.Add(colFrequency);

                //todo
                //  grdDrugDispense.Columns.Add(theCol16);
                grdDrugDispense.Columns.Add(colStatus);
                grdDrugDispense.Columns.Add(colPrint);
                grdDrugDispense.Columns.Add(colInstruction);
                grdDrugDispense.Columns.Add(colWhyPartial);
                //grdDrugDispense.Columns.Add(theCol22);
                grdDrugDispense.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Binds the search grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindSearchGrid(DataTable theDT)
        {
            grdResultView.DataSource = null;
            grdResultView.AutoGenerateColumns = false;
            /*
             * grdResultView.Columns.Clear();

             DataGridViewTextBoxColumn colLastName = new DataGridViewTextBoxColumn();
             colLastName.HeaderText = "Last Name";
             colLastName.Name = colLastName.DataPropertyName = "lastname";
             colLastName.Width = 200;

             DataGridViewTextBoxColumn colFirstName = new DataGridViewTextBoxColumn();
             colFirstName.HeaderText = "First Name";
             colFirstName.Name = colFirstName.DataPropertyName = "firstname";
             colFirstName.Width = 200;

             DataGridViewTextBoxColumn colMidName = new DataGridViewTextBoxColumn();
             colMidName.HeaderText = "Middle Name";
             colMidName.Name = colMidName.DataPropertyName = "Middlename";
             colMidName.Width = 200;

             DataGridViewTextBoxColumn colPatientNumber = new DataGridViewTextBoxColumn();
             colPatientNumber.HeaderText = "Patient #";
             colPatientNumber.Name = colPatientNumber.DataPropertyName = "PatientFacilityId";
             colPatientNumber.Width = 150;

             DataGridViewTextBoxColumn colSex = new DataGridViewTextBoxColumn();
             colSex.HeaderText = "Sex";
             colSex.Name = colSex.DataPropertyName = "Sex";
             colSex.Width = 50;

             DataGridViewTextBoxColumn colDOB = new DataGridViewTextBoxColumn();
             colDOB.HeaderText = "Date of Birth";
             colDOB.Name = colDOB.DataPropertyName = "dob";
             colDOB.Width = 120;

             DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
             colStatus.HeaderText = "Status";
             colStatus.Name = colStatus.DataPropertyName = "status";
             colStatus.Width = 60;

             DataGridViewTextBoxColumn colFacilityName = new DataGridViewTextBoxColumn();
             colFacilityName.HeaderText = "Patient Location";
             colFacilityName.Name = colFacilityName.DataPropertyName = "FacilityName";
             colFacilityName.Width = 130;
             colFacilityName.Visible = false;

             DataGridViewTextBoxColumn colPatientId = new DataGridViewTextBoxColumn();
             colPatientId.HeaderText = "Ptn_Pk";
             colPatientId.Name = colPatientId.DataPropertyName = "PatientId";
             colPatientId.Width = 10;
             colPatientId.Visible = false;

             grdResultView.Columns.Add(colFirstName);
             grdResultView.Columns.Add(colMidName);
             grdResultView.Columns.Add(colLastName);

             grdResultView.Columns.Add(colPatientNumber);
             grdResultView.Columns.Add(colSex);
             grdResultView.Columns.Add(colDOB);
             grdResultView.Columns.Add(colStatus);
             grdResultView.Columns.Add(colFacilityName);
             grdResultView.Columns.Add(colPatientId);
            // grdResultView.Columns.Add(theCol9);
             //grdResultView.Columns.Add(theCol10);*/
            grdResultView.Columns["dob"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            grdResultView.DataSource = theDT;
        }

        /// <summary>
        /// Handles the Click event of the btnART control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnART_Click(object sender, EventArgs e)
        {
            grpHivCareTrtPharmacyField.Visible = true;
            grpHivCareTrtPharmacyField.Left = btnART.Left;
            grpHivCareTrtPharmacyField.Top = btnART.Top + 120;

        }

        /// <summary>
        /// Handles the Click event of the btnClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemName.Text = "";
            txtDose.Text = "";
            txtDuration.Text = "";
            txtQtyPrescribed.Text = "";
            txtQtyDispensed.Text = "";
            txtBatchNo.Text = "";
            txtExpirydate.Text = "";
            txtSellingPrice.Text = "";
            cmbFrequency.SelectedValue = "0";
            //chkPrintPrescription.Checked = false;
            txtPatientInstructions.Text = "";
        }

        /// <summary>
        /// Handles the Click event of the btncopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btncopy_Click(object sender, EventArgs e)
        {
            //Utility theUtil = new Utility();
            //string address = GblIQCare.weburl();


            //ProcessStartInfo sInfo = new ProcessStartInfo(urlpath + "?loc=w&iqnum="
            //    + theUtil.EncodeTo64(lblIQNumber.Text) + "&AppName=" + theUtil.EncodeTo64(GblIQCare.AppUName) + "&apploc="
            //    + theUtil.EncodeTo64(GblIQCare.AppLocationId.ToString())
            //    + "&sysid=" + theUtil.EncodeTo64(GblIQCare.SystemId.ToString()) + "");
            //Process.Start(sInfo);
        }

        /// <summary>
        /// Handles the Click event of the btnDispenseSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDispenseSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                if (ValidateDrugDispense() == false)
                    return;

                if (txtQtyPrescribed.Text.Trim() != "" && txtQtyDispensed.Text.Trim() != "")
                {
                    decimal qtydis = Convert.ToDecimal(txtQtyDispensed.Text.Trim());
                    decimal qtypre = Convert.ToDecimal(txtQtyPrescribed.Text.Trim());
                    if (qtydis > qtypre)
                    {
                        if (MessageBox.Show("You have entered Dispensed Qty more than the Prescribed Qty" +
                            "\nDo you want to Continue?", "IQCare Management", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                if (txtItemName.Enabled == false)
                {
                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    DataView theDV = new DataView(theDT);
                    theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + " and ExpiryDate='" + txtExpirydate.Text + "'";
                    if (theDV.Count > 0)
                    {
                        theDV[0]["QtyDisp"] = txtQtyDispensed.Text;
                        theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                        theDV[0]["FrequencyName"] = cmbFrequency.Text;
                        theDV[0]["SellingPrice"] = txtSellingPrice.Text;
                        theDV[0]["WhyPartial"] = txtWhyPartial.Text;
                        //if (chkPrintPrescription.Checked)
                        //{
                            theDV[0]["PrintPrescriptionStatus"] = "1";
                        //}
                        //else
                        //{
                        //    theDV[0]["PrintPrescriptionStatus"] = "0";
                        //}

                        theDV[0]["PatientInstructions"] = txtPatientInstructions.Text;
                    }
                    txtItemName.Enabled = true;
                    BindPharmacyDispenseGrid(theDT);
                }
                else
                {
                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    if (theOrderStatus == "New Order")
                    {
                        DataRow[] theRmDR = theDT.Select("ItemId = " + theItemId.ToString());
                        if (theRmDR.Length > 0)
                        {
                            if (Convert.ToString(theRmDR[0]["BatchId"]) != "" && theprevbatchId == 0)
                            {
                                if (theRmDR[0]["BatchId"].ToString() == theBatchId.ToString())
                                {
                                    IQCareWindowMsgBox.ShowWindow("BatchExists", this);
                                    return;
                                }
                                DataTable dtrow = theRmDR.CopyToDataTable();
                                DataRow[] theRmFillDR = dtrow.Select("BatchId = " + theBatchId.ToString());
                                if (theRmFillDR.Length > 0)
                                {
                                    theDT.Rows.Remove(theRmFillDR[0]);
                                }
                            }
                            else
                                theDT.Rows.Remove(theRmDR[0]);
                        }
                    }

                    // Check Total Quantity  (Available qty and enter Quantity)
                    if (thePrecribeAmt != 0)
                    {
                        if (theOrderStatus == "Partial Dispense")
                        {
                            decimal totalQuantity = 0;
                            decimal totalItemQty = 0;
                            DataTable thePartialDT = theDT;
                            DataView dv = thePartialDT.DefaultView;
                            dv.RowFilter = "ItemId = " + theItemId.ToString();
                            DataTable dtnew = dv.ToTable();
                            foreach (DataRow dr in dtnew.Rows)
                            {
                                totalItemQty = totalItemQty + Convert.ToDecimal(dr[10].ToString());
                            }
                            if (dtnew.Rows.Count == 1 && totalItemQty == 0)
                            {
                                DataRow[] theRmDR = theDT.Select("ItemId = " + theItemId.ToString());
                                theDT.Rows.Remove(theRmDR[0]);
                                string s = txtQtyDispensed.Text;
                                if (s.IndexOf('.', 0) != -1)
                                {
                                    s = s.Substring(0, s.IndexOf('.', 0));
                                }
                                txtQtyDispensed.Text = s;
                                if (txtQtyDispensed.Text == "0")
                                {
                                    txtQtyDispensed.Focus();
                                    IQCareWindowMsgBox.ShowWindowConfirm("QuanitytData", this);
                                    return;
                                }
                            }
                            totalQuantity = totalItemQty + Convert.ToDecimal(txtQtyDispensed.Text);
                            if (totalQuantity > thePrecribeAmt)
                            {
                                IQCareWindowMsgBox.ShowWindowConfirm("QuanitytDetails", this);
                                return;
                            }
                        }
                    }
                    if ((theDispCurrentRow > -1) && (theDT.Rows.Count > 0))
                    {
                        DataView theDV = new DataView(theDT);
                        theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + " and ExpiryDate='" + txtExpirydate.Text.Replace("-", " ") + "'";
                        //theDV.RowFilter = "ItemId = " + theItemId.ToString() + " and BatchId = " + theBatchId.ToString() + "";
                        if (theDV.Count > 0)
                        {
                            theDV[0]["QtyDisp"] = txtQtyDispensed.Text;
                            theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            theDV[0]["FrequencyName"] = cmbFrequency.Text;
                            theDV[0]["SellingPrice"] = txtSellingPrice.Text;
                            theDV[0]["WhyPartial"] = txtWhyPartial.Text;
                            theDV[0]["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();

                            theDV[0]["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            theDV[0]["FrequencyName"] = cmbFrequency.Text;
                            theDV[0]["Prophylaxis"] = theProphylaxis;
                            theDV[0]["ItemType"] = theItemType;
                            theDV[0]["GenericAbb"] = theGenericAbb;
                            theDV[0]["Dose"] = txtDose.Text;
                            theDV[0]["Duration"] = txtDuration.Text;
                            theDV[0]["BillAmount"] = theBillAmt;
                            theDV[0]["CostPrice"] = theCostPrice;

                            //if (chkPrintPrescription.Checked)
                            //{
                                theDV[0]["PrintPrescriptionStatus"] = "1";
                            //}
                            //else
                            //{
                            //    theDV[0]["PrintPrescriptionStatus"] = "0";
                            //}

                            theDV[0]["PatientInstructions"] = txtPatientInstructions.Text;

                            lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        }
                        else
                        {
                            DataRow theDR = theDT.NewRow();
                            theDR["ItemId"] = theItemId;
                            theDR["ItemName"] = txtItemName.Text;
                            theDR["DispensingUnitId"] = theDispensingUnit;
                            theDR["DispensingUnitName"] = theDispensingUnitName;
                            theDR["BatchId"] = theBatchId;
                            theDR["BatchNo"] = txtBatchNo.Text;
                            theDR["WhyPartial"] = txtWhyPartial.Text;
                            theDR["ExpiryDate"] = txtExpirydate.Text;
                            theDR["QtyDisp"] = txtQtyDispensed.Text;
                            theDR["CostPrice"] = theCostPrice;
                            theDR["Margin"] = theMargin;
                            theDR["SellingPrice"] = txtSellingPrice.Text;
                            theDR["BillAmount"] = theBillAmt;
                            theDR["StrengthId"] = theStrength;
                            theDR["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                            theDR["FrequencyName"] = cmbFrequency.Text;
                            theDR["Prophylaxis"] = theProphylaxis;
                            theDR["ItemType"] = theItemType;
                            theDR["GenericAbb"] = theGenericAbb;
                            theDR["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();

                            theDR["Dose"] = txtDose.Text;
                            theDR["Duration"] = txtDuration.Text;

                            theDR["DataStatus"] = "1";

                            //if (chkPrintPrescription.Checked)
                            //{
                                theDR["PrintPrescriptionStatus"] = "1";
                            //}
                            //else
                            //{
                            //    theDR["PrintPrescriptionStatus"] = "0";
                            //}

                            theDR["PatientInstructions"] = txtPatientInstructions.Text;

                            lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                            theDT.Rows.Add(theDR);

                            DataTable theNewDT = CreatePharmacyDispenseTable();
                            theNewDT = theDT.Copy();

                            //   grdDrugDispense.DataSource = theNewDT;
                            BindPharmacyDispenseGrid(theNewDT);
                        }
                        theDT.AcceptChanges();
                        BindPharmacyDispenseGrid(theDT);
                        theDispCurrentRow = -1;
                    }
                    else
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR["ItemId"] = theItemId;
                        theDR["ItemName"] = txtItemName.Text;
                        theDR["DispensingUnitId"] = theDispensingUnit;
                        theDR["DispensingUnitName"] = theDispensingUnitName;
                        theDR["BatchId"] = theBatchId;
                        theDR["BatchNo"] = txtBatchNo.Text;
                        theDR["ExpiryDate"] = txtExpirydate.Text;
                        theDR["WhyPartial"] = txtWhyPartial.Text;
                        theDR["QtyDisp"] = txtQtyDispensed.Text;
                        theDR["CostPrice"] = theCostPrice;
                        theDR["Margin"] = theMargin;
                        theDR["SellingPrice"] = txtSellingPrice.Text;
                        theDR["BillAmount"] = theBillAmt;
                        theDR["StrengthId"] = theStrength;
                        theDR["FrequencyId"] = Convert.ToInt32(cmbFrequency.SelectedValue);
                        theDR["FrequencyName"] = cmbFrequency.Text;
                        theDR["Prophylaxis"] = theProphylaxis;
                        theDR["ItemType"] = theItemType;
                        theDR["GenericAbb"] = theGenericAbb;
                        theDR["OrderedQuantity"] = txtQtyPrescribed.Text.Trim();

                        theDR["Dose"] = txtDose.Text;
                        theDR["Duration"] = txtDuration.Text;

                        theDR["DataStatus"] = "1";

                        //if (chkPrintPrescription.Checked)
                        //{
                            theDR["PrintPrescriptionStatus"] = "1";
                        //}
                        //else
                        //{
                        //    theDR["PrintPrescriptionStatus"] = "0";
                        //}

                        theDR["PatientInstructions"] = txtPatientInstructions.Text;

                        lblPayAmount.Text = (Convert.ToDecimal(lblPayAmount.Text) + theBillAmt).ToString();
                        theDT.Rows.Add(theDR);

                        DataTable theNewDT = CreatePharmacyDispenseTable();
                        theNewDT = theDT.Copy();

                        BindPharmacyDispenseGrid(theNewDT);
                    }
                }

                theItemId = 0;
                theDispensingUnit = 0;
                theBatchId = 0;
                theCostPrice = 0;
                theMargin = 0;
                theBillAmt = 0;
                theDispensingUnitName = "";
                txtItemName.Text = "";
                txtBatchNo.Text = "";
                cmbFrequency.SelectedValue = "0";
                txtQtyDispensed.Text = "";
                txtExpirydate.Text = "";
                txtSellingPrice.Text = "";
                txtDose.Text = "";
                txtDuration.Text = "";
                txtItemName.Select();
                txtQtyPrescribed.Text = "";
                txtQtyPrescribed.Enabled = true;
                //chkPrintPrescription.Checked = false;
                txtPatientInstructions.Text = "";
                theprevbatchId = 0;
                txtWhyPartial.Text = "";
                labelWhyPartial.Visible = txtWhyPartial.Visible = false;
                //txtWeight.Text = "";
                //txtHeight.Text = "";
                //txtBSA.Text = "";
                //cmbprogram.SelectedValue = "0";
                //cmdPeriodTaken.SelectedValue = "0";
                //cmbProvider.SelectedValue = "0";
                //cmbRegimenLine.SelectedValue = "0";
                //cmbReason.SelectedValue = "0";
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExitingRecClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExitingRecClose_Click(object sender, EventArgs e)
        {
            grpExistingRec.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnFind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            //  btnART.Enabled = false;
            //      clearPopup();
            string theSex = "";
            string theDOB = "";
            if (Convert.ToInt32(cmbSex.SelectedValue) > 0)
                theSex = cmbSex.SelectedValue.ToString();
            if (dtpDOB.CustomFormat == " ")
                theDOB = "01-01-1900";
            else
                theDOB = dtpDOB.Text;

            DateTime? dateOfBirth = null;
            if (dtpDOB.Checked)
            {
                dateOfBirth = string.IsNullOrEmpty(dtpDOB.Text.Trim())
                      ? null
                      : (DateTime?)dtpDOB.Value;
            }
            int LocationId = GblIQCare.AppLocationId;
            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");

            DataTable dtPatient = PatientManager.GetPatientSearchResults(
                                  LocationId,
                                   txtLastName.Text.Trim(),
                                   textMidName.Text.Trim(),
                                   txtFirstName.Text.Trim(),
                                   txtPatientIdentification.Text.Trim(),
                                  theSex,
                                  "",
                                   dateOfBirth,
                                   null,
                                    999,
                                   50
                                                      );
            this.BindSearchGrid(dtPatient);

            //DataSet dsPatient = PatientManager.GetPatientSearchResults(Convert.ToInt32(cmbFacility.SelectedValue), txtLastName.Text, "", txtFirstName.Text, txtPatientIdentification.Text,
            //    theSex, Convert.ToDateTime(theDOB), "0", Convert.ToInt32(cmbService.SelectedValue));
            //BindSearchGrid(dsPatient.Tables[0]);
        }

        /// <summary>
        /// Handles the Click event of the btnHIVCareTrtPharFld control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnHIVCareTrtPharFld_Click(object sender, EventArgs e)
        {
            grpHivCareTrtPharmacyField.Visible = false;
            
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.thePrecribeAmt = new Decimal(0);
            this.theOrderId = 0;
            DataTable pharmacyDispenseTable = CreatePharmacyDispenseTable();
            this.grdDrugDispense.DataSource = (object)pharmacyDispenseTable;
            this.BindPharmacyDispenseGrid(pharmacyDispenseTable);
            this.dtDispensedDate.Text = GblIQCare.CurrentDate;
            this.cmbprogram.SelectedValue = "0";
            this.theItemId = 0;
            this.theDispensingUnit = 0;
            this.theBatchId = 0;
            this.theCostPrice = 0;
            this.theMargin = 0;
            this.theBillAmt = 0;
            this.theDispensingUnitName = "";
            this.txtItemName.Text = "";
            this.txtBatchNo.Text = "";
            this.cmbFrequency.SelectedValue = "0";
            this.txtQtyDispensed.Text = "";
            this.txtExpirydate.Text = "";
            this.txtSellingPrice.Text = "";
            this.txtDose.Text = "";
            this.txtDuration.Text = "";
            this.txtItemName.Select();
            this.txtQtyPrescribed.Text = "";
            this.txtQtyPrescribed.Enabled = true;
            this.txtWeight.Text = "";
            this.txtHeight.Text = "";
            this.txtBSA.Text = "";
           // this.chkPrintPrescription.Checked = false;
            this.txtPatientInstructions.Text = "";
            this.cmdPeriodTaken.SelectedValue = "0";
            this.cmbProvider.SelectedValue = "0";
            this.cmbRegimenLine.SelectedValue = "0";
            this.cmbReason.SelectedValue = "0";
            //  this.btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = true;
            this.cmdSave.Enabled = true;
            this.labelWhyPartial.Visible = txtWhyPartial.Visible = false;
            this.txtWhyPartial.Text = "";
            this.NxtAppDate.Format = DateTimePickerFormat.Custom;
            dtDispensedDate.Enabled = true;
            this.NxtAppDate.CustomFormat = " ";
            this.makeGridEditable = "Yes";
            this.dtRefillApp.CustomFormat = " ";
            this.valuePrescriptionDate.Text = "";
            this.labelPrescriptionDate.Visible = this.valuePrescriptionDate.Visible = false;
         //   this.chkPharmacyRefill.Checked = false;
            this.lblPayAmount.Text = "0.0";
            this.clearPopup();
            //btnART_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnPatientClinicalSummary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPatientClinicalSummary_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = thePatientId;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPatientClinicalSummary, IQCare.SCM"));
            //GblIQCare.theArea = "Dispense";
            theForm.Show();

            // frmPatientClinicalSummary frmPatientProfile = new frmPatientClinicalSummary();
            //frmPatientProfile.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnPharmacyNotes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPharmacyNotes_Click(object sender, EventArgs e)
        {
            GblIQCare.patientID = thePatientId;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPharmacynotes, IQCare.SCM"));
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnView_Click(object sender, EventArgs e)
        {
            grpExistingRec.Visible = true;
            grpExistingRec.Left = 68;
            grpExistingRec.Top = 56;
            grpExistingRec.Width = 530;
            grpExistingRec.Height = 230;

            IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(this.thePatientId, GblIQCare.intStoreId);
            BindExitingGrid(theDT);
        }

        /// <summary>
        /// Calculates the selling price.
        /// </summary>
        private void CalculateSellingPrice()
        {
            Int32 theQtyDispense = 0;
            if (theConfigSellingPrice == 0)
            {
                //IQCareWindowMsgBox.ShowWindowConfirm("MissingSellingPrice", this);
                // return;
            }
            if (txtQtyDispensed.Text == "")
                theQtyDispense = 0;
            else
                theQtyDispense = Convert.ToInt32(txtQtyDispensed.Text);
            if (this.priceBundled) { txtSellingPrice.Text = (1 * theSellingPrice).ToString(); }
            else { txtSellingPrice.Text = (theQtyDispense * theSellingPrice).ToString(); }
            if (theFunded == 0)
                theBillAmt = Convert.ToDecimal(txtSellingPrice.Text);
            else
                theBillAmt = Convert.ToDecimal("0");
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkPrintPrescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        //private void chkPrintPrescription_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPrintPrescription.Checked)
        //    {
        //        txtPatientInstructions.Enabled = true;
        //    }
        //    else
        //    {
        //        txtPatientInstructions.Text = "";
        //        txtPatientInstructions.Enabled = false;
        //    }
        //}

        /// <summary>
        /// Clears the popup.
        /// </summary>
        private void clearPopup()
        {
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtBSA.Text = "";
            cmdPeriodTaken.SelectedValue = "0";
            cmbProvider.SelectedValue = "0";
            cmbRegimenLine.SelectedValue = "0";
            cmbReason.SelectedValue = "0";
            cmbprogram.SelectedValue = "0";
        }

        /// <summary>
        /// Handles the KeyUp event of the cmbFrequency control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void cmbFrequency_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtyDispensed.Select();
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the cmdPrintPrescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmdPrintPrescription_Click(object sender, EventArgs e)
        {
            try
            {
                //Cells[19].Value.ToString() != "0"
                int ptnPharmacyPK = 0;
                DataSet dataSet = new DataSet();
                IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataTable tableDispense = (DataTable)grdDrugDispense.DataSource;
                int PrintStatus = 0;
                foreach (DataRow theDR in tableDispense.Rows)
                {
                    if (theDR["PrintPrescriptionStatus"].ToString() == "1")
                    {
                        PrintStatus = 1;
                    }
                }
                if (PrintStatus == 0)
                {
                    IQCareWindowMsgBox.ShowWindow("PrintPresCheck", this);
                    return;
                }
                else
                {
                    this.DispenseTab.Focus();
                    this.thePharmacyMaster = drug.GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId);
                    this.grpBoxLastDispense.Visible = true;
                    // btnART.Enabled = false;
                    //DataTable table1 = new DataView(tableDispense)
                    //{
                    //    RowFilter = "PrintPrescriptionStatus='1'"
                    //}.ToTable();

                    //// Add DataColumn in Existing Datatable////
                    //DataView theSrcDV = new DataView(tableDispense);

                    //theSrcDV.RowFilter = "PrintPrescriptionStatus='1'";

                    // DataTable tableForPrint = theSrcDV.ToTable();
                    DataTable tableForPrint = new DataView(tableDispense)
                    {
                        RowFilter = "PrintPrescriptionStatus='1'"
                    }.ToTable();
                    tableForPrint.TableName = "Table";

                    tableForPrint.Columns.Add(new DataColumn("Item Code", typeof(string))
                    {
                        DefaultValue = (object)""
                    });
                    tableForPrint.AcceptChanges();

                    dataSet.Tables.Add(tableForPrint);
                    //////    ///////////////////////////////////////////
                    ptnPharmacyPK = this.theOrderId;
                    this.XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");


                    DataSet prescriptionDetails = drug.GetPharmacyPrescriptionDetails(ptnPharmacyPK, Convert.ToInt32(this.thePatientId), 0);
                    for (int index = 0; index < prescriptionDetails.Tables.Count; index++)
                    {
                        ///Set the copy of source table in local instance
                        DataTable tableToAdd = prescriptionDetails.Tables[index].Copy();
                        int a = index + 1;
                        tableToAdd.TableName = "Table" + a;
                        ///Remove from source to avoid any exceptions
                        //thePharmDS.Tables.RemoveAt(index);
                        ///Add the copy to result set
                        dataSet.Tables.Add(tableToAdd);
                    }
                    //Image Streaming
                    DataTable dtFacility = new DataTable();
                    // object of data row
                    DataRow drow = null;
                    // add the column in table to store the image of Byte array type
                    dtFacility.Columns.Add("FacilityImage", System.Type.GetType("System.Byte[]"));
                    drow = dtFacility.NewRow();
                    int ImageFlag = 0;

                    // check the existance of image
                    if (File.Exists(GblIQCare.PresentationImagePath() + ((dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString() != "") ? dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString() : "")))
                    {
                        // define the filestream object to read the image
                        FileStream fs = default(FileStream);
                        // define te binary reader to read the bytes of image
                        BinaryReader br = default(BinaryReader);
                        // open image in file stream
                        fs = new FileStream(GblIQCare.PresentationImagePath() + dataSet.Tables[3].Rows[0]["FacilityLogo"].ToString(), FileMode.Open);

                        // initialise the binary reader from file streamobject
                        br = new BinaryReader(fs);
                        // define the byte array of filelength
                        //  byte[] imgbyte = new byte[fs.Length + 1];
                        // read the bytes from the binary reader
                        byte[] imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                        drow[0] = imgbyte;
                        // add the image in bytearray
                        dtFacility.Rows.Add(drow);
                        ImageFlag = 1;
                        // add row into the datatable
                        br.Close();
                        // close the binary reader
                        fs.Close();
                        // close the file stream
                    }

                    dataSet.Tables.Add(dtFacility);
                    ////////////////////////////////////////

                    //  dataSet.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\PatientPharmacyPrescription.xml");
                    frmReportViewer theRepViewer = new frmReportViewer();
                    theRepViewer.MdiParent = this.MdiParent;
                    theRepViewer.Location = new Point(0, 0);
                    if (dataSet.Tables[3].Rows[0]["FacilityTemplate"].ToString() == "1")
                    {
                        rptKNHPrescription rpt = new rptKNHPrescription();
                        rpt.SetDataSource(dataSet);
                        rpt.SetParameterValue("EnrollmentID", "");
                        rpt.SetParameterValue("PharmacyID", ptnPharmacyPK.ToString());
                        rpt.SetParameterValue("ModuleName", "");
                        rpt.SetParameterValue("Currency", getCurrency().ToString());
                        rpt.SetParameterValue("FacilityName", GblIQCare.AppLocation.ToString());
                        rpt.SetParameterValue("Imageflag", ImageFlag.ToString());
                        theRepViewer.crViewer.ReportSource = rpt;
                    }
                    else
                    {
                        rptSimplePrescription rpt = new rptSimplePrescription();
                        rpt.SetDataSource(dataSet);
                        rpt.SetParameterValue("EnrollmentID", "");
                        rpt.SetParameterValue("ModuleName", "");
                        rpt.SetParameterValue("Currency", getCurrency().ToString());
                        rpt.SetParameterValue("FacilityName", GblIQCare.AppLocation.ToString());
                        rpt.SetParameterValue("Imageflag", ImageFlag.ToString());
                        theRepViewer.crViewer.ReportSource = rpt;
                    }

                    drug = null;
                    theRepViewer.Show();
                    //this.Close();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, (Control)this);
            }
        }
       // bool setTreatmentFields = false;
       
        /// <summary>
        /// Handles the Click event of the cmdSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                if (MainTab.TabPages["DispenseTab"].Focus() == true)
                {
                    //if (!setTreatmentFields)
                    //{
                    //    MessageBox.Show("Set and verify the treatment details");
                    //    btnART_Click(sender, e);
                    //    return;
                    //}
                    DataTable theDT = (DataTable)grdDrugDispense.DataSource;
                    string theRegimen = "";
                    //Int32 theProgId = 0;
                    //Int32 theAge = Convert.ToDateTime(dtDispensedDate.Text).Year - theDOB.Year;
                    //if (theAge > 15)
                    //    theProgId = 116;
                    //else
                    //    theProgId = 117;

                    int theOrderType = Convert.ToDateTime(this.dtDispensedDate.Text).Year - this.theDOB.Year <= 15 ? 117 : 116;

                    DataTable theUniqueDT = theDT.Copy();
                    DataTable theDupFilteredDT = RemoveDuplicatesRecords(theUniqueDT);
                    foreach (DataRow theDR in theDupFilteredDT.Rows)
                    {
                        if (theDR["ItemType"].ToString() == "37" && theDR["Prophylaxis"].ToString() != "1")
                        {
                            if (theRegimen == "" && theDR["GenericAbb"].ToString() != "")
                                theRegimen = theRegimen + theDR["GenericAbb"].ToString();
                            else if (theRegimen != "" && theDR["GenericAbb"].ToString() != "")
                                theRegimen = theRegimen + "/" + theDR["GenericAbb"].ToString();
                        }
                    }
                    //string str = !(this.dtRefillApp.CustomFormat == " ") ? this.dtRefillApp.Text : "01-01-1900";

                    DateTime? refillDate = null;

                    if (dtRefillApp.Text != "")
                    {
                        refillDate = Convert.ToDateTime(dtRefillApp.Value);
                    }
                    string  programValue = cmbprogram.SelectedValue.ToString();
                    string strPeriod = cmdPeriodTaken.SelectedValue.ToString();
                    string strProvider = cmbProvider.SelectedValue.ToString();
                    string strRegimenLine= cmbRegimenLine.SelectedValue.ToString();

                    double? weight = null;
                    if (txtWeight.Text != "")
                    {
                        weight = Convert.ToDouble(txtWeight.Text);
                    }
                    double? height = null;
                    if (txtHeight.Text != "")
                    {
                        height = Convert.ToDouble(txtHeight.Text);
                    }
                    int? periodTaken = null;
                    if(strPeriod != "") periodTaken = Convert.ToInt32(strPeriod);

                    int? provider = null;
                    if (strProvider != "") provider = Convert.ToInt32(strProvider);

                    int? regimenLine = null;
                    if (strRegimenLine != "") regimenLine = Convert.ToInt32(strRegimenLine);

                    DataTable dtPharmacyDetails = thePharmacyManager.SavePharmacyDispense(
                        thePatientId, 
                        GblIQCare.AppLocationId, 
                        GblIQCare.intStoreId,
                        GblIQCare.AppUserId,
                        Convert.ToDateTime(dtDispensedDate.Text), 
                        theOrderType, 
                        Convert.ToInt32(cmbprogram.SelectedValue), 
                        theRegimen, 
                        theOrderId, 
                        theDT,
                        refillDate,
                        periodTaken,
                        regimenLine,
                        provider,
                        height,
                        weight);

                    if (theRegimen !="")
                    {
                        int val = theOrderId;
                        int ptnPharmacyPK = Convert.ToInt32(dtPharmacyDetails.Rows[0]["Ptn_Pharmacy_Pk"].ToString());
                        SaveUpdateArt(ptnPharmacyPK);
                    }
                    IQCareWindowMsgBox.ShowWindow("PharmacyDispenseSave", this);
                    DataTable theNewDT = CreatePharmacyDispenseTable();
                    BindPharmacyDispenseGrid(theNewDT);
                    grdDrugDispense.DataSource = theNewDT;

                    //todo

                    theOrderId = 0;
                    theOrderStatus = "";
                }

                else if (MainTab.TabPages["ReturnTab"].Focus() == true)
                {
                    if (Convert.ToDateTime(dtpReturnDate.Text) < Convert.ToDateTime(dtDispensedDate.Text))
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturnDate", this);
                        return;
                    }
                    DataTable theDT = (DataTable)grdReturnDetail.DataSource;
                    int theRowsforSave = 0;
                    foreach (DataRow theDR in theDT.Rows)
                    {
                        if (Convert.ToInt32(theDR["ReturnQty"]) > 0)
                        {
                            theRowsforSave = theRowsforSave + 1;
                            theDR["SellingPrice"] = Convert.ToDecimal("-" + theDR["UnitSellingPrice"].ToString()) * Convert.ToInt32(theDR["ReturnQty"]);
                            if (Convert.ToInt32(theDR["BillAmount"]) > 0)
                            {
                                theDR["BillAmount"] = theDR["SellingPrice"];
                            }
                            else
                            {
                                theDR["BillAmount"] = 0;
                            }
                        }
                    }

                    if (theRowsforSave == 0)
                    {
                        IQCareWindowMsgBox.ShowWindow("NoDrugReturn", this);
                        return;
                    }
                    thePharmacyManager.SavePharmacyReturn(thePatientId, GblIQCare.AppLocationId, GblIQCare.intStoreId, Convert.ToDateTime(dtpReturnDate.Text), GblIQCare.AppUserId, theReturnOrderId, theDT);
                    IQCareWindowMsgBox.ShowWindow("PharmacyReturnSave", this);
                    theDT = thePharmacyManager.GetPharmacyExistingRecord(thePatientId, GblIQCare.intStoreId);
                    BindDrugReturnGrid(theDT);
                    grdReturnDetail.DataSource = false;
                    grdReturnDetail.Columns.Clear();
                }
                thePharmacyMaster = thePharmacyManager.GetPharmacyDispenseMasters(thePatientId, GblIQCare.intStoreId);

                //btnART.Enabled = false;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Creates the pharmacy dispense table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreatePharmacyDispenseTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("ItemId", Type.GetType("System.Int32"));
            theDT.Columns.Add("ItemName", Type.GetType("System.String"));
            theDT.Columns.Add("DispensingUnitId", Type.GetType("System.Int32"));
            theDT.Columns.Add("DispensingUnitName", Type.GetType("System.String"));
            theDT.Columns.Add("BatchId", Type.GetType("System.Int32"));
            theDT.Columns.Add("BatchNo", Type.GetType("System.String"));
            theDT.Columns.Add("StrengthId", Type.GetType("System.Int32"));
            theDT.Columns.Add("FrequencyId", Type.GetType("System.Int32"));
            theDT.Columns.Add("FrequencyName", Type.GetType("System.String"));
            theDT.Columns.Add("ExpiryDate", Type.GetType("System.DateTime"));
            theDT.Columns.Add("QtyDisp", Type.GetType("System.Int32"));
            theDT.Columns.Add("CostPrice", Type.GetType("System.Decimal"));
            theDT.Columns.Add("Margin", Type.GetType("System.Decimal"));
            theDT.Columns.Add("SellingPrice", Type.GetType("System.Decimal"));
            theDT.Columns.Add("BillAmount", Type.GetType("System.Decimal"));
            theDT.Columns.Add("Prophylaxis", Type.GetType("System.Int32"));
            theDT.Columns.Add("ItemType", Type.GetType("System.Int32"));
            theDT.Columns.Add("GenericAbb", Type.GetType("System.String"));

            //todo
            theDT.Columns.Add("OrderedQuantity", Type.GetType("System.String"));
            theDT.Columns.Add("DataStatus", Type.GetType("System.String"));

            theDT.Columns.Add("Dose", Type.GetType("System.String"));
            theDT.Columns.Add("Duration", Type.GetType("System.String"));

            theDT.Columns.Add("PrintPrescriptionStatus", Type.GetType("System.Int32"));
            theDT.Columns.Add("PatientInstructions", Type.GetType("System.String"));
            theDT.Columns.Add("WhyPartial", Type.GetType("System.String"));
            DataColumn[] thePkey = new DataColumn[3];
            thePkey[0] = theDT.Columns["ItemId"];
            thePkey[1] = theDT.Columns["BatchId"];
            thePkey[2] = theDT.Columns["ExpiryDate"];
            theDT.PrimaryKey = thePkey;
            return theDT;
        }

        /// <summary>
        /// Handles the Enter event of the dtpDOB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpDOB_Enter(object sender, EventArgs e)
        {
            if (!dtpDOB.Checked)
            {
                dtpDOB.CustomFormat = " ";
                dtpDOB.Format = defaultDateTimePickerFormat;
            }
            else
            {
                dtpDOB.CustomFormat = "dd-MMM-yyyy";
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the dtpDOB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            if (!dtpDOB.Checked)
            {
                dtpDOB.CustomFormat = " ";
                dtpDOB.Format = defaultDateTimePickerFormat;
            }
        }

        /// <summary>
        /// Handles the Load event of the frmPatientDrugDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmPatientDrugDispense_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            MainTab.SelectedTab = MainTab.TabPages["FindPatientTab"];
            txtPatientIdentification.Select();
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        /// <returns></returns>
        private string getCurrency()
        {
            System.Data.DataSet theDS = new System.Data.DataSet();
            theDS.ReadXml(GblIQCare.GetXMLPath() + "\\Currency.xml");
            DataView theCurrDV = new DataView(theDS.Tables[0]);
            theCurrDV.RowFilter = "Id=" + Convert.ToInt32(GblIQCare.AppCountryId);
            string thestringCurrency = theCurrDV[0]["Name"].ToString();
            return thestringCurrency.Substring(thestringCurrency.LastIndexOf("(") + 1, 3);
        }

        private void GetPrice(int theItemId, int itemTypeId)
        {
            Price itemPrice = null;
            try
            {
                IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                itemPrice = bMgr.GetItemPrice(theItemId, itemTypeId, null);
                theSellingPrice = itemPrice.Amount;
                this.priceBundled = itemPrice.IsBundled;
            }
            catch
            {
                this.priceBundled = false;
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the grdDrugDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void grdDrugDispense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 19)
            //{
            //    grdDrugDispense.EndEdit();  //Stop editing of cell.
            //    if ((bool)grdDrugDispense.Rows[e.RowIndex].Cells["Print"].Value)
            //        MessageBox.Show("The Value is Checked", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    else
            //        MessageBox.Show("UnChecked", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the grdDrugDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void grdDrugDispense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int PresdispenceQty = 0;
            decimal totalOqdQty = 0;
            int rowIndex = grdDrugDispense.CurrentRow.Index;
            //if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "" && grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "0")
            if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "")
            {
                if (theOrderId > 0 && theOrderStatus == "Partial Dispense")
                {
                    IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
                    return;
                }
                else if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value).Equals(System.DBNull.Value))
                {
                    theDispCurrentRow = grdDrugDispense.CurrentRow.Index;
                    PresdispenceQty = Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value);

                    thePrecribeAmt = PresdispenceQty;

                    string itemID = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[0].Value.ToString();
                    DataTable dtGrd = ((DataTable)(grdDrugDispense.DataSource)).Copy();
                    DataView dv = dtGrd.DefaultView;
                    dv.RowFilter = "ItemId=" + itemID;

                    DataTable dt1 = dv.ToTable();// Table;

                    foreach (DataRow dr in dt1.Rows)
                    {
                        totalOqdQty = totalOqdQty + Convert.ToDecimal(dr["QtyDisp"].ToString());
                    }
                    //if (PresdispenceQty <= totalOqdQty)
                    //{
                    //    IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
                    //    return;
                    //}
                    //Already Dispensed Order
                    if (theOrderId > 0 && theOrderStatus == "Already Dispensed Order")
                    {
                        IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
                        return;
                    }
                }
            }
            else if (theOrderId > 0 && theOrderStatus != "New Order")
            {
                IQCareWindowMsgBox.ShowWindow("PharmacyCannotAlterRow", this);
                return;
            }
            theItemId = Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[0].Value);
            DataView theDV = new DataView(thePharmacyMaster.Tables[1]);

            // Add && theOrderStatus != "Partial Dispense"  below line
            if (theOrderStatus != "New Order" && theOrderStatus != "Partial Dispense")
            {
                theDV.RowFilter = "Drug_Pk = " + theItemId.ToString() + " and BatchId = " + grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[4].Value.ToString() +
                    " and ExpiryDate='" + grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[6].Value.ToString() + "'";
            }
            else
            {
                theDV.RowFilter = "Drug_Pk = " + theItemId.ToString();
            }
            if (theDV.Count < 1)
            {
                IQCareWindowMsgBox.ShowWindow("NoAvailQty", this);
                return;
            }

            if ((theDV.ToTable().Rows.Count == 0) && (Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value) == 0))
            {
                IQCareWindowMsgBox.ShowWindow("NoAvailQty", this);
                return;
            }
            else
            {
                txtItemName.Text = theDV[0]["DrugName"].ToString();

                if (theOrderStatus == "New Order")
                {
                    try
                    {
                        string frequency = grdDrugDispense.Rows[rowIndex].Cells["FrequencyId"].Value.ToString();

                        cmbFrequency.SelectedValue = frequency;
                        //grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[17].Value.ToString();
                    }
                    catch { }
                    txtSellingPrice.Text = "";
                    txtBatchNo.Text = "";
                    theBatchId = 0;
                    txtExpirydate.Text = "";
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString() != "")
                    {
                        txtDose.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString();
                    }
                    else
                    {
                        txtDose.Text = "";
                    }
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString() != "")
                    {
                        txtDuration.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString();
                    }
                    else
                    {
                        txtDuration.Text = "";
                    }
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString() != "")
                    {
                        txtQtyPrescribed.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value.ToString();
                        if (theExistingDrugs.Rows.Count > 0)
                        {
                            DataRow[] theexistrow = theExistingDrugs.Select("ItemId=" + theItemId.ToString());
                            if (theexistrow.Length > 0)
                                txtQtyPrescribed.Enabled = false;
                        }
                    }
                    else
                    {
                        txtQtyPrescribed.Text = "";
                    }
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString() != "0.00")
                    {
                        txtQtyDispensed.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString();
                    }
                    else
                    {
                        txtQtyDispensed.Text = "";
                    }
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString() != "")
                    {
                        txtSellingPrice.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString();
                    }
                    else
                    {
                        txtSellingPrice.Text = "";
                    }
                    //print prescription
                    //if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[19].Value.ToString() != "0")
                    //{
                    //    chkPrintPrescription.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPrintPrescription.Checked = false;
                    //}
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString() != "")
                    {
                        txtPatientInstructions.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString();
                    }
                    else
                    {
                        txtPatientInstructions.Text = "";
                    }

                    theprevbatchId = 1;
                    theSellingPrice = 0;
                    this.priceBundled = false;
                    theConfigSellingPrice = 0;
                    theCostPrice = 0;
                    theItemTypeId = 0;
                    theMargin = 0;
                    theDispensingUnit = 0;
                    theDispensingUnitName = "";
                    theFunded = 0;
                    theAvailQty = 0;
                    theStrength = 0;
                    theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);
                    txtItemName.Select();
                    KeyEventArgs theArg = new KeyEventArgs(Keys.Enter);
                    txtItemName_KeyUp(txtItemName, theArg);
                }
                else
                {
                    KeyEventArgs theArg1 = new KeyEventArgs(Keys.Enter);
                    txtItemName_KeyUp(txtItemName, theArg1);

                    txtBatchNo.Text = theDV[0]["BatchNo"].ToString();
                    theBatchId = Convert.ToInt32(theDV[0]["BatchId"]);
                    txtExpirydate.Text = ((DateTime)theDV[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat);

                    theSellingPrice = Convert.ToDecimal(theDV[0]["SellingPrice"]);
                    theItemTypeId = Convert.ToInt32(theDV[0]["ItemTypeID"]);
                    this.GetPrice(theItemId, theItemTypeId);
                    theConfigSellingPrice = Convert.ToDecimal(theDV[0]["ConfigSellingPrice"]);
                    theCostPrice = Convert.ToDecimal(theDV[0]["CostPrice"]);
                    theMargin = Convert.ToDecimal(theDV[0]["DispensingMargin"]);
                    theDispensingUnit = Convert.ToInt32(theDV[0]["DispensingId"]);
                    theDispensingUnitName = theDV[0]["DispensingUnit"].ToString();
                    theFunded = Convert.ToInt32(theDV[0]["Funded"]);
                    theAvailQty = Convert.ToInt32(theDV[0]["AvailQty"]);
                    theStrength = Convert.ToInt32(theDV[0]["StrengthId"]);
                    theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);

                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value.ToString() != "")
                    {
                        if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).Equals(System.DBNull.Value))
                        {
                            txtDose.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).ToString();
                        }
                    }
                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value.ToString() != "")
                    {
                        if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).Equals(System.DBNull.Value))
                        {
                            txtDuration.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).ToString();
                        }
                    }
                    // txtDose.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[7].Value).ToString();
                    //  txtDuration.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[9].Value).ToString();
                    cmbFrequency.SelectedValue = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[17].Value.ToString();

                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value.ToString() != "0.00")
                    {
                        txtQtyDispensed.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[11].Value).ToString();
                    }

                    txtQtyPrescribed.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[10].Value).ToString();
                    //if (txtQtyPrescribed.Text != "")
                    //{
                    //    txtQtyPrescribed.Enabled = false;
                    //}

                    if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value.ToString() != "")
                    {
                        if (!Convert.ToInt32(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value).Equals(System.DBNull.Value))
                        {
                            txtSellingPrice.Text = Convert.ToDecimal(grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[14].Value).ToString();
                        }
                    }
                    if (PresdispenceQty > totalOqdQty)
                    {
                        txtItemName.Enabled = true;
                    }
                    else
                    {
                        txtItemName.Enabled = false;
                    }

                    if (theItemType == 37 && cmbprogram.SelectedValue.ToString() == "223")
                        theProphylaxis = 1;
                    else
                        theProphylaxis = 0;
                    theGenericAbb = theDV[0]["GenericAbb"].ToString();

                    //if (grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[19].Value.ToString() != "0")
                    //{
                    //    chkPrintPrescription.Checked = true;
                    //}
                    //else
                    //{
                    //    chkPrintPrescription.Checked = false;
                    //}
                    txtPatientInstructions.Text = grdDrugDispense.Rows[grdDrugDispense.CurrentRow.Index].Cells[20].Value.ToString();
                }
            }
        }

        //this.images is an ImageList with your bitmaps
        /// <summary>
        /// Handles the CellPainting event of the grdDrugDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellPaintingEventArgs"/> instance containing the event data.</param>
        private void grdDrugDispense_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 19 && e.RowIndex == -1)
                {
                    e.PaintBackground(e.ClipBounds, false);

                    Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                    int offsetX = (e.CellBounds.Width - this.imageList1.ImageSize.Width) / 2;
                    int offsetY = (e.CellBounds.Height - this.imageList1.ImageSize.Height) / 2;
                    pt.X += offsetX;
                    pt.Y += offsetY;
                    this.imageList1.Draw(e.Graphics, pt, 0);
                    e.Handled = true;
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the grdExitingPharDisp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void grdExitingPharDisp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clearPopup();
           // this.chkPharmacyRefill.Checked = false;
            DataGridViewRow currentRow = grdExitingPharDisp.Rows[e.RowIndex];

            this.theOrderId = Convert.ToInt32(currentRow.Cells["Ptn_Pharmacy_Pk"].Value);
           // this.theOrderId = Convert.ToInt32(grdExitingPharDisp.Rows[grdExitingPharDisp.CurrentRow.Index].Cells[2].Value);
         //   this.theOrderStatus = grdExitingPharDisp.Rows[grdExitingPharDisp.CurrentRow.Index].Cells[1].Value.ToString();
            this.theOrderStatus = currentRow.Cells["Status"].Value.ToString();
            IDrug drugMgr = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataSet existingRecordDetails = drugMgr.GetPharmacyExistingRecordDetails(this.theOrderId);
            if (existingRecordDetails.Tables[0].Rows.Count > 0)
            {
                if (existingRecordDetails.Tables[0].Rows[0].IsNull("DispensedByDate") == false)
                    this.dtDispensedDate.Text = existingRecordDetails.Tables[0].Rows[0]["DispensedByDate"].ToString();
                if (existingRecordDetails.Tables[0].Rows[0].IsNull("ProgId") == false)
                    this.cmbprogram.SelectedValue = existingRecordDetails.Tables[0].Rows[0]["ProgId"].ToString();
                if (existingRecordDetails.Tables[0].Rows[0].IsNull("OrderedByDate") == false)
                {
                    this.valuePrescriptionDate.Text = ((DateTime)existingRecordDetails.Tables[0].Rows[0]["OrderedByDate"]).ToString(GblIQCare.AppDateFormat.ToString());
                    this.valuePrescriptionDate.Visible = this.labelPrescriptionDate.Visible = true;
                }
            }
            Decimal theBillAmount = new Decimal(0);
            theExistingDrugs = existingRecordDetails.Tables[0];
            foreach (DataRow theRow in (InternalDataCollectionBase)existingRecordDetails.Tables[0].Rows)
            {
                if (theRow.IsNull("BillAmount") == false)
                    theBillAmount = theBillAmount + Convert.ToDecimal(theRow["BillAmount"]);
                else
                    theBillAmount = theBillAmount + 0;
            }
            lblPayAmount.Text = theBillAmount.ToString();
            if (this.theOrderId > 0 && (this.theOrderStatus == "Already Dispensed Order" || this.theOrderStatus == "Partial Dispense"))
            {
                this.makeGridEditable = "No";
                this.dtRefillApp.CustomFormat = " ";
            }
            else
                this.makeGridEditable = "Yes";

            BindPharmacyDispenseGrid(existingRecordDetails.Tables[0]);

            //btnART.Enabled = false;
            this.grpBoxLastDispense.Visible = true;
            {

                if (theOrderId > 0)
                {
                    //IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                    DataSet theDS1 = drugMgr.GetPharmacyDetailsByDespenced(theOrderId);
                    if (theDS1.Tables[0].Rows.Count > 0)
                    {
                        txtWeight.Text = theDS1.Tables[0].Rows[0]["Weight"].ToString();
                        txtHeight.Text = theDS1.Tables[0].Rows[0]["Height"].ToString();
                        if (txtWeight.Text != "" && txtHeight.Text != "")
                        {
                            Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                            calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                            txtBSA.Text = Math.Round(calVal, 2).ToString();
                        }
                        if (theDS1.Tables[0].Rows[0]["ProgID"].ToString() != "")
                        {
                            cmbprogram.SelectedValue = theDS1.Tables[0].Rows[0]["ProgID"].ToString();
                        }
                        
                        if (theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                        {
                            cmdPeriodTaken.SelectedValue = theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                        }
                        if (theDS1.Tables[0].Rows[0]["ProviderID"].ToString() != "")
                        {
                            cmbProvider.SelectedValue = theDS1.Tables[0].Rows[0]["ProviderID"].ToString();
                        }
                        if (theDS1.Tables[0].Rows[0]["RegimenLine"].ToString() != "")
                        {
                            cmbRegimenLine.SelectedValue = theDS1.Tables[0].Rows[0]["RegimenLine"].ToString();
                        }
                        if (theDS1.Tables[0].Rows[0]["AppDate"].ToString() != "")
                        {
                            NxtAppDate.Format = DateTimePickerFormat.Custom;
                            NxtAppDate.CustomFormat = "dd-MMM-yyyy";
                            NxtAppDate.Text = theDS1.Tables[0].Rows[0]["AppDate"].ToString();
                        }
                        else
                        {
                            // NxtAppDate.Format = DateTimePickerFormat.Custom;
                            NxtAppDate.CustomFormat = " ";
                        }
                        if (theDS1.Tables[0].Rows[0]["AppReason"].ToString() != "")
                        {
                            cmbReason.SelectedValue = theDS1.Tables[0].Rows[0]["AppReason"].ToString();
                        }

                        if (cmbprogram.SelectedValue.ToString() == "222")
                        {
                            int patientID = thePatientId;
                            //DataSet GettheDS = thePharmacyManager1.SaveArtData(patientID, Convert.ToDateTime(dtDispensedDate.Text));
                        }
                    }
                }
            }
             /*
            if (existingRecordDetails.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < existingRecordDetails.Tables[0].Rows.Count; ++i)
                {
                    Int32 ItemID = Convert.ToInt32(existingRecordDetails.Tables[0].Rows[i]["ItemId"].ToString());
                    IDrug thePharmacyDetails = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                    DataSet drugTypeId = drugMgr.GetDrugTypeID(ItemID);
                    if (drugTypeId.Tables[0].Rows.Count > 0)
                    {
                        string DrugID = drugTypeId.Tables[0].Rows[0]["DrugTypeID"].ToString();
                        if (DrugID == "37")
                        {
                            int a = theOrderId;
                            // btnART.Enabled = true;
                            clearPopup();
                            if (btnART.Enabled)
                            {
                                if (theOrderId > 0)
                                {
                                    IDrug thePharmacyManager1 = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                                    DataSet theDS1 = thePharmacyManager1.GetPharmacyDetailsByDespenced(theOrderId);
                                    if (theDS1.Tables[0].Rows.Count > 0)
                                    {
                                        txtWeight.Text = theDS1.Tables[0].Rows[0]["Weight"].ToString();
                                        txtHeight.Text = theDS1.Tables[0].Rows[0]["Height"].ToString();
                                        if (txtWeight.Text != "" && txtHeight.Text != "")
                                        {
                                            Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                                            calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                                            txtBSA.Text = Math.Round(calVal, 2).ToString();
                                        }

                                        cmbprogram.SelectedValue = theDS1.Tables[0].Rows[0]["ProgID"].ToString();
                                        if (theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                                        {
                                            cmdPeriodTaken.SelectedValue = theDS1.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["ProviderID"].ToString() != "")
                                        {
                                            cmbProvider.SelectedValue = theDS1.Tables[0].Rows[0]["ProviderID"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["RegimenLine"].ToString() != "")
                                        {
                                            cmbRegimenLine.SelectedValue = theDS1.Tables[0].Rows[0]["RegimenLine"].ToString();
                                        }
                                        if (theDS1.Tables[0].Rows[0]["AppDate"].ToString() != "")
                                        {
                                            NxtAppDate.Format = DateTimePickerFormat.Custom;
                                            NxtAppDate.CustomFormat = "dd-MMM-yyyy";
                                            NxtAppDate.Text = theDS1.Tables[0].Rows[0]["AppDate"].ToString();
                                        }
                                        else
                                        {
                                            // NxtAppDate.Format = DateTimePickerFormat.Custom;
                                            NxtAppDate.CustomFormat = " ";
                                        }
                                        if (theDS1.Tables[0].Rows[0]["AppReason"].ToString() != "")
                                        {
                                            cmbReason.SelectedValue = theDS1.Tables[0].Rows[0]["AppReason"].ToString();
                                        }

                                        if (cmbprogram.SelectedValue.ToString() == "222")
                                        {
                                            int patientID = thePatientId;
                                            DataSet GettheDS = thePharmacyManager1.SaveArtData(patientID, Convert.ToDateTime(dtDispensedDate.Text));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            */
            // todo
            //if (theDS.Tables[0].Rows[0]["OrderedQuantity"].ToString() != null && theDS.Tables[0].Rows[0]["OrderedQuantity"].ToString() != "")
            //{
            //    thePrecribeAmt = Convert.ToDecimal(theDS.Tables[0].Rows[0]["OrderedQuantity"]);
            //}
            //Already Dispensed Order
            if (theOrderId > 0 && (theOrderStatus == "Already Dispensed Order" || theOrderStatus == "Partial Dispense"))
            {
                this.makeGridEditable = "No";
                cmdSave.Enabled = false;
                dtDispensedDate.Enabled = false;
            }
            else
            {
                this.cmdSave.Enabled = true;
                this.makeGridEditable = "Yes";
                dtDispensedDate.Enabled = true;
            }
            if (this.ARVBeingDispensed != "" && this.lastdispensedARV != "" && this.ARVBeingDispensed != this.lastdispensedARV)
                IQCareWindowMsgBox.ShowWindow("RegimenChangeAlert", (Control)this);
            grpExistingRec.Visible = false;
        }

        /// <summary>
        /// Handles the CellContentClick event of the grdResultView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void grdResultView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private string removeRegimenDuplicates(string lastRegimen)
        {
            return Enumerable.Aggregate<string>(Enumerable.Distinct<string>((IEnumerable<string>)lastRegimen.Split('/')), (Func<string, string, string>)((current, next) => current + "/" + next));
        }
        /// <summary>
        /// Handles the CellMouseDoubleClick event of the grdResultView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void grdResultView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.clearPopup();
            int rowIndex = this.grdResultView.CurrentRow.Index;
            DataGridViewRow currentRow = this.grdResultView.CurrentRow;
            this.thePatientId = Convert.ToInt32(currentRow.Cells["PatientId"].Value);
            this.theDOB = Convert.ToDateTime(currentRow.Cells["dob"].Value);
            this.MainTab.SelectedTab = this.MainTab.TabPages["DispenseTab"];
            string patientName = string.Format("{0} {1} {2}", currentRow.Cells["FirstName"].Value, currentRow.Cells["MiddleName"].Value, currentRow.Cells["LastName"].Value);
            this.lblPatientName.Text = patientName;
            //this.grdResultView.Rows[rowIndex].Cells[0].Value.ToString() + ", " +
            //this.grdResultView.Rows[rowIndex].Cells[1].Value.ToString();
            this.lblReturnPatName.Text = patientName;
            //this.grdResultView.Rows[rowIndex].Cells[0].Value.ToString() + ", " +
              //  this.grdResultView.Rows[rowIndex].Cells[1].Value.ToString();
            this.lblIQNumber.BackColor = this.BackColor;
            lblIQNumber.Text = currentRow.Cells["PatientFacilityId"].Value.ToString();
            this.lblReturnIQNumber.Text = currentRow.Cells["PatientFacilityId"].Value.ToString();
            //this.grdResultView.Rows[rowIndex].Cells[2].Value.ToString();
            IDrug theDrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            this.thePharmacyMaster = theDrugManager.GetPharmacyDispenseMasters(this.thePatientId, GblIQCare.intStoreId);
            if (this.thePharmacyMaster.Tables[0].Rows.Count > 0)
            {
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("NextRefillDate"))
                {
                    this.NextRefillValue.Text = "";

                }
                else
                {
                    this.NextRefillValue.Text = ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["NextRefillDate"]).ToString(GblIQCare.AppDateFormat.ToString());
                }
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("LastDispense"))
                {

                    this.LastDispenseDateValue.Text = "";
                    this.lblReturnLstDispDate.Text = "";
                    
                }
                else
                {
                    //  lblDispenseLstRegimen
                    //  this.lblLastDispDateValue.Text = ((DateTime)thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    //  this.lblReturnLstDispDate.Text = ((DateTime)thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                    // this.labelLastRegimenDate.Text = 
                    this.lblReturnLstDispDate.Text = this.LastDispenseDateValue.Text =
                    ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["LastDispense"]).ToString(GblIQCare.AppDateFormat.ToString());
                }
                this.labelLastRegimenDate.Text = "";
                if (!this.thePharmacyMaster.Tables[0].Rows[0].IsNull("CurrentRegimenDispenseDate"))
                {
                    this.labelLastRegimenDate.Text = ((DateTime)this.thePharmacyMaster.Tables[0].Rows[0]["CurrentRegimenDispenseDate"]).ToString(GblIQCare.AppDateFormat.ToString());
                }
                this.lastRegimenValue.Text = "";
                if (!this.thePharmacyMaster.Tables[0].Rows[0].IsNull("CurrentRegimen"))
                {
                    string regimenVale = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["CurrentRegimen"].ToString());
                    if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("CurrentRegimen") == false)
                    {
                        regimenVale = string.Format("{0} ( {1} )", regimenVale, this.thePharmacyMaster.Tables[0].Rows[0]["CurrentRegimenLine"].ToString());
                    }
                    this.lastRegimenValue.Text = regimenVale;

               }

                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("LastRegimen"))
                {
                    
                    this.lblReturnLstRegimen.Text = ""; 
                    this.lastdispensedARV = "";
                }
                else
                {
                    
                    this.lblReturnLstRegimen.Text = this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString();
                    this.lastdispensedARV = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["LastRegimen"].ToString()); ;
                }
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("CurrentRegimen"))
                {
                    this.CurrentRegimenValue.Text = "N/A";
                }
                else
                {
                    string regimenVale = this.removeRegimenDuplicates(this.thePharmacyMaster.Tables[0].Rows[0]["CurrentRegimen"].ToString());
                    if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("CurrentRegimen") == false)
                    {
                        regimenVale = string.Format("{0} ( {1} )", regimenVale, this.thePharmacyMaster.Tables[0].Rows[0]["CurrentRegimenLine"].ToString());
                    }
                    this.CurrentRegimenValue.Text = regimenVale;
                }
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("RecentWeight") == false)
                {
                    txtWeight.Text = this.thePharmacyMaster.Tables[0].Rows[0]["RecentWeight"].ToString();
                }
                if (this.thePharmacyMaster.Tables[0].Rows[0].IsNull("RecentHeight") == false)
                {
                    txtHeight.Text = this.thePharmacyMaster.Tables[0].Rows[0]["RecentHeight"].ToString();
                }
            }
            else
            {
                this.labelLastRegimenDate.Text = "";
                this.lastRegimenValue.Text = "";
                this.lblReturnLstRegimen.Text = "";
                this.lblReturnLstDispDate.Text = "";
                this.CurrentRegimenValue.Text = "N/A";
            }
           
            
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                txtBSA.Text = Math.Round(calVal, 2).ToString();
            }
            DataTable theDT = this.CreatePharmacyDispenseTable();
            this.BindPharmacyDispenseGrid(theDT);
            this.txtItemName.Select();
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the grdReturnDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void grdReturnDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewTextBoxEditingControl")
            {
                theCurrentRow = grdReturnDetail.CurrentRow.Index;
                theReturnQty = (TextBox)e.Control;
                theReturnQty.KeyUp += new KeyEventHandler(theReturnQty_KeyUp);
                theReturnQty.KeyPress += new KeyPressEventHandler(theReturnQty_KeyPress);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the grdReturnOrder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void grdReturnOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdReturnOrder.Rows[grdReturnOrder.CurrentRow.Index].Cells[1].Value.ToString() != "New Order")
            {
                theReturnOrderId = Convert.ToInt32(grdReturnOrder.Rows[grdReturnOrder.CurrentRow.Index].Cells[2].Value);
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataSet theDS = thePharmacyManager.GetPharmacyExistingRecordDetails(theReturnOrderId);
                if (theDS.Tables[1].Rows.Count > 0)
                {
                    if (theDS.Tables[1].Rows[0]["DispensedByDate"].ToString() != "")
                    {
                        lblReturnDispensedDate.Text = ((DateTime)theDS.Tables[1].Rows[0]["DispensedByDate"]).ToString("dd-MMM-yyyy");
                    }

                    DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeId = 33 and (DeleteFlag = 0 or DeleteFlag is null) and Id = " + theDS.Tables[1].Rows[0]["ProgId"].ToString();
                    DataTable dtfilter = theDV.ToTable();
                    if (dtfilter.Rows.Count > 0)
                    {
                        lblReturnProgram.Text = theDV[0]["Name"].ToString();
                        //DataTable theReturnDT = theDS.Tables[0];
                    }
                    BindDrugRetunDetailGrid(theDS.Tables[0]);
                }
            }
        }

        /// <summary>
        /// Handles the Enter event of the groupBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            // NxtAppDate.CustomFormat = " ";

            // btnART.Enabled = false;
            BindCombo();
            txtPatientIdentification.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
           dtRefillApp.Format= dtpDOB.Format = DateTimePickerFormat.Custom;
           dtRefillApp.CustomFormat = dtpDOB.CustomFormat = " ";

            cmbFacility.SelectedValue = GblIQCare.AppLocationId.ToString();
            dtDispensedDate.Text = GblIQCare.CurrentDate;
            dtpReturnDate.Text = GblIQCare.CurrentDate;
            cmbFacility.Enabled = false;
            lstSearch.Visible = false;
            cmdSave.Visible = false;
            cmdPrintPrescription.Visible = false;
            grpExistingRec.Visible = false;
            //chkPrintPrescription.Checked = false;
            this.btnPrintLabel.Visible = false;
            txtPatientInstructions.Enabled = true;
            Authentication();
            imageList1.Images.Add("pic1", Image.FromFile(GblIQCare.GetPath() + "\\printer.jpg"));
        }

        /// <summary>
        /// Handles the Click event of the label31 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label31_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the DoubleClick event of the lstSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            if (lstSearch.SelectedValue.ToString() != "")
            {
                string[] theId = lstSearch.SelectedValue.ToString().Split('-');
                theItemId = Convert.ToInt32(theId.GetValue(0));
                DataView theDV = new DataView(thePharmacyMaster.Tables[1]);
                theDV.RowFilter = "Drug_Pk = " + theItemId.ToString() + " and BatchId = " + theId.GetValue(1).ToString() + " and ExpiryDate='" + theId.GetValue(2).ToString() + "'";
                txtItemName.Text = theDV[0]["DrugName"].ToString();
                txtBatchNo.Text = theDV[0]["BatchNo"].ToString();
                theBatchId = Convert.ToInt32(theDV[0]["BatchId"]);
                txtExpirydate.Text = ((DateTime)theDV[0]["ExpiryDate"]).ToString(GblIQCare.AppDateFormat);
                theSellingPrice = Convert.ToDecimal(theDV[0]["SellingPrice"]);
                theItemTypeId = Convert.ToInt32(theDV[0]["ItemTypeID"]);
                this.GetPrice(theItemId, theItemTypeId);

                theConfigSellingPrice = Convert.ToDecimal(theDV[0]["ConfigSellingPrice"]);
                theCostPrice = Convert.ToDecimal(theDV[0]["CostPrice"]);
                theMargin = Convert.ToDecimal(theDV[0]["DispensingMargin"]);
                theDispensingUnit = Convert.ToInt32(theDV[0]["DispensingId"]);
                theDispensingUnitName = theDV[0]["DispensingUnit"].ToString();
                theFunded = Convert.ToInt32(theDV[0]["Funded"]);
                theAvailQty = Convert.ToInt32(theDV[0]["AvailQty"]);
                theStrength = Convert.ToInt32(theDV[0]["StrengthId"]);
                theItemType = Convert.ToInt32(theDV[0]["DrugTypeId"]);
                if (theItemType == 37 && cmbprogram.SelectedValue.ToString() == "223")
                {
                    theProphylaxis = 1;
                }
                else
                {
                    theProphylaxis = 0;
                }

                /*  if (theItemType == 37)
                  {
                      if (btnART.Enabled == false)
                      {
                          btnART.Enabled = true;
                      }
                  }
                  else
                  {
                      if (CheckARVDrugsInGrid() == 0)
                      {
                          if (btnART.Enabled == true)
                          {
                              btnART.Enabled = false;

                          }
                      }
                      else
                      {
                          btnART.Enabled = true;
                      }
                  }
                  */
                theGenericAbb = theDV[0]["GenericAbb"].ToString();
                lstSearch.Visible = false;
                cmbFrequency.Select();

                if (theAvailQty == 0)
                {
                    IQCareWindowMsgBox.ShowWindowConfirm("NoAvailQty", this);
                    return;
                }
            }
        }
        private int CheckARVDrugsInGrid()
        {
            //john
            int j = 0;
            for (int i = 0; i < grdDrugDispense.Rows.Count; i++)
            {
                try
                {
                    if (grdDrugDispense.Rows[i].Cells[23].Value.ToString() == "37")
                    {
                        j = 1;
                    }
                }
                catch { }

            }
            return j;
        }

        /// <summary>
        /// Handles the KeyUp event of the lstSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void lstSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstSearch_DoubleClick(sender, e);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the tabDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            // btnART.Enabled = false;
            if ((MainTab.TabPages["DispenseTab"].Focus() == true || MainTab.TabPages["ReturnTab"].Focus() == true) && thePatientId < 1)
            {
                IQCareWindowMsgBox.ShowWindow("PatientNotSelected", this);
                MainTab.SelectedTab = MainTab.TabPages["FindPatientTab"];
                return;
            }
            if (MainTab.TabPages["FindPatientTab"].Focus() == true)
            {
                this.thePatientId = 0;
                this.theOrderId = 0;
                this.theOrderStatus = "";
                this.Text = "Find Patient";
            }
            if (MainTab.TabPages["DispenseTab"].Focus() == true)
            {
                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "Id=" + GblIQCare.intStoreId.ToString();
                this.lblStoreName.Text = theDV[0]["Name"].ToString();
                this.cmdSave.Visible = true;
                this.cmdPrintPrescription.Visible = true;
                this.btnPrintLabel.Visible = true;
                this.theItemId = 0;
                this.theDispensingUnit = 0;
                this.theBatchId = 0;
                this.theCostPrice = 0;
                this.theMargin = 0;
                this.theBillAmt = 0;
                this.theDispensingUnitName = "";
                this.txtItemName.Text = "";
                this.txtBatchNo.Text = "";
                this.cmbFrequency.SelectedValue = "0";
                this.txtQtyDispensed.Text = "";
                this.txtExpirydate.Text = "";
                this.txtSellingPrice.Text = "";
                this.txtDose.Text = "";
                this.txtDuration.Text = "";
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
            if (MainTab.TabPages["ReturnTab"].Focus() == true)
            {
                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "Id=" + GblIQCare.intStoreId.ToString();
                lblReturnStoreName.Text = theDV[0]["Name"].ToString();
                cmdSave.Visible = true;
                cmdPrintPrescription.Visible = false;
                this.Text = "Return Drugs";
                lblReturnDispensedDate.Text = "";
                lblReturnProgram.Text = "";
                IDrug thePharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                DataTable theDT = thePharmacyManager.GetPharmacyExistingRecord(thePatientId, GblIQCare.intStoreId);
                BindDrugReturnGrid(theDT);
                grdReturnDetail.DataSource = false;
                grdReturnDetail.Columns.Clear();
            }
            if (MainTab.TabPages["IQCareWeb"].Focus() == true)
            {
                this.NavigateToIQCare();
            }
        }

        /// <summary>
        /// Handles the Enter event of the NxtAppDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NxtAppDate_Enter(object sender, EventArgs e)
        {
            NxtAppDate.CustomFormat = "dd-MMM-yyyy";
        }

        /// <summary>
        /// Removes the duplicates records.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            var result = dt.AsEnumerable()
                 .GroupBy(r => r.Field<int>("ItemID"))
                 .Select(g => g.First())
                 .CopyToDataTable();

            return result;
        }

        /// <summary>
        /// Handles the KeyPress event of the theReturnQty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void theReturnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the theReturnQty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void theReturnQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (theReturnQty.Text != "")
            {
                if (Convert.ToInt32(grdReturnDetail.Rows[theCurrentRow].Cells[7].Value) < Convert.ToInt32(theReturnQty.Text))
                {
                    IQCareWindowMsgBox.ShowWindow("ReturnQtyGrtthanIssue", this);
                    theReturnQty.Text = "";
                    return;
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtDose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDose_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
            // theBindManager.Win_Numeric(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        /// <summary>
        /// Handles the Leave event of the txtDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtDuration_Leave(object sender, EventArgs e)
        {
            DataView theDV = new DataView();
            double intqtyprescribed = 0;
            string multiplier = string.Empty;
            theDV = new DataView(XMLPharDS.Tables["mst_Frequency"]);
            string strfrequency = cmbFrequency.SelectedValue.ToString();
            theDV.RowFilter = "ID='" + strfrequency.ToString() + "'";
            DataTable theDT = theDV.ToTable();
            if (theDT.Rows.Count > 0)
            {
                multiplier = theDT.Rows[0]["multiplier"].ToString();
                if (multiplier != "0" && txtDose.Text != "" && txtDuration.Text != "")
                {
                    intqtyprescribed = Math.Ceiling((Convert.ToDouble(txtDose.Text) * Convert.ToInt32(txtDuration.Text) * Convert.ToInt32(multiplier)));
                    txtQtyPrescribed.Text = intqtyprescribed.ToString();
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the txtHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                txtBSA.Text = Math.Round(calVal, 2).ToString();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtItemName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_String(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the txtItemName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtItemName.Text != "")
            {
                lstSearch.Visible = true;
                lstSearch.Width = 900;
                lstSearch.Left = txtItemName.Left;
                lstSearch.Top = txtItemName.Top + txtItemName.Height;
                lstSearch.Height = 300;

                DataView theDV = new DataView(thePharmacyMaster.Tables[1]);
                theDV.RowFilter = "DrugName like '%" + txtItemName.Text.Trim().ToString().Replace("%", "[%]") + "%'";
                if (theDV.Count > 0)
                {
                    DataTable theDT = theDV.ToTable();
                    BindFunctions theBindManager = new BindFunctions();
                    theBindManager.Win_BindListBox(lstSearch, theDT, "DisplayItem", "DisplayItemId");
                }
                else
                {
                    lstSearch.DataSource = null;
                }
            }
            else
            {
                lstSearch.Visible = false;
            }
            if (e.KeyCode == Keys.Down)
                lstSearch.Select();
        }

        /// <summary>
        /// Handles the KeyPress event of the txtQtyDispensed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtQtyDispensed_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the txtQtyDispensed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtQtyDispensed_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQtyDispensed.Text.Trim() != "")
            {
                string s = txtQtyDispensed.Text;
                if (s.IndexOf('.', 0) != -1)
                {
                    s = s.Substring(0, s.IndexOf('.', 0));
                }
                txtQtyDispensed.Text = s;
                if (txtQtyDispensed.Text == "0")
                {
                    txtQtyDispensed.Text = "";
                    IQCareWindowMsgBox.ShowWindowConfirm("QuanitytData", this);
                    return;
                }

                if (Convert.ToInt32(txtQtyDispensed.Text) > theAvailQty)
                {
                    IQCareWindowMsgBox.ShowWindow("DrugDispenseQtyCompare", this);
                    return;
                }

                CalculateSellingPrice();

                decimal qtydis = Convert.ToDecimal(txtQtyDispensed.Text.Trim());
                decimal qtypre = Convert.ToDecimal(txtQtyPrescribed.Text.Trim());
                if (qtydis < qtypre)
                {
                    labelWhyPartial.Visible = txtWhyPartial.Visible = true;
                    txtWhyPartial.Text = "";
                }
                else { labelWhyPartial.Visible = txtWhyPartial.Visible = false; txtWhyPartial.Text = ""; }
                  
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnDispenseSubmit.Select();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtQtyPrescribed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtQtyPrescribed_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtWeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the txtWeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWeight.Text != "" && txtHeight.Text != "")
            {
                Double calVal = (Convert.ToDouble(txtWeight.Text) * Convert.ToDouble(txtHeight.Text) / 3600);
                calVal = (Double)Math.Sqrt(Convert.ToDouble(calVal));
                txtBSA.Text = Math.Round(calVal, 2).ToString();
            }
        }

        /// <summary>
        /// Validates the drug dispense.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateDrugDispense()
        {
            if (txtItemName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Drug Name";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtDose.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Dose";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (Convert.ToInt32(cmbFrequency.SelectedValue) < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Frequency";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
                return false;
            }
            if (txtDuration.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Duration";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtQtyPrescribed.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Quantity Prescribed";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            //if (Convert.ToInt32(cmbprogram.SelectedValue) < 1)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["Control"] = "Program";
            //    IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
            //    return false;
            //}
            if (txtQtyDispensed.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Quantity Dispensed";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }


            return true;
        }
        /// <summary>
        /// The web browser loaded
        /// </summary>
        private bool webBrowserLoaded = false;
        private void IQBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowserLoaded = true;
        }

        /// <summary>
        /// Navigates to iq care.
        /// </summary>
        private void NavigateToIQCare()
        {
            try
            {
                if (!this.webBrowserLoaded)
                {
                    string address = ConfigurationManager.AppSettings.Get("IQCareURL");
                    string AdditionalHeaders = "Content-Type: application/x-www-form-urlencoded";
                    IQBrowser.Navigate(new Uri(address), string.Empty, null, AdditionalHeaders);
                }
                else
                {
                    IQBrowser.Select();
                }
            }
            catch (Exception ex)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        //private void chkPharmacyRefill_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!this.chkPharmacyRefill.Checked)
        //    {
        //        this.dtRefillApp.CustomFormat = " ";
        //        this.dtRefillApp.Enabled = false;
        //    }
        //    else
        //    {
        //        this.dtRefillApp.CustomFormat = "dd-MMM-yyyy";
        //        this.dtRefillApp.Enabled = true;
        //    }
        //}

        private void dtRefillApp_Enter(object sender, EventArgs e)
        {
            this.dtRefillApp.CustomFormat = "dd-MMM-yyyy";
        }

        private void dtDispensedDate_ValueChanged(object sender, EventArgs e)
        {
            if (!(this.dtDispensedDate.Value > DateTime.Today))
                return;
            int num = (int)MessageBox.Show("Dispensing date cannot be greater than current date.");
            this.dtDispensedDate.Value = DateTime.Today;
        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            GblIQCare.dtPrintLabel = (DataTable)this.grdDrugDispense.DataSource;
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
            }
        }

        private void btnSaveComplete_Click(object sender, EventArgs e)
        {

        }




    }
}