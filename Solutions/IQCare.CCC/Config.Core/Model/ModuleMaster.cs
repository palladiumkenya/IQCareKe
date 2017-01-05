using System;
using System.ComponentModel.DataAnnotations;
using Common.Core.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Config.Core.Model
{
    [Table("ModuleMaster")]
    class ModuleMaster:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public String ReferenceId { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string ReleaseVersion { get; set; }

    }
}
