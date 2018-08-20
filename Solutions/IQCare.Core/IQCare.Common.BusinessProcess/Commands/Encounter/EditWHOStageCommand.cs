using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class EditWHOStageCommand : IRequest<Result<EditWHOStageCommandResponse>>
    {
        public PatientWHOStage PatientWHOStage;
    }

    public class EditWHOStageCommandResponse
    {
        public int WHOStageId { get; set; }
    }
}
