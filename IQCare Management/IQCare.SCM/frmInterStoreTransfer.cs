using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.SCM;


namespace IQCare.SCM
{
    public partial class frmInterStoreTransfer : Form
    {
        public int StoreID = 0;
        DataSet dsPOItems = new DataSet();
        DataSet dsPOItemsDetail = new DataSet();
        public bool deletedGridByKey = false;
        public Form PreviousForm;
        public ComboBox theGrdCombo;
     //   bool IsHandleAdded;
        DataTable dtOrdermaster;
        DataTable dtOrderItem;
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
        bool IsPOUpdated = false;

        public frmInterStoreTransfer()
        {
            InitializeComponent();
        }
        
        private void frmInterStoretransfer_Load(object sender, EventArgs e)
        {
            lblTotalAmount.Text = "";
            SetRights();
            dtpOrderDate.CustomFormat = "dd-MMM-yyyy";
            dtpOrderDate.Text = GblIQCare.CurrentDate;
            dtpOrderDate.Enabled = false;
            clsCssStyle theStyle = new clsCssStyle();
            chkRejectedStatus.Visible = false;
            theStyle.setStyle(this);
            dgwItemSubitemDetails.AllowUserToAddRows = false;
            BindStoreName();
            ddlDestinationStore.Enabled = false;
            lblSourceStore.Tag = "lblLabelRequired";
            theStyle.setStyle(lblSourceStore);
            if (GblIQCare.PurchaseOrderID != 0)
            {
                formInit();
            }
           
        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.InterStoreTransfer, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void dgwItemSubitemDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           
            //if (IsGridBind)
            //{
            //    if(dsPOItemsDetail !=null)
            //    {
            //    if (dsPOItemsDetail.Tables.Count > 1)
            //    {
            //        if (dsPOItemsDetail.Tables[1].Rows.Count > 0)
            //        {
            //            theGrdCombo.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[1].Rows[0]["ItemId"].ToString());
            //        }
            //    }
            //    }
            //}
        }
        private DataTable CreateOrderMasterTable()
        {
            
            DataTable dtOrdermaster = new DataTable();
            dtOrdermaster.Columns.Add("IsPO", typeof(int));
            dtOrdermaster.Columns.Add("POID", typeof(int));
            dtOrdermaster.Columns.Add("PONumber", typeof(string));
            dtOrdermaster.Columns.Add("OrderDate", typeof(DateTime));
            dtOrdermaster.Columns.Add("SupplierID", typeof(int));
            dtOrdermaster.Columns.Add("SrcStore", typeof(int));
            dtOrdermaster.Columns.Add("DestStore", typeof(int));
            dtOrdermaster.Columns.Add("UserID", typeof(int));
            dtOrdermaster.Columns.Add("PreparedBy", typeof(int));
            dtOrdermaster.Columns.Add("AthorizedBy", typeof(int));
            dtOrdermaster.Columns.Add("LocationID", typeof(int));
            dtOrdermaster.Columns.Add("IsRejectedStatus", typeof(int));
            return dtOrdermaster;
        }
        private DataTable CreateOrderItemTable()
        {
            DataTable dtOrderItem = new DataTable();
            dtOrderItem.Columns.Add("ItemID", typeof(int));
            dtOrderItem.Columns.Add("ItemName", typeof(String));
            dtOrderItem.Columns.Add("PurchaseUnit", typeof(int));
            dtOrderItem.Columns.Add("Quantity", typeof(int));
            dtOrderItem.Columns.Add("IssuedQuantity", typeof(int));
            dtOrderItem.Columns.Add("IssuedQuantityDU", typeof(int));
            dtOrderItem.Columns.Add("priceperunit", typeof(decimal));
            dtOrderItem.Columns.Add("totPrice", typeof(int));
            dtOrderItem.Columns.Add("BatchID", typeof(int));
            dtOrderItem.Columns.Add("AvaliableQty", typeof(int));
            dtOrderItem.Columns.Add("ExpiryDate", typeof(DateTime));
            dtOrderItem.Columns.Add("UnitQuantity", typeof(int));
            dtOrderItem.Columns.Add("IsPOorIST", typeof(int));
            //dtOrderItem.Columns.Add("Delete", typeof(String));
            // dtOrderItem.Columns.Add("IsFunded", typeof(int));
            return dtOrderItem;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
                    if (ddlSourceStore.SelectedValue.ToString() == "0")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Source Store";
                        IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                        ddlSourceStore.Focus();
                        return;
                    }

                    if (ddlDestinationStore.SelectedValue.ToString() == "0")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Destination Store";
                        IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                        ddlDestinationStore.Focus();
                        return;
                    }
                
