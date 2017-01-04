using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace Config.Core.Model
{
    [Table("LookUpItem")]
    class LookUpItem:BaseEntity
    {
        public string Name { get; set; }
    }
}
