using System.Collections.Generic;
using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class GetLabTestsCommand : IRequest<Result<GetLabTestsResponse>>
    {
        public string[] LabTests { get; set; }
    }

    public class GetLabTestsResponse
    {
        public List<KeyValuePair<string, List<LabTest>>> LabTestsList { get; set; }
    }
}