using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientReferral")]
    public class PatientReferral:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int ReferredFrom { get; set; }
        public int ReferredTo { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public String ReferredBy { get; set; }
    }
}
