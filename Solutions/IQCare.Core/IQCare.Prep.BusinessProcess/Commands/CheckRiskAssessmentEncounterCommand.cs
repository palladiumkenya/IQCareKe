using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class CheckRiskAssessmentEncounterCommand:IRequest<Result<RiskAssessmentExistResponse>>
    {
        public int PersonId { get; set; }
    }

    public class RiskAssessmentExistResponse
    {
        public string Message { get; set; }
        public List<PrepRiskAssessmentEncounterView> Encounters { get; set; }
    }


}
