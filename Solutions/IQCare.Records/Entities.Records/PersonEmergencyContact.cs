using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Table("PersonEmergencyContact")]
   public class PersonEmergencyContact:BaseEntity
    {
    
        [System.ComponentModel.DataAnnotations.KeyAttribute]
        public int  Id { get; set; }
        public int? PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public int EmergencyContactPersonId { get; set; }

        public string MobileContact { get; set; }
    }
}
