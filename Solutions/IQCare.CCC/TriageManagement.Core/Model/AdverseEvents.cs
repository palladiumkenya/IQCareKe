using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [Table("AdverseEvent")]
    public class AdverseEvents :BaseEntity
    {
        
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string EventName { get; set; }
        public string EventCause { get; set; }
        public string Severity { get; set; }
        public string Action { get; set; }
    }
}
