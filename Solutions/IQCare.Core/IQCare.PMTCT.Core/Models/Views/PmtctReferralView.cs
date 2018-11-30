using System;

namespace IQCare.PMTCT.Core.Models.Views
{
    public class PmtctReferralView
    {
        public int Id { get; set; }
       public int PatientId { get; set; }
       public int ReferredFrom { get; set; }
       public string ReferredFromName { get; set; }
       public int PatientMasterVisitId { get; set; }
      public string  ReferralReason { get; set; }
      public int   ReferredTo { get; set; }
      public string  RefferedToName { get; set; }
       public DateTime ReferralDate { get; set; }
      public string  ReferredBy { get; set; }
      public Boolean  DeleteFlag { get; set; }
      public int  CreateBy { get; set; }
      public  DateTime CreateDate { get; set; } 
    }
}