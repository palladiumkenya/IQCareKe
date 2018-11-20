using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetTestingCommand : IRequest<Result<GetTestingResultsResponse>>
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientEncounterId { get; set; }
    }

    public class GetTestingResultsResponse
    {
    }
}