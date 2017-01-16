using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("Disclosure")]

    public class PatientDisclosure :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        public string Category { get; set; }
        public string DisclosureStage { get; set; }
        public DateTime DisclosureDate { get; set; }
    }
}
