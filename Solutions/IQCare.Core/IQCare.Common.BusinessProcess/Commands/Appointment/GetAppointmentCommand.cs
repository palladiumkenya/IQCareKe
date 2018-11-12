using IQCare.Common.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
   public class GetAppointmentCommand:IRequest<Result<List<PatientAppointment>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}
