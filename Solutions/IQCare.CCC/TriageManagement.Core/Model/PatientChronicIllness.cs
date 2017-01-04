using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace TriageManagement.Core.Model
{
    [Table("PatientChronicIllness")]
    public class PatientChronicIllness : BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int ChronicIllness { get; set; }
        public string Treatment { get; set; }
        public int Dose { get; set; }
        public int Duration { get; set; }
    }
}
