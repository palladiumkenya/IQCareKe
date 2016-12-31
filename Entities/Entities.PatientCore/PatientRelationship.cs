
using Entities.Common;
using System;

namespace Entities.PatientCore
{
    [Serializable]
    public class PatientRelationship : IAuditEntity
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Relationship Relationship { get; set; }
        public string AuditData
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public int CreatedBy
        {
            get; set;
        }

        public bool DeleteFlag
        {
            get; set;
        }
    }
}
