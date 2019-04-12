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
   public  class GetLinkedRoomsCommandHandler : IRequestHandler<GetLinkedRoomsCommand, Result<GetLinkedRoomsResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<GetRoomsAndServicePointCommandHandler>();
        public List<ServiceRoomView> sRoomView;

        public List<RoomServiceList> RoomServiceLists=new List<RoomServiceList>();

        public GetLinkedRoomsCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }
        public async Task<Result<GetLinkedRoomsResponse>> Handle(GetLinkedRoomsCommand request, CancellationToken cancellationToken)
        {

            using (_queueUnitOfWork)
            {
                try
                {
                   
                        sRoomView = new List<ServiceRoomView>();
                        sRoomView = await _queueUnitOfWork.Repository<ServiceRoomView>().Get(x => x.DeleteFlag==false).OrderByDescending(x=>x.Id).ToListAsync();

                    sRoomView.ForEach(x =>
                    {
                        RoomServiceList rl = new RoomServiceList();
                        rl.Id = x.Id;
                        rl.Name = x.ServiceAreaName + "-" + x.ServicePointName + "-" + x.RoomName;
                        RoomServiceLists.Add(rl);
                    });


                    return Result<GetLinkedRoomsResponse>.Valid(new GetLinkedRoomsResponse()
                    {
                        ServiceRoomViews = sRoomView,
                        RoomServices = RoomServiceLists

                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<GetLinkedRoomsResponse>.Invalid(e.Message);
                }

            }
        }
    }
}
