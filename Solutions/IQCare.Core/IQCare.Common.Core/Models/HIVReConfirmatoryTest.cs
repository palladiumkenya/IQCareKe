using System;

namespace IQCare.Common.Core.Models
{
    public class HIVReConfirmatoryTest
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int TypeOfTest { get; set; }
        public int TestResult { get; set; }
        public DateTime TestResultDate { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}