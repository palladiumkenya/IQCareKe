using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPatientAppointmentCommand: IRequest<PatientAppointment>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}