using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    [Table("PersonContact")]
    public class PersonContact : BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("PersonId")]
        public virtual int PersonId { get; set; }
       // public virtual Person Person { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNo { get; set; }
    }
}
