using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory
{
    public class EditImmunizationHistoryCommand: IRequest<Result<Vaccination>>
    {
        public Vaccination Vaccination;
    }
}
