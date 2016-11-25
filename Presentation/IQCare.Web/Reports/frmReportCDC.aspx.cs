using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using Interface.Reports;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Owc11;
namespace IQCare.Web.Reports
{
    public partial class frmReportCDC : System.Web.UI.Page
    {

        #region "Export Variables"

        public DataTable theCountry;

        //string FName;
        #endregion
        private DataSet dsCDCRep;


        string theStartDate = string.Empty;
        string theEndDate = string.Empty;
        DataSet dsQuarterDate = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ReportName"] != null)
                lblHeadertext.Text = Session["ReportName"].ToString();
            if (Page.IsPostBack != true)
            {
                // (Master.FindControl("lblpntStatus")as Label).Text = Request.QueryString["sts"].ToString(); 
                DateTime currDateType;
                string currentDate;
                int currYear;

                currentDate = string.Format("{0:dd/mm/yyyy}", Application["AppCurrentDate"].ToString());
                currDateType = Convert.ToDateTime(currentDate);
                currYear = currDateType.Year;

                //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                //(Master.FindControl("lblMark") as Label).Text = "»";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = " Donor Reports » Track 1.0 Quarterly Report";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports » Track 1.0 Quarterly Report";

                txtYear.Attributes.Add("onkeyup", "chkNumeric('" + txtYear.ClientID + "')");
                txtYear.Attributes.Add("onblur", "CheckDate('" + currYear + "')");

                txtStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
                txtStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtEndDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
                txtEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                btnSubmit.Attributes.Add("Onclick", "javaScript:return Validate();");

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //string theReportName = "CDCReport";
                if (Session["ReportId"].ToString() == "CDCReport")
                {
                    IReports ReportDetails;
                    DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();

                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    }

                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {


                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");

                        if (chkAllLocations.Checked == true)
                        {
                            dsCDCRep = (DataSet)ReportDetails.GetCDSReportData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), 0);
                        }
                        else

