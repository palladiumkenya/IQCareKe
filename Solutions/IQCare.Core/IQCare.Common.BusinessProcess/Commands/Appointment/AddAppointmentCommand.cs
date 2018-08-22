using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class AddAppointmentCommand: IRequest<Result<AddApointmentCommandResponse>>
    {
        public PatientAppointment patientAppoitment;
    }

    public class AddApointmentCommandResponse
    {
        public int Id { get; set; }
    }
}
