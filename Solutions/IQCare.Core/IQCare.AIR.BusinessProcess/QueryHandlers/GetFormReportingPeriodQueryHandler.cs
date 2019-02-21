using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetFormReportingPeriodQueryHandler : IRequestHandler<GetFormReportingPeriodQuery, Result<List<FormReportingPeriodViewModel>>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger = Log.ForContext<GetFormReportingPeriodQueryHandler>();

        public GetFormReportingPeriodQueryHandler(IAirUnitOfWork airUnitOfWork, IMapper mapper)
        {
            _airUnitOfWork = airUnitOfWork;
            _mapper = mapper;

        }
        public Task<Result<List<FormReportingPeriodViewModel>>> Handle(GetFormReportingPeriodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reportingPeriods = _airUnitOfWork.Repository<ReportingPeriod>().Get(x => x.DeleteFlag == false)
                        .Include(x => x.ReportingForm)
                        .OrderByDescending(x => x.ReportDate).AsEnumerable();

                var reportingPeriodViewModel = _mapper.Map<List<FormReportingPeriodViewModel>>(reportingPeriods);

                return Task.FromResult(Result<List<FormReportingPeriodViewModel>>.Valid(reportingPeriodViewModel));
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured fetching form reporting periods";
                _logger.Error(errorMessage, ex);

                return Task.FromResult(Result<List<FormReportingPeriodViewModel>>.Invalid(errorMessage));

            }
        }
    }
}
