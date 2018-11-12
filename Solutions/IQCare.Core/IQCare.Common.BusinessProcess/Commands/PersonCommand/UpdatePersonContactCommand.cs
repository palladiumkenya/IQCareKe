using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.PersonCommand
{
    public class UpdatePersonContactCommand : IRequest<Result<PersonContact>>
    {
        public int PersonId { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeNumber { get; set; }
        public string EmailAddress { get; set; }
        public int UserId { get; set; }
    }
}