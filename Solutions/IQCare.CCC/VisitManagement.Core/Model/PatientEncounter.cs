using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;
using Microsoft.Build.Framework;

namespace VisitManagement.Core.Model
{
    [Table("PatientEncounter")]
    public class PatientEncounter :BaseEntity
    {
        [Required]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        [Required]
        public int EncounterTypeId { get; set; }
        [ForeignKey("EncounterTypeId")]

        [Required]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]

        [Required]
        public DateTime EncounterStarTime { get; set; }


        public DateTime EncounterEndTime { get; set; }

        [Required]
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]

        public string AuditData { get; set; }
    }
}
