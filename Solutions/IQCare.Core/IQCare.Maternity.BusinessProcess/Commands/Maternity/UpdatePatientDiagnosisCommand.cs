using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class UpdatePatientDiagnosisCommand : IRequest<Result<UpdatePatientDiagnosisResponse>>
    {
        public int DiagnosisId { get; set; }
        public AddPatientDiagnosisCommand DiagnosisCommand { get; set; }

    }

    public class UpdatePatientDiagnosisResponse
    {
        public string MessageResult { get; set; }
    }
}