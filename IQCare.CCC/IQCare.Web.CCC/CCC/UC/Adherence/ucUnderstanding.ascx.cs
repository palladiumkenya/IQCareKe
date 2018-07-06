using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQCare.CCC.UILogic.Adherence;
using Entities.CCC.Adherence;

namespace IQCare.Web.CCC.UC.Adherence
{
    public partial class ucUnderstanding : System.Web.UI.UserControl
    {
        public int understandingId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);

            if (!IsPostBack)
            {
                getUnderstanding(PatientId, PatientMasterVisitId);
            }
        }

        protected void getUnderstanding(int patientId, int patientMasterVisitId)
        {
            var _understanding = new AdherenceLogic();
            List<UnderstandingHIV> understandingList = _understanding.getUnderstandingHIV(patientId, patientMasterVisitId);
            foreach (var value in understandingList)
            {
                understandingId = value.Id;
                rbUnderstandHIVEffects.SelectedValue = value.UnderstandHIVEffects.ToString();
                rbUnderstandART.SelectedValue = value.UnderstandART.ToString();
                rbUnderstandSideEffects.SelectedValue = value.UnderstandSideEffect.ToString();
                rbUnderstandAdherenceBenefits.SelectedValue = value.UnderstandAdherenceBenefits.ToString();
                rbUnderstandConsequences.SelectedValue = value.UnderstandConsequences.ToString();
            }
        }
    }
}