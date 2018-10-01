using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Records
{
    [Serializable]
    [Table("LookupItemView")]
    public class LookupItemView
    {
        public int MasterId { get; set; }

        [Key]
        public Int64 RowId { get; set; }
        public int ItemId { get; set; }
        public string MasterName { get; set; }
        public string ItemName { get; set; }
        public string DisplayName { get; set; }
        public string ItemDisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
