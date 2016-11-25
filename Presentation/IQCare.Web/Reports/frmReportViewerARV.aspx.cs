#region Import namespace

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Interface.Reports;
using Excel = Microsoft.Office.Interop.Owc11;

#endregion Import namespace
namespace IQCare.Web.Reports
{
    public partial class frmReportViewerARV : System.Web.UI.Page
    {
        #region "Export Variables"
        public DataTable theCountry;
        private ReportDocument rptDocument;
        private string theDType = string.Empty;
        private DataTable theExcelDT;
        //string FName;
        #endregion "Export Variables"

        #region Comments
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Ashok Kr. Gupta
        // Code Modified By  : Deepika Sain
        // Written Date      : 06th Oct 2006
        // Modification Date : 23th Nov 2007
        // Description       : Report View Form
        //
        ////////////////////////////////////////////////////////////////////
        #endregion Comments

        # region  Variables Declaration

        private int thePatientId = 0;
        private string theQuarter = string.Empty;
        private int theReportId = 0;
        private string theReportName = string.Empty;
        private string theReportQuery = string.Empty;
        private string theReportSource = string.Empty;
        private string theReportTitle = string.Empty;
        private string theYear = string.Empty;
        //IReports ReportDetails;

        #endregion

        #region "User Defined Functions"

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ViewState.Remove("RptData");
            ViewState.Remove("RptName");
            if (ViewState["RepName"].ToString() == "AllPatientARVPickup" || ViewState["RepName"].ToString() == "SinglePatientARVPickup" || ViewState["RepName"].ToString() == "MisARVPickup" || ViewState["RepName"].ToString() == "PatientEnrollmentMonth")
            {
                //string theUrl = string.Format("{0}", "frmReport_PatientARVPickup.aspx?Patient=");
                Response.Redirect("frmReportFacilityJump.aspx?");
            }
            else if (ViewState["RepName"].ToString() == "Nigeria-Monthly NACA Report (IQCare data)")
            {
                Response.Redirect("frmReport_NigeriaMonthlyNACAReport.aspx");
            }

            if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"].ToString() != "")
            {
                if (Convert.ToInt32(Request.QueryString["ReportId"]) > 0)
                {
                    Response.Redirect("frmReportCustomNew.aspx?ReportId=" + Request.QueryString["ReportId"].ToString(), true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Back", "<script> history.back(); </script>");
            }

            Session.Remove("Report");
            Session.Remove("dtMonthlyNACAReportData");
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            IQCareUtils theUtils = new IQCareUtils();

            if (ViewState["RepName"].ToString() != "")
            {
                string FName = ViewState["FName"].ToString();
                string thePath = Server.MapPath("..\\ExcelFiles\\" + FName + ".xls");
                string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
                theUtils.ExportToExcel((DataTable)ViewState["RptData"], thePath, theTemplatePath);
                //theUtils.OpenExcelFile(thePath, Response);
                Response.Redirect("..\\ExcelFiles\\" + FName + ".xls");

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "btnPrint_Click", "fnPrient()", true);
            Response.Redirect("..\\ExcelFiles\\PView.pdf");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                Init_Page();
                Set_PatientReports();
            }

            //crViewer.ReportSource = (ReportDocument)ViewState["Report"];
            crViewer.ReportSource = (ReportDocument)Session["Report"];
        }

        private void CDCReport(ref DataSet dsCDCReport)
        {
        }

