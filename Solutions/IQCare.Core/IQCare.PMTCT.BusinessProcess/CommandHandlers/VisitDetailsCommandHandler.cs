using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class VisitDetailsCommandHandler : IRequestHandler<VisitDetailsCommand, Result<VisitDetails>>
    {
        private readonly IPmtctUnitOfWork _pmtctUnitOfWork;
        public VisitDetailsCommandHandler(IPmtctUnitOfWork pmtctUnitOfWork)
        {
            _pmtctUnitOfWork = pmtctUnitOfWork ?? throw new ArgumentNullException(nameof(pmtctUnitOfWork));
        }

        public async Task<Result<VisitDetails>> Handle(VisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_pmtctUnitOfWork)
            {
                try
                {
                    VisitDetails visitDetails = new VisitDetails();
                    return Result<VisitDetails>.Valid(visitDetails);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<VisitDetails>.Invalid(e.Message);
                }
            }
        }
    }
}