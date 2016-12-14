using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientContact")]

    public class PatientContact :BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public string PostalAddress { get; set; }
        public int MobileNo { get; set; }
        public bool IsActive { get; set; }
    }
}
