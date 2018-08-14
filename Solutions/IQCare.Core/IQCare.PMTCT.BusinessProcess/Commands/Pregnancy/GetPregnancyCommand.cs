using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
    public class GetPregnancyCommand: IRequest<Result<GetPregnancyCommandResult>>
    {
        public int PatientId { get; set; }
    }

    public class GetPregnancyCommandResult {
        public PatientPregnancy patientPregnancy { get; set; }
    }
}
