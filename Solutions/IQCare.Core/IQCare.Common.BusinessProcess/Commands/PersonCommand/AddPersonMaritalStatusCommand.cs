using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class AddPersonMaritalStatusCommand : IRequest<Result<AddPersonMaritalStatusResponse>>
    {
        public int PersonId { get; set; }
        public int MaritalStatusId { get; set; }
        public int UserId { get; set; }
    }

    public class AddPersonMaritalStatusResponse
    {
        public int PersonMaritalStatusId { get; set; }
    }
}