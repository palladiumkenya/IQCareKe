using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiMilestones
{
    public class EditMilestoneCommand: IRequest<Result<EditMilestoneResponse>>
   {
       public HEIMilestone  PatientMilestone;
   }

    public class EditMilestoneResponse
    {
        public string Message { get; set; }
    }
}
