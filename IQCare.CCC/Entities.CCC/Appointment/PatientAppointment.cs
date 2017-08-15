using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Visit;
using Entities.CCC.Lookup;

namespace Entities.CCC.Appointment
{
    [Serializable]
    [Table("PatientAppointment")]
    public class PatientAppointment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ReasonId { get; set; }
        public string Description { get; set; }
        public int DifferentiatedCareId { get; set; }
        public int StatusId { get; set; }      
        public DateTime? StatusDate { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }

    [Serializable]
    [Table("AppointmentSummaryView")]
    public class AppointmentSummary
    {
        [Key]
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Total { get; set; }
        public int Met { get; set; }
        public int Missed { get; set; }
        public int Pending { get; set; }
        public int PreviouslyMissed { get; set; }
    }
}