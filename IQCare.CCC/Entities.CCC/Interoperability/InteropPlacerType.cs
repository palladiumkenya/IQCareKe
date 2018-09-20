using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("Interop_PlacerType")]
    public class InteropPlacerType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
