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
   public class GetRoomsCommandHandler:IRequestHandler<GetRoomsCommand,Result<GetRoomsResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<GetRoomsCommandHandler>();


        public GetRoomsCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }



        public async Task<Result<GetRoomsResponse>> Handle(GetRoomsCommand request,CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {

                try
                {
                    var Rooms = await _queueUnitOfWork.Repository<Rooms>().Get(x => x.DeleteFlag == false).ToListAsync();
                    return Result<GetRoomsResponse>.Valid(new GetRoomsResponse()
                    {
                        RoomsList = Rooms
                    });

                }
                catch(Exception ex)
                {
                    return Result<GetRoomsResponse>.Invalid(ex.Message);
                }
            }
        } 
   }
}
