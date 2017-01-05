using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace PersonManagement.Core.Model
{
    [Table("PersonRelationship")]
    public class PersonRelationship : BaseEntity
    {
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public int RelatedTo { get; set; }

        [ForeignKey("RelatedTo")]
        public int RelationshipTypeId { get; set; }

       
    }
}

