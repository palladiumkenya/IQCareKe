using System;
using System.Collections.Generic;

namespace IQCare.Lab.Core.Models
{
    public enum ResultStatusEnum
    {
        Received
    }
    public class LabOrderTest
    {
        public LabOrderTest()
        {

        }

        public LabOrderTest(int labOrderId, int labTestId, string testNotes,int createdBy, bool isParent)
        {
            LabOrderId = labOrderId;
            LabTestId = labTestId;
            TestNotes = testNotes;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
            DeleteFlag = false;
            IsParent = isParent;
        }
        public int Id { get; private set; }

        public int LabOrderId { get; private set; }

        public int LabTestId { get; private set; }

        public string TestNotes { get; private set; }

        public bool IsParent { get; private set; }

        public int? ParentTestId { get; private set; }

        public bool DeleteFlag { get; private set; }

        public string ResultNotes { get; private set; }

        public int? ResultBy { get; private set; }

        public DateTime? ResultDate { get; private set; }

        public string ResultStatus { get; private set; }

        public int UserId { get; private set; }

        public DateTime? StatusDate { get; private set; }

        public string AuditData { get; private set; }

        public DateTime CreateDate { get; private set; }
        public virtual ICollection<LabOrderTestResult> LabOrderTestResults { get; set; }
        public int CreatedBy { get; private set; }
        public virtual LabOrder LabOrder { get; set; }

        public void ReceiveResult(int receivedBy,DateTime resultDate)
        {
            ResultBy = receivedBy;
            ResultDate = resultDate;
        }

        public void MarkAsReceived()
        {
            ResultStatus = "Received";
        }
    }
}