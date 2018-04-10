using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPatientPopulationCommand : IRequest<Result<AddPatientPopulationResponse>>
    {
        public int PersonId { get; set; }
        public int KeyPopulation { get; set; }
        public int UserId { get; set; }
    }

    public class AddPatientPopulationResponse
    {
        public int PatientPopulationId { get; set; }
    }
}