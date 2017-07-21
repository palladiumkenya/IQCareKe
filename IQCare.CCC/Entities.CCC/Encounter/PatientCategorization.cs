using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientCategorization")]
    class PatientCategorization : BaseEntity
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
        Stable,
        Unstable
    }
}
