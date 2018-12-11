using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using IQCare.Lab.Core.Models;

namespace IQCare.Lab.BusinessProcess.Commands
{
    public class AddLabOrderCommand : IRequest<Result<AddLabOrderResponse>>
    {
        public int Ptn_Pk { get; set; }
        public int PatientId { get; set; }
        public int LocationId { get; set; }
        public int VisitId { get; set; }
        public string Module { get; set; }
        public int OrderedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClinicalOrderNotes { get; set; }
        public string OrderStatus { get; set; }
        public int UserId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public List<LabTestInfo> LabTests{ get; set; }
    }

    public class LabTestInfo
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string LabTestName { get; set; }
    }

    public class AddLabOrderResponse
    {
        public int LabOrderId { get; set; }
        public List<LabOrderTest> LabOrderTests { get; set; }
    }
}