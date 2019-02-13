using System.Collections.Generic;
using IQCare.HTS.Core.Model;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class GetHivPositiveListCommand : IRequest<Result<List<HivPositiveListView>>>
    {
        public int PersonId { get; set; }
    }
}