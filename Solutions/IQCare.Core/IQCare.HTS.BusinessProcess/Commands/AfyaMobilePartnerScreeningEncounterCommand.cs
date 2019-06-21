using System.Collections.Generic;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobilePartnerScreeningEncounterCommand : IRequest<Result<string>>
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PARTNER_SCREENING_ENCOUNTER SCREENING { get; set; }
        public AfyaMobilePartnerScreeningEncounterCommand(AfyaMobilePartnerScreeningEncounterCommandDto dto)
        {
            MESSAGE_HEADER = dto.MESSAGE_HEADER;
            INTERNAL_PATIENT_ID = dto.INTERNAL_PATIENT_ID;
            SCREENING = new PARTNER_SCREENING_ENCOUNTER
            {
                PLACER_DETAIL = dto.PLACER_DETAIL,
                PARTNER_SCREENING = dto.PARTNER_SCREENING
            };
        }
    }

    public class AfyaMobilePartnerScreeningEncounterCommandDto
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PARTNER_SCREENING PARTNER_SCREENING { get; set; }
    }
}
