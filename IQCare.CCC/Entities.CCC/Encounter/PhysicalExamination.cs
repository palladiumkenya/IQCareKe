using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PhysicalExamination")]
   public class PhysicalExamination :BaseObject
    {
       [Column]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int ExaminationTypeId { get; set; }
        public int ExamId { get; set; }
        public int FindingId { get; set; }
        public string FindingsNotes { get; set; }

    }
}
