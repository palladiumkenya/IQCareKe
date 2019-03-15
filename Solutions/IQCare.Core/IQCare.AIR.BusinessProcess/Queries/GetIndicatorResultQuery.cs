using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetIndicatorResultQuery : IRequest<Result<ReportingFormIndicatorResultViewModel>>
    {
        public int  ReportingPeriodId { get; set; }

    }


    public class ReportingFormIndicatorResultViewModel
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public string StrReportDate { get; set; }
        public DateTime ReportDate { get; set; }
        public List<ReportSectionViewModel> ReportSections { get; set; }
    }

}
