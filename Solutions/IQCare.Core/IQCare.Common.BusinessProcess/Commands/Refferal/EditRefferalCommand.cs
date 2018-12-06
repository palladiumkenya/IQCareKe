using System;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Refferal
{
    public class EditRefferalCommand:IRequest<Result<EditRefferalCommandResponse>>
    {
        public int Id { get; set; }
        public DateTime ReferralDate { get; set; }
        public string ReferralReason { get; set; }
        public int ReferredBy { get; set; }
        public int ReferredFrom { get; set; }
        public int ReferredTo { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }

    public class EditRefferalCommandResponse
    {
        public int Id { get; set; }
    }
}
