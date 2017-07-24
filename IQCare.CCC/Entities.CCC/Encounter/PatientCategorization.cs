using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientCategorization")]
    public class PatientCategorization : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }

        public int PatientMasterVisitId { get; set; }
        public PatientCategorizationStatus Categorization { get; set; }
        public DateTime DateAssessed { get; set; }
    }

    public enum PatientCategorizationStatus
    {
        None,
        Stable,
        Unstable
    }
}