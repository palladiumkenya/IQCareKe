using IQCare.CCC.UILogic;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Encounter
{
    public partial class PatientEncounter : System.Web.UI.Page
    {
        public int PatientId;
        PatientEncounterLogic PEL = new PatientEncounterLogic();
        public int visitId = 0;
        public int PatientMasterVisitId = 0;
        public string visitdateval = "";
        public string LMPval = "";
        public string EDDval = "";
        public string nxtAppDateval = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);

            if (Request.QueryString["visitId"] != null)
            {
                visitId = int.Parse(Request.QueryString["visitId"].ToString());
                Session["PatientMasterVisitId"] = Request.QueryString["visitId"].ToString();
            }

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(tbscreeningstatus, "TBStatus");
                lookUp.populateDDL(nutritionscreeningstatus, "NutritionStatus");
                lookUp.populateDDL(onFP, "FPStatus");
                lookUp.populateDDL(fpMethod, "FPMethod");
                lookUp.populateDDL(examinationPregnancyStatus, "PregnancyStatus");
                lookUp.populateDDL(cacxscreening, "CaCxScreening");
                lookUp.populateDDL(stiScreening, "STIScreening");
                lookUp.populateDDL(stiPartnerNotification, "STIPartnerNotification");
                lookUp.populateDDL(ddlAdverseEventSeverity, "ADRSeverity");
                lookUp.populateDDL(ddlVisitBy, "VisitBy");
                lookUp.populateDDL(ChronicIllnessName, "ChronicIllness");
                lookUp.populateDDL(ddlVaccine, "Vaccinations");
                lookUp.populateDDL(ddlVaccineStage, "VaccinationStages");
                lookUp.populateDDL(ddlNoFP, "NoFamilyPlanning");
                lookUp.populateDDL(ddlExaminationType, "ExaminationType");
                lookUp.populateDDL(ddlExamination, "PhysicalExamination");
                lookUp.populateCBL(cblPHDP, "PHDP");
                lookUp.populateDDL(ddlReferredFor, "AppointmentType");
                lookUp.populateDDL(arvAdherance, "ARVAdherence");
                lookUp.populateDDL(ctxAdherance, "CTXAdherence");

                if (visitId > 0)
                    loadPatientEncounter();


            }
        }
        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
        }

        private void loadPatientEncounter()
        {
            Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity pce = new Entities.CCC.Encounter.PatientEncounter.PresentingComplaintsEntity();
            pce = PEL.loadPatientEncounter(visitId.ToString(), Session["PatientId"].ToString());

            /////PRESENTING COMPLAINTS
            visitdateval = pce.visitDate;
            LMPval = pce.lmp;
            EDDval = pce.edd;
            nxtAppDateval = pce.nextAppointmentDate;

            rblVisitScheduled.SelectedValue = pce.visitScheduled;
            ddlVisitBy.SelectedValue = pce.visitBy;
            complaints.Value = pce.complaints;
            tbscreeningstatus.SelectedValue = pce.tbScreening;
            nutritionscreeningstatus.SelectedValue = pce.nutritionStatus;
            examinationPregnancyStatus.SelectedValue = pce.pregStatus;
            rblANCProfile.SelectedValue = pce.ancProfile;
            onFP.SelectedValue = pce.onFP;
            fpMethod.SelectedValue = pce.fpMethod;
            //nofp
            cacxscreening.SelectedValue = pce.CaCX;
            stiScreening.SelectedValue = pce.STIScreening;
            stiPartnerNotification.SelectedValue = pce.STIPartnerNotification;

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
            ddlReferredFor.SelectedValue = pce.nextAppointmentType;

        }

    }

}