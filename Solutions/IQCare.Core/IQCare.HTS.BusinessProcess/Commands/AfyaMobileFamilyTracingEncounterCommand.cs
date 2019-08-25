using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobileFamilyTracingEncounterCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public FAMILY_TRACING_ENCOUNTER TRACING_ENCOUNTER { get; set; }

        public AfyaMobileFamilyTracingEncounterCommand(AfyaMobileFamilyTracingEncounterCommandDto dto)
        {
            MESSAGE_HEADER = dto.MESSAGE_HEADER;
            INTERNAL_PATIENT_ID = dto.INTERNAL_PATIENT_ID;
            TRACING_ENCOUNTER = new FAMILY_TRACING_ENCOUNTER()
            {
                PLACER_DETAIL = dto.PLACER_DETAIL,
                TRACING = dto.TRACING
            };
        }
    }

    public class AfyaMobileFamilyTracingEncounterCommandDto
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<FAMILY_TRACING> TRACING { get; set; }
    }
}
