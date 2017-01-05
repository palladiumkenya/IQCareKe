using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage 
{
    [Serializable]
    [Table("PatientVitals")]
    public class PatientVital 
    {
        [Column]
        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")] 
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientmasterVisitId")]
        public decimal Temperature { get; set; }
        public decimal RespiratoryRate { get; set; }
        public decimal HeartRate { get; set; }
        public int Bpdiastolic { get; set; }
        public int BpSystolic { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal  Muac { get; set; }
        public decimal SpO2 { get; set; }
     }
}
