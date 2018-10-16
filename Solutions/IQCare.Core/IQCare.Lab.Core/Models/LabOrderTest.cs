using System;

namespace IQCare.Lab.Core.Models
{
    public class LabOrderTest
    {
        public int Id { get; set; }

        public int LabOrderId { get; set; }

        public int LabTestId { get; set; }

        public string TestNotes { get; set; }

        public bool IsParent { get; set; }

        public int ParentTestId { get; set; }

        public bool DeleteFlag { get; set; }

        public string ResultNotes { get; set; }

        public int ResultBy { get; set; }

        public DateTime? ResultDate { get; set; }

        public string ResultStatus { get; set; }

        public int UserId { get; set; }

        public DateTime? StatusDate { get; set; }

        public string AuditDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }
    }
}