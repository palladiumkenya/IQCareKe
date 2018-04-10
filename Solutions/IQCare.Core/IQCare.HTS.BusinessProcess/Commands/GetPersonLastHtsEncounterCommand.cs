using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetPersonLastHtsEncounterCommand : IRequest<Result<GetPersonLastHtsEncounterResponse>>
    {
        public int PersonId { get; set; }
    }

    public class GetPersonLastHtsEncounterResponse
    {
        public int EncounterId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientEncounterID { get; set; }
    }
}