using System;
using System.Xml.Serialization;

namespace IQCare.HTS.Core.Model
{
    public class Tracing
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public int TracingType { get; set; }
        public DateTime DateTracingDone { get; set; }
        public int Mode { get; set; }
        public int Outcome { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }

        public int? Consent { get; set; }
        public DateTime? ReminderDate { get; set; }
        public DateTime? DateBookedTesting { get; set; }

        public int? ReasonNotContacted { get; set; }
        public string OtherReasonSpecify { get; set; }
    }
}