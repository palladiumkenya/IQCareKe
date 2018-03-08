using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class ReferPatientCommandHandler : IRequestHandler<ReferPatientCommand, Result<ReferPatientResponse>>
    {
        private readonly IHTSUnitOfWork _hTSUnitOfWork;
        public ReferPatientCommandHandler(IHTSUnitOfWork hTSUnitOfWork)
        {
            _hTSUnitOfWork = hTSUnitOfWork ?? throw new ArgumentNullException(nameof(hTSUnitOfWork));
        }
        public async Task<Result<ReferPatientResponse>> Handle(ReferPatientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
