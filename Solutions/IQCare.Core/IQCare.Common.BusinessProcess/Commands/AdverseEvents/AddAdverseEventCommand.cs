using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.AdverseEvents
{
    public class AddAdverseEventCommand : IRequest<Result<AdverseEventsResponse>>
    {
        public List<PatientAdverseEvent> AdverseEvents { get; set; }  
    }

    public class AdverseEventsResponse
    {
        public string Message { get; set; }
    }
}