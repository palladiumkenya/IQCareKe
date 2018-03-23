using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Consent
{
    public class AddConsentCommand : IRequest<Result<AddConsentResponse>>
    {
        public int PatientID { get; set; }
        public int PatientMasterVisitId { get; set; }
        public List<KeyValuePair<string, int>> ConsentType { get; set; }
        public string DeclineReason { get; set; }
        public int ServiceAreaId { get; set; }
        public DateTime ConsentDate { get; set; }
        public int UserId { get; set; }
    }

    public class AddConsentResponse
    {
        public bool IsConsentAdded { get; set; }
    }
}