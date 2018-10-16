using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class CompleteLabOrderCommandHandler : IRequestHandler<CompleteLabOrderCommand, Result<CompleteLabOrderResponse>>
    {
        ILabUnitOfWork _labUnitOfwork;
        public CompleteLabOrderCommandHandler(ILabUnitOfWork labUnitOfwork)
        {
            _labUnitOfwork = labUnitOfwork;
        }

        public Task<Result<CompleteLabOrderResponse>> Handle(CompleteLabOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
