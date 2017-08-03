using Application.Presentation;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPatientClinicalEncounter : System.Web.UI.UserControl
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
        //private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        private readonly ILookupManager _lookupItemManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            if (Request.QueryString["visitId"] != null)
            {
                Session["PatientMasterVisitId"] = Request.QueryString["visitId"].ToString();
                PatientEncounterExists = Convert.ToInt32(Request.QueryString["visitId"].ToString());
            }

            // Get Gender
            PatientLookup genderId = _patientLookupmanager.GetGenderID(Convert.ToInt32(HttpContext.Current.Session["PatientPK"]));
            if (genderId != null)
                genderID = genderId.Sex;

            LookupItemView genderType = _lookupItemManager.GetPatientGender(genderID);
            gender = genderType.ItemName;

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(tbscreeningstatus, "TBStatus");
                lookUp.populateDDL(nutritionscreeningstatus, "NutritionStatus");
                lookUp.populateDDL(AdverseEventAction, "AdverseEventsActions");
                lookUp.populateDDL(ddlAdverseEventSeverity, "ADRSeverity");
                lookUp.populateDDL(ddlVisitBy, "VisitBy");
                lookUp.populateDDL(ChronicIllnessName, "ChronicIllness");
                lookUp.populateDDL(ddlVaccine, "Vaccinations");
                lookUp.populateDDL(ddlVaccineStage, "VaccinationStages");
                lookUp.populateDDL(ddlExaminationType, "ReviewOfSystems");
                //lookUp.populateDDL(ddlExamination, "PhysicalExamination");
                lookUp.populateCBL(cblGeneralExamination, "GeneralExamination");
                lookUp.populateCBL(cblPHDP, "PHDP");
                lookUp.populateDDL(arvAdherance, "ARVAdherence");
                lookUp.populateDDL(ctxAdherance, "CTXAdherence");
                lookUp.populateDDL(ddlAllergySeverity, "ADRSeverity");
                lookUp.populateDDL(stabilityStatus, "StabilityAssessment");

                var patientVitals = new PatientVitalsManager();
                PatientVital patientTriage = patientVitals.GetByPatientId(Convert.ToInt32(Session["PatientPK"].ToString()));
                if(patientTriage != null)
                {
                    Weight = patientTriage.Weight.ToString();
                    txtWeight.Text = Weight;
                    txtHeight.Text = patientTriage.Height.ToString();
                    txtBMI.Text = patientTriage.BMI.ToString();
                }
                

                if (Convert.ToInt32(Session["PatientMasterVisitId"]) > 0)
                    loadPatientEncounter();

            }
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> statuses = mgr.GetLookItemByGroup("AppointmentStatus");
            if (statuses != null && statuses.Count > 0)
            {
                status.Items.Add(new ListItem("select", "0"));
                foreach (var k in statuses)
                {
                    status.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
                status.SelectedIndex = 1;
                status.Enabled = false;
            }

            List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            if (areas != null && areas.Count > 0)
            {
                ServiceArea.Items.Add(new ListItem("select", "0"));
                foreach (var k in areas)
                {
                    ServiceArea.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> reasons = mgr.GetLookItemByGroup("AppointmentReason");
            if (reasons != null && reasons.Count > 0)
            {
                Reason.Items.Add(new ListItem("select", "0"));
                foreach (var k in reasons)
                {
                    Reason.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> care = mgr.GetLookItemByGroup("DifferentiatedCare");
            if (care != null && care.Count > 0)
            {
                DifferentiatedCare.Items.Add(new ListItem("select", "0"));
                foreach (var k in care)
                {
                    DifferentiatedCare.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }
        }

        private void loadPatientEncounter()
        {
            Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity pce = new Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity();
            pce = PEL.loadPatientEncounter(Session["PatientMasterVisitId"].ToString(), Session["PatientPK"].ToString());

            /////PRESENTING COMPLAINTS
            visitdateval = pce.visitDate;
            LMPval = pce.lmp;
            EDDval = pce.edd;
            nxtAppDateval = pce.nextAppointmentDate;
            if (pce.visitScheduled == "1")
                vsYes.Checked = true;
            else if (pce.visitScheduled == "0")
                vsNo.Checked = true;

            //rblVisitScheduled.SelectedValue = pce.visitScheduled;
            ddlVisitBy.SelectedValue = pce.visitBy;

            if (pce.anyComplaint == "1")
                rdAnyComplaintsYes.Checked = true;
            else if (pce.anyComplaint == "0")
                rdAnyComplaintsNo.Checked = true;

            complaints.Value = pce.complaints;
            tbInfected.SelectedValue = pce.OnAntiTB;
            onIpt.SelectedValue = pce.OnIPT;

            tbscreeningstatus.SelectedValue = pce.tbScreening;
            nutritionscreeningstatus.SelectedValue = pce.nutritionStatus;
            txtWorkPlan.Text = pce.WorkPlan;
            foreach (ListItem item in cblGeneralExamination.Items)
            {
                for (int i = 0; i < pce.generalExams.Length; i++)
                {
                    if (item.Value == pce.generalExams[i])
                    {
                        item.Selected = true;
                    }
                }
            }

            

            ////PATIENT MANAGEMENT
            foreach (ListItem item in cblPHDP.Items)
            {
                for (int i = 0; i < pce.phdp.Length; i++)
                {
                    if (item.Value == pce.phdp[i])
                    {
                        item.Selected = true;
                    }
                }
            }

            arvAdherance.SelectedValue = pce.ARVAdherence;
            ctxAdherance.SelectedValue = pce.CTXAdherence;

        }
    }
}