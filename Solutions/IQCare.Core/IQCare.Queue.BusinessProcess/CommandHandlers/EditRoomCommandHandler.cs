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
   public class EditRoomCommandHandler : IRequestHandler<EditRoomCommand,Result<EditRoomsResponse>>
    {
        private IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<EditRoomCommandHandler>();
        public string message;
        public int id=0;
        public EditRoomCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }


        public async Task<Result<EditRoomsResponse>> Handle(EditRoomCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var RoomExists =await  _queueUnitOfWork.Repository<Rooms>().Get(x => x.Id==request.Id).FirstOrDefaultAsync();
                    if(RoomExists !=null)
                    {
                        RoomExists.RoomName = request.RoomName;
                        RoomExists.DisplayName = request.DisplayName;
                        RoomExists.Description = request.Description;
                        RoomExists.UpdateDate = DateTime.Now;
                        RoomExists.UpdatedBy = request.UpdatedBy;
                        RoomExists.Active = request.Active;
                        RoomExists.DeleteFlag = request.DeleteFlag;

                        _queueUnitOfWork.Repository<Rooms>().Update(RoomExists);
                       await  _queueUnitOfWork.SaveAsync();
                        message += "The Room details have been updated successfully";
                        id = RoomExists.Id;
                    }

                    return Result<EditRoomsResponse>.Valid(new EditRoomsResponse
                    {
                        Message = message,
                        RoomId = id
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<EditRoomsResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
