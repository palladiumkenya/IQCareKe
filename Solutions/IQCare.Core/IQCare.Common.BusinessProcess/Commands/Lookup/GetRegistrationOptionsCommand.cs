using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.Common.BusinessProcess.Commands.Lookup
{
    public class GetRegistrationOptionsCommand : IRequest<Result<GetRegistrationOptionsResponse>>
    {
        public string[] RegistrationOptions { get; set; }
    }

    public class GetRegistrationOptionsResponse
    {
        public List<KeyValuePair<string, List<LookupItemView>>> LookupItems { get; set; }
    }
}