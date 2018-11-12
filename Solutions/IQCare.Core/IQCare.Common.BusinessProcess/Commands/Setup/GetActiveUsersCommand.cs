using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class GetActiveUsersCommand : IRequest<Result<List<User>>>
    {
        
    }
}