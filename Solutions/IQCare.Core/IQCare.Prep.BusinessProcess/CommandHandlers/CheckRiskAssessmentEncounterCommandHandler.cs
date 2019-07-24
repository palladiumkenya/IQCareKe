using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using IQCare.Prep.Infrastructure;
using IQCare.Prep.Infrastructure.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class CheckRiskAssessmentEncounterCommandHandler:IRequestHandler<CheckRiskAssessmentEncounterCommand,Result<RiskAssessmentExistResponse>>
    {

        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly ILogger logger = Log.ForContext<CheckRiskAssessmentEncounterCommandHandler>();
        public string message;
        public int pmvId;
        public CheckRiskAssessmentEncounterCommandHandler(IPrepUnitOfWork prepUnitOfWork)
        {
            _prepUnitOfWork = prepUnitOfWork ?? throw new ArgumentNullException(nameof(prepUnitOfWork));
        }

        public async Task<Result<RiskAssessmentExistResponse>> Handle (CheckRiskAssessmentEncounterCommand request,CancellationToken cancellationToken)
         {
            try
            {
                var EncounterExists = await _prepUnitOfWork.Repository<PrepRiskAssessmentEncounterView>().Get(x => x.PersonId == request.PersonId && x.VisitDate.Value.Day == DateTime.Now.Day && x.VisitDate.Value.Month == DateTime.Now.Month && x.VisitDate.Value.Year == DateTime.Now.Year).ToListAsync();
                if(EncounterExists !=null)
                {
                    message += "The risk assessment  form exists";
                }
                else
                {
                    message += "risk assessment form does not exist";
                }


                return Result<RiskAssessmentExistResponse>.Valid(new RiskAssessmentExistResponse()
                {
                   Message = message,
                   Encounters = EncounterExists
                });
            }
            catch (Exception ex)
            {
                string message = $"An error  has Occured" + ex.Message;


                return await Task.FromResult(Result<RiskAssessmentExistResponse>.Invalid(message));
            }
        }

    }
}
