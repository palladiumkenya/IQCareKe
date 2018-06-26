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
using System.Linq;
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
        public Boolean IsEditAppointment = false;
        public int IsEditAppointmentId = 0;

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

        protected DateTime NextAppointmentDate { get; set; }


        //private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        private readonly ILookupManager _lookupItemManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
                PatientEncounterExists = Convert.ToInt32(Request.QueryString["visitId"].ToString());
            }
            else
            {
                Session["ExistingRecordPatientMasterVisitID"] = "0";

                ///////Check if visit is scheduled
                if (PEL.isVisitScheduled(HttpContext.Current.Session["PatientPK"].ToString()) > 0)
                    vsYes.Checked = true;
                else
                    vsNo.Checked = true;
            }

            // Get Gender
            PatientLookup genderId = _patientLookupmanager.GetGenderID(Convert.ToInt32(HttpContext.Current.Session["PatientPK"]));
            if (genderId != null)
                genderID = genderId.Sex;

            LookupItemView genderType = _lookupItemManager.GetPatientGender(genderID);
            gender = genderType.ItemName;

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


            List<LookupItemView> whoStage = mgr.GetLookItemByGroup("WHOStage");
            if (whoStage != null && whoStage.Count > 0)
            {
                WHOStage.Items.Add(new ListItem("select", ""));
                foreach (var k in whoStage)
                {
                    WHOStage.Items.Add(new ListItem(k.ItemName, k.ItemId.ToString()));
                }
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
                //lookUp.populateDDL(WHOStage, "WHOStage");

                var patientVitals = new PatientVitalsManager();
                PatientVital patientTriage = patientVitals.GetByPatientId(Convert.ToInt32(Session["PatientPK"].ToString()));
                if (patientTriage != null)
                {
                    Weight = patientTriage.Weight.ToString();
                    txtWeight.Text = Weight;
                    txtHeight.Text = patientTriage.Height.ToString();
                    txtBMI.Text = patientTriage.BMI.ToString();
                    txtBMIZ.Text = patientTriage.BMIZ.ToString();
                }


                //if (Convert.ToInt32(Session["PatientMasterVisitId"]) > 0)
                loadPatientEncounter();
                
            }   
        }

        private void loadPatientEncounter()
        {
            Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity pce = new Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity();
            pce = PEL.loadPatientEncounter(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            PatientAppointmentManager patientAppointmentManager = new PatientAppointmentManager();

            PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

            DataTable theDT = patientEncounter.loadPatientEncounterPhysicalExam(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            DataTable theDTAdverse = patientEncounter.loadPatientEncounterAdverseEvents(Session["ExistingRecordPatientMasterVisitID"].ToString() == "0" ? Session["PatientMasterVisitID"].ToString() : Session["ExistingRecordPatientMasterVisitID"].ToString(), Session["PatientPK"].ToString());
            bool isOnEdit = false;

            /////PRESENTING COMPLAINTS
            visitdateval = pce.visitDate;
            //if (pce.visitDate != "")
            //    visitdateval = pce.visitDate;
            //else
            //    visitdateval = "";

            LMPval = pce.lmp;
            EDDval = pce.edd;
            nxtAppDateval = pce.nextAppointmentDate;
            if (!String.IsNullOrWhiteSpace(pce.visitScheduled))
            {
                isOnEdit = true;
            }

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
            EverBeenOnIpt.SelectedValue = pce.EverBeenOnIPT;

            cough.SelectedValue = pce.Cough;
            fever.SelectedValue = pce.Fever;
            weightLoss.SelectedValue = pce.NoticeableWeightLoss;
            nightSweats.SelectedValue = pce.NightSweats;

            sputum.SelectedValue = pce.SputumSmear;
            geneXpert.SelectedValue = pce.geneXpert;
            chest.SelectedValue = pce.ChestXray;
            antiTb.SelectedValue = pce.startAntiTB;
            contactsInvitation.SelectedValue = pce.InvitationOfContacts;
            iptEvaluation.SelectedValue = pce.EvaluatedForIPT;

            IptCw.IPTurineColour.SelectedValue = pce.YellowColouredUrine;
            IptCw.IPTNumbness.SelectedValue = pce.Numbness;
            IptCw.IPTYellowEyes.SelectedValue = pce.YellownessOfEyes;
            IptCw.IPTAbdominalTenderness.SelectedValue = pce.AdominalTenderness;
            IptCw.IPTLiverTest.Text = pce.LiverFunctionTests;
            IptCw.IPTStartIPT.SelectedValue = pce.startIPT;
            IptCw.StartDateIPT.Text = pce.IPTStartDate;


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
            WHOStage.SelectedValue = pce.WhoStage;

            if (theDT.Rows.Count > 0 && isOnEdit)
            {
                systemsOkNo.Checked = true;
            }
            else if (theDT.Rows.Count == 0 && isOnEdit)
            {
                systemsOkYes.Checked = true;
            }

            if (theDTAdverse.Rows.Count > 0 && isOnEdit)
            {
                rdAnyAdverseEventsYes.Checked = true;
            }
            else if(theDTAdverse.Rows.Count == 0 && isOnEdit)
            {
                rdAnyAdverseEventsNo.Checked = true;
            }

            AppointmentDate.Text = pce.nextAppointmentDate;

            NextAppointmentDate = Convert.ToDateTime(pce.nextAppointmentDate);
            //if (pce.nextAppointmentDate != "")
            //{
            //    if (pce.nextAppointmentDate != null)
            //        AppointmentDate.Text = DateTime.Parse(pce.nextAppointmentDate.Trim()).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            //}
            ServiceArea.SelectedValue = pce.appointmentServiceArea;
            Reason.SelectedValue = pce.appointmentReason;
            DifferentiatedCare.SelectedValue = pce.nextAppointmentType;
            description.Text = pce.appointmentDesc; 
           IsEditAppointment= (pce.nextAppointmentType != null);
            // IsEditAppointmentId=(pce.)
            //status.SelectedValue = pce.appontmentStatus;
            if (IsEditAppointment)
            {
                if (!string.IsNullOrWhiteSpace(pce.nextAppointmentType))
                {
                    var app = patientAppointmentManager.GetByPatientId((int)Session["PatientPK"])
                        .Where(x => x.AppointmentDate == Convert.ToDateTime(pce.nextAppointmentDate)).ToList();
                    if (app != null)
                    {
                        IsEditAppointmentId = app[0].Id;
                    }

                }
            }

            //AppointmentDate.Text = pce.nextAppointmentDate.ToString();
            //ipt pop ups
            Page.ClientScript.RegisterStartupScript(this.GetType(), "tbInfectedYesNo", "tbInfectedChange();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "IcfChange", "IcfChange();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "IcfActionChange", "IcfActionChange();", true);
            
        }
    }
}