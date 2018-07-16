using System.Collections.Generic;
using IQCare.Common.Core.Models;
using IQCare.HTS.Core.Model;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetPersonTracingListCommand : IRequest<Result<List<PersonTracingView>>>
    {
        public int PersonId { get; set; }
    }
}