using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table("PatientTreatmentSupporter")]

    public class PatientTreatmentSupporter :BaseEntity
    {
        
        public int PatientId { get; set; }
        public int SupporterId { get; set; }
    }
}
