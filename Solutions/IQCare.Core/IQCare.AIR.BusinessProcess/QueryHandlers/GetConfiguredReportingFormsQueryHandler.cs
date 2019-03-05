using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Queries;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.AIR.BusinessProcess.QueryHandlers
{
    public class GetConfiguredReportingFormsQueryHandler : IRequestHandler<GetConfiguredReportingFormsQuery, Result<List<ReportingFormViewModel>>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<GetConfiguredReportingFormsQueryHandler>();

        public GetConfiguredReportingFormsQueryHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork;
        }

        public Task<Result<List<ReportingFormViewModel>>> Handle(GetConfiguredReportingFormsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var configuredReportingForms = _airUnitOfWork.Repository<ReportingForm>().Get(x => x.DeleteFlag == false)
                        .OrderByDescending(x => x.DateCreated).AsEnumerable();

                var reportingFormsModel = configuredReportingForms.Select(x => new ReportingFormViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateCreated = x.DateCreated
                }).ToList();

                return Task.FromResult(Result<List<ReportingFormViewModel>>.Valid(reportingFormsModel));
            }
            catch (Exception ex)
            {
                string message = "An error occured while fetching configured active reporting forms";
                _logger.Error(ex, message);
                return Task.FromResult(Result<List<ReportingFormViewModel>>.Invalid(message));
            }
        }
    }
}
