using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace Config.Core.Model
{
    [Table("LookUpmasterItem")]
    class LookUpItemMaster:BaseEntity
    {
        public int LookupMasterId {get; set; }
        [ForeignKey("LookUpMasterId")]
        public int LookUpItemId { get; set; }
        [ForeignKey("LookupItemId")]
        public string DisplayName { get; set; }
        public decimal OrdRank { get; set; }
    }
}
