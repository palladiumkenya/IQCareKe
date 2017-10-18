using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("PatientFamilyPlanningMethod")]

    public class PatientFamilyPlanningMethod :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Schema.Column]
        [Key]

        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }

        [Required]
        public int PatientFPId { get; set; }

        [Required]
        public int FPMethodId { get; set; }
  
    }
}
