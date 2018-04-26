using System;

namespace IQCare.SharedKernel.Model
{
    public class BaseEntity
    {
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
