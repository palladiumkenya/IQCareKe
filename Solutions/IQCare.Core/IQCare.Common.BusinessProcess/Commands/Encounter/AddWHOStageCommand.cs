using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class AddWHOStageCommand : IRequest<Result<AddWHOStageCommandResponse>>
    {
        public PatientWHOStage PatientWHOStage;
    }

    public class AddWHOStageCommandResponse
        {
        public int WhoStageId { get; set; }
    }

}
