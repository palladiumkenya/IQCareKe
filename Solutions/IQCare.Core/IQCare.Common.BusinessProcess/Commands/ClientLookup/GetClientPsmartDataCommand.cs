using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.ClientLookup
{
    public class GetClientPsmartDataCommand : IRequest<Result<List<HivTestTracker>>>
    {
        public int PersonId { get; set; }
    }
}