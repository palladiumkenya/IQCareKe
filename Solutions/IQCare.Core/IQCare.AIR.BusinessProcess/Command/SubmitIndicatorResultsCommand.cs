using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Command
{
    public class SubmitIndicatorResultsCommand : IRequest<Result<IndicatorResponse>>
    {
        public DateTime ReportingDate { get; set; }
        public int ReportingFormId { get; set; }
        public int CreatedBy { get; set; }
        public List<IndicatorResultCommand> IndicatorResults { get; set; }
    }

    public class IndicatorResultCommand
    {
        public int Id { get; set; }
        public string ResultText { get; set; }
        public decimal ? ResultNumeric { get; set; }

    }

    public class IndicatorResponse
    {
        public string Message { get; set; }
        public int ReportingFormId { get; set; }       
    }
}
