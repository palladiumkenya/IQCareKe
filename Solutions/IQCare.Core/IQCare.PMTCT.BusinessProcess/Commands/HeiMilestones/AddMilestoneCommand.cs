using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class AddMilestoneCommand : IRequest<Result<PatientMilestone>>
    {
        public PatientMilestone PatientMilestone;
    }
}
