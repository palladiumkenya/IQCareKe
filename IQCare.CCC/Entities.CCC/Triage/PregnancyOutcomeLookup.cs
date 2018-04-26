using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PregnancyOutcomeView")]
    public class PregnancyOutcomeLookup
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime LMP { get; set; }
        public DateTime EDD { get; set; }
        public string PregnancyStatus { get; set; }
        public string Outcome { get; set; }
        public int? PregnancyStatusId { get; set; }
        public string OutcomeStatus { get; set; }
    }
}
