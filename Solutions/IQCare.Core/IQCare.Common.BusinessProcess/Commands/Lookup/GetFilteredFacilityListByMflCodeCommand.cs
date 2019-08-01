using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetFilteredFacilityListByMflCodeCommand : IRequest<Result<GetFilteredFacilityListMflCodeResponse>>
    {
        public string searchString { get; set; }
    }

    public class GetFilteredFacilityListMflCodeResponse
    {
        public List<FacilityList> FacilityList { get; set; }
    }
}
