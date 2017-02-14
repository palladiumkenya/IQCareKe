using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.PatientCore
{
    [Serializable]
    [Table("PatientMaritalStatus")]
   public class PatientMaritalStatus:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public int MaritalStatusId { get; set; }
        public bool Active { get; set; }
    }
}
