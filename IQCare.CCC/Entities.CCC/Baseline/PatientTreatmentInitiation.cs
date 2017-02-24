using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Entities.CCC.Visit;
using Entities.Common;
using Entities.PatientCore;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("PatientTreatmentInitiation")]
   public class PatientTreatmentInitiation:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime DateStartedOnFirstline { get; set; }
        public string Cohort { get; set; }
        public int Regimen { get; set; }
        public decimal BaselineViralload { get; set; }
        public DateTime BaselineViralloadDate { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }

    }
}
