using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.AdverseEvents
{
    public class GetAdverseEventsCommand : IRequest<Result<List<PatientAdverseEvent>>>
    {
        public int PatientId { get; set; }
    }
}