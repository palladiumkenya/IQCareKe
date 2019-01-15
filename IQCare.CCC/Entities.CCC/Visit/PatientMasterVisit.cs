using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.CCC.Enrollment;
using Entities.Common;

namespace Entities.CCC.Visit
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
        public int? FacilityId { get; set; }
        public int? Status { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
    }
}
