using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("PatientStabilitySummary")]
    public class PatientStabilitySummary
    {
        [Key]
        public Int64 Id { get; set; }
        public int? Value { get; set; }
        public string Category { get; set; }
    }
}
