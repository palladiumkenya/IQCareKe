using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPatientByIdCommand : IRequest<Result<Patient>>
    {
        public int Id { get; set; }
    }
}