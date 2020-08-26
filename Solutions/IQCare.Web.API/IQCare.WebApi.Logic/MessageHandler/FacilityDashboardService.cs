using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public class FacilityDashboardService : IFacilityDashboardService
    {
        public object GetAppointmentSummaryByDate(DateTime appointmentDate)
        {
            var appointment = new PatientAppointmentManager();
            var apps = appointment.GetAppointmentSummaryByDate(appointmentDate).FirstOrDefault();
            return apps;
        }
    }
}
