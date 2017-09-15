using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("lookupMasterItem")]
   public class LookupMasterItem
    {
        public int Id { get; set; }
        public int LookupmasterId { get; set; }
        public int LookupItemId { get; set; }
        public string DisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
