using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonQueryCommand : IRequest<Result<PersonView>>
    {
        public int PersonId { get; set; }
    }
}