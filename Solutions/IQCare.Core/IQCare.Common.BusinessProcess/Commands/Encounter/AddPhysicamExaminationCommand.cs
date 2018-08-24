using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.examination
{
    public class AddPhysicamExaminationCommand :IRequest<Result<AddPhysicalExamniationResponse>>
    {
        public PatientPhysicalExamination patientPhysicalExamination;
    }
    public class AddPhysicalExamniationResponse
    {
        public int Id { get; set; }
    }
}
