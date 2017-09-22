using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPharmacyPrescription : System.Web.UI.UserControl
    {
        public string PMSCM = "0";
        public string PMSCMSAmePointDispense = "0";
        public string prescriptionDate = "";
        public string dispenseDate = "";
        public bool StartTreatment { get; set; }
        public string patType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["visitId"] != null)
            {
                Session["PatientMasterVisitId"] = Request.QueryString["visitId"].ToString();
            }

            if (!IsPostBack)
            {
                if (Session["SCMModule"] != null)
                    PMSCM = Session["SCMModule"].ToString();

                if (Session["SCMSamePointDispense"] != null)
                    PMSCMSAmePointDispense = Session["SCMSamePointDispense"].ToString();

                LookupLogic lookUp = new LookupLogic();
                PatientTreatmentTrackerManager treatmentTrackerManager = new PatientTreatmentTrackerManager();

                StartTreatment = treatmentTrackerManager.HasPatientTreatmentStarted(Convert.ToInt32(Session["PatientPK"].ToString()));

                int patientType = Convert.ToInt32(Session["PatientType"].ToString());
                patType = LookupLogic.GetLookupNameById(patientType).ToLower();

                if (patType == "transit")
                {
                    StartTreatment = true;
                }
                //lookUp.populateDDL(ddlTreatmentProgram, "TreatmentProgram");
                lookUp.populateDDL(ddlPeriodTaken, "PeriodDrugsTaken");
                lookUp.populateDDL(ddlTreatmentPlan, "TreatmentPlan");
                if(Convert.ToInt32(Session["Age"]) > 14)
                {
                    lookUp.populateDDL(regimenLine, "RegimenClassificationAdult", "RegimenClassificationPaeds");
                }
                else
                {
                    lookUp.populateDDL(regimenLine, "RegimenClassificationPaeds");
                }
                
                lookUp.getPharmacyDrugFrequency(ddlFreq);

                PatientEncounterLogic pel = new PatientEncounterLogic();
                pel.getPharmacyTreatmentProgram(ddlTreatmentProgram);

                LoadExistingData();
            }
        }

        void LoadExistingData()
        {
            LookupLogic lookUp = new LookupLogic();
            PatientEncounterLogic encounterLogic = new PatientEncounterLogic();

            List<Entities.CCC.Triage.PatientEncounter.PharmacyFields> lst = encounterLogic.getPharmacyFields(Session["PatientMasterVisitId"].ToString());
            if (lst.Count > 0)
            {
                ddlTreatmentProgram.SelectedValue = lst[0].TreatmentProgram;
                ddlPeriodTaken.SelectedValue = lst[0].PeriodTaken;
                ddlTreatmentPlan.SelectedValue = lst[0].TreatmentPlan;

                DataTable theDT = encounterLogic.getPharmacyDrugSwitchInterruptionReason(ddlTreatmentPlan.SelectedValue);
                ddlSwitchInterruptionReason.Items.Add(new ListItem("Select", "0"));
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ddlSwitchInterruptionReason.Items.Add(new ListItem(theDT.Rows[i]["DisplayName"].ToString(), theDT.Rows[i]["LookupItemId"].ToString()));
                }
                ddlSwitchInterruptionReason.SelectedValue = lst[0].TreatmentPlanReason;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "treatmentPlan", "drugSwitchInterruptionReason(" + ddlTreatmentPlan.SelectedValue + ");", true);

                regimenLine.SelectedValue = lst[0].RegimenLine;
                //////////////////////////////////////////////////////////////////////////////////////

                //var masterName = Regex.Replace(regimenLine.SelectedItem.Text, @"\s+", "");
                var masterName = regimenLine.SelectedItem.Text.Replace(" ", String.Empty);

                ///////////////////////////////////////////////////////////////////////////
                PatientEncounterLogic patientEncounter = new PatientEncounterLogic();

                DataTable theDTReg = patientEncounter.getPharmacyRegimens(masterName);
                ddlRegimen.Items.Add(new ListItem("Select", "0"));

                foreach (DataRow row in theDTReg.Rows)
                {
                    ddlRegimen.Items.Add(new ListItem(row["DisplayName"].ToString(), row["LookupItemId"].ToString()));
                }
                ////////////////////////////////////////////

                //var result = LookupLogic.GetLookUpItemViewByMasterName(masterName);

                //JavaScriptSerializer parser = new JavaScriptSerializer();
                //var regimen = parser.Deserialize<List<Entities.CCC.Encounter.PatientEncounter.KeyValue>>(result);
                //ddlRegimen.Items.Add(new ListItem("Select", "0"));
                //for (int i = 0; i < regimen.Count; i++)
                //{
                //    ddlRegimen.Items.Add(new ListItem(regimen[i].DisplayName, regimen[i].ItemId));
                //}
                /////////////////////////////////////////////////////////////////////////////////////////
                ddlRegimen.SelectedValue = lst[0].Regimen;
                txtPrescriptionDate.Text = lst[0].prescriptionDate;
                txtPrescriptionDate.Text = lst[0].dispenseDate;

                prescriptionDate = lst[0].prescriptionDate;
                dispenseDate = lst[0].dispenseDate;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "regimen", "selectRegimens(" + regimenLine.SelectedValue + ");", true);
            }
        }

        protected void ddlTreatmentProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}