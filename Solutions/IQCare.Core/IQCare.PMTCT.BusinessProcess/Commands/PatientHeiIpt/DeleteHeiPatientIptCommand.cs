using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiIpt
{
    public class DeleteHeiPatientIptCommand: IRequest<Result<PatientIpt>>
    {
        public int PatientId { get; set; }
    }
}