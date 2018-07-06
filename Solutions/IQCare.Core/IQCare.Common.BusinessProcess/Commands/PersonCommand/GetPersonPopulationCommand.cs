using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonPopulationCommand : IRequest<Result<List<Core.Models.PersonPopulation>>>
    {
        public int PersonId { get; set; }
    }
}