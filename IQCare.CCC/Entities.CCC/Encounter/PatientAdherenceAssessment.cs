using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("AdherenceAssessment")]
    public class PatientAdherenceAssessment:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public bool ForgetMedicine { get; set; }
        public bool CarelessAboutMedicine { get; set; }
        public bool FeelWorse { get; set; }
        public bool FeelBetter { get; set; }
        public bool? TakeMedicine { get; set; }
        public bool? StopMedicine { get; set; }
        public bool? UnderPressure { get; set; }
        public decimal? DifficultyRemembering { get; set; }
    }
}
