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
    public class GetPrepEncountersByVisitDateCommandHandler : IRequestHandler<GetPrepEncountersByVisitDateCommand, Result<List<PrepEncountersView>>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        public List<PrepEncountersView> list = new List<PrepEncountersView>();
        public GetPrepEncountersByVisitDateCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork;
        }

        public async Task<Result<List<PrepEncountersView>>> Handle(GetPrepEncountersByVisitDateCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try

                {
                    if (request.visitDate != null )
                    {
                        list = await _prepUnitOfWork.Repository<PrepEncountersView>()
                        .Get(x => x.PatientId == request.PatientId && x.ServiceAreaId == request.ServiceAreaId &&
                       x.VisitDate.Value.Day ==request.visitDate.Value.Day && x.VisitDate.Value.Year== request.visitDate.Value.Year && x.VisitDate.Value.Month == request.visitDate.Value.Month).Take(300).ToListAsync();

                    }
                   

                    return Result<List<PrepEncountersView>>.Valid(list);
                }
                catch (System.Exception e)
                {
                    Log.Error($"Could not fetch PrEP Encounters for PatientId: {request.PatientId}");
                    return Result<List<PrepEncountersView>>.Invalid($"Could not fetch PrEP Encounters for PatientId: {request.PatientId}");
                }
            }
        }
    }
}