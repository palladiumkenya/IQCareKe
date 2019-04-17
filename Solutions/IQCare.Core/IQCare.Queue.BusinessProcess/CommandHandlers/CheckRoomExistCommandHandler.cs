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
    public class CheckRoomExistCommandHandler : IRequestHandler<CheckRoomExistCommand, Result<RoomExistResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<CheckRoomExistCommandHandler>();
        public string message;
        public Boolean exist;

        public CheckRoomExistCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }
        public async Task<Result<RoomExistResponse>> Handle(CheckRoomExistCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var RoomExists = await _queueUnitOfWork.Repository<Rooms>().Get(x => x.RoomName.Trim().ToLower() == request.RoomName.Trim().ToLower()).ToListAsync();

                    if (RoomExists.Count > 0)
                    {
                        message += "The room  already exists ";
                        exist = true;
                    }
                    else
                    {
                        message += " The room does not exists";
                        exist = false;
                    }

                    return Result<RoomExistResponse>.Valid(new RoomExistResponse()
                    {
                        Message = message,
                        Rooms = RoomExists,
                        Exists = exist

                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<RoomExistResponse>.Invalid(ex.Message);
                }

            }

        }
    }
}
