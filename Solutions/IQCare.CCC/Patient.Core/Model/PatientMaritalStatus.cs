using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientLocation")]

    public class PatientMaritalStatus:BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int MaritalStatus { get; set; }
    }
}
