using System;
using System.Data;
using System.Web.UI.WebControls;

using Application.Common;
namespace IQCare.Web.Reports
{
    public partial class frmReportDonorJump : System.Web.UI.Page
    {
        DataSet theDSXML = new DataSet();
        private void Init_Page()
        {
            rdoCDCReport.Checked = false;
            rdoNMonthlyReport.Checked = false;
            rdoTMonthlyReport.Checked = false;
            rdoNACPCohort.Checked = false;
            rdoTrack1PMTCT.Checked = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            Master.PageScriptManager.RegisterAsyncPostBackControl(tabControl);
            //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;   
            //(Master.FindControl("lblheader") as Label).Text = "Donor Reports";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports";

            string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
            theDSXML.ReadXml(xmlFilePath);
            DataView theDV = new DataView(theDSXML.Tables[0]);
            string countryid = Session["AppCurrency"].ToString();
            theDV.RowFilter = "id=" + countryid + "";
            DataTable theDT = new DataTable();
            theDT = theDV.ToTable();
            hdCountryId.Value = Session["AppCurrency"].ToString();
            hdSystemId.Value = Session["SystemId"].ToString();
            //hdCountryId.Value =  Session["AppCurrency"].ToString();
            DataTable theDTModule = (DataTable)Session["AppModule"];
            string theModList = "";

            btnSubmit.Attributes.Add("Onclick", "javaScript:return Validate();");
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.DonorReports, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;

            }
            foreach (DataRow theDR in theDTModule.Rows)
            {
                if (theModList == "")
                    theModList = theDR["ModuleId"].ToString();
                else
                    theModList = theModList + "," + theDR["ModuleId"].ToString();
            }

            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script language = 'javascript' defer ='defer' id = 'scriptonload'>ShowHide('2');  </script>");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (rdoCDCReport.Checked == true)
            {
                Session["ReportId"] = "CDCReport";
                Session["ReportName"] = "Track 1.0 Facility Based Quaterly Report";
                Response.Redirect("frmReportCDC.aspx");
            }
            else if (rdoNMonthlyReport.Checked == true)
            {
                Session["ReportId"] = "NigeriaReport";
                Session["ReportName"] = "Nigeria-Monthly NACA Report (IQCare data)";
                Response.Redirect("frmReport_NigeriaMonthlyNACAReport.aspx");
            }
            else if (rdoNigeriaMR.Checked == true)
            {
                Session["ReportId"] = "MRReport";
                Session["ReportName"] = "Nigeria MR Report";
                Response.Redirect("frmReportCDC.aspx");
            }
            else if (rdoTMonthlyReport.Checked == true)
            {
                Session["ReportId"] = "TanzaniaReport";
                Session["ReportName"] = "TZNACPMonthlyReport";
                Response.Redirect("frmReport_TanzaniaNACPMonthlyReport.aspx");

            }
            else if (rdoNACPCohort.Checked == true)
            {
                Session["ReportId"] = "NACPCohortMonthlyreport";
                Session["ReportName"] = "NACPCohortMonthlyreport";
                Response.Redirect("frmReport_NACP_CohortAnalysis_Reprt.aspx");

            }
            else if (rdoTrack1PMTCT.Checked == true)
            {
                Session["ReportId"] = "Track1PMTCT";
                Session["ReportName"] = "Track 1.0 PMTCT";
                Response.Redirect("frmReportCDC.aspx");
            }
            else if (rdotrackHIVExposedPMTCT.Checked == true)
            {
                Session["ReportId"] = "HivExposedInfants";
                Session["ReportName"] = "Track 1.0 Hiv Exposed Infants Report";
                Response.Redirect("frmReportCDC.aspx");
            }
            else if (rdoUgandaOGAC.Checked == true)
            {
                Session["ReportId"] = "UgandaOGAC";
                Session["ReportName"] = "OGAC";
                Response.Redirect("frmReportCDC.aspx");


            }
            else if (rdoUgandaMonthly.Checked == true)
            {
                Session["ReportId"] = "UgandaMonthly";
                Session["ReportName"] = "Monthly Report Form-MOH";
                Response.Redirect("frmReportCDC.aspx");
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmFacilityHome.aspx");

        }


    }
}