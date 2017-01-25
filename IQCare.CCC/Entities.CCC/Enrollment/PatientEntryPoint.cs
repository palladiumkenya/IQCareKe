using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("ServiceEntryPoint")]
    public class PatientEntryPoint:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int ServiceAreaId { get; set; }
        public int EntryPointId { get; set; }
    }
}
