using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Enrollment
{
    [Serializable]
    [Table("ServiceEntryPoint")]
    public class PatientEntryPoint:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int ServiceAreaId { get; set; }
        public int EntryPoint { get; set; }
    }
}
