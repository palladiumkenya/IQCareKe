using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class GetActiveFacilitiesCommand : IRequest<Result<List<Facility>>>
    {
        
    }
}