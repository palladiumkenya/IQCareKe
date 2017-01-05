using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table( "PatientLocation")]

    public class PatientMaritalStatus:BaseEntity
    {
        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]

        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int MaritalStatusId { get; set; }
        [ForeignKey("MaritalStatusId")]

        public bool Active { get; set; }
    }
}
