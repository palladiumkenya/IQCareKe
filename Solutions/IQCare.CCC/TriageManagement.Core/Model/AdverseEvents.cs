using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "AdverseEvent")]
    public class AdverseEvents :BaseEntity
    {
        public int PatientMasterVisitId { get; set; }
        public string EventName { get; set; }
        public string EventCause { get; set; }
        public string Severity { get; set; }
    }
}
