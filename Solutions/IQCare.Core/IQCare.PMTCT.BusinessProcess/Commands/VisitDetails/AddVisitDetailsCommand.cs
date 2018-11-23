using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class AddVisitDetailsCommand: IRequest<Result<Core.Models.VisitDetails>>
    {
        public Core.Models.VisitDetails VisitDetails { get; set; }
    }
}