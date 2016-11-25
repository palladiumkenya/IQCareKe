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
    public partial class frmConfigureBudgetDetail : Form
    {
        DataSet dsDonorList = new DataSet();
        DataSet dsBudgetConfigList = new DataSet();
        DataSet dsProgramListByDonor = new DataSet();
        DataSet dsProgramList = new DataSet();
        DataTable DTItemlist = new DataTable();
        Hashtable selectedValHash = new Hashtable();
        DateTime FundEndDt;
        DateTime ProgramStartDt; 
        private const int CONST_CODEID = 201;         //CHECK WITH SANJAY WHERE TO PUT THIS VALUE?

        public frmConfigureBudgetDetail()
        {
            InitializeComponent();
        }

        private void frmConfigureBudget_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(1, 1);
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            SetRights();
        }

        private void SetRights()
        {
            if (!GblIQCare.HasFunctionRight(ApplicationAccess.BudgetConfiguration, FunctionAccess.Update, GblIQCare.dtUserRight))
            {
                btnSave.Visible = false;
                dgwBudgetConfig.ReadOnly = true;
            }
        }

        private void Init_Form()
        {
            string XmlFile = GblIQCare.GetXMLPath() + "Currency.xml";
            DataSet theDSCurrencyXML = new DataSet();
            theDSCurrencyXML.ReadXml(XmlFile);

            IBudgetConfigDetail objProgramlistByDonor = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsProgramListByDonor = objProgramlistByDonor.GetProgramListByDonor(Convert.ToInt32(GblIQCare.objHashtbl["DonorID"]));
            //if (dsProgramListByDonor.Tables[0].Select("Id = '" + GblIQCare.objHashtbl["ProgramID"] + "'").SingleOrDefault() != null)
            if (dsProgramListByDonor.Tables[0].Select("Id = '" + GblIQCare.objHashtbl["ProgramID"] + "'").Length>0)
            {
                //DataRow []dr=dsProgramListByDonor.Tables[0].Select("Id = '" + GblIQCare.objHashtbl["ProgramID"] + "'");
                //foreach(DataRow drdt in dr)
                //{
                    

                //}
                FundEndDt = Convert.ToDateTime(dsProgramListByDonor.Tables[0].Select("Id = '" + GblIQCare.objHashtbl["ProgramID"] + "' and FundingEndDate=max(FundingEndDate)").SingleOrDefault().ItemArray[10]);
                FundEndDt = Convert.ToDateTime(FundEndDt.Month + "-" + System.DateTime.DaysInMonth(FundEndDt.Year, FundEndDt.Month) + "-" + FundEndDt.Year);
                int month = Convert.ToInt32(dsProgramListByDonor.Tables[0].Select("Id = '" + GblIQCare.objHashtbl["ProgramID"] + "'  and FundingStartDate=min(FundingStartDate)").SingleOrDefault().ItemArray[4]);
                ProgramStartDt = Convert.ToDateTime(month + "-" + System.DateTime.DaysInMonth(Convert.ToInt32(GblIQCare.objHashtbl["ProgramYear"]), month) + "-" + GblIQCare.objHashtbl["ProgramYear"]);
            }

            string CurrencyCode = theDSCurrencyXML.Tables[0].Select("Id = '" + GblIQCare.AppCountryId + "'").SingleOrDefault().ItemArray[1].ToString().Split('(').LastOrDefault().Replace(")", "");
            lblCurrency.Text = "Currency: " + CurrencyCode;

            txtDonor.Text = GblIQCare.objHashtbl["DonorName"].ToString();
            txtProgram.Text = GblIQCare.objHashtbl["ProgramName"].ToString();
            txtProgramYear.Text = GblIQCare.objHashtbl["ProgramYear"].ToString();
            LoadGrid(LoadGridData().Tables[1]);
        }

        private void LoadGrid(DataTable theDT)
        {
            try
            {
                dgwBudgetConfig.AutoGenerateColumns = true;
                dgwBudgetConfig.DataSource = theDT;
                dgwBudgetConfig.Columns[0].Visible = false;
                dgwBudgetConfig.Columns["Month"].ReadOnly = true;
                foreach (DataGridViewTextBoxColumn dgc in dgwBudgetConfig.Columns)
                {
                    dgc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgc.HeaderText = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(dgc.HeaderText.ToLower());
                    if (dgc.HeaderText.ToUpper() == "MONTHLYTOTAL")
                    {
                        dgc.HeaderText = "Monthly Total";
                    }
                }
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
            this.Close();
        }

        private DataSet LoadGridData()
        {
            IBudgetConfigDetail objBudgetConfigList = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsBudgetConfigList = objBudgetConfigList.GetBudgetConfigDetails(Convert.ToInt32(GblIQCare.objHashtbl["DonorID"]), Convert.ToInt32(GblIQCare.objHashtbl["ProgramID"]), Convert.ToInt32(GblIQCare.objHashtbl["ProgramYearID"]), CONST_CODEID);
            return dsBudgetConfigList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DTItemlist = (DataTable)dgwBudgetConfig.DataSource;
                DataSet SaveBudgetDataSet = new DataSet();
                SaveBudgetDataSet.Tables.Add(dsBudgetConfigList.Tables[0].Copy());
                SaveBudgetDataSet.Tables.Add(DTItemlist.Copy());

                IBudgetConfigDetail objItemlist = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
                int retrows = objItemlist.SaveBudgetConfigDetails(DTItemlist, dsBudgetConfigList.Tables[0], GblIQCare.AppUserId, GblIQCare.objHashtbl);
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                //Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwBudgetConfig_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool monthFound = false;
            bool setYear = false;
            string programYear = GblIQCare.objHashtbl["ProgramYear"].ToString();

            for (int f = 0; f < dgwBudgetConfig.Rows.Count - 1; ++f)
            {
                if (monthFound == false)
                {
                    if (dgwBudgetConfig.Rows[f].Cells[0].Value.ToString() == "12")
                    {
                        monthFound = true;
                        lblLabelRequired.Text = "No funding available for the months in blue.";
                    }
                }
                ProgramStartDt = Convert.ToDateTime(dgwBudgetConfig.Rows[f].Cells[0].Value + "-" + System.DateTime.DaysInMonth(Convert.ToInt32(programYear), Convert.ToInt32(dgwBudgetConfig.Rows[f].Cells[0].Value)) + "-" + programYear);

                if (ProgramStartDt > FundEndDt)
                {
                    dgwBudgetConfig.Rows[f].ReadOnly = true;
                    dgwBudgetConfig.Rows[f].DefaultCellStyle.ForeColor = Color.Blue;
                }

                if (monthFound == true && setYear == false)
                {
                    programYear = (Convert.ToInt32(GblIQCare.objHashtbl["ProgramYear"].ToString()) + 1).ToString();
                    setYear = true;
                }
            }
        }

        private void dgwBudgetConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgwBudgetConfig_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 1)
            {
                dgwBudgetConfig.Rows[e.RowIndex].ErrorText = "";
                float newVal;
                if (dgwBudgetConfig.Rows[e.RowIndex].IsNewRow) { return; }
                if (!float.TryParse(e.FormattedValue.ToString(),
                  out newVal) || newVal < 0)
                {
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("The field cannot be blank or a negative number.", "", "", this);
                }
            }
        }

        private void dgwBudgetConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgwBudgetConfig.CurrentCell.Value = Convert.ToDouble(dgwBudgetConfig.CurrentCell.Value).ToString("F");

            dgwBudgetConfig.Rows[e.RowIndex].ErrorText = String.Empty;
            DTItemlist = (DataTable)dgwBudgetConfig.DataSource;

            for (int j = 2; j < dgwBudgetConfig.Columns.Count; ++j)
            {
                double total = 0;
                for (int i = 0; i < dgwBudgetConfig.Rows.Count - 1; ++i)
                {
                    total += Convert.ToDouble(dgwBudgetConfig.Rows[i].Cells[j].Value);
                }
                dgwBudgetConfig.Rows[12].Cells[j].Value = total.ToString("F");
            }

            for (int f = 0; f < dgwBudgetConfig.Rows.Count; ++f)
            {
                double total = 0;
                int columnCount = dgwBudgetConfig.Columns.Count - 1;
                for (int k = 2; k < dgwBudgetConfig.Columns.Count - 1; ++k)
                {
                    total += Convert.ToDouble(dgwBudgetConfig.Rows[f].Cells[k].Value);
                }
                dgwBudgetConfig.Rows[f].Cells[columnCount].Value = total.ToString("F");
            }
        }

        private void dgwBudgetConfig_GotFocus(object sender, EventArgs e)
        {
            DTItemlist = (DataTable)dgwBudgetConfig.DataSource;

            for (int j = 2; j < dgwBudgetConfig.Columns.Count; ++j)
            {
                double total = 0;
                for (int i = 0; i < dgwBudgetConfig.Rows.Count - 1; ++i)
                {
                    total += Convert.ToDouble(dgwBudgetConfig.Rows[i].Cells[j].Value);
                }
                dgwBudgetConfig.Rows[12].Cells[j].Value = total.ToString("F");
            }

            for (int f = 0; f < dgwBudgetConfig.Rows.Count; ++f)
            {
                double total = 0;
                int columnCount = dgwBudgetConfig.Columns.Count - 1;
                for (int k = 2; k < dgwBudgetConfig.Columns.Count-1; ++k)
                {
                    total += Convert.ToDouble(dgwBudgetConfig.Rows[f].Cells[k].Value);
                }
                dgwBudgetConfig.Rows[f].Cells[columnCount].Value = total.ToString("F");
            }
            
        }

    }
}
