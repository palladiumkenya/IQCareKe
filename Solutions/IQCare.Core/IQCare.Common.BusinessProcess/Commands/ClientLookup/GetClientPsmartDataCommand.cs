using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.ClientLookup
{
    public class GetClientPsmartDataCommand : IRequest<Result<List<HivTestTracker>>>
    {
        public int PersonId { get; set; }
    }
}