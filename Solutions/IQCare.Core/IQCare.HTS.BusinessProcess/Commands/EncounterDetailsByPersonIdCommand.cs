using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.BusinessProcess.Commands
{
   public  class EncounterDetailsByPersonIdCommand : IRequest<Result<List<EncountersDetailView>>>
    {
        public int personId { get; set; }
    }
}
