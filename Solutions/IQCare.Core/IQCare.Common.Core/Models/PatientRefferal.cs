using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public class PatientRefferal
    {
      public int  Id { get; set; }
      public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public string ReferredBy { get; set; }
        public int  DeleteFlag { get; set; }
    }
}
