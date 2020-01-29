using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Setup
{
    public class GetIdentifiersCommand : IRequest<Result<List<Identifier>>>
    {
    }
}
