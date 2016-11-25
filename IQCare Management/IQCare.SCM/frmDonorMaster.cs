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
    public partial class frmDonorMaster : Form
    {
        DataTable theDT = new DataTable();
        int PId, MaxPId;
        string strID, strName;
        string DonorId, DonorName;
        int intstatus;

        public frmDonorMaster()
        {
            InitializeComponent();
        }

        private void frmDonorMaster_Load(object sender, EventArgs e)
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
        private Boolean Validation_Form()
        {
            if (txtDonorID.Text == "")
            {
           
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Donor ID";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtDonorID.Focus();
                return false;

            }
            if (txtDonorName.Text == "")
            {
 
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Donor Name";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtDonorName.Focus();
                return false;
            }

            if (txtDonorShortName.Text == "")
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Donor Short Name";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                txtDonorShortName.Focus();
                return false;
            }
            DataTable theDT = (DataTable)dgwDonorList.DataSource;
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "Srno='" + PId + "'";
            if (theDV.Count == 0)
            {
                foreach (DataRow theDR in theDT.Rows)
                {
                        if (txtDonorID.Text.ToUpper() == Convert.ToString(theDR["DonorId"]).ToUpper())
                        {
                            IQCareWindowMsgBox.ShowWindow("DonorIdduplicate", this);
                            txtDonorID.Focus();
                            return false;
                        }

                        else if (txtDonorName.Text.ToUpper() == Convert.ToString(theDR["DonorName"]).ToUpper())
                        {
                            IQCareWindowMsgBox.ShowWindow("DonorNameduplicate", this);
                            txtDonorName.Focus();
                            return false;
                        }
                }
            }
            return true;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            IMasterList objDonorlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int retrows = objDonorlist.SaveDonorList(theDT, GblIQCare.AppUserId);
            IQCareWindowMsgBox.ShowWindow("DonorSave", this);
            Init_Form();

        }
        private void Clear_Form()
        {
            txtDonorID.Text = "";
            txtDonorName.Text = "";
            ddlStatus.SelectedIndex = 0;
            txtDonorShortName.Text = "";
            PId = 0;
            txtDonorID.Focus();

        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwDonorList.Columns.Clear();
                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Id";
                col1.DataPropertyName = "Id";
                col1.Width = 50;
                col1.ReadOnly = true;
                col1.Visible = false;

                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Donor ID";
                col2.DataPropertyName = "DonorId";
                col2.Width = 100;
                col2.ReadOnly = true;
                col2.Visible = true;

                DataGridViewTextBoxColumn colSrno = new DataGridViewTextBoxColumn();
                colSrno.HeaderText = "Srno";
                colSrno.DataPropertyName = "Srno";
                colSrno.Width = 100;
                colSrno.ReadOnly = true;
                colSrno.Visible = false;

                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Donor Name";
                col3.DataPropertyName = "DonorName";
                col3.Width = 270;
                col3.ReadOnly = true;
                col3.Visible = true;

                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Status";
                col4.DataPropertyName = "Status";
                col4.Width = 50;
                col4.ReadOnly = true;
                col4.Visible = false;

                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Status";
                col5.DataPropertyName = "StatusName";
                col5.Width = 80;
                col5.ReadOnly = true;
                col5.Visible = true;

                dgwDonorList.DataSource = null;
                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Donor Short Name";
                col6.DataPropertyName = "DonorShortName";
                col6.Width = 270;
                col6.ReadOnly = true;
                col6.Visible = true;


               
                dgwDonorList.Columns.Add(col1);
                dgwDonorList.Columns.Add(col2);
                dgwDonorList.Columns.Add(col3);
                dgwDonorList.Columns.Add(col4);
                dgwDonorList.Columns.Add(col5);
                dgwDonorList.Columns.Add(col6);
                dgwDonorList.Columns.Add(colSrno);

                dgwDonorList.AutoGenerateColumns = false;
                dgwDonorList.DataSource = theDT;
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
            IMasterList objDonorList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataSet dsDonorList = objDonorList.GetDonorList();
            MaxPId = Convert.ToInt32(dsDonorList.Tables[1].Rows[0][0]);
            ShowGrid(dsDonorList.Tables[0]);
            Clear_Form();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
                      
            if (Validation_Form() != false)
            {
                theDT = (DataTable)dgwDonorList.DataSource;
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "Srno='" + PId + "'";
                DataTable TmpDT = (DataTable)dgwDonorList.DataSource;
                DataView TmpDV = new DataView(TmpDT);
                theDV.RowFilter = "Srno='" + PId + "'";
                if (theDV.Count > 0)
                {
                    if (DonorId != txtDonorID.Text)
                    {
                        if (DonorName != txtDonorName.Text)
                        {
                            TmpDV.RowFilter = "DonorName='" + txtDonorName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("DonorNameduplicate", this);
                                txtDonorID.Focus();
                                return;
                            }
                        }

                        TmpDV.RowFilter = "DonorId='" + txtDonorID.Text + "'";
                        if (TmpDV.Count > 0)
                        {

                            IQCareWindowMsgBox.ShowWindow("DonorIdduplicate", this);
                            txtDonorID.Focus();
                            return;
                        }
                        theDV[0]["DonorId"] = txtDonorID.Text;
                        theDV[0]["DonorName"] = txtDonorName.Text;
                        theDV[0]["Status"] = ddlStatus.SelectedIndex;
                        theDV[0]["Donorshortname"] = txtDonorShortName.Text;
                        theDV[0]["StatusName"] = ddlStatus.Text;

                    }
                    else
                    {
                        if (DonorName != txtDonorName.Text)
                        {
                            TmpDV.RowFilter = "DonorName='" + txtDonorName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("DonorNameduplicate", this);
                                txtDonorID.Focus();
                                return;
                            }
                        }
                        theDV[0]["DonorId"] = txtDonorID.Text;
                        theDV[0]["DonorName"] = txtDonorName.Text;
                        theDV[0]["Status"] = ddlStatus.SelectedIndex;
                        theDV[0]["Donorshortname"] = txtDonorShortName.Text;
                        theDV[0]["StatusName"] = ddlStatus.Text;
                    }
                  
                }
                else
                {
                    MaxPId = MaxPId + 1;
                    DataRow theDRow = theDT.NewRow();
                    theDRow["Id"] = MaxPId;
                    theDRow["DonorId"] = txtDonorID.Text;
                    theDRow["DonorName"] = txtDonorName.Text; ;
                    theDRow["Status"] = ddlStatus.SelectedIndex;
                    theDRow["Donorshortname"] = txtDonorShortName.Text;
                    theDRow["StatusName"] = ddlStatus.Text;
                    theDRow["Srno"] = MaxPId;
                    theDT.Rows.Add(theDRow);
                }
                theDT.AcceptChanges();
                ShowGrid(theDT);
                Clear_Form();
            }



        }

        private void dgwDonorList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgwDonorList.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    //PId = Convert.ToInt32(dgwDonorList.Rows[e.RowIndex].Cells[0].Value.ToString());

                    if (dgwDonorList.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
                    {
                        PId = Convert.ToInt32(dgwDonorList.Rows[e.RowIndex].Cells[6].Value.ToString());
                    }
                }
                txtDonorID.Text = dgwDonorList.Rows[e.RowIndex].Cells[1].Value.ToString();
                DonorId = txtDonorID.Text;
                txtDonorName.Text = dgwDonorList.Rows[e.RowIndex].Cells[2].Value.ToString();
                DonorName = txtDonorName.Text;
                ddlStatus.SelectedIndex = Convert.ToInt32(dgwDonorList.Rows[e.RowIndex].Cells[3].Value.ToString());
                txtDonorShortName.Text = dgwDonorList.Rows[e.RowIndex].Cells[5].Value.ToString();

                strID=dgwDonorList.Rows[e.RowIndex].Cells[1].Value.ToString();
                strName = dgwDonorList.Rows[e.RowIndex].Cells[2].Value.ToString();
                intstatus = Convert.ToInt32(dgwDonorList.Rows[e.RowIndex].Cells[3].Value.ToString());
            }

        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DonorName, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Index >= 1)
                {
                    string Id = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                   
                    string StrsupID = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[1].Value.ToString();
                    string StrName = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrStatusID = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    string StrStatus = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[4].Value.ToString();
                   
                    string StrshortName = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[5].Value.ToString();
                   
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[0].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[0].Value;
                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[1].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[2].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[2].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[3].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[4].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[5].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[5].Value;
                    //dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex - 1].Cells[6].Value =
                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[0].Value = Id;
                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[1].Value = StrsupID;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[3].Value = StrStatusID;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[5].Value = StrshortName;
                    this.dgwDonorList.CurrentCell = this.dgwDonorList[2, dgwDonorList.CurrentCell.RowIndex - 1];
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwDonorList.DataSource;
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
                if (dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Index < dgwDonorList.Rows.Count - 1)
                {
                    string Id = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    
                    string StrSupId = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[1].Value.ToString();
                    string StrName = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrStatusID = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    string StrStatus = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[4].Value.ToString();
                    string StrshortName = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[5].Value.ToString();
                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[0].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[0].Value;
                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[1].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[1].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[2].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[2].Value;                    
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[3].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[3].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[4].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[4].Value;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex + 1].Cells[5].Value = dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[5].Value;

                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[0].Value = Id;
                   
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[1].Value = StrSupId;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[3].Value = StrStatusID;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Cells[5].Value = StrshortName;

                    this.dgwDonorList.CurrentCell = this.dgwDonorList[2, dgwDonorList.CurrentCell.RowIndex + 1];
                    dgwDonorList.Rows[dgwDonorList.CurrentCell.RowIndex].Selected = true;
                    theDT = (DataTable)dgwDonorList.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void txtDonorID_TextChanged(object sender, EventArgs e)
        {

        }

        //private bool Check_duplicatedata()
        //{
        //    DataTable theDT = (DataTable)dgwDonorList.DataSource;
        //    DataView theDV;
        //    theDV = new DataView(theDT);

        //    //theDV.RowFilter = "FieldId=" + fieldID.ToString() + " and Predefined=" + fieldStatus.ToString();

        //    theDV.RowFilter = "DonorId='" + txtDonorID.Text.ToString() + "'";
        //    if (theDV.Count > 0)
        //    {
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        IQCareWindowMsgBox.ShowWindow("DonorIdduplicate", theBuilder, this);
        //        txtDonorID.Focus();
        //        return false;

        //    }
        //    else
        //    {
        //        theDV = new DataView(theDT);
        //        theDV.RowFilter = "DonorName= '" + txtDonorName.Text.ToString() + "'";
        //        if (theDV.Count > 0)
        //        {
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            IQCareWindowMsgBox.ShowWindow("DonorNameduplicate", theBuilder, this);
        //            txtDonorName.Focus();
        //            return false;


        //        }
        //        else
        //        {
        //            theDV = new DataView(theDT);
        //            theDV.RowFilter = "DonorShortName='" + txtDonorShortName.Text.ToString() + "'";
        //            if (theDV.Count > 0)
        //            {
        //                MsgBuilder theBuilder = new MsgBuilder();
        //                IQCareWindowMsgBox.ShowWindow("DonorShortNameduplicate", theBuilder, this);
        //                txtDonorShortName.Focus();
        //                return false;
        //            }

        //        else
        //            {
        //                return true;

        //            }
        //        }
        //    }
        //}
                  
            
        //   public bool checkValidation(DataTable theDT)
        //{
        //    bool ret = true;
        //    for (int i = 0; i < theDT.Rows.Count; i++)
        //    {
              
        //        if (Convert.ToString(theDT.Rows[i]["DonorId"]).ToLower() == Convert.ToString(txtDonorID.Text).ToLower())
        //        {
                    
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            theBuilder.DataElements["Control"] = Convert.ToString(txtDonorID.Text);
        //            IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreID", theBuilder, this);
        //            ret = false;
        //            return ret;
        //        }
        //        if (Convert.ToString(theDT.Rows[i]["DonorName"]).ToLower() == Convert.ToString(txtDonorName.Text).ToLower())
        //        {
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            theBuilder.DataElements["Control"] = Convert.ToString(txtDonorName.Text);
        //            IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreName", theBuilder, this);
        //            ret = false;
        //            return ret;
        //        }
        //    }
        //    return ret;
        //}

       

    }
}
