using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Appointment
{
    public class GetPatientAppointmentCommand: IRequest<Result<List<PatientAppointment>>>
    {
        public int PatientId { get; set; }
    }
}