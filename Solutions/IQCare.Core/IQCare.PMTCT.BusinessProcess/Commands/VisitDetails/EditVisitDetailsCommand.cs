using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class EditVisitDetailsCommand: IRequest<Result<Core.Models.VisitDetails>>
    {
        public Core.Models.VisitDetails VisitDetails { get; set; }
    }
}