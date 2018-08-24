using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.examination
{
    public class UpdatePatientPhysicalExaminationCommand: IRequest<Result<UpdatePhysicalExaminationResponse>>
    {
        public PatientPhysicalExamination PatientPhysicalExamination;
    }

    public class UpdatePhysicalExaminationResponse
    {
        public int Id { get; set; }
    }
}
