using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Visit;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for FacilityService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FacilityService : System.Web.Services.WebService
    {
        private string Msg { get; set; }

        [WebMethod]
        public AppointmentStatistics GetAppointmentStatistics(DateTime date)
        {
            AppointmentStatistics statistics = new AppointmentStatistics();
            try
            {
                var appointment = new PatientAppointmentManager();
                var masterVisit = new PatientMasterVisitManager();
                statistics.TotalAppointments = appointment.GetByDate(date).Count;
                statistics.MetAppointments = appointment.GetByDate(date).Count(n => n.StatusId==1241);
                statistics.MissedAppointments = appointment.GetByDate(date).Count(n => n.StatusId == 1240);
                statistics.WalkIns = masterVisit.GetByDate(date).Count(n => n.VisitScheduled == 0); ;

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            
            return statistics;
        }
    }

    public class AppointmentStatistics
    {
        public int TotalAppointments { get; set; }
        public int MetAppointments { get; set; }
        public int MissedAppointments { get; set; }
        public int WalkIns { get; set; }
    }
}
