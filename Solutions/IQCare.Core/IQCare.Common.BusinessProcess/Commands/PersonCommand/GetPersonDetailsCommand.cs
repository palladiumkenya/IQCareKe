using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonDetailsCommand : IRequest<Result<PatientLookupView>>
    {
        public int PersonId { get; set; }
    }
}