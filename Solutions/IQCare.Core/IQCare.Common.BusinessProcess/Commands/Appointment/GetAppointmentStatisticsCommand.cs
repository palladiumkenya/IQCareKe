using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class GetAppointmentStatisticsCommand : IRequest<Result<List<AppointmentSummary>>>
    {
        public DateTime summaryDate { get; set; }
    }
}
