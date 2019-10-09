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
   public  class GetRoomsAndServicePointCommandHandler:IRequestHandler<GetRoomsAndServicePointCommand,Result<GetRoomsAndServicePointResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<GetRoomsAndServicePointCommandHandler>();
        public List<ServiceRoomView> sRoomView;
       

        public GetRoomsAndServicePointCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork=queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }
        public async Task<Result<GetRoomsAndServicePointResponse>> Handle(GetRoomsAndServicePointCommand request,CancellationToken cancellationToken)
        {

            using (_queueUnitOfWork)
            {
                try
                {
                    if(request.ServiceArea !=null )
                    {
                        sRoomView = new List<ServiceRoomView>();
                        sRoomView = await _queueUnitOfWork.Repository<ServiceRoomView>().Get(x => x.ServiceAreaId == request.ServiceArea).ToListAsync();
                       
                    }

                   
                    return Result<GetRoomsAndServicePointResponse>.Valid(new GetRoomsAndServicePointResponse()
                    {
                       ServiceRoomViews=sRoomView
                        

                    });
                }
                catch(Exception e)
                {
                    Log.Error(e.Message);
                    return Result<GetRoomsAndServicePointResponse>.Invalid(e.Message);
                }
            
            }
        }

    }
}
