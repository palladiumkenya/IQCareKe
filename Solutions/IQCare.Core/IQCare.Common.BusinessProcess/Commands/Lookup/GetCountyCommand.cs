using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetCountyCommand : IRequest<Result<GetCountyResponse>>
    {
        public int? CountyId { get; set; }
        public int? SubcountyId { get; set; }
        public int? WardId { get; set; }
    }

    public class GetCountyResponse
    {
        public List<County> County;
    }
}