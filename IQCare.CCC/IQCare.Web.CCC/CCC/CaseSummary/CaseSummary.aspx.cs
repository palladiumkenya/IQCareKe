using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.ClinicalSummary;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.CaseSummary
{
    public partial class CaseSummary : System.Web.UI.Page
    {
        string response = string.Empty;
        protected string DateOfEnrollment
        {
            get { return Session["DateOfEnrollment"].ToString(); }
        }

        public int PatientEncounterExists { get; set; }

        public int caseSummaryId, PatientId, PatientMasterVisitId, userId, NotesId,visitPatientMasterVisitId;
        public TextBox notesTb;
        public DateTime? VisitDate;
        protected void Page_Load(object sender, EventArgs e)
         {

            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            PatientMasterVisitManager VisitManager = new PatientMasterVisitManager();
            List<PatientMasterVisit> visitPatientMasterVisit = new List<PatientMasterVisit>();
            visitPatientMasterVisit = VisitManager.GetVisitDateByMasterVisitId(PatientId, PatientMasterVisitId);
            VisitDate = visitPatientMasterVisit[0].VisitDate;
             visitPatientMasterVisitId = visitPatientMasterVisit[0].Id;
            PatientLookupManager patientLookupManager = new PatientLookupManager();
            var patientDetails = patientLookupManager.GetPatientDetailSummary(PatientId);
            Session["DateOfEnrollment"] = patientDetails.EnrollmentDate;
            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
                PatientEncounterExists = Convert.ToInt32(Request.QueryString["visitId"].ToString());
            }
            //if (Request.QueryString["data"].ToString() == "getdata")
            //{
            //    response = GetClinicalSummaryData(Convert.ToInt32(PatientId), visitPK, locationId);
            //    SendResponse(response);
            //}
            //        int PatientId = 0;
            //        int visitPK = 0;
            //        int locationId = 0;
            //        int userId = 0;
            //        if (!IsPostBack)
            //        {
            //            //try
            //            //{
            //            //if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            //            //{
            //            //    Response.Redirect("~/frmlogin.aspx", true);
            //            //}

            //            if (!object.Equals(Session["PatientId"], null))
            //            {
            //                PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            //                //PatientId = Convert.ToInt32(Session["PatientId"]);
            //                if (PatientId == 0)
            //                {
            //                    Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx", true);
            //                }
            //                this.hidPID.Value = PatientId.ToString();
            //            }
            //            if (!object.Equals(Session["PatientVisitId"], null))
            //            {
            //                if (!object.Equals(Request.QueryString["add"], null))
            //                {
            //                    if (Request.QueryString["add"].ToString() == "0")
            //                    {
            //                        visitPK = 0;
            //                        Session["PatientVisitId"] = "0";
            //                    }
            //                }
            //                else
            //                {
            //                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            //                }
            //            }
            //            else
            //            {
            //                if (!object.Equals(Request.QueryString["add"], null))
            //                {
            //                    if (Request.QueryString["add"].ToString() == "0")
            //                    {
            //                        visitPK = 0;
            //                    }
            //                }
            //            }
            //            this.hidVId.Value = visitPK.ToString();

            //            if (!object.Equals(Session["AppLocationId"], null))
            //            {
            //                locationId = Convert.ToInt32(Session["AppLocationId"]);
            //            }
            //            if (!object.Equals(Session["AppUserId"], null))
            //            {
            //                userId = Convert.ToInt32(Session["AppUserId"]);
            //            }
            //            if (!object.Equals(Session["TechnicalAreaName"], null))
            //            {
            //                Label lblFacility = (Label)this.ucCaseSummary.FindControl("lblFacility");
            //                if (lblFacility != null)
            //                    lblFacility.Text = Session["TechnicalAreaName"].ToString();
            //                this.hidsrvNm.Value = Session["TechnicalAreaName"].ToString();
            //            }
            //            if (!object.Equals(Session["TechnicalAreaId"], null))
            //            {
            //                this.hidMOD.Value = Session["TechnicalAreaId"].ToString();
            //            }

            //            //  Authenticate();

            //            if (!object.Equals(Request.QueryString["data"], null))
            //            {
            //                string response = string.Empty;


            //                if (Session["AppLocation"] == null || Session.Count == 0 || Session["AppUserID"].ToString() == "")
            //                {
            //                 ////   CLogger.WriteLog(ELogLevel.ERROR, "Session expired!!");

            //                 //   ResponseType responsetype = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False), ErrorMessage = "Session expired" };
            //                 //   response = SerializerUtil.ConverToJson<ResponseType>(responsetype);
            //                 //   SendResponse(response);
            //                }

            //                if (Request.QueryString["data"].ToString() == "getdata")
            //                {
            //                    response = GetClinicalSummaryData(Convert.ToInt32(PatientId), visitPK, locationId);
            //                    SendResponse(response);
            //                }
            //                if (Request.QueryString["data"].ToString() == "savedata")
            //                {
            //                    System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
            //                    string jsonString = "";
            //                    jsonString = sr.ReadToEnd();
            //                    // response = SaveUpdateClinicalSummaryData(jsonString, PatientId, visitPK, locationId, userId);
            //                    SendResponse(response);
            //                }

            //            }

            //        }
            //    }
            //    private void SendResponse(string data)
            //    {
            //        //try
            //        //{
            //        Response.Clear();
            //        Response.ContentType = "application/json";
            //        Response.AddHeader("Content-type", "text/json");
            //        Response.AddHeader("Content-type", "application/json");
            //        Response.Write(data);
            //        Response.End();
            //        //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //}
            //    }
            //    private string GetClinicalSummaryData(int ptn_pk, int visitPK, int locationId)
            //    {
            //        string result = string.Empty;
            //        try
            //        {
            //            IClinicalSummaryForm ipatientClinicalSummary;
            //            ipatientClinicalSummary = (IClinicalSummaryForm)ObjectFactory.CreateInstance("BusinessProcess.CCC.ClinicalSummary.BClinicalSummary, BusinessProcess.CCC.ClinicalSummary");
            //          //  ClinicalSummary patientClinicalSummary = ipatientClinicalSummary.GetClinicalSummaryData(ptn_pk, visitPK, locationId);
            //            result = ConverToJson(ipatientClinicalSummary.GetClinicalSummaryData(ptn_pk, visitPK, locationId));
            //          //result = SerializerUtil.ConverToJson<ClinicalSummary>(patientClinicalSummary);
            //        }
            //        catch (Exception ex)
            //        {
            //            string str = "ptn_pk: " + ptn_pk.ToString() + ",visitPK: " + visitPK.ToString() + ",locationId:" + locationId.ToString();
            //            //CLogger.WriteLog(ELogLevel.ERROR, "GetClinicalSummaryData() exception: " + str + "-" + ex.ToString());
            //            //ResponseType response = new ResponseType() { Success = EnumUtil.GetEnumDescription(Success.False) };
            //            //result = SerializerUtil.ConverToJson<ResponseType>(response);
            //        }
            //        finally
            //        {

            //        }
            //        return result;
            //    }

            //    private string ConverToJson(IClinicalSummaryForm clinicalSummaryForm)
            //    {
            //        throw new NotImplementedException();
            //    }
            //}
        }
    }
}