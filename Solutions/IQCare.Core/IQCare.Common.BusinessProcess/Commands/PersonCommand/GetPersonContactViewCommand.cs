using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonContactViewCommand:IRequest<Result<PersonContactView>>
    {
        public int personId { get; set; }
    }
}
