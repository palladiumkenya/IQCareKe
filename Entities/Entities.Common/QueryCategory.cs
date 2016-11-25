using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    [Table("Mst_QueryBuilderCategory")]
    public class QueryCategory:BaseObject
    {
       [Column("CategoryId")]       
        [Key]
        public override int Id { get; set; }
        [Column("CategoryName")]
        public override string Name { get; set; }
        [NotMapped]
        public override bool Active
        {
            get
            {
                return !this.DeleteFlag;
            }

            set
            {
                this.DeleteFlag = !value;
            }
        }
        [NotMapped]public override string Description
        {
            get;
            set;
        }
        public virtual List<QueryDefinition> Queries { get; set; }
    }
}
