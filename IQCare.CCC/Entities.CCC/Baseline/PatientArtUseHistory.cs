using Entities.CCC.Visit;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Baseline
{
    [Serializable]

    [Table("ARTUseHistory")]
   public class PatientArtUseHistory
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Entities.CCC.Enrollment.PatientEntity Patient { get; set; }

        public int PatientMasterVisitId { get; set; }

        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }

        public string TreatmentType { get; set; }

        public string Purpose { get; set; }

        public string Regimen { get; set; }

        public DateTime DateLastUsed { get; set; }
    }
}
