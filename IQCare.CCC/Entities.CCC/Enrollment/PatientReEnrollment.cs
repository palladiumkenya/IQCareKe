using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientReenrollment")]
    public class PatientReEnrollment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity PatientEntity { get; set; }
        public DateTime ReenrollmentDate { get; set; }
    }
}
