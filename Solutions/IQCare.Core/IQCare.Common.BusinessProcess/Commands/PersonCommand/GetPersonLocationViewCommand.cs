using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class GetPersonLocationViewCommand : IRequest<Result<PersonLocationView>>
    {
        public int personId { get; set; }
    }
}
