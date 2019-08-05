using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
   public  class RiskAssessmentVisitQueryHandler:IRequestHandler<RiskAssessmentVisitQuery,Result<List<PrepRiskAssessmentEncounterView>>> 
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;
        public RiskAssessmentVisitQueryHandler(IPrepUnitOfWork prepUnitOfWork,IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
            _mapper = mapper;
        }

        public async Task<Result<List<PrepRiskAssessmentEncounterView>>> Handle(RiskAssessmentVisitQuery request,CancellationToken cancellationToken)
        {
            try
            {
                var RiskAssessmentEncounter = await _prepUnitOfWork.Repository<PrepRiskAssessmentEncounterView>().Get(x => x.PersonId == request.PersonId).OrderByDescending(x => x.VisitDate).Take(10).ToListAsync();


                return await Task.FromResult(Result<List<PrepRiskAssessmentEncounterView>>.Valid(RiskAssessmentEncounter));

            }
            catch(Exception ex)
            {
                return await Task.FromResult(Result<List<PrepRiskAssessmentEncounterView>>.Invalid(ex.Message.ToString()));
            }
        }

    }
}
