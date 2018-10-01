using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class GetPatientMilestoneCommand: IRequest<Result<PatientMilestone>>
    {
        public int PatientId { get; set; }
    }
}
