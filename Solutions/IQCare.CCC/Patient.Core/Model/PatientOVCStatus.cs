using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PatientManagement.Core.Model
{
    [Table( "PatientOVCStatus")]

    public class PatientOVCStatus :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int GuardianId { get; set; }
        [ForeignKey("GuardianId")]

        [Required]
        public string Orphan { get; set; }

        [Required]
        public string InSchool { get; set; }

        public int Active { get; set; }
    }
}
