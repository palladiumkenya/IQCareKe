using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class GetPatientEnrollmentByServiceAreaIdCommand : IRequest<Result<PatientEnrollment>>
    {
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
    }
}