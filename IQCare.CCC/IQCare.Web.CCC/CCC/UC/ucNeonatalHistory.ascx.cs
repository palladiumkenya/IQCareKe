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

namespace IQCare.Web.CCC.UC
{
    public partial class ucNeonatalHistory : System.Web.UI.UserControl
    {
        public int PatientEncounterExists { get; set; }
        PatientEncounterLogic PEL = new PatientEncounterLogic();
        public string nxtAppDateval = "";
        public int PatientId;
        public int PatientMasterVisitId;
        public int age;
        public string Weight = "0";
        public int userId;
        public int NotesId;

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
                lookUp.populateDDL(ddlMilestoneAssessed, "MilestoneAssessed");
                lookUp.populateDDL(ddlMilestoneStatus, "MilestoneStatus");
                lookUp.populateDDL(ddlImmunizationPeriod, "ImmunizationPeriod");
                lookUp.populateDDL(ddlImmunizationGiven, "ImmunizationGiven");
                populateRadioButtons();
                getNeonatalNotes(PatientId, PatientMasterVisitId);
            }
        }

        private void getNeonatalNotes(int PatientId, int PatientMasterVisitId)
        {
            var nHtx = new NeonatalHistoryLogic();
            List<PatientNeonatalHistory> neonatalNotesList = nHtx.getNeonatalNotes(PatientId, PatientMasterVisitId);
            foreach(var value in neonatalNotesList)
            {
                neonatalhistorynotes.InnerText = value.NeonatalHistoryNotes.ToString();
                rbRecordNeonatalHistory.SelectedValue = value.RecordNeonatalHistory.ToString();
                NotesId = value.Id;
            }
        }
        protected void populateRadioButtons()
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateRBL(rbRecordNeonatalHistory, "GeneralYesNo");
        }
    }
}