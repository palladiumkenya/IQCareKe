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
using System.Runtime.InteropServices;


namespace IQCare.SCM
{
    public partial class frmDonorProgramLinking : Form
    {
        DataTable theDT = new DataTable();
        int DonorId, ProgramId,UpdateFlag,rowindex=-1;

        string FundStDate = "";
        string FundEndDate = "";

        DataSet dsPDonorLink = new DataSet();
        public frmDonorProgramLinking()
        {
            InitializeComponent();
            
        }
        //public void OnKeyPress(object sender, KeyEventArgs e)
        //{

        //    MessageBox.Show(e.KeyCode.ToString(), "Your input");

        //}
        private void frmDonorProgramLinking_Load(object sender, EventArgs e)
        {
            dtpStartDate.CustomFormat = "dd-MMM-yyyy";
            dtpEndDate.CustomFormat = "dd-MMM-yyyy";
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            SetRights();
            ActiveControl = ddlDonorName;

           

        }
        private void Init_Form()
        {
            IMasterList objpDonorlink = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
             dsPDonorLink = objpDonorlink.GetProgramDonorLnk();
             BindDropdown();
             ShowGrid(dsPDonorLink.Tables[2]);
             Clear_Form();
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwDonorProgramLinking.Columns.Clear();
                dgwDonorProgramLinking.DataSource = null;

                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "DonorId";
                col1.DataPropertyName = "DonorId";
                col1.Width = 50;
                col1.ReadOnly = true;
                col1.Visible = false;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Donor Name";
                col2.DataPropertyName = "DonorName";
                col2.Width = 250;
                col2.ReadOnly = true;
                col2.Visible = true;

                dgwDonorProgramLinking.DataSource = null;
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "ProgramId";
                col3.DataPropertyName = "ProgramId";
                col3.Width = 100;
                col3.ReadOnly = true;
                col3.Visible = false;

                dgwDonorProgramLinking.DataSource = null;
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Program Name";
                col4.DataPropertyName = "ProgramName";
                col4.Width = 250;
                col4.ReadOnly = true;
                col4.Visible = true;

                dgwDonorProgramLinking.DataSource = null;
                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Funding Start Date";
                col5.DataPropertyName = "FundingStartDate";
                //col5.CustomFormat = "dd-MMM-yyyy";
                col5.Width = 145;
                col5.ReadOnly = true;
                col5.Visible = true;

                dgwDonorProgramLinking.DataSource = null;
                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Funding End Date";
                col6.DataPropertyName = "FundingEndDate";
                //col6.CustomFormat = "dd-MMM-yyyy";
                col6.Width = 145;
                col6.ReadOnly = true;
                col6.Visible = true;
                

                dgwDonorProgramLinking.Columns.Add(col1);
                dgwDonorProgramLinking.Columns.Add(col2);
                dgwDonorProgramLinking.Columns.Add(col3);
                dgwDonorProgramLinking.Columns.Add(col4);
                dgwDonorProgramLinking.Columns.Add(col5);
                dgwDonorProgramLinking.Columns.Add(col6);

                dgwDonorProgramLinking.AutoGenerateColumns = false;
                dgwDonorProgramLinking.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Validation_Form() != false)
            {
                theDT = (DataTable)dgwDonorProgramLinking.DataSource;
                DataView theDV = new DataView(theDT);
                DataTable theRemDT = theDT.Copy();
                
                ////////////////////////
                DonorId = Convert.ToInt32(ddlDonorName.SelectedValue);
                ProgramId = Convert.ToInt32(ddlProgramName.SelectedValue);
                if (UpdateFlag == 1)
                {
                    DataRow[] Removerows = theRemDT.Select("DonorId='" + DonorId + "' and ProgramId='" + ProgramId + "'");
                    theRemDT.Rows.Remove(Removerows[0]);
                    //theDV.RowFilter = "DonorId <>'" + DonorId + "' and ProgramId <>'" + ProgramId + "' and FundingStartdate<>'" + FundStDate + "' and FundingEnddate<>'" + FundEndDate + "'";
                    //theDV.RowFilter = "DonorId <>'" + DonorId + "' and  ProgramId <>'" + ProgramId + "'";
                }
                else
                    theDV.RowFilter = "DonorId='" + DonorId + "' and ProgramId='" + ProgramId + "'";
                
                IQCareUtils util=new IQCareUtils();
                DataTable theProgDT = new DataTable();
                if (UpdateFlag == 1)
                    theProgDT = theRemDT;
                else
                    theProgDT= util.CreateTableFromDataView(theDV);
                DataView theProgDV = new DataView(theProgDT);
                theProgDV.RowFilter = "ProgramId='" + ProgramId + "'";
                DataTable theFiltDT = util.CreateTableFromDataView(theProgDV);
                if (theProgDV.Count > 0)
                {
                    DataRow[] rows = theFiltDT.Select(String.Format("(#{0}# >= FundingStartdate AND #{0}# <= FundingEnddate) OR (#{1}# >= FundingStartdate AND #{1}# <= FundingEnddate)", String.Format("{0:dd-MMM-yyyy}", dtpStartDate.Text), String.Format("{0:dd-MMM-yyyy}", dtpEndDate.Text)));
                    if (rows.Length > 0)
                    {
                        IQCareWindowMsgBox.ShowWindow("ExistFunding", this);
                        return;

                    }
                }
               
                    
                ///////////////////////////
                if (rowindex > -1 )
                {
                    DataRow[] rows = theDT.Select("DonorId='" + DonorId + "' and ProgramId='" + ProgramId + "' and FundingStartdate='" + FundStDate + "' and FundingEnddate='" + FundEndDate + "'");
                    //theDV.RowFilter = "Name='" + UpdateName + "' and SRNO='" + UpdateSRNO + "'";
                    if (rows.Length > 0)
                    {
                        rows[0]["FundingStartDate"] = String.Format("{0:dd-MMM-yyyy}", dtpStartDate.Text);
                        rows[0]["FundingEndDate"] = String.Format("{0:dd-MMM-yyyy}", dtpEndDate.Text);
                    }

                }
                else
                {
                    DataRow theDRow = theDT.NewRow();

                    theDRow["DonorId"] = ddlDonorName.SelectedValue;
                    theDRow["DonorName"] = ddlDonorName.Text;
                    theDRow["ProgramId"] = ddlProgramName.SelectedValue;
                    theDRow["ProgramName"] = ddlProgramName.Text;
                    theDRow["FundingStartDate"] = String.Format("{0:dd-MMM-yyyy}", dtpStartDate.Text);
                    theDRow["FundingEndDate"] = String.Format("{0:dd-MMM-yyyy}", dtpEndDate.Text);
                    theDT.Rows.Add(theDRow);
                }
                ShowGrid(theDT);
                Clear_Form();
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            IMasterList objpdonorLnk = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            int retrows = objpdonorLnk.SaveProgramDonorLnk(theDT, GblIQCare.AppUserId);
            IQCareWindowMsgBox.ShowWindow("DonorSave", this);
            Init_Form();

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();

        }

