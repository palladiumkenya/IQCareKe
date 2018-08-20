using System;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Neonatal
{
    [Serializable]
    [Table("PatientImmunizationHistory")]
    public class PatientImmunizationHistory:BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int ImmunizationPeriodId { get; set; }
        public int ImmunizationGivenId { get; set; }
        public DateTime ImmunizationDate { get; set; }
    }
}
