using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AfyaMobileSynchronizeEncounterCommand : IRequest<Result<string>>
    {
        [Required, ValidateObject]
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        [Required, ValidateObject]
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        public PLACER_DETAIL PLACER_DETAIL { get; set; }
        public PRE_TEST PRE_TEST { get; set; }
    }
}