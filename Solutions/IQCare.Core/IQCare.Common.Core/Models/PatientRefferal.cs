using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientRefferal
    {
        public PatientRefferal()
        {

        }
        public PatientRefferal(int patientId, int masterVisitId, int referredFrom, int ? referredTo, 
            string referralReason, DateTime ? referralDate, int ? referredBy, int createdBy, int deleteFlag)
        {
            PatientId = patientId;
            PatientMasterVisitId = masterVisitId;
            ReferredFrom = referredFrom;
            ReferredTo = referredTo;
            ReferralReason = referralReason;
            ReferralDate = referralDate;
            ReferredBy = referredBy;
            CreateBy = createdBy;
            CreateDate = DateTime.Now;
        }
        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public int ReferredFrom { get; private set; }
        public int ? ReferredTo { get; private set; }
        public string ReferralReason { get; private set; }
        public DateTime ? ReferralDate { get; private set; }
        public int ? ReferredBy { get; private set; }
        public int DeleteFlag { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int CreateBy { get; private set; }

        public void UpdateReferralInfo(DateTime ? referralDate, string referralReason, int ? referredBy, int referredFrom, int ? referredTo)
        {
            ReferralDate = referralDate;
            ReferralReason = referralReason;
            ReferredBy = referredBy;
            ReferredFrom = referredFrom;
            ReferredTo = referredTo;
        }
    }
}
