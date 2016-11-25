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

namespace IQCare.SCM
{
    public partial class frmCommonItemMaster : Form
    {
        DataTable DTItemlist = new DataTable();
        int PId, MaxPId,UpdateFlag=0;
        string ItemName = "";
        string Status = "";
        string UpdateName = "";
        string UpdateSRNO = "";

        public frmCommonItemMaster()
        {
            InitializeComponent();
        }
        private void BindDropdown()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add("Active");
            ddlStatus.Items.Add("InActive");
            ddlStatus.SelectedIndex = 0;

        }
        private void Init_Form()
        {
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            String TableName = "Mst_"+GblIQCare.ItemTableName;
            DTItemlist = objItemCommonlist.GetCommonItemList(GblIQCare.ItemCategoryId, TableName,GblIQCare.AppLocationId);
            if (DTItemlist.Rows.Count > 0)
            {
                if (TableName != "Mst_Drugtype")
                {
                    if (TableName == "Mst_Decode")
                    {
                        DataRow[] theDR = DTItemlist.Select("CodeId = " + GblIQCare.ItemCategoryId+"");
                        theDR = DTItemlist.Select("SRNo=MAX(SRNo)");
                        if (theDR.Length > 0)
                            MaxPId = Convert.ToInt32(theDR[0][3]);
                        else
                            MaxPId = 0;
                    }
                    else
                    {
                        DataRow[] theDR = DTItemlist.Select("SRNo=MAX(SRNo)");
                        if (theDR.Length > 0)
                            MaxPId = Convert.ToInt32(theDR[0][0]);
                        else
                            MaxPId = 0;
                        ///MaxPId = Convert.ToInt32(DTItemlist.Select("SRNo=MAX(SRNo)")[0].ItemArray[3]);
                    }
                }
            }
            ShowGrid(DTItemlist);
            Clear_Form();
        }
        private void Clear_Form()
        {
            txtItemName.Text = "";
            ddlStatus.SelectedIndex = 0;
            PId = 0;
            UpdateFlag = 0;
            UpdateName = "";
            UpdateSRNO = "";
            txtItemName.Focus();
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwItemList.Columns.Clear();
                dgwItemList.DataSource = null;
                Id.DataPropertyName = "Id";
                Id.Name = "Id";
               PriorityId.Name = PriorityId.DataPropertyName = "SRNO";
               ColName.Name= ColName.DataPropertyName = "Name";
               ColStatus.Name= ColStatus.DataPropertyName = "Status";
               ColUpdate.Name= ColUpdate.DataPropertyName = "updateflag";
              
                Id.Visible = false;
                ColUpdate.Visible = false;

                dgwItemList.Columns.Add(Id);
                dgwItemList.Columns.Add(PriorityId);
                dgwItemList.Columns.Add(ColName);
                dgwItemList.Columns.Add(ColStatus);
                dgwItemList.Columns.Add(ColUpdate);
                dgwItemList.AutoGenerateColumns = false;
               
                dgwItemList.DataSource = theDT.DefaultView.ToTable();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        public void SetRights()
        {
            //form level permission set

            //"Cost Allocation Category"\\deepika
                if (GblIQCare.ItemFeatureId ==134)
            {

                if (GblIQCare.HasFunctionRight(ApplicationAccess.CostAllocationCategory, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            // "Item Type"
            else if (GblIQCare.ItemFeatureId==135)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.ItemType, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }

            // "Lab Test Location"
            else if (GblIQCare.ItemFeatureId == 135)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.LabTestLocation, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            //"Manufacturer Detail"
            else if (GblIQCare.ItemFeatureId==137)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.ManufacturerDetail, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            // "Purchasing/Dispensing Unit"
             else if (GblIQCare.ItemFeatureId == 138)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.PurchasingDispensingUnit, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            // "Return Reason"
              else if (GblIQCare.ItemFeatureId == 139)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.ReturnReason, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            //"Adjustment Reason"
                else if (GblIQCare.ItemFeatureId == 140)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.AdjustmentReason, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
            //"Drug Type"
              else if (GblIQCare.ItemFeatureId == 142)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugType, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }
           // "Batch Number"
                else if (GblIQCare.ItemFeatureId == 150)
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugType, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
                {
                    btnSave.Enabled = false;
                }
            }

        }

        private Boolean Validation_Form()
        {
            try
            {
                if (txtItemName.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = GblIQCare.ItemLabel;
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    txtItemName.Focus();
                    return false;
                }
                else
                {
                    if (ItemName.ToUpper() != txtItemName.Text.ToUpper() && Status != ddlStatus.SelectedItem.ToString())
                    {
                        DataTable DTItemlistnew = (DataTable)dgwItemList.DataSource;
                        foreach (DataRow theDR in DTItemlistnew.Rows)
                        {

                            if (txtItemName.Text.ToUpper() == Convert.ToString(theDR["Name"]).ToUpper() && ddlStatus.SelectedItem.ToString() == Convert.ToString(theDR["Status"]))
                            {
                                if (GblIQCare.ItemTableName == "DispensingUnit" && PId==0)
                                {
                                    IQCareWindowMsgBox.ShowWindow("DuplicateItemName", this);
                                    txtItemName.Focus();
                                    return false;
                                }
                            }

                        }
                    }
                    else
                    {
                        DataTable DTItemlistnew = (DataTable)dgwItemList.DataSource;
                        foreach (DataRow theDR in DTItemlistnew.Rows)
                        {
                            string  id = theDR["ID"].ToString();
                            if (id == "")
                            {
                                if (txtItemName.Text.ToUpper() == Convert.ToString(theDR["Name"]).ToUpper())
                                {
                                    IQCareWindowMsgBox.ShowWindow("SaveItem", this);
                                    txtItemName.Focus();
                                    return false;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            return true;
        }

        private void frmCommonItemMaster_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                lblItemName.Text = GblIQCare.ItemLabel;
                lblItemName.Visible = false;
                lblItem.Text = GblIQCare.ItemLabel + ":";
                this.Text = GblIQCare.ItemLabel;
                BindDropdown();
                Init_Form();
                SetRights();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation_Form() != false)
                {
                    DTItemlist = (DataTable)dgwItemList.DataSource;
                    DataView theDV = new DataView(DTItemlist);
                    //theDV.RowFilter = "SRNo='" + PId + "'";
                    if (UpdateFlag == 1 && PId == 0)
                    {
                        DataRow[] rows = DTItemlist.Select("Name='" + UpdateName + "' and SRNO='" + UpdateSRNO + "'");
                        //theDV.RowFilter = "Name='" + UpdateName + "' and SRNO='" + UpdateSRNO + "'";
                        if (rows.Length > 0)
                        {
                            rows[0]["Name"] = txtItemName.Text;
                            rows[0]["Status"] = ddlStatus.SelectedItem;
                        }
                        
                    }
                    else 
                    {
                        theDV.RowFilter = "Id='" + PId + "'";
                        if (theDV.Count > 0)
                        {
                            theDV[0]["Name"] = txtItemName.Text;
                            theDV[0]["Status"] = ddlStatus.SelectedItem;
                        }
                        else
                        {
                            MaxPId = MaxPId + 1;
                            DataRow theDRow = DTItemlist.NewRow();
                            theDRow["Status"] = ddlStatus.SelectedItem;
                            theDRow["Name"] = txtItemName.Text;
                            theDRow["SRNo"] = MaxPId;
                            DTItemlist.Rows.Add(theDRow);
                        }
                    }
                    DTItemlist.AcceptChanges();
                    ShowGrid(DTItemlist);
                    Clear_Form();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    
                    if (dgwItemList.Rows[e.RowIndex].Cells[0].Value.ToString() == "")
                    {
                        UpdateFlag = 1;
                        UpdateSRNO = dgwItemList.Rows[e.RowIndex].Cells[1].Value.ToString();
                        UpdateName = dgwItemList.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    if (dgwItemList.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        PId = Convert.ToInt32(dgwItemList.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    if (dgwItemList.Rows[e.RowIndex].Cells[4].Value.ToString() == "0")
                    {
                        txtItemName.Text = dgwItemList.Rows[e.RowIndex].Cells[2].Value.ToString();
                        ddlStatus.SelectedItem = dgwItemList.Rows[e.RowIndex].Cells[3].Value.ToString();
                        //IQCareWindowMsgBox.ShowWindow("RowUpdate", this);
                        txtItemName.Enabled = false;
                        return;
                    }
                    else { txtItemName.Enabled = true; }
                    ItemName = dgwItemList.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Status = dgwItemList.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtItemName.Text = dgwItemList.Rows[e.RowIndex].Cells[2].Value.ToString();
                    ddlStatus.SelectedItem = dgwItemList.Rows[e.RowIndex].Cells[3].Value.ToString();
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Selected = true;
                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String TableName = "Mst_" + GblIQCare.ItemTableName;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int retrows = objItemlist.SaveUpdateItemList(DTItemlist, GblIQCare.ItemCategoryId, TableName, GblIQCare.AppUserId);
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

                 if (dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Index >= 1)
                 {
                    DTItemlist = (DataTable)dgwItemList.DataSource;
                    string Id = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                    string StrName = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrStatus = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[0].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[2].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex - 1].Cells[3].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[3].Value;
                    if (Id == "")
                    { Id = "0"; }
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[3].Value = StrStatus;
                    this.dgwItemList.CurrentCell = this.dgwItemList[2, dgwItemList.CurrentCell.RowIndex - 1];
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwItemList.DataSource;
                     
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message;
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();


            if (lblItemName.Text.ToLower() != "billables")
            {
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 2;
                theForm.Show();
            }
            this.Close();
       }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Index < dgwItemList.Rows.Count-1)
                {
                    string Id = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[0].Value.ToString() == "" ? "0" : dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    string StrName = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrStatus = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[0].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[2].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex + 1].Cells[3].Value = dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[3].Value;
                    
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Cells[3].Value = StrStatus;
                    this.dgwItemList.CurrentCell = this.dgwItemList[2, dgwItemList.CurrentCell.RowIndex + 1];
                    dgwItemList.Rows[dgwItemList.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwItemList.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtItemName_Leave(object sender, EventArgs e)
        {
            ActiveControl = ddlStatus;
        }

    }
}
