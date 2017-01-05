using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("PersonContact")]

    public class PersonContact:BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PersonId")]

        [Required]
        public string PhysicalAddress { get; set; }

        [Required]
        public int MobileNumber { get; set; }

        public bool Active { get; set; }
    }
}
