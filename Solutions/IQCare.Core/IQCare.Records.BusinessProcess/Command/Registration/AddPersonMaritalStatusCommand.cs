using IQCare.Library;
using MediatR;
using PersonMaritalStatus = IQCare.Common.Core.Models.PersonMaritalStatus;

namespace IQCare.Records.BusinessProcess.Command.Registration
{
    public class AddPersonMaritalStatusCommand : IRequest<Result<PersonMaritalStatus>>
    {
        public int PersonId { get; set; }
        public int MaritalStatusId { get; set; }
        public int CreatedBy { get; set; }
    }
}