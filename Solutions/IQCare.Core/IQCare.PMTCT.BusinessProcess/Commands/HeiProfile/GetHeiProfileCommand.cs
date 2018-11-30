
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiProfile
{
    public class GetHeiProfileCommand:IRequest<Result<PatientHeiProfile>>
    {
       public int PatientId { get; set; }
    }

}
