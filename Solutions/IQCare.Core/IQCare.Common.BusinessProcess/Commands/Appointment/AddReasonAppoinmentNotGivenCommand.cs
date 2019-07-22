using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class AddReasonAppoinmentNotGivenCommand : IRequest<Result<AddReasonAppointmentNotGivenResponse>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ReasonAppointmentNotGiven { get; set; }
    }

    public class AddReasonAppointmentNotGivenResponse
    {
        public string Message { get; set; }
    }
}