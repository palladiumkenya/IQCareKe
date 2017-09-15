using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("pregnancy")]

    public class PatientPreganancy :BaseEntity
    {   
        [Column]
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public virtual PatientEntity Patient { get; set; }

        [Required]
        public int PatientmasterVisitId { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }

        [Required]
        public DateTime LMP { get; set; }

        [Required]
        public DateTime? EDD { get; set; }
        public string Gravidae { get; set; }
        public string parity { get; set; }
        public int Outcome { get; set; }
        public DateTime? DateOfOutcome { get; set; }
    }
}
