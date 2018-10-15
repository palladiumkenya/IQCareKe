using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIpt
{
    public class AddHeiPatientIptCommand: IRequest<Result<PatientIpt>>
    {
        public PatientIpt PatientIpt  { get; set; }
    }
}