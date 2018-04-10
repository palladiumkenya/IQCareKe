using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Partners
{
    public class GetPartnerCommand : IRequest<Result<PartnersView>>
    {
        public int PersonId { get; set; }
    }
}