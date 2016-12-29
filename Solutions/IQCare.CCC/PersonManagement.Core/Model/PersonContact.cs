using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("PersonContact")]

    public class PersonContact:BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PersonId")]
        public string PhysicalAddress { get; set; }
        public int MobileNumber { get; set; }
        public bool Active { get; set; }
    }
}
