using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptOutcome
{
    public class EditHeiPatientIptOutcomeCommand: IRequest<Result<PatientIptOutcome>>
    {
        public PatientIptOutcome PatientIptOutcome { get; set; }
    }
}