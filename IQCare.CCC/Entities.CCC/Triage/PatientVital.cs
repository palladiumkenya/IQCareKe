using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage 
{
    [Serializable]
    [Table("PatientVitals")]
    public class PatientVital  : BaseEntity
    {
        [Column]
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public decimal Temperature { get; set; }
        public decimal RespiratoryRate { get; set; }
        public decimal HeartRate { get; set; }
        public int Bpdiastolic { get; set; }
        public int BpSystolic { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal  Muac { get; set; }
        public decimal SpO2 { get; set; }
        public decimal BMI { get; set; }
        public decimal HeadCircumference { get; set; }
        public string BMIZ { get; set; }
        public string WeightForHeight { get; set; }
        public string WeightForAge { get; set; }
        public DateTime? VisitDate { get; set; }
        //public decimal AgeforZ { get; set; }
        public string NursesComments { get; set; }
     }

}
