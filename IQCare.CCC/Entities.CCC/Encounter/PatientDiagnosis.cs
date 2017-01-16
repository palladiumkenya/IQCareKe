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
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int Diagnosis { get; set; }
        public string ManagementPlan { get; set; }

    }
}
