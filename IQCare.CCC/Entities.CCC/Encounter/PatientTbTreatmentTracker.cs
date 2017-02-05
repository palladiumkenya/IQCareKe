using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("TBTreatmentTracker")]
   public class PatientTbTreatmentTracker:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public string YearMonth { get; set; }
        public int ScreeningId { get; set; }
        public string TbReNumber { get; set; }
    }
}
