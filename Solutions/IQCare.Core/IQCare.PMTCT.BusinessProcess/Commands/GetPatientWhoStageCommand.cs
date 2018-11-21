using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPatientWhoStageCommand: IRequest<Result<PatientWhoStage>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }
}