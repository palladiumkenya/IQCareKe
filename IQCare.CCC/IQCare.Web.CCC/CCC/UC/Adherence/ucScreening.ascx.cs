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
    public partial class ucScreening : System.Web.UI.UserControl
    {
        public int ScreenId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                getScreening(PatientId, PatientMasterVisitId);
            }
        }
        protected void getScreening(int patientId, int patientMasterVisitId)
        {
            var _screening = new AdherenceLogic();
            List<AdherenceScreening> screeningList = _screening.getAdherenceScreening(patientId, patientMasterVisitId);
            foreach (var value in screeningList)
            {
                ScreenId = value.Id;
                tbTotal.Text = value.Total.ToString();
                tbDepressionSeverity.Text = value.DepressionSeverity;
                tbRecommendedManagement.Text = value.RecommendedManagement;
            }
        }
    }
}