        private void ExportARVToExcel(DataTable dtDrugARVPickup)
        {
            theExcelDT = new DataTable();
            theExcelDT.Columns.Add("Patient Name", System.Type.GetType("System.String"));
            theExcelDT.Columns.Add("Enrollment No", System.Type.GetType("System.String"));
            theExcelDT.Columns.Add("Exiting Patient ID", System.Type.GetType("System.String"));
            theExcelDT.Columns.Add("Date Dispensed", System.Type.GetType("System.String"));
            theExcelDT.Columns.Add("Longest Duration", System.Type.GetType("System.Int32"));
            theExcelDT.Columns.Add("Next Collection", System.Type.GetType("System.String"));
            theExcelDT.Columns.Add("Days", System.Type.GetType("System.Int32"));
            theExcelDT.Columns.Add("Late/Early", System.Type.GetType("System.String"));

            int i = 0;
            DateTime orderedByDate;
            TimeSpan ts;
            Int32 dateDiff;
            for (i = 0; i < dtDrugARVPickup.Rows.Count; i++)
            {
                DataRow theDR = theExcelDT.NewRow();

                theDR[0] = dtDrugARVPickup.Rows[i]["Name"].ToString();
                theDR[1] = dtDrugARVPickup.Rows[i]["PatientEnrollmentID"];
                theDR[2] = dtDrugARVPickup.Rows[i]["PatientClinicID"].ToString().Trim();

                if (dtDrugARVPickup.Rows[i].IsNull("DispensedByDate") == true)
                {
                    theDR[3] = dtDrugARVPickup.Rows[i]["DispensedByDate"];
                }
                else
                {
                    theDR[3] = ((DateTime)dtDrugARVPickup.Rows[i]["DispensedByDate"]).ToString(Session["AppDateFormat"].ToString());
                }

                theDR[4] = dtDrugARVPickup.Rows[i]["LongestDaysLate"];

                orderedByDate = Convert.ToDateTime(dtDrugARVPickup.Rows[i]["DispensedByDate"]);
                ts = (Convert.ToDateTime(Application["AppCurrentDate"]) - orderedByDate);
                dateDiff = ts.Days;
                if (dtDrugARVPickup.Rows[i]["LongestDaysLate"].ToString() != "")
                {
                    if ((dtDrugARVPickup.Rows[i].IsNull("DateArrived") == true))
                    {
                        if (dtDrugARVPickup.Rows[i]["LongestDaysLate"] == System.DBNull.Value)
                        { theDR[5] = "Patient Not Due Yet"; }
                        else if (dateDiff <= Convert.ToInt32(dtDrugARVPickup.Rows[i]["LongestDaysLate"]))
                        { theDR[5] = "Patient Not Due Yet"; }
                    }
                    else
                    {
                        if (dtDrugARVPickup.Rows[i].IsNull("DateArrived") == false)
                        {
                            theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DateArrived"]).ToString(Session["AppDateFormat"].ToString());
                        }
                        else
                        {
                            theDR[5] = "Patient Late";
                        }
                    }
                }
                else
                {
                    if (dtDrugARVPickup.Rows[i].IsNull("DateArrived") == false)
                    {
                        theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DateArrived"]).ToString(Session["AppDateFormat"].ToString());
                    }
                    else
                    {
                        theDR[5] = "Patient Late";
                    }
                }
                if (dtDrugARVPickup.Rows[i].IsNull("DaysLateOrEarly") == true)
                {
                    theDR[6] = 0;
                    theDR[7] = "";
                }
                else
                {
                    theDR[6] = dtDrugARVPickup.Rows[i]["DaysLateOrEarly"];
                    if (Convert.ToInt32(dtDrugARVPickup.Rows[i]["DaysLateOrEarly"]) < 0)
                    {
                        theDR[7] = "Early";
                    }
                    else
                    {
                        theDR[7] = "Late";
                    }
                }
                theExcelDT.Rows.InsertAt(theDR, i);
            }
            ViewState["RptData"] = theExcelDT;
        }

