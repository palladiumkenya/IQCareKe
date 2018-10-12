using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment
{
    public class GetHeiPatientIcfActionCommand: IRequest<Result<List<HEiPatientIcfAction>>>
    {
        public int PatientId { get; set; }
    }
}