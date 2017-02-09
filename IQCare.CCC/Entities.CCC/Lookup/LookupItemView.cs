using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("LookupItemView")]
    
    public class LookupItemView
    {
        public int MasterId { get; set ; }

        [Key]
        public Int64 RowID { get; set; }
        public int ItemId { get; set ; }
        public string MasterName { get; set; }
        public string ItemName { get; set; }
        public string DisplayName { get; set; }
        public string ItemDisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
