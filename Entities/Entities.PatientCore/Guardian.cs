using Entities.Common;
using System;

namespace Entities.PatientCore
{
    public class Guardian : Person, IAuditEntity
    {
        public string AuditData { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        public string IdentificationNumber { get; set; }
        public override int Id { get; set; }
        public int PatientId { get; set; }
        public bool ParentFlag { get; set; }
    }
}
