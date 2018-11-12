using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddPatientPhysicalExaminationCommand: IRequest<Result<AddPatientPhysicalExamResponse>>
    {
        public PatientPhysicalExamination PatientPhysicalExamination;

    }
    public class AddPatientPhysicalExamResponse
    {
        public int PatientPhysicalExamId { get; set; }
    }
}
