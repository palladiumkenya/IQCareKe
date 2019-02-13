using System;
using System.Collections.Generic;
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
    public class GetHeiDeliveryCommandHandler : IRequestHandler<GetHeiDeliveryCommand, Result<List<HEIEncounter>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        public GetHeiDeliveryCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<List<HEIEncounter>>> Handle(GetHeiDeliveryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<HEIEncounter>().Get(x =>
                        x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId &&
                        x.DeleteFlag == false).ToListAsync();

                    return Result<List<HEIEncounter>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error($"Error fetching HeiEncounter for PatientId: {request.PatientId} and PatientMasterVisitId: {request.PatientMasterVisitId}");
                    return Result<List<HEIEncounter>>.Invalid(e.Message);
                }
            }
        }
    }
}