using Entities.CCC.Enrollment;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientFamilyPlanningMethod")]


   public class PatientFamilyPlanningMethod
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientFpId { get; set; }
        public int FPMethodId { get; set; }
        public int DeleteFlag { get; set; }
    }
}
