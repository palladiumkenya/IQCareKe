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

        public AfyaMobileFamilyScreeningEncounterCommand(AfyaMobileFamilyScreeningEncounterCommandDto dto)
        {
            MESSAGE_HEADER = dto.MESSAGE_HEADER;
            INTERNAL_PATIENT_ID = dto.INTERNAL_PATIENT_ID;
            SCREENING_ENCOUNTER = new FAMILY_SCREENING_ENCOUNTER
            {
                PLACER_DETAIL = dto.PLACER_DETAIL, FAMILY_SCREENING = dto.FAMILY_SCREENING
            };
        }

    }

    public class AfyaMobileFamilyScreeningEncounterCommandDto
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public FAMILY_SCREENING FAMILY_SCREENING { get; set; }
    }
}