        private void Clear_Form()
        {
            ddlDonorName.SelectedValue=0;
            ddlProgramName.SelectedValue = 0;
            dtpStartDate.Text = "";
            dtpEndDate.Text = "";
            DonorId = 0;
            ProgramId = 0;
            UpdateFlag = 0;
            ddlDonorName.Focus();

        }

        private Boolean Validation_Form()
        {
            if (ddlDonorName.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Donor Name";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlDonorName.Focus();
                return false;


            }
            if (ddlProgramName.SelectedValue.ToString() == "0")
            {
           
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Program Name";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlProgramName.Focus();
                return false;

            }
            if (dtpStartDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Start Date";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                dtpStartDate.Focus();
                return false;

            }
            if (dtpEndDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "End Date";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                dtpEndDate.Focus();
                return false;

            }
            if (dtpEndDate.Text != "" && dtpStartDate.Text != "")
            {
                int Compdate = DateTime.Compare(DateTime.Parse(dtpStartDate.Text),DateTime.Parse(dtpEndDate.Text));
                if (Compdate>0)
                {
                    IQCareWindowMsgBox.ShowWindow("CompareStartEndDate", this);
                    return false;
                }

            }

            return true;
        }

        private void BindDropdown()
        {
            BindFunctions objBindControls = new BindFunctions();
            objBindControls.Win_BindCombo(ddlProgramName, dsPDonorLink.Tables[0], "ProgramName", "Id");
            objBindControls.Win_BindCombo(ddlDonorName, dsPDonorLink.Tables[1], "DonorName", "Id");
            
        }

        private void dgwDonorProgramLinking_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgwDonorProgramLinking.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    rowindex = e.RowIndex;
                    DonorId = Convert.ToInt32(dgwDonorProgramLinking.Rows[e.RowIndex].Cells[0].Value.ToString());
                    ProgramId = Convert.ToInt32(dgwDonorProgramLinking.Rows[e.RowIndex].Cells[2].Value.ToString());
                    UpdateFlag = 1;
                    FundStDate = dgwDonorProgramLinking.Rows[e.RowIndex].Cells[4].Value.ToString();
                    FundEndDate = dgwDonorProgramLinking.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                ddlDonorName.SelectedValue = Convert.ToInt32(dgwDonorProgramLinking.Rows[e.RowIndex].Cells[0].Value.ToString());
                ddlProgramName.SelectedValue = Convert.ToInt32(dgwDonorProgramLinking.Rows[e.RowIndex].Cells[2].Value.ToString());
                dtpStartDate.Text = dgwDonorProgramLinking.Rows[e.RowIndex].Cells[4].Value.ToString();
                dtpEndDate.Text = dgwDonorProgramLinking.Rows[e.RowIndex].Cells[5].Value.ToString();

            }

        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DonorProgramLinking, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btn_Save.Enabled = false;
            }
           
        }

        private void dgwDonorProgramLinking_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                if (this.dgwDonorProgramLinking.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow dgvrCurrent in dgwDonorProgramLinking.SelectedRows)
                    {
                        if (dgvrCurrent == dgwDonorProgramLinking.CurrentRow)
                        {
                            string FStartDate = dgvrCurrent.Cells[4].Value.ToString();
                            //DateTime FStartDate = Convert.ToDateTime(dgwDonorProgramLinking.CurrentRow.Cells[2].ToString());
                            string FEndDate = dgvrCurrent.Cells[5].Value.ToString();
                            if (MessageBox.Show("Do You want to Delete this Row?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                //DonorProgramLinking.CurrentCell = null;
                                
                                 theDT = (DataTable)dgwDonorProgramLinking.DataSource;
                                 DataView theDV = new DataView(theDT);

                                 //////////////////////

                                 theDV.RowFilter = "DonorId='" + DonorId + "' and ProgramId='" + ProgramId + "' and FundingStartdate='" + FStartDate.ToString() + "' and FundingEnddate='" + FEndDate.ToString() + "'";
                                 if (theDV.Count > 0)
                                 {
                                     while (theDV.Count > 0)
                                     {
                                         theDV.Delete(0);
                                     }
                                 }
                                 theDT.AcceptChanges();
                                 ShowGrid(theDT);
                                 Clear_Form();
                            }
                        }

                        // Delete Row Here
                    }

                    
                }
            }

        }

        private void ddlDonorName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
       
      

       

     
        
        
    }
}
