using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("Disclosure")]

    public class PatientDisclosure :BaseEntity
    {
        [Key]
        public int?Id { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
       [ForeignKey("PatientMasterVisit")]
        public int PatientMasterVisitId { get; set; }      
        public string Category { get; set; }
        public string DisclosureStage { get; set; }
        public DateTime DisclosureDate { get; set; }

        public virtual PatientEntity PatientEntity { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
