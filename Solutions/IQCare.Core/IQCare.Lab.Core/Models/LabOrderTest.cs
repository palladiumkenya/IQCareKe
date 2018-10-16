using System;

namespace IQCare.Lab.Core.Models
{
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
            ResultStatus = "Received";
        }
        public int Id { get; private set; }

        public int LabOrderId { get; private set; }

        public int LabTestId { get; private set; }

        public string TestNotes { get; private set; }

        public bool IsParent { get; private set; }

        public int ParentTestId { get; private set; }

        public bool DeleteFlag { get; private set; }

        public string ResultNotes { get; private set; }

        public int ResultBy { get; private set; }

        public DateTime? ResultDate { get; private set; }

        public string ResultStatus { get; private set; }

        public int UserId { get; private set; }

        public DateTime? StatusDate { get; private set; }

        public string AuditDate { get; private set; }

        public DateTime CreateDate { get; private set; }

        public int CreatedBy { get; private set; }
    }
}