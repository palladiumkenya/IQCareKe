using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class AddMilestoneCommand : IRequest<Result<AddMilestoneResponse>>
    {
        public List<HEIMilestone> PatientMilestone;
    }

    public class AddMilestoneResponse
    {
        public string Message { get; set; }
    }
}
