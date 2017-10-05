using Entities.CCC.Visit;
using Entities.Common;
using Entities.PatientCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("AdverseEventOutcome")]
  public  class PatientAdverseEventOutcome :BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public Patient patient { get; set; }

        [Required]
        public int PatientMasterVisitid { get; set; }
        public PatientMasterVisit PatientMasterVisit { get; set; }

        [Required]
        [ForeignKey("PatientAdverseEventId")]
        public int PatientAdverseEventId { get; set; }
        public PatientAdverseEvent patientAdverseEvent { get; set; }

        [Required]
        public int OutcomeId { get; set; }

        [Required]
        public int ActionTakenId { get; set; }

    }

}
