using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class GetPatientAppointmentByServiceAreaCommand : IRequest<Result<List<PatientAppointmentMethodViewModel>>>
    {
        public int PatientId { get; set; }
        public int ServiceArea { get; set; }
    }
    
}

