using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptOutcome
{
    public class GetHeiPatientIptOutcomeCommand: IRequest<Result<List<PatientIptOutcome>>>
    {
        public int PatientId { get; set; }
    }
}