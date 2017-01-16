using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("LookupItem")]
    public class LookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeleteFlag { get; set; }
    }
}
