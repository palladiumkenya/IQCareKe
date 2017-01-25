using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientMasterVisit")]

    public class PatientMasterVisit :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int FacilityId { get; set; }
        public DateTime VisitDate { get; set; }
        public bool Schedule { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceAreaId { get; set; }
    }
}
