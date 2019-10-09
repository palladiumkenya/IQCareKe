using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.BusinessProcess.Commands.Matrix
{
    public class GetHtsFacilityStatisticsCommand : IRequest<Result<List<HtsFacilityDashboard>>>
    {
    }
}
