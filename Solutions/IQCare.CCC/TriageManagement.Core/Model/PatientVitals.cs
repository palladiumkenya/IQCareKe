using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace TriageManagement.Core.Model
{
    [Table( "PatientVitals")]
    public class PatientVitals :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        [Required]
        public decimal Temperature { get; set; }
        public decimal RespiratoryRate { get; set; }
        public decimal HeartRate { get; set; }
        public decimal BpDiastolic { get; set; }
        public decimal BpSystolic { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal Muac { get; set; }
        public decimal Spo2 { get; set; }
    }
}
