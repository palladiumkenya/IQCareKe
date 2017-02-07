using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.SCM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace IQCare.SCM
{
    public partial class frmGoodsRecieveNote : Form
    {
        public int StoreID = 0;
        DataSet dsPOItems = new DataSet();
        DataSet dsPOItemsDetail = new DataSet();
        public ComboBox theGrdCombo;
        DataTable dtGRNmaster;
        DataTable dtGRNItem;
        TextBox txtBatchName;
        DataTable theDTBatch = new DataTable();
        int GrnId = 0;
        AutoCompleteStringCollection BatchCollection = new AutoCompleteStringCollection();
        /* No used 
        /////// <summary>
        /////// if IsPO = 2 then No interstore transfer and no Purchase Order  Selected
        /////// if IsPO = 1 then  Purchase Order  Selected
        /////// if IsPO = 0 then  Interstore transfer  Selected
        /////// </summary>
        ////int IsPurchaseOrder = 2;
          */

        /// <summary>
        /// GblIQCare.ModePurchaseOrder =1 then Purchase Order 
        /// GblIQCare.ModePurchaseOrder = 2 then InterStore Transfer 
        /// </summary>
       // bool IsPOUpdated = false;

        public frmGoodsRecieveNote()
        {
            InitializeComponent();
        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.GoodReceiveNotes, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void frmGoodsRecieveNote_Load(object sender, EventArgs e)
        {
            dtpOrderDate.CustomFormat = "dd-MMM-yyyy";
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            SetRights();
            dtpOrderDate.Enabled = false;
            dgwPOItems.AllowUserToAddRows = false;
            BindStoreName();
            if (GblIQCare.PurchaseOrderID != 0)
            {
                formInit();
            }

        }
        private DataTable CreateGRNMasterTable()
        {

            DataTable dtGRNmaster = new DataTable();
            dtGRNmaster.Columns.Add("POID", typeof(int));
            dtGRNmaster.Columns.Add("GRNId", typeof(int));
            dtGRNmaster.Columns.Add("LocationID", typeof(int));
            dtGRNmaster.Columns.Add("OrderDate", typeof(DateTime));
            dtGRNmaster.Columns.Add("SupplierID", typeof(int));
            dtGRNmaster.Columns.Add("SourceStoreID", typeof(int));
            dtGRNmaster.Columns.Add("DestinStoreID", typeof(int));
            dtGRNmaster.Columns.Add("UserID", typeof(int));
            dtGRNmaster.Columns.Add("RecievedDate", typeof(DateTime));
            dtGRNmaster.Columns.Add("OrderNo", typeof(string));
            dtGRNmaster.Columns.Add("Freight", typeof(decimal));
            dtGRNmaster.Columns.Add("Tax", typeof(decimal));
            return dtGRNmaster;
        }
        private DataTable CreatePOItemTable()
        {
            DataTable dtPOItem = new DataTable();
            dtPOItem.Columns.Add("GRNId", typeof(int));
            dtPOItem.Columns.Add("ItemID", typeof(int));
            dtPOItem.Columns.Add("ItemName", typeof(string));
            dtPOItem.Columns.Add("ItemCode", typeof(string));
            dtPOItem.Columns.Add("OrderQuantity", typeof(int));
            dtPOItem.Columns.Add("TotPrice", typeof(decimal));
            dtPOItem.Columns.Add("Price", typeof(decimal));
            dtPOItem.Columns.Add("Units", typeof(string));
            dtPOItem.Columns.Add("TotalAmount", typeof(decimal));
            if (GblIQCare.ModePurchaseOrder == 2)
            {
                dtPOItem.Columns.Add("ISTItemID", typeof(string));
            }
            return dtPOItem;
        }
        private DataTable CreateGRNItemTable()
        {
            DataTable dtGRNItem = new DataTable();
            dtGRNItem.Columns.Add("AutoID", typeof(int));
            dtGRNItem.Columns.Add("GRNId", typeof(int));
            dtGRNItem.Columns.Add("ItemID", typeof(int));
            dtGRNItem.Columns.Add("BatchID", typeof(int));
            dtGRNItem.Columns.Add("BatchName", typeof(string));
            dtGRNItem.Columns.Add("RecievedQuantity", typeof(int));
            dtGRNItem.Columns.Add("QtyPerPurchaseUnit", typeof(int));
            dtGRNItem.Columns.Add("FreeRecievedQuantity", typeof(int));
            dtGRNItem.Columns.Add("ItemPurchasePrice", typeof(decimal));
            dtGRNItem.Columns.Add("TotPurchasePrice", typeof(decimal));
            dtGRNItem.Columns.Add("MasterPurchaseprice", typeof(decimal));
            dtGRNItem.Columns.Add("Margin", typeof(decimal));
            dtGRNItem.Columns.Add("SellingPrice", typeof(decimal));
            dtGRNItem.Columns.Add("SellingPricePerDispense", typeof(decimal));
            dtGRNItem.Columns.Add("ExpiryDate", typeof(DateTime));
            dtGRNItem.Columns.Add("UserID", typeof(int));
            dtGRNItem.Columns.Add("POId", typeof(int));
            dtGRNItem.Columns.Add("SourceStoreID", typeof(int));
            dtGRNItem.Columns.Add("DestinStoreID", typeof(int));
            //dtGRNItem.Columns.Add("InKindFlag", typeof(int));
            if (GblIQCare.ModePurchaseOrder == 2)
            {
                dtGRNItem.Columns.Add("ISTItemID", typeof(string));
            }
            return dtGRNItem;
        }
        private void BindSupplierDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            try
            {
                BindFunctions theBind = new BindFunctions();
                ddlSupplier.DataSource = null;
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                BindFunctions theBindManager = new BindFunctions();
                DataView theDV = new DataView(XMLDS.Tables["Mst_Supplier"]);
                theDV.RowFilter = "(DeleteFlag = 0 or  DeleteFlag is null)";
                DataTable theStoreDT = theDV.ToTable();
                theBind.Win_BindCombo(ddlSupplier, theStoreDT, "SupplierName", "Id");

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void BindStoreName()
        {
            StoreID = GblIQCare.intStoreId;
            //ddlSourceStore.Enabled = true;
            BindFunctions theBind = new BindFunctions();
            DataSet XMLDS = new DataSet();
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            DataView theDV;
            DataTable theStoreDT;
            //////theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            //////theDV.RowFilter = "(DeleteFlag =0 or  DeleteFlag is null)"; // and (Id = " + StoreID + ")";
            //////theStoreDT = theDV.ToTable();
            //////ddlSourceStore.DataSource = theStoreDT;
            //////ddlSourceStore.DisplayMember = "Name";
            //////ddlSourceStore.ValueMember = "Id";

            //////ddlSourceStore.Enabled = false;
            //////if (rdoPurchaseOrder.Checked)
            //////{
            //////}
            //////else if (rdoInterStoreTransfer.Checked)
            //////{
            //////    theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            //////    theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";// and  ( Id <>" + StoreID + ")";
            //////    theStoreDT = theDV.ToTable();
            //////    theBind.Win_BindCombo(ddlDestinationStore, theStoreDT, "Name", "Id");

            //////}
            //if (rdoPurchaseOrder.Checked)
            //{
            //    ddlSourceStore.Enabled = false;
            //}
            //else if (rdoInterStoreTransfer.Checked)
            //{
            //    ddlSourceStore.Enabled = true;
            //    theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            //    theDV.RowFilter = "(CentralStore = 1) and (DeleteFlag =0 or  DeleteFlag is null) ";
            //    theStoreDT = theDV.ToTable();
            //    theBind.Win_BindCombo(ddlSourceStore, theStoreDT, "Name", "Id");
            //}
            theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null) and  ( Id =" + StoreID + ")";
            theStoreDT = theDV.ToTable();
            //theBind.Win_BindCombo(ddlDestinationStore, theStoreDT, "Name", "Id");
            ddlDestinationStore.DataSource = theStoreDT;
            ddlDestinationStore.DisplayMember = "Name";
            ddlDestinationStore.ValueMember = "Id";

        }

        
        private void BindGrid(int PurchaseMode)
        {
            //IMasterList objPOItem = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            // dsPOItems = objPOItem.GetPurcaseOrderItem(PurchaseMode, GblIQCare.AppUserId, GblIQCare.intStoreId);
            if (PurchaseMode == 1)
            {
                dsPOItems = objPOItem.GetPurcaseOrderItem(PurchaseMode, GblIQCare.AppUserId, 0);
            }
            else if (PurchaseMode == 2)
            {
                //if(ddlSourceStore.SelectedValue !="0")
                //{
                //dsPOItems = objPOItem.GetPurcaseOrderItem(PurchaseMode, GblIQCare.AppUserId, Convert.ToInt32(ddlSourceStore.SelectedValue));
                //}
            }

            BindGrid();
            // BindGrid(dsPOItems);

        }
        public void formInit()
        {
            try
            {
                //IMasterList objPOItem = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                dsPOItemsDetail = objPOItem.GetPurchaseOrderDetailsByPoidGRN(GblIQCare.PurchaseOrderID);
                if (dsPOItemsDetail.Tables.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dsPOItemsDetail.Tables[0].Rows[0]["GRNId"])))
                    {
                        GrnId = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["GRNId"]);
                    }
                    //BindFunctions theBindManager = new BindFunctions();
                    //DataView theDV = new DataView(dsPOItemsDetail.Tables[3]);
                    //theDTBatch = theDV.ToTable();
                    //theBindManager.Win_BindListBox(lstSearchBatch, theDTBatch, "Name", "Id");

                    if (GblIQCare.ModePurchaseOrder == 2)
                    {
                        for (int i = 0; i < dsPOItemsDetail.Tables[3].Rows.Count; i++)
                        {
                            BatchCollection.Add(dsPOItemsDetail.Tables[3].Rows[i]["Name"].ToString());
                        }
                    }

                    if (dsPOItemsDetail.Tables[0].Rows.Count > 0)
                    {
                     
                        dtpOrderDate.Text = dsPOItemsDetail.Tables[0].Rows[0]["OrderDate"].ToString();
                        dtpOrderDate.CustomFormat = "dd-MMM-yyyy";
                        dtpOrderDate.Enabled = false;
                        txtOrderNumber.Text = dsPOItemsDetail.Tables[0].Rows[0]["OrderNo"].ToString();
                        txtOrderNumber.Enabled = false;
                        //ddlSourceStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SourceStoreID"].ToString());
                        //ddlSourceStore.Enabled = false;
                        if (GblIQCare.ModePurchaseOrder == 1)
                        {
                            ddlSupplier.DataSource = null;
                            BindFunctions theBind = new BindFunctions();
                            ddlSupplier.DataSource = null;
                            DataSet XMLDS = new DataSet();
                            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                            BindFunctions theBindManager = new BindFunctions();
                            DataView theDV = new DataView(XMLDS.Tables["Mst_Supplier"]);
                            theDV.RowFilter = "(DeleteFlag = 0 or  DeleteFlag is null) or  (Id = " + Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SupplierID"].ToString()) + ")";
                            DataTable theStoreDT = theDV.ToTable();
                            theBind.Win_BindCombo(ddlSupplier, theStoreDT, "SupplierName", "Id");
                            ddlSupplier.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SupplierID"].ToString());
                            ddlSupplier.Enabled = false;
                        }
                        else if (GblIQCare.ModePurchaseOrder == 2)
                        {
                         ddlDestinationStore.DataSource = null;
                            DataSet XMLDS = new DataSet();
                            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                            BindFunctions theBindManager = new BindFunctions();
                            // Id <>" + StoreID + " or
                            DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null )and  (Id =" + Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["DestinStoreID"].ToString()) + ") ";
                            DataTable theStoreDT = theDV.ToTable();
                            theBindManager.Win_BindCombo(ddlDestinationStore, theStoreDT, "Name", "Id");
                          
                        }
                        ddlDestinationStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["DestinStoreID"].ToString());
                        ddlDestinationStore.Enabled = false;
                        //ddlPreparedBy.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["PreparedBy"].ToString());
                        if (dsPOItemsDetail.Tables[0].Rows[0]["Status"].ToString() == "1")
                        {

                            btnSave.Enabled = true;
                           // IsPOUpdated = true;
                        }
                        else if (dsPOItemsDetail.Tables[0].Rows[0]["Status"].ToString() == "5")
                        {
                            btnSave.Enabled = false;
                            //IsPOUpdated = false;

                        }
                        else
                        {

                        }
                        //lblTotalAmount.Text = dsPOItemsDetail.Tables[1].Rows[0]["TotalAmount"].ToString();
                        BindGrid();
                        //dgwPOItems.AllowUserToAddRows = true;
                        dtGRNmaster = CreateGRNMasterTable();
                        dtGRNItem = CreateGRNItemTable();
                        DataTable thetempDT = dsPOItemsDetail.Tables[2].Copy();
                        DataView theDVTogetItem = new DataView(thetempDT);
                        if (GblIQCare.ModePurchaseOrder == 2)
                        {
                            theDVTogetItem.RowFilter = "RecievedQuantity >0";
                        }
                        else if (GblIQCare.ModePurchaseOrder == 1)
                        {
                            theDVTogetItem.RowFilter = "BatchName <>''";
                        }
                        dtGRNItem = theDVTogetItem.ToTable();

                        dgwPOItems.DataSource = dsPOItemsDetail.Tables[1];


                        // dgwPOItems.Rows[0].Selected = true;
                    }
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }

        private void brnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindGrid()
        {
            try
            {
                dgwPOItems.Columns.Clear();
                dgwPOItems.AutoGenerateColumns = false;
                dgwPOItems.AllowUserToAddRows = false;

                DataGridViewTextBoxColumn theColumnGRNId = new DataGridViewTextBoxColumn();
                theColumnGRNId.Name = "GRNId";
                theColumnGRNId.DataPropertyName = "GRNId";
                theColumnGRNId.Width = 50;
                theColumnGRNId.ReadOnly = true;
                theColumnGRNId.Visible = false;

                DataGridViewTextBoxColumn theColumnItemID = new DataGridViewTextBoxColumn();
                theColumnItemID.Name = "ItemID";
                theColumnItemID.DataPropertyName = "ItemID";
                theColumnItemID.Width = 50;
                theColumnItemID.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnItemName = new DataGridViewTextBoxColumn();
                theColumnItemName.HeaderText = "Item Name";
                theColumnItemName.Name = "ItemName";
                theColumnItemName.DataPropertyName = "ItemName";
                theColumnItemName.Width = 350;
                theColumnItemName.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnItemCode = new DataGridViewTextBoxColumn();
                theColumnItemCode.HeaderText = "Item Code";
                theColumnItemCode.DataPropertyName = "ItemCode";
                theColumnItemCode.Name = "ItemCode";
               
                theColumnItemCode.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnOrderQuantity = new DataGridViewTextBoxColumn();
                theColumnOrderQuantity.HeaderText = "Ordered Qty";
                theColumnOrderQuantity.DataPropertyName = "OrderQuantity";
                theColumnOrderQuantity.Name = "OrderQuantity";
               
                theColumnOrderQuantity.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnTotPrice = new DataGridViewTextBoxColumn();
                theColumnTotPrice.HeaderText = "Total Purchase Price";
                theColumnTotPrice.DataPropertyName = "TotPrice";
                theColumnTotPrice.Name = "TotPrice";
               
                theColumnTotPrice.ReadOnly = true;


                DataGridViewTextBoxColumn theColumnPrice = new DataGridViewTextBoxColumn();
                theColumnPrice.HeaderText = "Price Purchase /Purchase Unit";
                theColumnPrice.DataPropertyName = "Price";
                theColumnPrice.Name = "Price";
                
                theColumnPrice.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnUnits = new DataGridViewTextBoxColumn();
                theColumnUnits.HeaderText = "Units";
                theColumnUnits.DataPropertyName = "Units";
                theColumnUnits.Name = "Units";
                
                theColumnUnits.ReadOnly = true;



                dgwPOItems.DataSource = CreatePOItemTable();
                theColumnItemID.Visible = false;
                dgwPOItems.Columns.Add(theColumnGRNId);
                theColumnGRNId.Visible = false;
                dgwPOItems.Columns.Add(theColumnItemID);
                theColumnItemID.Visible = false;
                dgwPOItems.Columns.Add(theColumnItemName);
                dgwPOItems.Columns.Add(theColumnItemCode);
                dgwPOItems.Columns.Add(theColumnOrderQuantity);
                dgwPOItems.Columns.Add(theColumnUnits);
                dgwPOItems.Columns.Add(theColumnPrice);
                dgwPOItems.Columns.Add(theColumnTotPrice);
                if (GblIQCare.ModePurchaseOrder == 1)
                {
                    theColumnItemCode.Width = 100;
                    theColumnOrderQuantity.Width = 90;
                    theColumnUnits.Width = 90;
                    theColumnPrice.Width = 90;
                    theColumnTotPrice.Width = 85;
                }
                else if (GblIQCare.ModePurchaseOrder == 2)
                {
                    theColumnItemCode.Width = 60;
                    theColumnOrderQuantity.Width = 60;
                    theColumnTotPrice.Width = 60;
                    theColumnPrice.Width = 60;
                    theColumnUnits.Width = 60;

                    DataGridViewTextBoxColumn theColumnBatchName = new DataGridViewTextBoxColumn();
                    theColumnBatchName.HeaderText = "Batch No.";
                    theColumnBatchName.Name = "BatchName";
                    theColumnBatchName.DataPropertyName = "BatchName";
                    theColumnBatchName.Width = 100;
                    theColumnBatchName.ReadOnly = true;

                    DataGridViewTextBoxColumn theColumnExpiryDate = new DataGridViewTextBoxColumn();
                    theColumnExpiryDate.HeaderText = "Expiry Date";
                    theColumnExpiryDate.Name = "ExpiryDate";
                    theColumnExpiryDate.DataPropertyName = "ExpiryDate";
                    theColumnExpiryDate.Width = 80;
                    theColumnExpiryDate.ReadOnly = true;

                    DataGridViewTextBoxColumn theColumnBatchID = new DataGridViewTextBoxColumn();
                    theColumnBatchID.HeaderText = "BatchID";
                    theColumnBatchID.Name = "BatchID";
                    theColumnBatchID.DataPropertyName = "BatchID";
                    theColumnBatchID.Width = 100;
                    theColumnBatchID.ReadOnly = true;
                    theColumnBatchID.Visible = false;

                    DataGridViewTextBoxColumn theColumnISTitem = new DataGridViewTextBoxColumn();
                    theColumnISTitem.HeaderText = "ISTItemID";
                    theColumnISTitem.Name = "ISTItemID";
                    theColumnISTitem.DataPropertyName = "ISTItemID";
                    theColumnISTitem.Width = 100;
                    theColumnISTitem.ReadOnly = true;
                    theColumnISTitem.Visible = false;

                    dgwPOItems.Columns.Add(theColumnExpiryDate);
                    dgwPOItems.Columns.Add(theColumnBatchName);
                    dgwPOItems.Columns.Add(theColumnBatchID);
                    dgwPOItems.Columns.Add(theColumnISTitem);
                }
                //  BindGrnItemsGrid();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }
        private void BindGrnItemsGrid()
        {
            try
            {
                dgwGRNItems.Columns.Clear();
                dgwGRNItems.AutoGenerateColumns = false;
                dgwGRNItems.AllowUserToAddRows = false;


                DataGridViewTextBoxColumn theColumnGRNId = new DataGridViewTextBoxColumn();
                theColumnGRNId.HeaderText = "GRNId";
                theColumnGRNId.DataPropertyName = "GRNId";
                theColumnGRNId.Name = "GRNId";
                theColumnGRNId.Width = 50;
                theColumnGRNId.ReadOnly = true;


                DataGridViewTextBoxColumn theColumnAutoId = new DataGridViewTextBoxColumn();
                theColumnAutoId.HeaderText = "AutoID";
                theColumnAutoId.DataPropertyName = "AutoID";
                theColumnAutoId.Name = "AutoID";
                theColumnAutoId.Width = 50;
                theColumnAutoId.ReadOnly = true;


                DataGridViewTextBoxColumn theColumnItemID = new DataGridViewTextBoxColumn();
                theColumnItemID.HeaderText = "ItemID";
                theColumnItemID.DataPropertyName = "ItemID";
                theColumnItemID.Name = "ItemID";
                theColumnItemID.Width = 50;
                theColumnItemID.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnBatchID = new DataGridViewTextBoxColumn();
                theColumnBatchID.HeaderText = "BatchID";
                theColumnBatchID.DataPropertyName = "BatchID";
                theColumnBatchID.Name = "BatchID";
                theColumnBatchID.Width = 100;
                theColumnBatchID.ReadOnly = true;

                //
                DataGridViewTextBoxColumn theColumnQtyPerPurchaseUnit = new DataGridViewTextBoxColumn();
                theColumnQtyPerPurchaseUnit.HeaderText = "QtyPerPurchaseUnit";
                theColumnQtyPerPurchaseUnit.DataPropertyName = "QtyPerPurchaseUnit";
                theColumnQtyPerPurchaseUnit.Name = "QtyPerPurchaseUnit";
                theColumnQtyPerPurchaseUnit.Width = 100;
                theColumnQtyPerPurchaseUnit.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnBatchName = new DataGridViewTextBoxColumn();
                theColumnBatchName.HeaderText = "Batch No.";
                theColumnBatchName.DataPropertyName = "BatchName";
                theColumnBatchName.Name = "BatchName";
                theColumnBatchName.Width = 200;
                theColumnBatchName.ReadOnly = false;



                DataGridViewTextBoxColumn theColumnRecievedQuantity = new DataGridViewTextBoxColumn();
                theColumnRecievedQuantity.HeaderText = "Recieved Qty";
                theColumnRecievedQuantity.DataPropertyName = "RecievedQuantity";
                theColumnRecievedQuantity.Name = "RecievedQuantity";
                theColumnRecievedQuantity.Width = 150;
                theColumnRecievedQuantity.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnFreeRecievedQuantity = new DataGridViewTextBoxColumn();
                theColumnFreeRecievedQuantity.HeaderText = "Free Recieved Quantity";
                theColumnFreeRecievedQuantity.DataPropertyName = "FreeRecievedQuantity";
                theColumnFreeRecievedQuantity.Name = "FreeRecievedQuantity";
                theColumnFreeRecievedQuantity.Width = 100;
                theColumnFreeRecievedQuantity.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnItemPurchasePrice = new DataGridViewTextBoxColumn();
                theColumnItemPurchasePrice.HeaderText = "Item Purchase Price";
                theColumnItemPurchasePrice.DataPropertyName = "ItemPurchasePrice";
                theColumnItemPurchasePrice.Name = "ItemPurchasePrice";
                theColumnItemPurchasePrice.Width = 150;
                theColumnItemPurchasePrice.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnTotPurchasePrice = new DataGridViewTextBoxColumn();
                theColumnTotPurchasePrice.HeaderText = "Total Purchase Price";
                theColumnTotPurchasePrice.DataPropertyName = "TotPurchasePrice";
                theColumnTotPurchasePrice.Name = "TotPurchasePrice";
                theColumnTotPurchasePrice.Width = 150;
                theColumnTotPurchasePrice.ReadOnly = true;


                DataGridViewTextBoxColumn theColumnMasterPurchasePrice = new DataGridViewTextBoxColumn();
                theColumnMasterPurchasePrice.HeaderText = "MasterPurchasePrice";
                theColumnMasterPurchasePrice.DataPropertyName = "MasterPurchasePrice";
                theColumnMasterPurchasePrice.Name = "MasterPurchasePrice";
                theColumnMasterPurchasePrice.Width = 100;
                theColumnMasterPurchasePrice.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnMargin = new DataGridViewTextBoxColumn();
                theColumnMargin.HeaderText = "Margin %";
                theColumnMargin.DataPropertyName = "Margin";
                theColumnMargin.Name = "Margin";
                theColumnMargin.Width = 100;
                theColumnMargin.ReadOnly = true;
                theColumnMargin.Visible = false;


                DataGridViewTextBoxColumn theColumnSellingPrice = new DataGridViewTextBoxColumn();
                theColumnSellingPrice.HeaderText = "Selling Price/ Purchase Unit";
                theColumnSellingPrice.DataPropertyName = "SellingPrice";
                theColumnSellingPrice.Name = "SellingPrice";
                theColumnSellingPrice.Width = 100;
                theColumnSellingPrice.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnSellingPriceDispense = new DataGridViewTextBoxColumn();
                theColumnSellingPriceDispense.HeaderText = "Selling Price/Dispensing Unit";
                theColumnSellingPriceDispense.DataPropertyName = "SellingPricePerDispense";
                theColumnSellingPriceDispense.Name = "SellingPricePerDispense";
                theColumnSellingPriceDispense.Width = 100;
                theColumnSellingPriceDispense.ReadOnly = true;

                CalendarColumn col = new CalendarColumn();
                col.HeaderText = "Expiry Date";
                col.DataPropertyName = "ExpiryDate";
                col.Name = "ExpiryDate";
                col.Width = 150;
                col.MaxDate = DateTime.MaxValue;
                col.MinDate = DateTime.MinValue;

              
                dgwGRNItems.DataSource = CreateGRNItemTable();

                dgwGRNItems.Columns.Add(theColumnGRNId);
                theColumnGRNId.Visible = false;



                dgwGRNItems.Columns.Add(theColumnItemID);
                theColumnItemID.Visible = false;
                dgwGRNItems.Columns.Add(theColumnBatchID);
                theColumnBatchID.Visible = false;
                dgwGRNItems.Columns.Add(theColumnQtyPerPurchaseUnit);
                theColumnQtyPerPurchaseUnit.Visible = false;
                dgwGRNItems.Columns.Add(theColumnBatchName);
                //Expiry Date
                dgwGRNItems.Columns.Add(col);
                dgwGRNItems.Columns.Add(theColumnRecievedQuantity);
                theColumnFreeRecievedQuantity.Visible = false;
                dgwGRNItems.Columns.Add(theColumnFreeRecievedQuantity);
                theColumnFreeRecievedQuantity.Visible = false;
                dgwGRNItems.Columns.Add(theColumnItemPurchasePrice);
                dgwGRNItems.Columns.Add(theColumnTotPurchasePrice);

                dgwGRNItems.Columns.Add(theColumnMasterPurchasePrice);
                theColumnMasterPurchasePrice.Visible = false;
                dgwGRNItems.Columns.Add(theColumnMargin);

                dgwGRNItems.Columns.Add(theColumnSellingPrice);
                theColumnSellingPrice.Visible = false;
                dgwGRNItems.Columns.Add(theColumnSellingPriceDispense);
                theColumnSellingPriceDispense.Visible = false;
                //dgwGRNItems.Columns.Add(theColInKind);
                //theColInKind.Visible = true;
                dgwGRNItems.Columns.Add(theColumnAutoId);
                theColumnAutoId.Visible = false;

                if (GblIQCare.ModePurchaseOrder == 2)
                {
                    
                    DataGridViewTextBoxColumn theColumnISTitem = new DataGridViewTextBoxColumn();
                    theColumnISTitem.HeaderText = "ISTItemID";
                    theColumnISTitem.Name = "ISTItemID";
                    theColumnISTitem.DataPropertyName = "ISTItemID";
                    theColumnISTitem.Width = 100;
                    theColumnISTitem.ReadOnly = true;
                    theColumnISTitem.Visible = false;

                    col.ReadOnly = true;
                    theColumnBatchName.ReadOnly = true;
                    //dgwGRNItems.Columns.Add(theColumnExpiryDate);
                    dgwGRNItems.Columns.Add(theColumnISTitem);
                }

                // dgwGRNItems.Columns.Add(theColumnExpiryDate);

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }


        private void dgwGRNItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgwDataGridForEvent = sender as DataGridView;
            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 4)
            {
                txtBatchName = e.Control as TextBox;
                if (txtBatchName != null)
                {
                    //txtBatchName.KeyUp += null;
                    txtBatchName.KeyPress += new KeyPressEventHandler(txtValidation_KeyPress);
                    txtBatchName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtBatchName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtBatchName.AutoCompleteCustomSource = BatchCollection;
                }
            }

            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 6)
            {
                int tempreceiveQuantity = 0;
                DataTable theDTItems = (DataTable)dgwDataGridForEvent.DataSource;
                for (int i = 0; i < theDTItems.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(theDTItems.Rows[i]["RecievedQuantity"])))
                    {
                        tempreceiveQuantity = tempreceiveQuantity + Convert.ToInt32(theDTItems.Rows[i]["RecievedQuantity"].ToString());
                    }
                    if (tempreceiveQuantity > _orderQuantity)
                    {
                        IQCareWindowMsgBox.ShowWindow("GRNrecQtnGreaterOrderQtn", this);
                        theDTItems.Rows[i]["RecievedQuantity"] = 0;
                        theDTItems.Rows[i]["TotPurchasePrice"] = 0;

                        return;
                    }
                }
                
            }
            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 5)
            {
                TextBox txtRecievedQuantity = e.Control as TextBox;
                if (txtRecievedQuantity != null)
                {
                    txtRecievedQuantity.AutoCompleteCustomSource = null;
                    txtRecievedQuantity.KeyPress += new KeyPressEventHandler(txtValidation_KeyPress);

                }
            }
            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 6)
            {
                TextBox txttFreeRecievedQuantity = e.Control as TextBox;
                if (txttFreeRecievedQuantity != null)
                {
                    txttFreeRecievedQuantity.AutoCompleteCustomSource = null;
                    txttFreeRecievedQuantity.KeyPress += new KeyPressEventHandler(txtValidation_KeyPress);
                }
            }
            
            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 8)
            {
                TextBox txttItemPurchasePrice = e.Control as TextBox;
                if (txttItemPurchasePrice != null)
                {
                    txttItemPurchasePrice.AutoCompleteCustomSource = null;
                    txttItemPurchasePrice.KeyPress += new KeyPressEventHandler(txtValidation_KeyPress);
                }
            }
        }
        //private void chkInKindFlag_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(e.ToString());


        //}
        void txtValidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBind = new BindFunctions();
            if (dgwGRNItems.CurrentCell.ColumnIndex == 5 || dgwGRNItems.CurrentCell.ColumnIndex == 6)
            {
                theBind.Win_Numeric(e);
            }
            else if (dgwGRNItems.CurrentCell.ColumnIndex == 8)
            {
                theBind.Win_decimal(e);
            }
            else if (dgwGRNItems.CurrentCell.ColumnIndex == 4)
            {

            }

        }

        private void txtBatchName_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void lstSearchBatch_DoubleClick(object sender, EventArgs e)
        {
        }

        private void lstSearchBatch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    lstSearchBatch_DoubleClick(sender, e);
            //}

        }
        int _itemId = 0;
        int _orderQuantity = 0;
        string ISTItemID = "";
        private void dgwPOItems_Click(object sender, EventArgs e)
        {

            bool ret = FillGridItemToSave();
            if (ret)
            {
                BindGRNItemByclickPoItem();
            }
        }
        private bool FillGridItemToSave()
        {
            bool isretValidate = false;
            int itemcount = 0;
            int GridItemCount = 0;

            DataView theDVGridItem = new DataView((DataTable)dgwGRNItems.DataSource);
            DataView theDVGrnItem = new DataView(dtGRNItem);
            if (GblIQCare.ModePurchaseOrder == 2)
            {
                theDVGridItem.RowFilter = "(ISTItemID ='" + ISTItemID + "' and  RecievedQuantity >0)";
                theDVGrnItem.RowFilter = "(ISTItemID ='" + ISTItemID + "' and  RecievedQuantity >0)";
            }
            else if (GblIQCare.ModePurchaseOrder == 1)
            {
                theDVGridItem.RowFilter = "(ItemID ='" + _itemId + "')";
                theDVGrnItem.RowFilter = "(ItemID ='" + _itemId + "')";
            }
            if (theDVGrnItem.Count > 0)
            {
                itemcount = theDVGrnItem.ToTable().Rows.Count;
            }
            if (theDVGridItem.Count > 0)
            {
                GridItemCount = theDVGridItem.ToTable().Rows.Count;
            }
            if (GridItemCount == itemcount)
            {
            }
            else
            {

                for (int i = 0; i < dgwGRNItems.Rows.Count; i++)
                {

                    if (Convert.ToString(dgwGRNItems.Rows[i].Cells["BatchName"].Value) != "")
                    {
                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells["RecievedQuantity"].Value) == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "Recieved Quantity";
                            IQCareWindowMsgBox.ShowWindow("GoodRecNoteEmpty", theBuilder, this);

                            return isretValidate;
                        }

                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells["RecievedQuantity"].Value) == "0")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "Recieved Quantity";
                            IQCareWindowMsgBox.ShowWindow("GoodRecNoteZero", theBuilder, this);
                            return isretValidate;
                        }

                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells["ItemPurchasePrice"].Value) == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "Item Purchase Price";
                            IQCareWindowMsgBox.ShowWindow("GoodRecNoteEmpty", theBuilder, this);
                            return isretValidate;
                        }

                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells["BatchName"].Value) == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "Batch Name";
                            IQCareWindowMsgBox.ShowWindow("GoodRecNoteEmpty", theBuilder, this);
                            return isretValidate;
                        }
                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells["ExpiryDate"].Value) == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "ExpiryDate";
                            IQCareWindowMsgBox.ShowWindow("GoodRecNoteEmpty", theBuilder, this);
                            return isretValidate;
                        }

                        if (Convert.ToDateTime(dgwGRNItems.Rows[i].Cells["ExpiryDate"].Value) < Convert.ToDateTime(System.DateTime.Now))
                        {
                            IQCareWindowMsgBox.ShowWindow("ExpiryDate", this);                            
                            return isretValidate;
                        }

                        // OrderQuantity
                        if (Convert.ToString(dgwGRNItems.Rows[i].Cells[0].Value) == "" || Convert.ToString(dgwGRNItems.Rows[i].Cells[0].Value) == "-1")
                        // if (Convert.ToString(dgwGRNItems.Rows[i].Cells["GrnID"].Value) == "")
                        {

                          
                            DataRow theDRowItem = dtGRNItem.NewRow();
                            if (!string.IsNullOrEmpty(Convert.ToString(dgwGRNItems.Rows[i].Cells["GrnID"].Value)))
                            {
                                if (Convert.ToInt32(dgwGRNItems.Rows[i].Cells["GrnID"].Value) > 0)
                                {
                                    theDRowItem["GrnID"] = Convert.ToInt32(dgwGRNItems.Rows[i].Cells["GrnID"].Value);
                                }
                                else
                                {
                                    theDRowItem["GrnID"] = 0;
                                }//GrnId;//
                                theDRowItem["AutoID"] = i;
                            }
                            else
                            {
                                theDRowItem["GrnID"] = 0;
                                theDRowItem["AutoID"] = i + 1;

                            }
                            theDRowItem["ItemID"] = Convert.ToInt32(dgwGRNItems.Rows[i].Cells[1].Value);
                            theDRowItem["BatchName"] = Convert.ToString(dgwGRNItems.Rows[i].Cells["BatchName"].Value);
                            theDRowItem["RecievedQuantity"] = Convert.ToInt32(dgwGRNItems.Rows[i].Cells["RecievedQuantity"].Value);
                            theDRowItem["QtyPerPurchaseUnit"] = Convert.ToInt32(dgwGRNItems.Rows[i].Cells["QtyPerPurchaseUnit"].Value);
                            if (!string.IsNullOrEmpty(Convert.ToString(dgwGRNItems.Rows[i].Cells["FreeRecievedQuantity"].Value)))
                            {
                                theDRowItem["FreeRecievedQuantity"] = Convert.ToInt32(dgwGRNItems.Rows[i].Cells["FreeRecievedQuantity"].Value);
                            }
                            else
                            {
                                theDRowItem["FreeRecievedQuantity"] = 0;
                            }
                            theDRowItem["ItemPurchasePrice"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["ItemPurchasePrice"].Value);
                            theDRowItem["Margin"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["Margin"].Value);
                            theDRowItem["TotPurchasePrice"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["TotPurchasePrice"].Value);
                            //theDRowItem["SellingPrice"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["SellingPrice"].Value);
                            //theDRowItem["SellingPricePerDispense"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["SellingPricePerDispense"].Value);

                            theDRowItem["SellingPrice"] = 0;
                            theDRowItem["SellingPricePerDispense"] = 0;

                            theDRowItem["MasterPurchaseprice"] = Convert.ToDecimal(dgwGRNItems.Rows[i].Cells["MasterPurchaseprice"].Value);

                            theDRowItem["ExpiryDate"] = string.Format("{0:M/d/yyyy}", Convert.ToDateTime(dgwGRNItems.Rows[i].Cells["ExpiryDate"].Value));
                            theDRowItem["UserID"] = GblIQCare.AppUserId;
                            theDRowItem["POId"] = GblIQCare.PurchaseOrderID;
                            theDRowItem["DestinStoreID"] = Convert.ToInt32(ddlDestinationStore.SelectedValue);
                            theDRowItem["SourceStoreID"] = 0;
                            
                            dtGRNItem.Rows.Add(theDRowItem);
                        }


                    }

                }
                if (GblIQCare.ModePurchaseOrder == 2)
                {
                    for (int i = 0; i < dtGRNItem.Rows.Count; i++)
                    {
                        //if (string.IsNullOrEmpty(Convert.ToString(dtGRNItem.Rows[i]["GrnID"])))
                        //{
                       // var rows = dtGRNItem.Select("(AutoId =1000  and ISTItemID='" + ISTItemID + "' and RecievedQuantity <1)");
                        var rows = dtGRNItem.Select("( ISTItemID='" + ISTItemID + "' and RecievedQuantity <1)");
                        foreach (var row in rows)
                        {
                            row.Delete();
                            break;
                        }
                        // }
                    }
                }

            }

            isretValidate = true;
            return isretValidate;
        }
        private void BindGRNItemByclickPoItem()
        {
            BindGrnItemsGrid();
            if (dgwPOItems.CurrentRow != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dgwPOItems.CurrentRow.Cells["ItemID"].Value)))
                {
                    dgwPOItems.CurrentRow.Selected = true;
                    if (GblIQCare.ModePurchaseOrder == 2)
                    {
                        ISTItemID = Convert.ToString(dgwPOItems.CurrentRow.Cells["ISTItemID"].Value);
                        string[] strarr = dgwPOItems.CurrentRow.Cells["ISTItemID"].Value.ToString().Split('-');
                        _itemId = Convert.ToInt32(strarr[0]);
                    }
                    else
                    {
                        _itemId = Convert.ToInt32(dgwPOItems.CurrentRow.Cells["ItemID"].Value);
                    }
                    _orderQuantity = Convert.ToInt32(dgwPOItems.CurrentRow.Cells["OrderQuantity"].Value);
                    DataTable theDT = new DataTable();
                    DataView theDV = new DataView(dsPOItemsDetail.Tables[2]);
                    DataView theDVGridItem = new DataView(dtGRNItem);
                    if (GblIQCare.ModePurchaseOrder == 2)
                    {
                        theDV.RowFilter = "(ISTItemID ='" + ISTItemID + "' and  RecievedQuantity >0)";
                        theDVGridItem.RowFilter = "(ISTItemID ='" + ISTItemID+"' and  RecievedQuantity >0)";
                    }
                    else if (GblIQCare.ModePurchaseOrder == 1)
                    {
                        theDV.RowFilter = "(ItemID ='" + _itemId + "')";
                        theDVGridItem.RowFilter = "(ItemID ='" + _itemId + "')";

                        DataView dvBatch = dsPOItemsDetail.Tables[3].DefaultView;
                        dvBatch.RowFilter = "(ItemID ='" + _itemId + "')";

                        for (int i = 0; i < dvBatch.Count; i++)
                        {
                            BatchCollection.Add(dvBatch[i]["Name"].ToString());
                        }
                    }


                    if (theDV.ToTable().Rows.Count > theDVGridItem.ToTable().Rows.Count)
                    {
                        dgwGRNItems.DataSource = theDV.ToTable();
                        if (GblIQCare.ModePurchaseOrder == 2)
                        {
                            ISTpatriaReceived();
                        }
                        else
                        {
                            if (Convert.ToString(theDV[0]["BatchName"]) == "")
                            {
                                dgwGRNItems.AllowUserToAddRows = false;
                            }
                            else
                            {
                                dgwGRNItems.AllowUserToAddRows = true;
                            }
                        }
                    }
                    else
                    {
                        if (GblIQCare.ModePurchaseOrder == 2)
                        {
                                DataTable isttable = dtGRNItem.Copy();
                                DataView ISTview = new DataView(isttable);
                                ISTview.RowFilter = "(ISTItemID ='" + ISTItemID + "')";
                                dgwGRNItems.DataSource = ISTview.ToTable();
                                ISTpatriaReceived();
                        }
                        else
                        {
                            dgwGRNItems.DataSource = theDVGridItem.ToTable();
                            if (Convert.ToString(theDVGridItem[0]["BatchName"]) == "")
                            {
                                dgwGRNItems.AllowUserToAddRows = false;
                            }
                            else
                            {
                                if (CheckReceiveQuantity())
                                {
                                    dgwGRNItems.AllowUserToAddRows = true;
                                }
                                else
                                {
                                    dgwGRNItems.AllowUserToAddRows = false;
                                }
                               
                            }
                        }
                    }
                }
                else
                {

                    // dgwGRNItems.AllowUserToAddRows = true;
                }

            }
        }


        private void dgwGRNItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = dgwGRNItems.Rows[e.RowIndex];
            if (row != null)
            {
                if (string.IsNullOrEmpty(Convert.ToString(row.Cells["ItemID"].Value)))
                {
                    // row.Cells["ExpiryDate"].Value = "";
                }
                else
                {
                    //   row.Cells["ExpiryDate"].Value=
                }
                //else if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["ExpiryDate"].Value)))
                //{

                //}
            }
        }

        private void dgwGRNItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgwDataGrid = sender as DataGridView;
            // int ItemID = 0;
          //  decimal Margin = 0;
         //   decimal QtyPerPurchaseUnit = 0;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                
                if (dgwDataGrid.Columns[e.ColumnIndex].Name == "ItemPurchasePrice" && dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value != null)
                {
                    if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value) != "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["RecievedQuantity"].Value) != "")
                    {
                        
                        dgwDataGrid.Rows[e.RowIndex].Cells["TotPurchasePrice"].Value = Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["RecievedQuantity"].Value) * Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value);
                    }
                }
                if (dgwDataGrid.Columns[e.ColumnIndex].Name == "RecievedQuantity" && dgwDataGrid.Rows[e.RowIndex].Cells["RecievedQuantity"].Value != null)
                {
                    if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value) != "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["RecievedQuantity"].Value) != "")
                    {
                       
                        dgwDataGrid.Rows[e.RowIndex].Cells["TotPurchasePrice"].Value = Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["RecievedQuantity"].Value) * Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value);
                    }
                }


               

                if(e.ColumnIndex ==6)
                {
                  dgwGRNItems.AllowUserToAddRows = true;
                    DataTable theDTItems = (DataTable)dgwGRNItems.DataSource;
                    DataView theDV = new DataView(theDTItems);
                    if (_itemId == 0)
                    {
                        _itemId = Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex - 1].Cells["ItemID"].Value);
                    }
                    theDV.RowFilter = "ItemId=" + _itemId;
                    if (theDV.Count > 0)
                    {
                    if (!string.IsNullOrEmpty(Convert.ToString(theDV[0]["GRNId"])))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value)))
                        {
                            if (Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value) == 0)
                            {
                                int AutoId = Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex].Cells["AutoId"].Value);
                                if (AutoId > 0)
                                {
                                    // DataRow theDR = new DataRow(theDV);
                                    //dtGRNItem.Rows.Remove((DataRow)dgwDataGrid.Rows[e.RowIndex]);
                                    var rows = dtGRNItem.Select("AutoId = " + AutoId + "and ItemId=" + _itemId );
                                    foreach (var row in rows)
                                    {
                                        row.Delete();
                                        dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value = DBNull.Value;
                                    }

                                }
                            }
                        }
                        }
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    dgwGRNItems.AllowUserToAddRows = true;
                    DataTable theDTItems = (DataTable)dgwGRNItems.DataSource;
                    DataView theDV = new DataView(theDTItems);
                    if (_itemId == 0)
                    {
                        _itemId = Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex - 1].Cells["ItemID"].Value);
                    }
                    theDV.RowFilter = "ItemId=" + _itemId;
                    if (theDV.Count > 0)
                    {
                       
                        if (!string.IsNullOrEmpty(Convert.ToString(theDV[0]["GRNId"])))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value)))
                            {
                                if (Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value) == 0)
                                {
                                    int AutoId = Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex].Cells["AutoId"].Value);
                                    if (AutoId > 0)
                                    {
                                        // DataRow theDR = new DataRow(theDV);
                                        //dtGRNItem.Rows.Remove((DataRow)dgwDataGrid.Rows[e.RowIndex]);
                                        var rows = dtGRNItem.Select("AutoId = " + AutoId);
                                        foreach (var row in rows)
                                        {
                                            row.Delete();
                                            dgwDataGrid.Rows[e.RowIndex].Cells["GRNId"].Value = DBNull.Value;
                                        }

                                    }
                                }

                            }
                        }
                        if (GblIQCare.ModePurchaseOrder == 2)
                        {
                            DataTable theDTItemsbatch = (DataTable)dsPOItemsDetail.Tables[3];
                            DataView theDVItemsbatch = new DataView(theDTItemsbatch);
                            string bchname = Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["BatchName"].Value);
                            // string[] splitarry = bchname.Split('~');
                            theDVItemsbatch.RowFilter = "ItemId=" + _itemId + " and  Name='" + bchname + "'";
                            if (theDVItemsbatch.Count > 0)
                            {
                                dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value = theDVItemsbatch[0]["ExpiryDate"];
                            }
                            dgwDataGrid.Rows[e.RowIndex].Cells["Batchid"].Value = theDV[0]["Batchid"];
                            dgwDataGrid.Rows[e.RowIndex].Cells["ISTItemID"].Value = theDV[0]["ISTItemID"];
                            // dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value = theDV[0]["MasterPurchaseprice"];
                        }
                        else if (GblIQCare.ModePurchaseOrder == 1)
                        {
                            DataTable theDTItemsbatch = (DataTable)dsPOItemsDetail.Tables[3];
                            DataView theDVItemsbatch = new DataView(theDTItemsbatch);
                            string bchname = Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["BatchName"].Value);
                            // string[] splitarry = bchname.Split('~');
                            theDVItemsbatch.RowFilter = "ItemId=" + _itemId + " and  Name='" + bchname + "'";
                            if (theDVItemsbatch.Count > 0)
                            {
                                dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value = theDVItemsbatch[0]["ExpiryDate"];
                                dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].ReadOnly = true;
                            }
                            else
                            {
                                dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].ReadOnly = false; 
                            }
                            dgwDataGrid.Rows[e.RowIndex].Cells["Batchid"].Value = theDV[0]["Batchid"];
                        }

                        dgwDataGrid.Rows[e.RowIndex].Cells["ItemId"].Value = _itemId;
                        dgwDataGrid.Rows[e.RowIndex].Cells["Margin"].Value = theDV[0]["Margin"];
                        dgwDataGrid.Rows[e.RowIndex].Cells["QtyPerPurchaseUnit"].Value = theDV[0]["QtyPerPurchaseUnit"]; ;
                        dgwDataGrid.Rows[e.RowIndex].Cells["MasterPurchaseprice"].Value = theDV[0]["MasterPurchaseprice"];
                        //    dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value = theDV[0]["ItemPurchasePrice"];
                        dgwDataGrid.Rows[e.RowIndex].Cells["ItemPurchasePrice"].Value = theDV[0]["MasterPurchaseprice"];

                        // IsGrnItemFiled = false;
                        dgwGRNItems.AllowUserToAddRows = true;
                    }

                }
                if (e.ColumnIndex == 16)
                {
                    int theval = Convert.ToInt32(dgwDataGrid.Rows[e.RowIndex].Cells["InKindFlag"].Value);
                    //CheckBox chkInKindFlag = e. as CheckBox;
                    //if (chkInKindFlag.Checked)
                    //{
                    //    chkInKindFlag.Click += new EventHandler(chkInKindFlag_Click);

                    //}
                }

            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            FillMasterTabletoSave();
            if (FillGridItemToSave())
            {
                //IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objMasterlist = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                int ret = objMasterlist.SaveGoodreceivedNotes(dtGRNmaster, dtGRNItem, GblIQCare.ModePurchaseOrder);
                if (ret > -1)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    formInit();
                    BindGRNItemByclickPoItem();
                    //dtGRNmaster = CreateGRNMasterTable();
                    //dtGRNItem = CreateGRNItemTable();
                }
            }

        }
        private void FillMasterTabletoSave()
        {

            DataRow theDRow = dtGRNmaster.NewRow();
            theDRow["POID"] = GblIQCare.PurchaseOrderID;
            theDRow["GRNId"] = GrnId;
            theDRow["LocationID"] = GblIQCare.AppLocationId;
            theDRow["OrderDate"] = dtpOrderDate.Text;
            theDRow["DestinStoreID"] = Convert.ToInt32(ddlDestinationStore.SelectedValue);
            theDRow["SupplierID"] = ddlSupplier.SelectedValue;
            

            theDRow["UserID"] = GblIQCare.AppUserId;
            theDRow["OrderNo"] = txtOrderNumber.Text;
            theDRow["Freight"] = (Convert.ToString(txtFreight.Text) == "") ? 0 : Convert.ToDecimal(txtFreight.Text);
            theDRow["Tax"] = (Convert.ToString(txtTax.Text) == "") ? 0 : Convert.ToDecimal(txtTax.Text);
            dtGRNmaster.Rows.Add(theDRow);

        }

        private void dgwGRNItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int tempreceiveQuantity = 0;
            DataGridView dgwDataGrid = sender as DataGridView;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                if (e.ColumnIndex == 6)
                {
                   // DataTable theDTItems = (DataTable)dgwDataGrid.DataSource;
                    for (int i = 0; i < dgwDataGrid.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dgwDataGrid.Rows[i].Cells["RecievedQuantity"].Value)))
                        {
                            tempreceiveQuantity = tempreceiveQuantity + Convert.ToInt32(dgwDataGrid.Rows[i].Cells["RecievedQuantity"].Value.ToString());
                        }
                        if (tempreceiveQuantity > _orderQuantity)
                        {
                            IQCareWindowMsgBox.ShowWindow("GRNrecQtnGreaterOrderQtn", this);
                            dgwDataGrid.Rows[i].Cells["RecievedQuantity"].Value = 0;
                            dgwDataGrid.Rows[i].Cells["TotPurchasePrice"].Value = 0;
                          //  theDTItems.Rows[i]["TotPurchasePrice"] = 0;

                            return;
                        }
                    }
                }
            

                    if (e.ColumnIndex == 5)
                    {
                        if (dgwDataGrid.Columns[e.ColumnIndex].Name == "ExpiryDate" && dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value != null)
                        {
                            if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value) != "")
                            {
                                if (GblIQCare.ModePurchaseOrder == 1)
                                {
                                    DataTable theDTItemsbatch = (DataTable)dgwGRNItems.DataSource; ;
                                    DataView theDVItemsbatch = new DataView(theDTItemsbatch);
                                    string bchname = Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["BatchName"].Value);
                                    string expDate = Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                                    // string[] splitarry = bchname.Split('~');
                                    theDVItemsbatch.RowFilter = "ItemId=" + _itemId + " and batchName = '" + bchname + "' and  ExpiryDate <>'" + expDate + "'";
                                    if (theDVItemsbatch.Count > 0)
                                    {
                                        dgwDataGrid.Rows[e.RowIndex].Cells["ExpiryDate"].Value = DBNull.Value;
                                        IQCareWindowMsgBox.ShowWindow("ExpiryExistAss", this);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
      

            

        private void dgwPOItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //dgwPOItems.CurrentRow.Selected = true;
            DataGridViewRow row = dgwPOItems.Rows[e.RowIndex];
            if (row != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GrnID"].Value)))
                {
                    if (Convert.ToInt32(row.Cells["GrnID"].Value) > 0)
                    {
                        //  dgwPOItems.Rows[e.RowIndex].ReadOnly = true;  
                    }
                }
            }
        }

        private void dgwGRNItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dgwGRNItems.Rows[e.RowIndex];
            if (row != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["GrnID"].Value)))
                {
                    if (Convert.ToInt32(row.Cells["GrnID"].Value) > 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }
        
        private void ISTpatriaReceived()
        {
            if (dtGRNItem.Rows.Count > 0)
            {
                DataTable theDTItem = dtGRNItem.Copy();
                DataView theDVItem = new DataView(theDTItem);
                theDVItem.RowFilter = "(ISTItemID ='" + ISTItemID + "' and  RecievedQuantity =0 )";
              //  and AutoID=1000)";
                // int gridRowCount = dgwGRNItems.RowCount;
                if (theDVItem.Count > 0)
                {
                 //  dgwGRNItems.DataSource = theDVItem.ToTable() ;
                    return;
                }
                else
               {
                    DataTable thetempDTItem = dtGRNItem.Copy();
                    DataView thetempDVItem = new DataView(thetempDTItem);
                    thetempDVItem.RowFilter = "(ISTItemID ='" + ISTItemID + "' and Grnid=0)";
                    if (thetempDVItem.Count > 0)
                    {
                        return;
                    }
                    else
                    {
                        if (CheckReceiveQuantity())
                        {
                            AddDefaultrow();
                        }
                   }
                }

            }
            else
            {
                AddDefaultrow();
            }
        }
        private bool CheckReceiveQuantity()
        {
            bool ret = true;
            int tempreceiveQuantity = 0;
            DataTable theDTItems = (DataTable)dgwGRNItems.DataSource;
                for (int i = 0; i < theDTItems.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(theDTItems.Rows[i]["RecievedQuantity"])))
                    {
                        tempreceiveQuantity = tempreceiveQuantity + Convert.ToInt32(theDTItems.Rows[i]["RecievedQuantity"].ToString());
                    }
                    if (tempreceiveQuantity > _orderQuantity || (tempreceiveQuantity == _orderQuantity))
                    {

                       ret = false;
                        return ret ;
                    }
                }
                return ret;
        }
        private void AddDefaultrow()
        {
            DataTable thetempDT = dsPOItemsDetail.Tables[4].Copy();
            DataView thetempDVItem = new DataView(thetempDT);
            thetempDVItem.RowFilter = "(ISTItemID ='" + ISTItemID + "')";
            if (thetempDVItem.Count > 0)
            {
                DataRow theDRowItem = dtGRNItem.NewRow();
                theDRowItem["GrnID"] = DBNull.Value;
             //   theDRowItem["AutoID"] = 1000;
                theDRowItem["AutoID"] = 0;
                theDRowItem["ItemID"] = Convert.ToInt32(thetempDVItem[0]["ItemID"]);
                theDRowItem["BatchName"] = Convert.ToString(thetempDVItem[0]["BatchName"]);
                theDRowItem["RecievedQuantity"] = 0;
                theDRowItem["QtyPerPurchaseUnit"] = Convert.ToInt32(thetempDVItem[0]["QtyPerPurchaseUnit"]);
                if (!string.IsNullOrEmpty(Convert.ToString(thetempDVItem[0]["FreeRecievedQuantity"])))
                {
                    theDRowItem["FreeRecievedQuantity"] = Convert.ToInt32(thetempDVItem[0]["FreeRecievedQuantity"]);
                }
                else
                {
                    theDRowItem["FreeRecievedQuantity"] = 0;
                }
                theDRowItem["ItemPurchasePrice"] = Convert.ToDecimal(thetempDVItem[0]["MasterPurchaseprice"]); //Convert.ToDecimal(theDVItem[0]["ItemPurchasePrice"]);
                theDRowItem["Margin"] = Convert.ToDecimal(thetempDVItem[0]["Margin"]);
                theDRowItem["TotPurchasePrice"] = 0;
                theDRowItem["SellingPrice"] = Convert.ToDecimal(thetempDVItem[0]["SellingPrice"]);
                theDRowItem["SellingPricePerDispense"] = Convert.ToDecimal(thetempDVItem[0]["SellingPricePerDispense"]);

                theDRowItem["SellingPrice"] = 0;
                theDRowItem["SellingPricePerDispense"] = 0;

                theDRowItem["MasterPurchaseprice"] = Convert.ToDecimal(thetempDVItem[0]["MasterPurchaseprice"]);

                theDRowItem["ExpiryDate"] = string.Format("{0:M/d/yyyy}", Convert.ToDateTime(thetempDVItem[0]["ExpiryDate"]));
                theDRowItem["UserID"] = GblIQCare.AppUserId;
                theDRowItem["POId"] = GblIQCare.PurchaseOrderID;

                theDRowItem["DestinStoreID"] = Convert.ToInt32(ddlDestinationStore.SelectedValue);
                theDRowItem["SourceStoreID"] = 0;
                theDRowItem["InKindFlag"] = Convert.ToInt32(thetempDVItem[0]["InkindFlag"]);
                
                dtGRNItem.Rows.Add(theDRowItem);

                DataTable theDT = new DataTable();
                DataView theDV = new DataView(dtGRNItem);
                theDV.RowFilter = "(ISTItemID ='" + ISTItemID + "')";
                dgwGRNItems.DataSource = theDV.ToTable();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            ReportDocument objRptDoc = new ReportDocument();
            DataSet dsPrintPOItemsDetail = new DataSet();

            IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            dsPrintPOItemsDetail = objPOItem.GetPurchaseOrderDetailsByPoidGRN(GblIQCare.PurchaseOrderID);
            dsPrintPOItemsDetail.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\GooodsReceivedNote.xml");
            rptGRN rep = new rptGRN();
            rep.SetDataSource(dsPrintPOItemsDetail);
            //  rep.ParameterFields["FormDate","1"];
            rep.SetParameterValue("ModePurchaseOrder", GblIQCare.ModePurchaseOrder);
            rep.SetParameterValue("facilityname", GblIQCare.AppLocation);

            // , Convert.ToString(dtpFrom.Text)];


            frmReportViewer theRepViewer = new frmReportViewer();
            theRepViewer.MdiParent = this.MdiParent;
            theRepViewer.Location = new Point(0, 0);
            theRepViewer.crViewer.ReportSource = rep;
            theRepViewer.Show();
            this.Close();

        }

     
    }
}

        

    
    
    

