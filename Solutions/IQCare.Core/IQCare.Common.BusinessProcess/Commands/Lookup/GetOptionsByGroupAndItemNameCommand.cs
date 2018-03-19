using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetOptionsByGroupAndItemNameCommand : IRequest<Result<GetOptionsByGroupAndItemNameResponse>>
    {
        public string GroupName { get; set; }
        public string ItemName { get; set; }
    }

    public class GetOptionsByGroupAndItemNameResponse
    {
        public int ItemId { get; set; }
    }
}