                if (ddlPreparedBy.SelectedValue.ToString() == "0")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Prepared By";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    ddlPreparedBy.Focus();
                    return;
                }
                dtOrdermaster = CreateOrderMasterTable();
                DataRow theDRow = dtOrdermaster.NewRow();

                theDRow["OrderDate"] = dtpOrderDate.Text;
             
                theDRow["DestStore"] = Convert.ToInt32(ddlDestinationStore.SelectedValue);
                
                theDRow["SrcStore"] = Convert.ToInt32(ddlSourceStore.SelectedValue);
                  //  theDRow["DestStore"] = Convert.ToInt32(ddlDestinationStore.SelectedValue);
                
                theDRow["PreparedBy"] = Convert.ToInt32(ddlPreparedBy.SelectedValue);
                theDRow["AthorizedBy"] = Convert.ToInt32(ddlAuthorisedBy.SelectedValue);

                theDRow["UserID"] = Convert.ToInt32(GblIQCare.AppUserId);
                theDRow["LocationID"] = Convert.ToInt32(GblIQCare.AppLocationId);
                if (IsPOUpdated)
                {
                    theDRow["IsRejectedStatus"] = chkRejectedStatus.Checked ? 1 : 0;
                    theDRow["POID"] = GblIQCare.PurchaseOrderID;
                }
                dtOrdermaster.Rows.Add(theDRow);

                //DataTable theGrdTable = (DataTable)dgwItemSubitemDetails.DataSource;
                dtOrderItem = CreateOrderItemTable();

                for (int i = 0; i < dgwItemSubitemDetails.Rows.Count; i++)
                {
                   // if (Convert.ToInt32(dgwItemSubitemDetails.Rows[i].Cells[0].Value) > 0)
                    if (!String.IsNullOrEmpty(Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells[0].Value)))
                    {
                        if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["Units"].Value) == "")
                        {
                            IQCareWindowMsgBox.ShowWindow("PurchaseOrderUnitempty", this);
                            return;
                        }

                        if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["OrderQuantity"].Value) == "0")
                        {
                            IQCareWindowMsgBox.ShowWindow("PurchaseOrderQuantityZero", this);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["OrderQuantity"].Value)))
                        {
                            dgwItemSubitemDetails.Rows[i].Cells["OrderQuantity"].Value = 0;
                            IQCareWindowMsgBox.ShowWindow("PurchaseOrderQuantityZero", this);
                            return;
                        }
                    if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantity"].Value) == "0")
                    {
                        MessageBox.Show("Issued quantity should be greater than 0");
                        return;
                    }
                    if (String.IsNullOrEmpty(Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantity"].Value)))
                    {
                        dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantity"].Value = 0;
                        MessageBox.Show("Issued quantity should be greater than 0");
                        return;
                    }
                    if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantityDU"].Value) == "0")
                    {
                        MessageBox.Show("Issued quantity should be greater than 0");
                        return;
                    }
                    //if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["Price"].Value) == "0")
                    //{
                    //    IQCareWindowMsgBox.ShowWindow("PurchaseOrderPriceZero", this);
                    //    return;
                    //}
                    //if (Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells["Price"].Value) == "0")
                    //{
                    //    IQCareWindowMsgBox.ShowWindow("PurchaseOrderPriceZero", this);
                    //    return;
                    //}

                    DataRow theDRowItem = dtOrderItem.NewRow();
                        if(GblIQCare.ModePurchaseOrder == 2)
                        {
                            String strItemId = Convert.ToString(dgwItemSubitemDetails.Rows[i].Cells[0].Value);
                            string[] strarry = strItemId.Split('-');
                            theDRowItem["ItemID"] = Convert.ToInt32(strarry[0]);
                            theDRowItem["BatchID"] = Convert.ToInt32(strarry[1]);
                        // theDRowItem["ExpiryDate"] = Convert.ToDateTime(String.Format(MM-strarry[1]);//String.Format("{0:dd-MMM-yyyy}",
                        string dateString = strarry[2];
                       // DateTime expDate = new DateTime(Convert.ToInt32(dateString[2]), Convert.ToInt32(dateString[0]), Convert.ToInt32(dateString[1]));
                        theDRowItem["ExpiryDate"] = Utility.GetDateFromMMDDYYYY(dateString,'/');// expDate;
                           theDRowItem["AvaliableQty"] = Convert.ToInt32(dgwItemSubitemDetails.Rows[i].Cells["AvailableQTY"].Value); ;
                        }

                        theDRowItem["ItemName"] = dgwItemSubitemDetails.Rows[i].Cells[0].FormattedValue.ToString();
                        theDRowItem["Quantity"] = Convert.ToInt32(dgwItemSubitemDetails.Rows[i].Cells["OrderQuantity"].Value);
                    theDRowItem["IssuedQuantity"] = Convert.ToInt32(dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantity"].Value);
                    theDRowItem["IssuedQuantityDU"] = Convert.ToInt32(dgwItemSubitemDetails.Rows[i].Cells["IssuedQuantityDU"].Value);
                    theDRowItem["priceperunit"] = Convert.ToDecimal(dgwItemSubitemDetails.Rows[i].Cells["Price"].Value);
                        theDRowItem["UnitQuantity"] = Convert.ToDecimal(dgwItemSubitemDetails.Rows[i].Cells["UnitQuantity"].Value);
                    theDRowItem["IsPOorIST"] = 2;
                    
                        dtOrderItem.Rows.Add(theDRowItem);
                    }
                }

                if (dtOrderItem.Rows.Count <= 0)
                {
                    IQCareWindowMsgBox.ShowWindow("PurchaseOrderItem", this);
                    return;
                }
                //bool isDuplicate = false;
                for (int iDupitem = 0; iDupitem < dtOrderItem.Rows.Count; iDupitem++)
                {
                    for (int x = iDupitem + 1; x < dtOrderItem.Rows.Count; x++)
                    {
                        if (dtOrderItem.Rows[iDupitem]["ItemName"].ToString() == dtOrderItem.Rows[x]["ItemName"].ToString())
                        {
                            //pass message builder message
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = dtOrderItem.Rows[iDupitem]["ItemName"].ToString();
                            IQCareWindowMsgBox.ShowWindow("DuplicatePOItems", theBuilder, this);
                            
                            //isDuplicate = true;
                            //break;
                            return;
                        }
                    }
                }

                //IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                //int ret = objMasterlist.SavePurchaseOrder(dtOrdermaster, dtOrderItem, IsPOUpdated);

                IPurchase objMasterlist = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                int ret = objMasterlist.SavePurchaseOrder(dtOrdermaster, dtOrderItem, IsPOUpdated);

                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    if (GblIQCare.PurchaseOrderID == 0)
                    {
                        GblIQCare.PurchaseOrderID = ret;
                    }
                    formInit();
                    return;
                }
            
        }

       
       
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
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

            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDS = objItemCommonlist.GetStoreDetail();
            
            
            ddlSourceStore.Enabled = true;
            theDV = new DataView(theDS.Tables[2]);
            theDV.RowFilter = "Id <>" + StoreID;
            theStoreDT = theDV.ToTable();
                
            theBind.Win_BindCombo(ddlSourceStore, theStoreDT, "Name", "Id");
            
            theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null) and  ( Id =" + StoreID + ")";
            theStoreDT = theDV.ToTable();
            //theBind.Win_BindCombo(ddlDestinationStore, theStoreDT, "Name", "Id");
            ddlDestinationStore.DataSource = theStoreDT;
            ddlDestinationStore.DisplayMember = "Name";
            ddlDestinationStore.ValueMember = "Id";

        }
        private void OnChangedRdoPurchaseMode(object sender, EventArgs e)
        {
           
            lblTotalAmount.Text = "";
            
            ddlSourceStore.Enabled = true;
            ddlDestinationStore.Enabled = false;
            //  IsPurchaseOrder = 0;
            lblSourceStore.Tag = "lblLabelRequired";
             
            //clsCssStyle theStyle = new clsCssStyle();
            //theStyle.setStyle(this);
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(lblSourceStore);
            
            BindStoreName();

        }
        private void BindGrid(int PurchaseMode)
        {
            //IMasterList objPOItem = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
           if(PurchaseMode==1)
           {
               dsPOItems = objPOItem.GetPurcaseOrderItem(PurchaseMode, GblIQCare.AppUserId, 0);
           }
            else if(PurchaseMode ==2)
           {
                //if(ddlSourceStore.SelectedValue !="0")
                //{
                    dsPOItems = objPOItem.GetPurcaseOrderItem(PurchaseMode, GblIQCare.AppUserId, Convert.ToInt32(ddlSourceStore.SelectedValue));
                //}
           }

           

            BindGrid(dsPOItems);
            BindAuthorPreparedBy();
        }
        private void BindAuthorPreparedBy()
        {
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(ddlAuthorisedBy, dsPOItems.Tables[3], "EmpName", "EmployeeID");
            DataTable theDT = dsPOItems.Tables[3].Copy();

            theBind.Win_BindCombo(ddlPreparedBy, theDT, "EmpName", "EmployeeID");
        }
        private void BindGrid(DataSet theDS)
        {
            try
            {
                //dgwItemSubitemDetails.Columns.Clear();
                dgwItemSubitemDetails.AutoGenerateColumns = false;
                dgwItemSubitemDetails.AllowUserToAddRows = false;
               // dgwItemSubitemDetails.Columns["ItemName"]
                DataGridViewComboBoxColumn theColumnItemName = dgwItemSubitemDetails.Columns["ItemName"] as DataGridViewComboBoxColumn;//new DataGridViewComboBoxColumn();
                // ComboBox theColumnItemName = new ComboBox();
                theColumnItemName.HeaderText = "Item Name";
                theColumnItemName.Name = "ItemName";
                theColumnItemName.DataPropertyName = "ItemId";
                
                DataRow drItemSelect;
                drItemSelect = theDS.Tables[0].NewRow();
                drItemSelect["ItemId"] = 0;
                drItemSelect["ItemName"] = "Select";
                theDS.Tables[0].Rows.InsertAt(drItemSelect, 0);

                theColumnItemName.DataSource = theDS.Tables[0];
                
                theColumnItemName.DisplayMember = "ItemName";
                theColumnItemName.ValueMember = "ItemId";
               // theColumnItemName.Width = 350;
                theColumnItemName.ReadOnly = false;
                //theColumnItemName.AutoComplete = true;
                theColumnItemName.DefaultCellStyle.NullValue = "Select";
                

                DataGridViewTextBoxColumn theColumnItemCode = new DataGridViewTextBoxColumn();
                theColumnItemCode.HeaderText = "Item Code";
                theColumnItemCode.Name = "ItemCode";
                theColumnItemCode.DataPropertyName = "ItemCode";
                theColumnItemCode.ReadOnly = true;
                theColumnItemCode.Visible = false;

                DataGridViewTextBoxColumn theColumnUnit = new DataGridViewTextBoxColumn();
                theColumnUnit.HeaderText = "Purchase Units";
                theColumnUnit.Name = "Units";
                theColumnUnit.DataPropertyName = "Units";
                theColumnUnit.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnUnitQuantity = new DataGridViewTextBoxColumn();
                theColumnUnitQuantity.HeaderText = "Unit Qty";
                theColumnUnitQuantity.DataPropertyName = "UnitQuantity";
                theColumnUnitQuantity.Name = "UnitQuantity";
                theColumnUnitQuantity.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnQuantity = new DataGridViewTextBoxColumn();
                theColumnQuantity.HeaderText = "Ordered Qty";
                theColumnQuantity.DataPropertyName = "OrderQuantity";
                theColumnQuantity.Name = "OrderQuantity";
                theColumnQuantity.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnIssuedQuantity = new DataGridViewTextBoxColumn();
                theColumnIssuedQuantity.HeaderText = "Issued Qty - Purchase Unit";
                theColumnIssuedQuantity.DataPropertyName = "IssuedQuantity";
                theColumnIssuedQuantity.Name = "IssuedQuantity";
                theColumnIssuedQuantity.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnIssuedQuantityDU = new DataGridViewTextBoxColumn();
                theColumnIssuedQuantityDU.HeaderText = "Issued Qty - Disp Unit";
                theColumnIssuedQuantityDU.DataPropertyName = "IssuedQuantityDU";
                theColumnIssuedQuantityDU.Name = "IssuedQuantityDU";
                theColumnIssuedQuantityDU.ReadOnly = false;

                DataGridViewTextBoxColumn theColumnPrice = new DataGridViewTextBoxColumn();
                theColumnPrice.HeaderText = "Price /Unit";
                theColumnPrice.DataPropertyName = "Price";
                theColumnPrice.Name = "Price";
                theColumnPrice.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnTotPrice = new DataGridViewTextBoxColumn();
                theColumnTotPrice.HeaderText = "Total Price";
                theColumnTotPrice.DataPropertyName = "TotPrice";
                theColumnTotPrice.Name = "TotPrice";
                theColumnTotPrice.ReadOnly = true;

                ////DataGridViewButtonColumn theColumnDelete = new DataGridViewButtonColumn();
                ////theColumnDelete.DataPropertyName = "Delete";
                ////theColumnDelete.Name = "Delete";
                ////theColumnDelete.Text = "Delete";
                ////theColumnDelete.Width = 150;
               // theColumnDelete.ReadOnly = true;

                ////DataGridViewTextBoxColumn theColumnIsFunded = new DataGridViewTextBoxColumn();
                ////theColumnIsFunded.DataPropertyName = "IsFunded";
                ////theColumnIsFunded.Name = "IsFunded";
                ////theColumnIsFunded.Width = 10;
                ////theColumnIsFunded.Visible = false;


                dgwItemSubitemDetails.DataSource = dsPOItems.Tables[2];
               // dgwItemSubitemDetails.Columns.Add(theColumnItemName);
              //  dgwItemSubitemDetails.Columns.Add(theColumnItemCode);
              //  dgwItemSubitemDetails.Columns.Add(theColumnUnit);
              //  dgwItemSubitemDetails.Columns.Add(theColumnUnitQuantity);
              //  dgwItemSubitemDetails.Columns.Add(theColumnQuantity);
              //  dgwItemSubitemDetails.Columns.Add(theColumnPrice);
                if (GblIQCare.ModePurchaseOrder == 1)
                {
                   // theColumnItemCode.Width = 100;
                  //  theColumnUnit.Width = 90;
                   // theColumnQuantity.Width = 90;
                   // theColumnPrice.Width = 90;
                    //theColumnTotPrice.Width = 85;
                }
                else if (GblIQCare.ModePurchaseOrder ==2)
                {
                  //  theColumnItemCode.Width = 60;
                  //  theColumnUnit.Width = 60;
                  //  theColumnQuantity.Width = 60;
                  //  theColumnPrice.Width = 60;
                  //  theColumnTotPrice.Width = 60;
                  //  theColumnUnitQuantity.Width = 60;

                    DataGridViewTextBoxColumn theColumnExpiryDate = new DataGridViewTextBoxColumn();
                    theColumnExpiryDate.HeaderText = "Expiry Date";
                    theColumnExpiryDate.Name = "ExpiryDate";
                    theColumnExpiryDate.DataPropertyName = "ExpiryDate";
                   // theColumnExpiryDate.Width = 80;
                    theColumnExpiryDate.ReadOnly = true;

                    DataGridViewTextBoxColumn theColumnBatchName = new DataGridViewTextBoxColumn();
                    theColumnBatchName.HeaderText = "Batch No.";
                    theColumnBatchName.Name = "BatchName";
                    theColumnBatchName.DataPropertyName = "BatchName";
                 //   theColumnBatchName.Width = 100;
                    theColumnBatchName.ReadOnly = true;

                    DataGridViewTextBoxColumn theColumnBatchID = new DataGridViewTextBoxColumn();
                    theColumnBatchID.HeaderText = "BatchID";
                    theColumnBatchID.Name = "BatchID";
                    theColumnBatchID.DataPropertyName = "BatchID";
                   // theColumnBatchID.Width = 150;
                    theColumnBatchID.ReadOnly = true;
                    theColumnBatchID.Visible = false;

                    DataGridViewTextBoxColumn theColumnAvailableQTY = new DataGridViewTextBoxColumn();
                    theColumnAvailableQTY.HeaderText = "Available Qty";
                    theColumnAvailableQTY.Name = "AvailableQTY";
                    theColumnAvailableQTY.DataPropertyName = "AvailableQTY";
                   // theColumnAvailableQTY.Width = 60;
                    theColumnAvailableQTY.ReadOnly = true;

                   // dgwItemSubitemDetails.Columns.Add(theColumnExpiryDate);
                    //dgwItemSubitemDetails.Columns.Add(theColumnBatchName);
                   // //dgwItemSubitemDetails.Columns.Add(theColumnBatchID);
                  //  dgwItemSubitemDetails.Columns.Add(theColumnAvailableQTY);
                }
               // dgwItemSubitemDetails.Columns.Add(theColumnTotPrice);
               // dgwItemSubitemDetails.Columns.Add(theColumnDelete);
               // dgwItemSubitemDetails.Columns.Add(theColumnIsFunded);
                
            }
            catch(Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }
       

       

       // TextBox txtItem;
        private void dgwItemSubitemDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
            {
                
                theGrdCombo = (ComboBox)e.Control;
                theGrdCombo.SelectedValueChanged += null;
                deletedGridByKey = false;
                theGrdCombo.SelectedValueChanged += new EventHandler(theCombo_SelectedValueChanged);
               
                //theGrdCombo.DropDownStyle = ComboBoxStyle.DropDown;
                    //theGrdCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //theGrdCombo.AutoCompleteSource = AutoCompleteSource.ListItems;  
            }
            DataGridView dgwDataGridForEvent = sender as DataGridView;
            //!IsHandleAdded &&
            if ( dgwDataGridForEvent.CurrentCell.OwningColumn.Name == "OrderQuantity")//.ColumnIndex==3)
            {
                int f = dgwDataGridForEvent.CurrentCell.ColumnIndex;
                TextBox txtQuantity = e.Control as TextBox;
                if (txtQuantity != null)
                {
                    txtQuantity.KeyPress += new KeyPressEventHandler(txtQuantity_KeyPress);
                    //IsHandleAdded = true;
                }
            }
            if (dgwDataGridForEvent.CurrentCell.OwningColumn.Name == "IssuedQuantity")//.ColumnIndex==3)
            {
                int f = dgwDataGridForEvent.CurrentCell.ColumnIndex;
                TextBox txtIssuedQuantity = e.Control as TextBox;
                if (txtIssuedQuantity != null)
                {
                    txtIssuedQuantity.KeyPress += new KeyPressEventHandler(txtQuantity_KeyPress);
                    //IsHandleAdded = true;
                }
            }

            if (dgwDataGridForEvent.CurrentCell.OwningColumn.Name == "IssuedQuantityDU")//.ColumnIndex==3)
            {
                int f = dgwDataGridForEvent.CurrentCell.ColumnIndex;
                TextBox txtIssuedQuantityDU = e.Control as TextBox;
                if (txtIssuedQuantityDU != null)
                {
                    txtIssuedQuantityDU.KeyPress += new KeyPressEventHandler(txtQuantity_KeyPress);
                    //IsHandleAdded = true;
                }
            }
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewButtonColumn")
            {
             
            }
           
            
        }

        //void txtItem_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (txtItem.Text != "")
        //    {
        //        lstSearch.Visible = true;
        //        lstSearch.Width = txtItem.Width;
        //        lstSearch.Left = txtItem.Left;
        //        lstSearch.Top = txtItem.Top + txtItem.Height;
        //        lstSearch.Height = 300;
        //        DataView theDV = new DataView(dsPOItems.Tables[0]);
        //        theDV.RowFilter = "ItemName like '%" + txtItem.Text + "%'";
        //        if (theDV.Count > 0)
        //        {
        //            DataTable theDT = theDV.ToTable();
        //            BindFunctions theBindManager = new BindFunctions();
        //            theBindManager.Win_BindListBox(lstSearch, theDT, "ItemName", "ItemId");
        //        }
        //        else
        //        {
        //            lstSearch.DataSource = null;
        //        }

        //    }
        //    else
        //    {
        //        lstSearch.Visible = false;
        //    }
        //    if (e.KeyCode == Keys.Down)
        //        lstSearch.Select();

        //}
         void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
             BindFunctions theBind = new BindFunctions();
             theBind.Win_decimal(e);
        }

        void theCombo_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (theGrdCombo.Focused == true && theGrdCombo.SelectedValue != null)
                {

                    if (theGrdCombo.DataSource != null && theGrdCombo.SelectedValue.GetType().ToString() != "System.Data.DataRowView")
                    {
                        

                            string strdrug = theGrdCombo.SelectedValue.ToString();
                            ////DataSet dsSetItemdetail = dsPOItems.Copy();
                            ////DataView dvFilteredRow = new DataView();
                            ////dvFilteredRow = dsSetItemdetail.Tables[1].DefaultView;
                            string[] strArry = strdrug.Split('-');
                            DataTable theDT = new DataTable();
                            theDT = dsPOItems.Tables[1].Copy();
                            DataView dvFilteredRow = theDT.DefaultView;
                            DataTable dtRow = new DataTable();
                            dvFilteredRow.RowFilter = "Drug_Pk=" + strArry[0];

                            dtRow = dvFilteredRow.ToTable();
                            if (dtRow.Rows.Count > 0)
                            {
                                if (!deletedGridByKey)
                                {
                                dgwItemSubitemDetails.CurrentRow.Cells["ItemCode"].Value = dtRow.Rows[0]["DrugId"].ToString();
                                dgwItemSubitemDetails.CurrentRow.Cells["ItemCode"].ReadOnly = true;
                                dgwItemSubitemDetails.CurrentRow.Cells["Units"].Value = dtRow.Rows[0]["PurchaseUnitName"].ToString();
                                dgwItemSubitemDetails.CurrentRow.Cells["Units"].ReadOnly = true;
                                dgwItemSubitemDetails.CurrentRow.Cells["Price"].Value = Convert.ToDecimal(dtRow.Rows[0]["PurchaseUnitPrice"].ToString());
                                dgwItemSubitemDetails.CurrentRow.Cells["Price"].ReadOnly = true;
                                if (Convert.ToString(dtRow.Rows[0]["QtyPerPurchaseunit"]) != "")
                                {
                                    dgwItemSubitemDetails.CurrentRow.Cells["UnitQuantity"].Value = Convert.ToDecimal(dtRow.Rows[0]["QtyPerPurchaseunit"].ToString());
                                }
                                else { dgwItemSubitemDetails.CurrentRow.Cells["UnitQuantity"].Value = ""; }
                                dgwItemSubitemDetails.CurrentRow.Cells["UnitQuantity"].ReadOnly = true;
                                dgwItemSubitemDetails.CurrentRow.Cells["OrderQuantity"].Value = 0;
                                dgwItemSubitemDetails.CurrentRow.Cells["IssuedQuantity"].Value = 0;
                                dgwItemSubitemDetails.CurrentRow.Cells["IssuedQuantityDU"].Value = 0;
                                dgwItemSubitemDetails.CurrentRow.Cells["TotPrice"].ReadOnly = true;

                                //For Inter store Transfer 
                                if (GblIQCare.ModePurchaseOrder == 2)
                                {

                                    DataRowView theGrdCombodrow = (DataRowView)theGrdCombo.SelectedItem;
                                    string strdrugName = theGrdCombodrow.Row.ItemArray[1].ToString();

                                    DataTable theISTDT = dsPOItems.Tables[0].Copy();
                                    DataView dvISTFilteredRow = theISTDT.DefaultView;
                                    DataTable dtISTRow = new DataTable();
                                    dvISTFilteredRow.RowFilter = "ItemName ='" + strdrugName + "'";


                                    dtISTRow = dvISTFilteredRow.ToTable();
                                    dgwItemSubitemDetails.CurrentRow.Cells["AvailableQTY"].Value = dtISTRow.Rows[0]["AvailableQTY"].ToString();
                                    dgwItemSubitemDetails.CurrentRow.Cells["AvailableQTY"].ReadOnly = true;
                                    dgwItemSubitemDetails.CurrentRow.Cells["ExpiryDate"].Value = dtISTRow.Rows[0]["ExpiryDate"].ToString();
                                    dgwItemSubitemDetails.CurrentRow.Cells["ExpiryDate"].ReadOnly = true;
                                    dgwItemSubitemDetails.CurrentRow.Cells["BatchID"].Value = dtISTRow.Rows[0]["BatchId"].ToString();
                                    dgwItemSubitemDetails.CurrentRow.Cells["BatchID"].ReadOnly = true;
                                    dgwItemSubitemDetails.CurrentRow.Cells["BatchName"].Value = dtISTRow.Rows[0]["Batch"].ToString();
                                    dgwItemSubitemDetails.CurrentRow.Cells["BatchName"].ReadOnly = true;

                                    //dgwItemSubitemDetails.CurrentRow.Cells["OrderQuantity"].Value = 0;
                                    //dgwItemSubitemDetails.CurrentRow.Cells["TotPrice"].ReadOnly = true;
                                }


                                //dgwItemSubitemDetails.CurrentRow.Cells["Delete"].Value = "Delete";
                                //if (dtRow.Rows[0]["delete"].ToString() == "1")
                                //{

                                //    dgwItemSubitemDetails.CurrentRow.Cells["Delete"].ReadOnly = false;
                                //}
                                //else
                                //{
                                //    dgwItemSubitemDetails.CurrentRow.Cells["Delete"].ReadOnly = true;
                                //}

                                dgwItemSubitemDetails.AllowUserToAddRows = true;

                                ////DataView dvFundedRow = new DataView();
                                ////DataTable dtFundedRow = new DataTable();
                                ////dvFundedRow = dsSetItemdetail.Tables[4].DefaultView;
                                ////dvFundedRow.RowFilter = "Drug_Pk=" + theGrdCombo.SelectedValue.ToString();
                                ////dtFundedRow = dvFundedRow.ToTable();
                                ////if (dtFundedRow.Rows.Count > 0)
                                ////{
                                ////    dgwItemSubitemDetails.CurrentRow.Cells["IsFunded"].Value = dtFundedRow.Rows[0]["Isfunded"].ToString();
                                ////}
                                ////else
                                ////{
                                ////  dgwItemSubitemDetails.CurrentRow.Cells["IsFunded"].Value = 0;
                                ////}

                                ////if (dsSetItemdetail != null)
                                ////{
                                ////    dsSetItemdetail.Dispose();
                                ////    dsSetItemdetail = null;
                                ////}
                                dvFilteredRow = null;
                                dtRow = null;
                                theDT = null;
                                ////dvFundedRow = null;
                                ////dtFundedRow = null;
                                }
                            }
                            else
                            {

                                if (dgwItemSubitemDetails.SelectedRows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in dgwItemSubitemDetails.SelectedRows)
                                    {
                                        dgwItemSubitemDetails.Rows.Remove(row);
                                        //theGrdCombo.SelectedValueChanged += null;
                                        deletedGridByKey = true;
                                        
                                    }
                                }
                            }
                        
                    }
                }

            }
            //catch (System.StackOverflowException er)
            //{
            //    throw;
            //}
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }

            finally
            {
                

            }
        }

        private void dgwItemSubitemDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if ((theGrdCombo != null))
            {
               // theGrdCombo.SelectedIndex = theGrdCombo.FindStringExact(theGrdCombo.Text);
            }
        }

        private void dgwItemSubitemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    DataGridView dgwDataGrid = sender as DataGridView;
                    
                    if (dgwDataGrid.Columns[e.ColumnIndex].Name == "OrderQuantity" && dgwDataGrid.Rows[e.RowIndex].Cells["OrderQuantity"].Value != null && dgwDataGrid.Rows[e.RowIndex].Cells["Price"].Value != null)
                    {
                        if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["OrderQuantity"].Value) != "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["Price"].Value) != "")
                        {
                            dgwDataGrid.Rows[e.RowIndex].Cells["TotPrice"].Value = Convert.ToDecimal(Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["OrderQuantity"].Value.ToString()) * Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["Price"].Value.ToString()));
                        }
                    }
                    if (dgwDataGrid.Columns[e.ColumnIndex].Name == "TotPrice" && dgwDataGrid.Rows[e.RowIndex].Cells["TotPrice"].Value != null)
                    {                      
                        decimal SumPrice = 0;
                        foreach (DataGridViewRow theDR in dgwDataGrid.Rows)
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(theDR.Cells["TotPrice"].Value)))
                            {
                                SumPrice = SumPrice + Convert.ToDecimal(theDR.Cells["TotPrice"].Value);
                            }
                        }
                        lblTotalAmount.Text = Convert.ToString(SumPrice);
                    }

                    if (dgwDataGrid.Columns[e.ColumnIndex].Name == "IssuedQuantity" && dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value != null)
                    {
                        if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["OrderQuantity"].Value) != "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value) != "")
                        {
                            if (Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["OrderQuantity"].Value.ToString()) < Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value.ToString()))
                            {
                                MessageBox.Show("Quantity issued is greater than Quantity Ordered");
                                //dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value = 0;

                            }
                        }
                    }

                    if (dgwDataGrid.Columns[e.ColumnIndex].Name == "IssuedQuantity" && dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value != null)
                    {
                        if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["UnitQuantity"].Value) != "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value) != "")
                        {
                            if (Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["UnitQuantity"].Value) == "" && Convert.ToString(dgwDataGrid.Rows[e.RowIndex].Cells["UnitQuantity"].Value) == "0")
                            {
                                MessageBox.Show("Kindly Configure this item. Unit quantity for purchase unit is missing");
                                dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantityDU"].Value = 0;
                            }
                            else
                            {
                                dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantityDU"].Value = Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["UnitQuantity"].Value.ToString()) * Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value.ToString());
                            }


                            //if (Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["UnitQuantity"].Value.ToString()) < Convert.ToDecimal(dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value.ToString()))
                            //{
                            //    MessageBox.Show("Quantity issued cannot be greater than Quantity Ordered");
                            //    dgwDataGrid.Rows[e.RowIndex].Cells["IssuedQuantity"].Value = 0;

                            //}
                        }
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

        private void dgwItemSubitemDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message.Contains("constrained to be unique") == true)
            {
               // IQCareWindowMsgBox.ShowWindow("CheckDuplicateValue", this);
                return;
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = e.Exception.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }

        
        public void formInit()
        {
            try
            {
                //IMasterList objPOItem = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                dsPOItemsDetail = objPOItem.GetPurchaseOrderDetailsByPoid(GblIQCare.PurchaseOrderID);
                if (dsPOItemsDetail.Tables.Count > 0)
                {
                    if (dsPOItemsDetail.Tables[0].Rows.Count > 0)
                    {
                        //if (Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SupplierID"]) > 0 && Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SourceStoreID"]) == 0)
                        //{
                        //    // set IsPurchaseOrder = 1;
                        //    rdoPurchaseOrder.Checked = true;
                        //    rdoInterStoreTransfer.Enabled = false;
                        //}
                        //else if (Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SupplierID"]) == 0 && Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SourceStoreID"]) > 0)
                        //{
                        //    //set  IsPurchaseOrder = 0;
                        //    rdoInterStoreTransfer.Checked = true;
                        //    rdoPurchaseOrder.Enabled = false;
                        //}
                        dtpOrderDate.Text = dsPOItemsDetail.Tables[0].Rows[0]["OrderDate"].ToString();
                        dtpOrderDate.CustomFormat = "dd-MMM-yyyy";
                        dtpOrderDate.Enabled = false;
                        txtOrderNumber.Text = dsPOItemsDetail.Tables[0].Rows[0]["OrderNo"].ToString();
                        txtOrderNumber.Enabled = false;
                       // ddlSourceStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SourceStoreID"].ToString());

                        ddlDestinationStore.DataSource = null;
                        DataSet XMLDS = new DataSet();
                        XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                        BindFunctions theBindManager = new BindFunctions();
                        // Id <>" + StoreID + " or
                        DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                        //DataView theDV = new DataView(dsPOItemsDetail.Tables[3]);
                        theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null )and  (Id ="  + Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["DestinStoreID"].ToString()) + ") ";
                        DataTable theStoreDT = theDV.ToTable();
                        theBindManager.Win_BindCombo(ddlDestinationStore, theStoreDT, "Name", "Id");
                        // ddlDestinationStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["DestinStoreID"].ToString());
                        ddlSourceStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["SourceStoreID"].ToString());
                        ddlSourceStore.Enabled = false;
                        
                        ddlDestinationStore.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["DestinStoreID"].ToString());
                        ddlPreparedBy.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["PreparedBy"].ToString());
                        if (dsPOItemsDetail.Tables[0].Rows[0]["Status"].ToString() == "1")
                        {
                            chkRejectedStatus.Visible = true;
                            Btndelete.Enabled = true;
                            btnSave.Enabled = true;
                            IsPOUpdated = true;
                            btnPrint.Enabled = false;
                        }
                        else if (dsPOItemsDetail.Tables[0].Rows[0]["Status"].ToString() == "5")
                        {
                            btnSave.Enabled = false;
                            Btndelete.Enabled = false;
                            IsPOUpdated = false;
                            chkRejectedStatus.Visible = true;
                            chkRejectedStatus.Checked = true;
                            chkRejectedStatus.Enabled = false;
                            btnPrint.Enabled = true;
                        }
                        else
                        {
                            ddlAuthorisedBy.SelectedValue = Convert.ToInt32(dsPOItemsDetail.Tables[0].Rows[0]["AuthorizedBy"].ToString());
                            btnSave.Enabled = false;
                            Btndelete.Enabled = false;
                            chkRejectedStatus.Visible = false;
                            btnPrint.Enabled = true;

                        }
                        lblTotalAmount.Text = dsPOItemsDetail.Tables[1].Rows[0]["TotalAmount"].ToString();
                        BindGrid(dsPOItems);
                        dgwItemSubitemDetails.AllowUserToAddRows = true;
                        dgwItemSubitemDetails.DataSource = dsPOItemsDetail.Tables[1];

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

        private void dgwItemSubitemDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           //DataGridViewRow row = dgwItemSubitemDetails.Rows[e.RowIndex];
           // if (row != null)
           // {
              ////  if (!String.IsNullOrEmpty(Convert.ToString(row.Cells["IsFunded"].Value)))
            ////    {
            ////        int Status = Convert.ToInt32(row.Cells["IsFunded"].Value);
            ////        switch (Status)
            ////        {
            ////            case (1):
            ////                e.CellStyle.BackColor = Color.Cyan;
            ////                break;

            ////            default:
            ////                break;
            ////        }
            ////    }
               

           //}  
        }

        private void dgwItemSubitemDetails_KeyDown(object sender, KeyEventArgs e)
        {

           // Btndelete.PerformClick();
            //if (dgwItemSubitemDetails.SelectedRows.Count > 0 && e.KeyData == Keys.Delete)
            //{
            //    foreach (DataGridViewRow row in dgwItemSubitemDetails.SelectedRows)
            //    {
            //        dgwItemSubitemDetails.Rows.Remove(row);
            //    }
            //} 

        }

       

        private void Btndelete_Click(object sender, EventArgs e)
        {
             foreach (DataGridViewRow row in dgwItemSubitemDetails.SelectedRows)
                {
                
                 if (Convert.ToString(row.Cells[0].Value) != "")
                    {
                        dgwItemSubitemDetails.Rows.Remove(row);
                    }
                }
        }

        private void dgwItemSubitemDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GblIQCare.ModePurchaseOrder == 2)
                {
                    DataTable theDTItems = (DataTable)dgwItemSubitemDetails.DataSource;
                    if ((theDTItems.Rows.Count) > e.RowIndex)
                    {
                        if (e.ColumnIndex > -1 && e.RowIndex > -1)
                        {
                            
                            if (dgwItemSubitemDetails.Columns[e.ColumnIndex].Name == "OrderQuantity")
                            {

                                if (Convert.ToString(dgwItemSubitemDetails.Rows[e.RowIndex].Cells["OrderQuantity"].Value) == "0")
                                {
                                    IQCareWindowMsgBox.ShowWindow("PurchaseOrderQuantityZero", this);
                                    return;
                                }
                                if (!String.IsNullOrEmpty(Convert.ToString(theDTItems.Rows[e.RowIndex][3])))
                                {
                                    int OrderQuantity = Convert.ToInt32(theDTItems.Rows[e.RowIndex]["OrderQuantity"]);
                                    int AvaQTY = Convert.ToInt32(theDTItems.Rows[e.RowIndex]["AvailableQTY"]);
                                    if (AvaQTY < OrderQuantity)
                                    {
                                        IQCareWindowMsgBox.ShowWindow("ISTAvaQTYGreaterOrderQtn", this);
                                        theDTItems.Rows[e.RowIndex]["OrderQuantity"] = 0;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch //(System.Data.DeletedRowInaccessibleException er)
            {
                //throw;
            }

            finally
            {


            }
        }

       

        private void ddlSourceStore_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlSourceStore.SelectedIndex > 0)
            {
                BindGrid(GblIQCare.ModePurchaseOrder);

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument objRptDoc = new ReportDocument();
            DataSet dsPrintPOItemsDetail = new DataSet();
            //IMasterList objStockLedger = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objPOItem = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            dsPrintPOItemsDetail = objPOItem.GetPurchaseOrderDetailsByPoid(GblIQCare.PurchaseOrderID);
            dsPrintPOItemsDetail.WriteXmlSchema(GblIQCare.GetXMLPath() + "\\IntStorePO.xml");
            rptIntStorePOReport rep = new rptIntStorePOReport();
            rep.SetDataSource(dsPrintPOItemsDetail);
            //  rep.ParameterFields["FormDate","1"];
            rep.SetParameterValue("ModePurchaseOrder", GblIQCare.ModePurchaseOrder);
            rep.SetParameterValue("facilityname", GblIQCare.AppLocation);
            rep.SetParameterValue("CurrentUser", GblIQCare.AppUName);
            // , Convert.ToString(dtpFrom.Text)];

            frmReportViewer theRepViewer = new frmReportViewer();
            theRepViewer.MdiParent = this.MdiParent;
            theRepViewer.Location = new Point(0, 0);
            theRepViewer.crViewer.ReportSource = rep;
            theRepViewer.Show();
            this.Close();

        }

      

     

       

        //private void dgwItemSubitemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (dgwItemSubitemDetails.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex !=-1)
        //    //{
        //    //    if (dgwItemSubitemDetails.Rows.Count > 0)
        //    //        dgwItemSubitemDetails.Rows.RemoveAt(e.RowIndex);

        //    //        if (dgwItemSubitemDetails.Rows.Count == 0)
        //    //        {
        //    //            dgwItemSubitemDetails.AllowUserToAddRows = true;
        //    //        }
               
        //    //}
 
        //  }



        }

       

      
    
}
