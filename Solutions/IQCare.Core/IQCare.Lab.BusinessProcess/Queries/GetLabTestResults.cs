using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.Lab.Core.Models;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetLabTestResults : IRequest<Result<List<LabTestResultViewModel>>>
    {
        public int PatientId { get; set; }
        public  string LabOrderStatus { get; set; }            
    }

    public class GetLabOrderTestResults : IRequest<Result<List<LabTestResultViewModel>>>
    {
        public int LabOrderTestId { get; set; }
    }
    public class LabTestResultViewModel
    {

        public int LabOrderId { get; set; }
        public int LabOrderTestId { get; set; }
        public string LabTestName { get; set; }
        public string Parameter { get; set; }
        public DateTime? SampleDate { get; set; }
        public string OrderDate { get; set; }
        public string ResultStatus { get; set; }
        public string Result { get; set; }
        public string ResultUnits { get; set; }
        public int LabTestId { get; set; }
        public DateTime? ResultDate { get; set; }
        public string OrderReason { get; set; }
        
    }
}
