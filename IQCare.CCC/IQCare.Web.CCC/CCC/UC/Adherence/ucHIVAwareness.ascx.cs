using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Adherence;
using Entities.CCC.Adherence;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucHIVAwareness : System.Web.UI.UserControl
    {
        public int HIVStatusId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                getHIVAwareness(PatientId, PatientMasterVisitId);
            }
        }

        protected void getHIVAwareness(int PatientId, int PatientMasterVisitId)
        {
            var HA = new AdherenceLogic();
            List<HIVStatus> HIVStatusList = HA.getHIVStatus(PatientId, PatientMasterVisitId);
            foreach (var value in HIVStatusList)
            {
                HIVStatusId = value.Id;
                rbAccepted.SelectedValue = value.AcceptedStatus.ToString();
                rbDisclosureComplete.SelectedValue = value.DisclosureComplete.ToString();
            }
        }
        
    }
}