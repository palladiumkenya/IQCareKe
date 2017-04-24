using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("pregnancyIndicator")]

    public class PatientPregnancyIndicator :BaseEntity
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
        public DateTime LMP { get; set; }


        [Required]
        public DateTime? EDD { get; set; }

        [Required]
        public int PregnancyStatusId { get; set; }

        [Required]
        public bool ANCProfile { get; set; }

        public DateTime? ANCProfileDate { get; set; }


    }
}
