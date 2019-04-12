using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Queue.BusinessProcess.Command;
using IQCare.Queue.Core.Models;
using IQCare.Queue.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Queue.BusinessProcess.CommandHandlers
{
    public class GetServiceAreasCommandHandler : IRequestHandler<GetServiceAreasCommand, Result<GetServiceAreasResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<GetPrioritiesListCommandHandler>();

        public GetServiceAreasCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<GetServiceAreasResponse>> Handle(GetServiceAreasCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var ServiceArea = await _queueUnitOfWork.Repository<ServiceArea>().Get(x => x.DeleteFlag == false).ToListAsync();


                    return Result<GetServiceAreasResponse>.Valid(new GetServiceAreasResponse
                    {
                       
                        serviceAreas = ServiceArea

                    });

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<GetServiceAreasResponse>.Invalid(ex.Message);

                }
            }
        }
    }
}
