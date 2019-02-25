using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.AIR.BusinessProcess.CommandHandlers
{
    public class EditIndicatorResultsCommandHandler : IRequestHandler<EditIndicatorResultsCommand, Result<EditIndicatorResponse>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<EditIndicatorResultsCommandHandler>();
        public EditIndicatorResultsCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork;
        }
        public Task<Result<EditIndicatorResponse>> Handle(EditIndicatorResultsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reportingPeriod = _airUnitOfWork.Repository<ReportingPeriod>()
                    .Get(x => x.Id == request.ReportingPeriodId).SingleOrDefault();
                if(reportingPeriod == null)
                    return Task.FromResult(Result<EditIndicatorResponse>.Invalid($"Reporting Period Id {request.ReportingPeriodId} not found"));

                reportingPeriod.Update(request.ReportingDate);
                _airUnitOfWork.Repository<ReportingPeriod>().Update(reportingPeriod);

                var indicatorResults = _airUnitOfWork.Repository<IndicatorResult>()
                       .Get(x => x.ReportingPeriodId == request.ReportingPeriodId).AsEnumerable();

                if (!indicatorResults.Any())
                    return Task.FromResult(Result<EditIndicatorResponse>.Invalid($"Indicator results for period Id {request.ReportingPeriodId} not found"));

                if (request.IndicatorResults == null || !request.IndicatorResults.Any())
                    return Task.FromResult(Result<EditIndicatorResponse>.Invalid("Indicator Results to be edited not found"));

                var updatedIndicatorResults = indicatorResults.Where(x =>
                        request.IndicatorResults.Any(i => i.Id == x.IndicatorId && (x.ResultNumeric != i.ResultNumeric || x.ResultText != x.ResultText)))
                    .AsEnumerable();

                foreach (var result in updatedIndicatorResults)
                {
                    var newResultValue = request.IndicatorResults.SingleOrDefault(x => x.Id == result.IndicatorId);
                    if (newResultValue == null)
                        continue;
                    result.UpdateResult(newResultValue.ResultNumeric, newResultValue.ResultText);
                    _airUnitOfWork.Repository<IndicatorResult>().Update(result);
                }

                _airUnitOfWork.Save();

                return Task.FromResult(Result<EditIndicatorResponse>.Valid(new EditIndicatorResponse
                {
                    Message = $"Indicator results for report periodId{request.ReportingPeriodId} updated succesfully",
                    ReportingPeriodId = request.ReportingPeriodId
                }));
            }
            catch (Exception ex)
            {
                string message =$"An error occured while editing indicator results for reporting periodId {request.ReportingPeriodId}";

                _logger.Error(ex, message);
                return Task.FromResult(Result<EditIndicatorResponse>.Invalid(message));
            }

        }
    }
}
