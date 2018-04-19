using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPersonPopulationCommand : IRequest<Result<AddPersonPopulationResponse>>
    {
        public int PersonId { get; set; }
        public List<Population> Population { get; set; }
        public List<Priority> Priority { get; set; }
        public int UserId { get; set; }
    }

    public class Priority
    {
        public int PriorityId { get; set; }
    }

    public class Population
    {
        public string PopulationType { get; set; }
        public int PopulationCategory { get; set; }
    }

    public class AddPersonPopulationResponse
    {
        public bool isSuccessful { get; set; }
    }
}