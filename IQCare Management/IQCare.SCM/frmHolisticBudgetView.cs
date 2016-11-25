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
using System.Diagnostics;

namespace IQCare.SCM
{
    public partial class frmHolisticBudgetView : Form
    {
        DataSet dsCostAllocationList = new DataSet();
        DataTable DTItemlist = new DataTable();
        DataSet dsGetDefaultDate = new DataSet();
        DataSet dsHolisticBudgetViewList = new DataSet();
        private const int CONST_CODEID = 201;         //CHECK WITH SANJAY WHERE TO PUT THIS VALUE?

        public frmHolisticBudgetView()
        {
            InitializeComponent();
        }

        private void frmHolisticBudgetView_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new System.Drawing.Point(1, 1);
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                Init_Form();
                //BindDropdown();
                SetRights();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void SetRights()
        {
            if (!GblIQCare.HasFunctionRight(ApplicationAccess.BudgetConfiguration, FunctionAccess.Delete, GblIQCare.dtUserRight))
            {
                dgwHolisticBudgetView.ReadOnly = true;
            }
        }

        private void Init_Form()
        {
            IBudgetConfigDetail objCostAllocationlist = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsCostAllocationList = objCostAllocationlist.GetCostAllocation(CONST_CODEID);

            DataRow row = dsCostAllocationList.Tables[0].NewRow();
            row["Name"] = "Total Budget";
            row["Id"] = "0";
            dsCostAllocationList.Tables[0].Rows.InsertAt(row, 0);

            BindDropdown();

            dsGetDefaultDate = objCostAllocationlist.GetHolisticBudgetViewDefaultYear();

            if ((dsGetDefaultDate.Tables[0].Rows[0]["ProgramYear"].ToString() != "") || (dsGetDefaultDate.Tables[0].Rows[0]["ProgramYear"] != DBNull.Value))
            {
                ddlCalendarYear.SelectedValue = dsGetDefaultDate.Tables[0].Rows[0]["ProgramYear"].ToString();
            }
            else
                ddlCalendarYear.SelectedValue = DateTime.Now.Year.ToString();

            //SelectedComboVal();
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
            ddlCalendarYear.Items.Clear();
            objBindControls.Win_BindCombo(ddlCalendarYear, theDT, "Name", "Id");

            ddlCostCategory.DataSource = null;
            ddlCostCategory.Items.Clear();
            objBindControls.Win_BindCombo(ddlCostCategory, dsCostAllocationList.Tables[0], "Name", "Id");
        }

        private void LoadGrid(DataTable theDT)
        {
                Hashtable colHash = new Hashtable();
                int j = 0;
                dgwHolisticBudgetView.ReadOnly = true;

                if (ddlCostCategory.SelectedValue != null) { if (ddlCostCategory.SelectedIndex != 0) { if (theDT.Rows.Count == 0) { IQCareWindowMsgBox.ShowWindow("No Budget Information exists.", "", "", this); } } }

                
                if (theDT.Rows.Count > 0)
                {
                    DataRow row = theDT.NewRow();
                    row["BudgetMonthID"] = "13";
                    row["BudgetMonthName"] = "Total";
                    for (Int32 i = 5; i <= theDT.Columns.Count - 1; i++)
                    {
                        decimal theSum= Convert.ToDecimal(0);
                        foreach (DataRow theSumDT in theDT.Rows)
                        {
                            theSum = theSum + Convert.ToDecimal(theSumDT[i]);
                        }
                        row[i] = theSum;
                    }
                    theDT.Rows.InsertAt(row, theDT.Rows.Count);
                }

                dgwHolisticBudgetView.DataSource = null;
                dgwHolisticBudgetView.Columns.Clear();
                dgwHolisticBudgetView.AutoGenerateColumns = true;
                dgwHolisticBudgetView.DataSource = theDT;
                dgwHolisticBudgetView.Columns[0].Visible = false;
                dgwHolisticBudgetView.Columns[1].Visible = false;
                dgwHolisticBudgetView.Columns[2].Visible = false;
                dgwHolisticBudgetView.Columns[3].Visible = false;
                

                foreach (DataGridViewTextBoxColumn dgc in dgwHolisticBudgetView.Columns)
                {
                    dgc.HeaderText = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(dgc.HeaderText.ToLower());
                    dgc.Width = 270;
                    dgc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (dgc.HeaderText.ToUpper() == "MONTHLYTOTAL")
                    {
                        //dgc.HeaderText = "Monthly Total";
                        dgc.HeaderText = ddlCostCategory.Text;
                        dgc.Width = 260;
                    }
                    else if (dgc.HeaderText.ToUpper() == "BUDGETMONTHNAME")
                    {
                        dgc.HeaderText = "Month";
                        dgc.Width = 150;
                    }

                    if (dgc.HeaderText.Split('-').Count() > 1)
                    {
                        j = j + 1;
                        colHash.Add(dgc.HeaderText + " %", dgc.Index + j);
                    }
                }
                dgwHolisticBudgetView.AutoGenerateColumns = false;
                //foreach (DictionaryEntry item in colHash)
                //{
                //    DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                //    col1.HeaderText = item.Key.ToString();
                //    col1.Name = item.Key.ToString();
                //    dgwHolisticBudgetView.Columns.Insert(Convert.ToInt32(item.Value), col1);
                //}
                
        }

