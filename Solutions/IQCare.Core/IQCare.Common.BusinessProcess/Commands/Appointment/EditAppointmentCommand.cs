using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class EditAppointmentCommand:IRequest<Result<EditAppointmentCommandResponse>>
    {

    }

    public class EditAppointmentCommandResponse
    {
        public int Id { get; set; }
    }
}
