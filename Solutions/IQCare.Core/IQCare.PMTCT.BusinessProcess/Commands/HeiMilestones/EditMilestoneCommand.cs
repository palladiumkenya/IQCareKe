using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class EditMilestoneCommand: IRequest<Result<PatientMilestone>>
   {
       public PatientMilestone PatientMilestone;
   }
}
