using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.AdverseEvents;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.AdverseEvents
{
    public class GetAdverseEventsCommandHandler : IRequestHandler<GetAdverseEventsCommand, Result<List<PatientAdverseEvent>>>
    {
        ICommonUnitOfWork _commontUnitOfWork;

        public GetAdverseEventsCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<List<PatientAdverseEvent>>> Handle(GetAdverseEventsCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var adverseEvents = await _commontUnitOfWork.Repository<PatientAdverseEvent>()
                        .Get(x => x.PatientId == request.PatientId && x.DeleteFlag == false).ToListAsync();

                    return Result<List<PatientAdverseEvent>>.Valid(adverseEvents);
                }
                catch (Exception e)
                {
                    Log.Error($"An error occured while fetching adverseEvents for PatientId: {request.PatientId}. Exception: {e.Message} {e.InnerException}");
                    return Result<List<PatientAdverseEvent>>.Invalid($"An error occured while fetching adverseEvents for PatientId: {request.PatientId}.");
                }
            }
        }
    }
}