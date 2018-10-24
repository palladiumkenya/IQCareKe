using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands
{
    class PatientReferalsCommand : IRequest<Result<AddPatientReferalResponse>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ReferralReason { get; set; }
        public DateTime ReferralDate { get; set; }
        public string ReferredBy { get; set; }
        public bool DeleteFlag { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
    public class AddPatientReferalResponse
    {
        public int Id { get; set; }

    }
}
