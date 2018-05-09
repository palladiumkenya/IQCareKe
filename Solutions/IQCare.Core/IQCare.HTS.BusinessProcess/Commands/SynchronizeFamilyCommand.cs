using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizeFamilyCommand : IRequest<Result<SynchronizeFamilyResponse>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<FAMILY> FAMILY { get; set; }
    }

    public class SynchronizeFamilyResponse
    {
    }
}