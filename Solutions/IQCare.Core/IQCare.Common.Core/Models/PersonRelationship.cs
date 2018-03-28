using System;

namespace IQCare.Common.Core.Models
{
    public class PersonRelationship
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public int RelationshipTypeId { get; set; }
        public int BaselineResult { get; set; }
        public DateTime? BaselineDate { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}