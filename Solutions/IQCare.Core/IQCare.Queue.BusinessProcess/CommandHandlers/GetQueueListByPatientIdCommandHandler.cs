﻿using System;
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
    public class GetQueueListByPatientIdCommandHandler : IRequestHandler<GetQueueListByPatientIdCommand, Result<GetQueueListByPatientidResponse>>
    {
        private readonly IQueueUnitOfWork _queueUnitOfWork;

        private readonly ILogger _logger = Log.ForContext<GetQueueListByPatientIdCommandHandler>();

        public GetQueueListByPatientIdCommandHandler(IQueueUnitOfWork queueUnitOfWork)
        {
            _queueUnitOfWork = queueUnitOfWork ?? throw new ArgumentNullException(nameof(queueUnitOfWork));
        }

        public async Task<Result<GetQueueListByPatientidResponse>> Handle(GetQueueListByPatientIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sql = "exec pr_OpenDecryptedSession;" +
                              $"SELECT * FROM WaitingListView WHERE PatientId = {request.PatientId} order by PriorityRank asc,CreateDate desc; " +
                              $"exec [dbo].[pr_CloseDecryptedSession];";

                var result = await _queueUnitOfWork.Repository<WaitingListView>().FromSql(sql);
                var finalresult = result.ToList().Where(x => x.DeleteFlag == false && x.Status==false).ToList();

                return Result<GetQueueListByPatientidResponse>.Valid(new GetQueueListByPatientidResponse()
                {
                    waitingListViews = finalresult

                });
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return Result<GetQueueListByPatientidResponse>.Invalid(ex.Message);
            }
   

        }
    }
}
