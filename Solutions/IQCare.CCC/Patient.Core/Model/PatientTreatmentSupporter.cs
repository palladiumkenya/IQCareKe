using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table("PatientTreatmentSupporter")]

    public class PatientTreatmentSupporter :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int SupporterId { get; set; }
        [ForeignKey("SupporterId")]

        public string AuditData { get; set; }
    }
}
