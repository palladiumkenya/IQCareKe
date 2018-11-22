using System;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.CCC;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientNeonatalHistory")]
    public class PatientNeonatalHistory:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string NeonatalHistoryNotes { get; set; }
        public int RecordNeonatalHistory { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }


    }
}
