using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using PatientAppointment = IQCare.PMTCT.Core.Models.PatientAppointment;


namespace IQCare.PMTCT.BusinessProcess.Commands.Appointment
{
    public class GetAncAppointmentCommand: IRequest<Result<PatientAppointment>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}