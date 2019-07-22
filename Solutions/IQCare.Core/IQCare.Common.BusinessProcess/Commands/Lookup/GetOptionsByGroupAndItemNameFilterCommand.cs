using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetOptionsByGroupAndItemNameFilterCommand : IRequest<Result<List<LookupItemView>>>
    {
        public string GroupName { get; set; }
        public string ItemName { get; set; }
    }
}