using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class GetPrepEncountersCommandHandler : IRequestHandler<GetPrepEncountersCommand, Result<List<PrepEncountersView>>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        public List<PrepEncountersView> list = new List<PrepEncountersView>();
        public GetPrepEncountersCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork;
        }

        public async Task<Result<List<PrepEncountersView>>> Handle(GetPrepEncountersCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try

                {
                    if (request.fromDate != null && request.toDate != null)
                    {
                        list = await _prepUnitOfWork.Repository<PrepEncountersView>()
                        .Get(x => x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId &&
                        x.EncounterStartTime.Value <= request.toDate && x.EncounterStartTime.Value >= request.fromDate).Take(300).ToListAsync();

                    }
                    else
                    {
                        list = await _prepUnitOfWork.Repository<PrepEncountersView>()
                            .Get(x => x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId).ToListAsync();
                    }

                    return Result<List<PrepEncountersView>>.Valid(list);
                }
                catch (Exception e)
                {
                    Log.Error($"Could not fetch PrEP Encounters for PatientId: {request.PatientId}");
                    return Result<List<PrepEncountersView>>.Invalid($"Could not fetch PrEP Encounters for PatientId: {request.PatientId}");
                }
            }
        }
    }
}