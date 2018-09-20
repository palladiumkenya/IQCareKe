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
using IQCare.CCC.UILogic.Encounter;
using System.Web.Script.Serialization;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic.Screening;
using System.Linq;

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
        public RadioButtonList rbList;
        public int screenTypeId = 0;
        public int tannersScreeningValue = 0;

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
                populateCtrls();
                getRecordTannersStaging(PatientId);
            }
        }
        protected void getRecordTannersStaging(int PatientId)
        {
            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                
                foreach (var value in screeningList)
                {
                    tannersScreeningValue = tannersScreeningValue + 1;
                    RadioButtonList rbl = (RadioButtonList)PHTannersStaging.FindControl(value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
        }
        protected void populateCtrls()
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateDDL(ddlBreastsGenitals, "BreastsGenitals");
            lookUp.populateDDL(ddlPubicHair, "PubicHair");
            string jsonObject = "[]";
            jsonObject = LookupLogic.GetLookupItemByName("TannersStaging");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<LookupItemView> lookupList = ser.Deserialize<List<LookupItemView>>(jsonObject);
            foreach (var value in lookupList)
            {
                if (value.ItemName == "TannersRecord")
                {
                    screenTypeId = value.MasterId;
                    PHTannersStaging.Controls.Add(new LiteralControl("<label class='control-label  pull-left text-primary'>" + value.ItemDisplayName + "</label>"));
                    rbList = new RadioButtonList();
                    rbList.ID = value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "rbList";
                    rbList.SelectedValue = "104";
                    lookUp.populateRBL(rbList, "GeneralYesNo");
                    PHTannersStaging.Controls.Add(rbList);
                    RadioButtonList rbl = (RadioButtonList)PHTannersStaging.FindControl(value.ItemId.ToString());
                    rbl.SelectedValue = LookupLogic.GetLookupItemId("No");
                }
            }
        }
    }
}