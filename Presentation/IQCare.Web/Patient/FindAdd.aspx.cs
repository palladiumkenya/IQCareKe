using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Clinical;
using IQCare.Web.UILogic;

namespace IQCare.Web.Patient
{
    public partial class FindAdd : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            HtmlGenericControl lblServiceArea = (HtmlGenericControl)FindPatient.FindControl("lblServiceArea");
            lblServiceArea.InnerText = Request.QueryString["srvNm"];

            if (Convert.ToInt32(Session["Paperless"]) != 1)//waiting list is available only in paperless mode
            {
                btnWaitingList.Visible = false;
            }

            Session["HIVPatientStatus"] = 0;
            Session["PMTCTPatientStatus"] = 0;
            //SetEnrollmentCombo();
            Session["PatientId"] = 0;
            SessionManager.PatientId = 0;
            Session["TechnicalAreaName"] = Request.QueryString["srvNm"];
            Session["TechnicalAreaId"] = Request.QueryString["mod"];
            base.Session["TechIdentifier"] = null;

            string urlParam = string.Format("../Queue/WaitingListView.aspx?srvNM={0}&mod={1}", Request.QueryString["srvNm"], Request.QueryString["mod"]);
            btnWaitingList.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", urlParam);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Request.QueryString["mod"] == "0")
            {

                // FindPatient.Attributes.Add("IncludeEnrollement", "True");
                FindPatient.IncludeEnrollement = true;
            }
            else
            {
                FindPatient.IncludeEnrollement = false;
                int moduleId = int.Parse(Request.QueryString["mod"]);
                if (moduleId > 0)
                    FindPatient.SelectedServiceLine = moduleId;
            }
            FindPatient.SelectedPatientChanged += new CommandEventHandler(FindPatient_SelectedPatientChanged);
            FindPatient.PatientEnrollmentChanged += FindPatient_PatientEnrollmentChanged;

        }

        private void FindPatient_PatientEnrollmentChanged(object sender, CommandEventArgs e)
        {
            int locationId, patientId;

            List<KeyValuePair<string, object>> param = e.CommandArgument as List<KeyValuePair<string, object>>;
            patientId = (int)param.Find(l => l.Key == "PatientID").Value;
            locationId = (int)param.Find(l => l.Key == "LocationID").Value;

            if (locationId == Convert.ToInt32(base.Session["AppLocationId"]))
            {
                openPatientDetails(patientId);
            }
            else
            {
                string script = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
        }

        void FindPatient_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            int locationId, patientId;

            List<KeyValuePair<string, object>> param = e.CommandArgument as List<KeyValuePair<string, object>>;
            patientId = (int)param.Find(l => l.Key == "PatientID").Value;
            locationId = (int)param.Find(l => l.Key == "LocationID").Value;

            if (locationId == SessionManager.PatientId)
            {
                openPatientDetails(patientId);
            }
            else
            {
                string script = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
        }

        private void openPatientDetails(int patientID)
        {

            HttpContext.Current.Session["PatientId"] = patientID;
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
            HttpContext.Current.Session["PatientInformation"] = null;

            SessionManager.PatientId = patientID;
            SessionManager.VisitId = 0;
           

            string theUrl = "";
            if (Request.QueryString["srvNm"] == "Records" || Request.QueryString["srvNm"] == "Consultation")
            {
                Session["TechnicalAreaName"] = "Records";
                theUrl = string.Format("{0}?PatientId={1}", "./AddTechnicalArea.aspx", patientID);
                Response.Redirect(theUrl);
            }
            else
            {
                //Check if the patient is enrolled and go directly to patient home page if patient is enrolled
                IPatientRegistration PatRegMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = PatRegMgr.GetFieldNames(int.Parse(Request.QueryString["mod"]), patientID);
                if (theDS.Tables[3].Rows.Count > 0)//check if the patient was care ended for reenrolment
                {
                    theUrl = string.Format("{0}?PatientId={1}&mod={2}", "./AddTechnicalArea.aspx", patientID, Request.QueryString["mod"]);
                }
                else if (theDS.Tables[2].Rows.Count > 0 && theDS.Tables[2].Rows[0]["StartDate"].ToString() != "")
                {
                    Session["TechnicalAreaId"] = Request.QueryString["mod"];
                    theUrl = "~/ClinicalForms/frmPatient_Home.aspx";

                }
                else
                {
                    theUrl = string.Format("{0}?PatientId={1}&mod={2}", "./AddTechnicalArea.aspx", patientID, Request.QueryString["mod"]);

                }
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);


            }
        }
    }
}