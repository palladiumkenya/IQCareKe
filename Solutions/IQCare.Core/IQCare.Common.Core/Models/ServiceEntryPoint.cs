using System;

namespace IQCare.Common.Core.Models
{
    public class ServiceEntryPoint
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public int EntryPointId { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Active { get; set; }

        public string AuditData { get; set; }
    }
}