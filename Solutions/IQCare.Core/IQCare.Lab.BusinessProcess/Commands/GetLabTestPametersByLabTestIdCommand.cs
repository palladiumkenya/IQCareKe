using System.Collections.Generic;
using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class GetLabTestPametersByLabTestIdCommand : IRequest<Result<List<LabTestParameter>>>
    {
        public int LabTestId { get; set; }
    }
}