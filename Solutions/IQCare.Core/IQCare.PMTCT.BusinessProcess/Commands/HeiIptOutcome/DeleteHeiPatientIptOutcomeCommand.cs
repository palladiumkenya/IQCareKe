using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptOutcome
{
    public class DeleteHeiPatientIptOutcomeCommand: IRequest<Result<PatientIptOutcome>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
    }
}