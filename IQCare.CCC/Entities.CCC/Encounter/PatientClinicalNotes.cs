using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientClinicalNotes")]
    public class PatientClinicalNotes :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int FacilityId { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int ServiceAreaId { get; set; }
        public string ClinicalNotes { get; set; }
        public bool ModifyFlag { get; set; }
        public DateTime VersionStamp { get; set; }
    }
}