        private void Init_Page()
        {
            try
            {
                string theReportHeading = string.Empty;

                if (Request.QueryString["ReportId"] != null)
                {
                    theReportId = Convert.ToInt32(Request.QueryString["ReportId"]);
                }
                if (Request.QueryString["ReportName"] != null)
                {
                    theReportName = Request.QueryString["ReportName"];
                }

                if (Request.QueryString["StartDate"] != null)
                {
                    ViewState["theStartDate"] = Request.QueryString["StartDate"];
                }
                if (Request.QueryString["EndDate"] != null)
                {
                    ViewState["theEndDate"] = Request.QueryString["EndDate"];
                }
                if (Request.QueryString["PatientId"] != null && Request.QueryString["PatientId"].ToString() != "")
                {
                    thePatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                }
                else
                {
                    thePatientId = 0;
                }

                // For CDC report
                if ((Request.QueryString["QuarterId"] != null && Request.QueryString["QuarterId"].ToString() != ""))
                {
                    theQuarter = Request.QueryString["QuarterId"].ToString();
                }
                if ((Request.QueryString["Year"] != null && Request.QueryString["Year"].ToString() != ""))
                {
                    theYear = Request.QueryString["Year"].ToString();
                }

                //

                if (Request.QueryString["DType"] != null)
                {
                    theDType = Request.QueryString["DType"];
                }

                if (theReportName != "")
                {
                    if (theReportName == "UpARVPickup")
                    {
                        theReportHeading = "Upcoming ARV Pickup Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";
                    }
                    else if (theReportName == "MisARVPickup")
                    {
                        theReportHeading = "Missed ARV Pickup Report";
                        theReportSource = "rptMisARVPickup.rpt";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Missed ARV Pickup";

                        btnExcel.Visible = true;
                    }
                    else if (theReportName == "NewPatients")
                    {
                        theReportHeading = "New Patients Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";
                    }
                    else if (theReportName == "PregnantFU")
                    {
                        theReportHeading = "PregnantFU Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";
                    }
                    else if (theReportName == "SinglePatientARVPickup")
                    {
                        theReportHeading = "Patient ARV Pickup Report";
                        theReportSource = "rptPatientARVPickup.rpt";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Single Patient ARV Pickup";

                        btnExcel.Visible = true;
                    }
                    else if (theReportName == "AllPatientARVPickup")
                    {
                        theReportHeading = "All Patient ARV Pickup Report";
                        theReportSource = "rptAllPatientARVPickup.rpt";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "All Patients ARV Pickup";

                        btnExcel.Visible = true;
                    }
                    else if (theReportName == "PatientEnrollmentMonth")
                    {
                        theReportHeading = "Patient Enrollment Month";
                        theReportSource = "rptPatiEnrollMonth.rpt";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Enrolled by Month";
                        btnExcel.Visible = false;
                    }

                    else if (theReportName == "ARVAdherence")
                    {
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Facility Report >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                        theReportHeading = "Adherence to ARV Collection Report";
                        theReportSource = "rptAdARVCollectionClients.rpt";
                    }
                    else if (theReportName == "MisARVAppointment")
                    {
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Facility Report >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick up Report";
                        theReportHeading = "Missing ARV Appointment Report";
                        theReportSource = "rptMisARVAppointClients.rpt";
                    }
                    else if (theReportName == "CDCReport")
                    {
                        //theReportHeading = "CDC Report";
                        theReportHeading = "CDC Report Track1.0 Facility-Based Quarterly Report";
                        theReportSource = "CDCReport.rpt";
                        btnExcel.Visible = true;
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports >> Track 1.0 Quarterly Report";
                    }
                    else if (theReportName == "Nigeria-Monthly NACA Report (IQCare data)")
                    {
                        theReportHeading = "Nigeria-Monthly NACA Report ";
                        theReportSource = "rptNigeriaMonthlyNACAReport.rpt";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports »Nigeria-Monthly NACA Report";
                    }
                }

                hBar.InnerText = theReportHeading;
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

        private int isDate(string columnValue)
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(columnValue);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        private void PatientProfile(ref DataSet dsReportsPatient)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            DataSet dsReportsPatientProfile = (DataSet)ReportDetails.GetPatientProfileAndHistory(thePatientId);
            ReportDetails = null;
            //=====================================================================
            // Filling data set object
            foreach (DataRow dr in dsReportsPatientProfile.Tables[0].Rows)
            {
                dsReportsPatient.Tables["dtPatientProfile"].ImportRow(dr);
            }
            foreach (DataRow dr in dsReportsPatientProfile.Tables[1].Rows)
            {
                dsReportsPatient.Tables["dtARVTreatmentHistory"].ImportRow(dr);
            }
            //==================================================================
            //History of AIDS Defining Illness

            for (int i = 0; i < dsReportsPatientProfile.Tables[2].Rows.Count; i++)
            {
                DataRow dr = dsReportsPatientProfile.Tables[2].Rows[i];
                DataRow PrevRow = null;
                DataRow drAIDSHistory;

                if (i > 0)
                {
                    PrevRow = dsReportsPatientProfile.Tables[2].Rows[i - 1];
                }
                if (PrevRow == null)
                {
                    drAIDSHistory = dsReportsPatient.Tables["dtAIDSDefiningHistory"].NewRow();
                    drAIDSHistory["Name"] = dr["Name"];
                    drAIDSHistory["DateOfDisease"] = dr["DateOfDisease"];
                    if (dr["VisitDate"] != DBNull.Value)
                    {
                        drAIDSHistory["VisitDate"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                        drAIDSHistory["VisitDates"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                    }
                    drAIDSHistory["Ptn_Pk"] = dr["Ptn_Pk"];
                    dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Add(drAIDSHistory);
                }
                else
                {
                    if (dr["Name"].ToString() == PrevRow["Name"].ToString())
                    {
                        string prevDates = dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows[dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Count - 1]["VisitDates"].ToString().Trim();
                        dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows[dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Count - 1]["VisitDates"] = prevDates + ";  " + Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                    }
                    else
                    {
                        drAIDSHistory = dsReportsPatient.Tables["dtAIDSDefiningHistory"].NewRow();
                        drAIDSHistory["Name"] = dr["Name"];
                        drAIDSHistory["DateOfDisease"] = dr["DateOfDisease"];
                        if (dr["VisitDate"] != DBNull.Value)
                        {
                            drAIDSHistory["VisitDate"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                            drAIDSHistory["VisitDates"] = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                        }

                        drAIDSHistory["Ptn_Pk"] = dr["Ptn_Pk"];
                        dsReportsPatient.Tables["dtAIDSDefiningHistory"].Rows.Add(drAIDSHistory);
                    }
                }
            }
            //==================================================================
            //Customizing Lab History Table According To the Report
            string PrevTest = "";
            for (int i = 0; i < dsReportsPatientProfile.Tables[3].Rows.Count; i++)
            {
                DataRow dr = dsReportsPatientProfile.Tables[3].Rows[i];
                DataRow PrevRow = null;
                DataRow drLabHistory;
                if (i > 0)
                {
                    PrevRow = dsReportsPatientProfile.Tables[3].Rows[i - 1];
                }
                if (PrevRow == null)
                {
                    drLabHistory = dsReportsPatient.Tables["dtLabHistory"].NewRow();
                    drLabHistory["OrderedByDate"] = dr["OrderedByDate"];
                    drLabHistory["Weight"] = dr["Weight"];
                    if (dr["TestId"].ToString() == "1")
                    {
                        drLabHistory["CD4"] = dr["TestResults"];
                    }
                    else
                    {
                        drLabHistory["OtherLabResult"] = dr["SubTestName"] + " " + dr["TestResults"];
                        PrevTest = drLabHistory["OtherLabResult"].ToString();
                    }
                    dsReportsPatient.Tables["dtLabHistory"].Rows.Add(drLabHistory);
                }
                else
                {
                    if (dr["OrderedByDate"].ToString() == PrevRow["OrderedByDate"].ToString())
                    {
                        if (dr["TestId"].ToString() != "1")
                        {
                            string PrevTests = dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["OtherLabResult"].ToString();
                            dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["OtherLabResult"] = PrevTests + ";  " + dr["SubTestName"] + " " + dr["TestResults"];
                        }
                        else
                        {
                            dsReportsPatient.Tables["dtLabHistory"].Rows[dsReportsPatient.Tables["dtLabHistory"].Rows.Count - 1]["CD4"] = dr["TestResults"];
                        }
                    }
                    else
                    {
                        drLabHistory = dsReportsPatient.Tables["dtLabHistory"].NewRow();
                        drLabHistory["OrderedByDate"] = dr["OrderedByDate"];
                        drLabHistory["Weight"] = dr["Weight"];
                        if (dr["TestId"].ToString() == "1")
                        {
                            drLabHistory["CD4"] = dr["TestResults"];
                        }
                        else
                        {
                            drLabHistory["OtherLabResult"] = dr["SubTestName"] + " " + dr["TestResults"];
                            PrevTest = drLabHistory["OtherLabResult"].ToString();
                        }
                        dsReportsPatient.Tables["dtLabHistory"].Rows.Add(drLabHistory);
                    }
                }
            }
            //=====================================================================
        }

        private void Set_PatientReports()
        {
            try
            {
                string theEnrollmentID = string.Empty;

                rptDocument = new ReportDocument();
                rptDocument.Load(Server.MapPath(theReportSource));
                if (theReportName != "ARVAdherence" && theReportName != "MisARVAppointment" && theReportName != "CDCReport" && theReportName != "SinglePatientARVPickup" && theReportName != "AllPatientARVPickup" && theReportName != "PCustomReport" && theReportName != "LCustomReport" && theReportName != "MisARVPickup" && theReportName != "PatientEnrollmentMonth" && theReportName != "Nigeria-Monthly NACA Report (IQCare data)")
                {
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsPatient = (DataTable)ReportDetails.GetPatientDetails(thePatientId, Convert.ToDateTime(ViewState["theStartDate"]), Convert.ToDateTime(ViewState["theEndDate"])).Tables[0];
                    ReportDetails = null;
                    rptDocument.SetDataSource(dtReportsPatient);
                    rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
                    rptDocument.SetParameterValue("EndDate", ViewState["theEndDate"]);
                }
                else if (theReportName == "SinglePatientARVPickup")
                {
                    DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];
                    ExportARVToExcel(dtDrugARVPickup);
                    ViewState["FName"] = "SinglePatientARVPickup";

                    rptDocument.SetDataSource(dtDrugARVPickup);
                    IQCareUtils theUtil = new IQCareUtils();

                    rptDocument.SetParameterValue("StartDate", theUtil.MakeDate("01-01-1900"));
                    rptDocument.SetParameterValue("EndDate", theUtil.MakeDate("01-01-1900"));
                    if (Session["SystemId"].ToString() == "1")
                    {
                        rptDocument.SetParameterValue("Patient_FileId", "Existing Patient ClinicId:");
                    }
                    else
                    {
                        rptDocument.SetParameterValue("Patient_FileId", "File Reference:");
                    }
                }
                else if (theReportName == "AllPatientARVPickup")
                {
                    DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];
                    ExportARVToExcel(dtDrugARVPickup);
                    ViewState["FName"] = "AllPatientARVPickup";
                    rptDocument.SetDataSource(dtDrugARVPickup);
                }

               //Ajay
                else if (theReportName == "MisARVPickup")
                {
                    DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];

                    #region "Excel Export"
                    theExcelDT = new DataTable();
                    theExcelDT.Columns.Add("Enroll#", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Name/Address", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Phone/Emergency Phone", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Last Regimen", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Age", System.Type.GetType("System.Int32"));
                    theExcelDT.Columns.Add("Dispensed By Date", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("ARV for (x) Days", System.Type.GetType("System.Int32"));
                    theExcelDT.Columns.Add("Expected Return", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("OverDue", System.Type.GetType("System.Int32"));
                    theExcelDT.Columns.Add("Termination Status", System.Type.GetType("System.String"));

                    int i = 0;

                    for (i = 0; i < dtDrugARVPickup.Rows.Count; i++)
                    {
                        DataRow theDR = theExcelDT.NewRow();
                        theDR[0] = dtDrugARVPickup.Rows[i]["PatientEnrollmentID"];
                        theDR[1] = dtDrugARVPickup.Rows[i]["Name"].ToString() + "/" + dtDrugARVPickup.Rows[i]["Address"].ToString();
                        theDR[2] = dtDrugARVPickup.Rows[i]["Phone"].ToString() + "/" + dtDrugARVPickup.Rows[i]["EmergContactPhone"].ToString();
                        theDR[3] = dtDrugARVPickup.Rows[i]["PatientLastRegimen"].ToString();
                        theDR[4] = dtDrugARVPickup.Rows[i]["Age"];
                        theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DispensedByDate"]).ToString(Session["AppDateFormat"].ToString());
                        theDR[6] = dtDrugARVPickup.Rows[i]["LongestDaysLate"];
                        theDR[7] = ((DateTime)dtDrugARVPickup.Rows[i]["NextOrder"]).ToString(Session["AppDateFormat"].ToString());
                        theDR[8] = dtDrugARVPickup.Rows[i]["DaysLateOrEarly"];
                        theDR[9] = "Active";
                        theExcelDT.Rows.InsertAt(theDR, i);
                    }
                    ViewState["RptData"] = theExcelDT;
                    ViewState["FName"] = "MisARVPickup";
                    #endregion

                    rptDocument.SetDataSource(dtDrugARVPickup);
                    rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
                    //rptDocument.SetParameterValue("EndDate", theEndDate);
                }
                else if (theReportName == "PatientEnrollmentMonth")
                {
                    DataSet dtPatiEnrollMonth = (DataSet)Session["dtPatiEnrollMonth"];
                    rptDocument.SetDataSource(dtPatiEnrollMonth);
                }
                else if (theReportName == "ARVAdherence")
                {
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsClinical = (DataTable)ReportDetails.GetARVCollectionClients(thePatientId).Tables[0];
                    rptDocument.SetDataSource(dtReportsClinical);
                    ReportDetails = null;
                }
                else if (theReportName == "MisARVAppointment")
                {
                    IQCareUtils theUtil = new IQCareUtils();
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsClinical = (DataTable)ReportDetails.GetMisARVAppointClients(theDType, Convert.ToDateTime(theUtil.MakeDate(ViewState["theStartDate"].ToString()))).Tables[0];
                    ReportDetails = null;
                    rptDocument.SetDataSource(dtReportsClinical);
                    rptDocument.SetParameterValue("DateCategory", theDType);
                }
                else if (theReportName == "Nigeria-Monthly NACA Report (IQCare data)")
                {
                    ViewState["FName"] = "Nigeria-Monthly NACA Report (IQCare data)";
                    DataTable dtMonthlyNACAReportData = (DataTable)Session["dtMonthlyNACAReportData"];
                    rptDocument.SetDataSource(dtMonthlyNACAReportData);
                    //ExportNACAReportToExcel(dtMonthlyNACAReportData);
                    rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
                    rptDocument.SetParameterValue("EndDate", ViewState["theEndDate"]);
                    rptDocument.SetParameterValue("CurrentDate", System.DateTime.Today);
                }
                //Deepika
                //Int32 columnCouter;
                //columnCouter = 0;
                if ((theReportName != "CDCReport") && (theReportName != "PCustomReport") && (theReportName != "LCustomReport") && (theReportName != "PatientEnrollmentMonth")
                    && (theReportName != "Nigeria-Monthly NACA Report (IQCare data)"))
                {
                    theEnrollmentID = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-";
                    rptDocument.SetParameterValue("EnrollmentID", theEnrollmentID);
                    crViewer.EnableParameterPrompt = false;

                }

                rptDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\PView.pdf"));
                ViewState["RepName"] = theReportName;
                crViewer.ReportSource = rptDocument;
                crViewer.DataBind();
                crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                Session["Report"] = rptDocument;

                //-----------------------------------------
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

        #region Patient Prorile Function for fill Datasource
        #endregion
        #endregion

        # region System Generated Code
        #endregion

        private void writeInCell(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {
            try
            {
                Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
                //theRange.WrapText = true;
                if (tablename != "")
                    theRange.Value2 = ((DataSet)ViewState["RptData"]).Tables[tablename].Rows[0][column].ToString();
                else if (column != "")
                    theRange.Value2 = column.ToString();
                else
                    theRange.Value2 = ViewState["CntryName"].ToString();
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
    }
}