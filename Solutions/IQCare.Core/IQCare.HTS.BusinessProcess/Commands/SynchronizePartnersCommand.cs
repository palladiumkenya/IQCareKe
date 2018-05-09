using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizePartnersCommand : IRequest<Result<SynchronizePartnersResponse>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<PARTNER> PARTNERS { get; set; }
    }

    public class SynchronizePartnersResponse
    {
    }
}