using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiIpt
{
    public class EditHeiPatientIptCommand: IRequest<Result<PatientIpt>>
    {
        public PatientIpt PatientIpt { get; set; }
    }
}