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
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;

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
        public string AppointmentId;
        public Boolean IsEditAppointment = false;
        public int IsEditAppointmentId = 0;
        public RadioButtonList rbList;
        public TextBox notesTb;
        public int NotesId;
        public int userId;
        public int screenTypeId;

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

        protected ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected int GbvScreeningCategoryId
        {
            get
            {
                var gbvAssessmentId = Convert.ToInt32(lookupManager.GetLookupItemId("GBVAssessment"));
                return gbvAssessmentId;
            }
        }

        //private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        private readonly ILookupManager _lookupItemManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
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
                //Patient Nutrition assessment notes and screening
                getPatientNotesandScreening();
                populatePNS();
                getPNSData();

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
            if (age < 5)
            {
                Control NeonatalHistoryCtrl = Page.LoadControl("~/CCC/UC/ucNeonatalHistory.ascx");
                //divControls.Controls.Clear();
                NeonatalHistoryPH.Controls.Add(NeonatalHistoryCtrl);
            }

            if (age>=9 && age<= 19)
            {
                Control TannerStagingCtrl = Page.LoadControl("~/CCC/UC/ucTannerStaging.ascx");
                TannersStagingPH.Controls.Add(TannerStagingCtrl);
            }

            Control SocialHoistoryCtrl = Page.LoadControl("~/CCC/UC/ucSocialHistory.ascx");
            SocialHistoryPH.Controls.Add(SocialHoistoryCtrl);
        }
        private void populatePNS()
        {
            LookupLogic lookUp = new LookupLogic();
            LookupItemView[] questionsList = lookUp.getQuestions("NutritionAssessment").ToArray();
            int i = 0;
            foreach (var value in questionsList)
            {
                i = i + 1;
                screenTypeId = value.MasterId;
                string radioItems = "";
                int notesValue = 0;
                LookupItemView[] itemList = lookUp.getQuestions(value.ItemName).ToArray();
                if (itemList.Any())
                {
                    foreach (var items in itemList)
                    {
                        if (items.ItemName == "Notes")
                        {
                            notesValue = items.ItemId;
                        }
                        else
                        {
                            radioItems = items.ItemName;
                        }
                    }
                }
                PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-12' id='" + value.ItemName + "'>"));
                //Rdaios start
                if (radioItems != "")
                {
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-8 text-left'>"));
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<label>" + value.ItemDisplayName + "" + "</label>"));
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("</div>"));
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-4 text-right'>"));
                    rbList = new RadioButtonList();
                    rbList.ID = "nutritionarb" + value.ItemId.ToString();
                    rbList.RepeatColumns = 2;
                    rbList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    rbList.CssClass = "narbList";
                    lookUp.populateRBL(rbList, radioItems);
                    PHNutritionScreeningNotes.Controls.Add(rbList);
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("</div>"));
                }
                else
                {
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<label>" + value.ItemDisplayName + "" + radioItems + "</label>"));
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("</div>"));
                }

                //Radios end
                //notes start
                if (notesValue > 0)
                {
                    if (radioItems == "GeneralYesNo")
                    {
                        PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-12 text-left notessection'>"));
                    }
                    else
                    {
                        PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<div class='col-md-12 text-left'>"));
                    }

                    NotesId = value.ItemId;
                    notesTb = new TextBox();
                    notesTb.TextMode = TextBoxMode.MultiLine;
                    notesTb.CssClass = "form-control input-sm";
                    notesTb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    notesTb.ID = "nutritionatb" + value.ItemId.ToString();
                    notesTb.Rows = 3;
                    PHNutritionScreeningNotes.Controls.Add(notesTb);
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("</div>"));
                }
                //notes end
                PHNutritionScreeningNotes.Controls.Add(new LiteralControl("</div>"));
                var lastItem = questionsList.Last();
                if (!value.Equals(lastItem))
                {
                    PHNutritionScreeningNotes.Controls.Add(new LiteralControl("<hr />"));
                }
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
            AppointmentId = pce.appointmentId;
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
        public void getPNSData()
        {
            var PCN = new PatientClinicalNotesLogic();
            List<PatientClinicalNotes> notesList = PCN.getPatientClinicalNotes(PatientId);
            if (notesList.Any())
            {
                foreach (var value in notesList)
                {
                    //RefId = Convert.ToInt32(value.NotesCategoryId);
                    TextBox ntb = (TextBox)PHNutritionScreeningNotes.FindControl("nutritionatb" + value.NotesCategoryId.ToString());
                    if (ntb != null)
                    {
                        ntb.Text = value.ClinicalNotes;
                    }
                }
            }

            var PSM = new PatientScreeningManager();
            List<PatientScreening> screeningList = PSM.GetPatientScreening(PatientId);
            if (screeningList != null)
            {
                foreach (var value in screeningList)
                {
                    //RefId = Convert.ToInt32(value.ScreeningTypeId);
                    RadioButtonList rbl = (RadioButtonList)PHNutritionScreeningNotes.FindControl("nutritionarb" + value.ScreeningCategoryId.ToString());
                    if (rbl != null)
                    {
                        rbl.SelectedValue = value.ScreeningValueId.ToString();
                    }
                }
            }
        }
        public void getPatientNotesandScreening()
        {
            var PCN = new PatientClinicalNotesLogic();
            var PSM = new PatientScreeningManager();
            //get screening data
            PatientScreening[] patientScreeningData = PSM.GetPatientScreening(PatientId).ToArray();
            Session["patientScreeningData"] = patientScreeningData;
            //get notes data
            PatientClinicalNotes[] patientNotesData = PCN.getPatientClinicalNotes(PatientId).ToArray();
            Session["patientNotesData"] = patientNotesData;
        }
    }
}