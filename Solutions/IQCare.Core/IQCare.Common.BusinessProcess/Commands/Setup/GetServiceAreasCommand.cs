using System.Collections.Generic;
using IQCare.Library;
using MediatR;
using ServiceArea = IQCare.Common.Core.Models.ServiceArea;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class GetServiceAreasCommand : IRequest<Result<List<ServiceArea>>>
    {
        
    }
}