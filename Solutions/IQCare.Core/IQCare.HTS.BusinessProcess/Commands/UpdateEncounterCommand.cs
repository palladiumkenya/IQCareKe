using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class UpdateEncounterCommand : IRequest<Result<UpdateEncounterResponse>>
    {
        public Encounter Encounter { get; set; }
        public int encounterId { get; set; }
    }

    public class UpdateEncounterResponse
    {
        public bool IsSuccessFul { get; set; }
    }
}
