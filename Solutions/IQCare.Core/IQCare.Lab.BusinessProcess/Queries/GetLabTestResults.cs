﻿using MediatR;
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
        public LabOrderStatus ? Status { get; set; }          
    }

    public class LabTestResultViewModel
    {
        public int LabOrderTestId { get; set; }
        public string LabTestName { get; set; }
        public DateTime? SampleDate { get; set; }
        public string ResultStatus { get; set; }
        public string ResultTexts { get; set; }
        public int LabTestId { get; set; }
        public decimal ResultValues { get; set; }
        public string ResultUnits { get; set; }
        public string ResultOptions { get; set; }
        public DateTime? ResultDate { get; set; }
        public string OrderReason { get; set; }
        
    }
}
