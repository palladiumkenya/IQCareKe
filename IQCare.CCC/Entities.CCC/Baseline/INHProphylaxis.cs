using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Baseline
{
    [Serializable]
    [Table("INHProphylaxis")]
    public class INHProphylaxis : BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime StartDate { get; set; }
        public bool Complete { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
