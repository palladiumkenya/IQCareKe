using System;

namespace IQCare.Common.Core.Models
{
    public class PersonTracingView
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public DateTime TracingDate { get; set; }
        public string TracingMode { get; set; }
        public string TracingOutcome { get; set; }
        public DateTime? DateBookedTesting { get; set; }
        public string Consent { get; set; }
        public bool DeleteFlag { get; set; }
        public string ReasonNotContacted { get; set; }
        public string OtherReasonSpecify { get; set; }
    }
}