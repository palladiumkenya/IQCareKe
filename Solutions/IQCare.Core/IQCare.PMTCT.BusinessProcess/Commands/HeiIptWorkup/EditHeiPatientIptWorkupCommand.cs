using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup
{
    public class EditHeiPatientIptWorkupCommand: IRequest<Result<PatientIptWorkup>>
    {
        public     PatientIptWorkup PatientIptWorkup { get; set; }
    }
}