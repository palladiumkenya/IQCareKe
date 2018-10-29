using IQCare.Common.Core.Models;
using MediatR;
using System;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class AddPatientAppointmentCommand : IRequest<Result<AddPatientAppointmentResponse>>
    {
        public int PatientMasterVisitId { get; set; }
        public int ServiceAreaId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public DateTime ? StatusDate { get; set; }
        public int ? DifferentiatedCareId { get; set; }
        public string AppointmentReason { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AddPatientAppointmentResponse
    {
        public int AppointmentId { get; set; }

    }
}
