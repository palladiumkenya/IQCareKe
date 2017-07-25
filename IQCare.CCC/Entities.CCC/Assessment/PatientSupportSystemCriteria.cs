using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Assessment
{
    [Serializable]
    [Table("PatientSupportSystemCriteria")]
   public  class PatientSupportSystemCriteria
    {
        public int Id;
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Entities.CCC.Enrollment.PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public bool TakingART { get; set; }
        public bool TSIdentified { get; set; }
        public bool EnrollSMSReminder { get; set; }
        public bool OtherSupportSystems { get; set; }
    }
}
