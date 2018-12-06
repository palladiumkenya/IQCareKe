using IQCare.Library;
using MediatR;

namespace IQCare.Maternity.BusinessProcess.Commands.Maternity
{
    public class UpdatePatientDiagnosisCommand : IRequest<Result<UpdatePatientDiagnosisResponse>>
    {
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string ManagementPlan { get; set; }
    }

    public class UpdatePatientDiagnosisResponse
    {
        public string MessageResult { get; set; }
    }
}