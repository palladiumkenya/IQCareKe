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
    public partial class frmProgramItemLinking : Form
    {
        ArrayList arrItemList;
       // ArrayList arrItemDrugGeneric;
        DataTable DtItem;
        //DataTable drglbItemList;
        DataTable theItemListDT = new DataTable();
        DataTable theItemList = new DataTable();
        public frmProgramItemLinking()
        {
            InitializeComponent();
        }

        public DataTable CreateItemtTable()
        {
            DataTable dtItemList= new DataTable();
            dtItemList.Columns.Add("ItemID", typeof(Int32));
            dtItemList.Columns.Add("Checked", typeof(Int32));  
          //  dtItemList.Columns.Add("DrugGeneric", typeof(string));
            return dtItemList;
        }
        private void frmProgramItemLinking_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            SetRights();
            this.chkPrg.Enabled = false;//deepika
        }
        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ProgramItem, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
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
        private void Init_Form()
        {
            BindItemTypeDropdown();
            BindProgramDropdown();
           
           // BindItemList();
        }
        private void BindProgramDropdown()
        {
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                ddlProgramName.Items.Clear();
                IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                DataSet dsProgramlist = objProgramlist.GetProgramList();
                DataView theDV = new DataView(dsProgramlist.Tables[0]);
                theDV.RowFilter = "Status = 0";
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(ddlProgramName, theDT, "ProgramName", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void BindItemList(int itemTypeId,int subitemId ,int Programid)
        {
            try
            {
                chkItemList.DataSource = null;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                // drglbItemList = objItemlist.GetItemList(itemTypeId, subitemId);
                DataSet theDS = objItemlist.GetItemList(itemTypeId, subitemId, Programid);
                theItemListDT = theDS.Tables[0];
                theItemList = theDS.Tables[1];

                //DataView theDV = new DataView(theItemList);
                //theDV.RowFilter = "ItemTypeID ="+ itemTypeId.ToString();

                if (theItemList.Rows.Count > 0)
                {
                    chkPrg.Enabled = true;
                }
                else 
                {
                    chkPrg.Enabled = false;
                }

                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCheckListBox(chkItemList, theItemList, "ItemName", "ItemID");
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
                chkPrg.Checked = IsAllcheck;

                if (chkItemList.Items.Count == 0)
                    chkPrg.Checked = false;
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
                theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "ItemType", "ID");
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

        private void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            ////////if (ddlItemType.SelectedIndex != 0)
            ////////{
            ////////    BindItemsubTypeDropdown(ddlItemType.SelectedValue.ToString());
            ////////    int programid = 0;
            ////////    if (ddlProgramName.SelectedIndex != 0)
            ////////    {
            ////////        programid =Convert.ToInt32( ddlProgramName.SelectedValue.ToString());
            ////////    }
            ////////    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, programid);
            ////////}
            ////////else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            ////////{
            ////////   ddlItemSubType.DataSource = null;
            ////////   chkItemList.DataSource = null;
            ////////}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlProgramName.SelectedValue.ToString() == "0")
            {
           //   IQCareWindowMsgBox.ShowWindow("ProgramType", this);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Program Name";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlProgramName.Focus();
                return;
            }
            if (ddlItemType.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Item Type";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlItemType.Focus();
                return;
            }
            try
            {
                DtItem = CreateItemtTable();

                arrItemList = new ArrayList();
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    DataRow theDR = DtItem.NewRow();
                    theDR["ItemID"] = Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                    if (chkItemList.GetItemChecked(i)==true)
                        theDR["Checked"] = 1;
                    else
                        theDR["Checked"] = 0;
                    DtItem.Rows.Add(theDR);

                        //DataView theDV = new DataView(theItemList);
                        //theDV.RowFilter = "ItemID =" + theDR["ItemID"].ToString();
                        //theDR["DrugGeneric"] = theDV.ToTable().Rows[0]["DrugGeneric"].ToString();
                        // arrItemList.Add((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                    

                }

                string ItemType = ddlItemType.SelectedValue.ToString();
                IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int ret = objMasterlist.SaveItemList(DtItem, Convert.ToInt32(ddlItemType.SelectedValue), GblIQCare.AppUserId, Convert.ToInt32(ddlProgramName.SelectedValue));
                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    
                    chkItemList.DataSource = null;
                    chkItemList.Items.Clear();
                    ddlProgramName.SelectedValue = 0;
                    ddlItemType.SelectedValue = 0;
                    ddlItemSubType.SelectedValue = 0;
                    //BindItemList(0, 0, 0);
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

        private void ddlItemSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////int programid = 0;
            ////if (ddlItemSubType.SelectedIndex > 0)
            ////{
               
            ////    if (ddlProgramName.SelectedIndex != 0)
            ////    {
            ////        programid = Convert.ToInt32(ddlProgramName.SelectedValue.ToString());
            ////    }
            ////    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue.ToString()), programid);
            ////}
            ////else if (Convert.ToString(ddlItemSubType.SelectedValue) =="0")
            ////{
            ////    if (ddlProgramName.SelectedIndex != 0)
            ////    {
            ////        programid = Convert.ToInt32(ddlProgramName.SelectedValue.ToString());
            ////    }
            ////    BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, programid);
            ////}
        }

        private void ddlProgramName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlProgramName.SelectedIndex  > -1)
            //{
            //    ddlItemType.SelectedIndex = 0;
            //    chkItemList.DataSource = null;
            //    ddlItemSubType.DataSource = null;
            //    ddlItemSubType.Items.Clear();
               
            //}
        }

        private void chkPrg_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerator myEnumerator = chkItemList.CheckedIndices.GetEnumerator();
            int y;
            while (myEnumerator.MoveNext() != false)
            {
                y = (int)myEnumerator.Current;
                chkItemList.SetItemChecked(y, false);
            }
            if (chkPrg.Checked == true)
            {
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    this.chkItemList.SetItemChecked(i, true);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            chkPrg.Checked = false;
                if (Convert.ToInt32(ddlProgramName.SelectedValue) > 0)
                {
                    if (ddlItemType.SelectedIndex != 0)
                    {
                        //int Programmid = 0;
                        //if (ddlProgramName.SelectedIndex != 0)
                        //{
                        //    Programmid = Convert.ToInt32(ddlProgramName.SelectedValue.ToString());
                        //}

                        BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), Convert.ToInt32(ddlItemSubType.SelectedValue), Convert.ToInt32(ddlProgramName.SelectedValue.ToString()));
                    }

                }
                else
                {
                    chkPrg.Checked = false;
                    ddlItemType.SelectedIndex = 0;
                    chkItemList.DataSource = null;
                    ddlItemSubType.DataSource = null;
                    ddlItemSubType.Items.Clear();
                }
        }

        private void ddlItemType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlItemType.SelectedValue) > 0)
            {
                BindItemsubTypeDropdown(ddlItemType.SelectedValue.ToString());
                //int programid = 0;
                //if (ddlProgramName.SelectedIndex != 0)
                //{
                //    programid = Convert.ToInt32(ddlProgramName.SelectedValue.ToString());
                //}
                //BindItemList(Convert.ToInt32(ddlItemType.SelectedValue.ToString()), 0, programid);
            }
            else if (Convert.ToString(ddlItemSubType.SelectedValue) == "0")
            {
                ddlItemSubType.DataSource = null;
                chkItemList.DataSource = null;
            }
        }
        
    }
}
