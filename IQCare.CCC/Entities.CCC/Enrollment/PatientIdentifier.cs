using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("PatientIdentifier")]
    public class PatientIdentifier
    {
        [Column]

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientEnrollmentId { get; set; }
        [ForeignKey("PatientEnrollmentId")]
        public int IdentifierTypeId { get; set; }
        public string IdentifierValue { get; set; }

    }
}
