using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

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