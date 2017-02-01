using Entities.PatientCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientEncounter")]
   public class PatientEncounter :BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("patient")]
        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int EncounterTypeId { get; set; }
        [ForeignKey("PatientmasterVisit")]
        public int? PatientMasterVisitId { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public DateTime EncounterStartTime { get; set; }
        public DateTime EncounterEndTime { get; set; }
        public int ServiceAreaId { get; set; }
    }
}
