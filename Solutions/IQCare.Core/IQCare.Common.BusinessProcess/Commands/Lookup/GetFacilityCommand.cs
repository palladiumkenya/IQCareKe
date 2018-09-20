using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetFacilityCommand : IRequest<Result<List<FacilityList>>>
    {
        public string MflCode { get; set; }
    }
}