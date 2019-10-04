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
    public class CheckQueueCommandHandler : IRequestHandler<CheckQueueExistCommand, Result<CheckQueueResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<CheckQueueCommandHandler>();
        public string message;
        public Boolean exist;

        public CheckQueueCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }
        public async Task<Result<CheckQueueResponse>> Handle(CheckQueueExistCommand request, CancellationToken cancellationToken)
        {
            using (_queueUnitOfWork)
            {
                try
                {
                    var QueueExists = await _queueUnitOfWork.Repository<QueueWaitingList>().Get(x => x.PatientId==request.PatientId  && x.DeleteFlag==false && x.Status==false).ToListAsync();

                    if (QueueExists.Count > 0)
                    {
                        message += "The room  already exists ";
                        exist = true;
                    }
                    else
                    {
                        message += " The room does not exists";
                        exist = false;
                    }

                    return Result<CheckQueueResponse>.Valid(new CheckQueueResponse()
                    {
                        Message = message,
                        Exists = exist

                    });

                }
                catch(Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<CheckQueueResponse>.Invalid(ex.Message);

                }

            }
        }
    }
}
