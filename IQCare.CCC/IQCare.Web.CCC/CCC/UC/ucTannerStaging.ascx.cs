using Application.Presentation;
using Entities.CCC.Encounter;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Tanners;

namespace IQCare.Web.CCC.UC
{
    public partial class ucTannerStaging : System.Web.UI.UserControl
    {
        public int PatientEncounterExists { get; set; }
        PatientEncounterLogic PEL = new PatientEncounterLogic();
        public string visitdateval = "";
        public string LMPval = "";
        public string EDDval = "";
        public string nxtAppDateval = "";
        public int genderID;
        public string gender = "";
        public int PatientId;
        public int PatientMasterVisitId;
        public int age;
        public string Weight = "0";
        public int userId;
        public int TannersId;

        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        protected int PtnId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int PmVisitId
        {
            get { return Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : Session["patientMasterVisitId"]); }
        }

        protected string DateOfEnrollment
        {
            get { return Session["DateOfEnrollment"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(ddlBreastsGenitals, "BreastsGenitals");
                lookUp.populateDDL(ddlPubicHair, "PubicHair");
                lookUp.populateRBL(rbRecordTannersStaging, "GeneralYesNo");
                getRecordTannersStaging(PatientId);
            }
        }
        protected void getRecordTannersStaging(int PatientId)
        {
            var stgMgr = new TannersStagingManager();
            List<TannersStaging> tannersRecordList = stgMgr.getRecordTannersStaging(PatientId);
            foreach (var value in tannersRecordList)
            {
                rbRecordTannersStaging.SelectedValue = value.RecordTannersStaging.ToString();
                TannersId = value.Id;
            }
        }
    }
}