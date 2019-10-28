using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;


namespace IQCare.Common.BusinessProcess.Commands.AdverseEvents
{
    public class DeleteAdverseEventsCommand : IRequest<Result<DeleteAdverseEventResponse>>
    {
        public int Id { get; set; }
    }

    public class DeleteAdverseEventResponse
    {
        public int ResultOutcome { get; set; }
        public string Message { get; set; }
    }
}
