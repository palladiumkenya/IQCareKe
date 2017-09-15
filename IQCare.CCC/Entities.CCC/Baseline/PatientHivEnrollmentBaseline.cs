using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("HIVEnrollmentBaseline")]

    public class PatientHivEnrollmentBaseline:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }     
        public int PatientMasterVisitId { get; set; }    
        public DateTime HivDiagnosisDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int EnrollmentWhoStage { get; set; }
        public DateTime ArtInitiationDate { get; set; }
        public bool ArtHistoryUse { get; set; }
        public bool HivRetest { get; set; }
        public int HivRetestTypeId { get; set; }
        public string ReasonForNoRetest { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
