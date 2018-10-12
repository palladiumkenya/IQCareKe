using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIpt
{
    public class GetHeiPatientIptCommand: IRequest<Result<List<HeiPatientIpt>>>
    {
        public int PatientId { get; set; }
    }
}