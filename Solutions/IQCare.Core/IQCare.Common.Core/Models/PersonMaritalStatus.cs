using System;

namespace IQCare.Common.Core.Models
{
    public class PersonMaritalStatus
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int MaritalStatusId { get; set; }

        public bool Active { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string AuditData { get; set; }
    }
}