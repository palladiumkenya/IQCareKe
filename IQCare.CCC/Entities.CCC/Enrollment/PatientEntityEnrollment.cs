using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientEnrollment")]
    public class PatientEntityEnrollment : BaseEntity
    {
        [Column]

        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public bool TransferIn { get; set; }
        public bool CareEnded { get; set; }
    }
}
