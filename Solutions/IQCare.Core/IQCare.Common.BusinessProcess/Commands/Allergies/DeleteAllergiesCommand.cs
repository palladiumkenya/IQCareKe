using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Allergies
{
    public class DeleteAllergiesCommand : IRequest<Result<DeleteAllergiesResponse>>
    {
        public int Id { get; set; }
    }

    public class DeleteAllergiesResponse
    {
        public int ResultOutcome { get; set; }
        public string Message { get; set; }
    }
}