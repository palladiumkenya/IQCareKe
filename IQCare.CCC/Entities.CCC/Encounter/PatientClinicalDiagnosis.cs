using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientClinicalDiagnosis")]

    public class PatientClinicalDiagnosis :BaseObject
    {
        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientmasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}
