using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("PatientClinicalNotes")]
    public class PatientClinicalNotes :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int FacilityId { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int ServiceAreaId { get; set; }
        public string ClinicalNotes { get; set; }
        public bool ModifyFlag { get; set; }
        public DateTime VersionStamp { get; set; }
    }
}
