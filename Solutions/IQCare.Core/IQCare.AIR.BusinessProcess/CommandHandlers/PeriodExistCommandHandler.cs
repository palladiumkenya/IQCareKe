using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IQCare.AIR.BusinessProcess.Command;
using IQCare.AIR.Core.Domain;
using IQCare.AIR.Infrastructure.UnitOfWork;
using IQCare.Library;
using MediatR;
using Serilog;
using System.Threading.Tasks;

namespace IQCare.AIR.BusinessProcess.CommandHandlers
{
    public class PeriodExistCommandHandler: IRequestHandler<PeriodExistCommand , Result<PeriodExistResponse>>
    {
        private readonly IAirUnitOfWork _airUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<PeriodExistCommandHandler>();
        public string message;
        public PeriodExistCommandHandler(IAirUnitOfWork airUnitOfWork)
        {
            _airUnitOfWork = airUnitOfWork ?? throw new ArgumentNullException(nameof(airUnitOfWork));
        }
        public Task<Result<PeriodExistResponse>> Handle(PeriodExistCommand request,CancellationToken cancellationToken)
        {
            try
            {
                var reportingPeriodExists = _airUnitOfWork.Repository<ReportingPeriod>().Get(x =>
                  x.ReportDate.Month == request.ReportingDate.Month && x.ReportDate.Year == request.ReportingDate.Year).ToList();
                if(reportingPeriodExists !=null)
                {
                    message += "The reporting period exists";

                }
                else
                {
                    message += "ReportingPeriod does not exist";
                }


                return Task.FromResult(Result<PeriodExistResponse>.Valid(new PeriodExistResponse()
                {
                    Message = message,
                    period = reportingPeriodExists
                }));
            }
            catch(Exception ex)
            {
                return Task.FromResult(Result<PeriodExistResponse>.Invalid(ex.Message));

            }
        }

    }
   
}
