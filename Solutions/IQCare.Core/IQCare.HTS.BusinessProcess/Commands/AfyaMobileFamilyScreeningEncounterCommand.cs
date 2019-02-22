using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobileFamilyScreeningEncounterCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public FAMILY_SCREENING_ENCOUNTER SCREENING_ENCOUNTER { get; set; }
    }
}