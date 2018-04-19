using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddPnsTracingCommand : IRequest<Result<AddPnsTracingResponse>>
    {
        public DateTime TracingDate { get; set; }
        public int TracingMode { get; set; }
        public int TracingOutcome { get; set; }
        public int Consent { get; set; }
        public DateTime DateBookedTesting { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public int TracingType { get; set; }
    }

    public class AddPnsTracingResponse
    {
        public int TracingId { get; set; }
    }
}