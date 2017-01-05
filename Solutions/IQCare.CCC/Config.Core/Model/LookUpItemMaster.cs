using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace Config.Core.Model
{
    [Table("LookUpmasterItem")]
    class LookUpItemMaster:BaseEntity
    {
        [Required]
        public int LookupMasterId {get; set; }
        [ForeignKey("LookUpMasterId")]

        [Required]
        public int LookUpItemId { get; set; }
        [ForeignKey("LookupItemId")]

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public decimal OrdRank { get; set; }
    }
}
