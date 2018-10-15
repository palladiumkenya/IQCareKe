using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup
{
    public class AddHeiPatientIptWorkupCommand:IRequest<Result<PatientIptWorkup>>
    {
        public PatientIptWorkup PatientIptWorkup { get; set; }
    }
}