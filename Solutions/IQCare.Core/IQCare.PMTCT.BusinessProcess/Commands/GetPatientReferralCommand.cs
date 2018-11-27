using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPatientReferralCommand: IRequest<PatientReferral>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}