using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetFormValueQuery : IRequest<Result<GetFormValueResponse>>
    {
        public int Id { get; set; }


    }
    public class GetFormValueResponse
    {
        public List<ReportingValues> reportingValues { get; set; }
    }

    public class ReportingValues
    {
        public int ReportingFormId { get; set; }

        public int ReportingId { get; set; }

        public DateTime ReportDate { get; set; }

        public int IndicatorId { get; set; }


        public int ReportingPeriodId { get; set; }

        public decimal ResultNumeric { get; set; }

        public string ResultText { get; set; }


    }
}
