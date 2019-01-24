using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class GetPatientPhysicalExaminationCommand: IRequest<Result<GetPatientPhysicamExamResponse>>
    {
        public int patientId { get; set; }
    }

    public class GetPatientPhysicamExamResponse
    {
        public PatientPhysicalExamination PatientPhysicamExamination; 
    }

}
