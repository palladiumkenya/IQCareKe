using System;

namespace IQCare.Common.Core.Models
{
    public class ServiceArea
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public bool DeleteFlag { get; set; }

        public string AuditData { get; set; }
    }
}