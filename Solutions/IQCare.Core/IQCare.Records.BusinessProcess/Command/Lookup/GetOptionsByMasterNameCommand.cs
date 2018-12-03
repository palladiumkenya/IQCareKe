using System.Collections.Generic;
using IQCare.Library;
using MediatR;
using LookupItemView = IQCare.Common.Core.Models.LookupItemView;

namespace IQCare.Records.BusinessProcess.Command.Lookup
{
    public class GetOptionsByMasterNameCommand : IRequest<Result<List<LookupItemView>>>
    {
        public string MasterName { get; set; }
    }
}