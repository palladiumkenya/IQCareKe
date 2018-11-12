using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.UC
{
    public partial class ucICF : System.Web.UI.UserControl
    {
        public int PatientId;
        public int PatientMasterVisitId;
        public int age;
        public int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            age = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                populateCTRLS();
            }
        }
        public void populateCTRLS()
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateDDL(ddlOnAntiTBDrugs, "GeneralYesNo");
            lookUp.populateDDL(ddlICFCough, "GeneralYesNo");
            lookUp.populateDDL(ddlICFFever, "GeneralYesNo");
            lookUp.populateDDL(ddlICFWeight, "GeneralYesNo");
            lookUp.populateDDL(ddlICFNightSweats, "GeneralYesNo");
            lookUp.populateDDL(ddlICFRegimen, "TBRegimen");
            lookUp.populateDDL(ddlICFCurrentlyOnIPT, "GeneralYesNo");
            lookUp.populateDDL(ddlICFStartIPT, "GeneralYesNo");
            lookUp.populateDDL(ddlICFTBScreeningOutcome, "TBFindings");

            lookUp.populateDDL(ddlSputumSmear, "SputumSmear");
            lookUp.populateDDL(ddlGeneXpert, "GeneExpert");
            lookUp.populateDDL(ddlChestXray, "ChestXray");
            lookUp.populateDDL(ddlStartAntiTB, "GeneralYesNo");
            lookUp.populateDDL(ddlInvitationofContacts, "GeneralYesNo");
            lookUp.populateDDL(ddlEvaluatedforIPT, "GeneralYesNo");
        }
    }
}