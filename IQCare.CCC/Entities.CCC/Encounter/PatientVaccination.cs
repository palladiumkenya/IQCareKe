using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("Vaccination")]
   public class PatientVaccination:BaseObject
    {
        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int Vaccine { get; set; }
        public string VaccineStage { get; set; }
    }
}
