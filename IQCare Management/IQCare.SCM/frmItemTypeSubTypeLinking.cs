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
    public partial class frmItemTypeSubTypeLinking : Form
    {  
      
       // int[] arrSubItemList;
        ArrayList arrSubItemList;
        //DataTable theDTdrugType;
        DataTable theDrugTypeDT = new DataTable();
        DataTable theDrugType = new DataTable();
        public frmItemTypeSubTypeLinking()
        {
            InitializeComponent();
        }

        private void frmItemType_SubTypeLinking_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            SetRights();
        }
        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ItemTypeSubTypeLinking, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));//deepika
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }
        private void Init_Form()
        {
            BindItemTypeDropdown();
            BindDrugTypeList();

        }
        private void BindItemTypeDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlItemType.Items.Clear();
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetItemType();
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "ItemType <> 'Lab Tests' and DeleteFlag = 0";
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(ddlItemType, theDV.ToTable(), "ItemType", "ID");
        }
        private void BindDrugTypeList()
        {
            IQCareUtils theUtils = new IQCareUtils();
            //chkSubItemTypeList.Items.Clear();
            IMasterList objDrugTypelist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int ItemTypeid = 0;
            if (ddlItemType.SelectedIndex > 0)
            {
                ItemTypeid = Convert.ToInt32(ddlItemType.SelectedValue.ToString());
            }
            DataSet theDS = objDrugTypelist.GetDrugType(ItemTypeid);
            theDrugTypeDT = theDS.Tables[0];
            theDrugType = theDS.Tables[1];
            ////////DataView theDV = new DataView(theDT);
            ////////theDV.RowFilter = "DeleteFlag = 0";
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCheckListBox(chkSubItemTypeList, theDrugType, "DrugTypeName", "DrugTypeID");

        }
        private void BindDrugTypeListByItemID(int itemID)
        {
            IMasterList objDrugTypelist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int ItemTypeid = 0;
            if (ddlItemType.SelectedIndex > 0)
            {
                ItemTypeid = Convert.ToInt32(ddlItemType.SelectedValue.ToString());
            }
            DataSet theDS = objDrugTypelist.GetDrugType(ItemTypeid);
            theDrugTypeDT = theDS.Tables[0];
            theDrugType = theDS.Tables[1];
            
            
            DataView theDV = new DataView(theDrugTypeDT);
            theDV.RowFilter = "MapTypeId=" + itemID.ToString();
            DataTable theDT = theDV.ToTable();
            chkSubItemTypeList.DataSource = null;
            chkSubItemTypeList.Items.Clear();
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCheckListBox(chkSubItemTypeList, theDrugType, "DrugTypeName", "DrugTypeID");
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                for (int j = 0; j < chkSubItemTypeList.Items.Count; j++)
                {
                    if (Convert.ToInt32(theDT.Rows[i]["MapTypeId"]) == itemID && theDT.Rows[i]["DrugTypeName"].ToString() == chkSubItemTypeList.GetItemText(chkSubItemTypeList.Items[j]))
                    {
                        this.chkSubItemTypeList.SetItemChecked(j, true);
                    }
                }
            }
            bool IsAllcheck = true;
            for (int i = 0; i < chkSubItemTypeList.Items.Count; i++)
            {
                if (chkSubItemTypeList.GetItemChecked(i) == false)
                {
                    IsAllcheck = false;
                    break;
                }

            }
            chktype.Checked = IsAllcheck;
        }

        private void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlItemType.SelectedIndex != 0)
            {
                BindDrugTypeListByItemID(Convert.ToInt32(ddlItemType.SelectedValue.ToString()));
            }
            else
            {
                BindDrugTypeList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlItemType.SelectedValue.ToString() == "0")
            {
               // IQCareWindowMsgBox.ShowWindow("ItemTypeSelect", this);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Item type";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlItemType.Focus();
                return ;
            }
            arrSubItemList = new ArrayList();
            for (int i = 0; i < chkSubItemTypeList.Items.Count; i++)
            {
                if (chkSubItemTypeList.GetItemChecked(i) == true)
                {
                    arrSubItemList.Add((((System.Data.DataRowView)(chkSubItemTypeList.Items[i])).Row.ItemArray[0]).ToString());
                }
            }
            string ItemType = ddlItemType.SelectedValue.ToString();
            IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int ret=  objMasterlist.SaveSubItemList(arrSubItemList, Convert.ToInt32(ddlItemType.SelectedValue), GblIQCare.AppUserId);
            theDrugTypeDT = ((DataSet)objMasterlist.GetDrugType(Convert.ToInt32(ItemType))).Tables[0];
            if (ret > 0)
            {
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                return;
            }
        }

        private void chktype_CheckedChanged(object sender, EventArgs e)
        {

            IEnumerator myEnumerator = chkSubItemTypeList.CheckedIndices.GetEnumerator();
            int y;
            while (myEnumerator.MoveNext() != false)
            {
                y = (int)myEnumerator.Current;
                chkSubItemTypeList.SetItemChecked(y, false);
            }
            if (chktype.Checked == true)
            {
                for (int i = 0; i < chkSubItemTypeList.Items.Count; i++)
                {
                    this.chkSubItemTypeList.SetItemChecked(i, true);
                }
            }
        }

       

    }
}
