using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientEncounter")]
   public class PatientEncounter
    {
        public int PatientId { get; set; }
        [ForeignKey("patientId")]
        public int EncounterTypeId { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientmasterVisitId")]
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
    }
}
