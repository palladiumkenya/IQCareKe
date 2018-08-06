using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.UC
{
    public partial class ucFollowupEducation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["visitId"] != null)
            {
                Session["ExistingRecordPatientMasterVisitID"] = Request.QueryString["visitId"].ToString();
            }
            else
            {
                Session["ExistingRecordPatientMasterVisitID"] = "0";
            }

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(ddlCounsellingType, "FollowupEducation");
            }
        }

        protected void ddlCounsellingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupLogic lookUp = new LookupLogic();
            lookUp.populateDDL(ddlCounsellingTopic, "ProgressionRX");
        }
    }
}