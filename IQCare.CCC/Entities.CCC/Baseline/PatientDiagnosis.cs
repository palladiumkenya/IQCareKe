using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Entities.CCC.Visit;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("DiagnosisARVHistory")]
    public class PatientDiagnosis:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }  
        public DateTime HIVDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWHOStage { get; set; }
        public DateTime ARTInitiationDate { get; set; }
        public virtual Patient Patient { get; set; }

    }
}
