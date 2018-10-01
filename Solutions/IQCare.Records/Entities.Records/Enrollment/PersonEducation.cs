using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records.Enrollment
{
    [Serializable]
    [Table("PersonEducation")]
    public class PersonEducation:BaseEntity
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public int EducationLevel{ get; set; }
    }
}
