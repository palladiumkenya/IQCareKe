using System.ComponentModel.DataAnnotations.Schema;
using Common.Core.Model;

namespace Config.Core.Model
{
    [Table("LookUpmaster")]
    class LookUpMaster:BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
