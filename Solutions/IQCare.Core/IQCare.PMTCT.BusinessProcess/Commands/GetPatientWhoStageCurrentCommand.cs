using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class GetPatientWhoStageCurrentCommand : IRequest<Result<PatientWhoStage>>
    {
        public int PatientId { get; set; }
    }
}