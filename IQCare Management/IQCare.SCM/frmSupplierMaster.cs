using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.SCM;
using Application.Common;
using Application.Presentation;
using System.Xml;

namespace IQCare.SCM
{
    public partial class frmSupplierMaster : Form
    {
        DataTable theDT = new DataTable();
        int PId, MaxPId;
        public frmSupplierMaster()
        {
            InitializeComponent();
        }

        private void frmSupplierMaster_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindDropdown();
            Init_Form();
            SetRights();

        }

        private void BindDropdown()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add("Active");
            ddlStatus.Items.Add("In Active");
            ddlStatus.SelectedIndex = 0;

        }
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwSupplierList.Columns.Clear();
                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Id";
                col1.DataPropertyName = "Id";
                col1.Width = 50;
                col1.ReadOnly = true;
                col1.Visible = false;

                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Supplier Id";
                col2.DataPropertyName = "SupplierId";
                col2.Width = 100;
                col2.ReadOnly = true;
                col2.Visible = true;

                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Supplier Name";
                col3.DataPropertyName = "SupplierName";
                col3.Width = 270;
                col3.ReadOnly = true;
                col3.Visible = true;

                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Status";
                col4.DataPropertyName = "Status";
                col4.Width = 50;
                col4.ReadOnly = true;
                col4.Visible = false;

                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Status";
                col5.DataPropertyName = "StatusName";
                col5.Width = 80;
                col5.ReadOnly = true;
                col5.Visible = true;

                dgwSupplierList.DataSource = null;
                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Address";
                col6.DataPropertyName = "Address";
                col6.Width = 270;
                col6.ReadOnly = true;
                col6.Visible = true;

                dgwSupplierList.Columns.Add(col1);
                dgwSupplierList.Columns.Add(col2);
                dgwSupplierList.Columns.Add(col3);
                dgwSupplierList.Columns.Add(col4);
                dgwSupplierList.Columns.Add(col5);
                dgwSupplierList.Columns.Add(col6);

                dgwSupplierList.AutoGenerateColumns = false;
                dgwSupplierList.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void Clear_Form()
        {
            txtSupplierID.Text = "";
            txtSupplierName.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtSupplierAddress.Text = "";
            PId = 0;
            txtSupplierID.Focus();
            

        }

        private void Init_Form()
        {
            IMasterList objSupplierList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataSet dsSupplierList = objSupplierList.GetSupplierList();
            MaxPId = Convert.ToInt32(dsSupplierList.Tables[1].Rows[0][0]);
            ShowGrid(dsSupplierList.Tables[0]);
            Clear_Form();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            //BindFunctions objBindControls = new BindFunctions();
            //if (objBindControls.Win_Numeric(txtSupplierID.Text) == false)
            //{

            //    IQCareWindowMsgBox.ShowWindow("SupplierID", this);
            //    txtSupplierID.Focus();
            //    return;
            
            //}

            //if (Check_duplicatedata() == false)
            //{
            //    return;

            //}

           
            
            theDT = (DataTable)dgwSupplierList.DataSource;
              if (Validation_Form() != false)
                {
                    //theDT = (DataTable)dgwSupplierList.DataSource;
                    DataView theDV = new DataView(theDT);
                    theDV.RowFilter = "Id='" + PId + "'";
                    if (theDV.Count > 0)
                    {
                        theDV[0]["SupplierId"] = txtSupplierID.Text;
                        theDV[0]["SupplierName"] = txtSupplierName.Text; ;
                        theDV[0]["Status"] = ddlStatus.SelectedIndex;
                        theDV[0]["Address"] = txtSupplierAddress.Text;
                        theDV[0]["StatusName"] = ddlStatus.Text;
                    }
                    else
                    {
                        MaxPId = MaxPId + 1;
                        DataRow theDRow = theDT.NewRow();
                        theDRow["Id"] = MaxPId;
                        theDRow["SupplierId"] = txtSupplierID.Text;
                        theDRow["SupplierName"] = txtSupplierName.Text; ;
                        theDRow["Status"] = ddlStatus.SelectedIndex;
                        theDRow["Address"] = txtSupplierAddress.Text;
                        theDRow["StatusName"] = ddlStatus.Text;
                        theDT.Rows.Add(theDRow);
                    }
                    theDT.AcceptChanges();
                    ShowGrid(theDT);
                    Clear_Form();
                }
            
        }

        private Boolean Validation_Form()
        {
            if (txtSupplierID.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Supplier ID";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtSupplierID.Focus();
                return false;



            }
            if (txtSupplierName.Text == "")
            {
                
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Supplier Name";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtSupplierName.Focus();
                return false;
            }
            else
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                   // if (txtSupplierName.Text.ToUpper() == Convert.ToString(theDR["SupplierName"]).ToUpper())
                    if (txtSupplierName.Text.ToUpper() == Convert.ToString(theDR["SupplierName"]).ToUpper() && txtSupplierID.Text.ToUpper() == Convert.ToString(theDR["SupplierId"]).ToUpper() && ddlStatus.SelectedIndex.ToString() == theDR["Status"].ToString() && txtSupplierAddress.Text.ToUpper() == Convert.ToString(theDR["Address"]).ToUpper())//deepika
                    
                    {
                        
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Supplier Name";
                        IQCareWindowMsgBox.ShowWindow("DuplicateStrength", theBuilder, this);
                        txtSupplierName.Focus();
                        return false;


                    }

                }
            }

            if (txtSupplierAddress.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Supplier Address";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtSupplierAddress.Focus();
                return false;

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IMasterList objSupplierlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int retrows = objSupplierlist.SaveSupplierList(theDT, GblIQCare.AppUserId);
            IQCareWindowMsgBox.ShowWindow("SupplierSave", this);
            Init_Form();
        }

        private void dgwSupplierList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgwSupplierList.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    PId = Convert.ToInt32(dgwSupplierList.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                txtSupplierID.Text = dgwSupplierList.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSupplierName.Text = dgwSupplierList.Rows[e.RowIndex].Cells[2].Value.ToString();
                ddlStatus.SelectedIndex = Convert.ToInt32(dgwSupplierList.Rows[e.RowIndex].Cells[3].Value.ToString());
                txtSupplierAddress.Text = dgwSupplierList.Rows[e.RowIndex].Cells[5].Value.ToString();
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
        private void btnUp_Click(object sender, EventArgs e)
        {

            try
            {

                if (dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Index >= 1)
                {
                    //string Id = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                    string StrsupID = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[1].Value.ToString();
                    string StrName = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrStatusID = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    string StrStatus = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[4].Value.ToString();
                    string StrAdd = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[5].Value.ToString();
                    //dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[0].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[1].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[2].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[3].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[4].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex - 1].Cells[5].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[5].Value;

                    //dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[1].Value = StrsupID;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[3].Value = StrStatusID;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[5].Value = StrAdd;
                    this.dgwSupplierList.CurrentCell = this.dgwSupplierList[2, dgwSupplierList.CurrentCell.RowIndex - 1];
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwSupplierList.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Index < dgwSupplierList.Rows.Count - 1)
                {
                   // string Id = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    string StrSupId = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[1].Value.ToString();
                    string StrName = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrStatusid = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    string StrStatus = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[4].Value.ToString();
                    string StrAdd = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[5].Value.ToString();
                    //dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[0].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[1].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[2].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[3].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[4].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex + 1].Cells[5].Value = dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[5].Value;
                    //dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[1].Value = StrSupId;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[3].Value = StrStatusid;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Cells[5].Value = StrAdd;
                    this.dgwSupplierList.CurrentCell = this.dgwSupplierList[2, dgwSupplierList.CurrentCell.RowIndex + 1];
                    dgwSupplierList.Rows[dgwSupplierList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwSupplierList.DataSource;
                }
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
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Supplier, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void txtSupplierID_KeyPress(object sender, KeyPressEventArgs e)
        {
           BindFunctions objBindControls = new BindFunctions();
           //objBindControls.Win_Numeric(e);
           //objBindControls.Win_decimal(e);
           objBindControls.Win_String(e);
          

        }

        private bool Check_duplicatedata()
        {
            DataTable theDT = (DataTable)dgwSupplierList.DataSource;
            DataView theDV;
            theDV = new DataView(theDT);

            theDV.RowFilter = "SupplierId='" + txtSupplierID.Text.ToString() + "'";
            if (theDV.Count > 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                IQCareWindowMsgBox.ShowWindow("DonorIdduplicate", theBuilder, this);
                txtSupplierID.Focus();
                return false;

            }
            else
            {
                theDV = new DataView(theDT);
                theDV.RowFilter = "SupplierName= '" + txtSupplierName.Text.ToString() + "'";
                if (theDV.Count > 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    IQCareWindowMsgBox.ShowWindow("DonorNameduplicate", theBuilder, this);
                    txtSupplierName.Focus();
                    return false;
                }
                

            }
            return true;

        }
    }
}