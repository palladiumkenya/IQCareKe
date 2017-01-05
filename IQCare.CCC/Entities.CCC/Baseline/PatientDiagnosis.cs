using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientDiagnosis")]
    public class PatientDiagnosis
    {
        [Column]

        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int Diagnosis { get; set; }
        public string ManagementPlan { get; set; }
    }
}
