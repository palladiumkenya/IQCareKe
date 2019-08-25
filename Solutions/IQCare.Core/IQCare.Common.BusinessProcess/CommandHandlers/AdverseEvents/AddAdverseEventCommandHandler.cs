using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.AdverseEvents;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.AdverseEvents
{
    public class AddAdverseEventCommandHandler : IRequestHandler<AddAdverseEventCommand, Result<AdverseEventsResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;

        public AddAdverseEventCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<AdverseEventsResponse>> Handle(AddAdverseEventCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    if (request.AdverseEvents.Any())
                    {
                        List<PatientAdverseEvent> patientAdverseEvents = new List<PatientAdverseEvent>();
                        request.AdverseEvents.ForEach(x => patientAdverseEvents.Add(new PatientAdverseEvent
                        {
                            PatientId = x.PatientId,
                            PatientMasterVisitId = x.PatientMasterVisitId,
                            Action = x.Action,
                            AdverseEventId = x.AdverseEventId,
                            CreateBy = x.CreateBy,
                            CreateDate = x.CreateDate,
                            DeleteFlag = x.DeleteFlag,
                            EventCause = x.EventCause,
                            EventName = x.EventName,
                            Severity = x.Severity
                        }));

                        await _commontUnitOfWork.Repository<PatientAdverseEvent>().AddRangeAsync(patientAdverseEvents);
                        await _commontUnitOfWork.SaveAsync();
                    }

                    return Result<AdverseEventsResponse>.Valid(new AdverseEventsResponse()
                    {
                        Message = "Successfully added adverse events"
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to add adverse events");
                    return Result<AdverseEventsResponse>.Invalid($"Failed to add adverse events");
                }
            }
        }
    }
}