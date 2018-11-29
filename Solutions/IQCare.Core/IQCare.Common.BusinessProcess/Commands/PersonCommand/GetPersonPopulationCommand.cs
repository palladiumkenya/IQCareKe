using IQCare.Library;
using MediatR;
using System.Collections.Generic;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonPopulationCommand : IRequest<Result<List<Core.Models.PersonPopulation>>>
    {
        public int PersonId { get; set; }
    }
}