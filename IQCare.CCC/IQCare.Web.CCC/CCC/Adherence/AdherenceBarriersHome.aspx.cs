using Entities.CCC.Visit;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class AdherenceBarriersHome : System.Web.UI.Page
    {
        public DateTime? VisitDate;
        public int PatientId, PatientMasterVisitId,PmVisitId,userId;
        public int serviceAreaId;
        public int PatientEncounterExists { get; set; }
        protected string DateOfEnrollment
        {
            get { return Session["DateOfEnrollment"].ToString(); }
        }


    protected void Page_Load(object sender, EventArgs e)
        {

            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);

            PatientMasterVisitManager VisitManager = new PatientMasterVisitManager();
            List<PatientMasterVisit> visitPatientMasterVisit = new List<PatientMasterVisit>();
            visitPatientMasterVisit = VisitManager.GetVisitDateByMasterVisitId(PatientId, PatientMasterVisitId);
            VisitDate = visitPatientMasterVisit[0].VisitDate;
            PatientLookupManager patientLookupManager = new PatientLookupManager();
            var patientDetails = patientLookupManager.GetPatientDetailSummary(PatientId);
            Session["DateOfEnrollment"] = patientDetails.EnrollmentDate;
            serviceAreaId = Convert.ToInt32(LookupLogic.GetLookupItemId("MoH 257 GREENCARD"));
            PmVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());
            userId = Convert.ToInt32(Session["AppUserId"]);



            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
                PatientEncounterExists = Convert.ToInt32(Request.QueryString["visitId"].ToString());
            }

        }
    }
}