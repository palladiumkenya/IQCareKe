using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("TBTreatmentTracker")]
   public class PatientTbTreatmentTracker:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public string YearMonth { get; set; }
        public int ScreeningId { get; set; }
        public string TbReNumber { get; set; }
    }
}
