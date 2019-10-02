using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Matrix
{
    public class GetILMessageStatsCommand : IRequest<Result<List<ILMessageStats>>>
    {

    }
}
