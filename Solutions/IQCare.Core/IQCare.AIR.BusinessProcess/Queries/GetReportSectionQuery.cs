using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetReportSectionQuery : IRequest<Result<List<ReportSectionViewModel>>>
    {
        public int FormId { get; set; }

    }

    public class GetReportSubSectionsQuery : IRequest<Result<List<ReportSubSectionViewModel>>>
    {

        public int SectionId { get; set; }

    }

}
