using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MessageHandler
{
    public interface IFacilityDashboardService
    {
        Object GetAppointmentSummaryByDate(DateTime appointmentDate);
    }
}
