using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment
{
    public class GetPatientIcfCommand: IRequest<Result<List<HeiPatientIcf>>>
    {
        public int PatientId { get; set; }
    }
}