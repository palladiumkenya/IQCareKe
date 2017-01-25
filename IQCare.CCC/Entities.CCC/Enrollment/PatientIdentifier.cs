using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientIdentifier")]
    public class PatientIdentifier :BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("PatientEnrollmentId")]
        public int PatientEnrollmentId { get; set; }
        public virtual PatientEnrollment PatientEnrollment { get; set; }
        public int IdentifierTypeId { get; set; }
        public string IdentifierValue { get; set; }

    }
}
