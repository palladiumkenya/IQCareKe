using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizePartnersCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<PARTNER> PARTNERS { get; set; }
    }

    public class SynchronizePartnersResponse
    {
    }
}