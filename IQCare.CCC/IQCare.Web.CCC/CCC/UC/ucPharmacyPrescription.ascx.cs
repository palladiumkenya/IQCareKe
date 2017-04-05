using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucPharmacyPrescription : System.Web.UI.UserControl
    {
        public string PMSCM = "";
        public string PMSCMSAmePointDispense = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SCMModule"] != null)
                PMSCM = Session["SCMModule"].ToString();

            if (Session["SCMSamePointDispense"] != null)
                PMSCMSAmePointDispense = Session["SCMSamePointDispense"].ToString();

            LookupLogic lookUp = new LookupLogic();
            lookUp.populateDDL(ddlTreatmentProgram, "TreatmentProgram");
            lookUp.populateDDL(ddlPeriodTaken, "PeriodDrugsTaken");
            lookUp.populateDDL(ddlTreatmentPlan, "TreatmentPlan");
            lookUp.populateDDL(regimenLine, "RegimenLines");
            lookUp.getPharmacyDrugFrequency(ddlFreq);
        }
    }
}