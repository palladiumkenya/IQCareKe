using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using MediatR;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.Appointment
{
    public class GetPatientAppointmentViewQuery : IRequest<Result<List<PatientAppointmentMethodViewModel>>>
    {
        public int PatientId { get; set; }
    }
    public class PatientAppointmentMethodViewModel
     {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string Description { get; set; }
        public DateTime AppDate { get; set; }
    }
}
