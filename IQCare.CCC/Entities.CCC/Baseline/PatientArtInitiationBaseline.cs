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
    [Table("ARTInitiationBaseline")]
    public class PatientArtInitiationBaseline:BaseEntity
    {

        private PatientArtInitiationBaseline() { }

        [Key]
        public int Id { get; set; }
        [ForeignKey("patient")]
        public bool HBVInfected { get; set; }
        public bool Pregnant { get; set; }
        public bool TBInfected { get; set; }
        public int WHOStage { get; set; }
        public bool Breastfeeding { get; set; }
        public int CD4Count { get; set; }
        public int ViralLoad { get; set; }
        public DateTime ViralLoadDate { get; set; }
        public decimal MUAC { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string ARTCohort { get; set; }
        public DateTime FirstlineDate { get; set; }
        public int StartRegimen { get; set; } 


        public virtual Patient Patient { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }



    }
}
