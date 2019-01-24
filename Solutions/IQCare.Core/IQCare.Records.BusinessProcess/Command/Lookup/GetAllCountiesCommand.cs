using System.Collections.Generic;
using IQCare.Library;
using MediatR;
using County = IQCare.Common.Core.Models.County;

namespace IQCare.Records.BusinessProcess.Command.Lookup
{
    public class GetAllCountiesCommand : IRequest<Result<List<County>>>
    {
        
    }
}