using System;

namespace Entities.Common
{
    [Serializable]
    public class PersonRelationship : IAuditEntity
    {
        public int Id { get; set; }
       
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public Relationship Relationship { get; set; }
        public Person Relative { get; set; }
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
