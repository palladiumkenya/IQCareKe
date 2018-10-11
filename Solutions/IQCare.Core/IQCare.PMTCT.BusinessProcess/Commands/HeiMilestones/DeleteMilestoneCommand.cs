using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class DeleteMilestoneCommand: IRequest<Result<DeleteMilestoneResponse>>
    {
        public int PatientId { get; set; }
    }

    public class DeleteMilestoneResponse
    {
        public string Message { get; set; }
    }
}
