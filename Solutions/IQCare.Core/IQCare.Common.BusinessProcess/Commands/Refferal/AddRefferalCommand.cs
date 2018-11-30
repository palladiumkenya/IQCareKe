using IQCare.Common.Core.Models;
using MediatR;
using System;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.Commands.Refferal
{
    public class AddPatientReferralCommand : IRequest<Result<AddPatientReferralResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ReferredFrom { get; set; }
        public int ? ReferredTo { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ? ReferralDate { get; set; }
        public int ? ReferredBy { get; set; }
        public int DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AddPatientReferralResponse
    {

        public int PatientReferralId { get; set; }
  
    }
}
