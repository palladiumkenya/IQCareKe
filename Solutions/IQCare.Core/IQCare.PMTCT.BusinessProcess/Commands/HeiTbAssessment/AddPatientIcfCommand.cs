using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiTbAssessment
{
    public class AddPatientIcfCommand:IRequest<Result<HeiPatientIcf>>
    {
        public HeiPatientIcf HeiPatientIcf { get; set; }
    }

}