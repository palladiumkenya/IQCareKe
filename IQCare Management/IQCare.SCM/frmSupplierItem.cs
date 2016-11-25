using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;
using System.Collections;

namespace IQCare.SCM
{
    public partial class frmSupplierItem : Form
    {
        ArrayList arrItemList;
        //ArrayList arrItemDrugGeneric;
        DataTable DtItem;
       // DataTable drglbItemList;
        DataTable theItemListDT = new DataTable();
        DataTable theItemList = new DataTable();
        public frmSupplierItem()
        {
            InitializeComponent();
        }

        private void frmSupplierItem_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
           // if (GblIQCare.ItemFeatureId == 144)
           // {
                Init_Form();
                SetRights();
           // }
          //  else if (GblIQCare.ItemFeatureId == 149)
           // {
                //this.Text = "Store Item Linking";
                //lblSupplier.Text = "Store Name:";
                //lblItemType.Visible = true;
                //lblSubItem.Visible = true;
                //ddlItemType.Visible = true;
                //ddlItemSubType.Visible = true;
                //BindStoreDropdown();
                //BindItemListStore(0);
           // }
        }
        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.ItemFeatureId == 144)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.SupplierItem, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            else if (GblIQCare.ItemFeatureId == 149)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.StoreItem, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }

        }
        private void Init_Form()
        {
            if (GblIQCare.ItemFeatureId == 144)
            {
                BindSupplierDropdown(); 
            }
            else if (GblIQCare.ItemFeatureId == 149)
            {
                this.Text = "Store Item Linking";
                lblSupplier.Text = "Store Name:";
                lblItemType.Visible = true;
                lblSubItem.Visible = true;
                ddlItemType.Visible = true;
                ddlItemSubType.Visible = true;
                BindStoreDropdown();
              //  BindItemListStore(0);
            }

            BindItemTypeDropdown();
           
        }
        private void BindSupplierDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlSupplierName.Items.Clear();
            try
            {
                IMasterList objSupplierlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataSet dsSupplierlist = objSupplierlist.GetSupplierList();
                DataView theDV = new DataView(dsSupplierlist.Tables[0]);
                theDV.RowFilter = "Status = 0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(ddlSupplierName, theDT, "SupplierName", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void BindItemTypeDropdown()
        {
            try
            {
                
                IQCareUtils theUtils = new IQCareUtils();
                ddlItemType.Items.Clear();
                IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataTable theDT = objProgramlist.GetItemType();
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "DeleteFlag = 0";

                BindFunctions theBind = new BindFunctions();
                //theBind.Win_BindCombo(ddlItemType, theDT, "ItemType", "ID");
                theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "ItemType", "ID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
       
        }

        private void BindStoreDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlSupplierName.Items.Clear();
            try
            {
                IMasterList objSupplierlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataSet dsSupplierlist = objSupplierlist.GetSupplierList();
                DataView theDV = new DataView(dsSupplierlist.Tables[2]);
                theDV.RowFilter = "Status = 0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(ddlSupplierName, theDT, "StoreName", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }


        private void BindItemsubTypeDropdown(string ItemTypeId)
        {
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                ddlItemSubType.DataSource = null;
                IMasterList objSubitemType = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataTable theDT = objSubitemType.GetSubItemType();
                BindFunctions theBind = new BindFunctions();
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "ItemTypeId =" + ItemTypeId;
                theBind.Win_BindCombo(ddlItemSubType, theDV.ToTable(), "DrugTypeName", "drugTypeID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }
        public DataTable CreateItemTable()
        {
            DataTable dtItemList = new DataTable();
            dtItemList.Columns.Add("ItemID", typeof(int));
            return dtItemList;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           // if (GblIQCare.ItemFeatureId == 144)
           // {
                if (ddlSupplierName.SelectedValue.ToString() == "0")
                {
                    //IQCareWindowMsgBox.ShowWindow("SupplierName", this);
                    MsgBuilder theBuilder = new MsgBuilder();
                    if (GblIQCare.ItemFeatureId == 144)
                    {
                        theBuilder.DataElements["Control"] = "Supplier Name";
                    }
                    else if (GblIQCare.ItemFeatureId == 149)
                    {
                        theBuilder.DataElements["Control"] = "Store Name";
                    }
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    ddlSupplierName.Focus();
                    return;
                }
                if (ddlItemType.SelectedValue.ToString() == "0")
                {
                    //IQCareWindowMsgBox.ShowWindow("ItemTypeSelect", this);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Item type";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    ddlItemType.Focus();
                    return;
                }
           // }
            try
            {
                DtItem = CreateItemTable();
                arrItemList = new ArrayList();
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    if (chkItemList.GetItemChecked(i) == true)
                    {
                        DataRow theDR = DtItem.NewRow();
                        theDR["ItemID"] = Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                        DataView theDV = new DataView(theItemList);
                        theDV.RowFilter = "ItemID =" + theDR["ItemID"].ToString();
                        // arrItemList.Add((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                        DtItem.Rows.Add(theDR);

                    }
                }

                IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int ret=0;
                if (GblIQCare.ItemFeatureId == 144)
                {
                    ret = objMasterlist.SaveSupplierItemList(DtItem, Convert.ToInt32(ddlItemType.SelectedValue), GblIQCare.AppUserId, Convert.ToInt32(ddlSupplierName.SelectedValue));

                    ddlSupplierName.SelectedValue = 0;
                    ddlItemType.SelectedValue = 0;
                    ddlItemSubType.SelectedValue = 0;
                }
                else if (GblIQCare.ItemFeatureId == 149)
                {
                   // ret = objMasterlist.SaveStoreItemList(DtItem, GblIQCare.AppUserId, Convert.ToInt32(ddlSupplierName.SelectedValue));
                    ret = objMasterlist.SaveStoreItemList_Filtered(DtItem,  GblIQCare.AppUserId, Convert.ToInt32(ddlSupplierName.SelectedValue),Convert.ToInt32(ddlItemType.SelectedValue));
                    ddlSupplierName.SelectedValue = 0;

                    ddlItemType.SelectedValue = 0;
                    ddlItemSubType.SelectedValue = 0;
                }

               

               // BindItemListStore(0);

                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    chkItemList.DataSource = null;
                    chkItemList.Items.Clear();
                    return;
                }

            }

            


            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlItemType.SelectedIndex != 0)
            //{
            //    BindItemsubTypeDropdown(ddlItemType.SelectedValue.ToString());
            //    int Supplierid = 0;
            //    if (ddlSupplierName.SelectedIndex != 0)
            //    {
            //        Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
            //    }
            //    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, Supplierid);
            //}
            //else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            //{
            //    ddlItemSubType.DataSource = null;
            //    chkItemList.DataSource = null;
            //}
        }

        private void ddlItemSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int Supplierid = 0;
            //if (ddlItemSubType.SelectedIndex > 0)
            //{
            //    if (ddlSupplierName.SelectedIndex != 0)
            //    {
            //        Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
            //    }
            //    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue.ToString()), Supplierid);
            //}
            //else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            //{
            //    if (ddlSupplierName.SelectedIndex != 0)
            //    {
            //        Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
            //    }
            //    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, Supplierid);
            //}
        }
        private void BindItemList(int itemTypeId, int subitemId,int Supplierid)
        {
            //try
            //{
                chkItemList.DataSource = null;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataSet theDS = objItemlist.GetItemListSupplier(itemTypeId, subitemId, Supplierid);
                theItemListDT = theDS.Tables[0];
                theItemList = theDS.Tables[1];
                BindFunctions theBind = new BindFunctions();

                //DataView theDV = new DataView(theItemList);
                //theDV.RowFilter = "ItemTypeID =" + Convert.ToInt32(ddlItemType.SelectedValue.ToString());

                theBind.Win_BindCheckListBox(chkItemList, theItemList, "ItemName", "ItemID");
               // theBind.Win_BindCheckListBox(chkItemList, theDV.ToTable(), "ItemName", "ItemID");
                //for (int i = 0; i < drglbItemList.Rows.Count; i++)
                //{
                //    if (drglbItemList.Rows[i]["MappedItem"].ToString() == "True")
                //    {
                //        this.chkItemList.SetItemChecked(i, true);
                //    }
                //}
                for (int i = 0; i < theItemListDT.Rows.Count; i++)
                {
                    for (int j = 0; j < chkItemList.Items.Count; j++)
                    {
                        if (Convert.ToInt32(theItemListDT.Rows[i]["ItemTypeID"]) == itemTypeId && Convert.ToInt32(theItemListDT.Rows[i]["ItemID"]) == Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[j])).Row.ItemArray[0]).ToString()) && theItemListDT.Rows[i]["ItemName"].ToString() == chkItemList.GetItemText(chkItemList.Items[j]))
                        {
                            this.chkItemList.SetItemChecked(j, true);
                        }
                    }
                }
                bool IsAllcheck = true;
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    if (chkItemList.GetItemChecked(i) == false)
                    {
                        IsAllcheck = false;
                        break;
                    }

                }
                chkAll.Checked = IsAllcheck;

                if (chkItemList.Items.Count == 0)
                    chkAll.Checked = false;
            //}
            //catch (Exception err)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["MessageText"] = err.Message.ToString();
            //    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            //}
        }

        private void BindItemListStore_Filtered(int itemTypeId, int subitemId, int StoreId)
        {
            //try
            //{
            chkItemList.DataSource = null;
            IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataSet theDS = objItemlist.GetItemListStore_Filtered(itemTypeId, subitemId, StoreId);
            theItemListDT = theDS.Tables[0];
            theItemList = theDS.Tables[1];
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCheckListBox(chkItemList, theItemList, "ItemName", "ItemID");
            
            for (int i = 0; i < theItemListDT.Rows.Count; i++)
            {
                for (int j = 0; j < chkItemList.Items.Count; j++)
                {
                    if (Convert.ToInt32(theItemListDT.Rows[i]["ItemTypeID"]) == itemTypeId && Convert.ToInt32(theItemListDT.Rows[i]["ItemID"]) == Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[j])).Row.ItemArray[0]).ToString()) && theItemListDT.Rows[i]["ItemName"].ToString() == chkItemList.GetItemText(chkItemList.Items[j]))
                    {
                        this.chkItemList.SetItemChecked(j, true);
                    }
                }
            }
            bool IsAllcheck = true;
            for (int i = 0; i < chkItemList.Items.Count; i++)
            {
                if (chkItemList.GetItemChecked(i) == false)
                {
                    IsAllcheck = false;
                    break;
                }

            }
            chkAll.Checked = IsAllcheck;

            if (chkItemList.Items.Count == 0)
                chkAll.Checked = false;
           
        }

        private void BindItemListStore(int StoreId)
        {
            try
            {
                chkItemList.DataSource = null;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataSet theDS = objItemlist.GetItemListStore(StoreId);
                theItemListDT = theDS.Tables[0];
                theItemList = theDS.Tables[1];
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCheckListBox(chkItemList, theItemListDT, "ItemName", "ItemID");

                if (theItemList.Rows.Count > 0 && StoreId > 0)
                {
                    for (int j = 0; j < theItemList.Rows.Count; j++)
                    {
                        for (int i = 0; i < theItemListDT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(theItemListDT.Rows[i]["ItemID"]) == Convert.ToInt32(theItemList.Rows[j]["ItemID"]))
                            {
                                this.chkItemList.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                bool IsAllcheck = true;
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    if (chkItemList.GetItemChecked(i) == false)
                    {
                        IsAllcheck = false;
                        break;
                    }
                    
                }
                chkAll.Checked = IsAllcheck;
            }
 
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (GblIQCare.ItemFeatureId == 144)
            //{
            //    if (ddlSupplierName.SelectedIndex > -1)
            //    {
            //        ddlItemType.SelectedIndex = 0;
            //        chkItemList.DataSource = null;
            //        ddlItemSubType.DataSource = null;
            //        ddlItemSubType.Items.Clear();
            //    }

            //}
            //else if (GblIQCare.ItemFeatureId == 149)
            //{
            //    if (ddlSupplierName.SelectedIndex > 0)
            //    {
                    
            //        BindItemListStore(Convert.ToInt32(ddlSupplierName.SelectedValue));
            //        ddlItemSubType.DataSource = null;
            //        ddlItemSubType.Items.Clear();
            //    }
            //}
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerator myEnumerator = chkItemList.CheckedIndices.GetEnumerator();
            int y;
            while (myEnumerator.MoveNext() != false)
            {
                y = (int)myEnumerator.Current;
                chkItemList.SetItemChecked(y, false);
            }
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    this.chkItemList.SetItemChecked(i, true);
                }
            }
        }

        private void ddlItemType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlItemType.SelectedValue)> 0)
            {
                BindItemsubTypeDropdown(ddlItemType.SelectedValue.ToString());
                ////int Supplierid = 0;
                ////if (ddlSupplierName.SelectedIndex != 0)
                ////{
                ////    Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
                ////}
                ////BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, Supplierid);
            }
            else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            {
                ddlItemSubType.DataSource = null;
                chkItemList.DataSource = null;
            }
        }

        private void ddlItemSubType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //int Supplierid = 0;
            //if (ddlItemSubType.SelectedIndex > 0)
            //{
            //    if (ddlSupplierName.SelectedIndex != 0)
            //    {
            //        Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
            //    }
            //    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue.ToString()), Supplierid);
            //}
            //else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            //{
            //    if (ddlSupplierName.SelectedIndex != 0)
            //    {
            //        Supplierid = Convert.ToInt32(ddlSupplierName.SelectedValue.ToString());
            //    }
            //    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, Supplierid);
            //}
        }

        private void ddlSupplierName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ////if (GblIQCare.ItemFeatureId == 144)
            ////{
            ////    if (ddlSupplierName.SelectedIndex > -1)
            ////    {
            ////        ddlItemType.SelectedIndex = 0;
            ////        chkItemList.DataSource = null;
            ////        ddlItemSubType.DataSource = null;
            ////        ddlItemSubType.Items.Clear();
            ////    }

            ////}
            ////else if (GblIQCare.ItemFeatureId == 149)
            ////{
            ////    if (ddlSupplierName.SelectedIndex > 0)
            ////    {

            ////        BindItemListStore(Convert.ToInt32(ddlSupplierName.SelectedValue));
            ////        ddlItemSubType.DataSource = null;
            ////        ddlItemSubType.Items.Clear();
            ////    }
            ////}
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            if (GblIQCare.ItemFeatureId == 144)
            {
                if (Convert.ToInt32(ddlSupplierName.SelectedValue) > 0)
                {

                    if (Convert.ToInt32(ddlItemType.SelectedValue) > 0)
                    {
                       

                        BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue), Convert.ToInt32(ddlSupplierName.SelectedValue.ToString()));
                    }

                }
                else
                {
                    chkAll.Checked = false;
                    ddlItemType.SelectedIndex = 0;
                    chkItemList.DataSource = null;
                    ddlItemSubType.DataSource = null;
                    ddlItemSubType.Items.Clear();
                }
            }
            else if (GblIQCare.ItemFeatureId == 149)
            {
                if (Convert.ToInt32(ddlSupplierName.SelectedValue) > 0)
                {
                    if (Convert.ToInt32(ddlItemType.SelectedValue) > 0)
                    {

                        BindItemListStore_Filtered(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue), Convert.ToInt32(ddlSupplierName.SelectedValue.ToString()));
                        // BindItemListStore(Convert.ToInt32(ddlSupplierName.SelectedValue));
                        ////ddlItemSubType.DataSource = null;
                        ////ddlItemSubType.Items.Clear();
                    }
                }
                else
                {
                   
                   
                    chkAll.Checked = false;
                    ddlItemType.SelectedIndex = 0;
                    chkItemList.DataSource = null;
                    ddlItemSubType.DataSource = null;
                    ddlItemSubType.Items.Clear();
                }
            }
        }

       

      
    }
}
