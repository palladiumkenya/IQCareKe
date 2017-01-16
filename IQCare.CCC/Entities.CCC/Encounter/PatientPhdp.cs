using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientPHDP")]

   public class PatientPhdp:BaseObject
    {
        [Column]

        public virtual int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int Phdp { get; set; }
    }
}
