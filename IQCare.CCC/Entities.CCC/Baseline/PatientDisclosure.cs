using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("Disclosure")]

    public class PatientDisclosure :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Category { get; set; }
        public string DisclosureStage { get; set; }
        public DateTime DisclosureDate { get; set; }
    }
}
