using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientFamilyPlanning")]
    public class PatientFamilyPlanning :BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public virtual PatientEntity Patient { get; set; }

        [Required]
        public int PatientMasterVisitId { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }

        [Required]
        public int FamilyPlanningStatusId { get; set; }

        public int ReasonNotOnFPId { get; set; }
    }
}
