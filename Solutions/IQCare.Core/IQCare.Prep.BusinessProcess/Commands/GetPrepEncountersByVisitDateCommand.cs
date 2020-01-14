using System;
using System.Collections.Generic;
using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
   public  class GetPrepEncountersByVisitDateCommand : IRequest<Result<List<PrepEncountersView>>>
    {
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }

        public DateTime? visitDate { get; set; }

       
    }
}