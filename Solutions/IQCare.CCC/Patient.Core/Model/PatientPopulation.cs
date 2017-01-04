using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientPopulation")]
    public class PatientPopulation : BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PopulationId { get; set; }

    }
}
