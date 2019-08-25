using IQCare.Common.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Enrollment
{
    public class GetServiceEntryPointCommand : IRequest<Result<ServiceEntryPoint>>
    {
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
    }
}