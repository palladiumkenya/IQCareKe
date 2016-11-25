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
using System.Globalization;
using System.Collections;

namespace IQCare.SCM
{
    public partial class frmConfigureBudget : Form
    {
        DataSet dsDonorList = new DataSet();
        DataSet dsProgramListByDonor = new DataSet();
        DataSet dsProgramList = new DataSet();
        DataTable DTItemlist = new DataTable();
        private const int CONST_CODEID = 201;         //CHECK WITH SANJAY WHERE TO PUT THIS VALUE?

        public frmConfigureBudget()
        {
            InitializeComponent();
        }

        private void frmConfigureBudget_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(1, 1);
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            BindDropdown();
            SetRights();
        }

        private void SetRights()
        {
            if (!GblIQCare.HasFunctionRight(ApplicationAccess.BudgetConfiguration, FunctionAccess.Delete, GblIQCare.dtUserRight))
            {
                btnDelete.Visible = false;
                dgwBudgetConfig.ReadOnly = true;
            }
        }

        private void Init_Form()
        {
            IBudgetConfigDetail objDonorlist = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsDonorList = objDonorlist.GetDonorList();

            IBudgetConfigDetail objProgramlist = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsProgramList = objProgramlist.GetProgramList();

            LoadGrid(LoadGridData().Tables[0]);
        }

        private void BindDropdown()
        {

            string XmlFile = GblIQCare.GetXMLPath() + "Currency.xml";
            DataSet theDSCurrencyXML = new DataSet();
            theDSCurrencyXML.ReadXml(XmlFile);

            string CurrencyCode = theDSCurrencyXML.Tables[0].Select("Id = '" + GblIQCare.AppCountryId + "'").SingleOrDefault().ItemArray[1].ToString().Split('(').LastOrDefault().Replace(")", "");
            lblCurrency.Text = "Currency: " + CurrencyCode;

            BindFunctions objBindControls = new BindFunctions();
            DataTable theDT = new DataTable();
            theDT = objBindControls.GetYears(DateTime.Now.AddYears(1).Year, "Name", "Id");
            ddlProgramYear.Items.Clear();
            objBindControls.Win_BindCombo(ddlProgramYear, theDT, "Name", "Id");

            ddlDonorPayer.DataSource = null;
            ddlDonorPayer.Items.Clear();
            objBindControls.Win_BindCombo(ddlDonorPayer, dsDonorList.Tables[0], "DonorName", "Id");

            ddlProgram.DataSource = null;
            ddlProgram.Items.Clear();
            objBindControls.Win_BindCombo(ddlProgram, dsProgramList.Tables[0], "ProgramName", "Id");
        }

