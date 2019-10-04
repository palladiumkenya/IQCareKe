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
namespace IQCare.Queue.BusinessProcess.CommandHandlers
{
    public class AddRoomCommandHandler:IRequestHandler<AddRoomCommand,Result<RoomResultResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<AddRoomCommandHandler>();

        public AddRoomCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }


        public async Task<Result<RoomResultResponse>> Handle(AddRoomCommand request,CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    Rooms rm= new Rooms();
                    rm.RoomName = request.RoomName;
                    rm.DisplayName = request.DisplayName;
                    rm.CreateDate = DateTime.Now;
                    rm.CreatedBy = request.CreatedBy;
                    rm.Active = request.Active;
                    rm.DeleteFlag = request.DeleteFlag;
                    rm.Description = request.Description;

                    await _queueUnitOfWork.Repository<Rooms>().AddAsync(rm);
                    await _queueUnitOfWork.SaveAsync();
                    return Result<RoomResultResponse>.Valid(new RoomResultResponse()
                    {
                        Id = rm.Id,
                        Message = "The room has been added successfully"

                    });
                }
                catch(Exception e)
                {
                    Log.Error(e.Message);
                    return Result<RoomResultResponse>.Invalid(e.Message);

                }
           

            }

        }
    }
}
