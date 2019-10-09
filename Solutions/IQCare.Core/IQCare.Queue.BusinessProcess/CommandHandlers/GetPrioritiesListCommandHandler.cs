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
    public class GetPrioritiesListCommandHandler : IRequestHandler<GetPrioritiesListCommand, Result<GetPrioritiesListResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<GetPrioritiesListCommandHandler>();

        public GetPrioritiesListCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }


        public async Task<Result<GetPrioritiesListResponse>> Handle(GetPrioritiesListCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {

                    var priorities = await _queueUnitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "Priority").ToListAsync();



                    return Result<GetPrioritiesListResponse>.Valid(new GetPrioritiesListResponse
                    {
                        Priority = priorities,


                    });

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<GetPrioritiesListResponse>.Invalid(ex.Message);
                }

            }
        }

    }
}