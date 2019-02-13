using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records.Enrollment
{
    [Serializable]
    [Table("PatientIdentifier")]
    public class PatientEntityIdentifier : BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        [ForeignKey("PatientEnrollment")]
        public int PatientEnrollmentId { get; set; }
        public virtual PatientEntityEnrollment PatientEnrollment { get; set; }
        public int IdentifierTypeId { get; set; }
        public string IdentifierValue { get; set; }
        [ForeignKey("IdentifierTypeId")]
        public virtual Identifier Identifiers { get; set; }
    }
}
