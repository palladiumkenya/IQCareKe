using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("TreatmentEventTracker")]
   public class PatientTreatmentEventTracker:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        [Column("MasterVisitId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public DateTime EventDate { get; set; }
        public int EventType { get; set; }
        public string Category { get; set; }
        public string Previous { get; set; }
        public string Current { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }
}
