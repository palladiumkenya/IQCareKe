using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizeClientsCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<NEWCLIENT> CLIENTS { get; set; }
    }

    public class SynchronizeClientsResponse
    {
        public string afyaMobileId { get; set; }
    }
}