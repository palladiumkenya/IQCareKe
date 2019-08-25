using System;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class EditAppointmentCommand:IRequest<Result<EditAppointmentCommandResponse>>
    {
        public int? AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int? DifferentiatedCareId { get; set; }
        public int ReasonId { get; set; }
        public int ServiceAreaId { get; set; }
        public int StatusId { get; set; }
    }

    public class EditAppointmentCommandResponse
    {
        public string Message { get; set; }
    }
}
