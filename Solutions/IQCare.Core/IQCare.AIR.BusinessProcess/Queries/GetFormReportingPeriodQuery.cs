using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetFormReportingPeriodQuery : IRequest<Result<List<FormReportingPeriodViewModel>>>
    {
        public int ? ReportingFormId { get; set; }

    }

    public class FormReportingPeriodViewModel
    {
        public int Id { get; set; }

        public int ReportingFormId { get; set; }
        public string ReportName { get; set; }
        public DateTime ReportDate { get; set; }
        public string StrReportDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
