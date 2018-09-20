using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.IL
{
    [Serializable]
    [Table("vw_ILStatistics")]
    public class IlStatistics
    {
        [Key]
        public int Id { get; set; }
        public int Outbox { get; set; }
        public int Inbox { get; set; }
    }
}