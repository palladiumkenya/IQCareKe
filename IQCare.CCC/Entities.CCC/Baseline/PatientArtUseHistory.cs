using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Baseline
{
    [Serializable]

    [Table("ARTUseHistory")]
   public class PatientArtUseHistory
    {
        [Column]

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string TreatmentType { get; set; }
        public string Purpose { get; set; }
        public string Regimen { get; set; }
        public DateTime DateLastUsed { get; set; }
    }
}
