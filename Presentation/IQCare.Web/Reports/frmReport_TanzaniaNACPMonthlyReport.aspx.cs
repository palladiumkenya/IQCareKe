using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Excel = Microsoft.Office.Interop.Owc11;
namespace IQCare.Web.Reports
{
    public partial class TanzaniaNACPMonthlyReport : System.Web.UI.Page
    {
        DateTime theYear;


        public DataSet theRepDS;
        public DataSet theRepQuarterDS;

        protected void Init_Form()
        {

            BindFunctions BindManager = new BindFunctions();
            DataTable Month = BindManager.GetMonths();
            BindManager.BindCombo(ddMonth, Month, "Name", "Id");
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            DataSet repquarter = (DataSet)ReportDetails.GetReportQuarter();
            BindManager.BindCombo(ddQuarter, repquarter.Tables[0], "QTR_Desc", "Qtr_pk");


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                //(Master.FindControl("lblMark") as Label).Text = "»";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = "Donor Reports »NACP Tanzania Monthly Report ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports »NACP Tanzania Monthly Report";

                Init_Form();
                txtYear.Attributes.Add("onkeyup", "chkNumeric('" + txtYear.ClientID + "')");
                txtyears.Attributes.Add("onkeyup", "chkNumeric('" + txtyears.ClientID + "')");
                rdoMonth.Checked = true;
            }
            if ((rdoMonth.Checked == true) || (rdoQuarter.Checked == true))
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script language='javascript' defer ='defer' id = 'scriptonload'>btnEnabledDisabled();</script>");

        }
        //  private void writeCellWiseInExcel(Excel.Spreadsheet theSheet) { }
        private void writeCellWiseInExcel(Excel.Spreadsheet theSheet)
        {
            try
            {
                if ((ddQuarter.SelectedValue.ToString() != "0") && (txtyears.Text != ""))
                {
                    // theSheet.
                    writeInCell(theSheet, "C4", "FacilityInfo1", "Date");
                    writeInCell(theSheet, "C6", "FacilityInfo1", "FacilityName");
                    writeInCell(theSheet, "C8", "FacilityInfo1", "District");
                    writeInCell(theSheet, "E4", "", Application["AppCurrentDate"].ToString());
                    writeInCell(theSheet, "E8", "FacilityInfo1", "Region");

                    writeInCell(theSheet, "E15", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE15");
                    writeInCell(theSheet, "E16", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE16");
                    writeInCell(theSheet, "E17", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE17");
                    writeInCell(theSheet, "E18", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE18");

                    writeInCell(theSheet, "E21", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE21");
                    writeInCell(theSheet, "E22", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE22");
                    writeInCell(theSheet, "E23", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE23");
                    writeInCell(theSheet, "E24", theRepQuarterDS.Tables[1].TableName.ToString(), "Table3ColE24");

                    writeInCell(theSheet, "D35", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColD35");
                    writeInCell(theSheet, "D36", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColD36");
                    writeInCell(theSheet, "D37", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColD37");
                    writeInCell(theSheet, "D38", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColD38");

                    writeInCell(theSheet, "D41", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColD41");
                    writeInCell(theSheet, "E41", theRepQuarterDS.Tables[2].TableName.ToString(), "Table4ColE41");

                }
                else if ((ddMonth.SelectedValue.ToString() != "0") && (txtYear.Text.Trim() != ""))
                {

                    writeInCell(theSheet, "C5", "FacilityInfo", "Date");
                    writeInCell(theSheet, "C7", "FacilityInfo", "FacilityName");
                    writeInCell(theSheet, "C9", "FacilityInfo", "District");
                    writeInCell(theSheet, "E5", "", Application["AppCurrentDate"].ToString());
                    writeInCell(theSheet, "E9", "FacilityInfo", "Region");

                    writeInCell(theSheet, "D16", theRepDS.Tables[1].TableName.ToString(), "Table1ColD16");
                    writeInCell(theSheet, "D17", theRepDS.Tables[1].TableName.ToString(), "Table1ColD17");
                    writeInCell(theSheet, "D18", theRepDS.Tables[1].TableName.ToString(), "Table1ColD18");
                    writeInCell(theSheet, "D19", theRepDS.Tables[1].TableName.ToString(), "Table1ColD19");

                    writeInCell(theSheet, "E16", theRepDS.Tables[1].TableName.ToString(), "Table1ColE16");
                    writeInCell(theSheet, "E17", theRepDS.Tables[1].TableName.ToString(), "Table1ColE17");
                    writeInCell(theSheet, "E18", theRepDS.Tables[1].TableName.ToString(), "Table1ColE18");
                    writeInCell(theSheet, "E19", theRepDS.Tables[1].TableName.ToString(), "Table1ColE19");

                    writeInCell(theSheet, "G16", theRepDS.Tables[1].TableName.ToString(), "Table1ColG16");
                    writeInCell(theSheet, "G17", theRepDS.Tables[1].TableName.ToString(), "Table1ColG17");
                    writeInCell(theSheet, "G18", theRepDS.Tables[1].TableName.ToString(), "Table1ColG18");
                    writeInCell(theSheet, "G19", theRepDS.Tables[1].TableName.ToString(), "Table1ColG19");

                    writeInCell(theSheet, "D24", theRepDS.Tables[2].TableName.ToString(), "Table2ColD24");
                    writeInCell(theSheet, "D25", theRepDS.Tables[2].TableName.ToString(), "Table2ColD25");
                    writeInCell(theSheet, "D26", theRepDS.Tables[2].TableName.ToString(), "Table2ColD26");
                    writeInCell(theSheet, "D27", theRepDS.Tables[2].TableName.ToString(), "Table2ColD27");
                    writeInCell(theSheet, "D29", theRepDS.Tables[2].TableName.ToString(), "Table2ColD29");
                    writeInCell(theSheet, "D30", theRepDS.Tables[2].TableName.ToString(), "Table2ColD30");

                    writeInCell(theSheet, "E24", theRepDS.Tables[2].TableName.ToString(), "Table2ColE24");
                    writeInCell(theSheet, "E25", theRepDS.Tables[2].TableName.ToString(), "Table2ColE25");
                    writeInCell(theSheet, "E26", theRepDS.Tables[2].TableName.ToString(), "Table2ColE26");
                    writeInCell(theSheet, "E27", theRepDS.Tables[2].TableName.ToString(), "Table2ColE27");
                    writeInCell(theSheet, "E29", theRepDS.Tables[2].TableName.ToString(), "Table2ColE29");
                    writeInCell(theSheet, "E30", theRepDS.Tables[2].TableName.ToString(), "Table2ColE30");
                    writeInCell(theSheet, "F32", theRepDS.Tables[2].TableName.ToString(), "Table2ColF32");
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }
        private void writeInCell(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {

            Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
            //theRange.Value2
            //theRange.WrapText = true;
            string theExitvalue = "";
            if (theRange.Value2 != null)
                theExitvalue = theRange.Value2.ToString();
            else
                theExitvalue = "";
            if ((ddQuarter.SelectedValue.ToString() != "0") && (txtyears.Text != ""))
            {

                try
                {

                    if (tablename != "")
                    {
                        if (theExitvalue.ToString().Trim() == "")
                            theRange.Value2 = theRepQuarterDS.Tables[tablename].Rows[0][column].ToString();
                        else
                            theRange.Value2 = theExitvalue + "  " + theRepQuarterDS.Tables[tablename].Rows[0][column].ToString();
                    }
                    else
                    {
                        if (theExitvalue.ToString().Trim() == "")
                            theRange.Value2 = column;
                        else
                            theRange.Value2 = theExitvalue + "  " + column;

                    }

                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    return;
                }
            }

            else if ((ddMonth.SelectedValue.ToString() != "0") && (txtYear.Text != ""))
            {
                try
                {
                    if (tablename != "")
                    {
                        if (theExitvalue.ToString().Trim() == "")
                            theRange.Value2 = theRepDS.Tables[tablename].Rows[0][column].ToString();
                        else
                            theRange.Value2 = theExitvalue + "  " + theRepDS.Tables[tablename].Rows[0][column].ToString();
                    }
                    else
                    {
                        if (theExitvalue.ToString().Trim() == "")
                            theRange.Value2 = column;
                        else
                            theRange.Value2 = theExitvalue + "  " + column;

                    }
                }

                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                    return;
                }

            }
        }
        private Boolean validation()
        {
            if (rdoMonth.Checked == true)
            {

                IQCareUtils theUtils = new IQCareUtils();




                DateTime app = Convert.ToDateTime(Application["AppCurrentDate"]);

                if (ddMonth.SelectedItem.Text != "Select" && txtYear.Text != "")
                {
                    theYear = Convert.ToDateTime(theUtils.MakeDate("01-" + ddMonth.SelectedItem.Text + "-" + txtYear.Text));


                    if (theYear.Year > app.Year)
                    {
                        IQCareMsgBox.Show("Year", this);
                        txtYear.Focus();
                        return false;
                    }
                    if (theYear > Convert.ToDateTime(Application["AppCurrentDate"]))
                    {
                        IQCareMsgBox.Show("Month", this);
                        txtYear.Focus();
                        return false;
                    }


                }
                if (txtYear.Text == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Year";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }

                if (ddMonth.SelectedItem.Text == "Select")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Month";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }



            }

            if (rdoQuarter.Checked == true)
            {
                IReports ReportDetails;
                DataSet dsQuarterDate = new DataSet();
                IQCareUtils theUtils = new IQCareUtils();



                DateTime app = Convert.ToDateTime(Application["AppCurrentDate"]);

                if (ddQuarter.SelectedItem.Text != "Select" && txtyears.Text != "")
                {
                    ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtyears.Text.Trim()));


                    DateTime quartermonth = Convert.ToDateTime(dsQuarterDate.Tables[0].Rows[0][0].ToString());



                    if (quartermonth.Year > app.Year)
                    {
                        IQCareMsgBox.Show("Year", this);
                        txtYear.Focus();
                        return false;
                    }
                    if (quartermonth > Convert.ToDateTime(Application["AppCurrentDate"]))
                    {
                        IQCareMsgBox.Show("Quarter", this);
                        txtYear.Focus();
                        return false;
                    }
                }
                if (txtyears.Text == "")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Year";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }


                if (ddQuarter.SelectedItem.Text == "Select")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Quarter";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;

                }



            }

            return true;

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation() == false)
                {
                    return;
                }
                IQCareUtils theUtils = new IQCareUtils();


                if ((ddQuarter.SelectedValue.ToString() != "0") && (txtyears.Text.Trim() != ""))
                {

                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    theRepQuarterDS = (DataSet)ReportDetails.GetNACPQuarterlyReportData(Convert.ToInt32(ddQuarter.SelectedValue.ToString()), Convert.ToInt32(txtyears.Text.Trim()), Convert.ToInt32(Session["AppLocationId"]));

                    #region "TableNames"
                    theRepQuarterDS.Tables[0].TableName = "FacilityInfo1";
                    theRepQuarterDS.Tables[1].TableName = "Table1Data1";
                    theRepQuarterDS.Tables[2].TableName = "Table2Data1";

                    #endregion
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\TZNACPQuarterlyReport.xml");
                    theApp.XMLURL = theFilePath;
                    writeCellWiseInExcel(theApp);

                    //theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPQuarterlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionOpenInExcel, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPQuarterlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //theUtils.OpenExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPQuarterlyReport.xls"),Response);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPQuarterlyReport.xls"), Response);




                }
                else if ((ddMonth.SelectedValue.ToString() != "0") && (txtYear.Text.Trim() != ""))
                {



                    int years = Convert.ToInt32(txtYear.Text);
                    int Months = Convert.ToInt32(ddMonth.SelectedItem.Value);

                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    theRepDS = (DataSet)ReportDetails.GetNACPMonthlyReportData(Months, years, Convert.ToInt32(Session["AppLocationId"]));

                    #region "TableNames"
                    theRepDS.Tables[0].TableName = "FacilityInfo";
                    theRepDS.Tables[1].TableName = "Table1Data";
                    theRepDS.Tables[2].TableName = "Table2Data";
                    #endregion


                    //Response.Redirect("..\\ExcelFiles\\TZNACPMonthlyReport.xls");
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\TanzaniaNACPMonthlyreport.xml");
                    theApp.XMLURL = theFilePath;
                    writeCellWiseInExcel(theApp);

                    // theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionOpenInExcel, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportAsAppropriate);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //theUtils.OpenExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Response);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Response);
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmReportDonorJump.aspx");
        }
    }
}