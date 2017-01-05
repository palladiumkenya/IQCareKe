using System;

namespace Entities.Common
{
    public interface IAuditEntity
    {
        int CreatedBy { get; set; }
        DateTime CreateDate { get; set; }
        bool DeleteFlag { get; set; }
        string AuditData { get; set; }
    }
}
