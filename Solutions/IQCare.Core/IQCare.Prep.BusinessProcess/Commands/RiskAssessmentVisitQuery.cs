using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;


namespace IQCare.Prep.BusinessProcess.Commands
{
   public  class RiskAssessmentVisitQuery:IRequest<Result<List<PrepRiskAssessmentEncounterView>>>
    {
        public int PersonId { get; set; }
    }


}

