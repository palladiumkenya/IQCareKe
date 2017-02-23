using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientMasterVisit")]

    public class PatientMasterVisit : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int VisitSchedule { get; set; }
        public int VisitBy { get; set; }
        public int VisitType { get; set; }
        public DateTime VisitDate { get; set; }
        public bool Active { get; set; }
        public int Status { get; set; }
    }
}
