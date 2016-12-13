using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [System.Data.Linq.Mapping.Table(Name = "PatientTreatmentSupporter")]

    class PatientTreatmentSupporter :BaseEntity
    {
        
        public int PatientId { get; set; }
        public string SupporterName { get; set; }
        public int Phone { get; set; }

    }
}
