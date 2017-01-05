using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace Config.Core.Model
{
    [Table("LookUpItem")]
    class LookUpItem:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
