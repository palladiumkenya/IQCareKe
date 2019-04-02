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
    public class CheckLinkageServiceExistsCommandHandler:IRequestHandler<CheckLinkageServiceExistsCommand,Result<CheckLinkageServiceExistsResponse>>
    {

        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<CheckLinkageServiceExistsCommandHandler>();
        public List<ServiceRoom> servicerooms;
        public Boolean exist;
        public CheckLinkageServiceExistsCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<CheckLinkageServiceExistsResponse>> Handle(CheckLinkageServiceExistsCommand request,CancellationToken cancellationToken)
        {
           using (_queueUnitOfWork)
            {
                try
                {
                    var LinkageServiceArea = await _queueUnitOfWork.Repository<ServiceRoom>().Get(x => x.ServiceAreaid == request.ServiceAreaId && x.ServicePointId == request.ServicePointId && x.RoomId == request.RoomId && x.DeleteFlag == false).ToListAsync();
                    if(LinkageServiceArea !=null)
                    {
                        servicerooms = LinkageServiceArea;
                        exist = true;
                    }
                    else
                    {
                        exist = false;
                    }

                    return Result<CheckLinkageServiceExistsResponse>.Valid(new CheckLinkageServiceExistsResponse() {

                        Exists = exist,
                        ServiceRooms=servicerooms
              
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<CheckLinkageServiceExistsResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
