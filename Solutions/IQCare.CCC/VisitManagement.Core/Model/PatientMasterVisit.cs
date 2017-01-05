using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace VisitManagement.Core.Model
{
    [Table("PatientMasterVisit")]

    public class PatientMasterVisit:BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int FacilityId { get; set; }
        [ForeignKey("FacilityId")]

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public bool Schedule { get; set; }

        [Required]
        public int VisitBy { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ServiceAreaId { get; set; }
        [ForeignKey("ServiceAreaId")]

        public int AuditData { get; set; }
    }
}
