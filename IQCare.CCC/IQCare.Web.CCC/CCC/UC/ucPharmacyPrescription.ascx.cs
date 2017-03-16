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
        protected void Page_Load(object sender, EventArgs e)
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateDDL(ddlTreatmentPlan, "TreatmentPlan");
            lookUp.populateDDL(regimenLine, "RegimenLines");
            lookUp.getPharmacyDrugFrequency(ddlFreq);
        }
    }
}