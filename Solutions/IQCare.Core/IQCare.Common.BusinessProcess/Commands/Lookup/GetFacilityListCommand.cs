using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetFacilityListCommand : IRequest<Result<GetFacilityListResponse>>
    {
        
    }

    public class GetFacilityListResponse
    {
        public List<FacilityList> FacilityList { get; set; }
    }
}