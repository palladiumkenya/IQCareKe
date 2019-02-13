using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Adherence
{
    [Serializable]
    [Table("AdherenceHIVInfection")]
    public class UnderstandingHIV:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int UnderstandHIVEffects { get; set; }
        public int UnderstandART { get; set; }
        public int UnderstandSideEffect { get; set; }
        public int UnderstandAdherenceBenefits { get; set; }
        public int UnderstandConsequences { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
    }
}
