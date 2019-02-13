using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiIptWorkup
{
    public class DeleteHeiPatientIptWorkupCommand:IRequest<Result<PatientIptWorkup>>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
    }
}