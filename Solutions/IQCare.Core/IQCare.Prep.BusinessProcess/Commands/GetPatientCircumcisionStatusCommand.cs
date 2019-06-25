using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class GetPatientCircumcisionStatusCommand : IRequest<Result<PatientCircumcisionStatus>>
    {
        public int PatientId { get; set; }
    }
}