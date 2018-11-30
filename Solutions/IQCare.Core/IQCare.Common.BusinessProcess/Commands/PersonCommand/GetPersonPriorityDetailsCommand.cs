using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonPriorityDetailsCommand : IRequest<Result<List<PersonPriority>>>
    {
        public int PersonId { get; set; }
    }
}