using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("AdherenceOutcome")]

   public class PatientAdherenceOutcome :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientmasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int AdherenceType { get; set; }
        public int Score { get; set; }
        public string ScoreDescription { get; set; }

    }
}
