using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Encounter;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Screening;
using Entities.CCC.Screening;
using System.Web.Script.Serialization;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Visit;

namespace IQCare.Web.CCC.UC.Depression
{
    public partial class ucGBVScreening : System.Web.UI.UserControl
    {
        public int PatientId, PatientMasterVisitId, userId, SocialHistoryId;
        public DateTime? VisitDate;
        public int screenTypeId = 0, recordId = 0;
        public RadioButtonList rbList;
        public int NotesId;
        public int PmVisitId;
        public int serviceAreaId;

        protected string DateOfEnrollment
        {
            get { return Session["DateOfEnrollment"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            PmVisitId = Convert.ToInt32(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString());

            PatientMasterVisitManager VisitManager = new PatientMasterVisitManager();
            List<PatientMasterVisit> visitPatientMasterVisit = new List<PatientMasterVisit>();
            visitPatientMasterVisit = VisitManager.GetVisitDateByMasterVisitId(PatientId, PatientMasterVisitId);
            VisitDate = visitPatientMasterVisit[0].VisitDate;
            PatientLookupManager patientLookupManager = new PatientLookupManager();
            var patientDetails = patientLookupManager.GetPatientDetailSummary(PatientId);
            Session["DateOfEnrollment"] = patientDetails.EnrollmentDate;
            serviceAreaId = Convert.ToInt32(LookupLogic.GetLookupItemId("MoH 257 GREENCARD"));
            if (!IsPostBack)
            {
                //Alcohol Frequency
                getGbvScreeningCTRLs();
                getGbvScreeningData(PatientId);
            }
        }
        public void getGbvScreeningCTRLs()
        {
            LookupLogic lookUp = new LookupLogic();
            List<LookupItemView> questionsList = lookUp.getQuestions("GBVQuestions");
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                PHgbvquestions.Controls.Add(new LiteralControl("<div class='row'>"));
                PHgbvquestions.Controls.Add(new LiteralControl("<div class='col-md-10 text-left'>"));
                PHgbvquestions.Controls.Add(new LiteralControl("<label>" + i + ". " + value.ItemDisplayName + "" + "</label>"));
                PHgbvquestions.Controls.Add(new LiteralControl("</div>"));
                PHgbvquestions.Controls.Add(new LiteralControl("<div class='col-md-2 text-right'>"));
                rbList = new RadioButtonList();
                rbList.ID = "gbv" + value.ItemId.ToString();
                rbList.RepeatColumns = 4;
                rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                rbList.CssClass = "gbvrbList";
                lookUp.populateRBL(rbList, "GeneralYesNo");
                PHgbvquestions.Controls.Add(rbList);
                PHgbvquestions.Controls.Add(new LiteralControl("</div>"));
                PHgbvquestions.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHgbvquestions.Controls.Add(new LiteralControl("<hr />"));
                }
            }
        }
        public void getGbvScreeningData(int PatientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreeningByVisitId(PatientId, PatientMasterVisitId);
                //.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    screenTypeId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rblPC1Qs = (RadioButtonList)PHgbvquestions.FindControl("gbv" + value.ScreeningCategoryId.ToString());
                    if (rblPC1Qs != null)
                    {
                        rblPC1Qs.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
        }
    }
}