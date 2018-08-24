using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.examination
{
    public class DeletePhysicalExaminationCommand:IRequest<Result<DeletePhysicalExamniationResponse>>
    {
        public int PatientPhysicalExaminationId { get; set; }
    }

    public class DeletePhysicalExamniationResponse
    {
        public int Id { get; set; }
    }
}
