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
    public partial class ucReferralsandNetworks : System.Web.UI.UserControl
    {
        public int RefId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                getReferrals(PatientId, PatientMasterVisitId);
            }
        }
        protected void getReferrals(int patientId, int patientMasterVisitId)
        {
            var _referrals = new AdherenceLogic();
            List<Referrals> ReferralsList = _referrals.getReferrals(PatientId, PatientMasterVisitId);
            foreach (var value in ReferralsList)
            {
                RefId = value.Id;
                rbPatientReferred.SelectedValue = value.PatientReferred.ToString();
                rbAppointmentsAttended.SelectedValue = value.AppointmentsAttended.ToString();
                tbExperience.Text = value.Experience;
            }
        }
    }
}