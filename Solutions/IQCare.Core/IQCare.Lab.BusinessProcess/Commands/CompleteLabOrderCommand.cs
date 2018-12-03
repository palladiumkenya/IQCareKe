using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class CompleteLabOrderCommand : IRequest<Result<CompleteLabOrderResponse>>
    {
        public int LabOrderId { get; set; }
        public int LabOrderTestId { get; set; }
        public int LabTestId { get; set; }
        public int UserId { get; set; }
        public List<AddLabTestResultCommand> LabTestResults { get; set; }
    }  

    public class AddLabTestResultCommand
    {
        public int ParameterId { get; set; }
        public decimal? ResultValue { get; set; }
        public string ResultText { get; set; }
        public int? ResultOptionId { get; set; }
        public string ResultOption { get; set; }
        public string ResultUnit { get; set; }
        public int? ResultUnitId { get; set; }
        public int? ResultConfigId { get; set; }
        public bool Undetectable { get; set; }
        public decimal DetectionLimit { get; set; }
    }

    public class CompleteLabOrderResponse
    {
        public string Message { get; set; }
        public int LabOrderId { get; set; }
    }
}
