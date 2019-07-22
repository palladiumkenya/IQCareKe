using System;

namespace IQCare.Common.Core.Models
{
    public class PatientAdverseEvent
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int PatientMasterVisitId { get; set; }
        public string EventName { get; set; }

        public string EventCause { get; set; }

        public string Severity { get; set; }

        public string Action { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuditData { get; set; }

        public int AdverseEventId { get; set; }
    }
}