using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientPHDP")]

   public class PatientPhdp:BaseObject
    {
        [Column]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public int Phdp { get; set; }
    }
}
