using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("PersonContactView")]
    public class PersonContactLookUp
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
