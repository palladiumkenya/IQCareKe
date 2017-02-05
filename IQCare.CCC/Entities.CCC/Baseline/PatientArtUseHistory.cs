using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.CCC.Visit;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Baseline
{
    [Serializable]

    [Table("ARTUseHistory")]
   public class PatientArtUseHistory:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        [ForeignKey("PatientMasterVisit")]
        public int? PatientMasterVisitId { get; set; }
        public string TreatmentType { get; set; }
        public string Purpose { get; set; }
        public string Regimen { get; set; }
        public DateTime DateLastUsed { get; set; }

        public virtual Patient Patient { get; set;  }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
