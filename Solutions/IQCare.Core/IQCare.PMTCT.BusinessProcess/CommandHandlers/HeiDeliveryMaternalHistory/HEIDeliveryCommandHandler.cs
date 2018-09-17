using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiDeliveryMaternalHistory
{
    public class HEIDeliveryCommandHandler: IRequestHandler<HEIDeliveryCommand, Result<AddHeiDeliveryCommandResponse>>
    {
        public HEIDeliveryCommandHandler()
        {
            
        }

        public Task<Result<AddHeiDeliveryCommandResponse>> Handle(HEIDeliveryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}