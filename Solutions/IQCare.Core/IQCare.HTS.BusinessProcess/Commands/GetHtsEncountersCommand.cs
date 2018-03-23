using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetHtsEncountersCommand : IRequest<Result<List<HTSEncountersView>>>
    {
        public int PatientId { get; set; }
    }
}