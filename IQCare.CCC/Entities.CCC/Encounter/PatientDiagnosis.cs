using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientDiagnosis")]
    public class PatientDiagnosis
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int Diagnosis { get; set; }
        public string ManagementPlan { get; set; }

    }
}
