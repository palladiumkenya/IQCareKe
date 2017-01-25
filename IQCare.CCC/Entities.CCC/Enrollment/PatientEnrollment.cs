using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientEnrollment")]
    public class PatientEnrollment :BaseEntity
    {
        [Column]

        [Key]
        public int Id { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public bool TransferIn { get; set; }
    }
}
