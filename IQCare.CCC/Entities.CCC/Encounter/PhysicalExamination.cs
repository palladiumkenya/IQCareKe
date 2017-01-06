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
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int ExaminationTypeId { get; set; }
        public int ExamId { get; set; }
        public string Finding { get; set; }
    }
}
