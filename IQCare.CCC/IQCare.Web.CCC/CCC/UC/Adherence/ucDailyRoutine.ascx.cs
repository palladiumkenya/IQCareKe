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
    public partial class ucDailyRoutine : System.Web.UI.UserControl
    {
        public int dailyRoutineId, PatientId, PatientMasterVisitId, userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(Request.QueryString["visitId"] != null ? Request.QueryString["visitId"] : HttpContext.Current.Session["PatientMasterVisitId"]);
            userId = Convert.ToInt32(Session["AppUserId"]);
            if (!IsPostBack)
            {
                getDailyRoutine(PatientId, PatientMasterVisitId);
            }
        }
        protected void getDailyRoutine(int PatientId, int PatientMasterVisitId)
        {
            var DR = new AdherenceLogic();
            List<DailyRoutine> dailyRoutineList = DR.getDailyRoutine(PatientId, PatientMasterVisitId);
            foreach (var value in dailyRoutineList)
            {
                dailyRoutineId = value.Id;
                tbTypicalDay.Text = value.TypicalDay;
                tbMedicineAdministration.Text = value.MedicineAdministration;
                tbTravelCase.Text = value.TravelCase;
                tbPrimaryCaregiver.Text = value.MedicineAdministration;
            }
        }
    }
}