using System;

namespace IQCare.HTS.Core.Model
{
    public class Tracing
    {
        public int PersonID { get; set; }
        public int TracingType { get; set; }
        public DateTime DateTracingDone { get; set; }
        public int Mode { get; set; }
        public int Outcome { get; set; }
        public DateTime BookingDate { get; set; }
        public string Remarks { get; set; }
    }
}