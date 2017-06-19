using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("FacilityList")]
    public class FacilityList
    {
        [Key]
        public int Id { get; set; }
        public string MFLCode { get; set; }
        public string Name { get; set; }
    }
}
