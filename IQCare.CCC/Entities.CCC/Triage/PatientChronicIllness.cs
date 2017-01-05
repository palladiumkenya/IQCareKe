using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientChronicIllness")]
    public class PatientChronicIllness
    {
        [Column]

        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int ChronicIllness { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
    }
}
