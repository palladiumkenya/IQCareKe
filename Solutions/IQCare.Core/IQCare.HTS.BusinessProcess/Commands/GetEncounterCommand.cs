using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetEncounterCommand : IRequest<Result<GetEncounterResponse>>
    {
        public int EncounterId { get; set; }
    }

    public class GetEncounterResponse
    {
        public List<HtsEncounter> encounter { get; set; }
        public List<ClientDisability> disabilities { get; set; }
        public List<PatientEncounter> patientEncounter { get; set; }
        public List<PatientConsent> consent { get; set; }
        public List<PatientScreening> tbStatus { get; set; }
        public List<HtsEncounterResult> htsResults { get; set; }
        public List<PatientConsent> consentToListPartners { get; set; }
    }
}