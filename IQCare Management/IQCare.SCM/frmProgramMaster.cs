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
    public partial class frmProgramMaster : Form
    {
        
        DataTable theDT = new DataTable();
        int PId,MaxPId;

        
        public frmProgramMaster()
        {
            InitializeComponent();
        }

        private void frmMasterList_Load(object sender, EventArgs e)
        {
           
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindDropdown();
            Init_Form();
            SetRights();
           
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }
        private void BindDropdown()
        {
            BindFunctions objBindControls = new BindFunctions();
            DataTable theDT = new DataTable();
           theDT = objBindControls.GetMonths();
           ddlFiscalyrmonth.Items.Clear();
           objBindControls.Win_BindCombo(ddlFiscalyrmonth, theDT, "Name", "Id");
           ddlStatus.Items.Clear();
           ddlStatus.Items.Add("Active");
           ddlStatus.Items.Add("In Active");
           ddlStatus.SelectedIndex = 0;
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            theDT = (DataTable)dgwProgramList.DataSource;
            if (Validation_Form() != false)
            {
                
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "Id='" + PId + "'";
                if (theDV.Count > 0)
                {
                    theDV[0]["ProgramId"] = txtProgramID.Text;
                    theDV[0]["ProgramName"] = txtProgramName.Text;
                    theDV[0]["Status"] = ddlStatus.SelectedIndex;
                    theDV[0]["FiscalYearMonth"] = ddlFiscalyrmonth.SelectedValue;
                    theDV[0]["MonthName"] = ddlFiscalyrmonth.Text;
                    theDV[0]["StatusName"] = ddlStatus.Text;
                }
                else
                {
                    MaxPId = MaxPId + 1;
                    DataRow theDRow = theDT.NewRow();
                    theDRow["Id"] = MaxPId;
                    theDRow["ProgramId"] = txtProgramID.Text;
                    theDRow["ProgramName"] = txtProgramName.Text; 
                    theDRow["Status"] = ddlStatus.SelectedIndex;
                    theDRow["FiscalYearMonth"] = ddlFiscalyrmonth.SelectedValue;
                    theDRow["MonthName"] = ddlFiscalyrmonth.Text;
                    theDRow["StatusName"] = ddlStatus.Text;
                    theDT.Rows.Add(theDRow);
                }
                theDT.AcceptChanges();
                ShowGrid(theDT);
                Clear_Form();
            }

        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwProgramList.Columns.Clear();
                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Id";
                col1.DataPropertyName = "Id";
                col1.Width = 50;
                col1.ReadOnly = true;
                col1.Visible = false;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Program ID";
                col2.DataPropertyName = "ProgramId";
                col2.Width = 150;
                col2.ReadOnly = true;
                col2.Visible = true;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Program Name";
                col3.DataPropertyName = "ProgramName";
                col3.Width = 360;
                col3.ReadOnly = true;
                col3.Visible = true;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Status";
                col4.DataPropertyName = "Status";
                col4.Width = 50;
                col4.ReadOnly = true;
                col4.Visible = false;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Status";
                col5.DataPropertyName = "StatusName";
                col5.Width = 70;
                col5.ReadOnly = true;
                col5.Visible = true;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Fiscal Year Month";
                col6.DataPropertyName = "FiscalYearMonth";
                col6.Width = 150;
                col6.ReadOnly = true;
                col6.Visible = false;

                dgwProgramList.DataSource = null;
                DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
                col7.HeaderText = "Fiscal Year Month";
                col7.DataPropertyName = "MonthName";
                col7.Width = 140;
                col7.ReadOnly = true;
                col7.Visible = true;


                dgwProgramList.Columns.Add(col1);
                dgwProgramList.Columns.Add(col2);
                dgwProgramList.Columns.Add(col3);
                dgwProgramList.Columns.Add(col4);
                dgwProgramList.Columns.Add(col5);
                dgwProgramList.Columns.Add(col6);
                dgwProgramList.Columns.Add(col7);


                dgwProgramList.AutoGenerateColumns = false;
                dgwProgramList.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void Init_Form()
        {
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataSet dsProgramlist = objProgramlist.GetProgramList();
            MaxPId = Convert.ToInt32(dsProgramlist.Tables[1].Rows[0][0]);
            ShowGrid(dsProgramlist.Tables[0]);
            Clear_Form();
            
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int retrows = objProgramlist.SaveProgramList(theDT, GblIQCare.AppUserId);
            IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
            Init_Form();

        }

        private void Clear_Form()
        {
            txtProgramID.Text = "";
            txtProgramName.Text = "";
            ddlStatus.SelectedIndex = 0;
            ddlFiscalyrmonth.SelectedValue = 0;
            PId = 0;
            txtProgramID.Focus(); 

        }

        private void dgwProgramList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgwProgramList.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    PId = Convert.ToInt32(dgwProgramList.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                txtProgramID.Text = dgwProgramList.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtProgramName.Text = dgwProgramList.Rows[e.RowIndex].Cells[2].Value.ToString();
                ddlStatus.SelectedIndex = Convert.ToInt32(dgwProgramList.Rows[e.RowIndex].Cells[3].Value.ToString());
                ddlFiscalyrmonth.SelectedValue = Convert.ToInt32(dgwProgramList.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
        }
        private Boolean Validation_Form()
        {

            if (txtProgramID.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "ProgramID";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtProgramID.Focus();
                return false;



            }
            if (txtProgramName.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Program Name";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtProgramName.Focus();
                return false;

            }
            else
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                    //if (txtProgramName.Text.ToUpper() == Convert.ToString(theDR["ProgramName"]).ToUpper())
                    if (txtProgramName.Text.ToUpper() == Convert.ToString(theDR["ProgramName"]).ToUpper() && txtProgramID.Text.ToUpper() == Convert.ToString(theDR["ProgramId"]).ToUpper() && ddlStatus.SelectedIndex.ToString() == theDR["Status"].ToString() && ddlFiscalyrmonth.SelectedIndex.ToString() == theDR["FiscalYearMonth"].ToString())//deepika
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Program Name";
                        IQCareWindowMsgBox.ShowWindow("DuplicateItemName", theBuilder, this);
                        txtProgramName.Focus();
                        return false;
                    }

                }
            }

            if (ddlFiscalyrmonth.SelectedValue.ToString() == "0")
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Fiscal yr month";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlFiscalyrmonth.Focus();
                return false;
            }

            return true;
        
        
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Index >= 1)
                {
                    //string Id = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                    string StrsupID = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[1].Value.ToString();
                    string StrName = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrStatusID = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    string StrStatus = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[4].Value.ToString();
                    string StrMonthID = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[5].Value.ToString();
                    string StrMonth = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[6].Value.ToString();
                    //dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[0].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[1].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[2].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[3].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[4].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[5].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[5].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex - 1].Cells[6].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[6].Value;
                    //dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[1].Value = StrsupID;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[3].Value = StrStatusID;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[5].Value = StrMonthID;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[6].Value = StrMonth;
                    this.dgwProgramList.CurrentCell = this.dgwProgramList[2, dgwProgramList.CurrentCell.RowIndex - 1];
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwProgramList.DataSource;
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
                if (dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Index < dgwProgramList.Rows.Count - 1)
                {
                    //string Id = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    string StrProId = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[1].Value.ToString();
                    string StrName = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrStatusID = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    string StrStatus = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[4].Value.ToString();
                    string StrMonthID = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[5].Value.ToString();
                    string StrMonth = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[6].Value.ToString();
                    //dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[0].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[0].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[1].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[2].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[3].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[4].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[5].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[5].Value;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex + 1].Cells[6].Value = dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[6].Value;
                    //dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[1].Value = StrProId;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[3].Value = StrStatusID;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[5].Value = StrMonthID;
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Cells[6].Value = StrMonth;
                    this.dgwProgramList.CurrentCell = this.dgwProgramList[2, dgwProgramList.CurrentCell.RowIndex + 1];
                    dgwProgramList.Rows[dgwProgramList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwProgramList.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }


        }

        private void txtProgramName_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Program, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }


    }
}
