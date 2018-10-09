using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory
{
    public class EditImmunizationHistoryCommand: IRequest<Result<EditImmunizationResponse>>
    {
        public Vaccination Vaccination;
    }

    public class EditImmunizationResponse
    {
       public string Message { get; set; }
    }
}
