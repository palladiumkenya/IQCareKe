using System;
using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientVitals")]
    public class PatientVitals :BaseEntity
    {
        public int PatientMasterVisitId { get; set; }
        public decimal Temperature { get; set; }
        public decimal RR { get; set; }
        public decimal HR { get; set; }
        public decimal BPDiastolic { get; set; }
        public decimal BPSystolic { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public int Pain { get; set; }
        public int TLC { get; set; }
        public decimal TLCPercent { get; set; }
        public int Oedema { get; set; }
        public decimal Muac { get; set; }
        public decimal SPO2 { get; set; }
        public DateTime LMP { get; set; }
    }
}
