using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Assessment
{
    [Serializable]
    [Table("PatientPsychosocialCriteria")]
    public  class PatientPsychoscialCriteria
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Entities.CCC.Enrollment.PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public bool BenefitART { get;set; }
        public bool Alcohol { get; set; }
        public bool Depression { get; set; }
        public bool Disclosure { get; set; }
        public bool AdministerART { get; set; }
        public bool AdherenceBarriers { get; set; }
        public bool AccurateLocator { get; set; }
    }
}
