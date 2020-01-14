using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
   public  class GetHtsEncounterDetailsViewByVisitDateCommandHandler : IRequestHandler<GetHtsEncounterDetailsViewByVisitDateCommand, Result<List<EncountersDetailView>>>
    {

        private readonly IHTSUnitOfWork _unitOfWork;
        public GetHtsEncounterDetailsViewByVisitDateCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<EncountersDetailView>>> Handle(GetHtsEncounterDetailsViewByVisitDateCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {

                    var result = await _unitOfWork.Repository<EncountersDetailView>()
                    .Get(x => x.PersonId == request.personId && x.EncounterDate.Date.Day == request.VisitDate.Date.Day
                    && x.EncounterDate.Date.Year ==request.VisitDate.Date.Year 
                    && x.EncounterDate.Date.Month==x.EncounterDate.Date.Month ).ToListAsync();

                    return Result<List<EncountersDetailView>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Result<List<EncountersDetailView>>.Invalid(e.Message);
                }
            }
        }
    }
}

