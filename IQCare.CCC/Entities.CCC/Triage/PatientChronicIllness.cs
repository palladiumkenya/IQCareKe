using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientChronicIllness")]
    public class PatientChronicIllness : BaseObject
    {
        [Column]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int ChronicIllness { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
    }
}
