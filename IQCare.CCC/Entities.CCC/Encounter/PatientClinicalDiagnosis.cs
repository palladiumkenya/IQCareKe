using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientClinicalDiagnosis")]

    public class PatientClinicalDiagnosis : BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientmasterVisitId { get; set; }
        [ForeignKey("PatientmasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}
