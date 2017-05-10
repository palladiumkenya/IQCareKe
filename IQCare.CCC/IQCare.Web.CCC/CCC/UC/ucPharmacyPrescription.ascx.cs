using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Entities.CCC.Encounter.PatientEncounter;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPharmacyPrescription : System.Web.UI.UserControl
    {
        public string PMSCM = "";
        public string PMSCMSAmePointDispense = "";
        public string prescriptionDate = "";
        public string dispenseDate = "";
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
                //lookUp.populateDDL(ddlTreatmentProgram, "TreatmentProgram");
                lookUp.populateDDL(ddlPeriodTaken, "PeriodDrugsTaken");
                lookUp.populateDDL(ddlTreatmentPlan, "TreatmentPlan");
                lookUp.populateDDL(regimenLine, "RegimenClassification");//RegimenLines
                lookUp.getPharmacyDrugFrequency(ddlFreq);

                PatientEncounterLogic pel = new PatientEncounterLogic();
                pel.getPharmacyTreatmentProgram(ddlTreatmentProgram);

                loadExistingData();
            }
        }

        void loadExistingData()
        {
            LookupLogic lookUp = new LookupLogic();
            PatientEncounterLogic encounterLogic = new PatientEncounterLogic();
            List<PharmacyFields> lst = encounterLogic.getPharmacyFields(Session["PatientMasterVisitId"].ToString());
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
                var result = LookupLogic.GetLookUpItemViewByMasterName(masterName);

                JavaScriptSerializer parser = new JavaScriptSerializer();
                var regimen = parser.Deserialize<List<KeyValue>>(result);
                ddlRegimen.Items.Add(new ListItem("Select", "0"));
                for (int i = 0; i < regimen.Count; i++)
                {
                    ddlRegimen.Items.Add(new ListItem(regimen[i].DisplayName, regimen[i].ItemId));
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                ddlRegimen.SelectedValue = lst[0].Regimen;
                txtPrescriptionDate.Text = lst[0].prescriptionDate;
                txtPrescriptionDate.Text = lst[0].dispenseDate;

                prescriptionDate = lst[0].prescriptionDate;
                dispenseDate = lst[0].dispenseDate;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "regimen", "selectRegimens(" + regimenLine.SelectedValue + ");", true);
            }
        }
    }
}