using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonDetailsCommand : IRequest<Result<PatientLookupView>>
    {
        public int PersonId { get; set; }
    }
}