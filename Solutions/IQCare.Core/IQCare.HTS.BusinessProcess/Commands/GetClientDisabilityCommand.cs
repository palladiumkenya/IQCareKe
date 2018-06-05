using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetClientDisabilityCommand : IRequest<Result<List<ClientDisability>>>
    {
        public int PersonId { get; set; }
    }
}