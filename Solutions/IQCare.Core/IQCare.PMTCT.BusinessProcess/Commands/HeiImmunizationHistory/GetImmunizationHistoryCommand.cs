using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiImmunizationHistory
{
    public class GetImmunizationHistoryCommand: IRequest<Result<List<Vaccination>>>
    {
        public int PatientId { get; set; }
    }
}
