using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("ServiceAreaIdentifiers")]
    public class ServiceAreaIdentifiers
    {
        [Key]
        public int Id { get; set; }
        public int ServiceAreaId { get; set; }
        public int IdentifierId { get; set; }
        public bool RequiredFlag { get; set; }

        public virtual ServiceArea ServiceArea { get; set; }
        public virtual Identifier Identifier { get; set; }
    }
}
