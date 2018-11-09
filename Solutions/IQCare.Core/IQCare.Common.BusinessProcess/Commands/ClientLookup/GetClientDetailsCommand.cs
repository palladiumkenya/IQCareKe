using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.ClientLookup
{
    public class GetClientDetailsCommand : IRequest<Result<GetClientDetailsResponse>>
    {
        public int ServiceAreaId { get; set; }
        public int PatientId { get; set; }
    }

    public class GetClientDetailsResponse
    {
        public List<PatientLookupView> PatientLookup { get; set; }
    }
}