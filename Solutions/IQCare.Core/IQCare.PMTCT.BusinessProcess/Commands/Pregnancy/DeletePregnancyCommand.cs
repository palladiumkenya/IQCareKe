using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
    public class DeletePregnancyCommand:IRequest<Result<DeletePregnancyCommandResult>>
    {
        public int PregancyId { get; set; }
        public int PatientId { get; set; }
    }

    public class DeletePregnancyCommandResult
    {
        public int PregnancyId { get; set; }
    }
}
