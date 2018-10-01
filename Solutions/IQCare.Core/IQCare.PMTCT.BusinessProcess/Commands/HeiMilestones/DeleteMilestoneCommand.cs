using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class DeleteMilestoneCommand: IRequest<Result<PatientMilestone>>
    {
        public int PatientId { get; set; }
    }
}
