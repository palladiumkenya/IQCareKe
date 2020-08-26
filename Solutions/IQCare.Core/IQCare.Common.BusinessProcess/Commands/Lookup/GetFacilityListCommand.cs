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

    public class FacilityViewModel
    {
        public int Id { get; set; }
        public string MflCode { get; set; }
        public string Name { get; set; }
        public string PositionId { get; set; }

        public int Frequency { get; set; }
    }

    public class GetActiveFaciltyCommand : IRequest<Result<FacilityViewModel>>
    {
       

    }
}