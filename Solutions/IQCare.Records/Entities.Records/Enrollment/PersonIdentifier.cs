using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records.Enrollment
{

    [Serializable]
    public class PersonIdentifier : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int PersonId { get; set; }
        public int IdentifierId { get; set; }
        public string IdentifierValue { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [ForeignKey("IdentifierId")]
        public virtual Identifier Identifiers { get; set; }
    }
}
