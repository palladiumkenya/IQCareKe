using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetHtsEncounterDetailsViewByPersonIdCommand : IRequest<Result<List<EncountersDetailView>>>
    {
        public int personId{ get; set; }
    }
}
