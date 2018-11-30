using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetOptionsByGroupNameCommand : IRequest<Result<GetOptionsByGroupNameResponse>>
    {
        public string GroupName { get; set; }
    }

    public class GetOptionsByGroupNameResponse
    {
        public List<LookupItemView> LookupItems { get; set; }
    }
}