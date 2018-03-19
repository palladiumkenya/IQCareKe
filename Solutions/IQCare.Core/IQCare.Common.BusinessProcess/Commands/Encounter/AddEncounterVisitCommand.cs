using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Encounter
{
    public class AddEncounterVisitCommand : IRequest<Result<AddEncounterVisitResponse>>
    {
        public int PatientId { get; set; }
        public int EncounterType { get; set; }
        public int ServiceAreaId { get; set; }
        public int UserId { get; set; }
    }

    public class AddEncounterVisitResponse
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientEncounterId { get; set; }
    }
}