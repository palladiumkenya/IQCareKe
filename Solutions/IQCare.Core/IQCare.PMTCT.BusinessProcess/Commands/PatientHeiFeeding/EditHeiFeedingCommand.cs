
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{
   public class EditHeiFeedingCommand:IRequest<Result<EditHeiFeedingCommandResult>>
    {
        public HeiFeeding heiFeeding;
    }

    public class EditHeiFeedingCommandResult
    {
         public int Id {get;set;}
    }
}
