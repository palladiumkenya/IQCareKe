using System;
using Common.Core.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Config.Core.Model
{
    [Table("ModuleMaster")]
    class ModuleMaster:BaseEntity
    {
        public string Name { get; set; }
        public String ReferenceId { get; set; }
        public string DisplayName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseVersion { get; set; }

    }
}
