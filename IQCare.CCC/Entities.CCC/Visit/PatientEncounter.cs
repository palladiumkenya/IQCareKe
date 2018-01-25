using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientEncounter")]
   public class PatientEncounter :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int EncounterTypeId { get; set; }
        public int Status { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int ServiceAreaId { get; set; }

    }
}
