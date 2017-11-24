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
    [Table("PatientHivDiagnosis")]
    public class PatientHivDiagnosis:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? HivDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWhoStage { get; set; }
        
        public DateTime? ArtInitiationDate { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }

        //public virtual PatientEntity PatientEntity {get;set;}
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
