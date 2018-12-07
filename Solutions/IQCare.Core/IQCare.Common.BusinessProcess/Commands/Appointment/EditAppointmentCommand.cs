using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class EditAppointmentCommand:IRequest<Result<EditAppointmentCommandResponse>>
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
    }

    public class EditAppointmentCommandResponse
    {
        public string Message { get; set; }
    }
}
