using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Relationship
{

    public class GetPartnerCommand : IRequest<Result<PartnersView>>
    {
        public int PersonId { get; set; }
    }
}