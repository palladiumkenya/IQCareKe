using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetLabTestResults : IRequest<List<LabTestResultViewModel>>
    {
        public int PatientId { get; set; }

    }

    public class LabTestResultViewModel
    {
        public int LabOrderTestId { get; set; }
        public string LabTestName { get; set; }
        public DateTime? SampleDate { get; set; }
        public string Reasons { get; set; }
        public string Results { get; set; }
        public string ResultTexts { get; set; }
        public int LabTestId { get; set; }
        public decimal ResultValues { get; set; }
        public string ResultUnits { get; set; }
        public string ResultOptions { get; set; }
        public DateTime? ResultDate { get; set; }
    }
}
