using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientCareending")]
    public class PatientCareEnding : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int PatientEnrollmentId { get; set; }
        public int ExitReason { get; set; }
        public DateTime ExitDate { get; set; }
        public string TransferOutFacility { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string CareEndingNotes { get; set; }
        public bool Active { get; set; }
    }
}
