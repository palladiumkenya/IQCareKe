using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table( "PatientLocation")]

    public class PatientMaritalStatus:BaseEntity
    {
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int MaritalStatusId { get; set; }
        public bool Active { get; set; }
    }
}
