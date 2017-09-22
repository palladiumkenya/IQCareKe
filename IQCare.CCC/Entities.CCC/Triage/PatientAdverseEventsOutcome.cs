using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.CCC.Visit;
using Entities.PatientCore;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("AdverseEventOutcome")]
   public class PatientAdverseEventsOutcome
    {
        [Key]
        public int Id { get; set; }
        public PatientAdverseEvent PatientAdverseEvent { get; set; }
        public int PatientAdverseEventId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public PatientMasterVisit PatientMasterVisit { get; set; }
        [ForeignKey("PatientMasterVisitid")]
        public int PatientMasterVisitId { get;set;}
        public int OutcomeId { get; set; }
        public int ActionTakenId { get; set; }

    }
}
