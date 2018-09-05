using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientReferral
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ReferreFrom { get; set; }
        public int ReferredTo { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public int ReferredBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
