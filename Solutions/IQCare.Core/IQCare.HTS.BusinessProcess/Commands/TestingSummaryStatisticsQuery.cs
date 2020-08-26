using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class TestingSummaryStatisticsQuery : IRequest<Result<List<TestingSummaryStatistics>>>
    {

    }
}
