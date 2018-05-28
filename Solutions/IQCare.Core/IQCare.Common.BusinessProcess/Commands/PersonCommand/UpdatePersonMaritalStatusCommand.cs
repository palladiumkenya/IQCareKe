using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class UpdatePersonMaritalStatusCommand : IRequest<Result<PersonMaritalStatus>>
    {
        public int PersonId { get; set; }
        public int MaritalStatusId { get; set; }
        public int UserId { get; set; }
    }
}