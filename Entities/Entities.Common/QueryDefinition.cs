using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Common
{
    [Serializable]
    [Table("mst_QueryBuilderReports")]
    public class QueryDefinition:BaseObject
    {
        [Column("ReportId")]
        public override int Id { get; set; }
        [Column("ReportName")]
        public override string Name { get; set; }
       [Column("ReportQuery")]
        public string QueryText { get; set; }
      
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
       public override bool DeleteFlag
        {
            get
            {
                return base.DeleteFlag;
            }

            set
            {
                base.DeleteFlag = value;
            }
        }
        [Column("qryDescription")]
        public override string Description
        {
            get;
            set;
        }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual QueryCategory Category { get; set; }
    }
}
