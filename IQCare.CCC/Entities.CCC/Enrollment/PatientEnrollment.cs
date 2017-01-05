using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientEnrollment")]
    public class PatientEnrollment
    {
        [Column]

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int ServiceAreaId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentStatusId { get; set; }
        public bool TransferIn { get; set; }
    }
}
