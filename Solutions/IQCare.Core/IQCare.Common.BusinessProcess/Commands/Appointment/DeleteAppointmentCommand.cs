using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class DeleteAppointmentCommand : IRequest<Result<DeleteAppointmentReason>>
    {
        public int AppointmentId { get; set; }
    }

    public class DeleteAppointmentReason
    {
        public string Message { get; set; }
    }
}