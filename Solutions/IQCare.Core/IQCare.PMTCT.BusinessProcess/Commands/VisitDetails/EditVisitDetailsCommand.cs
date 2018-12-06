using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class EditVisitDetailsCommand: IRequest<Result<Core.Models.VisitDetails>>
    {
        public EditVisitDetails VisitDetails { get; set; }
    }

    public class EditVisitDetails
    {
        public int Id { get; set; }
        public int? VisitNumber { get; set; }
        public int? VisitType { get; set; }
        public int? DaysPostPartum { get; set; }
    }
}