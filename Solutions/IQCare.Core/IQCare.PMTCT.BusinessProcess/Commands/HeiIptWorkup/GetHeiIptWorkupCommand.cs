using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup
{
    public class GetHeiIptWorkupCommand: IRequest<Result<List<PatientIptWorkup>>>
    {
        public int PatientId { get; set; }
    }
}