                            dsCDCRep = (DataSet)ReportDetails.GetCDSReportData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));

                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[8].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\CDC.xml");
                        theApp.XMLURL = theFilePath;
                        writeCellWiseInExcel(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\CDC.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\CDC.xls"), Response);
                    }
                }
                //////////////////////////////////////////////////////////PMTCT TRACK1.0
                else if (Session["ReportId"].ToString() == "Track1PMTCT")
                {
                    IReports ReportDetails;
                    DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();

                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    }
                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {


                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsCDCRep = (DataSet)ReportDetails.GetPMTCTTrack10ReportData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));
                        #region "TableNames"
                        dsCDCRep.Tables[0].TableName = "Table0";
                        dsCDCRep.Tables[1].TableName = "Table1";

                        #endregion

                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[1].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\PMTCTTRACK1.0.xml");
                        theApp.XMLURL = theFilePath;
                        writeCellWiseInExcel_Track10(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\PMTCTTRACK1.0.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\PMTCTTRACK1.0.xls"), Response);
                    }

                }
                ////////////////////////////////HIV EXPOSED INFANTS //////////////////////////////////////////////////
                else if (Session["ReportId"].ToString() == "HivExposedInfants")
                {
                    IReports ReportDetails;
                    DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();

                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    }
                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {


                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsCDCRep = (DataSet)ReportDetails.GetExposedInfantsData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));


                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[5].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\HivExposedInfants.xml");
                        theApp.XMLURL = theFilePath;
                        writeCellWiseInExcel_HivExposedInfants(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\HivExposedInfants.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\HivExposedInfants.xls"), Response);
                    }

                }
                //////////////////////////////////Nigeria MR Report////////////////////////////////////////////////////////////////


                else if (Session["ReportId"].ToString() == "MRReport")
                {
                    IReports ReportDetails;
                    DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();

                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    }
                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {


                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsCDCRep = (DataSet)ReportDetails.GetMRReportData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));


                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[72].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\MRReport.xml");
                        theApp.XMLURL = theFilePath;
                        writeCellWiseInExcel_MRReport(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\MRReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\MRReport.xls"), Response);
                    }

                }


                ////////////////////////////////Reporting PMTCT Cascade Data - OGAC //////////////////////////////////////////////////
                else if (Session["ReportId"].ToString() == "UgandaOGAC")
                {
                    IReports ReportDetails;
                    DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();

                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    }
                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {


                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsCDCRep = (DataSet)ReportDetails.GetOGACData(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));


                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[8].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\OGAC.xml");
                        theApp.XMLURL = theFilePath;
                        writeCellWiseInExcel_OGAC(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\OGAC.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\OGAC.xls"), Response);
                    }

                }
                else if (Session["ReportId"].ToString() == "UgandaMonthly")
                {
                    if (FieldValidation() == false)
                    {
                        return;
                    }

                    IReports ReportDetails;
                    //DataSet dsQuarterDate = new DataSet();
                    IQCareUtils theUtil = new IQCareUtils();






                    if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                    {
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                        if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                        {
                            theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                            theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());



                        }
                    }
                    else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                    {
                        theStartDate = theUtil.MakeDate(txtStartDate.Value);
                        theEndDate = theUtil.MakeDate(txtEndDate.Value);


                    }
                    if (((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0")) || ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != "")))
                    {

                        IQCareUtils theUtilsCF = new IQCareUtils();
                        ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                        dsCDCRep = (DataSet)ReportDetails.GetUgandaMOHMonthlyReport(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), Convert.ToInt16(Session["AppLocationId"]));

                        #region "TableNames"
                        dsCDCRep.Tables[0].TableName = "TableDate";
                        dsCDCRep.Tables[1].TableName = "Table0";
                        dsCDCRep.Tables[2].TableName = "Table1";
                        dsCDCRep.Tables[3].TableName = "Table2";
                        dsCDCRep.Tables[4].TableName = "Table3";

                        #endregion


                        DataSet theDSXML = new DataSet();
                        string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
                        theDSXML.ReadXml(xmlFilePath);
                        DataView theDV = new DataView(theDSXML.Tables[0]);
                        string countryid = dsCDCRep.Tables[5].Rows[0]["Currency"].ToString();
                        theDV.RowFilter = "id =" + countryid + "";
                        theCountry = (DataTable)theUtilsCF.CreateTableFromDataView(theDV);
                        string s = theCountry.Rows[0]["Name"].ToString();
                        string[] theCount = s.Split(new char[] { ',' });
                        ViewState["CntryName"] = theCount[0].ToString();

                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\UgandaMonthlyReport.xml");
                        theApp.XMLURL = theFilePath;
                        WriteCellWiseInExcel_UgandaMonthlyReport(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\UgandaMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\UgandaMonthlyReport.xls"), Response);
                    }



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
        private Boolean FieldValidation()
        {
            try
            {
                IReports ReportDetails;

                IQCareUtils theUtil = new IQCareUtils();
                string theStartDate = string.Empty;

                string theEndDate = string.Empty;

                if ((this.txtYear.Text.Trim().ToString() != "") && (this.ddQuarter.SelectedValue.ToString() != "0"))
                {
                    ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    dsQuarterDate = ReportDetails.GetCDSReportQuarterDate(Convert.ToInt32(ddQuarter.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()));
                    if (dsQuarterDate != null && dsQuarterDate.Tables[0].Rows.Count > 0)
                    {
                        theStartDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][0]).ToString(Session["AppDateFormat"].ToString());
                        theEndDate = ((DateTime)dsQuarterDate.Tables[0].Rows[0][1]).ToString(Session["AppDateFormat"].ToString());
                        DateTime theStartYear = Convert.ToDateTime(theStartDate);
                        DateTime theEndYear = Convert.ToDateTime(theEndDate);

                        if (theStartYear.Year > theEndYear.Year)
                        {
                            IQCareMsgBox.Show("Year", this);
                            txtYear.Focus();
                            return false;

                        }
                        if (theStartYear > Convert.ToDateTime(Application["AppCurrentDate"]))
                        {
                            IQCareMsgBox.Show("Quarter", this);
                            txtYear.Focus();
                            return false;

                        }
                    }
                }
                else if ((this.txtStartDate.Value.ToString() != "") && (this.txtEndDate.Value.ToString() != ""))
                {
                    theStartDate = theUtil.MakeDate(txtStartDate.Value);
                    theEndDate = theUtil.MakeDate(txtEndDate.Value);
                    DateTime theStartYear = Convert.ToDateTime(theStartDate);
                    DateTime theEndYear = Convert.ToDateTime(theEndDate);

                    if (theStartYear.Year > theEndYear.Year)
                    {
                        IQCareMsgBox.Show("Year", this);
                        txtYear.Focus();
                        return false;

                    }
                    if (theEndYear > Convert.ToDateTime(Application["AppCurrentDate"]))
                    {
                        IQCareMsgBox.Show("Month", this);
                        txtYear.Focus();
                        return false;

                    }
                }







            }
            catch
            {
                return false;
            }

            return true;
        }

        private void writeCellWiseInExcel(Excel.Spreadsheet theSheet)
        {
            try
            {
                //For "dtHIVPaliative" table 

                writeInCell(theSheet, "E12", "Table", "VCountMLess15EverEnrolled");
                writeInCell(theSheet, "E13", "Table", "VCountMAbove15EverEnrolled");
                writeInCell(theSheet, "E14", "Table", "VCountFLess15EverEnrolled");
                writeInCell(theSheet, "E15", "Table", "VCountFAbove15EverEnrolled");

                writeInCell(theSheet, "G12", "Table", "VCountMLess15NewEnrollee");
                writeInCell(theSheet, "G13", "Table", "VCountMAbove15NewEnrollee");
                writeInCell(theSheet, "G14", "Table", "VCountFLess15NewEnrollee");
                writeInCell(theSheet, "G15", "Table", "VCountFAbove15NewEnrollee");

                writeInCell(theSheet, "Q12", "Table", "RHIVCareMLess15");
                writeInCell(theSheet, "Q13", "Table", "RHIVCareMAbove15");
                writeInCell(theSheet, "Q14", "Table", "RHIVCareFLess15");
                writeInCell(theSheet, "Q15", "Table", "RHIVCareFAbove15");

                writeInCell(theSheet, "Q18", "Table", "RHIVCareART");

                //For "PediatricHIVPalliative" table 
                writeInCell(theSheet, "E21", "Table1", "VCountM0To1EverEnrolled");
                writeInCell(theSheet, "E22", "Table1", "VCountMLess2To4EverEnrolled");
                writeInCell(theSheet, "E23", "Table1", "VCountMLess5To14EverEnrolled");
                writeInCell(theSheet, "E24", "Table1", "VCountF0To1EverEnrolled");
                writeInCell(theSheet, "E25", "Table1", "VCountF2To4EverEnrolled");
                writeInCell(theSheet, "E26", "Table1", "VCountF5To15EverEnrolled");

                writeInCell(theSheet, "G21", "Table1", "VCountM0To1NewEnrollee");
                writeInCell(theSheet, "G22", "Table1", "VCountM2To4NewEnrollee");
                writeInCell(theSheet, "G23", "Table1", "VCountM5To14NewEnrollee");
                writeInCell(theSheet, "G24", "Table1", "VCountF0To1NewEnrollee");
                writeInCell(theSheet, "G25", "Table1", "VCountF2To4NewEnrollee");
                writeInCell(theSheet, "G26", "Table1", "VCountF5To14NewEnrollee");

                writeInCell(theSheet, "Q21", "Table1", "RHIVCareM0To1");
                writeInCell(theSheet, "Q22", "Table1", "RHIVCareM2To4");
                writeInCell(theSheet, "Q23", "Table1", "RHIVCareM5To14");
                writeInCell(theSheet, "Q24", "Table1", "RHIVCareF0To1");
                writeInCell(theSheet, "Q25", "Table1", "RHIVCareF2To4");
                writeInCell(theSheet, "Q26", "Table1", "RHIVCareF5To14");

                //For "ARTCare" table 
                writeInCell(theSheet, "E31", "Table2", "ARTCareMale0to14A");
                writeInCell(theSheet, "E32", "Table2", "ARTCareMale15B");
                writeInCell(theSheet, "E33", "Table2", "ARTCareFemale0to14C");
                writeInCell(theSheet, "E34", "Table2", "ARTCareFemale15D");

                writeInCell(theSheet, "E37", "Table2", "ARTCarePregnantFemaleF");

                writeInCell(theSheet, "G31", "Table2", "ARTCareMale0to14G");
                writeInCell(theSheet, "G32", "Table2", "ARTCareMale15H");
                writeInCell(theSheet, "G33", "Table2", "ARTCareFemale0to14I");
                writeInCell(theSheet, "G34", "Table2", "ARTCareFemale15J");
                writeInCell(theSheet, "G37", "Table2", "ARTCarePregnantFemaleL");

                writeInCell(theSheet, "L31", "Table2", "ARTCareMaleNewEnroleeAA");
                writeInCell(theSheet, "L32", "Table2", "ARTCareMaleNewEnroleeBB");
                writeInCell(theSheet, "L33", "Table2", "ARTCareFemaleNewEnroleeCC");
                writeInCell(theSheet, "L34", "Table2", "ARTCareFemaleNewEnroleeDD");
                writeInCell(theSheet, "L37", "Table2", "ARTCarePregnantFemaleNewEnroleeFF");

                writeInCell(theSheet, "N31", "Table2", "ARTCareMaleTransferInGG");
                writeInCell(theSheet, "N32", "Table2", "ARTCareMaleTransferInhh");
                writeInCell(theSheet, "N33", "Table2", "ARTCareFemaleTransferInII");
                writeInCell(theSheet, "N34", "Table2", "ARTCareFemaleTransferInJJ");
                writeInCell(theSheet, "N37", "Table2", "ARTCarePregnantFemaleTransferInLL");

                writeInCell(theSheet, "Q31", "Table2", "ARTCareMaleCurrentMM");
                writeInCell(theSheet, "Q32", "Table2", "ARTCareMaleCurrentNN");
                writeInCell(theSheet, "Q33", "Table2", "ARTCareFemaleCurrentOO");
                writeInCell(theSheet, "Q34", "Table2", "ARTCareFemaleCurrentPP");
                writeInCell(theSheet, "Q37", "Table2", "ARTCarePregnantFemaleCurrentRR");


                //For "PediatricARTCare" table 
                writeInCell(theSheet, "E44", "Table3", "PediatricARTCareMale0To1A");
                writeInCell(theSheet, "E45", "Table3", "PediatricARTCareMale2To4B");
                writeInCell(theSheet, "E46", "Table3", "PediatricARTCareMale5To14C");
                writeInCell(theSheet, "E47", "Table3", "PediatricARTCareFemale0To1D");
                writeInCell(theSheet, "E48", "Table3", "PediatricARTCareFemale2To4E");
                writeInCell(theSheet, "E49", "Table3", "PediatricARTCareFemale5To14F");

                writeInCell(theSheet, "G44", "Table3", "PediatricARTCareMale0To1G");
                writeInCell(theSheet, "G45", "Table3", "PediatricARTCareMale2To4H");
                writeInCell(theSheet, "G46", "Table3", "PediatricARTCareMale5To14I");
                writeInCell(theSheet, "G47", "Table3", "PediatricARTCareFemale0To1J");
                writeInCell(theSheet, "G48", "Table3", "PediatricARTCareFemale2To4k");
                writeInCell(theSheet, "G49", "Table3", "PediatricARTCareFemale5To14L");

                writeInCell(theSheet, "L44", "Table3", "PediatricARTCareMaleNewEnrolee0to1S");
                writeInCell(theSheet, "L45", "Table3", "PediatricARTCareMaleNewEnrolee2to4t");
                writeInCell(theSheet, "L46", "Table3", "PediatricARTCareMaleNewEnrolee5to14u");
                writeInCell(theSheet, "L47", "Table3", "PediatricARTCareFemaleNewEnrolee0to1V");
                writeInCell(theSheet, "L48", "Table3", "PediatricARTCareFemaleNewEnrolee2to4w");
                writeInCell(theSheet, "L49", "Table3", "PediatricARTCareFemaleNewEnrolee5to14X");

                writeInCell(theSheet, "N44", "Table3", "TransfersMalesLessThan2");
                writeInCell(theSheet, "N45", "Table3", "TransfersMalesBetween2And4");
                writeInCell(theSheet, "N46", "Table3", "TransfersMalesBetween5And14");
                writeInCell(theSheet, "N47", "Table3", "TransfersFemalesLessThan2");
                writeInCell(theSheet, "N48", "Table3", "TransfersFemalesBetween2And4");
                writeInCell(theSheet, "N49", "Table3", "TransfersFemalesBetween5And14");

                writeInCell(theSheet, "Q44", "Table3", "TotalMalesLessThan2");
                writeInCell(theSheet, "Q45", "Table3", "TotalMalesBetween2And4");
                writeInCell(theSheet, "Q46", "Table3", "TotalMalesBetween5And14");
                writeInCell(theSheet, "Q47", "Table3", "TotalFemalesLessThan2");
                writeInCell(theSheet, "Q48", "Table3", "TotalFemalesBetween2And4");
                writeInCell(theSheet, "Q49", "Table3", "TotalFemalesBetween5And14");

                //For "ChangeInCD4HalfYearly" table 
                writeInCell(theSheet, "E61", "Table4", "cohortmonth");
                writeInCell(theSheet, "E62", "Table4", "CohortBaseline");
                writeInCell(theSheet, "E63", "Table4", "CohortCd4Count");
                writeInCell(theSheet, "E64", "Table4", "CD4Median6Baseline");


                writeInCell(theSheet, "G62", "Table4", "cohort6months");
                writeInCell(theSheet, "G63", "Table4", "CD4Cohort6months");
                writeInCell(theSheet, "G64", "Table4", "CD4Median6outof6");
                writeInCell(theSheet, "G65", "Table4", "CD4Cohort6outof6");

                //For "ChangeInCD4Yearly" table 

                writeInCell(theSheet, "N62", "Table5", "CohortBaseline");
                writeInCell(theSheet, "N63", "Table5", "CohortCd4Count");
                writeInCell(theSheet, "N64", "Table5", "CD4Median12Baseline");

                writeInCell(theSheet, "Q62", "Table5", "cohort12months");
                writeInCell(theSheet, "Q63", "Table5", "CD4Cohort12months");
                writeInCell(theSheet, "Q64", "Table5", "CD4Median12outof12");
                writeInCell(theSheet, "Q65", "Table5", "CD4Cohort12outof12");

                writeInCell(theSheet, "E78", "", dsCDCRep.Tables["Table6"].Rows[0]["StoppedARTMalesLess15"].ToString());
                writeInCell(theSheet, "E79", "", dsCDCRep.Tables["Table6"].Rows[0]["StoppedARTMalesGreater15"].ToString());
                writeInCell(theSheet, "E80", "", dsCDCRep.Tables["Table6"].Rows[0]["StoppedARTFemalesLess15"].ToString());
                writeInCell(theSheet, "E81", "", dsCDCRep.Tables["Table6"].Rows[0]["StoppedARTFemalesGreater15"].ToString());

                writeInCell(theSheet, "G78", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleTransferredOutLess15"].ToString());
                writeInCell(theSheet, "G79", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleTransferredOutGreater15"].ToString());
                writeInCell(theSheet, "G80", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleTransferredOutLess15"].ToString());
                writeInCell(theSheet, "G81", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleTransferredOutGreater15"].ToString());


                writeInCell(theSheet, "I78", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleDeathLess15"].ToString());
                writeInCell(theSheet, "I79", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleDeathGreater15"].ToString());
                writeInCell(theSheet, "I80", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleDeathLess15"].ToString());
                writeInCell(theSheet, "I81", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleDeathGreater15"].ToString());



                writeInCell(theSheet, "L78", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleLostFollowupLess15"].ToString());
                writeInCell(theSheet, "L79", "", dsCDCRep.Tables["Table6"].Rows[0]["MaleLostFollowupGreater15"].ToString());
                writeInCell(theSheet, "L80", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleLostFollowupLess15"].ToString());
                writeInCell(theSheet, "L81", "", dsCDCRep.Tables["Table6"].Rows[0]["FemaleLostFollowupGreater15"].ToString());


                writeInCell(theSheet, "N78", "Table6", "MaleUnknownLess15");
                writeInCell(theSheet, "N79", "Table6", "MaleUnknownGreater15");
                writeInCell(theSheet, "N80", "Table6", "FemaleUnknownLess15");
                writeInCell(theSheet, "N81", "Table6", "FemaleUnknownGreater15");

                //For "PediatricARTCareFollowUp" table

                writeInCell(theSheet, "E85", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTMalesLess2"].ToString());
                writeInCell(theSheet, "E86", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTMalesBetween2And4"].ToString());
                writeInCell(theSheet, "E87", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTMalesBetween5And14"].ToString());
                writeInCell(theSheet, "E88", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTFemalesLess2"].ToString());
                writeInCell(theSheet, "E89", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTFemalesBetween2And4"].ToString());
                writeInCell(theSheet, "E90", "", dsCDCRep.Tables["Table7"].Rows[0]["StoppedARTFemalesBetween5And14"].ToString());

                writeInCell(theSheet, "G85", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutMalesLess2"].ToString());
                writeInCell(theSheet, "G86", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutMalesBetween2And4"].ToString());
                writeInCell(theSheet, "G87", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutMalesBetween5And14"].ToString());
                writeInCell(theSheet, "G88", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutFemalesLess2"].ToString());
                writeInCell(theSheet, "G89", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutFemalesBetween2And4"].ToString());
                writeInCell(theSheet, "G90", "", dsCDCRep.Tables["Table7"].Rows[0]["TransferredOutFemalesBetween5And14"].ToString());

                writeInCell(theSheet, "I85", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTMalesLess2"].ToString());
                writeInCell(theSheet, "I86", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTMalesBetween2And4"].ToString());
                writeInCell(theSheet, "I87", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTMalesBetween5And14"].ToString());
                writeInCell(theSheet, "I88", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTFemalesLess2"].ToString());
                writeInCell(theSheet, "I89", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTFemalesBetween2And4"].ToString());
                writeInCell(theSheet, "I90", "", dsCDCRep.Tables["Table7"].Rows[0]["DeathARTFemalesBetween5And14"].ToString());

                writeInCell(theSheet, "L85", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTMalesLess2"].ToString());
                writeInCell(theSheet, "L86", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTMalesBetween2And4"].ToString());
                writeInCell(theSheet, "L87", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTMalesBetween5And14"].ToString());
                writeInCell(theSheet, "L88", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTFemalesLess2"].ToString());
                writeInCell(theSheet, "L89", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTFemalesBetween2And4"].ToString());
                writeInCell(theSheet, "L90", "", dsCDCRep.Tables["Table7"].Rows[0]["LostFollowupARTFemalesBetween5And14"].ToString());


                writeInCell(theSheet, "N85", "Table7", "UnknownARTMalesLess2");
                writeInCell(theSheet, "N86", "Table7", "UnknownARTMalesBetween2And4");
                writeInCell(theSheet, "N87", "Table7", "UnknownARTMalesBetween5And14");
                writeInCell(theSheet, "N88", "Table7", "UnknownARTFemalesLess2");
                writeInCell(theSheet, "N89", "Table7", "UnknownARTFemalesBetween2And4");
                writeInCell(theSheet, "N90", "Table7", "UnknownARTFemalesBetween5And14");
                if (dsCDCRep.Tables["Table8"].Rows[0]["PepFarStartDate"].ToString() != "")
                {
                    writeInCell(theSheet, "E4", "", Convert.ToDateTime(dsCDCRep.Tables["Table8"].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                }
                writeInCell(theSheet, "E5", "", Convert.ToDateTime(dsCDCRep.Tables["Table8"].Rows[0]["QtrStartDate"].ToString()).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(theSheet, "E6", "Table8", "grantee");
                writeInCell(theSheet, "E7", "Table8", "FacilityName");
                writeInCell(theSheet, "L5", "", Convert.ToDateTime(dsCDCRep.Tables["Table8"].Rows[0]["QtrEndDate"].ToString()).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(theSheet, "L6", "Table8", "FacilityName");
                writeInCell(theSheet, "L7", "", ViewState["CntryName"].ToString());

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }

        private void writeCellWiseInExcel_HivExposedInfants(Excel.Spreadsheet thesheet)
        {
            try
            {
                writeInCell(thesheet, "B1", "", "TRACK 1.0 EXPOSED HIV INFANTS REPORT");
                if (dsCDCRep.Tables[5].Rows[0]["PepFarStartDate"] != null && dsCDCRep.Tables[5].Rows[0]["PepFarStartDate"].ToString() != "")
                {
                    writeInCell(thesheet, "C3", "", Convert.ToDateTime(dsCDCRep.Tables[5].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                }
                writeInCell(thesheet, "C4", "", Convert.ToDateTime(dsCDCRep.Tables[5].Rows[0]["QtrStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "C5", "", dsCDCRep.Tables[5].Rows[0]["grantee"].ToString());
                writeInCell(thesheet, "C6", "", dsCDCRep.Tables[5].Rows[0]["FacilityName"].ToString());
                writeInCell(thesheet, "C7", "", ViewState["CntryName"].ToString());
                writeInCell(thesheet, "E4", "", Convert.ToDateTime(dsCDCRep.Tables[5].Rows[0]["QtrEndDate"]).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "E5", "", dsCDCRep.Tables[5].Rows[0]["FacilityName"].ToString());
                // writeInCell(thesheet, "E6", "",dsCDCRep.Tables[4].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "E7", "", "PMTCT");
                writeInCell(thesheet, "E12", "", dsCDCRep.Tables[0].Rows[0]["Count0"].ToString());
                writeInCell(thesheet, "E13", "", dsCDCRep.Tables[1].Rows[0]["Count1"].ToString());
                writeInCell(thesheet, "E14", "", dsCDCRep.Tables[2].Rows[0]["Count2"].ToString());
                writeInCell(thesheet, "E15", "", dsCDCRep.Tables[3].Rows[0]["Count3"].ToString());



            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }
        private void writeCellWiseInExcel_OGAC(Excel.Spreadsheet thesheet)
        {
            try
            {
                writeInCell(thesheet, "D1", "", "OGAC");
                if (dsCDCRep.Tables[8].Rows[0]["PepFarStartDate"] != null && dsCDCRep.Tables[8].Rows[0]["PepFarStartDate"].ToString() != "")
                {
                    writeInCell(thesheet, "C3", "", Convert.ToDateTime(dsCDCRep.Tables[8].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                }
                writeInCell(thesheet, "C4", "", Convert.ToDateTime(dsCDCRep.Tables[8].Rows[0]["QtrStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "C5", "", dsCDCRep.Tables[8].Rows[0]["grantee"].ToString());
                writeInCell(thesheet, "C6", "", dsCDCRep.Tables[8].Rows[0]["FacilityName"].ToString());
                writeInCell(thesheet, "C7", "", ViewState["CntryName"].ToString());
                writeInCell(thesheet, "E4", "", Convert.ToDateTime(dsCDCRep.Tables[8].Rows[0]["QtrEndDate"]).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "E5", "", dsCDCRep.Tables[8].Rows[0]["FacilityName"].ToString());
                writeInCell(thesheet, "E7", "", "PMTCT");

                writeInCell(thesheet, "E14", "", dsCDCRep.Tables[0].Rows[0]["c7"].ToString());
                writeInCell(thesheet, "E15", "", dsCDCRep.Tables[1].Rows[0]["c8"].ToString());
                writeInCell(thesheet, "E16", "", dsCDCRep.Tables[2].Rows[0]["c9"].ToString());
                writeInCell(thesheet, "E17", "", dsCDCRep.Tables[3].Rows[0]["c10"].ToString());
                writeInCell(thesheet, "E18", "", dsCDCRep.Tables[4].Rows[0]["c11"].ToString());
                writeInCell(thesheet, "E19", "", dsCDCRep.Tables[5].Rows[0]["c12"].ToString());
                writeInCell(thesheet, "E20", "", dsCDCRep.Tables[6].Rows[0]["c13"].ToString());
                writeInCell(thesheet, "E22", "", dsCDCRep.Tables[7].Rows[0]["c14"].ToString());



            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }
        private void writeCellWiseInExcel_Track10(Excel.Spreadsheet thesheet)
        {
            try
            {
                if (dsCDCRep.Tables["Table1"].Rows[0]["PepFarStartDate"].ToString() != "")
                {
                    writeInCell(thesheet, "C4", "", Convert.ToDateTime(dsCDCRep.Tables["Table1"].Rows[0]["PepFarStartDate"]).ToString(Session["AppDateFormat"].ToString()));
                }
                writeInCell(thesheet, "C5", "", Convert.ToDateTime(dsCDCRep.Tables["Table1"].Rows[0]["QtrStartDate"].ToString()).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "C6", "", "PMTCT");
                writeInCell(thesheet, "C7", "Table1", "FacilityName");
                writeInCell(thesheet, "G5", "", Convert.ToDateTime(dsCDCRep.Tables["Table1"].Rows[0]["QtrEndDate"].ToString()).ToString(Session["AppDateFormat"].ToString()));
                writeInCell(thesheet, "G6", "Table1", "FacilityName");
                writeInCell(thesheet, "G7", "dsCDCRep.Tables[Table1].TableName.ToString()", "");

                writeInCell(thesheet, "E16", "Table0", "SDRegimenWithANC");
                writeInCell(thesheet, "G16", "Table0", "SDRegimenWithMaternity");
                writeInCell(thesheet, "E17", "Table0", "MDRegimenWithANC");
                writeInCell(thesheet, "G17", "Table0", "MDRegimenWithMaternity");
                writeInCell(thesheet, "E18", "Table0", "ARTANC");
                writeInCell(thesheet, "G18", "Table0", "ARTMaternity");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }


        }

        private void WriteCellWiseInExcel_UgandaMonthlyReport(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "A3", "TableDate", "FName");
            writeInCell(theSheet, "A4", "TableDate", "Date");

            writeInCell(theSheet, "B8", "Table0", "PregnantWomenTestedHIVPositive");

            writeInCell(theSheet, "B16", "Table1", "PregnantWomenWithNVP");
            writeInCell(theSheet, "B17", "Table1", "PregnantWomenWithAZTandNVP");
            writeInCell(theSheet, "B18", "Table1", "PregnantWomenWithTripleTherapy");
            writeInCell(theSheet, "B19", "Table1", "PregnantWomenWithTripleTherapy1");

            writeInCell(theSheet, "B23", "Table2", "HIVPositiveDeliveries");
            writeInCell(theSheet, "B24", "Table2", "DeliveriesWithNVPDuringLabour");
            writeInCell(theSheet, "B25", "Table2", "DeliveriesWithAZTandNVPDuringLabour");
            writeInCell(theSheet, "B26", "Table2", "DeliveriesWithTripleTherapy");
            //writeInCell(theSheet, "B25", "Table2", "PartnerReferred");
            writeInCell(theSheet, "B29", "Table2", "MaternityMotherWithExclusiveBreastFeeding");
            writeInCell(theSheet, "B30", "Table2", "InfantsWithNVP");
            writeInCell(theSheet, "B31", "Table2", "InfantsWithNVPANDAZT");

            writeInCell(theSheet, "B34", "Table3", "childrenTestedForHIVEqualto2Months");
            writeInCell(theSheet, "B35", "Table3", "childrenTestedForHIVEqualto12Months");
            writeInCell(theSheet, "B36", "Table3", "childrenTestedForHIVEqualto18Months");
            writeInCell(theSheet, "B37", "Table3", "childrenTestedForHIVEqualto5Years");

            writeInCell(theSheet, "B39", "Table3", "childrenTestedForHIVPositiveEqualto2Months");
            writeInCell(theSheet, "B40", "Table3", "childrenTestedForHIVPositiveEqualto12Months");
            writeInCell(theSheet, "B41", "Table3", "childrenTestedForHIVPositiveEqualto18Months");
            writeInCell(theSheet, "B42", "Table3", "childrenTestedForHIVPositiveEqualto5Years");



        }
        private void writeCellWiseInExcel_MRReport(Excel.Spreadsheet thesheet)
        {
            try
            {


                int ColE52 = Convert.ToInt32(dsCDCRep.Tables[16].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[20].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[24].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[28].Rows[0][0].ToString());
                int ColF52 = Convert.ToInt32(dsCDCRep.Tables[17].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[21].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[25].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[29].Rows[0][0].ToString());
                int ColE53 = Convert.ToInt32(dsCDCRep.Tables[18].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[22].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[26].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[30].Rows[0][0].ToString());
                int ColF53 = Convert.ToInt32(dsCDCRep.Tables[19].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[23].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[27].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[31].Rows[0][0].ToString());

                int ColE69 = Convert.ToInt32(dsCDCRep.Tables[40].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[44].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[48].Rows[0][0].ToString());
                int ColF69 = Convert.ToInt32(dsCDCRep.Tables[41].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[45].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[49].Rows[0][0].ToString());
                int ColE70 = Convert.ToInt32(dsCDCRep.Tables[42].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[46].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[50].Rows[0][0].ToString());
                int ColF70 = Convert.ToInt32(dsCDCRep.Tables[43].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[47].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[51].Rows[0][0].ToString());

                int ColE81 = Convert.ToInt32(dsCDCRep.Tables[60].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[64].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[68].Rows[0][0].ToString());
                int ColF81 = Convert.ToInt32(dsCDCRep.Tables[61].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[65].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[69].Rows[0][0].ToString());
                int ColE82 = Convert.ToInt32(dsCDCRep.Tables[62].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[66].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[70].Rows[0][0].ToString());
                int ColF82 = Convert.ToInt32(dsCDCRep.Tables[63].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[67].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[71].Rows[0][0].ToString());
                int colE3 = 0;
                int colF3 = 0;
                int colE4 = 0;
                int colF4 = 0;
                int colE8 = 0;
                int colF8 = 0;
                int colE9 = 0;
                int colF9 = 0;
                int colE10 = 0;
                int colF10 = 0;
                int colE11 = 0;
                int colF11 = 0;
                int colE30 = 0;
                int colF30 = 0;
                int colE31 = 0;
                int colF31 = 0;
                int colE32 = 0;
                int colF32 = 0;
                int colE33 = 0;
                int colF33 = 0;

                foreach (DataRow dr in dsCDCRep.Tables[4].Rows)
                {
                    colE3 = colE3 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[5].Rows)
                {
                    colF3 = colF3 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[6].Rows)
                {
                    colE4 = colE4 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[7].Rows)
                {
                    colF4 = colF4 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[20].Rows)
                {
                    colE8 = colE8 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[21].Rows)
                {
                    colF8 = colF8 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[22].Rows)
                {
                    colE9 = colE9 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[23].Rows)
                {
                    colF9 = colF9 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[24].Rows)
                {
                    colE10 = colE10 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[25].Rows)
                {
                    colF10 = colF10 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[26].Rows)
                {
                    colE11 = colE11 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[27].Rows)
                {
                    colF11 = colF11 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[64].Rows)
                {
                    colE30 = colE30 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[65].Rows)
                {
                    colF30 = colF30 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[66].Rows)
                {
                    colE31 = colE31 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[67].Rows)
                {
                    colF31 = colF31 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[68].Rows)
                {
                    colE32 = colE32 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[69].Rows)
                {
                    colF32 = colF32 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[70].Rows)
                {
                    colE33 = colE33 + Convert.ToInt16(dr[0]);
                }
                foreach (DataRow dr in dsCDCRep.Tables[71].Rows)
                {
                    colF33 = colF33 + Convert.ToInt16(dr[0]);
                }
                int ColG33 = colE3 + colF3;
                int ColG34 = colE4 + colF4;
                int ColG35 = Convert.ToInt32(dsCDCRep.Tables[8].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[9].Rows[0][0].ToString());
                int ColG36 = Convert.ToInt32(dsCDCRep.Tables[10].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[11].Rows[0][0].ToString());
                int ColG37 = Convert.ToInt32(dsCDCRep.Tables[12].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[13].Rows[0][0].ToString());
                int ColG38 = Convert.ToInt32(dsCDCRep.Tables[14].Rows[0][0].ToString()) + Convert.ToInt32(dsCDCRep.Tables[15].Rows[0][0].ToString());

                writeInCell(thesheet, "B1", "", "MR REPORT");
                writeInCell(thesheet, "A2", "", "Facility : " + Session["AppLocation"].ToString());
                writeInCell(thesheet, "A3", "", "From Date : " + theStartDate.ToString() + "  " + "To date :" + theEndDate.ToString());
                writeInCell(thesheet, "E14", "Table", "Column_E1");
                writeInCell(thesheet, "F14", "Table1", "Column_F1");
                writeInCell(thesheet, "E15", "Table2", "Column_E2");
                writeInCell(thesheet, "F15", "Table3", "Column_F2");
                writeInCell(thesheet, "E33", "", colE3.ToString());
                writeInCell(thesheet, "F33", "", colF3.ToString());
                writeInCell(thesheet, "G33", "", colE3.ToString());
                writeInCell(thesheet, "E34", "", colE4.ToString());
                writeInCell(thesheet, "F34", "", colF4.ToString());
                writeInCell(thesheet, "G34", "", colE4.ToString());
                writeInCell(thesheet, "E35", "Table8", "Column_E5a");
                writeInCell(thesheet, "F35", "Table9", "Column_F5a");
                writeInCell(thesheet, "G35", "Table8", "Column_E5a");
                writeInCell(thesheet, "E36", "Table10", "Column_E5b");
                writeInCell(thesheet, "F36", "Table11", "Column_F5b");
                writeInCell(thesheet, "G36", "Table10", "Column_E5b");
                writeInCell(thesheet, "E37", "Table12", "Column_E5c");
                writeInCell(thesheet, "F37", "Table13", "Column_F5c");
                writeInCell(thesheet, "G37", "Table12", "Column_E5c");
                writeInCell(thesheet, "E38", "Table14", "Column_E5d");
                writeInCell(thesheet, "F38", "Table15", "Column_F5d");
                writeInCell(thesheet, "G38", "Table14", "Column_E5d");
                writeInCell(thesheet, "E52", "", ColE52.ToString());
                writeInCell(thesheet, "F52", "", ColF52.ToString());
                writeInCell(thesheet, "E53", "", ColE53.ToString());
                writeInCell(thesheet, "F53", "", ColF53.ToString());

                writeInCell(thesheet, "E54", "Table16", "Column_E6");
                writeInCell(thesheet, "F54", "Table17", "Column_F6");
                writeInCell(thesheet, "E55", "Table18", "Column_E7");
                writeInCell(thesheet, "F55", "Table19", "Column_F7");
                writeInCell(thesheet, "E56", "", colE8.ToString());
                writeInCell(thesheet, "F56", "", colF8.ToString());
                writeInCell(thesheet, "E57", "", colE9.ToString());
                writeInCell(thesheet, "F57", "", colF9.ToString());
                writeInCell(thesheet, "E58", "", colE10.ToString());
                writeInCell(thesheet, "F58", "", colF10.ToString());
                writeInCell(thesheet, "E59", "", colE11.ToString());
                writeInCell(thesheet, "F59", "", colF11.ToString());
                writeInCell(thesheet, "E60", "Table28", "Column_E12");
                writeInCell(thesheet, "F60", "Table29", "Column_F12");
                writeInCell(thesheet, "E61", "Table30", "Column_E13");
                writeInCell(thesheet, "F61", "Table31", "Column_F13");
                writeInCell(thesheet, "E62", "Table32", "Column_E14");
                writeInCell(thesheet, "F62", "Table33", "Column_F14");
                writeInCell(thesheet, "E63", "Table34", "Column_E15");
                writeInCell(thesheet, "F63", "Table35", "Column_F15");
                writeInCell(thesheet, "E67", "Table36", "Column_E16");
                writeInCell(thesheet, "F67", "Table37", "Column_F16");
                writeInCell(thesheet, "E68", "Table38", "Column_E17");
                writeInCell(thesheet, "F68", "Table39", "Column_F17");
                writeInCell(thesheet, "E69", "", ColE69.ToString());
                writeInCell(thesheet, "F69", "", ColF69.ToString());
                writeInCell(thesheet, "E70", "", ColE70.ToString());
                writeInCell(thesheet, "F70", "", ColF70.ToString());
                writeInCell(thesheet, "E71", "Table40", "Column_E18");
                writeInCell(thesheet, "F71", "Table41", "Column_F18");
                writeInCell(thesheet, "E72", "Table42", "Column_E19");
                writeInCell(thesheet, "F72", "Table43", "Column_F19");
                writeInCell(thesheet, "E73", "Table44", "Column_E20");
                writeInCell(thesheet, "F73", "Table45", "Column_F20");
                writeInCell(thesheet, "E74", "Table46", "Column_E21");
                writeInCell(thesheet, "F74", "Table47", "Column_F21");
                writeInCell(thesheet, "E75", "Table48", "Column_E22");
                writeInCell(thesheet, "F75", "Table49", "Column_F22");
                writeInCell(thesheet, "E76", "Table50", "Column_E23");
                writeInCell(thesheet, "F76", "Table51", "Column_F23");
                writeInCell(thesheet, "E77", "Table52", "Column_E24");
                writeInCell(thesheet, "F77", "Table53", "Column_F24");
                writeInCell(thesheet, "E78", "Table54", "Column_E25");
                writeInCell(thesheet, "F78", "Table55", "Column_F25");
                writeInCell(thesheet, "E79", "Table56", "Column_E26");
                writeInCell(thesheet, "F79", "Table57", "Column_F26");
                writeInCell(thesheet, "E80", "Table58", "Column_E27");
                writeInCell(thesheet, "F80", "Table59", "Column_F27");
                writeInCell(thesheet, "E81", "", ColE81.ToString());
                writeInCell(thesheet, "F81", "", ColF81.ToString());
                writeInCell(thesheet, "E82", "", ColE82.ToString());
                writeInCell(thesheet, "F82", "", ColF82.ToString());
                writeInCell(thesheet, "E83", "Table60", "Column_E28");
                writeInCell(thesheet, "F83", "Table61", "Column_F28");
                writeInCell(thesheet, "E84", "Table62", "Column_E29");
                writeInCell(thesheet, "F84", "Table63", "Column_F29");
                writeInCell(thesheet, "E85", "", colE30.ToString());
                writeInCell(thesheet, "F85", "", colF30.ToString());
                writeInCell(thesheet, "E86", "", colE31.ToString());
                writeInCell(thesheet, "F86", "", colF31.ToString());
                writeInCell(thesheet, "E87", "", colE32.ToString());
                writeInCell(thesheet, "F87", "", colF32.ToString());
                writeInCell(thesheet, "E88", "", colE33.ToString());
                writeInCell(thesheet, "F88", "", colF33.ToString());
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }

        private void writeInCell(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {

            try
            {
                Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
                theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
                //theRange.Borders();

                string theExitvalue = "";
                if (theRange.Value2 != null)
                    theExitvalue = theRange.Value2.ToString();
                else
                    theExitvalue = "";

                if (tablename != "")
                {
                    if (theExitvalue.ToString().Trim() == "")
                        theRange.Value2 = dsCDCRep.Tables[tablename].Rows[0][column].ToString();
                    else
                        theRange.Value2 = theExitvalue + "  " + dsCDCRep.Tables[tablename].Rows[0][column].ToString();
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
            finally
            {

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmReportDonorJump.aspx");
        }

    }
}