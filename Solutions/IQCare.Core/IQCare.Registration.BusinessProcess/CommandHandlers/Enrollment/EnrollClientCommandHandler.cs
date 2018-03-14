using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Registration.BusinessProcess.Commands.Enrollment;
using MediatR;

namespace IQCare.Registration.BusinessProcess.Handlers.Enrollment
{
    public class EnrollClientCommandHandler : IRequestHandler<EnrollClientCommand, Result<EnrollClientResponse>>
    {
        public async Task<Result<EnrollClientResponse>> Handle(EnrollClientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}