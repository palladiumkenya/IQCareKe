using Entities.CCC.Visit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Entities.CCC.Enrollment;
using Entities.Common;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientDiagnosis")]
    public class DiagnosisArvHistory:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        public virtual int PatientId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual int PatientMasterVisitId { get; set; }
        public DateTime HivDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWhoStage { get; set; }
        public DateTime ArtInitiationDate { get; set; }
        public virtual PatientEntity Patient { get; set; }

        public virtual PatientEntity PatientEntity {get;set;}
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
