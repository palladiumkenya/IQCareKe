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
    public class GetQueueListByRoomIdCommandHandler : IRequestHandler<GetQueueListByRoomIdCommand, Result<GetQueueListByRoomsResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<GetQueueListByRoomIdCommandHandler>();

        public GetQueueListByRoomIdCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<GetQueueListByRoomsResponse>> Handle(GetQueueListByRoomIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sql = "exec pr_OpenDecryptedSession;" +
                              $"SELECT * FROM WaitingListView WHERE RoomId = {request.RoomId} order by PriorityRank asc,CreateDate desc; " +
                              $"exec [dbo].[pr_CloseDecryptedSession];";

                var result = await _queueUnitOfWork.Repository<WaitingListView>().FromSql(sql);
                var finalresult = result.ToList().Where(x => x.DeleteFlag == false && x.Status == false).ToList();

                return Result<GetQueueListByRoomsResponse>.Valid(new GetQueueListByRoomsResponse()
                {
                    waitingListViews = finalresult

                });
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Result<GetQueueListByRoomsResponse>.Invalid(ex.Message);
            }


        }

    }
    
}
