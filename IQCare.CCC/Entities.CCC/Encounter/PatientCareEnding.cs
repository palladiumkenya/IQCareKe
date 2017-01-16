using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientCareending")]
   public class PatientCareEnding :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int PatientEnrollmentId { get; set; }
        public int ExitReason { get; set; }
        public DateTime ExitDate { get; set; }

    }
}
