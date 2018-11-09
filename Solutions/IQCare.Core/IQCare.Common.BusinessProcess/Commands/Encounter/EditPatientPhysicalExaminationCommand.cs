using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class EditPatientPhysicalExaminationCommand : IRequest<Result<EditPatientPhysicalExamResponse>>
    {
        public PatientPhysicalExamination PatientPhysicalExamination;
    }

    public class EditPatientPhysicalExamResponse
    {
        public int PatientPhysicalExamId { get; set; }
    }
}
