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
    public class AddQueueCommandHandler : IRequestHandler<AddQueueCommand, Result<AddQueueResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<AddQueueCommandHandler>();

        public AddQueueCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<AddQueueResponse>> Handle(AddQueueCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    QueueWaitingList qwl = new QueueWaitingList();
                    qwl.PatientId = request.PatientId;
                    qwl.Priority = request.Priority;
                    qwl.ServiceRoomId = request.ServiceRoomId;
                    qwl.Status = request.Status;
                    qwl.CreateDate = DateTime.Now;
                    qwl.CreatedBy = request.CreatedBy;

                    await _queueUnitOfWork.Repository<QueueWaitingList>().AddAsync(qwl);
                    await _queueUnitOfWork.SaveAsync();
                    return Result<AddQueueResponse>.Valid(new AddQueueResponse()
                    {
                        PatientId = qwl.PatientId,
                        Message = "The Patient  has been added to the waiting List successfully"

                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<AddQueueResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
