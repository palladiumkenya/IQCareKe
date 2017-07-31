using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Lookup
{
    [Serializable]
    [Table("TestingSummaryStatistics")]
    public class TestingSummaryStatistics
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
    }
}
