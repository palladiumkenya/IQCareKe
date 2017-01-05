using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace TriageManagement.Core.Model
{
    [Table("AdverseEvent")]
    public class AdverseEvents :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventCause { get; set; }

        [Required]
        public string Severity { get; set; }

        [Required]
        public string Action { get; set; }
    }
}
