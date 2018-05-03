using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class SynchronizePartnersCommand : IRequest<Result<SynchronizePartnersResponse>>
    {
        
    }

    public class SynchronizePartnersResponse
    {
    }
}