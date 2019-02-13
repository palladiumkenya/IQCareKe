using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("Vaccination")]
   public class PatientVaccination:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int Vaccine { get; set; }
        public string VaccineStage { get; set; }
        public DateTime? VaccineDate { get; set; }

        public int? PeriodId { get; set; }

        public int? VaccineStageId { get; set; }
    }
}
