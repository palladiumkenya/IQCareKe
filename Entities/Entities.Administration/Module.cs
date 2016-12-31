using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Administration
{
    [Serializable]
    [Table("ModuleMaster")]
    public class Module:BaseObject
    {
        public string Version { get; set; }
        public string VersionDate { get; set; }
    }
}
