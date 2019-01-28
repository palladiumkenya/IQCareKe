using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class ClientMonitoringEditCommandHandler : IRequestHandler<ClientMonitoringCommand, Result<ClientMonitoringCommandResponse>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private readonly IPmtctUnitOfWork _unitOfWork;
        private int results = 0;

        public ClientMonitoringEditCommandHandler(ICommonUnitOfWork commonUnitOfWork, IPmtctUnitOfWork unitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ClientMonitoringCommandResponse>> Handle(ClientMonitoringCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                ClientMonitoringServices clientMonitoringService = new ClientMonitoringServices(_unitOfWork);
            }
        }
    }
}