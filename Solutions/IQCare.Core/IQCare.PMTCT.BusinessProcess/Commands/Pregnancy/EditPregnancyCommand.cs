
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
   public class EditPregnancyCommand:IRequest<Result<EditPregnancyCommandResult>>
    {
        public PatientPregnancy patientPregnancy;
    }

    public class EditPregnancyCommandResult
    {
         public int PregnancyId {get;set;}
    }
}
