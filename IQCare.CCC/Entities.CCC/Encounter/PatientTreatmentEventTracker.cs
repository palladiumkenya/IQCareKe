using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("TreatmentEventTracker")]
   public class PatientTreatmentEventTracker:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        [Column("MasterVisitId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public DateTime EventDate { get; set; }
        public int EventType { get; set; }
        public string Category { get; set; }
        public string Previous { get; set; }
        public string Current { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }
}
