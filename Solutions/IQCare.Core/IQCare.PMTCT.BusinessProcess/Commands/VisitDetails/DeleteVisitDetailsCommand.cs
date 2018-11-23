using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class DeleteVisitDetailsCommand : IRequest<Result<Core.Models.VisitDetails>>
    {
        public Core.Models.VisitDetails VisitDetails { get; set; }
    }
}