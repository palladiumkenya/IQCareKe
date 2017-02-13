using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Encounter
{
    public partial class PatientEncounter : System.Web.UI.Page
    {
        PatientEncounterLogic PEL = new PatientEncounterLogic();
        public string serversideval = "0";
        int visitId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["visitId"] != null)
                visitId = int.Parse(Request.QueryString["visitId"].ToString());
                

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

                if(visitId > 0)
                    loadPatientEncounter();
                PEL.EncounterHistory(TreeViewEncounterHistory);

            }
        }

        private void loadPatientEncounter()
        {
            DataSet theDS = PEL.loadPatientEncounter(visitId, "1");
            VisitDate.Text = theDS.Tables[0].Rows[0]["visitDate"].ToString();
            if (theDS.Tables[0].Rows[0]["visitDate"].ToString() == "1")
                scheduledYes.Checked = true;
            else
                scheduledNo.Checked = true;
        }

    }

}