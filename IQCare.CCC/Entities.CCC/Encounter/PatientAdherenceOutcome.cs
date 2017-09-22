using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Visit;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("AdherenceOutcome")]

   public class PatientAdherenceOutcome :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Entities.CCC.Enrollment.PatientEntity Patient { get; set; }
        public int PatientmasterVisitId { get; set; }
        [ForeignKey("PatientmasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int AdherenceType { get; set; }
        public int Score { get; set; }
        public string ScoreDescription { get; set; }

    }
}
