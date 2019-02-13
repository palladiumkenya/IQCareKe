using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiDeliveryMaternalHistory
{
    public class UpdateHeiDeliveryAt24MonthsCommandHandler : IRequestHandler<UpdateHeiDeliveryAt24MonthsCommand, Result<HEIEncounter>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public UpdateHeiDeliveryAt24MonthsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<HEIEncounter>> Handle(UpdateHeiDeliveryAt24MonthsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var encounter = await _unitOfWork.Repository<HEIEncounter>()
                        .Get(x => x.Id == request.HeiEncounterId).FirstOrDefaultAsync();

                    if (encounter != null)
                    {
                        encounter.Outcome24MonthId = request.OutcomeAt24MonthsId;

                        _unitOfWork.Repository<HEIEncounter>().Update(encounter);
                        await _unitOfWork.SaveAsync();

                        return Result<HEIEncounter>.Valid(encounter);
                    }
                    else
                    {
                        return Result<HEIEncounter>.Invalid($"Could not find hei encouter for Id: {request.HeiEncounterId}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<HEIEncounter>.Invalid(e.Message);
                }
            }
        }
    }
}