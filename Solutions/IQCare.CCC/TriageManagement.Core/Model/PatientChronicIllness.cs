using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace TriageManagement.Core.Model
{
    [Table("PatientChronicIllness")]
    public class PatientChronicIllness : BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]

        [Required]
        public int ChronicIllness { get; set; }

        [Required]
        public string Treatment { get; set; }

        public int Dose { get; set; }
        public int Duration { get; set; }
    }
}
