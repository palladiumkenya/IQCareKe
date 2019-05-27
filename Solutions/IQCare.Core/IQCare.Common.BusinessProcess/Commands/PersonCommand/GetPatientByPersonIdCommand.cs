using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPatientByPersonIdCommand : IRequest<Result<Patient>>
    {
        public int PersonId { get; set; }
    }
}