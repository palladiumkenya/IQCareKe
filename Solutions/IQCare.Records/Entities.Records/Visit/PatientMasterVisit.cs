using Entities.Common;
using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
   
    [Serializable]
    [Table("PatientMasterVisit")]

    public class PatientMasterVisit : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int? VisitScheduled { get; set; }
        public int? VisitBy { get; set; }
        public int? VisitType { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool Active { get; set; }
        public int? Status { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
    }
}
