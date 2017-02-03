using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("HIVEnrollmentBaseline")]
    public class PatientHivEnrollmentBaseline:BaseObject
    {
        [Column]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public DateTime HivDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWhoStage { get; set; }
        public DateTime ArtInitiationDate { get; set; }
        public bool ArtHistoryUse { get; set; }
        public bool HivRetest { get; set; }
        public string ReasonForNoRetest { get; set; }
    }
}
