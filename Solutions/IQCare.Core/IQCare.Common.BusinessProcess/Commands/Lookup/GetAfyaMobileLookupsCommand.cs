using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetAfyaMobileLookupsCommand : IRequest<Result<List<LookupItemView>>>
    {
        public string[] options { get; set; }
    }
}