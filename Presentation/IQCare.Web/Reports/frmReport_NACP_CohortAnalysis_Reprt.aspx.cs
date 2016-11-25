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
using Application.Presentation;
using Interface.Reports;
using Application.Common;


using Excel = Microsoft.Office.Interop.Owc11;
namespace IQCare.Web.Reports
{
    public partial class NACP_CohortAnalysis_Reprt : System.Web.UI.Page
    {
        DateTime theYear;
        public DataSet theRepDS;
        protected void Init_Form()
        {

            BindFunctions BindManager = new BindFunctions();
            DataTable Month = BindManager.GetMonths();
            BindManager.BindCombo(ddMonth, Month, "Name", "Id");
            BindManager.BindCombo(ddMonthyear, Month, "Name", "Id");



        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                ////(Master.FindControl("lblMark") as Label).Text = "»";
                //(Master.FindControl("lblheader") as Label).Text = "Donor Reports »Cohort Tracking  ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports >> Cohort Tracking";

                Init_Form();
                txtYear.Attributes.Add("onkeyup", "chkNumeric('" + txtYear.ClientID + "')");
                txtyears.Attributes.Add("onkeyup", "chkNumeric('" + txtyears.ClientID + "')");
                rdoMonth.Checked = true;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validation() == true)
            {


                if (rdoMonth.Checked)
                {
                    int years = Convert.ToInt32(txtYear.Text);
                    int Months = Convert.ToInt32(ddMonth.SelectedItem.Value);

                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    theRepDS = (DataSet)ReportDetails.GetNACPCohortMonthlyReport(Months, years, Session["AppLocationId"].ToString());

                    #region "TableNames"
                    theRepDS.Tables[0].TableName = "FacilityInfo";
                    theRepDS.Tables[1].TableName = "Table1Data";
                    theRepDS.Tables[2].TableName = "Table2Data";
                    theRepDS.Tables[3].TableName = "Table3Data";
                    theRepDS.Tables[4].TableName = "Table4Data";
                    theRepDS.Tables[5].TableName = "Table5Data";
                    theRepDS.Tables[6].TableName = "Table6Data";
                    theRepDS.Tables[7].TableName = "Table7Data";
                    theRepDS.Tables[8].TableName = "Table8Data";
                    theRepDS.Tables[9].TableName = "Table9Data";
                    theRepDS.Tables[10].TableName = "Table10Data";
                    theRepDS.Tables[11].TableName = "Table11Data";
                    theRepDS.Tables[12].TableName = "Table12Data";
                    theRepDS.Tables[13].TableName = "Table13Data";
                    theRepDS.Tables[14].TableName = "Table14Data";
                    theRepDS.Tables[15].TableName = "Table15Data";
                    theRepDS.Tables[16].TableName = "Table16Data";
                    theRepDS.Tables[17].TableName = "Table17Data";
                    theRepDS.Tables[18].TableName = "Table18Data";
                    theRepDS.Tables[19].TableName = "Table19Data";
                    theRepDS.Tables[20].TableName = "Table20Data";
                    theRepDS.Tables[21].TableName = "Table21Data";
                    theRepDS.Tables[22].TableName = "Table22Data";
                    theRepDS.Tables[23].TableName = "Table23Data";
                    #endregion


                    //Response.Redirect("..\\ExcelFiles\\TZNACPMonthlyReport.xls");
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\NACPCohortMonthlyreport.xml");
                    theApp.XMLURL = theFilePath;
                    writeCellWiseInExcel(theApp);

                    // theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionOpenInExcel, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportAsAppropriate);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\NACPCohortMonthlyreport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //theUtils.OpenExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Response);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\NACPCohortMonthlyreport.xls"), Response);
                }
                if (rdoQuarter.Checked)
                {
                    int years = Convert.ToInt32(txtyears.Text);
                    int Months = Convert.ToInt32(ddMonthyear.SelectedItem.Value);

                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    theRepDS = (DataSet)ReportDetails.GetNACPSixCohortMonthlyReport(Months, years, Session["AppLocationId"].ToString());

                    theRepDS.Tables[0].TableName = "FacilityInfo";
                    theRepDS.Tables[1].TableName = "Table1Data";
                    theRepDS.Tables[2].TableName = "Table2Data";
                    theRepDS.Tables[3].TableName = "Table3Data";
                    theRepDS.Tables[4].TableName = "Table4Data";
                    theRepDS.Tables[5].TableName = "Table5Data";
                    theRepDS.Tables[6].TableName = "Table6Data";
                    theRepDS.Tables[7].TableName = "Table7Data";
                    theRepDS.Tables[8].TableName = "Table8Data";
                    theRepDS.Tables[9].TableName = "Table9Data";
                    theRepDS.Tables[10].TableName = "Table10Data";
                    theRepDS.Tables[11].TableName = "Table11Data";
                    theRepDS.Tables[12].TableName = "Table12Data";
                    theRepDS.Tables[13].TableName = "Table13Data";
                    theRepDS.Tables[14].TableName = "Table14Data";
                    theRepDS.Tables[15].TableName = "Table15Data";
                    theRepDS.Tables[16].TableName = "Table16Data";
                    theRepDS.Tables[17].TableName = "Table17Data";
                    theRepDS.Tables[18].TableName = "Table18Data";
                    theRepDS.Tables[19].TableName = "Table19Data";
                    theRepDS.Tables[20].TableName = "Table20Data";
                    theRepDS.Tables[21].TableName = "Table21Data";
                    theRepDS.Tables[22].TableName = "Table22Data";
                    theRepDS.Tables[23].TableName = "Table23Data";
                    //-----------------
                    theRepDS.Tables[26].TableName = "Table26Data";
                    theRepDS.Tables[27].TableName = "Table27Data";
                    theRepDS.Tables[28].TableName = "Table28Data";
                    theRepDS.Tables[29].TableName = "Table29Data";
                    theRepDS.Tables[30].TableName = "Table30Data";
                    theRepDS.Tables[31].TableName = "Table31Data";
                    theRepDS.Tables[32].TableName = "Table32Data";
                    theRepDS.Tables[33].TableName = "Table33Data";
                    theRepDS.Tables[34].TableName = "Table34Data";
                    theRepDS.Tables[35].TableName = "Table35Data";
                    theRepDS.Tables[36].TableName = "Table36Data";
                    theRepDS.Tables[37].TableName = "Table37Data";
                    theRepDS.Tables[38].TableName = "Table38Data";
                    theRepDS.Tables[39].TableName = "Table39Data";
                    theRepDS.Tables[40].TableName = "Table40Data";
                    theRepDS.Tables[41].TableName = "Table41Data";
                    theRepDS.Tables[42].TableName = "Table42Data";
                    theRepDS.Tables[43].TableName = "Table43Data";
                    theRepDS.Tables[44].TableName = "Table44Data";
                    theRepDS.Tables[45].TableName = "Table45Data";
                    theRepDS.Tables[46].TableName = "Table46Data";
                    theRepDS.Tables[47].TableName = "Table47Data";
                    //-------------------------
                    theRepDS.Tables[51].TableName = "Table51Data";
                    theRepDS.Tables[52].TableName = "Table52Data";
                    theRepDS.Tables[53].TableName = "Table53Data";
                    theRepDS.Tables[54].TableName = "Table54Data";
                    theRepDS.Tables[55].TableName = "Table55Data";
                    theRepDS.Tables[56].TableName = "Table56Data";
                    theRepDS.Tables[57].TableName = "Table57Data";
                    theRepDS.Tables[58].TableName = "Table58Data";
                    theRepDS.Tables[59].TableName = "Table59Data";
                    theRepDS.Tables[60].TableName = "Table60Data";
                    theRepDS.Tables[61].TableName = "Table61Data";
                    theRepDS.Tables[62].TableName = "Table62Data";
                    theRepDS.Tables[63].TableName = "Table63Data";
                    theRepDS.Tables[64].TableName = "Table64Data";
                    theRepDS.Tables[65].TableName = "Table65Data";
                    theRepDS.Tables[66].TableName = "Table66Data";
                    theRepDS.Tables[67].TableName = "Table67Data";
                    theRepDS.Tables[68].TableName = "Table68Data";
                    theRepDS.Tables[69].TableName = "Table69Data";
                    theRepDS.Tables[70].TableName = "Table70Data";
                    theRepDS.Tables[71].TableName = "Table71Data";
                    theRepDS.Tables[72].TableName = "Table72Data";
                    //
                    theRepDS.Tables[76].TableName = "Table76Data";
                    theRepDS.Tables[77].TableName = "Table77Data";
                    theRepDS.Tables[78].TableName = "Table78Data";
                    theRepDS.Tables[79].TableName = "Table79Data";
                    theRepDS.Tables[80].TableName = "Table80Data";
                    theRepDS.Tables[81].TableName = "Table81Data";
                    theRepDS.Tables[82].TableName = "Table82Data";
                    theRepDS.Tables[83].TableName = "Table83Data";
                    theRepDS.Tables[84].TableName = "Table84Data";
                    theRepDS.Tables[85].TableName = "Table85Data";
                    theRepDS.Tables[86].TableName = "Table86Data";
                    theRepDS.Tables[87].TableName = "Table87Data";
                    theRepDS.Tables[88].TableName = "Table88Data";
                    theRepDS.Tables[89].TableName = "Table89Data";
                    theRepDS.Tables[90].TableName = "Table90Data";
                    theRepDS.Tables[91].TableName = "Table91Data";
                    theRepDS.Tables[92].TableName = "Table92Data";
                    theRepDS.Tables[93].TableName = "Table93Data";
                    theRepDS.Tables[94].TableName = "Table94Data";
                    theRepDS.Tables[95].TableName = "Table95Data";
                    theRepDS.Tables[96].TableName = "Table96Data";
                    theRepDS.Tables[97].TableName = "Table97Data";
                    //
                    theRepDS.Tables[101].TableName = "Table101Data";
                    theRepDS.Tables[102].TableName = "Table102Data";
                    theRepDS.Tables[103].TableName = "Table103Data";
                    theRepDS.Tables[104].TableName = "Table104Data";
                    theRepDS.Tables[105].TableName = "Table105Data";
                    theRepDS.Tables[106].TableName = "Table106Data";
                    theRepDS.Tables[107].TableName = "Table107Data";
                    theRepDS.Tables[108].TableName = "Table108Data";
                    theRepDS.Tables[109].TableName = "Table109Data";
                    theRepDS.Tables[110].TableName = "Table110Data";
                    theRepDS.Tables[111].TableName = "Table111Data";
                    theRepDS.Tables[112].TableName = "Table112Data";
                    theRepDS.Tables[113].TableName = "Table113Data";
                    theRepDS.Tables[114].TableName = "Table114Data";
                    theRepDS.Tables[115].TableName = "Table115Data";
                    theRepDS.Tables[116].TableName = "Table116Data";
                    theRepDS.Tables[117].TableName = "Table117Data";
                    theRepDS.Tables[118].TableName = "Table118Data";
                    theRepDS.Tables[119].TableName = "Table119Data";
                    theRepDS.Tables[120].TableName = "Table120Data";
                    theRepDS.Tables[121].TableName = "Table121Data";
                    theRepDS.Tables[122].TableName = "Table122Data";

                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\NACPCohortSixMonthlyreport.xml");
                    theApp.XMLURL = theFilePath;
                    writeCellWiseInExcelSixCohort(theApp);


                    theApp.Export(Server.MapPath("..\\ExcelFiles\\NACPCohortSixMonthlyreport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);

                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\NACPCohortSixMonthlyreport.xls"), Response);

                }
            }
        }
        private void writeCellWiseInExcel(Excel.Spreadsheet theSheet)
        {
            try
            {
                if ((ddMonth.SelectedValue.ToString() != "0") && (txtYear.Text.Trim() != ""))
                {


                    writeInCell(theSheet, "F1", "FacilityInfo", "FacilityName");
                    writeInCell(theSheet, "D2", "", "National Care and Treatment programme cohort analysis Form");
                    //set column name
                    writeInCell(theSheet, "C3", theRepDS.Tables[23].TableName.ToString(), "BaseDate");
                    writeInCell(theSheet, "D3", theRepDS.Tables[23].TableName.ToString(), "EndDate_6mo");
                    writeInCell(theSheet, "E3", theRepDS.Tables[23].TableName.ToString(), "EndDate_12mo");
                    writeInCell(theSheet, "F3", theRepDS.Tables[23].TableName.ToString(), "EndDate_24mo");
                    writeInCell(theSheet, "G3", theRepDS.Tables[23].TableName.ToString(), "EndDate_36mo");
                    writeInCell(theSheet, "H3", theRepDS.Tables[23].TableName.ToString(), "EndDate_48mo");
                    writeInCell(theSheet, "I3", theRepDS.Tables[23].TableName.ToString(), "EndDate_60mo");
                    writeInCell(theSheet, "J3", theRepDS.Tables[23].TableName.ToString(), "EndDate_72mo");
                    //Started on ART in this clinic original cohort
                    writeInCell(theSheet, "A5", "", "G");
                    writeInCell(theSheet, "B5", "", "Started on ART in this clinic original cohort");
                    writeInCell(theSheet, "C5", theRepDS.Tables[1].TableName.ToString(), "G_BaseMonth");
                    writeInCell(theSheet, "D5", theRepDS.Tables[1].TableName.ToString(), "G_1");
                    writeInCell(theSheet, "E5", theRepDS.Tables[1].TableName.ToString(), "G_2");
                    writeInCell(theSheet, "F5", theRepDS.Tables[1].TableName.ToString(), "G_3");
                    writeInCell(theSheet, "G5", theRepDS.Tables[1].TableName.ToString(), "G_4");
                    writeInCell(theSheet, "H5", theRepDS.Tables[1].TableName.ToString(), "G_5");
                    writeInCell(theSheet, "I5", theRepDS.Tables[1].TableName.ToString(), "G_6");
                    writeInCell(theSheet, "J5", theRepDS.Tables[1].TableName.ToString(), "G_7");
                    //Transfers in
                    writeInCell(theSheet, "A6", "", "TI");
                    writeInCell(theSheet, "B6", "", "Transfers in");
                    writeInCell(theSheet, "C6", theRepDS.Tables[2].TableName.ToString(), "TI_BaseMonth");
                    writeInCell(theSheet, "D6", theRepDS.Tables[2].TableName.ToString(), "TI_1");
                    writeInCell(theSheet, "E6", theRepDS.Tables[2].TableName.ToString(), "TI_2");
                    writeInCell(theSheet, "F6", theRepDS.Tables[2].TableName.ToString(), "TI_3");
                    writeInCell(theSheet, "G6", theRepDS.Tables[2].TableName.ToString(), "TI_4");
                    writeInCell(theSheet, "H6", theRepDS.Tables[2].TableName.ToString(), "TI_5");
                    writeInCell(theSheet, "I6", theRepDS.Tables[2].TableName.ToString(), "TI_6");
                    writeInCell(theSheet, "J6", theRepDS.Tables[2].TableName.ToString(), "TI_7");
                    //Transfers out
                    writeInCell(theSheet, "A7", "", "TO");
                    writeInCell(theSheet, "B7", "", "Transfers out");
                    writeInCell(theSheet, "C7", theRepDS.Tables[3].TableName.ToString(), "TO_BaseMonth");
                    writeInCell(theSheet, "D7", theRepDS.Tables[3].TableName.ToString(), "TO_1");
                    writeInCell(theSheet, "E7", theRepDS.Tables[3].TableName.ToString(), "TO_2");
                    writeInCell(theSheet, "F7", theRepDS.Tables[3].TableName.ToString(), "TO_3");
                    writeInCell(theSheet, "G7", theRepDS.Tables[3].TableName.ToString(), "TO_4");
                    writeInCell(theSheet, "H7", theRepDS.Tables[3].TableName.ToString(), "TO_5");
                    writeInCell(theSheet, "I7", theRepDS.Tables[3].TableName.ToString(), "TO_6");
                    writeInCell(theSheet, "J7", theRepDS.Tables[3].TableName.ToString(), "TO_7");
                    //Net current cohort
                    writeInCell(theSheet, "A8", "", "N");
                    writeInCell(theSheet, "B8", "", "Net current cohort");
                    writeInCell(theSheet, "C8", theRepDS.Tables[4].TableName.ToString(), "N_BaseMonth");
                    writeInCell(theSheet, "D8", theRepDS.Tables[4].TableName.ToString(), "N_1");
                    writeInCell(theSheet, "E8", theRepDS.Tables[4].TableName.ToString(), "N_2");
                    writeInCell(theSheet, "F8", theRepDS.Tables[4].TableName.ToString(), "N_3");
                    writeInCell(theSheet, "G8", theRepDS.Tables[4].TableName.ToString(), "N_4");
                    writeInCell(theSheet, "H8", theRepDS.Tables[4].TableName.ToString(), "N_5");
                    writeInCell(theSheet, "I8", theRepDS.Tables[4].TableName.ToString(), "N_6");
                    writeInCell(theSheet, "J8", theRepDS.Tables[4].TableName.ToString(), "N_7");
                    // On original first line regimen
                    writeInCell(theSheet, "A9", "", "H");
                    writeInCell(theSheet, "B9", "", "On original 1st line regimen");
                    writeInCell(theSheet, "C9", theRepDS.Tables[5].TableName.ToString(), "H_BaseMonth");
                    writeInCell(theSheet, "D9", theRepDS.Tables[5].TableName.ToString(), "H_1");
                    writeInCell(theSheet, "E9", theRepDS.Tables[5].TableName.ToString(), "H_2");
                    writeInCell(theSheet, "F9", theRepDS.Tables[5].TableName.ToString(), "H_3");
                    writeInCell(theSheet, "G9", theRepDS.Tables[5].TableName.ToString(), "H_4");
                    writeInCell(theSheet, "H9", theRepDS.Tables[5].TableName.ToString(), "H_5");
                    writeInCell(theSheet, "I9", theRepDS.Tables[5].TableName.ToString(), "H_6");
                    writeInCell(theSheet, "J9", theRepDS.Tables[5].TableName.ToString(), "H_7");
                    //On alternate 1st line regimen(substituted)
                    writeInCell(theSheet, "A10", "", "I");
                    writeInCell(theSheet, "B10", "", "On alternate 1st line regimen(substituted)");
                    writeInCell(theSheet, "C10", theRepDS.Tables[6].TableName.ToString(), "I_BaseMonth");
                    writeInCell(theSheet, "D10", theRepDS.Tables[6].TableName.ToString(), "I_1");
                    writeInCell(theSheet, "E10", theRepDS.Tables[6].TableName.ToString(), "I_2");
                    writeInCell(theSheet, "F10", theRepDS.Tables[6].TableName.ToString(), "I_3");
                    writeInCell(theSheet, "G10", theRepDS.Tables[6].TableName.ToString(), "I_4");
                    writeInCell(theSheet, "H10", theRepDS.Tables[6].TableName.ToString(), "I_5");
                    writeInCell(theSheet, "I10", theRepDS.Tables[6].TableName.ToString(), "I_6");
                    writeInCell(theSheet, "J10", theRepDS.Tables[6].TableName.ToString(), "I_7");
                    // On 2nd line or other regimen (switched)
                    writeInCell(theSheet, "A11", "", "J");
                    writeInCell(theSheet, "B11", "", "On 2nd line or other regimen(switched)");
                    writeInCell(theSheet, "C11", theRepDS.Tables[7].TableName.ToString(), "J_BaseMonth");
                    writeInCell(theSheet, "D11", theRepDS.Tables[7].TableName.ToString(), "J_1");
                    writeInCell(theSheet, "E11", theRepDS.Tables[7].TableName.ToString(), "J_2");
                    writeInCell(theSheet, "F11", theRepDS.Tables[7].TableName.ToString(), "J_3");
                    writeInCell(theSheet, "G11", theRepDS.Tables[7].TableName.ToString(), "J_4");
                    writeInCell(theSheet, "H11", theRepDS.Tables[7].TableName.ToString(), "J_5");
                    writeInCell(theSheet, "I11", theRepDS.Tables[7].TableName.ToString(), "J_6");
                    writeInCell(theSheet, "J11", theRepDS.Tables[7].TableName.ToString(), "J_7");
                    //Current regimen unrecorded
                    writeInCell(theSheet, "A12", "", "");
                    writeInCell(theSheet, "B12", "", "Current regimen unrecorded");
                    writeInCell(theSheet, "C12", theRepDS.Tables[8].TableName.ToString(), "UN_BaseMonth");
                    writeInCell(theSheet, "D12", theRepDS.Tables[8].TableName.ToString(), "UN_1");
                    writeInCell(theSheet, "E12", theRepDS.Tables[8].TableName.ToString(), "UN_2");
                    writeInCell(theSheet, "F12", theRepDS.Tables[8].TableName.ToString(), "UN_3");
                    writeInCell(theSheet, "G12", theRepDS.Tables[8].TableName.ToString(), "UN_4");
                    writeInCell(theSheet, "H12", theRepDS.Tables[8].TableName.ToString(), "UN_5");
                    writeInCell(theSheet, "I12", theRepDS.Tables[8].TableName.ToString(), "UN_6");
                    writeInCell(theSheet, "J12", theRepDS.Tables[8].TableName.ToString(), "UN_7");
                    //Stopped/Current ARV status unrecorded
                    writeInCell(theSheet, "A14", "", "");
                    writeInCell(theSheet, "B14", "", "Stopped/Current ARV status unrecorded");
                    writeInCell(theSheet, "C14", theRepDS.Tables[9].TableName.ToString(), "S_BaseMonth");
                    writeInCell(theSheet, "D14", theRepDS.Tables[9].TableName.ToString(), "S_1");
                    writeInCell(theSheet, "E14", theRepDS.Tables[9].TableName.ToString(), "S_2");
                    writeInCell(theSheet, "F14", theRepDS.Tables[9].TableName.ToString(), "S_3");
                    writeInCell(theSheet, "G14", theRepDS.Tables[9].TableName.ToString(), "S_4");
                    writeInCell(theSheet, "H14", theRepDS.Tables[9].TableName.ToString(), "S_5");
                    writeInCell(theSheet, "I14", theRepDS.Tables[9].TableName.ToString(), "S_6");
                    writeInCell(theSheet, "J14", theRepDS.Tables[9].TableName.ToString(), "S_7");
                    //Died
                    writeInCell(theSheet, "A15", "", "");
                    writeInCell(theSheet, "B15", "", "Died");
                    writeInCell(theSheet, "C15", theRepDS.Tables[10].TableName.ToString(), "D_BaseMonth");
                    writeInCell(theSheet, "D15", theRepDS.Tables[10].TableName.ToString(), "D_1");
                    writeInCell(theSheet, "E15", theRepDS.Tables[10].TableName.ToString(), "D_2");
                    writeInCell(theSheet, "F15", theRepDS.Tables[10].TableName.ToString(), "D_3");
                    writeInCell(theSheet, "G15", theRepDS.Tables[10].TableName.ToString(), "D_4");
                    writeInCell(theSheet, "H15", theRepDS.Tables[10].TableName.ToString(), "D_5");
                    writeInCell(theSheet, "I15", theRepDS.Tables[10].TableName.ToString(), "D_6");
                    writeInCell(theSheet, "J15", theRepDS.Tables[10].TableName.ToString(), "D_7");
                    //Unkonwn
                    writeInCell(theSheet, "A16", "", "");
                    writeInCell(theSheet, "B16", "", "Unknown");
                    writeInCell(theSheet, "C16", theRepDS.Tables[11].TableName.ToString(), "U_BaseMonth");
                    writeInCell(theSheet, "D16", theRepDS.Tables[11].TableName.ToString(), "U_1");
                    writeInCell(theSheet, "E16", theRepDS.Tables[11].TableName.ToString(), "U_2");
                    writeInCell(theSheet, "F16", theRepDS.Tables[11].TableName.ToString(), "U_3");
                    writeInCell(theSheet, "G16", theRepDS.Tables[11].TableName.ToString(), "U_4");
                    writeInCell(theSheet, "H16", theRepDS.Tables[11].TableName.ToString(), "U_5");
                    writeInCell(theSheet, "I16", theRepDS.Tables[11].TableName.ToString(), "U_6");
                    writeInCell(theSheet, "J16", theRepDS.Tables[11].TableName.ToString(), "U_7");
                    //Lost to follwo up/did not visit for three months
                    writeInCell(theSheet, "A17", "", "");
                    writeInCell(theSheet, "B17", "", "Lost to follow up/did not visit during months");
                    writeInCell(theSheet, "C17", theRepDS.Tables[12].TableName.ToString(), "L_BaseMonth");
                    writeInCell(theSheet, "D17", theRepDS.Tables[12].TableName.ToString(), "L_1");
                    writeInCell(theSheet, "E17", theRepDS.Tables[12].TableName.ToString(), "L_2");
                    writeInCell(theSheet, "F17", theRepDS.Tables[12].TableName.ToString(), "L_3");
                    writeInCell(theSheet, "G17", theRepDS.Tables[12].TableName.ToString(), "L_4");
                    writeInCell(theSheet, "H17", theRepDS.Tables[12].TableName.ToString(), "L_5");
                    writeInCell(theSheet, "I17", theRepDS.Tables[12].TableName.ToString(), "L_6");
                    writeInCell(theSheet, "J17", theRepDS.Tables[12].TableName.ToString(), "L_7");
                    //Number of cohort alive and on ART
                    writeInCell(theSheet, "A19", "", "");
                    writeInCell(theSheet, "B19", "", "Number of cohort alive and on ART");
                    writeInCell(theSheet, "C19", theRepDS.Tables[13].TableName.ToString(), "NAL_BaseMonth");
                    writeInCell(theSheet, "D19", theRepDS.Tables[13].TableName.ToString(), "NAL_1");
                    writeInCell(theSheet, "E19", theRepDS.Tables[13].TableName.ToString(), "NAL_2");
                    writeInCell(theSheet, "F19", theRepDS.Tables[13].TableName.ToString(), "NAL_3");
                    writeInCell(theSheet, "G19", theRepDS.Tables[13].TableName.ToString(), "NAL_4");
                    writeInCell(theSheet, "H19", theRepDS.Tables[13].TableName.ToString(), "NAL_5");
                    writeInCell(theSheet, "I19", theRepDS.Tables[13].TableName.ToString(), "NAL_6");
                    writeInCell(theSheet, "J19", theRepDS.Tables[13].TableName.ToString(), "NAL_7");
                    //Percent of cohort alive and on ART
                    writeInCell(theSheet, "A20", "", "");
                    writeInCell(theSheet, "B20", "", "Percent of cohort alive and on ART");
                    writeInCell(theSheet, "C20", theRepDS.Tables[14].TableName.ToString(), "NALP_BaseMonth");
                    writeInCell(theSheet, "D20", theRepDS.Tables[14].TableName.ToString(), "NALP_1");
                    writeInCell(theSheet, "E20", theRepDS.Tables[14].TableName.ToString(), "NALP_2");
                    writeInCell(theSheet, "F20", theRepDS.Tables[14].TableName.ToString(), "NALP_3");
                    writeInCell(theSheet, "G20", theRepDS.Tables[14].TableName.ToString(), "NALP_4");
                    writeInCell(theSheet, "H20", theRepDS.Tables[14].TableName.ToString(), "NALP_5");
                    writeInCell(theSheet, "I20", theRepDS.Tables[14].TableName.ToString(), "NALP_6");
                    writeInCell(theSheet, "J20", theRepDS.Tables[14].TableName.ToString(), "NALP_7");
                    //Number of patients aged 7 years or more with CD4
                    writeInCell(theSheet, "A22", "", "");
                    writeInCell(theSheet, "B22", "", "Number of patients aged 7 years or more with CD4");
                    writeInCell(theSheet, "C22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCell(theSheet, "D22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_1");
                    writeInCell(theSheet, "E22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_2");
                    writeInCell(theSheet, "F22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_3");
                    writeInCell(theSheet, "G22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_4");
                    writeInCell(theSheet, "H22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_5");
                    writeInCell(theSheet, "I22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_6");
                    writeInCell(theSheet, "J22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_7");
                    //Number with CD4>=200
                    writeInCell(theSheet, "A23", "", "");
                    writeInCell(theSheet, "B23", "", "Number with CD4>=200 ");
                    writeInCell(theSheet, "C23", theRepDS.Tables[16].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCell(theSheet, "D23", theRepDS.Tables[16].TableName.ToString(), "NCD4_1");
                    writeInCell(theSheet, "E23", theRepDS.Tables[16].TableName.ToString(), "NCD4_2");
                    writeInCell(theSheet, "F23", theRepDS.Tables[16].TableName.ToString(), "NCD4_3");
                    writeInCell(theSheet, "G23", theRepDS.Tables[16].TableName.ToString(), "NCD4_4");
                    writeInCell(theSheet, "H23", theRepDS.Tables[16].TableName.ToString(), "NCD4_5");
                    writeInCell(theSheet, "I23", theRepDS.Tables[16].TableName.ToString(), "NCD4_6");
                    writeInCell(theSheet, "J23", theRepDS.Tables[16].TableName.ToString(), "NCD4_7");
                    //Percent with CD4>=200
                    writeInCell(theSheet, "A24", "", "");
                    writeInCell(theSheet, "B24", "", "Percent with CD4>=200");
                    writeInCell(theSheet, "C24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCell(theSheet, "D24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_1");
                    writeInCell(theSheet, "E24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_2");
                    writeInCell(theSheet, "F24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_3");
                    writeInCell(theSheet, "G24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_4");
                    writeInCell(theSheet, "H24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_5");
                    writeInCell(theSheet, "I24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_6");
                    writeInCell(theSheet, "J24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_7");
                    //Number Working
                    writeInCell(theSheet, "A25", "", "");
                    writeInCell(theSheet, "B25", "", "Number Working");
                    writeInCell(theSheet, "C25", theRepDS.Tables[18].TableName.ToString(), "NW_BaseMonth");
                    writeInCell(theSheet, "D25", theRepDS.Tables[18].TableName.ToString(), "NW_1");
                    writeInCell(theSheet, "E25", theRepDS.Tables[18].TableName.ToString(), "NW_2");
                    writeInCell(theSheet, "F25", theRepDS.Tables[18].TableName.ToString(), "NW_3");
                    writeInCell(theSheet, "G25", theRepDS.Tables[18].TableName.ToString(), "NW_4");
                    writeInCell(theSheet, "H25", theRepDS.Tables[18].TableName.ToString(), "NW_5");
                    writeInCell(theSheet, "I25", theRepDS.Tables[18].TableName.ToString(), "NW_6");
                    writeInCell(theSheet, "J25", theRepDS.Tables[18].TableName.ToString(), "NW_7");
                    //Number Ambulatory
                    writeInCell(theSheet, "A26", "", "");
                    writeInCell(theSheet, "B26", "", "Number Ambulatory");
                    writeInCell(theSheet, "C26", theRepDS.Tables[19].TableName.ToString(), "NA_BaseMonth");
                    writeInCell(theSheet, "D26", theRepDS.Tables[19].TableName.ToString(), "NA_1");
                    writeInCell(theSheet, "E26", theRepDS.Tables[19].TableName.ToString(), "NA_2");
                    writeInCell(theSheet, "F26", theRepDS.Tables[19].TableName.ToString(), "NA_3");
                    writeInCell(theSheet, "G26", theRepDS.Tables[19].TableName.ToString(), "NA_4");
                    writeInCell(theSheet, "H26", theRepDS.Tables[19].TableName.ToString(), "NA_5");
                    writeInCell(theSheet, "I26", theRepDS.Tables[19].TableName.ToString(), "NA_6");
                    writeInCell(theSheet, "J26", theRepDS.Tables[19].TableName.ToString(), "NA_7");
                    //Number Bedridden
                    writeInCell(theSheet, "A27", "", "");
                    writeInCell(theSheet, "B27", "", "Number Bedridden");
                    writeInCell(theSheet, "C27", theRepDS.Tables[20].TableName.ToString(), "NB_BaseMonth");
                    writeInCell(theSheet, "D27", theRepDS.Tables[20].TableName.ToString(), "NB_1");
                    writeInCell(theSheet, "E27", theRepDS.Tables[20].TableName.ToString(), "NB_2");
                    writeInCell(theSheet, "F27", theRepDS.Tables[20].TableName.ToString(), "NB_3");
                    writeInCell(theSheet, "G27", theRepDS.Tables[20].TableName.ToString(), "NB_4");
                    writeInCell(theSheet, "H27", theRepDS.Tables[20].TableName.ToString(), "NB_5");
                    writeInCell(theSheet, "I27", theRepDS.Tables[20].TableName.ToString(), "NB_6");
                    writeInCell(theSheet, "J27", theRepDS.Tables[20].TableName.ToString(), "NB_7");
                    //Number functional status unrecorded
                    writeInCell(theSheet, "A28", "", "");
                    writeInCell(theSheet, "B28", "", "Number functional status unrecorded");
                    writeInCell(theSheet, "C28", theRepDS.Tables[21].TableName.ToString(), "NU_BaseMonth");
                    writeInCell(theSheet, "D28", theRepDS.Tables[21].TableName.ToString(), "NU_1");
                    writeInCell(theSheet, "E28", theRepDS.Tables[21].TableName.ToString(), "NU_2");
                    writeInCell(theSheet, "F28", theRepDS.Tables[21].TableName.ToString(), "NU_3");
                    writeInCell(theSheet, "G28", theRepDS.Tables[21].TableName.ToString(), "NU_4");
                    writeInCell(theSheet, "H28", theRepDS.Tables[21].TableName.ToString(), "NU_5");
                    writeInCell(theSheet, "I28", theRepDS.Tables[21].TableName.ToString(), "NU_6");
                    writeInCell(theSheet, "J28", theRepDS.Tables[21].TableName.ToString(), "NU_7");
                    //Number who visited and were recorded on ARVs as many times as there are months
                    writeInCell(theSheet, "A30", "", "");
                    writeInCell(theSheet, "B30", "", "Number who visited and were recorded on ARVs as many times as there are months");
                    writeInCell(theSheet, "C30", theRepDS.Tables[22].TableName.ToString(), "NM_BaseMonth");
                    writeInCell(theSheet, "D30", theRepDS.Tables[22].TableName.ToString(), "NM_1");
                    writeInCell(theSheet, "E30", theRepDS.Tables[22].TableName.ToString(), "NM_2");
                    writeInCell(theSheet, "F30", theRepDS.Tables[22].TableName.ToString(), "NM_3");
                    writeInCell(theSheet, "G30", theRepDS.Tables[22].TableName.ToString(), "NM_4");
                    writeInCell(theSheet, "H30", theRepDS.Tables[22].TableName.ToString(), "NM_5");
                    writeInCell(theSheet, "I30", theRepDS.Tables[22].TableName.ToString(), "NM_6");
                    writeInCell(theSheet, "J30", theRepDS.Tables[22].TableName.ToString(), "NM_7");







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
        private void writeCellWiseInExcelSixCohort(Excel.Spreadsheet theSheet)
        {
            try
            {
                if ((ddMonthyear.SelectedValue.ToString() != "0") && (txtyears.Text.Trim() != ""))
                {


                    writeInCells(theSheet, "F1", "FacilityInfo", "FacilityName");
                    writeInCells(theSheet, "D2", "", "National Care and Treatment programme cohort analysis Form");
                    //set column name
                    writeInCells(theSheet, "C3", theRepDS.Tables[23].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "D3", theRepDS.Tables[23].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "E3", theRepDS.Tables[23].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "F3", theRepDS.Tables[23].TableName.ToString(), "EndDate_24mo");
                    //--
                    writeInCells(theSheet, "G3", theRepDS.Tables[48].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "H3", theRepDS.Tables[48].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "I3", theRepDS.Tables[48].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "J3", theRepDS.Tables[48].TableName.ToString(), "EndDate_24mo");
                    //--
                    writeInCells(theSheet, "K3", theRepDS.Tables[73].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "L3", theRepDS.Tables[73].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "M3", theRepDS.Tables[73].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "N3", theRepDS.Tables[73].TableName.ToString(), "EndDate_24mo");
                    //
                    writeInCells(theSheet, "O3", theRepDS.Tables[98].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "P3", theRepDS.Tables[98].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "Q3", theRepDS.Tables[98].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "R3", theRepDS.Tables[98].TableName.ToString(), "EndDate_24mo");
                    //
                    writeInCells(theSheet, "S3", theRepDS.Tables[123].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "T3", theRepDS.Tables[123].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "U3", theRepDS.Tables[123].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "V3", theRepDS.Tables[123].TableName.ToString(), "EndDate_24mo");

                    //
                    writeInCells(theSheet, "W3", theRepDS.Tables[148].TableName.ToString(), "BaseDate");
                    writeInCells(theSheet, "X3", theRepDS.Tables[148].TableName.ToString(), "EndDate_6mo");
                    writeInCells(theSheet, "Y3", theRepDS.Tables[148].TableName.ToString(), "EndDate_12mo");
                    writeInCells(theSheet, "Z3", theRepDS.Tables[148].TableName.ToString(), "EndDate_24mo");

                    //Started on ART in this clinic original cohort
                    writeInCells(theSheet, "A5", "", "G");
                    writeInCells(theSheet, "B5", "", "Started on ART in this clinic original cohort");
                    writeInCells(theSheet, "C5", theRepDS.Tables[1].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "D5", theRepDS.Tables[1].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "E5", theRepDS.Tables[1].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "F5", theRepDS.Tables[1].TableName.ToString(), "G_3");
                    //--
                    writeInCells(theSheet, "G5", theRepDS.Tables[26].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "H5", theRepDS.Tables[26].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "I5", theRepDS.Tables[26].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "J5", theRepDS.Tables[26].TableName.ToString(), "G_3");
                    //-
                    writeInCells(theSheet, "K5", theRepDS.Tables[51].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "L5", theRepDS.Tables[51].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "M5", theRepDS.Tables[51].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "N5", theRepDS.Tables[51].TableName.ToString(), "G_3");
                    //
                    writeInCells(theSheet, "O5", theRepDS.Tables[76].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "P5", theRepDS.Tables[76].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "Q5", theRepDS.Tables[76].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "R5", theRepDS.Tables[76].TableName.ToString(), "G_3");
                    //
                    writeInCells(theSheet, "S5", theRepDS.Tables[101].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "T5", theRepDS.Tables[101].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "U5", theRepDS.Tables[101].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "V5", theRepDS.Tables[101].TableName.ToString(), "G_3");
                    //
                    writeInCells(theSheet, "W5", theRepDS.Tables[126].TableName.ToString(), "G_BaseMonth");
                    writeInCells(theSheet, "X5", theRepDS.Tables[126].TableName.ToString(), "G_1");
                    writeInCells(theSheet, "Y5", theRepDS.Tables[126].TableName.ToString(), "G_2");
                    writeInCells(theSheet, "Z5", theRepDS.Tables[126].TableName.ToString(), "G_3");




                    //Transfers in
                    writeInCells(theSheet, "A6", "", "TI");
                    writeInCells(theSheet, "B6", "", "Transfers in");
                    writeInCells(theSheet, "C6", theRepDS.Tables[2].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "D6", theRepDS.Tables[2].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "E6", theRepDS.Tables[2].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "F6", theRepDS.Tables[2].TableName.ToString(), "TI_3");
                    //--
                    writeInCells(theSheet, "G6", theRepDS.Tables[27].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "H6", theRepDS.Tables[27].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "I6", theRepDS.Tables[27].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "J6", theRepDS.Tables[27].TableName.ToString(), "TI_3");
                    //-
                    writeInCells(theSheet, "K6", theRepDS.Tables[52].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "L6", theRepDS.Tables[52].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "M6", theRepDS.Tables[52].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "N6", theRepDS.Tables[52].TableName.ToString(), "TI_3");
                    //-
                    writeInCells(theSheet, "O6", theRepDS.Tables[77].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "P6", theRepDS.Tables[77].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "Q6", theRepDS.Tables[77].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "R6", theRepDS.Tables[77].TableName.ToString(), "TI_3");
                    //-
                    writeInCells(theSheet, "S6", theRepDS.Tables[102].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "T6", theRepDS.Tables[102].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "U6", theRepDS.Tables[102].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "V6", theRepDS.Tables[102].TableName.ToString(), "TI_3");
                    //-
                    writeInCells(theSheet, "W6", theRepDS.Tables[127].TableName.ToString(), "TI_BaseMonth");
                    writeInCells(theSheet, "X6", theRepDS.Tables[127].TableName.ToString(), "TI_1");
                    writeInCells(theSheet, "Y6", theRepDS.Tables[127].TableName.ToString(), "TI_2");
                    writeInCells(theSheet, "Z6", theRepDS.Tables[127].TableName.ToString(), "TI_3");



                    //Transfers out
                    writeInCells(theSheet, "A7", "", "TO");
                    writeInCells(theSheet, "B7", "", "Transfers out");
                    writeInCells(theSheet, "C7", theRepDS.Tables[3].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "D7", theRepDS.Tables[3].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "E7", theRepDS.Tables[3].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "F7", theRepDS.Tables[3].TableName.ToString(), "TO_3");
                    //---
                    writeInCells(theSheet, "G7", theRepDS.Tables[28].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "H7", theRepDS.Tables[28].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "I7", theRepDS.Tables[28].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "J7", theRepDS.Tables[28].TableName.ToString(), "TO_3");
                    //-
                    writeInCells(theSheet, "K7", theRepDS.Tables[53].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "L7", theRepDS.Tables[53].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "M7", theRepDS.Tables[53].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "N7", theRepDS.Tables[53].TableName.ToString(), "TO_3");
                    //-
                    writeInCells(theSheet, "O7", theRepDS.Tables[78].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "P7", theRepDS.Tables[78].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "Q7", theRepDS.Tables[78].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "R7", theRepDS.Tables[78].TableName.ToString(), "TO_3");
                    //-
                    writeInCells(theSheet, "S7", theRepDS.Tables[103].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "T7", theRepDS.Tables[103].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "U7", theRepDS.Tables[103].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "V7", theRepDS.Tables[103].TableName.ToString(), "TO_3");
                    //-
                    writeInCells(theSheet, "W7", theRepDS.Tables[128].TableName.ToString(), "TO_BaseMonth");
                    writeInCells(theSheet, "X7", theRepDS.Tables[128].TableName.ToString(), "TO_1");
                    writeInCells(theSheet, "Y7", theRepDS.Tables[128].TableName.ToString(), "TO_2");
                    writeInCells(theSheet, "Z7", theRepDS.Tables[128].TableName.ToString(), "TO_3");

                    //Net current cohort
                    writeInCells(theSheet, "A8", "", "N");
                    writeInCells(theSheet, "B8", "", "Net current cohort");
                    writeInCells(theSheet, "C8", theRepDS.Tables[4].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "D8", theRepDS.Tables[4].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "E8", theRepDS.Tables[4].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "F8", theRepDS.Tables[4].TableName.ToString(), "N_3");
                    //--
                    writeInCells(theSheet, "G8", theRepDS.Tables[29].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "H8", theRepDS.Tables[29].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "I8", theRepDS.Tables[29].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "J8", theRepDS.Tables[29].TableName.ToString(), "N_3");
                    //-
                    writeInCells(theSheet, "K8", theRepDS.Tables[54].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "L8", theRepDS.Tables[54].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "M8", theRepDS.Tables[54].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "N8", theRepDS.Tables[54].TableName.ToString(), "N_3");
                    //-
                    writeInCells(theSheet, "O8", theRepDS.Tables[79].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "P8", theRepDS.Tables[79].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "Q8", theRepDS.Tables[79].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "R8", theRepDS.Tables[79].TableName.ToString(), "N_3");
                    //-
                    writeInCells(theSheet, "S8", theRepDS.Tables[104].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "T8", theRepDS.Tables[104].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "U8", theRepDS.Tables[104].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "V8", theRepDS.Tables[104].TableName.ToString(), "N_3");
                    //-
                    writeInCells(theSheet, "W8", theRepDS.Tables[129].TableName.ToString(), "N_BaseMonth");
                    writeInCells(theSheet, "X8", theRepDS.Tables[129].TableName.ToString(), "N_1");
                    writeInCells(theSheet, "Y8", theRepDS.Tables[129].TableName.ToString(), "N_2");
                    writeInCells(theSheet, "Z8", theRepDS.Tables[129].TableName.ToString(), "N_3");

                    // On original first line regimen
                    writeInCells(theSheet, "A9", "", "H");
                    writeInCells(theSheet, "B9", "", "On original 1st line regimen");
                    writeInCells(theSheet, "C9", theRepDS.Tables[5].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "D9", theRepDS.Tables[5].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "E9", theRepDS.Tables[5].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "F9", theRepDS.Tables[5].TableName.ToString(), "H_3");
                    //---------
                    writeInCells(theSheet, "G9", theRepDS.Tables[30].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "H9", theRepDS.Tables[30].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "I9", theRepDS.Tables[30].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "J9", theRepDS.Tables[30].TableName.ToString(), "H_3");
                    //-
                    writeInCells(theSheet, "K9", theRepDS.Tables[55].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "L9", theRepDS.Tables[55].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "M9", theRepDS.Tables[55].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "N9", theRepDS.Tables[55].TableName.ToString(), "H_3");
                    //-
                    writeInCells(theSheet, "O9", theRepDS.Tables[80].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "P9", theRepDS.Tables[80].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "Q9", theRepDS.Tables[80].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "R9", theRepDS.Tables[80].TableName.ToString(), "H_3");
                    //-
                    writeInCells(theSheet, "S9", theRepDS.Tables[105].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "T9", theRepDS.Tables[105].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "U9", theRepDS.Tables[105].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "V9", theRepDS.Tables[105].TableName.ToString(), "H_3");
                    //-
                    writeInCells(theSheet, "W9", theRepDS.Tables[130].TableName.ToString(), "H_BaseMonth");
                    writeInCells(theSheet, "X9", theRepDS.Tables[130].TableName.ToString(), "H_1");
                    writeInCells(theSheet, "Y9", theRepDS.Tables[130].TableName.ToString(), "H_2");
                    writeInCells(theSheet, "Z9", theRepDS.Tables[130].TableName.ToString(), "H_3");

                    //On alternate 1st line regimen(substituted)
                    writeInCells(theSheet, "A10", "", "I");
                    writeInCells(theSheet, "B10", "", "On alternate 1st line regimen(substituted)");
                    writeInCells(theSheet, "C10", theRepDS.Tables[6].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "D10", theRepDS.Tables[6].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "E10", theRepDS.Tables[6].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "F10", theRepDS.Tables[6].TableName.ToString(), "I_3");

                    //-------
                    writeInCells(theSheet, "G10", theRepDS.Tables[31].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "H10", theRepDS.Tables[31].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "I10", theRepDS.Tables[31].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "J10", theRepDS.Tables[31].TableName.ToString(), "I_3");
                    //-
                    writeInCells(theSheet, "K10", theRepDS.Tables[56].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "L10", theRepDS.Tables[56].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "M10", theRepDS.Tables[56].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "N10", theRepDS.Tables[56].TableName.ToString(), "I_3");
                    //-
                    writeInCells(theSheet, "O10", theRepDS.Tables[81].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "P10", theRepDS.Tables[81].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "Q10", theRepDS.Tables[81].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "R10", theRepDS.Tables[81].TableName.ToString(), "I_3");
                    //-
                    writeInCells(theSheet, "S10", theRepDS.Tables[106].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "T10", theRepDS.Tables[106].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "U10", theRepDS.Tables[106].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "V10", theRepDS.Tables[106].TableName.ToString(), "I_3");
                    //-
                    writeInCells(theSheet, "W10", theRepDS.Tables[131].TableName.ToString(), "I_BaseMonth");
                    writeInCells(theSheet, "X10", theRepDS.Tables[131].TableName.ToString(), "I_1");
                    writeInCells(theSheet, "Y10", theRepDS.Tables[131].TableName.ToString(), "I_2");
                    writeInCells(theSheet, "Z10", theRepDS.Tables[131].TableName.ToString(), "I_3");

                    // On 2nd line or other regimen (switched)
                    writeInCells(theSheet, "A11", "", "J");
                    writeInCells(theSheet, "B11", "", "On 2nd line or other regimen(switched)");
                    writeInCells(theSheet, "C11", theRepDS.Tables[7].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "D11", theRepDS.Tables[7].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "E11", theRepDS.Tables[7].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "F11", theRepDS.Tables[7].TableName.ToString(), "J_3");
                    //--
                    writeInCells(theSheet, "G11", theRepDS.Tables[32].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "H11", theRepDS.Tables[32].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "I11", theRepDS.Tables[32].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "J11", theRepDS.Tables[32].TableName.ToString(), "J_3");
                    //-
                    writeInCells(theSheet, "K11", theRepDS.Tables[57].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "L11", theRepDS.Tables[57].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "M11", theRepDS.Tables[57].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "N11", theRepDS.Tables[57].TableName.ToString(), "J_3");
                    //-
                    writeInCells(theSheet, "O11", theRepDS.Tables[82].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "P11", theRepDS.Tables[82].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "Q11", theRepDS.Tables[82].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "R11", theRepDS.Tables[82].TableName.ToString(), "J_3");
                    //-
                    writeInCells(theSheet, "S11", theRepDS.Tables[107].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "T11", theRepDS.Tables[107].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "U11", theRepDS.Tables[107].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "V11", theRepDS.Tables[107].TableName.ToString(), "J_3");
                    //-
                    writeInCells(theSheet, "W11", theRepDS.Tables[132].TableName.ToString(), "J_BaseMonth");
                    writeInCells(theSheet, "X11", theRepDS.Tables[132].TableName.ToString(), "J_1");
                    writeInCells(theSheet, "Y11", theRepDS.Tables[132].TableName.ToString(), "J_2");
                    writeInCells(theSheet, "Z11", theRepDS.Tables[132].TableName.ToString(), "J_3");


                    //Current regimen unrecorded
                    writeInCells(theSheet, "A12", "", "");
                    writeInCells(theSheet, "B12", "", "Current regimen unrecorded");
                    writeInCells(theSheet, "C12", theRepDS.Tables[8].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "D12", theRepDS.Tables[8].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "E12", theRepDS.Tables[8].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "F12", theRepDS.Tables[8].TableName.ToString(), "UN_3");
                    //
                    writeInCells(theSheet, "G12", theRepDS.Tables[33].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "H12", theRepDS.Tables[33].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "I12", theRepDS.Tables[33].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "J12", theRepDS.Tables[33].TableName.ToString(), "UN_3");
                    //-
                    writeInCells(theSheet, "K12", theRepDS.Tables[58].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "L12", theRepDS.Tables[58].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "M12", theRepDS.Tables[58].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "N12", theRepDS.Tables[58].TableName.ToString(), "UN_3");
                    //-
                    writeInCells(theSheet, "O12", theRepDS.Tables[83].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "P12", theRepDS.Tables[83].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "Q12", theRepDS.Tables[83].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "R12", theRepDS.Tables[83].TableName.ToString(), "UN_3");
                    //-
                    writeInCells(theSheet, "S12", theRepDS.Tables[108].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "T12", theRepDS.Tables[108].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "U12", theRepDS.Tables[108].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "V12", theRepDS.Tables[108].TableName.ToString(), "UN_3");
                    //-
                    writeInCells(theSheet, "W12", theRepDS.Tables[133].TableName.ToString(), "UN_BaseMonth");
                    writeInCells(theSheet, "X12", theRepDS.Tables[133].TableName.ToString(), "UN_1");
                    writeInCells(theSheet, "Y12", theRepDS.Tables[133].TableName.ToString(), "UN_2");
                    writeInCells(theSheet, "Z12", theRepDS.Tables[133].TableName.ToString(), "UN_3");

                    //Stopped/Current ARV status unrecorded
                    writeInCells(theSheet, "A14", "", "");
                    writeInCells(theSheet, "B14", "", "Stopped/Current ARV status unrecorded");
                    writeInCells(theSheet, "C14", theRepDS.Tables[9].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "D14", theRepDS.Tables[9].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "E14", theRepDS.Tables[9].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "F14", theRepDS.Tables[9].TableName.ToString(), "S_3");
                    //--
                    writeInCells(theSheet, "G14", theRepDS.Tables[34].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "H14", theRepDS.Tables[34].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "I14", theRepDS.Tables[34].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "J14", theRepDS.Tables[34].TableName.ToString(), "S_3");
                    //-
                    writeInCells(theSheet, "K14", theRepDS.Tables[59].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "L14", theRepDS.Tables[59].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "M14", theRepDS.Tables[59].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "N14", theRepDS.Tables[59].TableName.ToString(), "S_3");
                    //-
                    writeInCells(theSheet, "O14", theRepDS.Tables[84].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "P14", theRepDS.Tables[84].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "Q14", theRepDS.Tables[84].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "R14", theRepDS.Tables[84].TableName.ToString(), "S_3");
                    //-
                    writeInCells(theSheet, "S14", theRepDS.Tables[109].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "T14", theRepDS.Tables[109].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "U14", theRepDS.Tables[109].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "V14", theRepDS.Tables[109].TableName.ToString(), "S_3");
                    //-
                    writeInCells(theSheet, "W14", theRepDS.Tables[134].TableName.ToString(), "S_BaseMonth");
                    writeInCells(theSheet, "X14", theRepDS.Tables[134].TableName.ToString(), "S_1");
                    writeInCells(theSheet, "Y14", theRepDS.Tables[134].TableName.ToString(), "S_2");
                    writeInCells(theSheet, "Z14", theRepDS.Tables[134].TableName.ToString(), "S_3");



                    //Died
                    writeInCells(theSheet, "A15", "", "");
                    writeInCells(theSheet, "B15", "", "Died");
                    writeInCells(theSheet, "C15", theRepDS.Tables[10].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "D15", theRepDS.Tables[10].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "E15", theRepDS.Tables[10].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "F15", theRepDS.Tables[10].TableName.ToString(), "D_3");
                    //
                    writeInCells(theSheet, "G15", theRepDS.Tables[35].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "H15", theRepDS.Tables[35].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "I15", theRepDS.Tables[35].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "J15", theRepDS.Tables[35].TableName.ToString(), "D_3");
                    //-
                    writeInCells(theSheet, "K15", theRepDS.Tables[60].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "L15", theRepDS.Tables[60].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "M15", theRepDS.Tables[60].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "N15", theRepDS.Tables[60].TableName.ToString(), "D_3");
                    //-
                    writeInCells(theSheet, "O15", theRepDS.Tables[85].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "P15", theRepDS.Tables[85].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "Q15", theRepDS.Tables[85].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "R15", theRepDS.Tables[85].TableName.ToString(), "D_3");
                    //-
                    writeInCells(theSheet, "S15", theRepDS.Tables[110].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "T15", theRepDS.Tables[110].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "U15", theRepDS.Tables[110].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "V15", theRepDS.Tables[110].TableName.ToString(), "D_3");
                    //-
                    writeInCells(theSheet, "W15", theRepDS.Tables[135].TableName.ToString(), "D_BaseMonth");
                    writeInCells(theSheet, "X15", theRepDS.Tables[135].TableName.ToString(), "D_1");
                    writeInCells(theSheet, "Y15", theRepDS.Tables[135].TableName.ToString(), "D_2");
                    writeInCells(theSheet, "Z15", theRepDS.Tables[135].TableName.ToString(), "D_3");

                    //Unkonwn
                    writeInCells(theSheet, "A16", "", "");
                    writeInCells(theSheet, "B16", "", "Unknown");
                    writeInCells(theSheet, "C16", theRepDS.Tables[11].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "D16", theRepDS.Tables[11].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "E16", theRepDS.Tables[11].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "F16", theRepDS.Tables[11].TableName.ToString(), "U_3");
                    //--
                    writeInCells(theSheet, "G16", theRepDS.Tables[36].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "H16", theRepDS.Tables[36].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "I16", theRepDS.Tables[36].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "J16", theRepDS.Tables[36].TableName.ToString(), "U_3");
                    //-
                    writeInCells(theSheet, "K16", theRepDS.Tables[61].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "L16", theRepDS.Tables[61].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "M16", theRepDS.Tables[61].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "N16", theRepDS.Tables[61].TableName.ToString(), "U_3");
                    //-
                    writeInCells(theSheet, "O16", theRepDS.Tables[86].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "P16", theRepDS.Tables[86].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "Q16", theRepDS.Tables[86].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "R16", theRepDS.Tables[86].TableName.ToString(), "U_3");
                    //-
                    writeInCells(theSheet, "S16", theRepDS.Tables[111].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "T16", theRepDS.Tables[111].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "U16", theRepDS.Tables[111].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "V16", theRepDS.Tables[111].TableName.ToString(), "U_3");
                    //-
                    writeInCells(theSheet, "W16", theRepDS.Tables[136].TableName.ToString(), "U_BaseMonth");
                    writeInCells(theSheet, "X16", theRepDS.Tables[136].TableName.ToString(), "U_1");
                    writeInCells(theSheet, "Y16", theRepDS.Tables[136].TableName.ToString(), "U_2");
                    writeInCells(theSheet, "Z16", theRepDS.Tables[136].TableName.ToString(), "U_3");

                    //Lost to follwo up/did not visit for three months
                    writeInCells(theSheet, "A17", "", "");
                    writeInCells(theSheet, "B17", "", "Lost to follow up/did not visit during months");
                    writeInCells(theSheet, "C17", theRepDS.Tables[12].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "D17", theRepDS.Tables[12].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "E17", theRepDS.Tables[12].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "F17", theRepDS.Tables[12].TableName.ToString(), "L_3");
                    //---
                    writeInCells(theSheet, "G17", theRepDS.Tables[37].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "H17", theRepDS.Tables[37].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "I17", theRepDS.Tables[37].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "J17", theRepDS.Tables[37].TableName.ToString(), "L_3");
                    //-
                    writeInCells(theSheet, "K17", theRepDS.Tables[62].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "L17", theRepDS.Tables[62].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "M17", theRepDS.Tables[62].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "N17", theRepDS.Tables[62].TableName.ToString(), "L_3");
                    //-
                    writeInCells(theSheet, "O17", theRepDS.Tables[87].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "P17", theRepDS.Tables[87].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "Q17", theRepDS.Tables[87].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "R17", theRepDS.Tables[87].TableName.ToString(), "L_3");
                    //-
                    writeInCells(theSheet, "S17", theRepDS.Tables[112].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "T17", theRepDS.Tables[112].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "U17", theRepDS.Tables[112].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "V17", theRepDS.Tables[112].TableName.ToString(), "L_3");
                    //-
                    writeInCells(theSheet, "W17", theRepDS.Tables[137].TableName.ToString(), "L_BaseMonth");
                    writeInCells(theSheet, "X17", theRepDS.Tables[137].TableName.ToString(), "L_1");
                    writeInCells(theSheet, "Y17", theRepDS.Tables[137].TableName.ToString(), "L_2");
                    writeInCells(theSheet, "Z17", theRepDS.Tables[137].TableName.ToString(), "L_3");

                    //Number of cohort alive and on ART
                    writeInCells(theSheet, "A19", "", "");
                    writeInCells(theSheet, "B19", "", "Number of cohort alive and on ART");
                    writeInCells(theSheet, "C19", theRepDS.Tables[13].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "D19", theRepDS.Tables[13].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "E19", theRepDS.Tables[13].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "F19", theRepDS.Tables[13].TableName.ToString(), "NAL_3");
                    //--
                    writeInCells(theSheet, "G19", theRepDS.Tables[38].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "H19", theRepDS.Tables[38].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "I19", theRepDS.Tables[38].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "J19", theRepDS.Tables[38].TableName.ToString(), "NAL_3");
                    //-
                    writeInCells(theSheet, "K19", theRepDS.Tables[63].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "L19", theRepDS.Tables[63].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "M19", theRepDS.Tables[63].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "N19", theRepDS.Tables[63].TableName.ToString(), "NAL_3");
                    //-
                    writeInCells(theSheet, "O19", theRepDS.Tables[88].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "P19", theRepDS.Tables[88].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "Q19", theRepDS.Tables[88].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "R19", theRepDS.Tables[88].TableName.ToString(), "NAL_3");
                    //-
                    writeInCells(theSheet, "S19", theRepDS.Tables[113].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "T19", theRepDS.Tables[113].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "U19", theRepDS.Tables[113].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "V19", theRepDS.Tables[113].TableName.ToString(), "NAL_3");
                    //-
                    writeInCells(theSheet, "W19", theRepDS.Tables[138].TableName.ToString(), "NAL_BaseMonth");
                    writeInCells(theSheet, "X19", theRepDS.Tables[138].TableName.ToString(), "NAL_1");
                    writeInCells(theSheet, "Y19", theRepDS.Tables[138].TableName.ToString(), "NAL_2");
                    writeInCells(theSheet, "Z19", theRepDS.Tables[138].TableName.ToString(), "NAL_3");

                    //Percent of cohort alive and on ART
                    writeInCells(theSheet, "A20", "", "");
                    writeInCells(theSheet, "B20", "", "Percent of cohort alive and on ART");
                    writeInCells(theSheet, "C20", theRepDS.Tables[14].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "D20", theRepDS.Tables[14].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "E20", theRepDS.Tables[14].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "F20", theRepDS.Tables[14].TableName.ToString(), "NALP_3");
                    //--
                    writeInCells(theSheet, "G20", theRepDS.Tables[39].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "H20", theRepDS.Tables[39].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "I20", theRepDS.Tables[39].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "J20", theRepDS.Tables[39].TableName.ToString(), "NALP_3");
                    //-
                    writeInCells(theSheet, "K20", theRepDS.Tables[64].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "L20", theRepDS.Tables[64].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "M20", theRepDS.Tables[64].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "N20", theRepDS.Tables[64].TableName.ToString(), "NALP_3");
                    //-
                    writeInCells(theSheet, "O20", theRepDS.Tables[89].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "P20", theRepDS.Tables[89].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "Q20", theRepDS.Tables[89].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "R20", theRepDS.Tables[89].TableName.ToString(), "NALP_3");
                    //-
                    writeInCells(theSheet, "S20", theRepDS.Tables[114].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "T20", theRepDS.Tables[114].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "U20", theRepDS.Tables[114].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "V20", theRepDS.Tables[114].TableName.ToString(), "NALP_3");
                    //-
                    writeInCells(theSheet, "W20", theRepDS.Tables[139].TableName.ToString(), "NALP_BaseMonth");
                    writeInCells(theSheet, "X20", theRepDS.Tables[139].TableName.ToString(), "NALP_1");
                    writeInCells(theSheet, "Y20", theRepDS.Tables[139].TableName.ToString(), "NALP_2");
                    writeInCells(theSheet, "Z20", theRepDS.Tables[139].TableName.ToString(), "NALP_3");

                    //Number of patients aged 7 years or more with CD4
                    writeInCells(theSheet, "A22", "", "");
                    writeInCells(theSheet, "B22", "", "Number of patients aged 7 years or more with CD4");
                    writeInCells(theSheet, "C22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "D22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "E22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "F22", theRepDS.Tables[15].TableName.ToString(), "AG7_NCD4_3");
                    //--
                    writeInCells(theSheet, "G22", theRepDS.Tables[40].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "H22", theRepDS.Tables[40].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "I22", theRepDS.Tables[40].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "J22", theRepDS.Tables[40].TableName.ToString(), "AG7_NCD4_3");
                    //--
                    writeInCells(theSheet, "K22", theRepDS.Tables[65].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "L22", theRepDS.Tables[65].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "M22", theRepDS.Tables[65].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "N22", theRepDS.Tables[65].TableName.ToString(), "AG7_NCD4_3");
                    //--
                    writeInCells(theSheet, "O22", theRepDS.Tables[90].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "P22", theRepDS.Tables[90].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "Q22", theRepDS.Tables[90].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "R22", theRepDS.Tables[90].TableName.ToString(), "AG7_NCD4_3");

                    //--
                    writeInCells(theSheet, "S22", theRepDS.Tables[115].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "T22", theRepDS.Tables[115].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "U22", theRepDS.Tables[115].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "V22", theRepDS.Tables[115].TableName.ToString(), "AG7_NCD4_3");

                    //--
                    writeInCells(theSheet, "W22", theRepDS.Tables[140].TableName.ToString(), "AG7_NCD4_BaseMonth");
                    writeInCells(theSheet, "X22", theRepDS.Tables[140].TableName.ToString(), "AG7_NCD4_1");
                    writeInCells(theSheet, "Y22", theRepDS.Tables[140].TableName.ToString(), "AG7_NCD4_2");
                    writeInCells(theSheet, "Z22", theRepDS.Tables[140].TableName.ToString(), "AG7_NCD4_3");


                    //Number with CD4>=200
                    writeInCells(theSheet, "A23", "", "");
                    writeInCells(theSheet, "B23", "", "Number with CD4>=200");
                    writeInCells(theSheet, "C23", theRepDS.Tables[16].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "D23", theRepDS.Tables[16].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "E23", theRepDS.Tables[16].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "F23", theRepDS.Tables[16].TableName.ToString(), "NCD4_3");
                    //-
                    writeInCells(theSheet, "G23", theRepDS.Tables[41].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "H23", theRepDS.Tables[41].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "I23", theRepDS.Tables[41].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "J23", theRepDS.Tables[41].TableName.ToString(), "NCD4_3");
                    //-
                    writeInCells(theSheet, "K23", theRepDS.Tables[66].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "L23", theRepDS.Tables[66].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "M23", theRepDS.Tables[66].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "N23", theRepDS.Tables[66].TableName.ToString(), "NCD4_3");
                    //-
                    writeInCells(theSheet, "O23", theRepDS.Tables[91].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "P23", theRepDS.Tables[91].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "Q23", theRepDS.Tables[91].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "R23", theRepDS.Tables[91].TableName.ToString(), "NCD4_3");
                    //-
                    writeInCells(theSheet, "S23", theRepDS.Tables[116].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "T23", theRepDS.Tables[116].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "U23", theRepDS.Tables[116].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "V23", theRepDS.Tables[116].TableName.ToString(), "NCD4_3");
                    //-
                    writeInCells(theSheet, "W23", theRepDS.Tables[141].TableName.ToString(), "NCD4_BaseMonth");
                    writeInCells(theSheet, "X23", theRepDS.Tables[141].TableName.ToString(), "NCD4_1");
                    writeInCells(theSheet, "Y23", theRepDS.Tables[141].TableName.ToString(), "NCD4_2");
                    writeInCells(theSheet, "Z23", theRepDS.Tables[141].TableName.ToString(), "NCD4_3");


                    //Percent with CD4>=200
                    writeInCells(theSheet, "A24", "", "");
                    writeInCells(theSheet, "B24", "", "Percent with CD4>=200");
                    writeInCells(theSheet, "C24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "D24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "E24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "F24", theRepDS.Tables[17].TableName.ToString(), "PNCD4_3");
                    //
                    writeInCells(theSheet, "G24", theRepDS.Tables[42].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "H24", theRepDS.Tables[42].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "I24", theRepDS.Tables[42].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "J24", theRepDS.Tables[42].TableName.ToString(), "PNCD4_3");
                    //-
                    writeInCells(theSheet, "K24", theRepDS.Tables[67].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "L24", theRepDS.Tables[67].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "M24", theRepDS.Tables[67].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "N24", theRepDS.Tables[67].TableName.ToString(), "PNCD4_3");
                    //-
                    writeInCells(theSheet, "O24", theRepDS.Tables[92].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "P24", theRepDS.Tables[92].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "Q24", theRepDS.Tables[92].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "R24", theRepDS.Tables[92].TableName.ToString(), "PNCD4_3");
                    //-
                    writeInCells(theSheet, "S24", theRepDS.Tables[117].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "T24", theRepDS.Tables[117].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "U24", theRepDS.Tables[117].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "V24", theRepDS.Tables[117].TableName.ToString(), "PNCD4_3");
                    //-
                    writeInCells(theSheet, "W24", theRepDS.Tables[142].TableName.ToString(), "PNCD4_BaseMonth");
                    writeInCells(theSheet, "X24", theRepDS.Tables[142].TableName.ToString(), "PNCD4_1");
                    writeInCells(theSheet, "Y24", theRepDS.Tables[142].TableName.ToString(), "PNCD4_2");
                    writeInCells(theSheet, "Z24", theRepDS.Tables[142].TableName.ToString(), "PNCD4_3");


                    //Number Working
                    writeInCells(theSheet, "A25", "", "");
                    writeInCells(theSheet, "B25", "", "Number Working");
                    writeInCells(theSheet, "C25", theRepDS.Tables[18].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "D25", theRepDS.Tables[18].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "E25", theRepDS.Tables[18].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "F25", theRepDS.Tables[18].TableName.ToString(), "NW_3");
                    //--
                    writeInCells(theSheet, "G25", theRepDS.Tables[43].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "H25", theRepDS.Tables[43].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "I25", theRepDS.Tables[43].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "J25", theRepDS.Tables[43].TableName.ToString(), "NW_3");
                    //-
                    writeInCells(theSheet, "K25", theRepDS.Tables[68].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "L25", theRepDS.Tables[68].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "M25", theRepDS.Tables[68].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "N25", theRepDS.Tables[68].TableName.ToString(), "NW_3");
                    //-
                    writeInCells(theSheet, "O25", theRepDS.Tables[93].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "P25", theRepDS.Tables[93].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "Q25", theRepDS.Tables[93].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "R25", theRepDS.Tables[93].TableName.ToString(), "NW_3");
                    //-
                    writeInCells(theSheet, "S25", theRepDS.Tables[118].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "T25", theRepDS.Tables[118].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "U25", theRepDS.Tables[118].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "V25", theRepDS.Tables[118].TableName.ToString(), "NW_3");
                    //-
                    writeInCells(theSheet, "W25", theRepDS.Tables[143].TableName.ToString(), "NW_BaseMonth");
                    writeInCells(theSheet, "X25", theRepDS.Tables[143].TableName.ToString(), "NW_1");
                    writeInCells(theSheet, "Y25", theRepDS.Tables[143].TableName.ToString(), "NW_2");
                    writeInCells(theSheet, "Z25", theRepDS.Tables[143].TableName.ToString(), "NW_3");

                    //Number Ambulatory
                    writeInCells(theSheet, "A26", "", "");
                    writeInCells(theSheet, "B26", "", "Number Ambulatory");
                    writeInCells(theSheet, "C26", theRepDS.Tables[19].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "D26", theRepDS.Tables[19].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "E26", theRepDS.Tables[19].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "F26", theRepDS.Tables[19].TableName.ToString(), "NA_3");
                    //--
                    writeInCells(theSheet, "G26", theRepDS.Tables[44].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "H26", theRepDS.Tables[44].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "I26", theRepDS.Tables[44].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "J26", theRepDS.Tables[44].TableName.ToString(), "NA_3");
                    //
                    writeInCells(theSheet, "K26", theRepDS.Tables[69].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "L26", theRepDS.Tables[69].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "M26", theRepDS.Tables[69].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "N26", theRepDS.Tables[69].TableName.ToString(), "NA_3");
                    //
                    writeInCells(theSheet, "O26", theRepDS.Tables[94].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "P26", theRepDS.Tables[94].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "Q26", theRepDS.Tables[94].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "R26", theRepDS.Tables[94].TableName.ToString(), "NA_3");
                    //
                    writeInCells(theSheet, "S26", theRepDS.Tables[119].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "T26", theRepDS.Tables[119].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "U26", theRepDS.Tables[119].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "V26", theRepDS.Tables[119].TableName.ToString(), "NA_3");
                    //
                    writeInCells(theSheet, "W26", theRepDS.Tables[144].TableName.ToString(), "NA_BaseMonth");
                    writeInCells(theSheet, "X26", theRepDS.Tables[144].TableName.ToString(), "NA_1");
                    writeInCells(theSheet, "Y26", theRepDS.Tables[144].TableName.ToString(), "NA_2");
                    writeInCells(theSheet, "Z26", theRepDS.Tables[144].TableName.ToString(), "NA_3");

                    //Number Bedridden
                    writeInCells(theSheet, "A27", "", "");
                    writeInCells(theSheet, "B27", "", "Number Bedridden");
                    writeInCells(theSheet, "C27", theRepDS.Tables[20].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "D27", theRepDS.Tables[20].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "E27", theRepDS.Tables[20].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "F27", theRepDS.Tables[20].TableName.ToString(), "NB_3");
                    //--
                    writeInCells(theSheet, "G27", theRepDS.Tables[45].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "H27", theRepDS.Tables[45].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "I27", theRepDS.Tables[45].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "J27", theRepDS.Tables[45].TableName.ToString(), "NB_3");
                    //-
                    writeInCells(theSheet, "K27", theRepDS.Tables[70].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "L27", theRepDS.Tables[70].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "M27", theRepDS.Tables[70].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "N27", theRepDS.Tables[70].TableName.ToString(), "NB_3");
                    //-
                    writeInCells(theSheet, "O27", theRepDS.Tables[95].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "P27", theRepDS.Tables[95].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "Q27", theRepDS.Tables[95].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "R27", theRepDS.Tables[95].TableName.ToString(), "NB_3");
                    //-
                    writeInCells(theSheet, "S27", theRepDS.Tables[120].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "T27", theRepDS.Tables[120].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "U27", theRepDS.Tables[120].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "V27", theRepDS.Tables[120].TableName.ToString(), "NB_3");
                    //-
                    writeInCells(theSheet, "W27", theRepDS.Tables[145].TableName.ToString(), "NB_BaseMonth");
                    writeInCells(theSheet, "X27", theRepDS.Tables[145].TableName.ToString(), "NB_1");
                    writeInCells(theSheet, "Y27", theRepDS.Tables[145].TableName.ToString(), "NB_2");
                    writeInCells(theSheet, "Z27", theRepDS.Tables[145].TableName.ToString(), "NB_3");

                    //Number functional status unrecorded
                    writeInCells(theSheet, "A28", "", "");
                    writeInCells(theSheet, "B28", "", "Number functional status unrecorded");
                    writeInCells(theSheet, "C28", theRepDS.Tables[21].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "D28", theRepDS.Tables[21].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "E28", theRepDS.Tables[21].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "F28", theRepDS.Tables[21].TableName.ToString(), "NU_3");
                    //
                    writeInCells(theSheet, "G28", theRepDS.Tables[46].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "H28", theRepDS.Tables[46].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "I28", theRepDS.Tables[46].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "J28", theRepDS.Tables[46].TableName.ToString(), "NU_3");
                    //-
                    writeInCells(theSheet, "K28", theRepDS.Tables[71].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "L28", theRepDS.Tables[71].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "M28", theRepDS.Tables[71].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "N28", theRepDS.Tables[71].TableName.ToString(), "NU_3");
                    //-
                    writeInCells(theSheet, "O28", theRepDS.Tables[96].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "P28", theRepDS.Tables[96].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "Q28", theRepDS.Tables[96].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "R28", theRepDS.Tables[96].TableName.ToString(), "NU_3");
                    //-
                    writeInCells(theSheet, "S28", theRepDS.Tables[121].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "T28", theRepDS.Tables[121].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "U28", theRepDS.Tables[121].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "V28", theRepDS.Tables[121].TableName.ToString(), "NU_3");
                    //-
                    writeInCells(theSheet, "W28", theRepDS.Tables[146].TableName.ToString(), "NU_BaseMonth");
                    writeInCells(theSheet, "X28", theRepDS.Tables[146].TableName.ToString(), "NU_1");
                    writeInCells(theSheet, "Y28", theRepDS.Tables[146].TableName.ToString(), "NU_2");
                    writeInCells(theSheet, "Z28", theRepDS.Tables[146].TableName.ToString(), "NU_3");

                    //Number who visited and were recorded on ARVs as many times as there are months
                    writeInCells(theSheet, "A30", "", "");
                    writeInCells(theSheet, "B30", "", "Number who visited and were recorded on ARVs as many times as there are months");
                    writeInCells(theSheet, "C30", theRepDS.Tables[22].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "D30", theRepDS.Tables[22].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "E30", theRepDS.Tables[22].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "F30", theRepDS.Tables[22].TableName.ToString(), "NM_3");
                    //--
                    writeInCells(theSheet, "G30", theRepDS.Tables[47].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "H30", theRepDS.Tables[47].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "I30", theRepDS.Tables[47].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "J30", theRepDS.Tables[47].TableName.ToString(), "NM_3");
                    //-
                    writeInCells(theSheet, "K30", theRepDS.Tables[72].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "L30", theRepDS.Tables[72].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "M30", theRepDS.Tables[72].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "N30", theRepDS.Tables[72].TableName.ToString(), "NM_3");
                    //-
                    writeInCells(theSheet, "O30", theRepDS.Tables[97].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "P30", theRepDS.Tables[97].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "Q30", theRepDS.Tables[97].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "R30", theRepDS.Tables[97].TableName.ToString(), "NM_3");
                    //-
                    writeInCells(theSheet, "S30", theRepDS.Tables[122].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "T30", theRepDS.Tables[122].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "U30", theRepDS.Tables[122].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "V30", theRepDS.Tables[122].TableName.ToString(), "NM_3");
                    //-
                    writeInCells(theSheet, "W30", theRepDS.Tables[147].TableName.ToString(), "NM_BaseMonth");
                    writeInCells(theSheet, "X30", theRepDS.Tables[147].TableName.ToString(), "NM_1");
                    writeInCells(theSheet, "Y30", theRepDS.Tables[147].TableName.ToString(), "NM_2");
                    writeInCells(theSheet, "Z30", theRepDS.Tables[147].TableName.ToString(), "NM_3");








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
            //theRange.WrapText = true;
            string theExitvalue = "";
            if (theRange.Value2 != null)
                theExitvalue = theRange.Value2.ToString();
            else
                theExitvalue = "";
            if ((ddMonth.SelectedValue.ToString() != "0") && (txtYear.Text != ""))
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
        private void writeInCells(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {

            Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
            //theRange.WrapText = true;
            string theExitvalue = "";
            if (theRange.Value2 != null)
                theExitvalue = theRange.Value2.ToString();
            else
                theExitvalue = "";
            if ((ddMonthyear.SelectedValue.ToString() != "0") && (txtyears.Text != ""))
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
            IQCareUtils theUtils = new IQCareUtils();
            if (rdoMonth.Checked == true)
            {

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
                DateTime app = Convert.ToDateTime(Application["AppCurrentDate"]);

                if (ddMonthyear.SelectedItem.Text != "Select" && txtyears.Text != "")
                {
                    theYear = Convert.ToDateTime(theUtils.MakeDate("01-" + ddMonthyear.SelectedItem.Text + "-" + txtyears.Text));


                    if (theYear.Year > app.Year)
                    {
                        IQCareMsgBox.Show("Year", this);
                        txtyears.Focus();
                        return false;
                    }
                    if (theYear > Convert.ToDateTime(Application["AppCurrentDate"]))
                    {
                        IQCareMsgBox.Show("Month", this);
                        txtyears.Focus();
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

                if (ddMonthyear.SelectedItem.Text == "Select")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Month";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }



            }

            return true;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmReportDonorJump.aspx");
        }
    }
}