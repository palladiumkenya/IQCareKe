using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientBaselineAssessment")]
    public class PatientBaselineAssessment : BaseEntity
    {

        //private PatientArtInitiationBaseline() { }
        //
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity PatientEntity { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public bool HBVInfected { get; set; }
        public bool Pregnant { get; set; }
        public bool TBInfected { get; set; }
        public bool Breastfeeding { get; set; }
        public int WHOStage { get; set; }    
        public Decimal CD4Count { get; set; }
        public decimal? MUAC { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
    }
}
