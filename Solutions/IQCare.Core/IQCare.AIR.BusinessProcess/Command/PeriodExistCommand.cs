using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Core.Domain;
using IQCare.Library;
using MediatR;
namespace IQCare.AIR.BusinessProcess.Command
{
    public class PeriodExistCommand : IRequest<Result<PeriodExistResponse>>
    {

        public DateTime ReportingDate { get; set; }

    }
    public class PeriodExistResponse
    {
        public string Message { get; set; }
        public List<ReportingPeriod> period { get; set; }
    }
}

