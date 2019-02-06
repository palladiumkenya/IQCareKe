using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{
    public class DeleteHeiFeedingCommand:IRequest<Result<DeleteHeiFeedingCommandResult>>
    {
        public int Id { get; set; }
    }

    public class DeleteHeiFeedingCommandResult
    {
        public int Id { get; set; }
    }
}
