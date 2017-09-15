using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    public class PersonRelationship : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int PatientId { get; set; }
        public int RelationshipTypeId { get; set; }
        public int BaselineResult { get; set; }
        public DateTime? BaselineDate { get; set; }
    }
}
