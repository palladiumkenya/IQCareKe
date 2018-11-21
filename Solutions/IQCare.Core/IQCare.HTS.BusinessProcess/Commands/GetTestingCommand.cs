using System.Collections.Generic;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetTestingCommand : IRequest<Result<GetTestingResultsResponse>>
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientEncounterId { get; set; }
    }

    public class GetTestingResultsResponse
    {
        public List<HtsEncounter> Encounter { get; set; }
        public List<Core.Model.Testing> Testing { get; set; }
        public List<HtsEncounterResult> EncounterResults { get; set; }
    }
}