using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.Lab.BusinessProcess.Queries
{
    public class GetLabOrdersQuery : IRequest<Result<List<LabOrderViewModel>>>
    {
        public int PatientId { get; set; }
        public string OrderStatus { get; set; }
    }

    public class GetLabTestByOrderId : IRequest<Result<List<LabOrderTestViewModel>>>
    {
        public int Id { get; set; }
        
    }

    public class LabOrderViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string DateCreated { get; set; }
        public string OrderNotes { get; set; }
        public string OrderStatus { get; set; }
        public string OrderNumber { get; set; }
        public List<LabOrderTestViewModel> LabTests { get; set; }

    }

    public class LabOrderTestViewModel
    {

        public int Id { get; set; }

        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public string LabTestName { get; set; }
        public string TestNotes { get; set; }
        public string ResultNotes { get; set; }
        public int ResultBy { get; set; }
        public DateTime? ResultDate { get; set; }
        public string ResultStatus { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
