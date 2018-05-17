using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("Api_PatientAppointmentsView")]
    public class PatientAppointmentMessage
    {
        [Key]
        public Int64 Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set; }
        public string Description { get; set; }
    }
}
