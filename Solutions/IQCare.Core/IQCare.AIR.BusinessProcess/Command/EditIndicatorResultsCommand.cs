using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Command
{
    public class EditIndicatorResultsCommand : IRequest<Result<EditIndicatorResponse>>
    {
        public int ReportingPeriodId { get; set; }
        public int ReportingFormId { get; set; }
        public DateTime ReportingDate { get; set; }
        public List<IndicatorResultCommand> IndicatorResults { get; set; }
    }

    public class EditIndicatorResponse
    {
        public int ReportingPeriodId { get; set; }
        public string Message { get; set; }
    }
}
