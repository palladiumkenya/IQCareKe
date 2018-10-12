using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIpt
{
    public class DeleteHeiPatientIptCommand: IRequest<Result<HeiPatientIpt>>
    {
        public int PatientId { get; set; }
    }
}