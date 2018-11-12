using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetHtsEncounterDetailsViewCommand : IRequest<Result<List<EncountersDetailView>>>
    {
        public int EncounterId { get; set; }
    }
}