        private void LoadGrid(DataTable theDT)
        {
            try
            {
                dgwBudgetConfig.DataSource = null;
                dgwBudgetConfig.Columns.Clear();
                dgwBudgetConfig.AutoGenerateColumns = true;
                dgwBudgetConfig.DataSource = theDT;
                dgwBudgetConfig.Columns[0].Visible = false;
                
                foreach (DataGridViewTextBoxColumn dgc in dgwBudgetConfig.Columns)
                {
                    dgc.HeaderText = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(dgc.HeaderText.ToLower());
                    dgc.ReadOnly = true;
                    if (dgc.HeaderText.ToUpper() == "PROGRAMSTARTMONTH")
                    {
                        dgc.HeaderText = "Program Start Month";
                    }
                    else if (dgc.HeaderText.ToUpper() == "PROGRAMYEARTOTAL")
                    {
                        dgc.HeaderText = "Program Year Total";
                    }
                }
                DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
                col1.HeaderText = "Del";
                col1.Name = "DeleteFlag";
                col1.ReadOnly = false;
                dgwBudgetConfig.Columns.Add(col1);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private DataSet LoadGridData()
        {
            IBudgetConfigDetail objBudgetConfigList = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            DataSet dsBudgetConfigList = objBudgetConfigList.GetBudgetConfigTotal(CONST_CODEID);
            return dsBudgetConfigList;
        }

        private void ddlDonorPayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFunctions objBindControls = new BindFunctions();
            int donorVal = 0;
            if (ddlDonorPayer.SelectedValue != null) { if (ddlDonorPayer.SelectedIndex != 0) { donorVal = Convert.ToInt32(ddlDonorPayer.SelectedValue); } }
            IBudgetConfigDetail objProgramlistByDonor = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsProgramListByDonor = objProgramlistByDonor.GetProgramListByDonor(donorVal);

            ddlProgram.DataSource = null;
            ddlProgram.Items.Clear();
            objBindControls.Win_BindCombo(ddlProgram, dsProgramListByDonor.Tables[1], "ProgramName", "Id");
            if (ddlDonorPayer.SelectedIndex == 0)
            {
                ddlProgram.DataSource = null;
                ddlProgram.Items.Clear();
                objBindControls.Win_BindCombo(ddlProgram, dsProgramList.Tables[0], "ProgramName", "Id");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlDonorPayer.SelectedIndex == 0) { IQCareWindowMsgBox.ShowWindow("Please select a Donor.", "", "", this); return; }
            if (ddlProgram.SelectedIndex == 0) { IQCareWindowMsgBox.ShowWindow("Please select a Program.", "", "", this); return; }
            if (ddlProgramYear.SelectedIndex == 0) { IQCareWindowMsgBox.ShowWindow("Please select a Program Year.", "", "", this); return; }

            GblIQCare.objHashtbl = SelectedValue(0, Convert.ToInt32(ddlDonorPayer.SelectedValue), ddlDonorPayer.Text, Convert.ToInt32(ddlProgram.SelectedValue), ddlProgram.Text, Convert.ToInt32(ddlProgramYear.SelectedValue), ddlProgramYear.Text);

            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmConfigureBudgetDetail, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        private void dgwBudgetConfig_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int BudgetConfigureID;
            int DonorID;
            string DonorName;
            int ProgramID;
            string ProgramName;
            int ProgramYearId;
            string ProgramYear;

            BudgetConfigureID = Convert.ToInt32(dgwBudgetConfig.CurrentRow.Cells["Budgetconfigid"].Value);
            DonorID = Convert.ToInt32(dsDonorList.Tables[0].Select("DonorName = '" + dgwBudgetConfig.CurrentRow.Cells["Donor"].Value + "'").SingleOrDefault().ItemArray[0]);
            DonorName = dgwBudgetConfig.CurrentRow.Cells["Donor"].Value.ToString();
            ProgramID = Convert.ToInt32(dsProgramList.Tables[0].Select("ProgramName = '" + dgwBudgetConfig.CurrentRow.Cells["Program"].Value + "'").SingleOrDefault().ItemArray[0]);
            ProgramName = dgwBudgetConfig.CurrentRow.Cells["Program"].Value.ToString();
            ProgramYearId = Convert.ToInt32(dgwBudgetConfig.CurrentRow.Cells["Year"].Value);
            ProgramYear = dgwBudgetConfig.CurrentRow.Cells["Year"].Value.ToString();

            GblIQCare.objHashtbl = SelectedValue(BudgetConfigureID, DonorID, DonorName, ProgramID, ProgramName, ProgramYearId, ProgramYear);

            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmConfigureBudgetDetail, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Location = new System.Drawing.Point(1, 1);
            theForm.Show();
            this.Close();
        }

        public Hashtable SelectedValue(int BudgetConfigureID, int DonorID, string DonorName, int ProgramID, string ProgramName, int ProgramYearId, string ProgramYear)
        {
            Hashtable selectedValHash = new Hashtable();
            selectedValHash.Add("BudgetConfigureID", BudgetConfigureID);
            selectedValHash.Add("DonorID", DonorID);
            selectedValHash.Add("DonorName", DonorName);
            selectedValHash.Add("ProgramID", ProgramID);
            selectedValHash.Add("ProgramName", ProgramName);
            selectedValHash.Add("ProgramYearID", ProgramYearId);
            selectedValHash.Add("ProgramYear", ProgramYear);
            return selectedValHash;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult;
                dialogResult = IQCareWindowMsgBox.ShowWindow("Are you sure you would like to delete this budget?", "?", "", this);

                if (dialogResult == DialogResult.Yes)
                {

                    foreach (DataGridViewRow dgvr in dgwBudgetConfig.Rows)
                    {
                        if (dgvr.Cells["DeleteFlag"].Value != null)
                        {
                            IBudgetConfigDetail objItemlist = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
                            int retrows = objItemlist.DeleteBudgetConfigDetails(Convert.ToInt32(dgvr.Cells["BudgetConfigID"].Value), Convert.ToInt32(dgvr.Cells["DeleteFlag"].Value), GblIQCare.AppUserId);
                        }
                    }
                    IQCareWindowMsgBox.ShowWindow("Budget Configuration sucessfully Deleted.", "", "", this);
                    Init_Form();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
    }
}
