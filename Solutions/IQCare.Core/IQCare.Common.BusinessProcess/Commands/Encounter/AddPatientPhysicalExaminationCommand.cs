using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddPatientPhysicalExaminationCommand: IRequest<Result<AddPatientPhysicalExamResponse>>
    {
        public PatientPhysicalExamination PatientPhysicalExamination;

    }
    public class AddPatientPhysicalExamResponse
    {
        public int PatientPhysicalExamIdd { get; set; }
    }
}
