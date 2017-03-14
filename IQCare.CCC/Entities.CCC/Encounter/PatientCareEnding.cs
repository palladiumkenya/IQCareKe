using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Encounter
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
        public string CareEndingNotes { get; set; }
        public bool Active { get; set; }
    }
}
