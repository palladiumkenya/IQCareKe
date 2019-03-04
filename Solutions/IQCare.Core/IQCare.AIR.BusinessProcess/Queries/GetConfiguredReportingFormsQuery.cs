using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.AIR.BusinessProcess.Queries
{
    public class GetConfiguredReportingFormsQuery : IRequest<Result<List<ReportingFormViewModel>>>
    {

    }
}
