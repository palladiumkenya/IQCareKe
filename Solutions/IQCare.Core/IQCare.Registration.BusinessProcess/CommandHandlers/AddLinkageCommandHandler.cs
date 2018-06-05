using IQCare.Common.Core.Models;
using IQCare.Registration.BusinessProcess.Commands;
using IQCare.Registration.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Registration.BusinessProcess.Handlers
{
    using Response = Result<AddLinkageResponse>;

    public class AddLinkageCommandHandler : IRequestHandler<AddLinkageCommand, Response>
    {
        private readonly IRegistrationUnitOfWork _unitOfWork;
        public AddLinkageCommandHandler(IRegistrationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<Response> Handle(AddLinkageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
