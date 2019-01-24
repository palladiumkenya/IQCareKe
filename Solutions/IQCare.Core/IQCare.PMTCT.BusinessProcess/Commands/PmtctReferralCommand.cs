using System.Diagnostics.Contracts;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.Views;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class PmtctReferralCommand: IRequest<Result<PmtctReferralView>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}