using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Matrix
{
    public class GetILStatisticsCommand : IRequest<Result<ILStatisticsResponse>>
    {
    }

    public class ILStatisticsResponse
    {
        public int Outbox { get; set; }
        public int Inbox { get; set; }
    }
}
