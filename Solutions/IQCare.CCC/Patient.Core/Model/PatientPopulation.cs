using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientPopulation")]
    public class PatientPopulation : BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int PopulationId { get; set; }
        [ForeignKey("PopulationId")]

        public string AuditData { get; set; }
    }
}
