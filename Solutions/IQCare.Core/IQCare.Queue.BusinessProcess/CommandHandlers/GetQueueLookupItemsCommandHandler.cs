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
    public class GetQueueLookupItemsCommandHandler : IRequestHandler<GetQueueLookupItemsCommand, Result<GetQueueLookupItemsResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<GetQueueLookupItemsCommandHandler>();
    

        public GetQueueLookupItemsCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<GetQueueLookupItemsResponse>> Handle(GetQueueLookupItemsCommand request,CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var roomlist = await _queueUnitOfWork.Repository<Rooms>().Get(x=>x.DeleteFlag ==false).ToListAsync();
                    var priorities =await  _queueUnitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "Priority").ToListAsync();
                    var servicePoints =await _queueUnitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "ServicePoint").ToListAsync();
                    var ServiceArea =await _queueUnitOfWork.Repository<ServiceArea>().Get(x => x.DeleteFlag == false).ToListAsync();

                  
                    return Result<GetQueueLookupItemsResponse>.Valid(new GetQueueLookupItemsResponse
                    {
                        Priority = priorities,
                        rooms = roomlist,
                        ServicePoint = servicePoints,
                        serviceAreas= ServiceArea

                    });

                }
                catch(Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<GetQueueLookupItemsResponse>.Invalid(ex.Message);
                }
           
            }
        }
    }
}
