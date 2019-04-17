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
    public class EditQueueCommandHandler : IRequestHandler<EditQueueCommand, Result<EditQueueResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<EditQueueCommandHandler>();
        public string message;
        public Boolean updated;
        public int Id;

        public EditQueueCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }
        public async Task<Result<EditQueueResponse>> Handle(EditQueueCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var editqueue = await _queueUnitOfWork.Repository<QueueWaitingList>().Get(x => x.Id == request.Id).FirstOrDefaultAsync();
                    if(editqueue !=null )
                    {
                        editqueue.Status = request.Status;
                        editqueue.DeleteFlag = request.DeleteFlag;
                        editqueue.UpdateDate = DateTime.Now;
                        editqueue.UpdatedBy = request.UpdatedBy;
                        _queueUnitOfWork.Repository<QueueWaitingList>().Update(editqueue);
                       await  _queueUnitOfWork.SaveAsync();
                        Id = editqueue.Id;
                        updated = true;
                        
                    }

                    return Result<EditQueueResponse>.Valid(new EditQueueResponse()
                    {
                        Message = "The waitinglist has been updated successfully",
                        Id = Id,
                        Updated = true
                    });

                }
                catch (Exception ex)
                {
                    return Result<EditQueueResponse>.Invalid(ex.Message);
                }

            }

        }
    }
}