        private DataSet LoadGridData(int costCategoryVal, int calendarYearVal)
        {
            IBudgetConfigDetail objHolisticBudgetViewList = (IBudgetConfigDetail)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBudgetConfigDetail,BusinessProcess.SCM");
            dsHolisticBudgetViewList = objHolisticBudgetViewList.GetHolisticBudgetView(costCategoryVal, calendarYearVal);
            return dsHolisticBudgetViewList;
        }

        private void ddlCostCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedComboVal();
        }

        private void ddlCalendarYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedComboVal();
        }

        private void SelectedComboVal()
        {
            try
            {
                //if (ddlCostCategory.SelectedIndex == 0) { return; }
                if (ddlCalendarYear.SelectedIndex == 0) { return; }

                BindFunctions objBindControls = new BindFunctions();
                int costCategoryVal = 0;
                int calendarYearVal = 0;

                if (ddlCostCategory.SelectedValue != null) { if (ddlCostCategory.SelectedIndex >= 0) { costCategoryVal = Convert.ToInt32(ddlCostCategory.SelectedValue); } }
                if (ddlCalendarYear.SelectedValue != null) { if (ddlCalendarYear.SelectedIndex != 0) { calendarYearVal = Convert.ToInt32(ddlCalendarYear.SelectedValue); } }
                LoadGrid(LoadGridData(costCategoryVal, calendarYearVal).Tables[0]);
                dgwHolisticBudgetView.Focus();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        public Hashtable SelectedValue(int DonorID, int ProgramID, int ProgramYearId)
        {
            Hashtable selectedValHash = new Hashtable();
            selectedValHash.Add("DonorID", DonorID);
            selectedValHash.Add("ProgramID", ProgramID);
            selectedValHash.Add("ProgramYearID", ProgramYearId);
            return selectedValHash;
        }

        private void dgwHolisticBudgetView_GotFocus(object sender, EventArgs e)
        {
            try
            {
                ///////////DTItemlist = (DataTable)dgwHolisticBudgetView.DataSource;

                //////////if (DTItemlist.Rows.Count > 0)
                //////////{
                //////////    for (int j = 5; j < dgwHolisticBudgetView.Columns.Count; ++j)
                //////////    {
                //////////        double total = 0;

                //////////        for (int i = 0; i < dgwHolisticBudgetView.Rows.Count - 1; ++i)
                //////////        {
                //////////            total += Convert.ToDouble(dgwHolisticBudgetView.Rows[i].Cells[j].Value);
                //////////        }
                //////////        dgwHolisticBudgetView.Rows[12].Cells[j].Value = total.ToString("F");
                //////////    }

                //////////}

                foreach (DataGridViewTextBoxColumn dgc in dgwHolisticBudgetView.Columns)
                {
                    if (dgc.HeaderText.Split('%').Count() > 1)
                    {
                        for (int f = 0; f < dgwHolisticBudgetView.Rows.Count; ++f)
                        {
                            dgwHolisticBudgetView.Rows[f].Cells[dgc.Index].Value = (((Convert.ToDouble(dgwHolisticBudgetView.Rows[f].Cells[dgc.Index - 1].Value)) / (Convert.ToDouble(dgwHolisticBudgetView.Rows[f].Cells[5].Value))) * 100).ToString("F").Replace("NaN", "0.00");
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
        }

         private void btnClose_Click(object sender, EventArgs e)
         {
             this.Close();
         }
    }
}
