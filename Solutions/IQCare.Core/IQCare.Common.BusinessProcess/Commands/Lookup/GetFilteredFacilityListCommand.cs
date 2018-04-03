using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetFilteredFacilityListCommand : IRequest<Result<GetFilteredFacilityListResponse>>
    {
        public string searchString { get; set; }
    }

    public class GetFilteredFacilityListResponse
    {
        public List<FacilityList> FacilityList { get; set; }
    }
}