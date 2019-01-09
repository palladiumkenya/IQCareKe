using System.Collections.Generic;
using IQCare.Lab.Core.Models;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class GetLabTestPametersByLabTestIdCommand : IRequest<Result<List<LabTestParamaterViewModel>>>
    {
        public int LabTestId { get; set; }
    }

    public class LabTestParamaterViewModel
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string DataType { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int LabTestId { get; set; }
        public object ResultOptions { get; set; }
    }

}