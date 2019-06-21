using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobilePartnerTracingEncounterCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PARTNER_TRACING_ENCOUNTER PARTNERTRACING { get; set; }

        public AfyaMobilePartnerTracingEncounterCommand(AfyaMobilePartnerTracingEncounterCommandDto dto)
        {
            MESSAGE_HEADER = dto.MESSAGE_HEADER;
            INTERNAL_PATIENT_ID = dto.INTERNAL_PATIENT_ID;
            PARTNERTRACING = new PARTNER_TRACING_ENCOUNTER()
            {
                PLACER_DETAIL = dto.PLACER_DETAIL,
                TRACING = dto.TRACING
            };
        }
    }

    public class AfyaMobilePartnerTracingEncounterCommandDto
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public List<PARTNER_TRACING> TRACING { get; set; }
    }
}
