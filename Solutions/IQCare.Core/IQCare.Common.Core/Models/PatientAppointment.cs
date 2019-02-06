using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientAppointment
    {
        public PatientAppointment()
        {

        }
        public PatientAppointment(int patientId, int masterVisitId, int serviceAreaId, DateTime appointmentDate, int reasonId, int statusId, 
            int ? differentiatedCareId,DateTime ? statusDate, int createdBy, string description)
        {
            PatientId = patientId;
            PatientMasterVisitId = masterVisitId;
            ServiceAreaId = serviceAreaId;
            AppointmentDate = appointmentDate;
            ReasonId = reasonId;
            Description = description;
            StatusDate = statusDate;
            DifferentiatedCareId = differentiatedCareId;
            StatusId = statusId;
            CreateDate = DateTime.Now;
            CreatedBy = createdBy;
        }
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ServiceAreaId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ReasonId { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime ? StatusDate { get; set; }
        public int ? DifferentiatedCareId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
