using System;
using System.Collections.Generic;
using System.Data;
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
    public class SubmitIndicatorResultCommandHandler : IRequestHandler<SubmitIndicatorResultsCommand, Result<IndicatorResponse>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<SubmitIndicatorResultCommandHandler>();

        public SubmitIndicatorResultCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork;
        }

        public async Task<Result<IndicatorResponse>> Handle(SubmitIndicatorResultsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var transaction = _airUnitOfWork.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    var reportingPeriod = new ReportingPeriod(request.ReportingFormId, request.ReportingDate, request.CreatedBy);
                    await _airUnitOfWork.Repository<ReportingPeriod>().AddAsync(reportingPeriod);

                    var indicatorResults = request.IndicatorResults.Select(x =>
                        new IndicatorResult(reportingPeriod.Id, x.Id, x.ResultText, x.ResultNumeric, request.CreatedBy));

                    await _airUnitOfWork.Repository<IndicatorResult>().AddRangeAsync(indicatorResults);
                    await _airUnitOfWork.SaveAsync();

                    transaction.Commit();

                    return Result<IndicatorResponse>.Valid(new IndicatorResponse()
                    {
                        Message = "Indicator results added succesfully",
                        ReportingFormId = request.ReportingFormId
                    });
                }
               
            }
            catch (Exception ex)
            {
                _logger.Error(ex,
                    $"An error occured while submitting indicator results for period {request.ReportingDate} and ReportId {request.ReportingFormId}");

                return Result<IndicatorResponse>.Invalid("An error occured while submittig indicator results");
            }

        }
    }
}
