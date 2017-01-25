using Entities.CCC.Visit;
using Entities.Common;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
   public class PatientMaritalStatus:BaseEntity
    {
        [ForeignKey("PersonId")]
        public virtual int PersonId { get; set; }
        public virtual Person person { get; set; }
        public int MaritalStatusId { get; set; }
        public bool Active { get; set; }
    }
}
