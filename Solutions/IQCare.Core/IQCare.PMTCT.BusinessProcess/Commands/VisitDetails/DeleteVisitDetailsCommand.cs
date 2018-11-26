using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.VisitDetails
{
    public class DeleteVisitDetailsCommand : IRequest<Result<DeleteVisitDetailsCommandResponse>>
    {
        public int Id { get; set; }
    }

    public class DeleteVisitDetailsCommandResponse
    {
        public int Id { get; set; }
    }
}