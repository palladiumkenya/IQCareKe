using System;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Neonatal
{
    [Serializable]
    [Table("PatientMilestone")]
    public class PatientMilestone:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int milestoneAssessedId { get; set; }
        public DateTime milestoneDate { get; set; }
        public int milestoneAchievedId { get; set; }
        public int milestoneStatusId { get; set; }
        public string milestoneComments { get; set; }
    }
}

