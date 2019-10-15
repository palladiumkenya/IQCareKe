using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System.Collections.Generic;
using PatientAppointment = IQCare.PMTCT.Core.Models.PatientAppointment;


namespace IQCare.PMTCT.BusinessProcess.Commands.Appointment
{
    public class GetAncAppointmentCommand: IRequest<Result<List<PatientAppointment>>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}